using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IDeveloperService
    {
        Developer GetDeveloper(int? id);
        IEnumerable<Developer> GetDevelopers();

        OperationDetails CreateDeveloper(Developer developer);

        OperationDetails UpdateDeveloper(Developer developer);

        OperationDetails DeleteDeveloper(int? id);
        void Dispose();
    }
}
