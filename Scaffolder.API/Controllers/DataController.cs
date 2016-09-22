using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Meta;
using System.Collections.Generic;
using Scaffolder.Core.Engine.Sql;

namespace Scaffolder.API.Controllers
{
    [Route("[controller]")]
    public class DataController : Scaffolder.API.Application.ControllerBase
    {
        private Repository _repository;

        public DataController(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

        [HttpGet]
        public IEnumerable<dynamic> Get(Filter filter = null)
        {
            var table = Schema.GetTable(filter.TableName);
            _repository = new Repository(_db, GetQueryBuilder(), table);

            return _repository.Select(filter);
        }

        private IQueryBuilder GetQueryBuilder()
        {
            return new SqlQueryBuilder();
        }

        [HttpPost]
        public dynamic Post([FromBody]Payload payload)
        {
            var table = Schema.GetTable(payload.TableName);
            _repository = new Repository(_db, GetQueryBuilder(), table);

            return _repository.Insert(payload.Entity);
        }

        [HttpPut]
        public dynamic Put([FromBody]Payload payload)
        {
            var table = Schema.GetTable(payload.TableName);
            _repository = new Repository(_db, GetQueryBuilder(), table);

            return _repository.Update(payload.Entity);
        }

        [HttpDelete()]
        public bool Delete([FromBody]Payload payload)
        {
            var table = Schema.GetTable(payload.TableName);
            _repository = new Repository(_db, GetQueryBuilder(), table);

            return _repository.Delete(payload.Entity);
        }
    }
}
