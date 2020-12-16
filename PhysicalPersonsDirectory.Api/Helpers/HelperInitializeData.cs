using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhysicalPersonsDirectory.Common.Resources;
using PhysicalPersonsDirectory.Domain.DomainClasses;
using System;
using System.Linq;
using _PhoneType = PhysicalPersonsDirectory.Common.Enums.PhoneType.PhoneType;
using _Gender = PhysicalPersonsDirectory.Common.Enums.Gender.Gender;
using _RelationType = PhysicalPersonsDirectory.Common.Enums.RelationType.RelationType;
using PhysicalPersonsDirectory.Domain;
using System.Collections.Generic;

namespace PhysicalPersonsDirectory.Api.Helpers
{
    static class HelperInitializeData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                using (var context = new PhysicalPersonsContext(serviceProvider.GetRequiredService<DbContextOptions<PhysicalPersonsContext>>()))
                {
                    if (context.Genders.Any())
                    {
                        return;
                    }

                    context.Genders.AddRange
                        (
                            new Gender { Id = (int)_Gender.Male, GenderName = RsStrings.Male },
                            new Gender { Id = (int)_Gender.Female, GenderName = RsStrings.Female }
                        );

                    context.PhoneTypes.AddRange
                        (
                        new PhoneType { Id = (int)_PhoneType.Mobile, PhoneTypeName = RsStrings.Mobile },
                        new PhoneType { Id = (int)_PhoneType.Office, PhoneTypeName = RsStrings.Office },
                        new PhoneType { Id = (int)_PhoneType.Home, PhoneTypeName = RsStrings.Home }
                        );

                    context.RelationTypes.AddRange
                        (
                            new RelationType { Id = (int)_RelationType.Colleague, RelationTypeName = RsStrings.Colleague },
                            new RelationType { Id = (int)_RelationType.Familiar, RelationTypeName = RsStrings.Familiar },
                            new RelationType { Id = (int)_RelationType.Relative, RelationTypeName = RsStrings.Relative },
                            new RelationType { Id = (int)_RelationType.Other, RelationTypeName = RsStrings.Other }
                        );

                    context.Citys.AddRange
                        (
                        new City { CityName = RsStrings.Tbilisi },
                        new City { CityName = RsStrings.Batumi },
                        new City { CityName = RsStrings.Kutaisi },
                        new City { CityName = RsStrings.Rustavi },
                        new City { CityName = RsStrings.Gori },
                        new City { CityName = RsStrings.Zugdidi },
                        new City { CityName = RsStrings.Poti },
                        new City { CityName = RsStrings.Sighnaghi }
                        );

                    context.SaveChanges();

                    context.Persons.AddRange(
                        new Person
                        {
                            Fname = "ხვიჩა",
                            Lname = "ჭელიძე",
                            PersonalNumber = "12345678911",
                            BirthDate = DateTime.Now.AddYears(-50),
                            GenderId = (int)_Gender.Male,
                            CityId = context.Citys.Where(t => t.CityName == RsStrings.Tbilisi).Select(t => t.Id).FirstOrDefault(),
                            PersonPhones = new List<PersonPhone>()
                            {
                                new PersonPhone{ PhoneNumber = "598504343", PhoneTypeId = (int)_PhoneType.Mobile },
                                new PersonPhone{ PhoneNumber = "0322252525", PhoneTypeId = (int)_PhoneType.Home }
                            }
                        },
                        new Person
                        {
                            Fname = "გოჩა",
                            Lname = "ბანძელაძე",
                            PersonalNumber = "12345678922",
                            BirthDate = DateTime.Now.AddYears(-55),
                            GenderId = (int)_Gender.Male,
                            CityId = context.Citys.Where(t => t.CityName == RsStrings.Tbilisi).Select(t => t.Id).FirstOrDefault(),
                            PersonPhones = new List<PersonPhone>()
                            {
                                new PersonPhone{ PhoneNumber = "598505343", PhoneTypeId = (int)_PhoneType.Mobile },
                                new PersonPhone{ PhoneNumber = "0322404040", PhoneTypeId = (int)_PhoneType.Office }
                            }
                        },
                        new Person
                        {
                            Fname = "ნათია",
                            Lname = "მელაძე",
                            PersonalNumber = "12345678933",
                            BirthDate = DateTime.Now.AddYears(-59),
                            GenderId = (int)_Gender.Male,
                            CityId = context.Citys.Where(t => t.CityName == RsStrings.Batumi).Select(t => t.Id).FirstOrDefault(),
                            PersonPhones = new List<PersonPhone>()
                            {
                                new PersonPhone{ PhoneNumber = "598555343", PhoneTypeId = (int)_PhoneType.Mobile }
                            }
                        },
                        new Person
                        {
                            Fname = "ქეთი",
                            Lname = "მაღლაკელიძე",
                            PersonalNumber = "12345678944",
                            BirthDate = DateTime.Now.AddYears(-59),
                            GenderId = (int)_Gender.Male,
                            CityId = context.Citys.Where(t => t.CityName == RsStrings.Kutaisi).Select(t => t.Id).FirstOrDefault(),
                            PersonPhones = new List<PersonPhone>()
                            {
                                new PersonPhone{ PhoneNumber = "598565343", PhoneTypeId = (int)_PhoneType.Mobile },
                                new PersonPhone{ PhoneNumber = "0322888888", PhoneTypeId = (int)_PhoneType.Office },
                                new PersonPhone{ PhoneNumber = "0322555555", PhoneTypeId = (int)_PhoneType.Home }
                            }
                        });

                    context.SaveChanges();

                    context.RelatedPersons.AddRange
                        (
                            new RelatedPerson
                            {
                                PersonId = context.Persons.Where(t => t.Fname == "ხვიჩა").Select(t => t.Id).FirstOrDefault(),
                                RelatedPersonId = context.Persons.Where(t => t.Fname == "გოჩა").Select(t => t.Id).FirstOrDefault(),
                                RelationTypeId = (int)_RelationType.Familiar
                            },
                           new RelatedPerson
                           {
                               PersonId = context.Persons.Where(t => t.Fname == "ხვიჩა").Select(t => t.Id).FirstOrDefault(),
                               RelatedPersonId = context.Persons.Where(t => t.Fname == "ნათია").Select(t => t.Id).FirstOrDefault(),
                               RelationTypeId = (int)_RelationType.Colleague
                           },
                           new RelatedPerson
                           {
                               PersonId = context.Persons.Where(t => t.Fname == "ხვიჩა").Select(t => t.Id).FirstOrDefault(),
                               RelatedPersonId = context.Persons.Where(t => t.Fname == "ქეთი").Select(t => t.Id).FirstOrDefault(),
                               RelationTypeId = (int)_RelationType.Relative
                           },
                           new RelatedPerson
                           {
                               PersonId = context.Persons.Where(t => t.Fname == "გოჩა").Select(t => t.Id).FirstOrDefault(),
                               RelatedPersonId = context.Persons.Where(t => t.Fname == "ქეთი").Select(t => t.Id).FirstOrDefault(),
                               RelationTypeId = (int)_RelationType.Colleague
                           }
                        );

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
