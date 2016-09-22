using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Meta;

namespace Scaffolder.Core.Sql
{
   
    public class SqlSchemaBuilder : ISchemaBuilder
    {
        private readonly IDatabase _db;

        public SqlSchemaBuilder(String connectrionString)
        {
            _db = new SqlDatabase(connectrionString);
        }

        public Schema Build()
        {
            var database = new Schema();

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

        private IEnumerable<String> GetDatabaseTables()
        {
            var sql = "SELECT TABLE_NAME FROM information_schema.tables WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME != 'sysdiagrams'";

            var tables = _db.Execute(sql, r => r[0].ToString());
            return tables;
        }

        private static Table MapTableColumns(IDataRecord r, Table t)
        {

            var column = new Column
            {
                Type = ParseColumnType(r["DATA_TYPE"].ToString()),
                Position = 1,
                Name = r["COLUMN_NAME"].ToString(),
                Title = r["COLUMN_NAME"].ToString(),
                IsNullable = r["IS_NULLABLE"].ToString() == "YES",
                ShowInGrid = true,
                IsKey = false,
                Description = ""
            };

            if (column.Type == ColumnType.DateTime)
            {
                column.MinValue = new DateTime(1753, 1, 1);
            }

            if (t.Columns.Count > 0)
            {
                column.Position = t.Columns.Max(o => o.Position) + 1;
            }

            t.Columns.Add(column);
            return t;
        }
        
        private static ColumnType ParseColumnType(string type)
        {
            if (type.ToLower() == "nvarchar")
            {
                return ColumnType.Text;
            }
            else if (type.ToLower() == "int")
            {
                return ColumnType.Integer;
            }
            else if (type.ToLower() == "datetime")
            {
                return ColumnType.DateTime;
            }
            else if (type.ToLower() == "float")
            {
                return ColumnType.Double;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
