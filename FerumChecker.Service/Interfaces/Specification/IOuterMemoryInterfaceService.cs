using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IOuterMemoryInterfaceService
    {
        OuterMemoryInterface GetOuterMemoryInterface(int? id);
        IEnumerable<OuterMemoryInterface> GetOuterMemoryInterfaces();

        OperationDetails CreateOuterMemoryInterface(OuterMemoryInterface outerMemoryInterface);

        OperationDetails UpdateOuterMemoryInterface(OuterMemoryInterface outerMemoryInterface);

        OperationDetails DeleteOuterMemoryInterface(int? id);
        void Dispose();
    }
}
