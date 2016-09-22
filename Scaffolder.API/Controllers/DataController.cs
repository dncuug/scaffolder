using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core.Meta;
using System.Collections.Generic;

namespace Scaffolder.API.Controllers
{
    [Route("[controller]")]
    public class DataController : Application.ControllerBase
    {
        public DataController(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

        [HttpGet]
        //public IEnumerable<dynamic> Get([ModelBinder(BinderType = typeof(FilterModelBinder))]Filter filter)
        public IEnumerable<dynamic> Get(Filter filter)
        {
            var table = Schema.GetTable(filter.TableName);
            var repository = CreateRepository(table);

            return repository.Select(filter);
        }

        [HttpPost]
        public dynamic Post([FromBody]Payload payload)
        {
            var table = Schema.GetTable(payload.TableName);
            var repository = CreateRepository(table);

            return repository.Insert(payload.Entity);
        }

        [HttpPut]
        public dynamic Put([FromBody]Payload payload)
        {
            var table = Schema.GetTable(payload.TableName);
            var repository = CreateRepository(table);

            return repository.Update(payload.Entity);
        }

        [HttpDelete]
        public bool Delete([FromBody]Payload payload)
        {
            var table = Schema.GetTable(payload.TableName);
            var repository = CreateRepository(table);

            return repository.Delete(payload.Entity);
        }
    }
}
