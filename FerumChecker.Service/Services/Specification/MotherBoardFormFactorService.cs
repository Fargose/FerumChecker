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
    public class MotherBoardFormFactorService : IMotherBoardFormFactorService
    {

        IUnitOfWork Database { get; set; }

        public MotherBoardFormFactorService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public MotherBoardFormFactor GetMotherBoardFormFactor(int? id)
        {
            return Database.MotherBoardFormFactors.Get(id.Value);
        }

        public IEnumerable<MotherBoardFormFactor> GetMotherBoardFormFactors()
        {
            return Database.MotherBoardFormFactors.GetAll();
        }

        public OperationDetails UpdateMotherBoardFormFactor(MotherBoardFormFactor motherBoardFormFactor)
        {
            
            Database.MotherBoardFormFactors.Update(motherBoardFormFactor);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateMotherBoardFormFactor(MotherBoardFormFactor motherBoardFormFactor)
        {
            Database.MotherBoardFormFactors.Create(motherBoardFormFactor);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteMotherBoardFormFactor(int? id)
        {
            Database.MotherBoardFormFactors.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
