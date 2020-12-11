using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Domain.DomainClasses
{
    public class RelatedPerson
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
        public int RelationTypeId { get; set; }

        public Person Person { get; set; }
        public Person RelatedPersonNavigation { get; set; }
        public RelationType RelationType { get; set; }
    }
}
