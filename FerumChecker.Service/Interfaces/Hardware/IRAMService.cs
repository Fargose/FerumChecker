using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IRAMService
    {
        RAM GetRAM(int? id);
        IEnumerable<RAM> GetRAMs();

        OperationDetails CreateRAM(RAM ram);

        OperationDetails UpdateRAM(RAM ram);

        OperationDetails DeleteRAM(int? id);
        void Dispose();
    }
}
