using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IGPUService
    {
        GPU GetGPU(int? id);
        IEnumerable<GPU> GetGPUs();

        OperationDetails CreateGPU(GPU gpu);

        OperationDetails UpdateGPU(GPU gpu);

        OperationDetails DeleteGPU(int? id);
        void Dispose();
    }
}
