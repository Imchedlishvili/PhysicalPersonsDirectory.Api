using PhysicalPersonsDirectory.Services.Models.Person.Shared;
using System.Collections.Generic;

namespace PhysicalPersonsDirectory.Services.Models.Person.Add
{
    public class AddPersonRequest : PersonDetailsBaseModel
    {
        public List<PersonPhoneBaseModel> PersonPhones { get; set; }
    }
}
