using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface ISSDService
    {
        SSD GetSSD(int? id);
        IEnumerable<SSD> GetSSDs();

        OperationDetails CreateSSD(SSD ssd);

        OperationDetails UpdateSSD(SSD ssd);

        OperationDetails DeleteSSD(int? id);
        void Dispose();
    }
}
