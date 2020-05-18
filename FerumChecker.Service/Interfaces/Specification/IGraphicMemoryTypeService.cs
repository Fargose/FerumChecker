using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Hardware
{
    public interface IGraphicMemoryTypeService
    {
        GraphicMemoryType GetGraphicMemoryType(int? id);
        IEnumerable<GraphicMemoryType> GetGraphicMemoryTypes();

        OperationDetails CreateGraphicMemoryType(GraphicMemoryType graphicMemoryType);

        OperationDetails UpdateGraphicMemoryType(GraphicMemoryType graphicMemoryType);

        OperationDetails DeleteGraphicMemoryType(int? id);
        void Dispose();
    }
}
