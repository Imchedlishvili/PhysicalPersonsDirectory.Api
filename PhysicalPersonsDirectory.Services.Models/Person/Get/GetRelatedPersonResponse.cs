using PhysicalPersonsDirectory.Services.Models.Base;
using PhysicalPersonsDirectory.Services.Models.Person.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Get
{
    public class GetRelatedPersonResponse : ResponseBaseModel
    {
        public List<PersonBaseModel> Persons { get; set; }
    }
}
