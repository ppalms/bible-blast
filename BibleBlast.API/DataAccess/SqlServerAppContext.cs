using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BibleBlast.API.Models;
using BibleBlast.API.Helpers;

namespace BibleBlast.API.DataAccess
{
    public partial class SqlServerAppContext : DbContext
    {
        public SqlServerAppContext(DbContextOptions<SqlServerAppContext> options) : base(options) { }

        public DbSet<Kid> Kids { get; set; }
        // public DbSet<Parent> Parents { get; set; }
        // public DbSet<KidParent> KidParents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplySingularTableNameConvention();

            // modelBuilder.Entity<KidParent>()
            //     .HasKey(x => new { x.Kid, x.ParentId });

            // modelBuilder.Entity<KidParent>()
            //     .HasOne(x => x.Kid)
            //     .WithMany(x => x.Parents)
            //     .HasForeignKey(x => x.KidId)
            //     .OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<KidParent>()
            //     .HasOne(x => x.Parent)
            //     .WithMany(x => x.Kids)
            //     .HasForeignKey(x => x.ParentId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
