using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Meta;

namespace Scaffolder.Core.Engine.Sql
{
    public class SqlQueryBuilder : IQueryBuilder
    {
        private String BuildSelect(Table table, Filter filter)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("SElECT ");

            var columns = table.Columns.Where(o => o.ShowInGrid == true || filter.DetailMode || o.IsKey == true).Select(o => o.Name).ToList();

            sb.Append(String.Join(", ", columns));

            sb.AppendFormat(" FROM [{0}] ", filter.TableName);

            var whereCaluses = filter.Parameters.Select(o => BuildClause(table, o)).Where(o => !String.IsNullOrEmpty(o)).ToList();

            if (whereCaluses.Any())
            {
                sb.AppendFormat(" WHERE {0}", String.Join(" AND ", whereCaluses));
            }

            return sb.ToString();
        }

        public String Build(Query query, Table table, Filter filter = null)
        {
            switch (query)
            {
                case Query.Select:
                    return BuildSelect(table, filter);
                case Query.Insert:
                    return BuildInsert(table);
                case Query.Update:
                    return BuildUpdate(table);
                case Query.Delete:
                    return BuildDelete(table);
            }

            throw new NotSupportedException("Unknown query type");
        }

        private string BuildInsert(Table table)
        {
            //var keyFields = table.Columns.Where(o => o.IsKey == true).ToList();
            //var parameters = new Dictionary<string, object>();
            //var select = BuildSelect(table, new Filter {TableName = table.Name , Parameters = parameters } );

            var sb = new StringBuilder();

            var fields = table.Columns.Where(o => o.AutoIncrement != true).ToList();

            sb.AppendFormat("INSERT INTO [{0}] ({1})", table.Name, String.Join(", ", fields.Select(o => o.Name)));
            sb.AppendFormat(" OUTPUT INSERTED.* ");
            sb.AppendFormat(" VALUES({0})", String.Join(", ", fields.Select(o => "@" + o.Name)));

            return sb.ToString();
        }

        private string BuildUpdate(Table table)
        {
            var sb = new StringBuilder();

            var fields = table.Columns.Where(o => o.AutoIncrement != true).ToList();
            var keyFields = table.Columns.Where(o => o.IsKey == true).ToList();

            sb.AppendFormat("UPDATE [{0}] SET {1}", table.Name, String.Join(", ", fields));
            sb.AppendLine(String.Join(", ", fields.Select(o => String.Format("[{0}] = @{0}", o.Name))));
            sb.AppendFormat(" OUTPUT INSERTED.* ");
            sb.AppendFormat(" WHERE");
            sb.AppendLine(String.Join(", ", keyFields.Select(o => String.Format("[{0}] = @{0}", o.Name))));

            return sb.ToString();
        }

        private string BuildDelete(Table table)
        {
            var sb = new StringBuilder();

            var keyFields = table.Columns.Where(o => o.IsKey == true).ToList();

            sb.AppendFormat("DELETE FROM [{0}] ", table.Name);
            sb.AppendFormat(" OUTPUT DELETED.* ");
            sb.AppendFormat(" WHERE");
            sb.AppendLine(String.Join(", ", keyFields.Select(o => String.Format("[{0}] = @{0}", o.Name))));

            return sb.ToString();
        }

        private string BuildClause(Table table, KeyValuePair<string, object> p)
        {
            var column = table.GetColumn(p.Key);

            if (column == null)
            {
                return String.Empty;
            }

            if (column.Type == ColumnType.Text)
            {
                return String.Format("[{0}] LIKE @{0}", p.Key);
            }

            return String.Format("[{0}] = @{0}", p.Key);
        }
    }
}
