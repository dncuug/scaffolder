using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            TableName = "";
        }

        public string TableName { get; set; }

        public bool DetailMode { get; set; }

        public System.Data.SqlClient.SortOrder SortOrder { get; set; }

        public Dictionary<String, Object> Parameters { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }
    }
}
