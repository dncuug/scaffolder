using System;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Meta;

namespace Scaffolder.Core.Engine.MySql
{
    public class MySqlQueryBuilder : IQueryBuilder
    {
        public string Build(Query query, Table table, Filter filter = null)
        {
            throw new NotImplementedException();
        }

	    public string BuildRecordCountQuery(Table table, Filter filter)
	    {
		    throw new NotImplementedException();
	    }
    }
}
