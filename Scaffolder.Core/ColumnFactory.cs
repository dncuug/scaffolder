using Scaffolder.Core.Base;
using System;
using System.Data;

namespace Scaffolder.Core
{
    public class ColumnFactory
    {
        public Column CreateColumn(IDataReader r)
        {
            var type = ParseColumnType(r["DATA_TYPE"].ToString());

            switch (type)
            {
                case ColumnType.Email:
                case ColumnType.Url:
                case ColumnType.Phone:
                case ColumnType.Text:
                case ColumnType.HTML:
                    return CreateTextColumn(r);
                case ColumnType.DateTime:
                    return CreateDateColumn(r);
                case ColumnType.File:
                case ColumnType.Image:
                    return CreateFileColumn(r);
                case ColumnType.Integer:
                    return CreateIntegerColumn(r);
                case ColumnType.Double:
                    return CreateDoubleColumn(r);
                case ColumnType.Binary:
                    return CreateBaseColumn(r);
                default:
                    throw new NotImplementedException();
            }
        }

        private FileColumn CreateFileColumn(IDataReader r)
        {
            var column = new FileColumn();
            MapBaseProperties(column, r);
            return column;
        }

        private IntegerColumn CreateIntegerColumn(IDataReader r)
        {
            var column = new IntegerColumn();
            MapBaseProperties(column, r);
            return column;
        }

        private DoubleColumn CreateDoubleColumn(IDataReader r)
        {
            var column = new DoubleColumn();
            MapBaseProperties(column, r);
            return column;
        }

        private DateColumn CreateDateColumn(IDataReader r)
        {
            var column = new DateColumn();
            MapBaseProperties(column, r);
            column.MinValue = new DateTime(1753, 1, 1);
            return column;
        }

        private TextColumn CreateTextColumn(IDataReader r)
        {
            var column = new TextColumn();
            MapBaseProperties(column, r);
            return column;
        }

        private Column CreateBaseColumn(IDataReader r)
        {
            var column = new Column();
            MapBaseProperties(column, r);
            return column;
        }

        public void MapBaseProperties(Column column, IDataReader r)
        {
            column.Name = r["COLUMN_NAME"].ToString();
            column.AllowNullValue = r["IS_NULLABLE"].ToString() == "YES";

            column.Type = ParseColumnType(r["DATA_TYPE"].ToString());

            column.Title = column.Name;
        }

        private ColumnType ParseColumnType(string type)
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
