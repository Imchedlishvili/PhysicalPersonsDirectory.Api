using PhysicalPersonsDirectory.Domain;
using PhysicalPersonsDirectory.Services.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicalPersonsDirectory.Services.Services.Concrete
{
    public class PersonService : IPersonService
    {
        private readonly PhysicalPersonsContext _db;
        public PersonService(PhysicalPersonsContext db)
        {
            _db = db;
        }
    }
}
