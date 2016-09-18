using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

        public dynamic Select(Filter filter)
        {
            throw new NotImplementedException();
            //var query = _queryBuilder.Build(filter);
            //_db.Execute(query, Map, filter.Parameters);
        }

        private dynamic Map(IDataReader r)
        {
            throw new NotImplementedException();
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
