using Scaffolder.Core.Data;
using Scaffolder.Core.Meta;
using System;

namespace Scaffolder.Core.Base
{
    public interface IQueryBuilder
    {
        String Build(Query query, Table table, Filter filter = null);
	    String BuildRecordCountQuery(Table table, Filter filter);
    }
}
