using PhysicalPersonsDirectory.Services.Models.Person.Add;
using PhysicalPersonsDirectory.Services.Models.Person.Delete;
using PhysicalPersonsDirectory.Services.Models.Person.Edit;
using PhysicalPersonsDirectory.Services.Models.Person.Get;

namespace PhysicalPersonsDirectory.Services.Services.Abstract
{
    public interface IPersonService
    {
        AddPersonResponse AddPerson(AddPersonRequest request);
        EditPersonResponse EditPerson(EditPersonRequest request);
        DeletePersonResponse DeletePerson(DeletePersonRequest request);
        AddPersonImageResponse AddPersonImage(AddPersonImageRequest request);
        EditPersonImageResponse EditPersonImage(EditPersonImageRequest request);
        AddRelatedPersonResponse AddRelatedPerson(AddRelatedPersonRequest request);
        DeleteRelatedPersonResponse DeleteRelatedPerson(DeleteRelatedPersonRequest request);
        GetPersonDetailsResponse GetPersonDetails(GetPersonDetailsRequest request);
        GetPersonResponse GetPersons(GetPersonRequest request);
        GetRelatedPersonResponse GetRelatedPersons();
    }
}
