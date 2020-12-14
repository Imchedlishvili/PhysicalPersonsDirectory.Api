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
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using PhysicalPersonsDirectory.Services.Models.Person.Shared;

namespace PhysicalPersonsDirectory.Services.Services.Concrete
{
    public class PersonService : IPersonService
    {
        private readonly PhysicalPersonsContext _db;
        public PersonService(PhysicalPersonsContext db)
        {
            _db = db;
        }

        #region --  private methods --

        private PersonImageResponseModel saveImage(PersonImageRequestModel personImage)
        {
            var d1 = AppContext.BaseDirectory;       //C:\Users\iago\source\MyRepos\PhysicalPersonsDirectory.Api\PhysicalPersonsDirectory.Api\bin\Debug\netcoreapp3.1\
            var d2 = AppDomain.CurrentDomain.BaseDirectory; //C:\Users\iago\source\MyRepos\PhysicalPersonsDirectory.Api\PhysicalPersonsDirectory.Api\bin\Debug\netcoreapp3.1\
            var d3 = Directory.GetCurrentDirectory(); //C:\Users\iago\source\MyRepos\PhysicalPersonsDirectory.Api\PhysicalPersonsDirectory.Api
            var d4 = Environment.CurrentDirectory;              //C:\Users\iago\source\MyRepos\PhysicalPersonsDirectory.Api\PhysicalPersonsDirectory.Api
            var d5 = this.GetType().Assembly.Location;          //C:\Users\iago\source\MyRepos\PhysicalPersonsDirectory.Api\PhysicalPersonsDirectory.Api\bin\Debug\netcoreapp3.1\PhysicalPersonsDirectory.Services.dll

            var t1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); //C:\Users\iago\source\MyRepos\PhysicalPersonsDirectory.Api\PhysicalPersonsDirectory.Api\bin\Debug\netcoreapp3.1
                                                                                      //var t2 = Path.GetDirectoryName(Application.ExecutablePath);
            var t3 = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);     //C:\Users\iago\source\MyRepos\PhysicalPersonsDirectory.Api\PhysicalPersonsDirectory.Api\bin\Debug\netcoreapp3.1

            var t15 = Path.GetDirectoryName("../PhysicalPersonsDirectory.Api/Resources/Images");
            var t10 = Path.GetDirectoryName("..\\PhysicalPersonsDirectory.Api\\Resources\\Images");
            var t11 = Path.GetDirectoryName("~\\PhysicalPersonsDirectory.Api\\Resources\\Images"); //C:\Users\iago\source\MyRepos\PhysicalPersonsDirectory.Api\PhysicalPersonsDirectory.Api\Resources\Images
            var t20 = Path.GetDirectoryName("~/PhysicalPersonsDirectory.Api/Resources/Images");


            //var d6 = System.Net.Mime.MediaTypeNames.Application.ExecutablePath;
            //var d7 = System.Net.Mime.MediaTypeNames.Application.StartupPath;

            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            return Success(new PersonImageResponseModel() { ImagePatch = "" });
        }

        #endregion


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
                return Error(new AddPersonResponse(), RsStrings.AddPersonUnexpectedException);
            }
        }

        public EditPersonResponse EditPerson(EditPersonRequest request)
        {
            try
            {
                var person = _db.Persons.Where(t => t.Id == request.PersonId).Include(t => t.PersonPhones).FirstOrDefault();

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

                        var newPhones = (from t in request.PersonPhones
                                         where t.Id == null
                                         select new PersonPhone
                                         {
                                             PhoneNumber = t.PhoneNumber,
                                             PhoneTypeId = (int)Enum.Parse(typeof(PhoneType), t.PhoneType)
                                         }).ToList();

                        _db.PersonPhones.RemoveRange(removedPhones);
                        _db.PersonPhones.UpdateRange(editedPhones);
                        _db.PersonPhones.AddRange(newPhones);
                        _db.SaveChanges();
                    }
                }

                return Success(new EditPersonResponse() { PersonId = person.Id });
            }
            catch (Exception ex)
            {
                return Error(new EditPersonResponse(), RsStrings.EditPersonUnexpectedException);
            }
        }

        public DeletePersonResponse DeletePerson(DeletePersonRequest request)
        {
            try
            {
                var relatedPersons = _db.RelatedPersons.Where(t => t.PersonId == request.PersonId || t.RelatedPersonId == request.PersonId).ToList();
                if (relatedPersons != null && relatedPersons.Count > 0)
                {
                    _db.RelatedPersons.RemoveRange(relatedPersons);
                    _db.SaveChanges();
                }

                return Success(new DeletePersonResponse());
            }
            catch (Exception ex)
            {
                return Error(new DeletePersonResponse(), RsStrings.DeletePersonUnexpectedException);
            }
        }

        public AddPersonImageResponse AddPersonImage(AddPersonImageRequest request)
        {
            try
            {
                var rs = saveImage(request);
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
                var rs = saveImage(request);
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
                var relatedPerson = new RelatedPerson
                {
                    PersonId = request.PersonId,
                    RelatedPersonId = request.RelatedPersonId,
                    RelationTypeId = (int)Enum.Parse(typeof(RelationType), request.RelationType)
                };

                _db.RelatedPersons.Add(relatedPerson);
                _db.SaveChanges();

                return Success(new AddRelatedPersonResponse());
            }
            catch (Exception ex)
            {
                return Error(new AddRelatedPersonResponse(), RsStrings.AddRelatedPersonUnexpectedException);
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
