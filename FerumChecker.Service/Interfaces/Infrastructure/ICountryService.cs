using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface ICountryService
    {
        Country GetCountry(int? id);
        IEnumerable<Country> GetCountrys();

        OperationDetails CreateCountry(Country country);

        OperationDetails UpdateCountry(Country country);

        OperationDetails DeleteCountry(int? id);
        void Dispose();
    }
}
