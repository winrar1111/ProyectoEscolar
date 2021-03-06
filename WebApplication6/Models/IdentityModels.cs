﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication6.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Id_Empleado")]
        public Empleado Empleado { get; set; }
        [Display(Name ="Codigo Empleado")]
        [Required]
        public int Id_Empleado { get; set; }

        [Display(Name ="Ultima fecha de conexcion")]
        public DateTime Fecha_Conexcion{ get; set; }
        
        public List<ControlDisciplinario> controlDisciplinaris{ get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Empleado> TbEmpleado { get; set; }

        public DbSet<Materias> tbmaterias { get; set; }

        public DbSet<Estudios_Maestro> tbEstudios_Maestro { get; set; }

        public DbSet<AniosACursar> tbAniosACursar { get; set; }

        public DbSet<Estudiantes> tbestudiantes { get; set; }
        public DbSet<FamiliarEstudiante> FamiliarEstudiantes { get; set; }

        public DbSet<Enfermedad> tbEnfermedad { get; set; }

        public DbSet<TPMedicacion> tbTPmedicacion { get; set; }

        public DbSet<HistorialAcademico> tbHistorialAcademico { get; set; }
        public DbSet<EnfermedadEstudiantes> EnfermedadEstudiantes { get; set; }
        public DbSet<ControlDisciplinario> tbcontrolDisciplinario { get; set; }
        public DbSet<HistorialAñosCursado> tbHistorialAniosCursado { get; set; }

        public DbSet<Permisos> tbPermisos { get; set; }

        public DbSet<PermisosYRoles> tbpermisosyroles { get; set; }

        public DbSet<CursoEscolar> tbCursoEscolar{ get; set; }

        public DbSet<Curso_Asignaturas>  tbCursoAsignaturas { get; set; }

        public DbSet<EmpleadoMaterias> tbEmpleadoMaterias { get; set; }

        public DbSet<CursoEstudiantes> CursoEstudiantes{ get; set; }

        public  DbSet<OpcionesDeColegio>  OpcionesDeColegios  { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebApplication6.Models.Secciones> Secciones { get; set; }
    }
}