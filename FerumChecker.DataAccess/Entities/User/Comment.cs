using FerumChecker.DataAccess.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FerumChecker.DataAccess.Entities.User
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }


        [MaxLength(1000, ErrorMessage = "Некоректне поле")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("UserProfile")]
        public string OwnerId { get; set; }

        public UserProfile Owner { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [ForeignKey("ComputerAssembly")]
        public int ComputerAssemblyId { get; set; }

        public ComputerAssembly ComputerAssemblies { get; set; }
    }
}
