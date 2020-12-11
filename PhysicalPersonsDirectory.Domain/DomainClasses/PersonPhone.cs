using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Domain.DomainClasses
{
    public class PersonPhone
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }

        public int PhoneTypeId { get; set; }
        public PhoneType PhoneType { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
