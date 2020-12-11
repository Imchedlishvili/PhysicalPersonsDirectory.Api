using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Domain.DomainClasses
{
    public class Person
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int GenderId { get; set; }
        public string PersonalNumber { get; set; }        
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public string ImagePatch { get; set; }        
    }
}
