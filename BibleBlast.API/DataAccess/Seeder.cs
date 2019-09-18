using System.Collections.Generic;
using System.IO;
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
            SeedMemoryCategories();
            SeedMemories();
            SeedKidMemories();
            SeedAwardItems();
            SeedAwards();
            SeedAwardMemories();
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

            var kidData = File.ReadAllText("DataAccess/SeedData/KidSeedData.json");
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

            var userData = File.ReadAllText("DataAccess/SeedData/UserSeedData.json");
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

            var admin = new User { UserName = "admin" };
            _userManager.CreateAsync(admin, "password").Wait();
            _userManager.AddToRoleAsync(admin, UserRoles.Admin).Wait();

            var coach = new User { UserName = "jmcguirk", OrganizationId = 2 };
            _userManager.CreateAsync(coach, "password").Wait();
            _userManager.AddToRoleAsync(coach, UserRoles.Coach).Wait();
        }

        private void SeedMemoryCategories()
        {
            if (_context.MemoryCategories.Any())
            {
                return;
            }

            var categoryData = File.ReadAllText("DataAccess/SeedData/MemoryCategorySeedData.json");
            var categories = JsonConvert.DeserializeObject<List<MemoryCategory>>(categoryData);

            _context.MemoryCategories.AddRange(categories);
            _context.SaveChanges();
        }

        private void SeedMemories()
        {
            if (_context.Memories.Any())
            {
                return;
            }

            var memoryData = File.ReadAllText("DataAccess/SeedData/MemorySeedData.json");
            var memories = JsonConvert.DeserializeObject<List<Memory>>(memoryData);

            _context.Memories.AddRange(memories);
            _context.SaveChanges();
        }

        private void SeedKidMemories()
        {
            if (_context.KidMemories.IgnoreQueryFilters().Any())
            {
                return;
            }

            var kidMemoryData = File.ReadAllText("DataAccess/SeedData/KidMemorySeedData.json");
            var kidMemories = JsonConvert.DeserializeObject<List<KidMemory>>(kidMemoryData);

            _context.KidMemories.AddRange(kidMemories);
            _context.SaveChanges();
        }

        private void SeedAwardItems()
        {
            if (_context.AwardItems.Any())
            {
                return;
            }

            var awardItemData = File.ReadAllText("DataAccess/SeedData/AwardItemSeedData.json");
            var awardItems = JsonConvert.DeserializeObject<List<AwardItem>>(awardItemData);

            _context.AwardItems.AddRange(awardItems);
            _context.SaveChanges();
        }

        private void SeedAwards()
        {
            if (_context.Awards.Any())
            {
                return;
            }

            var awardData = File.ReadAllText("DataAccess/SeedData/AwardSeedData.json");
            var awards = JsonConvert.DeserializeObject<List<Award>>(awardData);

            _context.Awards.AddRange(awards);
            _context.SaveChanges();
        }

        private void SeedAwardMemories()
        {
            if (_context.AwardMemories.Any())
            {
                return;
            }

            var awardMemoryData = File.ReadAllText("DataAccess/SeedData/AwardMemorySeedData.json");
            var awardMemories = JsonConvert.DeserializeObject<List<AwardMemory>>(awardMemoryData);

            _context.AwardMemories.AddRange(awardMemories);
            _context.SaveChanges();
        }
    }
}
