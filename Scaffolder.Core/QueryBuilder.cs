using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scaffolder.Core.Base;

namespace Scaffolder.Core
{
    public class QueryBuilder
    {
        public String Build(Table table, Filter filter)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("SElECT ");
            
            var columns = table.Columns.Where(o => o.ShowInGrid || filter.DetailMode).Select( o => o.Name);

            sb.Append(String.Join(", ", columns));
            
            sb.AppendFormat(" FROM {0}", filter.TableName);

            return sb.ToString();
        }
    }
}
