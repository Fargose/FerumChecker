using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class RequirementTypeService : IRequirementTypeService
    {

        IUnitOfWork Database { get; set; }

        public RequirementTypeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public RequirementType GetRequirementType(int? id)
        {
            return Database.RequirementTypes.Get(id.Value);
        }

        public IEnumerable<RequirementType> GetRequirementTypes()
        {
            return Database.RequirementTypes.GetAll();
        }

        public OperationDetails UpdateRequirementType(RequirementType requirementType)
        {
            
            Database.RequirementTypes.Update(requirementType);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateRequirementType(RequirementType requirementType)
        {
            Database.RequirementTypes.Create(requirementType);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteRequirementType(int? id)
        {
            Database.RequirementTypes.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
