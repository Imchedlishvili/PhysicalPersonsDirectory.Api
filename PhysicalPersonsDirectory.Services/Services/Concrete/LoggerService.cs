using PhysicalPersonsDirectory.Domain;
using PhysicalPersonsDirectory.Services.Services.Abstract;
using System;
using logError = PhysicalPersonsDirectory.Domain.DomainClasses.LogError;

namespace PhysicalPersonsDirectory.Services.Services.Concrete
{
    public class LoggerService : ILoggerService
    {
        private readonly PhysicalPersonsContext _db;
        public LoggerService(PhysicalPersonsContext db)
        {
            _db = db;
        }

        public void LogError(string message)
        {
            try
            {
                _db.LogErrors.Add(new logError { LogInfo = message });
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
