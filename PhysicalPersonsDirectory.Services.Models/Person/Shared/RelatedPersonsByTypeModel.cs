using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class RelatedPersonsByTypeModel
    {
        public int RelatedPersonCount { get; set; }
        public string RelationType { get; set; }
    }
}
