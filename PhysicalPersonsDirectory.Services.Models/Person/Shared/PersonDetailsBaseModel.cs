using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class PersonDetailsBaseModel : PersonBaseModel
    {
        public List<RelatedPersonModel> RelatedPersons { get; set; }
    }
}
