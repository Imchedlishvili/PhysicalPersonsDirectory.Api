using PhysicalPersonsDirectory.Services.Models.Person.Shared;
using System;
using System.Collections.Generic;

namespace PhysicalPersonsDirectory.Services.Models.Person.Add
{
    public class AddPersonRequest 
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public int CityId { get; set; }
        public List<PersonPhoneBaseModel> PersonPhones { get; set; }
    }
}
