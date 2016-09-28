using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Meta;

namespace Scaffolder.Core.Engine.Sql
{
    public class SqlQueryBuilder : IQueryBuilder
    {
        public String Build(Query query, Table table, Filter filter = null, Dictionary<string, Object> parametrs = null)
        {
            switch (query)
            {
                case Query.Select:
                    return BuildSelect(table, filter);
                case Query.Insert:
                    return BuildInsert(table);
                case Query.Update:
                    return BuildUpdate(table, parametrs);
                case Query.Delete:
                    return BuildDelete(table);
            }

            throw new NotSupportedException("Unknown query type");
        }

        public string BuildRecordCountQuery(Table table, Filter filter)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("SElECT COUNT(*) FROM [{0}] ", filter.TableName);

            var whereCaluses = filter.Parameters.Select(o => BuildClause(table, o, false)).Where(o => !String.IsNullOrEmpty(o)).ToList();

            if (whereCaluses.Any())
            {
                sb.AppendFormat(" WHERE {0}", String.Join(" AND ", whereCaluses));
            }

            return sb.ToString();
        }

        private String BuildSelect(Table table, Filter filter)
        {
            var references = table.Columns.Where(o => o.Reference != null).ToList();

            var sb = new StringBuilder();

            sb.AppendFormat("SElECT ");

            var columns = table.Columns.Where(o => o.ShowInGrid == true || filter.DetailMode || o.IsKey == true).Select(o => $"[{table.Name}].[{o.Name}]").ToList();

            foreach (var r in references)
            {
                //columns.Add($"[{r.Reference.Table}].[{r.Reference.TextColumn}] AS {r.Reference.GetColumnAlias()}");
                columns.Add($"[{r.Reference.GetTableAlias()}].[{r.Reference.TextColumn}] AS {r.Reference.GetColumnAlias()}");
            }

            sb.Append(String.Join(", ", columns));

            sb.AppendFormat(" FROM [{0}] ", filter.TableName);

            var whereCaluses = filter.Parameters.Select(o => BuildClause(table, o, true)).Where(o => !String.IsNullOrEmpty(o)).ToList();


            foreach (var r in references)
            {
                //sb.AppendFormat(" LEFT JOIN {0} ON [{0}].[{1}] = [{2}].[{3}] ", r.Reference.Table, r.Reference.KeyColumn, table.Name, r.Name);

                sb.AppendFormat(" LEFT JOIN {0} AS {1} ON [{1}].[{2}] = [{3}].[{4}] ", r.Reference.Table, r.Reference.GetTableAlias(), r.Reference.KeyColumn, table.Name, r.Name);
            }

            if (whereCaluses.Any())
            {
                sb.AppendFormat(" WHERE {0}", String.Join(" AND ", whereCaluses));
            }
            
            var order = filter.SortOrder == SortOrder.Descending ? "DESC" : "ASC";

            var orderByColumn = CheckOrderByColumn(table.Columns, filter.SortColumn);

            sb.AppendFormat(@" ORDER BY [{0}].[{1}] {2} ", table.Name, orderByColumn, order);

            if (filter.PageSize.HasValue)
            {
                var offset = filter.PageSize * (filter.CurrentPage - 1);

                sb.AppendFormat(@" OFFSET {0} ROWS
  							       FETCH NEXT {1} ROWS ONLY;", offset, filter.PageSize);
            }

            return sb.ToString();
        }

        private static string CheckOrderByColumn(IEnumerable<Column> columns, string column)
        {
            if (columns.All(o => !String.Equals(column, o.Name, StringComparison.OrdinalIgnoreCase)) || String.IsNullOrEmpty(column))
            {
                var sortColumn = columns.FirstOrDefault(o => o.IsKey == true);

                if (sortColumn == null)
                {
                    sortColumn = columns.FirstOrDefault();
                }

                return sortColumn.Name;
            }

            return column;
        }

        private static string BuildInsert(Table table)
        {
            var sb = new StringBuilder();

            var fields = table.Columns.Where(o => o.AutoIncrement != true).ToList();

            sb.AppendFormat("INSERT INTO [{0}] ({1})", table.Name, String.Join(", ", fields.Select(o => o.Name)));
            sb.AppendFormat(" OUTPUT INSERTED.* ");
            sb.AppendFormat(" VALUES({0})", String.Join(", ", fields.Select(o => "@" + o.Name)));

            return sb.ToString();
        }

        private static string BuildUpdate(Table table, Dictionary<string, Object> parameters = null)
        {
            var sb = new StringBuilder();

            var fields = table.Columns.Where(o => o.AutoIncrement != true && o.IsKey != true).Select(o => o.Name).ToList();
            var keyFields = table.GetPrimaryKeys();

            if (parameters != null)
            {
                fields = fields.Where(o => parameters.Keys.Any(k => String.Equals(o, k, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            sb.AppendFormat("UPDATE [{0}] SET ", table.Name);
            sb.AppendLine(String.Join(", ", fields.Select(o => String.Format("[{0}] = @{0}", o))));
            sb.AppendFormat(" OUTPUT INSERTED.* ");
            sb.AppendFormat(" WHERE ");
            sb.AppendLine(String.Join(", ", keyFields.Select(o => String.Format("[{0}] = @{0}", o.Name))));

            return sb.ToString();
        }

        private static string BuildDelete(Table table)
        {
            var sb = new StringBuilder();

            var keyFields = table.GetPrimaryKeys();

            sb.AppendFormat("DELETE FROM [{0}] ", table.Name);
            sb.AppendFormat(" OUTPUT DELETED.* ");
            sb.AppendFormat(" WHERE");
            sb.AppendLine(String.Join(", ", keyFields.Select(o => String.Format("[{0}] = @{0}", o.Name))));

            return sb.ToString();
        }

        private static string BuildClause(Table table, KeyValuePair<string, object> p, bool includeTableName)
        {
            var column = table.GetColumn(p.Key);

            if (column == null)
            {
                return String.Empty;
            }

            if (column.Type == ColumnType.Text)
            {
                if (includeTableName)
                {
                    return String.Format("[{0}].[{1}] LIKE @{1}", table.Name, column.Name);
                }

                return String.Format("[{0}] LIKE @{0}", column.Name);
            }
            if (includeTableName)
            {
                return String.Format("[{0}].[{1}] = @{1}", table.Name, column.Name);
            }

            return String.Format("[{0}] = @{0}", column.Name);
        }
    }
}
