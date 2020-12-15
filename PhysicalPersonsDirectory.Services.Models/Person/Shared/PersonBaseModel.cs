using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class PersonBaseModel
    {
        public int PersonId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

        public string PersonalNumber { get; set; }        
        public DateTime BirthDate { get; set; }
        public byte[] Image { get; set; }
        public string PhoneNumber { get; set; }

        public int GenderId { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }            
        public string City { get; set; }            
    }
}
