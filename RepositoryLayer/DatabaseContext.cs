using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace RepositoryLayer
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Users>(user =>
            {
                user.HasIndex(x => x.UserName).IsUnique(true);
            });
        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Calculations> Calculations { get; set; }
    }
}
