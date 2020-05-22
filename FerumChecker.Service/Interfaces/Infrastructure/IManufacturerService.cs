using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IManufacturerService
    {
        Manufacturer GetManufacturer(int? id);
        IEnumerable<Manufacturer> GetManufacturers();

        OperationDetails CreateManufacturer(Manufacturer manufacturer);

        OperationDetails UpdateManufacturer(Manufacturer manufacturer);

        OperationDetails DeleteManufacturer(int? id);
        void Dispose();
    }
}
