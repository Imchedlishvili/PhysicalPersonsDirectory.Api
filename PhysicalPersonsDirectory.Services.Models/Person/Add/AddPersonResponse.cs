using PhysicalPersonsDirectory.Services.Models.Base;

namespace PhysicalPersonsDirectory.Services.Models.Person.Add
{
    public class AddPersonResponse : ResponseBaseModel
    {
        public int PersonId { get; set; }
    }
}
