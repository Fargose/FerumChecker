using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.DTO.Hardware
{
    public class RecomendationDTO
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Type { get; set; }

        public string Display { get; set; }
    }
}
