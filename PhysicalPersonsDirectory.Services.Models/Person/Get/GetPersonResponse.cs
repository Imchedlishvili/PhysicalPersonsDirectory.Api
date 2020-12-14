using PhysicalPersonsDirectory.Services.Models.Paging;
using PhysicalPersonsDirectory.Services.Models.Person.Shared;
using System.Collections.Generic;

namespace PhysicalPersonsDirectory.Services.Models.Person.Get
{
    public class GetPersonResponse : PagingBaseResponseModel
    {
        public List<PersonBaseModel> Persons { get; set; }
    }
}
