using PhysicalPersonsDirectory.Services.Models.Base;
using PhysicalPersonsDirectory.Services.Models.Person.Shared;

namespace PhysicalPersonsDirectory.Services.Models.Person.Get
{
    public class GetPersonDetailsResponse : ResponseBaseModel
    {
        public PersonDetailsBaseModel PersonDetails { get; set; }
    }
}
