using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class PersonImageRequestModel
    {
        public int PersonId { get; set; }
        public byte[] Image { get; set; }
        public IFormFile ImageTest { get; set; }
    }
}
