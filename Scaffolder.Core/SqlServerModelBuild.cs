using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Scaffolder.Core.Data;

namespace Scaffolder.Core
{
    public class SqlServerModelBuild
    {
        private SqlServerDatabase _db;

        public SqlServerModelBuild(String connectrionString)
        {
            _db = new SqlServerDatabase(connectrionString);
        }

        public IEnumerable<Table> Build()
        {
            var result = new List<Table>();

            var tableList = GetDatabaseTables();

            foreach (var name in tableList)
            {
                var table = GetDataTable(name);

                result.Add(table);
            }

            return result;
        }

        private Table GetDataTable(string name)
        {
            var sql = @"
                        SELECT COLUMN_NAME
	                           IS_NULLABLE,
                               DATA_TYPE,
                               CHARACTER_MAXIMUM_LENGTH
                          FROM INFORMATION_SCHEMA.COLUMNS
                         WHERE TABLE_NAME = @TableName";


            var table = new Table(name);
             _db.Execute(sql, r => MapTable(r, table), new Dictionary<string, object> { { "@TableName", name } });

        }

        private Table MapTable(IDataReader arg, Table t)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<String> GetDatabaseTables()
        {
            var sql = "SELECT TABLE_NAME FROM information_schema.tables WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME != 'sysdiagrams'";

            var tables = _db.Execute(sql, r => r[0].ToString());
            return tables;

        }
    }
}
