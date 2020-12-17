using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhysicalPersonsDirectory.Common.Resources;
using PhysicalPersonsDirectory.Domain;
using PhysicalPersonsDirectory.Domain.DomainClasses;
using PhysicalPersonsDirectory.Services.Models.Paging;
using PhysicalPersonsDirectory.Services.Models.Person.Add;
using PhysicalPersonsDirectory.Services.Models.Person.Delete;
using PhysicalPersonsDirectory.Services.Models.Person.Edit;
using PhysicalPersonsDirectory.Services.Models.Person.Get;
using PhysicalPersonsDirectory.Services.Models.Person.Shared;
using PhysicalPersonsDirectory.Services.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static PhysicalPersonsDirectory.Services.Services.Helpers.Paging;
using static PhysicalPersonsDirectory.Services.Services.Helpers.ServiceResponse;
using PhoneType = PhysicalPersonsDirectory.Common.Enums.PhoneType.PhoneType;

namespace PhysicalPersonsDirectory.Services.Services.Concrete
{
    public class PersonService : IPersonService
    {
        private readonly PhysicalPersonsContext _db;
        public IConfiguration Configuration { get; }
        public PersonService(PhysicalPersonsContext db, IConfiguration configuration)
        {
            _db = db;
            Configuration = configuration;
        }

        #region --  private methods --

        private PersonImageResponseModel saveImage(PersonImageRequestModel personImage)
        {
            var directoryPath = Path.Combine("Resources", "Images");
            var imagePath = Path.Combine(directoryPath, $"{personImage.PersonId}.JPG");
            var imageBytes = Convert.FromBase64String(personImage.Image);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            if (imageBytes.Length > 0)
            {
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    stream.Write(imageBytes, 0, imageBytes.Length);
                    stream.Flush();
                }
            }

            return Success(new PersonImageResponseModel() { ImagePatch = imagePath });
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
                if (relatedPersons.Count == 0)
                {
                    return Fail(new DeletePersonResponse(), RsStrings.PersonNotFound);
                }
                else
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
                var person = _db.Persons.Where(t => t.Id == request.PersonId).FirstOrDefault();
                person.ImagePatch = rs.ImagePatch;
                _db.SaveChanges();

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
                var person = _db.Persons.Where(t => t.Id == request.PersonId).FirstOrDefault();
                person.ImagePatch = rs.ImagePatch;
                _db.SaveChanges();

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
                var persons = _db.Persons.AsNoTracking().Where(t => t.Id == request.PersonId || t.Id == request.RelatedPersonId).ToList();
                if (persons.Count == 0)
                {
                    return Fail(new AddRelatedPersonResponse(), RsStrings.PersonAndRelatedPersonNotFound);
                }

                if (!persons.Where(t => t.Id == request.PersonId).Any())
                {
                    return Fail(new AddRelatedPersonResponse(), RsStrings.PersonNotFound);
                }

                if (!persons.Where(t => t.Id == request.RelatedPersonId).Any())
                {
                    return Fail(new AddRelatedPersonResponse(), RsStrings.RelatedPersonNotFound);
                }

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
                var relatedItem = _db.RelatedPersons.Where(t => t.Id == request.Id).FirstOrDefault();
                if (relatedItem == null)
                {
                    return Fail(new DeleteRelatedPersonResponse(), RsStrings.DeleteRelatedPersonNotFound);
                }

                _db.RelatedPersons.Remove(relatedItem);
                _db.SaveChanges();

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
                var personDetails = _db.Persons.AsNoTracking()
                                       .Include(p => p.PersonPhones).AsNoTracking()
                                       .Include(rp => rp.RelatedPersons).AsNoTracking()
                                       .Where(t => t.Id == request.PersonId)
                                       .FirstOrDefault();

                var relatedPersonIds = personDetails.RelatedPersons.Select(t => t.RelatedPersonId).ToList();
                var relatedPersonDetails = _db.Persons.AsNoTracking()
                                              .Include(p => p.PersonPhones).AsNoTracking()
                                              .Where(t => relatedPersonIds.Contains(t.Id))
                                              .ToList();

                var result = new PersonDetailsBaseModel
                {
                    PersonId = personDetails.Id,
                    Fname = personDetails.Fname,
                    Lname = personDetails.Lname,
                    PersonalNumber = personDetails.PersonalNumber,
                    BirthDate = personDetails.BirthDate,
                    GenderId = personDetails.GenderId,
                    CityId = personDetails.CityId,
                    Image = !string.IsNullOrEmpty(personDetails.ImagePatch) ? File.ReadAllBytes(personDetails.ImagePatch) : null, //new byte[] { },

                    PersonPhones = personDetails.PersonPhones.Select(p => new PersonPhoneModel
                    {
                        Id = p.Id,
                        PersonId = p.PersonId,
                        PhoneNumber = p.PhoneNumber,
                        PhoneType = ((PhoneType)p.PhoneTypeId).ToString()
                    }).ToList(),

                    RelatedPersons = relatedPersonDetails.Select(t => new PersonDetailsBaseModel
                    {
                        PersonId = t.Id,
                        Fname = t.Fname,
                        Lname = t.Lname,
                        PersonalNumber = t.PersonalNumber,
                        BirthDate = t.BirthDate,
                        GenderId = t.GenderId,
                        CityId = t.CityId,
                        Image = !string.IsNullOrEmpty(t.ImagePatch) ? File.ReadAllBytes(t.ImagePatch) : null, //new byte[] { },

                        PersonPhones = t.PersonPhones.Select(p => new PersonPhoneModel
                        {
                            Id = p.Id,
                            PersonId = p.PersonId,
                            PhoneNumber = p.PhoneNumber,
                            PhoneType = ((PhoneType)p.PhoneTypeId).ToString()
                        }).ToList()
                    }).ToList()
                };

                return Success(new GetPersonDetailsResponse() { PersonDetails = result });
            }
            catch (Exception ex)
            {
                return Error(new GetPersonDetailsResponse(), RsStrings.GetPersonDetailsUnexpectedException);
            }
        }

        public GetPersonResponse GetPersons(GetPersonRequest request)
        {
            throw new Exception();
            try
            {
                if (request.PagingModel == null)
                {
                    request.PagingModel = new PagingBaseRequestModel() { Sorting = new SortingModel { SortBy = "CreateDate" } };
                }

                var phoneQuery = (from t in _db.PersonPhones.AsNoTracking() select t);
                var personsQuery = (from p in _db.Persons.AsNoTracking()
                                    join g in _db.Genders.AsNoTracking() on p.GenderId equals g.Id
                                    join c in _db.Citys.AsNoTracking() on p.CityId equals c.Id
                                    where (request.Fname == null || p.Fname.Contains(request.Fname))
                                       && (request.Lname == null || p.Lname.Contains(request.Lname))
                                       && (request.PersonalNumber == null || p.PersonalNumber.Contains(request.PersonalNumber))
                                       && (request.BirthDateFrom == null || p.BirthDate >= request.BirthDateFrom)
                                       && (request.BirthDateTo == null || p.BirthDate <= request.BirthDateFrom)
                                       && (request.GenderId == null || p.GenderId == request.GenderId)
                                       && (request.CityId == null || p.CityId == request.CityId)
                                       && (request.PhoneNumber == null || phoneQuery.Where(pp => pp.PersonId == p.Id && pp.PhoneNumber.Contains(request.PhoneNumber)).Any())
                                    select new PersonBaseModel
                                    {
                                        PersonId = p.Id,
                                        Fname = p.Fname,
                                        Lname = p.Lname,
                                        PersonalNumber = p.PersonalNumber,
                                        BirthDate = p.BirthDate,
                                        GenderId = p.GenderId,
                                        Gender = g.GenderName,
                                        CityId = p.CityId,
                                        City = c.CityName
                                    });

                var persons = PaginatedList(personsQuery, request.PagingModel);
                var personIds = persons.Select(t => t.PersonId).ToList();
                var personPhones = phoneQuery.Where(t => personIds.Contains(t.PersonId)).ToList();

                foreach (var item in persons)
                {
                    item.PersonalNumber = personPhones.Where(t => t.PersonId == item.PersonId).Select(t => t.PhoneNumber).FirstOrDefault();
                }

                var result = new GetPersonResponse
                {
                    Persons = persons,
                    TotalCount = personsQuery.Count(),
                    CurrentElementStart = request.PagingModel.Paging.Offset,
                    CurrentElementEnd = request.PagingModel.Paging.Offset + request.PagingModel.Paging.Limit
                };

                return Success(result);
            }
            catch (Exception ex)
            {
                return Error(new GetPersonResponse(), RsStrings.GetPersonsUnexpectedException);
            }
        }

        public GetRelatedPersonResponse GetRelatedPersons()
        {
            try
            {
                var relatedPersons = (from p in _db.Persons.AsNoTracking()
                                      join g in _db.Genders.AsNoTracking() on p.GenderId equals g.Id
                                      join c in _db.Citys.AsNoTracking() on p.CityId equals c.Id
                                      join rp in _db.RelatedPersons.AsNoTracking() on p.Id equals rp.PersonId into rplj
                                      from rp in rplj.DefaultIfEmpty()
                                      join rt in _db.RelationTypes.AsNoTracking() on rp.RelationTypeId equals rt.Id into rtlj
                                      from rt in rtlj.DefaultIfEmpty()
                                      select new
                                      {
                                          PersonId = p.Id,
                                          Fname = p.Fname,
                                          Lname = p.Lname,
                                          BirthDate = p.BirthDate,
                                          Gender = g.GenderName,
                                          City = c.CityName,
                                          rt.RelationTypeName
                                      }).ToList();

                var groupRelatedPersons = (from t in relatedPersons
                                           group t by new { t.PersonId, t.Fname, t.Lname, t.BirthDate, t.Gender, t.City } into g
                                           select new PersonModel
                                           {
                                               PersonId = g.Key.PersonId,
                                               Fname = g.Key.Fname,
                                               Lname = g.Key.Lname,
                                               BirthDate = g.Key.BirthDate,
                                               Gender = g.Key.Gender,
                                               City = g.Key.City,
                                               RelatedPersonsByType = g.GroupBy(rt => rt.RelationTypeName).Select(rpt => new RelatedPersonsByTypeModel
                                               {
                                                   RelatedPersonCount = rpt.Count(),
                                                   RelationType = rpt.Key
                                               }).ToList()
                                           }).ToList();

                return Success(new GetRelatedPersonResponse() { RelatedPersons = groupRelatedPersons });
            }
            catch (Exception ex)
            {
                return Error(new GetRelatedPersonResponse(), RsStrings.GetRelatedPersonsUnexpectedException);
            }
        }
    }
}
