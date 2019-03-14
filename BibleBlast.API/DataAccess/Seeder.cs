using System;
using System.Collections.Generic;
using System.Linq;
using BibleBlast.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BibleBlast.API.DataAccess
{
    public class Seeder
    {
        private readonly SqlServerAppContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seeder(SqlServerAppContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Seed()
        {
            SeedOrganizations();
            SeedKids();
            SeedUsers();
        }

        private void SeedOrganizations()
        {
            if (_context.Organizations.Any())
            {
                return;
            }

            _context.Organizations.AddRange(new[]
            {
                new Organization { Name = "First United Methodist Church Tulsa" },
                new Organization { Name = "All Souls Church Tulsa" },
            });

            _context.SaveChanges();
        }

        private void SeedKids()
        {
            if (_context.Kids.IgnoreQueryFilters().Any())
            {
                return;
            }

            var kidData = System.IO.File.ReadAllText("DataAccess/KidSeedData.json");
            var kids = JsonConvert.DeserializeObject<List<Kid>>(kidData);

            _context.Kids.AddRange(kids);
            _context.SaveChanges();
        }

        private void SeedUsers()
        {
            if (_userManager.Users.IgnoreQueryFilters().Any())
            {
                return;
            }

            var roles = new List<Role>
            {
                new Role { Name = UserRoles.Member },
                new Role { Name = UserRoles.Coach },
                new Role { Name = UserRoles.Admin },
            };

            foreach (var role in roles)
            {
                _roleManager.CreateAsync(role).Wait();
            }

            var userData = System.IO.File.ReadAllText("DataAccess/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var user in users)
            {
                _userManager.CreateAsync(user, "password").Wait();
                _userManager.AddToRoleAsync(user, UserRoles.Member).Wait();

                var userKids = _context.Kids.IgnoreQueryFilters()
                    .Where(kid => user.LastName == kid.LastName)
                    .Select(kid => new UserKid { KidId = kid.Id, UserId = user.Id })
                    .ToList();

                _context.UserKids.AddRange(userKids);
                _context.SaveChanges();
            }

            var adminUser = new User { UserName = "admin" };
            _userManager.CreateAsync(adminUser, "password").Wait();

            var admin = _userManager.FindByNameAsync("admin").Result;
            _userManager.AddToRoleAsync(admin, UserRoles.Admin).Wait();

            var coachMcGuirk = _userManager.FindByNameAsync("jmcguirk").Result;
            _userManager.AddToRoleAsync(coachMcGuirk, UserRoles.Coach).Wait();
        }
    }
}
