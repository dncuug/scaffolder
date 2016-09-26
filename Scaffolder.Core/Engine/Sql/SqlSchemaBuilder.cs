using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Meta;

namespace Scaffolder.Core.Engine.Sql
{
    public class SqlSchemaBuilder : SchemeBuilderBase
    {
        public SqlSchemaBuilder(IDatabase db)
            : base(db)
        {
        }

        protected override Table GetDataTable(string name)
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

            IEnumerable<string> keyColumns = GetTablePrimaryKeys(name);
            IEnumerable<string> identityColumns = GetTableIdentityColumns(name);

            _db.Execute(sql, r => MapTableColumns(r, table, keyColumns, identityColumns), parameters);

            return table;
        }

        protected override IEnumerable<string> GetTablePrimaryKeys(string name)
        {
            var sql = @"SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
                JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu ON tc.CONSTRAINT_NAME = ccu.Constraint_name
            WHERE tc.CONSTRAINT_TYPE = 'Primary Key'
            AND tc.TABLE_NAME = @TableName";

            var parameters = new Dictionary<string, object>
            {
                 { "@TableName", name }
            };

            return _db.Execute(sql, r => r["COLUMN_NAME"].ToString(), parameters).ToList();
        }

        private IEnumerable<string> GetTableIdentityColumns(string name)
        {
            var sql = @"select COLUMN_NAME
                from INFORMATION_SCHEMA.COLUMNS
                where TABLE_SCHEMA = 'dbo'
                and COLUMNPROPERTY(object_id(TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1
                AND TABLE_NAME = @TableName
                order by TABLE_NAME";

            var parameters = new Dictionary<string, object>
            {
                 { "@TableName", name }
            };

            return _db.Execute(sql, r => r["COLUMN_NAME"].ToString(), parameters).ToList();
        }

        protected override IEnumerable<String> GetDatabaseTables()
        {
            var sql = "SELECT TABLE_NAME FROM information_schema.tables WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME != 'sysdiagrams'";

            var tables = _db.Execute(sql, r => r[0].ToString());
            return tables;
        }

        private static Table MapTableColumns(IDataRecord r, Table t, IEnumerable<string> keyColumns, IEnumerable<string> identityColumns)
        {
            var columnName = r["COLUMN_NAME"].ToString();
            var columnType = ParseColumnType(r["DATA_TYPE"].ToString(), r["COLUMN_NAME"].ToString());

            var column = new Column
            {
                Type = columnType,
                Position = 1,
                Name = columnName,
                Title = columnName,
                IsNullable = r["IS_NULLABLE"].ToString() == "YES",
                ShowInGrid = ShowInGrid(columnType, columnName),
                IsKey = keyColumns.Contains(columnName),
                Readonly = false,
                AutoIncrement = identityColumns.Contains(columnName),
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

        private static ColumnType ParseColumnType(string type, string name)
        {
            name = name.ToLower();
            type = type.ToLower();

            if (type == "nvarchar" || type == "varchar" || type == "ntext" || type == "text" || type == "nchar")
            {
                if (name.Contains("password"))
                    return ColumnType.Password;

                if (name.Contains("content") || name.Contains("html"))
                    return ColumnType.HTML;

                if (name.Contains("image") || name.Contains("picture")
                || name.Contains("logo") || name.Contains("background"))
                    return ColumnType.Image;

                if (name.Contains("phone"))
                    return ColumnType.Phone;

                if (name.Contains("email"))
                    return ColumnType.Email;

                if (name.Contains("link") || name.Contains("url"))
                    return ColumnType.Url;

                return ColumnType.Text;
            }
            else if (type == "int")
            {
                return ColumnType.Integer;
            }
            else if (type == "datetime")
            {
                return ColumnType.DateTime;
            }
            else if (type == "float")
            {
                return ColumnType.Double;
            }
            if (type == "bit")
            {
                return ColumnType.Boolean;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
