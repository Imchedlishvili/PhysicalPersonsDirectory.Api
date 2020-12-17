using PhysicalPersonsDirectory.Domain;
using PhysicalPersonsDirectory.Domain.DomainClasses;
using System;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private readonly PhysicalPersonsContext _db;
        public LoggerManager(PhysicalPersonsContext db)
        {
            _db = db;
        }

        public LoggerManager()
        {
        }

        public void LogError(string message)
        {
            //try
            //{
            //    _db.LogErrors.Add(new LogError { LogInfo = message });
            //    _db.SaveChanges();
            //}
            //catch (Exception ex)
            //{

            //}
        }
    }
}
