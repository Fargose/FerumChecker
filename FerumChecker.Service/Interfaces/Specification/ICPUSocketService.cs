using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface ICPUSocketService
    {
        CPUSocket GetCPUSocket(int? id);
        IEnumerable<CPUSocket> GetCPUSockets();

        OperationDetails CreateCPUSocket(CPUSocket cpuSocket);

        OperationDetails UpdateCPUSocket(CPUSocket cpuSocket);

        OperationDetails DeleteCPUSocket(int? id);
        void Dispose();
    }
}
