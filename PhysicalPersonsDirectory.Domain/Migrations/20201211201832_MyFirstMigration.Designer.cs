﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhysicalPersonsDirectory.Domain;

namespace PhysicalPersonsDirectory.Domain.Migrations
{
    [DbContext(typeof(PhysicalPersonsContext))]
    [Migration("20201211201832_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_City_ID");

                    b.ToTable("Citys");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.Gender", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("GenderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_Gender_ID");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePatch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PersonalNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("Id")
                        .HasName("PK_Person_ID");

                    b.HasIndex("CityId");

                    b.HasIndex("GenderId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.PersonPhone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PhoneTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_PersonPhone_ID");

                    b.HasIndex("PersonId");

                    b.HasIndex("PhoneTypeId");

                    b.ToTable("PersonPhones");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.PhoneType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("PhoneTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_PhoneType_ID");

                    b.ToTable("PhoneTypes");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.RelatedPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("RelatedPersonId")
                        .HasColumnType("int");

                    b.Property<int>("RelationTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_RelatedPerson_ID");

                    b.HasIndex("RelatedPersonId");

                    b.HasIndex("RelationTypeId");

                    b.ToTable("RelatedPersons");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.RelationType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("RelationTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_RelationType_ID");

                    b.ToTable("RelationTypes");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.Person", b =>
                {
                    b.HasOne("PhysicalPersonsDirectory.Domain.DomainClasses.City", "City")
                        .WithMany("Persons")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhysicalPersonsDirectory.Domain.DomainClasses.Gender", "Gender")
                        .WithMany("Persons")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.PersonPhone", b =>
                {
                    b.HasOne("PhysicalPersonsDirectory.Domain.DomainClasses.Person", "Person")
                        .WithMany("PersonPhones")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhysicalPersonsDirectory.Domain.DomainClasses.PhoneType", "PhoneType")
                        .WithMany("PersonPhones")
                        .HasForeignKey("PhoneTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("PhoneType");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.RelatedPerson", b =>
                {
                    b.HasOne("PhysicalPersonsDirectory.Domain.DomainClasses.Person", "Person")
                        .WithMany("RelatedPersons")
                        .HasForeignKey("RelatedPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhysicalPersonsDirectory.Domain.DomainClasses.RelationType", "RelationType")
                        .WithMany("RelatedPersons")
                        .HasForeignKey("RelationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("RelationType");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.City", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.Gender", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.Person", b =>
                {
                    b.Navigation("PersonPhones");

                    b.Navigation("RelatedPersons");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.PhoneType", b =>
                {
                    b.Navigation("PersonPhones");
                });

            modelBuilder.Entity("PhysicalPersonsDirectory.Domain.DomainClasses.RelationType", b =>
                {
                    b.Navigation("RelatedPersons");
                });
#pragma warning restore 612, 618
        }
    }
}
