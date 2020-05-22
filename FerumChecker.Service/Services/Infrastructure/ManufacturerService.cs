using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class ManufacturerService : IManufacturerService
    {

        IUnitOfWork Database { get; set; }

        public ManufacturerService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Manufacturer GetManufacturer(int? id)
        {
            return Database.Manufacturers.Get(id.Value);
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return Database.Manufacturers.GetAll();
        }

        public OperationDetails UpdateManufacturer(Manufacturer manufacturer)
        {
            
            Database.Manufacturers.Update(manufacturer);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateManufacturer(Manufacturer manufacturer)
        {
            Database.Manufacturers.Create(manufacturer);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteManufacturer(int? id)
        {
            Database.Manufacturers.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
