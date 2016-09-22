using System;
using System.Collections.Generic;
using System.Data;

namespace Scaffolder.Core.Data
{
    public interface IDatabase
    {
        Object ExecuteScalar(string sql, Dictionary<String, Object> parameters = null);
        void ExecuteNonQuery(string sql, Dictionary<String, Object> parameters = null);
        IEnumerable<T> Execute<T>(string sql, Func<IDataReader, T> map, Dictionary<String, Object> parameters = null);
    }

}
