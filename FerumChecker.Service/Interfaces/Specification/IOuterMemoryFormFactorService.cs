using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IOuterMemoryFormFactorService
    {
        OuterMemoryFormFactor GetOuterMemoryFormFactor(int? id);
        IEnumerable<OuterMemoryFormFactor> GetOuterMemoryFormFactors();

        OperationDetails CreateOuterMemoryFormFactor(OuterMemoryFormFactor outerMemoryFormFactor);

        OperationDetails UpdateOuterMemoryFormFactor(OuterMemoryFormFactor outerMemoryFormFactor);

        OperationDetails DeleteOuterMemoryFormFactor(int? id);
        void Dispose();
    }
}
