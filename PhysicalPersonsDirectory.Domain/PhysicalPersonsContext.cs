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

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_Person_ID");
                entity.HasOne<Gender>(g => g.Gender)
                      .WithMany(p => p.Persons)
                      .HasForeignKey(g => g.GenderId);
                entity.HasOne<City>(c => c.City)
                      .WithMany(p => p.Persons)
                      .HasForeignKey(c => c.CityId);
                entity.Property(t => t.Fname).IsRequired().HasMaxLength(50);
                entity.Property(t => t.Lname).IsRequired().HasMaxLength(50);
                entity.Property(t => t.PersonalNumber).IsRequired().HasMaxLength(11);
                entity.Property(t => t.BirthDate).IsRequired();
            });

            modelBuilder.Entity<PersonPhone>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("PK_PersonPhone_ID");
                entity.HasOne<PhoneType>(pt => pt.PhoneType)
                      .WithMany(pp => pp.PersonPhones)
                      .HasForeignKey(pt => pt.PhoneTypeId);
                entity.HasOne<Person>(p => p.Person)
                      .WithMany(pp => pp.PersonPhones)
                      .HasForeignKey(pp => pp.PersonId);
                entity.Property(t => t.PhoneNumber).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<RelatedPerson>(entity =>
            {
                entity.HasKey(rp => rp.Id).HasName("PK_RelatedPerson_ID");
                entity.HasOne<Person>(p => p.Person)
                      .WithMany(rp => rp.RelatedPersons)
                      .HasForeignKey(rp => rp.PersonId);
                entity.HasOne<Person>(p => p.Person)
                     .WithMany(rp => rp.RelatedPersons)
                     .HasForeignKey(rp => rp.RelatedPersonId);
                entity.HasOne<RelationType>(rt => rt.RelationType)
                     .WithMany(rp => rp.RelatedPersons)
                     .HasForeignKey(rp => rp.RelationTypeId);
            });
        }


        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<PhoneType> PhoneTypes { get; set; }
        public virtual DbSet<RelationType> RelationTypes { get; set; }
        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonPhone> PersonPhones { get; set; }
        public virtual DbSet<RelatedPerson> RelatedPersons { get; set; }
    }
}
