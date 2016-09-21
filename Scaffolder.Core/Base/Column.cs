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
        public bool? ShowInGrid { get; set; }
        public bool AutoIncrement { get; set; }

        public virtual void LoadExtendInformation(Column obj)
        {
            if (!String.IsNullOrEmpty(obj.Title))
            {
                this.Title = obj.Title;
            }

            if (!String.IsNullOrEmpty(obj.Description))
            {
                this.Description = obj.Description;
            }

            if (obj.Position != 0)
            {
                this.Position = obj.Position;
            }

            if (obj.ShowInGrid.HasValue)
            {
                this.ShowInGrid = obj.ShowInGrid;
            }
        }
    }

    public class TextColumn : Column
    {
        public override ColumnType Type
        {
            get { return ColumnType.Text; }
            set { }
        }

        public int? MaxLength { get; set; }

        public override void LoadExtendInformation(Column obj)
        {
            base.LoadExtendInformation(obj);

            if (obj is TextColumn)
            {
                var column = obj as TextColumn;

                if (column.MaxLength.HasValue)
                {
                    this.MaxLength = column.MaxLength;
                }
            }
        }
    }

    public class DateColumn : Column
    {
        public enum Mode
        {
            Date,
            DateTime
        }

        public Mode? ColumnMode { get; set; }

        public override ColumnType Type
        {
            get { return ColumnType.DateTime; }
            set { }
        }

        public DateTime? MaxValue { get; set; }
        public DateTime? MinValue { get; set; }

        public override void LoadExtendInformation(Column obj)
        {
            base.LoadExtendInformation(obj);

            if (obj is DateColumn)
            {
                var column = obj as DateColumn;

                if (column.MaxValue.HasValue)
                {
                    this.MaxValue = column.MaxValue;
                }

                if (column.MinValue.HasValue)
                {
                    this.MaxValue = column.MinValue;
                }

                if (column.ColumnMode.HasValue)
                {
                    this.ColumnMode = column.ColumnMode;
                }
            }
        }
    }

    public class FileColumn : Column
    {
        public override ColumnType Type
        {
            get { return ColumnType.File; }
            set { }
        }

        public bool IsImage { get; set; }

        public String StorageConnectoinString { get; set; }
        public String StorageUrl { get; set; }

        public override void LoadExtendInformation(Column obj)
        {
            base.LoadExtendInformation(obj);

            if (obj is FileColumn)
            {
                var column = obj as FileColumn;

                if (!String.IsNullOrEmpty(StorageConnectoinString))
                {
                    this.StorageConnectoinString = column.StorageConnectoinString;
                }

                if (!String.IsNullOrEmpty(StorageUrl))
                {
                    this.StorageUrl = column.StorageUrl;
                }

                if (!String.IsNullOrEmpty(StorageUrl))
                {
                    this.StorageUrl = column.StorageUrl;
                }
            }
        }
    }

    public class IntegerColumn : Column
    {
        public override ColumnType Type
        {
            get { return ColumnType.Integer; }
            set { }
        }

        public int? MaxValue { get; set; }
        public int? MinValue { get; set; }

        public override void LoadExtendInformation(Column obj)
        {
            base.LoadExtendInformation(obj);

            if (obj is IntegerColumn)
            {
                var column = obj as IntegerColumn;

                if (column.MaxValue.HasValue)
                {
                    this.MaxValue = column.MaxValue;
                }

                if (column.MinValue.HasValue)
                {
                    this.MinValue = column.MinValue;
                }
            }
        }
    }

    public class DoubleColumn : Column
    {
        public override ColumnType Type
        {
            get { return ColumnType.Double; }
            set { }
        }

        public Double? MaxValue { get; set; }
        public Double? MinValue { get; set; }

        public override void LoadExtendInformation(Column obj)
        {
            base.LoadExtendInformation(obj);

            if (obj is DoubleColumn)
            {
                var column = obj as DoubleColumn;

                if (column.MaxValue.HasValue)
                {
                    this.MaxValue = column.MaxValue;
                }

                if (column.MinValue.HasValue)
                {
                    this.MinValue = column.MinValue;
                }
            }
        }
    }
}
