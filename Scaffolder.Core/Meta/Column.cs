using System;

namespace Scaffolder.Core.Meta
{
    public class Column : BaseObject
    {
        public Column()
        {
            ShowInGrid = true;
        }

        public int? Position { get; set; }
        public bool? IsKey { get; set; }
        public bool? IsNullable { get; set; }
        public ColumnType? Type { get; set; }
        public Reference Reference { get; set; }
        public bool? ShowInGrid { get; set; }
        public bool? AutoIncrement { get; set; }
        public dynamic MaxValue { get; set; }
        public dynamic MinValue { get; set; }
        public int? MaxLength { get; set; }
    }
}
