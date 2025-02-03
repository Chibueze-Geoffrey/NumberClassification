using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace NumberClassification.API.Filters
{
    public class ValidateNumberQueryParameterAttribute : ActionFilterAttribute, IFilterMetadata
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Query.ContainsKey("number"))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    detail = new List<object>
                    {
                        new
                        {
                            type = "missing",
                            loc = new List<string> { "query", "number" },
                            msg = "Field required",
                            input = (string)null
                        }
                    }
                });
            }
        }
    }
}
