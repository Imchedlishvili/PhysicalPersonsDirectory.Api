using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Domain.DomainClasses
{
    public class PersonPhone
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int PhoneId { get; set; }
        public bool IsDefault { get; set; }
    }
}
