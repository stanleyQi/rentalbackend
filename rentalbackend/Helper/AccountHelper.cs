using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rentalbackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rentalbackend.Helper
{
    public class AccountHelper
    {
        public async static void CreateUserRoles(IServiceProvider serviceProvider, IConfiguration config)
        {

            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityResult roleResult;

            //Adding lessor Role
            var roleCheck = await RoleManager.RoleExistsAsync("lessor");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("lessor"));
            }

            //Adding tenant Role
            roleCheck = await RoleManager.RoleExistsAsync("tenant");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("tenant"));
            }

            //Adding agent Role
            roleCheck = await RoleManager.RoleExistsAsync("agent");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("agent"));
            }

            //Adding admin Role
            roleCheck = await RoleManager.RoleExistsAsync("admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("admin"));
            }


        }
    }
}
