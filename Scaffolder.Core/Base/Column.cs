using System;

namespace Scaffolder.Core.Base
{
    public class Column : BaseObject
    {
        public Column()
        {
            ShowInGrid = true;
        }

        public int Position { get; set; }
        public bool IsKey { get; set; }
        public virtual ColumnType Type { get; set; }
        public bool AllowNullValue { get; set; }
        public Reference Reference { get; set; }
        public bool ShowInGrid { get; set; }
        public bool AutoIncrement { get; set; }
        public dynamic MaxValue { get; set; }
        public dynamic MinValue { get; set; }
        public int? MaxLength { get; set; }
    }
}
