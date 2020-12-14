using PhysicalPersonsDirectory.Services.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class PersonImageResponseModel : ResponseBaseModel
    {
        public string ImagePatch { get; set; }
    }
}
