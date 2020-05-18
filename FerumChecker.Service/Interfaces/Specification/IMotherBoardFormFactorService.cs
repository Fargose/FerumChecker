using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IMotherBoardFormFactorService
    {
        MotherBoardFormFactor GetMotherBoardFormFactor(int? id);
        IEnumerable<MotherBoardFormFactor> GetMotherBoardFormFactors();

        OperationDetails CreateMotherBoardFormFactor(MotherBoardFormFactor motherBoardFormFactor);

        OperationDetails UpdateMotherBoardFormFactor(MotherBoardFormFactor motherBoardFormFactor);

        OperationDetails DeleteMotherBoardFormFactor(int? id);
        void Dispose();
    }
}
