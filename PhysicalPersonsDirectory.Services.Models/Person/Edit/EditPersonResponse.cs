using PhysicalPersonsDirectory.Services.Models.Base;

namespace PhysicalPersonsDirectory.Services.Models.Person.Edit
{
    public class EditPersonResponse : ResponseBaseModel
    {
        public int PersonId { get; set; }
    }
}
