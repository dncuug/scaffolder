using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Engine.Sql;
using Scaffolder.Core.Meta;
using System;
using Scaffolder.API.Application.Security;

namespace Scaffolder.API.Application
{
    public class ControllerBase : Controller
    {
        protected ApplicationContext ApplicationContext { get; private set; }

        protected readonly AppSettings Settings;

        public ControllerBase(IOptions<AppSettings> settings)
        {
            Settings = settings.Value;

            LoadApplicationContext();

        }

        private void LoadApplicationContext()
        {
            var identity = this.User.Identity as ApplicationClaimsIdentity;

            if (identity != null)
            {
                ApplicationContext = ApplicationContext.Load(identity.ConfiguratoinLocation);
            }
            else
            {
                ApplicationContext = null;
            }
        }

        protected ISchemaBuilder GetSchemaBuilder()
        {
            var db = GetDatabase();

            return new SqlSchemaBuilder(db);
            //return new MySqlSchemaBuilder(db);
        }

        protected IRepository CreateRepository(Table table)
        {
            var db = GetDatabase();

            var queryBuilder = new SqlQueryBuilder();
            //var queryBuilder = new MySqlQueryBuilder();

            return new Repository(db, queryBuilder, table);
        }

        private IDatabase GetDatabase()
        {
            return new SqlDatabase(ApplicationContext.Configuration.ConnectionString);
            //return new MySqlDatabase(connectionString);
        }
    }
}