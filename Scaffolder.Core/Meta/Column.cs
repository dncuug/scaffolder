using System;

namespace Scaffolder.Core.Meta
{
    public class Column : BaseObject
    {
        public Column()
        {
            ShowInGrid = true;
        }

        private ColumnType? _columnType;
        private bool? _readonly;

        public int? Position { get; set; }
        public bool? IsKey { get; set; }
        public bool? IsNullable { get; set; }
        public Reference Reference { get; set; }
        public bool? ShowInGrid { get; set; }
        public bool? AutoIncrement { get; set; }
        public dynamic MaxValue { get; set; }
        public dynamic MinValue { get; set; }
        public int? MaxLength { get; set; }

        public bool? Readonly
        {
            get
            {
                if (AutoIncrement == true)
                {
                    _readonly = true;
                }

                return _readonly;
            }
            set
            {
                _readonly = value;
            }
        }

        public ColumnType? Type
        {
            get
            {
                if (Reference != null)
                {
                    _columnType = ColumnType.Reference;
                }
                return _columnType;
            }
            set { _columnType = value; }
        }

        public override string ToString()
        {
            return $"{Name} Key: {IsKey} IsNullable: {IsNullable} ShowInGrid: {ShowInGrid}";
        }
    }
}
