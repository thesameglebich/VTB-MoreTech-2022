using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi.DataAccessLayer.Models;

namespace WebApi.Extensions.IApplicationBuilderExtensions
{
    public static class SeedingExtensions
    {
        public static void SeedUsers(this IApplicationBuilder builder)
        {
            var config = builder.ApplicationServices.GetRequiredService<IConfiguration>();
            var seedAdmin = config.GetSection("AdminUser").Get<SeedUser>();

            var scope = builder.ApplicationServices.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

            if (seedAdmin != null)
            {
                CreateUser(seedAdmin, userManager, UserRole.Superadmin);
            }
        }

        private static void CreateUser(SeedUser seedUser, UserManager<User> userManager, UserRole role)
        {
            var user = userManager.Users.IgnoreQueryFilters().FirstOrDefault(u => u.Email == seedUser.Email);
            if (user == null)
            {
                var res = userManager.CreateAsync(new User()
                {
                    Email = seedUser.Email,
                    Name = seedUser.Name,
                    Surname = seedUser.Surname,
                    UserName = seedUser.Username,
                    Role = role,
                    PrivateKey = "8f545ebf1410031ae63a60d703b61ef1fac22a13da34753002a4e22d84c41bb6",
                    PublicKey = "0x27fF0fEC01610bd78C1F16EE8ac12Fda26A8B94e"
                }, seedUser.Password).GetAwaiter().GetResult();

                if (!res.Succeeded)
                {
                    throw new InvalidOperationException($"{role} create error");
                }
            }
        }

        private class SeedUser
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
        }
    }
}
