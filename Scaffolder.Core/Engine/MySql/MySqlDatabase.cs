using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Scaffolder.Core.Data;

namespace Scaffolder.Core.Engine.MySql
{
	public class MySqlDatabase : DatabaseBase<MySqlConnection, MySqlCommand>
	{
		public MySqlDatabase(string connectionString)
			: base(connectionString)
		{
		}

		protected override MySqlConnection CreateConnection(string connectionString)
		{
			return new MySqlConnection(connectionString);
		}

		protected override MySqlCommand CreateCommand(MySqlConnection connection, string sql,
			Dictionary<string, object> parameters = null)
		{
			var command = new MySqlCommand(sql, connection);

			if (parameters != null)
			{
				foreach (var p in parameters)
				{
					command.Parameters.Add(new MySqlParameter
					{
						ParameterName = p.Key,
						Value = p.Value
					});
				}
			}

			return command;
		}
	}
}