namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimerMigracion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnfermedadEstudiantes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Medicacion = c.String(),
                        Id_Estudiante = c.Int(nullable: false),
                        Id_Enfermedad = c.Int(nullable: false),
                        Id_Medicacion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enfermedads", t => t.Id_Enfermedad, cascadeDelete: true)
                .ForeignKey("dbo.Estudiantes", t => t.Id_Estudiante, cascadeDelete: true)
                .ForeignKey("dbo.TPMedicacions", t => t.Id_Medicacion, cascadeDelete: true)
                .Index(t => t.Id_Estudiante)
                .Index(t => t.Id_Enfermedad)
                .Index(t => t.Id_Medicacion);
            
            CreateTable(
                "dbo.Enfermedads",
                c => new
                    {
                        Id_Enfermedad = c.Int(nullable: false, identity: true),
                        Str_NombreEnfermedad = c.String(nullable: false),
                        Str_Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id_Enfermedad);
            
            CreateTable(
                "dbo.Estudiantes",
                c => new
                    {
                        Id_Estudiante = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Apellido = c.String(),
                        Direccion = c.String(),
                        Correo = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Domicilio = c.String(),
                        Telefono = c.String(),
                        Genero = c.String(),
                        Estado_Estudiante = c.Boolean(nullable: false),
                        Codigo_Estudiante = c.String(nullable: false),
                        Codigo_MINED = c.String(nullable: false),
                        Str_Nombre_Padre = c.String(),
                        Str_Nombre_Madre = c.String(),
                        Image = c.Binary(storeType: "image"),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Estudiante)
                .ForeignKey("dbo.AniosACursars", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AniosACursars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Str_Curso = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ControlDisciplinarios",
                c => new
                    {
                        Id_Control = c.Int(nullable: false, identity: true),
                        Fecha_Emision = c.DateTime(nullable: false),
                        Descripcion = c.String(),
                        Id_Estudiantes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Control)
                .ForeignKey("dbo.Estudiantes", t => t.Id_Estudiantes, cascadeDelete: true)
                .Index(t => t.Id_Estudiantes);
            
            CreateTable(
                "dbo.FamiliarEstudiantes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Apellido = c.String(),
                        Direccion = c.String(),
                        Correo = c.String(),
                        FechaNaci = c.DateTime(nullable: false),
                        Domicilio = c.String(),
                        Telefono = c.String(),
                        Genero = c.String(),
                        Identificacion = c.String(),
                        EstadoCivil = c.String(),
                        BL_Estado_Familiar = c.Boolean(nullable: false),
                        Id_Estudiante = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estudiantes", t => t.Id_Estudiante, cascadeDelete: true)
                .Index(t => t.Id_Estudiante);
            
            CreateTable(
                "dbo.HistorialAcademicoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cod_Escuela = c.String(nullable: false),
                        Id_Estudiante = c.Int(nullable: false),
                        Str_NombreColegio = c.String(nullable: false),
                        Num_AnioCursado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estudiantes", t => t.Id_Estudiante, cascadeDelete: true)
                .Index(t => t.Id_Estudiante);
            
            CreateTable(
                "dbo.HistorialAñosCursado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Escuela = c.Int(nullable: false),
                        Id_Anio = c.Int(nullable: false),
                        FechFinaly = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AniosACursars", t => t.Id_Anio, cascadeDelete: false)
                .ForeignKey("dbo.HistorialAcademicoes", t => t.Id_Escuela, cascadeDelete: true)
                .Index(t => t.Id_Escuela)
                .Index(t => t.Id_Anio);
            
            CreateTable(
                "dbo.TPMedicacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Str_Tipo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Empleadoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Apellido = c.String(),
                        Direccion = c.String(),
                        Correo = c.String(),
                        FechaNaci = c.DateTime(nullable: false),
                        Domicilio = c.String(),
                        Telefono = c.String(),
                        Genero = c.String(),
                        Identificacion = c.String(),
                        Codigo_Empleado = c.String(nullable: false, maxLength: 10),
                        EstadoCivil = c.String(),
                        Dt_Contratacion = c.DateTime(nullable: false),
                        BL_Estado_Empleado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Estudios_Maestro",
                c => new
                    {
                        Id_Estudio = c.Int(nullable: false, identity: true),
                        Str_Tipo_Estudio = c.String(nullable: false),
                        Str_Nombre_Estudio = c.String(nullable: false),
                        Str_Centro_Estudio = c.String(nullable: false),
                        Bl_Estado_Estudio = c.Boolean(nullable: false),
                        Fch_Finalizacion = c.DateTime(nullable: false),
                        Id_Empleado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Estudio)
                .ForeignKey("dbo.Empleadoes", t => t.Id_Empleado, cascadeDelete: true)
                .Index(t => t.Id_Empleado);
            
            CreateTable(
                "dbo.Materias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo_Materia = c.String(nullable: false),
                        Nombre_Materia = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Estudios_Maestro", "Id_Empleado", "dbo.Empleadoes");
            DropForeignKey("dbo.EnfermedadEstudiantes", "Id_Medicacion", "dbo.TPMedicacions");
            DropForeignKey("dbo.HistorialAñosCursado", "Id_Escuela", "dbo.HistorialAcademicoes");
            DropForeignKey("dbo.HistorialAñosCursado", "Id_Anio", "dbo.AniosACursars");
            DropForeignKey("dbo.HistorialAcademicoes", "Id_Estudiante", "dbo.Estudiantes");
            DropForeignKey("dbo.FamiliarEstudiantes", "Id_Estudiante", "dbo.Estudiantes");
            DropForeignKey("dbo.EnfermedadEstudiantes", "Id_Estudiante", "dbo.Estudiantes");
            DropForeignKey("dbo.ControlDisciplinarios", "Id_Estudiantes", "dbo.Estudiantes");
            DropForeignKey("dbo.Estudiantes", "Id", "dbo.AniosACursars");
            DropForeignKey("dbo.EnfermedadEstudiantes", "Id_Enfermedad", "dbo.Enfermedads");
            DropIndex("dbo.Estudios_Maestro", new[] { "Id_Empleado" });
            DropIndex("dbo.HistorialAñosCursado", new[] { "Id_Anio" });
            DropIndex("dbo.HistorialAñosCursado", new[] { "Id_Escuela" });
            DropIndex("dbo.HistorialAcademicoes", new[] { "Id_Estudiante" });
            DropIndex("dbo.FamiliarEstudiantes", new[] { "Id_Estudiante" });
            DropIndex("dbo.ControlDisciplinarios", new[] { "Id_Estudiantes" });
            DropIndex("dbo.Estudiantes", new[] { "Id" });
            DropIndex("dbo.EnfermedadEstudiantes", new[] { "Id_Medicacion" });
            DropIndex("dbo.EnfermedadEstudiantes", new[] { "Id_Enfermedad" });
            DropIndex("dbo.EnfermedadEstudiantes", new[] { "Id_Estudiante" });
            DropTable("dbo.Materias");
            DropTable("dbo.Estudios_Maestro");
            DropTable("dbo.Empleadoes");
            DropTable("dbo.TPMedicacions");
            DropTable("dbo.HistorialAñosCursado");
            DropTable("dbo.HistorialAcademicoes");
            DropTable("dbo.FamiliarEstudiantes");
            DropTable("dbo.ControlDisciplinarios");
            DropTable("dbo.AniosACursars");
            DropTable("dbo.Estudiantes");
            DropTable("dbo.Enfermedads");
            DropTable("dbo.EnfermedadEstudiantes");
        }
    }
}
