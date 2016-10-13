using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application.Security;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Engine.Sql;
using Scaffolder.Core.Meta;
using System.Security.Claims;

namespace Scaffolder.API.Application
{
    public class ControllerBase : Controller
    {
        private ApplicationContext _applicationContext;

        private static Object _lock = new Object();

        protected ApplicationContext ApplicationContext
        {
            get
            {
                if (_applicationContext == null)
                {
                    lock (_lock)
                    {
                        if (_applicationContext == null)
                        {
                            var login = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                            var authorizationManager = new AuthorizationManager(Settings.WorkingDirectory);
                            var configuratoinLocation = authorizationManager.GetConfiguratoinLocationForUser(login);
                            _applicationContext = ApplicationContext.Load(configuratoinLocation);
                        }
                    }

                }

                return _applicationContext;
            }
        }

        protected AppSettings Settings { get; private set; }

        public ControllerBase(IOptions<AppSettings> settings)
        {
            Settings = settings.Value;
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