using System;
using System.Collections.Generic;
using System.Data;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;

namespace Scaffolder.Core
{
    public class SqlServerModelBuild
    {
        private readonly SqlServerDatabase _db;

        public SqlServerModelBuild(String connectrionString)
        {
            _db = new SqlServerDatabase(connectrionString);
        }

        public Database Build()
        {
            var database = new Database();

            var tableList = GetDatabaseTables();

            foreach (var name in tableList)
            {
                var table = GetDataTable(name);
                database.Tables.Add(table);
            }

            return database;
        }

        private Table GetDataTable(string name)
        {
            var sql = @"
                        SELECT COLUMN_NAME,
	                           IS_NULLABLE,
                               DATA_TYPE,
                               CHARACTER_MAXIMUM_LENGTH
                          FROM INFORMATION_SCHEMA.COLUMNS
                         WHERE TABLE_NAME = @TableName";


            var table = new Table(name);

            var parameters = new Dictionary<string, object>
            {
                 { "@TableName", name }
            };

            _db.Execute(sql, r => MapTableColumns(r, table), parameters);

            return table;
        }

        private Table MapTableColumns(IDataReader r, Table t)
        {
            var factory = new ColumnFactory();
            var column = factory.CreateColumn(r);
            t.Columns.Add(column);
            return t;
        }

        private IEnumerable<String> GetDatabaseTables()
        {
            var sql = "SELECT TABLE_NAME FROM information_schema.tables WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME != 'sysdiagrams'";

            var tables = _db.Execute(sql, r => r[0].ToString());
            return tables;
        }
    }
}
