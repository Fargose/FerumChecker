using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class DeveloperService : IDeveloperService
    {

        IUnitOfWork Database { get; set; }

        public DeveloperService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Developer GetDeveloper(int? id)
        {
            return Database.Developers.Get(id.Value);
        }

        public IEnumerable<Developer> GetDevelopers()
        {
            return Database.Developers.GetAll();
        }

        public OperationDetails UpdateDeveloper(Developer developer)
        {
            
            Database.Developers.Update(developer);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateDeveloper(Developer developer)
        {
            Database.Developers.Create(developer);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteDeveloper(int? id)
        {
            Database.Developers.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
