using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Scaffolder.Core
{
    public class Filter
    {
        public Filter()
        {
            PageSize = 10;
            SortOrder = SortOrder.Ascending;
            CurrentPage = 1;
            Parameters = new Dictionary<string, object>();
        }

        public System.Data.SqlClient.SortOrder SortOrder { get; set; }

        public Dictionary<String, Object> Parameters { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }
    }
}
