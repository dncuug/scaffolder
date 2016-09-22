using System;
using System.Collections.Generic;
using System.Data;
using Scaffolder.Core.Data;

namespace Scaffolder.Core.Engine.MySql
{
    public class MySqlDatabase : IDatabase
    {
        public object ExecuteScalar(string sql, Dictionary<string, object> parameters = null)
        {
            throw new NotImplementedException();
        }

        public void ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Execute<T>(string sql, Func<IDataReader, T> map, Dictionary<string, object> parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}
