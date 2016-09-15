using System;
using System.Collections.Generic;

namespace Scaffolder.Core
{
    public class Table
    {
        public List<Column> Columns { get; set; }
        public String Name { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
    }

}
