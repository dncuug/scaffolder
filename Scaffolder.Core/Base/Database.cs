using System.Collections.Generic;

namespace Scaffolder.Core.Base
{
    public class Database : BaseObject
    {
        public Database()
        {
            Tables = new List<Table>();
        }

        public List<Table> Tables { get; set; }
    }
}
