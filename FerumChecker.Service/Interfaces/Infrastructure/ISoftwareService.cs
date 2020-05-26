using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface ISoftwareService
    {
        Software GetSoftware(int? id);
        IEnumerable<Software> GetSoftwares();

        OperationDetails CreateSoftware(Software software);

        OperationDetails UpdateSoftware(Software software);

        OperationDetails DeleteSoftware(int? id);
        void Dispose();
    }
}
