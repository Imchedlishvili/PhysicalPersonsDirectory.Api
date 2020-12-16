using PhysicalPersonsDirectory.Services.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Get
{
    public class GetPersonRequest : PagingBaseRequest
    {
        public int PersonId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

        public string PersonalNumber { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
        public string PhoneNumber { get; set; }

        public int? GenderId { get; set; }
        public int? CityId { get; set; }
    }
}
