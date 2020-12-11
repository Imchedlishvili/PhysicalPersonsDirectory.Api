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
       
        public string PersonalNumber { get; set; }        
        public DateTime BirthDate { get; set; }
        public string ImagePatch { get; set; }

        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public IList<PersonPhone> PersonPhones { get; set; }
        public IList<RelatedPerson> RelatedPersons { get; set; }
    }
}
