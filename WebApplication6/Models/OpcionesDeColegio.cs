using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class OpcionesDeColegio
    {
        [Key]
        public int Id { get; set; } 

        [Display(Name ="Maximo de Estudiante por aula")]
        public int MaximoEstudiantes { get; set; }

        [Display(Name ="Nombre del Colegio")]
        public string NombreColegio { get; set; }

    }
}