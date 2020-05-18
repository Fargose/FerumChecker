using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class RAMTypeService : IRAMTypeService
    {

        IUnitOfWork Database { get; set; }

        public RAMTypeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public RAMType GetRAMType(int? id)
        {
            return Database.RAMTypes.Get(id.Value);
        }

        public IEnumerable<RAMType> GetRAMTypes()
        {
            return Database.RAMTypes.GetAll();
        }

        public OperationDetails UpdateRAMType(RAMType ramType)
        {
            
            Database.RAMTypes.Update(ramType);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateRAMType(RAMType ramType)
        {
            Database.RAMTypes.Create(ramType);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteRAMType(int? id)
        {
            Database.RAMTypes.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
