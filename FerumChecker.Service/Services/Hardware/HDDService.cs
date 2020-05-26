using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class HDDService : IHDDService
    {

        IUnitOfWork Database { get; set; }

        public HDDService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public HDD GetHDD(int? id)
        {
            return Database.HDDs.Get(id.Value);
        }

        public IEnumerable<HDD> GetHDDs()
        {
            return Database.HDDs.GetAll();
        }

        public OperationDetails UpdateHDD(HDD hdd)
        {
            
            Database.HDDs.Update(hdd);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateHDD(HDD hdd)
        {
            Database.HDDs.Create(hdd);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteHDD(int? id)
        {
            Database.HDDs.Delete(id.Value);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
