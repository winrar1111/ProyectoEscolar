namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Evaluaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evaluaciones",
                c => new
                    {
                        Id_Evaluacion = c.Int(nullable: false, identity: true),
                        Id_Materia = c.Int(nullable: false),
                        Id_CursoA = c.Int(nullable: false),
                        Fecha_Comienzo = c.DateTime(nullable: false),
                        Fecha_Final = c.DateTime(nullable: false),
                        Descripcion = c.String(),
                        ValorTotal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Evaluacion)
                .ForeignKey("dbo.Curso_Asignaturas", t => t.Id_CursoA, cascadeDelete: true)
                .ForeignKey("dbo.Materias", t => t.Id_Materia, cascadeDelete: false)
                .Index(t => t.Id_Materia)
                .Index(t => t.Id_CursoA);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evaluaciones", "Id_Materia", "dbo.Materias");
            DropForeignKey("dbo.Evaluaciones", "Id_CursoA", "dbo.Curso_Asignaturas");
            DropIndex("dbo.Evaluaciones", new[] { "Id_CursoA" });
            DropIndex("dbo.Evaluaciones", new[] { "Id_Materia" });
            DropTable("dbo.Evaluaciones");
        }
    }
}
