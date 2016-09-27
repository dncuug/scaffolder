using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Scaffolder.Core.Base;
using Scaffolder.Core.Meta;

namespace Scaffolder.Core.Data
{
    public interface IRepository
    {
        IEnumerable<object> Select(Filter filter);
        dynamic Insert(Object obj);
        dynamic Update(Object obj);
        dynamic Delete(Object obj);

        int GetRecordCount(Filter filter);
    }

    public class Repository : IRepository
    {
        private readonly IDatabase _db;
        private readonly IQueryBuilder _queryBuilder;
        private readonly Table _table;

        public Repository(Data.IDatabase db, IQueryBuilder queryBuilder, Table table)
        {
            _db = db;
            _table = table;
            _queryBuilder = queryBuilder;
        }

        public IEnumerable<dynamic> Select(Filter filter)
        {
            var query = _queryBuilder.Build(Query.Select, _table, filter);

            var parameters = filter.Parameters.ToDictionary(x => "@" + x.Key, x => x.Value);
            var result = _db.Execute(query, r => Map(r, filter.DetailMode), parameters).ToList();
            return result;
        }

        public int GetRecordCount(Filter filter)
        {
            var query = _queryBuilder.BuildRecordCountQuery(_table, filter);

            var parameters = filter.Parameters.ToDictionary(x => "@" + x.Key, x => x.Value);
            var result = Convert.ToInt32(_db.ExecuteScalar(query, parameters));
            return result;
        }

        public dynamic Insert(Object obj)
        {
            var autoIncrementColumns = _table.Columns.Where(c => c.AutoIncrement == true).ToList();
            var parameters = GetParameters(obj).Where(p => autoIncrementColumns.All(c => c.Name != p.Key)).ToDictionary(x => x.Key, x => x.Value);

            var query = _queryBuilder.Build(Query.Insert, _table);

            var result = _db.Execute(query, r => Map(r, true), parameters).FirstOrDefault();
            return result;
        }

        public dynamic Update(Object obj)
        {
            var autoIncrementColumns = _table.Columns.Where(c => c.AutoIncrement == true && c.IsKey != true).ToList();
            var parameters = GetParameters(obj).Where(p => autoIncrementColumns.All(c => c.Name != p.Key)).ToDictionary(x => x.Key, x => x.Value);

            var query = _queryBuilder.Build(Query.Update, _table, null, parameters);

            var result = _db.Execute(query, r => Map(r, true), parameters).FirstOrDefault();
            return result;
        }

        public dynamic Delete(Object obj)
        {
            var keyColumns = _table.Columns.Where(c => c.IsKey == true).ToList();
            var parameters = GetParameters(obj).Where(p => keyColumns.Any(k => k.Name == p.Key)).ToDictionary(x => x.Key, x => x.Value); ;

            var query = _queryBuilder.Build(Query.Delete, _table);

            var result = _db.Execute(query, r => Map(r, true), parameters).FirstOrDefault();
            return result;
        }

        private dynamic Map(IDataRecord r, bool loadAllColumns)
        {
            var obj = new ExpandoObject();

            foreach (var c in _table.Columns)
            {
                if (c.ShowInGrid == true && c.Reference != null)
                {
                    AddProperty(obj, $"{c.Reference.GetColumnAlias()}" , r[$"{c.Reference.GetColumnAlias()}"]);
                }
                else if (c.ShowInGrid == true || c.IsKey == true || loadAllColumns)
                {
                    AddProperty(obj, c.Name, r[c.Name]);
                }
            }

            return obj;
        }

        private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;

            if (expandoDict.ContainsKey(propertyName))
            {
                expandoDict[propertyName] = propertyValue;
            }
            else
            {
                expandoDict.Add(propertyName, propertyValue);
            }
        }

        private static Dictionary<string, object> GetParameters(Object obj)
        {
            var type = obj.GetType();

            if (type == typeof(Newtonsoft.Json.Linq.JObject))
            {
                return ((Newtonsoft.Json.Linq.JObject)obj).ToObject<Dictionary<string, object>>();
            }

            var properties = type.GetProperties();

            var result = new Dictionary<String, Object>();

            foreach (var p in properties)
            {
                result.Add(p.Name, p.GetValue(obj));
            }

            return result;
        }
    }
}
