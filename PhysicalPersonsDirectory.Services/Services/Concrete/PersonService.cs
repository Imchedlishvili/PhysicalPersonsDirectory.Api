using Microsoft.EntityFrameworkCore;
using PhysicalPersonsDirectory.Domain;
using PhysicalPersonsDirectory.Domain.DomainClasses;
using PhysicalPersonsDirectory.Services.Models.Person.Add;
using PhysicalPersonsDirectory.Services.Models.Person.Delete;
using PhysicalPersonsDirectory.Services.Models.Person.Edit;
using PhysicalPersonsDirectory.Services.Models.Person.Get;
using PhysicalPersonsDirectory.Services.Services.Abstract;
using System;
using System.Linq;
using System.Collections.Generic;
using static PhysicalPersonsDirectory.Services.Services.Helpers.ServiceResponse;
using PhysicalPersonsDirectory.Common.Resources;
using PhoneType = PhysicalPersonsDirectory.Common.Enums.PhoneType.PhoneType;

namespace PhysicalPersonsDirectory.Services.Services.Concrete
{
    public class PersonService : IPersonService
    {
        private readonly PhysicalPersonsContext _db;
        public PersonService(PhysicalPersonsContext db)
        {
            _db = db;
        }


        public AddPersonResponse AddPerson(AddPersonRequest request)
        {
            try
            {
                var person = new Person()
                {
                    Fname = request.Fname,
                    Lname = request.Lname,
                    PersonalNumber = request.PersonalNumber,
                    BirthDate = request.BirthDate,
                    ImagePatch = request.ImagePatch,
                    GenderId = request.GenderId,
                    CityId = request.CityId,
                    PersonPhones = request.PersonPhones?.Select(t => new PersonPhone { PhoneNumber = t.PhoneNumber, PhoneTypeId = (int)Enum.Parse(typeof(PhoneType), t.PhoneType) }).ToList()
                };

                _db.Persons.Add(person);
                _db.SaveChanges();

                return Success(new AddPersonResponse() { PersonId = person.Id });
            }
            catch (Exception ex)
            {
                return Error(new AddPersonResponse(), RsStrings.AddPersonException);
            }
        }

        public EditPersonResponse EditPerson(EditPersonRequest request)
        {
            try
            {
                var person = _db.Persons.Where(t => t.Id == request.PersonId.GetValueOrDefault()).Include(t => t.PersonPhones).FirstOrDefault();

                person.Fname = request.Fname;
                person.Lname = request.Lname;
                person.PersonalNumber = request.PersonalNumber;
                person.BirthDate = request.BirthDate;
                person.GenderId = request.GenderId;
                person.CityId = request.CityId;

                if (request.PersonPhones == null || request.PersonPhones.Count == 0)
                {
                    if (person.PersonPhones != null)
                    {
                        person.PersonPhones = null;
                        //_db.PersonPhones.RemoveRange(person.PersonPhones.ToList());
                    }
                }
                else
                {
                    var personPhones = person.PersonPhones.ToList();
                    if (personPhones == null)
                    {
                        var newPhones = request.PersonPhones.Select(t => new PersonPhone
                        {
                            PhoneNumber = t.PhoneNumber,
                            PhoneTypeId = (int)Enum.Parse(typeof(PhoneType), t.PhoneType)
                        }).ToList();

                        _db.PersonPhones.AddRange(newPhones);
                    }
                    else
                    {
                        var newPhones = (from t in request.PersonPhones
                                         where t.Id == null
                                         select new PersonPhone
                                         {
                                             PhoneNumber = t.PhoneNumber,
                                             PhoneTypeId = (int)Enum.Parse(typeof(PhoneType), t.PhoneType)
                                         }).ToList();

                        var phoneIds = request.PersonPhones.Where(t => t.Id != null).Select(t => t.Id).ToList();
                        var removedPhones = personPhones.Where(t => !phoneIds.Contains(t.Id)).ToList();

                        var personPhoneIds = personPhones.Select(t => t.Id).ToList();
                        var editedPhones = (from t in request.PersonPhones
                                            where t.Id != null
                                               && personPhoneIds.Contains(t.Id.Value)
                                            select new PersonPhone
                                            {
                                                Id = t.Id.Value,
                                                PhoneNumber = t.PhoneNumber
                                            }).ToList();


                        foreach (var item in request.PersonPhones)
                        {
                            var personPhone = personPhones.Where(t => t.Id == item.Id).FirstOrDefault();
                            personPhone.PhoneNumber = item.PhoneNumber;
                        }

                    }
                }

                return Success(new EditPersonResponse());
            }
            catch (Exception ex)
            {
                return Error(new EditPersonResponse(), "Unexpected error occured.");
            }
        }

        public DeletePersonResponse DeletePerson(DeletePersonRequest request)
        {
            try
            {

                return Success(new DeletePersonResponse());
            }
            catch (Exception ex)
            {
                return Error(new DeletePersonResponse(), "Unexpected error occured.");
            }
        }

        public AddPersonImageResponse AddPersonImage(AddPersonImageRequest request)
        {
            try
            {

                return Success(new AddPersonImageResponse());
            }
            catch (Exception ex)
            {
                return Error(new AddPersonImageResponse(), "Unexpected error occured.");
            }
        }

        public EditPersonImageResponse EditPersonImage(EditPersonImageRequest request)
        {
            try
            {

                return Success(new EditPersonImageResponse());
            }
            catch (Exception ex)
            {
                return Error(new EditPersonImageResponse(), "Unexpected error occured.");
            }
        }

        public AddRelatedPersonResponse AddRelatedPerson(AddRelatedPersonRequest request)
        {
            try
            {

                return Success(new AddRelatedPersonResponse());
            }
            catch (Exception ex)
            {
                return Error(new AddRelatedPersonResponse(), "Unexpected error occured.");
            }
        }

        public DeleteRelatedPersonResponse DeleteRelatedPerson(DeleteRelatedPersonRequest request)
        {
            try
            {

                return Success(new DeleteRelatedPersonResponse());
            }
            catch (Exception ex)
            {
                return Error(new DeleteRelatedPersonResponse(), "Unexpected error occured.");
            }
        }

        public GetPersonDetailsResponse GetPersonDetails(GetPersonDetailsRequest request)
        {
            try
            {

                return Success(new GetPersonDetailsResponse());
            }
            catch (Exception ex)
            {
                return Error(new GetPersonDetailsResponse(), "Unexpected error occured.");
            }
        }

        public GetPersonResponse GetPersons(GetPersonRequest request)
        {
            try
            {

                return Success(new GetPersonResponse());
            }
            catch (Exception ex)
            {
                return Error(new GetPersonResponse(), "Unexpected error occured.");
            }
        }

        public GetRelatedPersonResponse GetRelatedPersons(GetRelatedPersonRequest request)
        {
            try
            {

                return Success(new GetRelatedPersonResponse());
            }
            catch (Exception ex)
            {
                return Error(new GetRelatedPersonResponse(), "Unexpected error occured.");
            }
        }
    }
}
