namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class PersonPhoneModel : PersonPhoneBaseModel
    {
        public int? Id { get; set; }
        public int? PersonId { get; set; }
    }
}
