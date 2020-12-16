using Microsoft.AspNetCore.Mvc;
using PhysicalPersonsDirectory.Services.Models.Person.Add;
using PhysicalPersonsDirectory.Services.Models.Person.Delete;
using PhysicalPersonsDirectory.Services.Models.Person.Edit;
using PhysicalPersonsDirectory.Services.Models.Person.Get;
using PhysicalPersonsDirectory.Services.Services.Abstract;

namespace PhysicalPersonsDirectory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpPost("add")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<AddPersonResponse> AddPerson([FromBody] AddPersonRequest request)
        {
            return _personService.AddPerson(request);
        }

        [HttpPut("edit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<EditPersonResponse> EditPerson([FromBody] EditPersonRequest request)
        {
            return _personService.EditPerson(request);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<DeletePersonResponse> DeletePerson([FromBody] DeletePersonRequest request)
        {
            return _personService.DeletePerson(request);
        }

        [HttpPost("addimage")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<AddPersonImageResponse> AddPersonImage([FromBody] AddPersonImageRequest request)
        {
            return _personService.AddPersonImage(request);
        }

        [HttpPut("editmage")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<EditPersonImageResponse> EditPersonImage([FromBody] EditPersonImageRequest request)
        {
            return _personService.EditPersonImage(request);
        }

        [HttpPost("Addrelatedperson")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<AddRelatedPersonResponse> AddRelatedPerson([FromBody] AddRelatedPersonRequest request)
        {
            return _personService.AddRelatedPerson(request);
        }

        [HttpDelete("deleterelatedperson")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<DeleteRelatedPersonResponse> DeleteRelatedPerson([FromBody] DeleteRelatedPersonRequest request)
        {
            return _personService.DeleteRelatedPerson(request);
        }

        [HttpGet("persondetails")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<GetPersonDetailsResponse> GetPersonDetails([FromQuery] GetPersonDetailsRequest request)
        {
            return _personService.GetPersonDetails(request);
        }

        [HttpGet("persons")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<GetPersonResponse> GetPersons([FromQuery] GetPersonRequest request)
        {
            return _personService.GetPersons(request);
        }

        [HttpGet("relatedpersons")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<GetRelatedPersonResponse> GetRelatedPersons()
        {
            return _personService.GetRelatedPersons();
        }
    }
}
