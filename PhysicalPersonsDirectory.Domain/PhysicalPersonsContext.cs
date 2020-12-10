using Microsoft.EntityFrameworkCore;
using System;

namespace PhysicalPersonsDirectory.Domain
{
    public class PhysicalPersonsContext : DbContext
    {
        public PhysicalPersonsContext(DbContextOptions<PhysicalPersonsContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new System.Exception("DbContext not configured - check appsettings.json and startup.cs");
            }

            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=VehicleDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
