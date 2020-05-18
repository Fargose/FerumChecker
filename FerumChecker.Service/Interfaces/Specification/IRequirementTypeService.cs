using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IRequirementTypeService
    {
        RequirementType GetRequirementType(int? id);
        IEnumerable<RequirementType> GetRequirementTypes();

        OperationDetails CreateRequirementType(RequirementType requirementType);

        OperationDetails UpdateRequirementType(RequirementType requirementType);

        OperationDetails DeleteRequirementType(int? id);
        void Dispose();
    }
}
