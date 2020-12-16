using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class PersonModel : PersonBaseModel
    {
        public List<RelatedPersonsByTypeModel> RelatedPersonsByType { get; set; }
    }
}
