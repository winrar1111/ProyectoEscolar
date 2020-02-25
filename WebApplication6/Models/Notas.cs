using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace WebApplication6.Models
{
    public class Notas
    {
        [Key]
        public int Id_Nota { get; set; }

        [ForeignKey("Id_Estudiante")]
       
        public Estudiantes Estudiantes{ get; set; }

        public int Id_Estudiante { get; set; }
        
        [ForeignKey("Id_Curso")]
        public Curso_Asignaturas Curso_Asignaturas{ get; set; }

        public int Id_Curso { get; set; }


        [Display(Name ="Primer Parcial")]
        public float Nota1  { get; set; }
        [Display(Name = "Segundo Parcial")]
        public float Nota2  { get; set; }
        [Display(Name = "Tercer Parcial")]
        public float Nota3  { get; set; }
        [Display(Name = "Cuarto Parcial")]
        public float Nota4  { get; set; }
        [Display(Name = "Promedio")]
        public float promedio { get; set; }

        [Display(Name ="Aprobado")]
        public bool Bl_Aprobado { get; set; }

    }
}