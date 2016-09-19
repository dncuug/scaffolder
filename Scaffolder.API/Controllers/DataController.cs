using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core;
using System.Collections.Generic;

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
            filter.TableName = "Pages";

            filter.Parameters.Add("PageTypesId", 1);
            filter.Parameters.Add("Title", "%ces%");

            var table = DatabaseModel.GetTable(filter.TableName);
            _repository = new Repository(_db, table);

            return _repository.Select(filter);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            //SELECT by ID
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
            //INSERT
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            //UPDATE
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //DELETE
        }
    }
}
