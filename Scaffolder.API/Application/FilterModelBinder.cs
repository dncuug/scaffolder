using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Scaffolder.Core.Meta;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Scaffolder.API.Application
{
    public class FilterModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var model = Activator.CreateInstance(bindingContext.ModelType) as Filter;
            
            var fields = new List<String>
            {
                "TableName",
                "PageSize",
                "PageSize",
                "SortColumn",
                "CurrentPage",
                "CurrentPage"
            };

            var parameters = bindingContext.HttpContext.Request.Query.ToList();
            
            model.TableName = TryGetValue(parameters, "TableName", "");
            model.PageSize = TryGetValue(parameters, "PageSize", 25);
            model.SortOrder = TryGetValue(parameters, "SortOrder", SortOrder.Ascending);
            model.SortColumn = TryGetValue(parameters, "SortColumn", "");
            model.CurrentPage = TryGetValue(parameters, "CurrentPage", 1);
            model.DetailMode = TryGetValue(parameters, "DetailMode", false);

            parameters = parameters.Where(p => fields.All(f => !String.Equals(f, p.Key, StringComparison.OrdinalIgnoreCase))).ToList();
            var json = parameters.Where(o => o.Key == "Parameters").Select(o => o.Value).FirstOrDefault().ToString();
            model.Parameters = (JsonConvert.DeserializeObject<Dictionary<string, Object>>(json)) ?? new Dictionary<string, object>();

            bindingContext.Model = model;
            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.FromResult(bindingContext.Result);
        }

        private bool TryGetValue(List<KeyValuePair<String, StringValues>> parameters, string key, bool defaultValue)
        {
            try
            {
                return Convert.ToBoolean(parameters.Where(o => String.Equals(o.Key, key, StringComparison.OrdinalIgnoreCase)).Select(o => o.Value).FirstOrDefault());
            }
            catch
            {
                return defaultValue;
            }
        }

        private SortOrder TryGetValue(List<KeyValuePair<String, StringValues>> parameters, string key, SortOrder defaultValue)
        {
            try
            {
                return (SortOrder)Convert.ToInt32(parameters.Where(o => String.Equals(o.Key, key, StringComparison.OrdinalIgnoreCase)).Select(o => o.Value).FirstOrDefault());
            }
            catch
            {
                return defaultValue;
            }
        }

        private int TryGetValue(List<KeyValuePair<String, StringValues>> parameters, string key, int defaultValue)
        {
            try
            {
                var p = parameters.Where(o => String.Equals(o.Key, key, StringComparison.OrdinalIgnoreCase)).Select(o => o.Value).FirstOrDefault();
                return Convert.ToInt32(p.ToString());
            }
            catch
            {
                return defaultValue;
            }
        }

        private string TryGetValue(List<KeyValuePair<String, StringValues>> parameters, string key, string defaultValue)
        {
            try
            {
                return Convert.ToString(parameters.Where(o => String.Equals(o.Key,key, StringComparison.OrdinalIgnoreCase)).Select(o => o.Value).FirstOrDefault());
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
