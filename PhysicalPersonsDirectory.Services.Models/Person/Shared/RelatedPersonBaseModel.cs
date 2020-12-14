namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class RelatedPersonBaseModel
    {
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
        public int RelationTypeId { get; set; }
    }
}
