using Scaffolder.Core.Base;
using Scaffolder.Core.Meta;
using System;
using System.Collections.Generic;
using Scaffolder.Core.Data;

namespace Scaffolder.Core.Engine
{
    public abstract class SchemeBuilderBase : ISchemaBuilder
    {
        protected readonly IDatabase _db;

        public SchemeBuilderBase(IDatabase db)
        {
            _db = db;
        }

        public Schema Build()
        {
            var database = new Schema();

            var tableList = GetDatabaseTables();

            foreach (var name in tableList)
            {
                var table = GetDataTable(name);
                database.Tables.Add(table);
            }

            return database;
        }

        protected abstract Table GetDataTable(string name);

        protected abstract IEnumerable<String> GetDatabaseTables();

        protected abstract IEnumerable<string> GetTablePrimaryKeys(string name);

        protected static bool ShowInGrid(ColumnType type, string name)
        {
            if (type == ColumnType.HTML ||
            type == ColumnType.Image ||
            type == ColumnType.File ||
            type == ColumnType.Binary)
            {
                return false;
            }

            name = name.ToLower();

            if (name == "id" || name == "description")
            {
                return false;
            }


            return true;
        }
    }
}