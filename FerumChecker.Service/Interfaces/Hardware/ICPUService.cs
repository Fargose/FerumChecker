using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface ICPUService
    {
        CPU GetCPU(int? id);
        IEnumerable<CPU> GetCPUs();

        OperationDetails CreateCPU(CPU cpu);

        OperationDetails UpdateCPU(CPU cpu);

        OperationDetails DeleteCPU(int? id);
        void Dispose();
    }
}
