using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.DataAccess.Map
{
    public class ComputerAssemblyMap
    {
        public ComputerAssemblyMap(EntityTypeBuilder<ComputerAssembly> entityBuilder)
        {
           // entityBuilder.HasKey(p => p.ComputerAssemblyId);
        }
    }
}
