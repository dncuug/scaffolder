using System;
using System.Collections.Generic;

namespace Scaffolder.Core.Base
{
    public class Table : BaseObject
    {
        public Table()
        {
            Description = String.Empty;
            Columns = new List<Column>();
        }
        public Table(string name)
            : this()
        {
            Name = name;
            Title = name;
        }

        public List<Column> Columns { get; set; }
    }
}