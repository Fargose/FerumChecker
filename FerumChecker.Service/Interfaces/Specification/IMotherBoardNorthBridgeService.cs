using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IMotherBoardNorthBridgeService
    {
        MotherBoardNorthBridge GetMotherBoardNorthBridge(int? id);
        IEnumerable<MotherBoardNorthBridge> GetMotherBoardNorthBridges();

        OperationDetails CreateMotherBoardNorthBridge(MotherBoardNorthBridge motherBoardNorthBridge);

        OperationDetails UpdateMotherBoardNorthBridge(MotherBoardNorthBridge motherBoardNorthBridge);

        OperationDetails DeleteMotherBoardNorthBridge(int? id);
        void Dispose();
    }
}
