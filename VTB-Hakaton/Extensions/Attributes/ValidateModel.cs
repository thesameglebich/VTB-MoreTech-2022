using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Models.CommonModels;

namespace WebApi.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ValidateModelAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new Error(
                    key: x.Key,
                    value: x.Value.Errors
                        .Select(e => e.ErrorMessage)
                        .LastOrDefault() ?? string.Empty))
                .ToList();

            context.Result = new JsonResult(new Result(HttpStatusCode.BadRequest, errors));
        }
    }
}
