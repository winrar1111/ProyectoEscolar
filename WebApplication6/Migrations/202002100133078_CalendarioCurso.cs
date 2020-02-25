namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalendarioCurso : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarioCursoes",
                c => new
                    {
                        Id_CalendarioCurso = c.Int(nullable: false, identity: true),
                        Id_Curso = c.Int(nullable: false),
                        Id_Periodo = c.Int(nullable: false),
                        Id_CursoAsignatura = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_CalendarioCurso)
                .ForeignKey("dbo.Curso_Asignaturas", t => t.Id_CursoAsignatura, cascadeDelete: true)
                .ForeignKey("dbo.CursoEscolars", t => t.Id_Curso, cascadeDelete: false)
                .ForeignKey("dbo.PeriodosEscolares", t => t.Id_Periodo, cascadeDelete: true)
                .Index(t => t.Id_Curso)
                .Index(t => t.Id_Periodo)
                .Index(t => t.Id_CursoAsignatura);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CalendarioCursoes", "Id_Periodo", "dbo.PeriodosEscolares");
            DropForeignKey("dbo.CalendarioCursoes", "Id_Curso", "dbo.CursoEscolars");
            DropForeignKey("dbo.CalendarioCursoes", "Id_CursoAsignatura", "dbo.Curso_Asignaturas");
            DropIndex("dbo.CalendarioCursoes", new[] { "Id_CursoAsignatura" });
            DropIndex("dbo.CalendarioCursoes", new[] { "Id_Periodo" });
            DropIndex("dbo.CalendarioCursoes", new[] { "Id_Curso" });
            DropTable("dbo.CalendarioCursoes");
        }
    }
}
