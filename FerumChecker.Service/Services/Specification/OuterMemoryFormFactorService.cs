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
    public class OuterMemoryFormFactorService : IOuterMemoryFormFactorService
    {

        IUnitOfWork Database { get; set; }

        public OuterMemoryFormFactorService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public OuterMemoryFormFactor GetOuterMemoryFormFactor(int? id)
        {
            return Database.OuterMemoryFormFactors.Get(id.Value);
        }

        public IEnumerable<OuterMemoryFormFactor> GetOuterMemoryFormFactors()
        {
            return Database.OuterMemoryFormFactors.GetAll();
        }

        public OperationDetails UpdateOuterMemoryFormFactor(OuterMemoryFormFactor outerMemoryFormFactor)
        {
            
            Database.OuterMemoryFormFactors.Update(outerMemoryFormFactor);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateOuterMemoryFormFactor(OuterMemoryFormFactor outerMemoryFormFactor)
        {
            Database.OuterMemoryFormFactors.Create(outerMemoryFormFactor);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteOuterMemoryFormFactor(int? id)
        {
            Database.OuterMemoryFormFactors.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
