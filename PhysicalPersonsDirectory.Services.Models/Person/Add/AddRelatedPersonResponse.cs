﻿using PhysicalPersonsDirectory.Services.Models.Base;

namespace PhysicalPersonsDirectory.Services.Models.Person.Add
{
    public class AddRelatedPersonResponse : ResponseBaseModel
    {
        public int RelatedPersonId { get; set; }
    }
}
