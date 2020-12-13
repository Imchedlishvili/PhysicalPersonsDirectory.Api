using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Models.Person.Shared
{
    public class RelatedPersonModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
        public int RelationTypeId { get; set; }

    }
}
