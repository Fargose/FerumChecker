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
    public class MotherBoardNorthBridgeService : IMotherBoardNorthBridgeService
    {

        IUnitOfWork Database { get; set; }

        public MotherBoardNorthBridgeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public MotherBoardNorthBridge GetMotherBoardNorthBridge(int? id)
        {
            return Database.MotherBoardNorthBridges.Get(id.Value);
        }

        public IEnumerable<MotherBoardNorthBridge> GetMotherBoardNorthBridges()
        {
            return Database.MotherBoardNorthBridges.GetAll();
        }

        public OperationDetails UpdateMotherBoardNorthBridge(MotherBoardNorthBridge motherBoardNorthBridge)
        {
            
            Database.MotherBoardNorthBridges.Update(motherBoardNorthBridge);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateMotherBoardNorthBridge(MotherBoardNorthBridge motherBoardNorthBridge)
        {
            Database.MotherBoardNorthBridges.Create(motherBoardNorthBridge);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteMotherBoardNorthBridge(int? id)
        {
            Database.MotherBoardNorthBridges.Delete(id.Value);
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
