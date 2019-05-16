using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cheers.Domain.Entities
{
    public class CheersDbContext : DbContext
    {
        public CheersDbContext(DbContextOptions<CheersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Venue> Venues { get; set; }

    }
}
