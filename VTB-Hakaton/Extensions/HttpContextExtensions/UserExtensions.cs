using System.Linq;
using System.Security.Claims;
using WebApi.DataAccessLayer;
using WebApi.DataAccessLayer.Models;

namespace WebApi.Extensions.HttpContextExtensions
{
    public static class UserExtensions
    {
        public static int? Id(this ClaimsPrincipal httpUser)
        {
            return int.TryParse(httpUser.FindFirstValue(ClaimTypes.NameIdentifier), out int id) ?
                id : null;
        }

        public static string RoleString(this ClaimsPrincipal httpUser)
        {
            return httpUser.FindFirstValue(ClaimTypes.Role);
        }

        public static string ShortName(this ClaimsPrincipal httpUser)
        {
            return httpUser.FindFirstValue(ClaimTypes.GivenName);
        }
        /*
        public static UserRole GetUserRole(this ClaimsPrincipal httpUser, DB ctx)
        {
            var userId = httpUser.Id();

            return ctx.Users
                .Where(x => x.Id == userId)
                .Select(x => x.Role)
                .FirstOrDefault();
        }*/
        /*
        public static int? GetCompanyId(this ClaimsPrincipal httpUser, DB ctx)
        {
            var userId = httpUser.Id();
            return ctx.Companies
                .Where(x => x.OwnerId == userId)
                .Select(x => x.Id)
                .FirstOrDefault();
        }*/
    }
}
