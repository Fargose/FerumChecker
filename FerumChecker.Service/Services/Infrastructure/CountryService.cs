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
    public class CountryService : ICountryService
    {

        IUnitOfWork Database { get; set; }

        public CountryService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Country GetCountry(int? id)
        {
            return Database.Countries.Get(id.Value);
        }

        public IEnumerable<Country> GetCountrys()
        {
            return Database.Countries.GetAll();
        }

        public OperationDetails UpdateCountry(Country country)
        {
            
            Database.Countries.Update(country);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateCountry(Country country)
        {
            Database.Countries.Create(country);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteCountry(int? id)
        {
            Database.Countries.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
