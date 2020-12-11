using System.Collections.Generic;

namespace PhysicalPersonsDirectory.Domain.DomainClasses
{
    public class PhoneType
    {
        public int Id { get; set; }
        public string PhoneTypeName { get; set; }

        public IList<Phone> Phones { get; set; }
    }
}
