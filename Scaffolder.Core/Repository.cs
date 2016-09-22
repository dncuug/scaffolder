using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;

namespace Scaffolder.Core
{
    public class Repository
    {
        private readonly Data.SqlServerDatabase _db;
        private readonly Table _table;
        private readonly QueryBuilder _queryBuilder;

        public Repository(Data.SqlServerDatabase db, Table table)
        {
            _db = db;
            _table = table;
            _queryBuilder = new QueryBuilder();
        }
        
        public IEnumerable<dynamic> Select(Filter filter)
        {
            var query = _queryBuilder.Build(Query.Select, _table, filter);

            var parameters = filter.Parameters.ToDictionary(x => "@" + x.Key, x => x.Value);
            var result = _db.Execute(query, r => Map(r, filter.DetailMode), parameters).ToList();
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
            var autoIncrementColumns = _table.Columns.Where(c => c.AutoIncrement == true).ToList();
            var parameters = GetParameters(obj).Where(p => autoIncrementColumns.All(c => c.Name != p.Key)).ToDictionary(x => x.Key, x => x.Value);

            var query = _queryBuilder.Build(Query.Update, _table);

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
                if (c.ShowInGrid == true || loadAllColumns)
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
