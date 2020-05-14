using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IHDDService
    {
        HDD GetHDD(int? id);
        IEnumerable<HDD> GetHDDs();

        OperationDetails CreateHDD(HDD hdd);

        OperationDetails UpdateHDD(HDD hdd);

        OperationDetails DeleteHDD(int? id);
        void Dispose();
    }
}
