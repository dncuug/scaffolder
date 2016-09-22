using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core;
using System.Collections.Generic;
using System;

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
            var table = DatabaseModel.GetTable(filter.TableName);
            _repository = new Repository(_db, table);

            return _repository.Select(filter);
        }
        
        [HttpPost]
        public dynamic Post([FromBody]Payload payload)
        {
            var table = DatabaseModel.GetTable(payload.TableName);
            _repository = new Repository(_db, table);

            return _repository.Insert(payload.Entity);
        }
        
        [HttpPut]
        public dynamic Put([FromBody]Payload payload)
        {
            var table = DatabaseModel.GetTable(payload.TableName);
            _repository = new Repository(_db, table);

            return _repository.Update(payload.Entity);
        }

        [HttpDelete()]
        public bool Delete([FromBody]Payload payload)
        {
            var table = DatabaseModel.GetTable(payload.TableName);
            _repository = new Repository(_db, table);

            return _repository.Delete(payload.Entity);
        }
    }
}
