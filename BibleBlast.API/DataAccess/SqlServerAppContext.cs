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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplySingularTableNameConvention();
        }
    }
}
