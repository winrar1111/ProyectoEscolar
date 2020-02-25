namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EvaluacionesEstudiantes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EvaluacionesEstudiantes",
                c => new
                    {
                        Id_EvaluacionesEstudiantes = c.Int(nullable: false, identity: true),
                        Id_Evaluaciones = c.Int(nullable: false),
                        Id_Estudiante = c.Int(nullable: false),
                        Resultado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_EvaluacionesEstudiantes)
                .ForeignKey("dbo.Estudiantes", t => t.Id_Estudiante, cascadeDelete: false)
                .ForeignKey("dbo.Evaluaciones", t => t.Id_Evaluaciones, cascadeDelete: true)
                .Index(t => t.Id_Evaluaciones)
                .Index(t => t.Id_Estudiante);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EvaluacionesEstudiantes", "Id_Evaluaciones", "dbo.Evaluaciones");
            DropForeignKey("dbo.EvaluacionesEstudiantes", "Id_Estudiante", "dbo.Estudiantes");
            DropIndex("dbo.EvaluacionesEstudiantes", new[] { "Id_Estudiante" });
            DropIndex("dbo.EvaluacionesEstudiantes", new[] { "Id_Evaluaciones" });
            DropTable("dbo.EvaluacionesEstudiantes");
        }
    }
}
