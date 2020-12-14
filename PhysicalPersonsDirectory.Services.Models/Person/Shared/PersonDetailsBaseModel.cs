using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class PersonDetailsBaseModel : PersonBaseModel
    {
        public List<PersonPhoneModel> PersonPhones { get; set; }
        public List<PersonDetailsBaseModel> RelatedPersons { get; set; }
    }
}
