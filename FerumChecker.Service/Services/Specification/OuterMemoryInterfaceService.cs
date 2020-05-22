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
    public class OuterMemoryInterfaceService : IOuterMemoryInterfaceService
    {

        IUnitOfWork Database { get; set; }

        public OuterMemoryInterfaceService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public OuterMemoryInterface GetOuterMemoryInterface(int? id)
        {
            return Database.OuterMemoryInterfaces.Get(id.Value);
        }

        public IEnumerable<OuterMemoryInterface> GetOuterMemoryInterfaces()
        {
            return Database.OuterMemoryInterfaces.GetAll();
        }

        public OperationDetails UpdateOuterMemoryInterface(OuterMemoryInterface outerMemoryInterface)
        {
            
            Database.OuterMemoryInterfaces.Update(outerMemoryInterface);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateOuterMemoryInterface(OuterMemoryInterface outerMemoryInterface)
        {
            Database.OuterMemoryInterfaces.Create(outerMemoryInterface);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteOuterMemoryInterface(int? id)
        {
            Database.OuterMemoryInterfaces.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
