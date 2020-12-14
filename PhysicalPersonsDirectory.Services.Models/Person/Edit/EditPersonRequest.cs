using PhysicalPersonsDirectory.Services.Models.Person.Shared;
using System.Collections.Generic;

namespace PhysicalPersonsDirectory.Services.Models.Person.Edit
{
    public class EditPersonRequest : PersonDetailsBaseModel
    {
        public int? PersonId { get; set; }
        public List<PersonPhoneModel> PersonPhones { get; set; }
    }
}
