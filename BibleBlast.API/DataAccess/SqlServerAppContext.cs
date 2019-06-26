using Microsoft.EntityFrameworkCore;
using BibleBlast.API.Models;
using BibleBlast.API.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BibleBlast.API.DataAccess
{
    public partial class SqlServerAppContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IUserResolver _userResolver;

        public SqlServerAppContext(DbContextOptions<SqlServerAppContext> options, IUserResolver userResolver) : base(options)
        {
            _userResolver = userResolver;
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

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<UserKid>(entity =>
            {
                entity.HasKey(uk => new { uk.UserId, uk.KidId });

                entity.HasOne(uk => uk.Kid)
                    .WithMany(k => k.Parents)
                    .HasForeignKey(uk => uk.KidId)
                    .IsRequired();

                entity.HasOne(uk => uk.User)
                    .WithMany(k => k.Kids)
                    .HasForeignKey(uk => uk.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Kid>()
                .HasQueryFilter(kid => kid.IsActive
                    && kid.OrganizationId == _userResolver.OrganizationId
                    || _userResolver.UserRole == Models.UserRoles.Admin);

            modelBuilder.Entity<KidMemory>(entity =>
            {
                entity.HasKey(km => new { km.KidId, km.MemoryId });
                entity.HasQueryFilter(km => km.Kid.IsActive
                    && km.Kid.OrganizationId == _userResolver.OrganizationId
                    || _userResolver.UserRole == Models.UserRoles.Admin);
            });

            modelBuilder.Entity<Memory>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<MemoryCategory>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<User>()
                .HasQueryFilter(user => user.IsActive
                    && user.OrganizationId == _userResolver.OrganizationId
                    || _userResolver.UserRole == Models.UserRoles.Admin);

            modelBuilder.ApplySingularTableNameConvention();
        }
    }
}
