using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Scaffolder.Core.Base;

namespace Scaffolder.Core
{
    public static class ObjectExtender
    {
        public static void MapExtendInformation(BaseObject src, BaseObject dst)
        {
            if (!String.IsNullOrEmpty(src.Title))
            {
                dst.Title = src.Title;
            }

            if (!String.IsNullOrEmpty(src.Description))
            {
                dst.Description = src.Description;
            }
        }

        public static void MapExtendInformation(Table src, Table dst)
        {
            MapExtendInformation((BaseObject)src, (BaseObject)dst);

            if (src.Position != 0)
            {
                dst.Position = src.Position;
            }
        }

        public static void MapExtendInformation(Column src, Column dst)
        {
            if (src.Type != dst.Type)
            {
                var copyOfDestenationColumnWithNewType = (Column) Activator.CreateInstance(src.GetType());

                var srcColumnFields = src.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                var dstColumnFields = dst.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

                foreach (var field in dstColumnFields)
                {
                    if (dstColumnFields.Any(f => f.Name == field.Name && f.FieldType == field.FieldType))
                    {
                        var value = field.GetValue(dst);
                        field.SetValue(copyOfDestenationColumnWithNewType, value);
                    }
                }

            }
        }
    }
}
