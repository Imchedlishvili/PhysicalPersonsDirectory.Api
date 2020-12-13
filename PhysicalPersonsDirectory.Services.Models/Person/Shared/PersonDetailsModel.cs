using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class PersonDetailsModel 
    {
        public int? PersonId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImagePatch { get; set; }

        public int GenderId { get; set; }
        public int CityId { get; set; }

        public List<PersonPhoneModel> PersonPhones { get; set; }        
    }
}
