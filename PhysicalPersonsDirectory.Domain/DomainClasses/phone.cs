using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Domain.DomainClasses
{
    public class Phone
    {
        public int Id { get; set; }
        public int PhoneTypeId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
