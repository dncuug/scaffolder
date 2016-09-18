using System.Collections.Generic;

namespace Scaffolder.Core.Base
{
    public class Table : BaseObject
    {
        public Table(string name)
        {
            Name = name;
            Columns = new List<Column>();
        }

        public List<Column> Columns { get; set; }
    }
}