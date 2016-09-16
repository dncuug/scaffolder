using System;

namespace Scaffolder.Core
{
    public class Column : BaseObject
    {
        public virtual ColumnType Type { get; set; }
        public bool AllowNullValue { get; set; }
        public Reference Reference { get; set; }
    }

    public class TextColumn : Column
    {
        public override ColumnType Type
        {
            get { return ColumnType.Text; }
            set { }
        }

        public int MaxLength { get; set; }
    }

    public class DateColumn : Column
    {
        public enum Mode
        {
            Date,
            DateTime
        }

        public Mode ColumnMode { get; set; }

        public override ColumnType Type
        {
            get { return ColumnType.DateTime; }
            set { }
        }

        public DateTime MaxValue { get; set; }
        public DateTime MinValue { get; set; }
    }

    public class FileColumn : Column
    {
        public override ColumnType Type
        {
            get { return ColumnType.File; }
            set { }
        }

        public String StorageConnectoinString { get; set; }
        public String StorageUrl { get; set; }
    }

    public class IntegerColumn : Column
    {
        public override ColumnType Type
        {
            get { return ColumnType.Integer; }
            set { }
        }

        public int MaxValue { get; set; }
        public int MinValue { get; set; }
    }

    public class DoubleColumn : Column
    {
        public override ColumnType Type
        {
            get { return ColumnType.Double; }
            set { }
        }

        public Double MaxValue { get; set; }
        public Double MinValue { get; set; }
    }
}
