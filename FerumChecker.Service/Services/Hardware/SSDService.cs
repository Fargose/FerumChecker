using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class SSDService : ISSDService
    {

        IUnitOfWork Database { get; set; }

        public SSDService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public SSD GetSSD(int? id)
        {
            return Database.SSDs.Get(id.Value);
        }

        public IEnumerable<SSD> GetSSDs()
        {
            return Database.SSDs.GetAll();
        }

        public OperationDetails UpdateSSD(SSD ssd)
        {
            
            Database.SSDs.Update(ssd);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateSSD(SSD ssd)
        {
            Database.SSDs.Create(ssd);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteSSD(int? id)
        {
            Database.SSDs.Delete(id.Value);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
