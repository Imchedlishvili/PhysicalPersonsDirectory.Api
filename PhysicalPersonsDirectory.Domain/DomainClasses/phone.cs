namespace PhysicalPersonsDirectory.Domain.DomainClasses
{
    public class Phone
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }

        public int PhoneTypeId { get; set; }
        public PhoneType PhoneType { get; set; }
    }
}
