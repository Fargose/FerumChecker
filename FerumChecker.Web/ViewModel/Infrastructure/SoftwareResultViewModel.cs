using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerumChecker.Web.ViewModel.Infrastructure
{
    public class SoftwareResultViewModel
    {
        public OperationDetails Result {get ;set;}

        public ComputerAssembly ComputerAssembly { get; set; }

        public SoftwareViewModel Software { get; set; }
    }
}
