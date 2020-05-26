using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class SoftwareService : ISoftwareService
    {

        IUnitOfWork Database { get; set; }

        public SoftwareService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Software GetSoftware(int? id)
        {
            return Database.Softwares.Get(id.Value);
        }

        public IEnumerable<Software> GetSoftwares()
        {
            return Database.Softwares.GetAll();
        }

        public OperationDetails UpdateSoftware(Software software)
        {
            
            Database.Softwares.Update(software);
            SetCPURequirment(software, software.SoftwareCPURequirements.ToList());
            SetVideoCardRequirment(software, software.SoftwareVideoCardRequirements.ToList());
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateSoftware(Software software)
        {
            Database.Softwares.Create(software);
            SetCPURequirment(software, software.SoftwareCPURequirements.ToList());
            SetVideoCardRequirment(software, software.SoftwareVideoCardRequirements.ToList());
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteSoftware(int? id)
        {
            Database.Softwares.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        private OperationDetails SetCPURequirment(Software software, List<SoftwareCPURequirement> cpuRequirments)
        {
            if (software.Id > -1 && cpuRequirments != null)
            {
                var oldInterfaces = Database.SoftwareCPURequirements.GetAll().Where(m => m.SoftwareId == software.Id);
                foreach (var item in oldInterfaces)
                {
                    Database.SoftwareCPURequirements.Delete(item.Id.Value);
                }
                foreach (var item in cpuRequirments)
                {
                    item.SoftwareId = software.Id;
                    this.Database.SoftwareCPURequirements.Create(item);
                }
            }

            return new OperationDetails(true, "Ok", "");
        }

        private OperationDetails SetVideoCardRequirment(Software software, List<SoftwareVideoCardRequirement> videoCardsRequirment)
        {
            if (software.Id > -1 && videoCardsRequirment != null)
            {
                var oldInterfaces = Database.SoftwareVideoCardRequirements.GetAll().Where(m => m.SoftwareId == software.Id);
                foreach (var item in oldInterfaces)
                {
                    Database.SoftwareVideoCardRequirements.Delete(item.Id.Value);
                }
                foreach (var item in videoCardsRequirment)
                {
                    item.SoftwareId = software.Id;
                    this.Database.SoftwareVideoCardRequirements.Create(item);
                }
            }

            return new OperationDetails(true, "Ok", "");
        }


        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
