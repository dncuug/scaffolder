using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Scaffolder.Core.Data
{
    public abstract class DatabaseBase<TConnectoinType, TCommandType> : IDatabase
        where TCommandType : DbCommand
        where TConnectoinType : DbConnection, new()
    {
        protected String ConnectionString { get; private set; }

        public DatabaseBase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public virtual Object ExecuteScalar(string sql, Dictionary<String, Object> parameters = null)
        {
            using (var connection = CreateConnection(ConnectionString))
            {
                var command = CreateCommand(connection, sql, parameters);

                connection.Open();
                var result = command.ExecuteScalar();
                connection.Close();

                return result;
            }
        }

        public virtual void ExecuteNonQuery(string sql, Dictionary<String, Object> parameters = null)
        {
            using (var connection = CreateConnection(ConnectionString))
            {
                var command = CreateCommand(connection, sql, parameters);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public virtual IEnumerable<T> Execute<T>(string sql, Func<IDataReader, T> map, Dictionary<String, Object> parameters = null)
        {
            var result = new List<T>();

            using (var connection = CreateConnection(ConnectionString))
            {
                var command = CreateCommand(connection, sql, parameters);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (map != null)
                        {
                            result.Add(map(reader));
                        }
                    }
                }

                connection.Close();
            }

            return result;
        }

        protected abstract TConnectoinType CreateConnection(string connectionString);

        protected abstract TCommandType CreateCommand(TConnectoinType connection, String sql, Dictionary<String, Object> parameters = null);
    }
}