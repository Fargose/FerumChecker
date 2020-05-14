using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IPCCaseService
    {
        PCCase GetPCCase(int? id);
        IEnumerable<PCCase> GetPCCases();

        OperationDetails CreatePCCase(PCCase pcCase);

        OperationDetails UpdatePCCase(PCCase pcCase);

        OperationDetails DeletePCCase(int? id);
        void Dispose();
    }
}
