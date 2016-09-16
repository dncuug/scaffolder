using System;
using System.Collections.Generic;

namespace Scaffolder.Core
{
    public class Table : BaseObject
    {
        public Table(string name)
        {
            Name = name;
        }

        public List<Column> Columns { get; set; }
    }
}