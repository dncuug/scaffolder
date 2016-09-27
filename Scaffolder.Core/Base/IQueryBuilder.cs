using Scaffolder.Core.Data;
using Scaffolder.Core.Meta;
using System;
using System.Collections.Generic;

namespace Scaffolder.Core.Base
{
    public interface IQueryBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="table"></param>
        /// <param name="filter"></param>
        /// <param name="parameters">If set - then use only fileds from paramter list</param>
        /// <returns></returns>
        String Build(Query query, Table table, Filter filter = null, Dictionary<string, object> parameters = null);
        String BuildRecordCountQuery(Table table, Filter filter);
    }
}
