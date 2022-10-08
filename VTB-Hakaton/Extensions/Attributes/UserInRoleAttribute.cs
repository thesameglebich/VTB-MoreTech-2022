using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using WebApi.DataAccessLayer.Models;
using WebApi.Extensions.TextExtensions;
using WebApi.Models.CommonModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApi.Extensions.Attributes
{
    /// <summary>
    /// Checks if user has a specified role in JWT token
    /// Requires Authorize attribute to function properly
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class UserInRoleAttribute : ActionFilterAttribute
    {
        private readonly UserRole _role;
        public UserInRoleAttribute(UserRole role)
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var stringToken = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault().SplitAfter(' ');
            JwtSecurityToken token = new(stringToken);
            if(token.Payload.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role &&
                (c.Value == _role.ToString() || c.Value == UserRole.Superadmin.ToString())) == null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.WriteAsync(
                    JsonConvert.SerializeObject(new Result(HttpStatusCode.Forbidden, new Error("Нет прав")),
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            }
            base.OnActionExecuting(context);
        }
    }
}
