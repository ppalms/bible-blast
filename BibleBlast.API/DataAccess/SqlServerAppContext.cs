using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BibleBlast.API.Models;
using BibleBlast.API.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BibleBlast.API.DataAccess
{
    public partial class SqlServerAppContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IOrganizationProvider _organizationProvider;

        public SqlServerAppContext(DbContextOptions<SqlServerAppContext> options, IOrganizationProvider organizationProvider) : base(options)
        {
            _organizationProvider = organizationProvider;
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Kid> Kids { get; set; }
        public DbSet<UserKid> UserKids { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<MemoryCategory> MemoryCategories { get; set; }
        public DbSet<KidMemory> KidMemories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(x =>
            {
                x.HasKey(ur => new { ur.UserId, ur.RoleId });

                x.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                x.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<UserKid>(x =>
            {
                x.HasKey(uk => new { uk.UserId, uk.KidId });

                x.HasOne(uk => uk.Kid)
                    .WithMany(k => k.Parents)
                    .HasForeignKey(uk => uk.KidId)
                    .IsRequired();

                x.HasOne(uk => uk.User)
                    .WithMany(k => k.Kids)
                    .HasForeignKey(uk => uk.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Kid>().HasQueryFilter(x => x.IsActive)
                .HasQueryFilter(x => x.OrganizationId == _organizationProvider.OrganizationId);

            modelBuilder.Entity<KidMemory>(x =>
            {
                x.HasKey(uk => new { uk.KidId, uk.MemoryId });
            });

            modelBuilder.Entity<Memory>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<MemoryCategory>().Property(x => x.Name).IsRequired();

            modelBuilder.ApplySingularTableNameConvention();
        }
    }
}
