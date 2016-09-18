using System;
using System.Collections.Generic;
using System.Linq;

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

        public override string ToString()
        {
            return Name;
        }

        public Column GetColumn(string name)
        {
            return Columns.SingleOrDefault(o => String.Equals(o.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}