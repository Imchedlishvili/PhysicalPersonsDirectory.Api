namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class RelatedPersonBaseModel
    {
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
        public string RelationType { get; set; }
    }
}
