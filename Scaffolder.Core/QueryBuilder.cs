using Scaffolder.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scaffolder.Core.Data;

namespace Scaffolder.Core
{
    public class QueryBuilder
    {
        private String BuildSelect(Table table, Filter filter)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("SElECT ");

            var columns = table.Columns.Where(o => o.ShowInGrid == true || filter.DetailMode).Select(o => o.Name);

            sb.Append(String.Join(", ", columns));

            sb.AppendFormat(" FROM [{0}] ", filter.TableName);

            var whereCaluses = filter.Parameters.Select(o => BuildClause(table, o)).ToList();

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
            var sb = new StringBuilder();

            var fields = table.Columns.Where(o => o.AutoIncrement != true).ToList();

            sb.AppendFormat("INSERT INTO [{0}] ({1})", table.Name, String.Join(", ", fields));
            sb.AppendFormat(" VALUES({0})", String.Join(", ", fields));

            return sb.ToString();
        }

        private string BuildUpdate(Table table)
        {
            var sb = new StringBuilder();

            var fields = table.Columns.Where(o => o.AutoIncrement != true).ToList();
            var keyFields = table.Columns.Where(o => o.IsKey == true).ToList();

            sb.AppendFormat("UPDATE [{0}] SET {1}", table.Name, String.Join(", ", fields));
            sb.AppendLine(String.Join(", ", fields.Select(o => String.Format("[{0}] = @{0}", o.Name))));
            sb.AppendFormat(" WHERE");
            sb.AppendLine(String.Join(", ", keyFields.Select(o => String.Format("[{0}] = @{0}", o.Name))));

            return sb.ToString();
        }

        private string BuildDelete(Table table)
        {
            var sb = new StringBuilder();

            var keyFields = table.Columns.Where(o => o.IsKey == true).ToList();

            sb.AppendFormat("DELETE FROM [{0}] ", table.Name);
            sb.AppendFormat(" WHERE");
            sb.AppendLine(String.Join(", ", keyFields.Select(o => String.Format("[{0}] = @{0}", o.Name))));

            return sb.ToString();
        }

        private string BuildClause(Table table, KeyValuePair<string, object> p)
        {
            var column = table.GetColumn(p.Key);

            if (column.Type == ColumnType.Text)
            {
                return String.Format("[{0}] LIKE @{0}", p.Key);
            }

            return String.Format("[{0}] = @{0}", p.Key);
        }
    }
}
