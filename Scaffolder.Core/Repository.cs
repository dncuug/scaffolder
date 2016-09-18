using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using Scaffolder.Core.Base;

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
            var query = _queryBuilder.Build(_table, filter);
            var result = _db.Execute(query, Map, filter.Parameters).ToList();
            return result;
        }

        private dynamic Map(IDataReader r)
        {
            var obj = new ExpandoObject();

            foreach (var c in _table.Columns)
            {
                AddProperty(obj, c.Name, r[c.Name]);
            }

            return obj;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        public dynamic Insert(dynamic obj)
        {
            throw new NotImplementedException();
        }

        public dynamic Update(dynamic obj)
        {
            throw new NotImplementedException();
        }

        public dynamic Delete(dynamic obj)
        {
            throw new NotImplementedException();
        }
    }
}
