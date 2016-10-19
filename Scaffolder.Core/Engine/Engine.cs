using System;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Engine.MySql;
using Scaffolder.Core.Engine.Sql;
using Scaffolder.Core.Meta;

namespace Scaffolder.Core.Engine
{
	public class Engine
	{
		private readonly DatabaseEngine _databaseEngine;

		private readonly String _connectionString;

		public Engine(string connectionString, DatabaseEngine databaseEngine)
		{
			_connectionString = connectionString;
			_databaseEngine = databaseEngine;
		}

		public ISchemaBuilder CreateSchemaBuilder()
		{
			var db = GetDatabase();

			switch (_databaseEngine)
			{
				case DatabaseEngine.SqlServer: return new SqlSchemaBuilder(db);
				case DatabaseEngine.MySQL: return new MySqlSchemaBuilder(db);
				case DatabaseEngine.Oracle: throw new NotImplementedException();
				case DatabaseEngine.PostgreSql: throw new NotImplementedException();
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public IRepository CreateRepository(Table table)
		{
			var db = GetDatabase();
			var queryBuilder = GetQueryBuilder();
			return new Repository(db, queryBuilder, table);
		}

		private IQueryBuilder GetQueryBuilder()
		{
			switch (_databaseEngine)
			{
				case DatabaseEngine.SqlServer: return new SqlQueryBuilder();
				case DatabaseEngine.MySQL: return new MySqlQueryBuilder();
				case DatabaseEngine.Oracle: throw new NotImplementedException();
				case DatabaseEngine.PostgreSql: throw new NotImplementedException();
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private IDatabase GetDatabase()
		{
			switch (_databaseEngine)
			{
				case DatabaseEngine.SqlServer: return new SqlDatabase(_connectionString);
				case DatabaseEngine.MySQL: return new MySqlDatabase(_connectionString);
				case DatabaseEngine.Oracle: throw new NotImplementedException();
				case DatabaseEngine.PostgreSql: throw new NotImplementedException();
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}