using App.Domain.Core.User.Entities;
using App.Domain.Core.User.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.DataBase.SQL.SeedDate
{
    public class IdentitySeeder
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public IdentitySeeder(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private async Task CreateUserIfNotExistsAsync(string userName, string email, string password, string roleName)
        {

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await _roleManager.CreateAsync(
                    new IdentityRole<int>(roleName));

                if (!roleResult.Succeeded)
                    throw new Exception(string.Join(", ",
                        roleResult.Errors.Select(e => e.Description)));
            }


            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new AppUser
                {
                    UserName = userName,
                    Address="tehran",
                    Name = userName,
                    LastName = userName,
                    Email = email,
                    PhoneNumber = "09120000000",
                    IsActive = true,
                    IsDeleted = false,
                    SubmitDate = DateTime.Now
                    
                };

                var result = await _userManager.CreateAsync(user, password);
                
                if (!result.Succeeded)
                    throw new Exception(string.Join(", ",
                        result.Errors.Select(e => e.Description)));
            }

            // اختصاص Role به User
            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                var roleResult = await _userManager.AddToRoleAsync(user, roleName);

                if (!roleResult.Succeeded)
                    throw new Exception(string.Join(", ",
                        roleResult.Errors.Select(e => e.Description)));
            }
        }

        public async Task SeedAsync()
        {
            await CreateUserIfNotExistsAsync("admin", "admin@admin.com", "@123456Admin", RoleEnum.AdminRole.ToString());

            await CreateUserIfNotExistsAsync("customer", "customer@customer.com", "@123456Customer", RoleEnum.CustomerRole.ToString());

            await CreateUserIfNotExistsAsync("expert", "expert@expert.com", "@123456expert", RoleEnum.ExpertRole.ToString());
        }
    }
}
