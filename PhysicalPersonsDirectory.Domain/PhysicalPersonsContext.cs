using Microsoft.EntityFrameworkCore;
using PhysicalPersonsDirectory.Domain.DomainClasses;
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_Gender_ID");
                entity.Property(t => t.Id).ValueGeneratedNever();
                entity.Property(t => t.GenderName).IsRequired();
            });

            modelBuilder.Entity<PhoneType>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_PhoneType_ID");
                entity.Property(t => t.Id).ValueGeneratedNever();
                entity.Property(t => t.PhoneTypeName).IsRequired();
            });

            modelBuilder.Entity<RelationType>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_RelationType_ID");
                entity.Property(t => t.Id).ValueGeneratedNever();
                entity.Property(t => t.RelationTypeName).IsRequired();
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_City_ID");
                entity.Property(t => t.CityName).IsRequired();
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_Phone_ID");
                entity.HasOne<PhoneType>(pt => pt.PhoneType)
                      .WithMany(p => p.Phones)
                      .HasForeignKey(pt => pt.PhoneTypeId);
                entity.Property(t => t.PhoneNumber).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_Person_ID");
                entity.HasOne<Gender>(g => g.Gender)
                      .WithMany(p => p.Persons)
                      .HasForeignKey(g=>g.Gender);
                entity.HasOne<City>(c => c.City)
                      .WithMany(p => p.Persons)
                      .HasForeignKey(c => c.CityId);

                entity.Property(t => t.Fname).IsRequired();
                entity.Property(t => t.Lname).IsRequired();
                entity.Property(t => t.PersonalNumber).IsRequired().HasMaxLength(11);
                entity.Property(t => t.BirthDate).IsRequired();
            });
        }

        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<PhoneType> PhoneTypes { get; set; }
        public virtual DbSet<RelationType> RelationTypes { get; set; }

        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonPhone> PersonPhones { get; set; }
        public virtual DbSet<RelatedPerson> RelatedPersons { get; set; }

    }
}
