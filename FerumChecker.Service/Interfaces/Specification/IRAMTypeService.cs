using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IRAMTypeService
    {
        RAMType GetRAMType(int? id);
        IEnumerable<RAMType> GetRAMTypes();

        OperationDetails CreateRAMType(RAMType ramType);

        OperationDetails UpdateRAMType(RAMType ramType);

        OperationDetails DeleteRAMType(int? id);
        void Dispose();
    }
}
