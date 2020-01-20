using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class Materias
    {
        [Key]
        public int Id { get; set; }
        //
        [Display(Name = "Codigo Materia")]
        [Required(ErrorMessage = "El {0} Es requerido")]
        public string Codigo_Materia { get; set; }
        //
        //[Index(IsUnique = true)]
        [Display(Name = "Nombre de materia")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Nombre_Materia { get; set; }

    }
}