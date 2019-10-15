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

        public DbSet<Users> Users { get; set; }

        public DbSet<Calculations> Calculations { get; set; }
    }
}
