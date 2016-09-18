using Scaffolder.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scaffolder.Core
{
    public class QueryBuilder
    {
        public String Build(Table table, Filter filter)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("SElECT ");

            var columns = table.Columns.Where(o => o.ShowInGrid || filter.DetailMode).Select(o => o.Name);

            sb.Append(String.Join(", ", columns));

            sb.AppendFormat(" FROM {0} ", filter.TableName);

            var whereCaluses = filter.Parameters.Select(o => BuildClause(table, o)).ToList();
            
            sb.AppendFormat(" WHERE {0}", String.Join(" AND ", whereCaluses));

            return sb.ToString();
        }

        private string BuildClause(Table table, KeyValuePair<string, object> p)
        {
            var column = table.GetColumn(p.Key);

            if (column.Type == ColumnType.Text)
            {
                return String.Format("{0} LIKE @{0}", p.Key);
            }

            return String.Format("{0} = @{0}", p.Key);
        }
    }
}
