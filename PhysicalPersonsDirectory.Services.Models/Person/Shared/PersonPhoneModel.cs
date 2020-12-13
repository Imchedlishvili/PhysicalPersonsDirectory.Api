using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class PersonPhoneModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneTypeId { get; set; }
    }
}
