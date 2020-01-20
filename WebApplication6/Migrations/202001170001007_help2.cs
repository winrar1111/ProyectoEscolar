namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class help2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CursoEstudiantes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Curso = c.Int(nullable: false),
                        Id_Estudiante = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CursoEscolars", t => t.Id_Curso, cascadeDelete: true)
                .ForeignKey("dbo.Estudiantes", t => t.Id_Estudiante, cascadeDelete: false)
                .Index(t => t.Id_Curso)
                .Index(t => t.Id_Estudiante);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CursoEstudiantes", "Id_Estudiante", "dbo.Estudiantes");
            DropForeignKey("dbo.CursoEstudiantes", "Id_Curso", "dbo.CursoEscolars");
            DropIndex("dbo.CursoEstudiantes", new[] { "Id_Estudiante" });
            DropIndex("dbo.CursoEstudiantes", new[] { "Id_Curso" });
            DropTable("dbo.CursoEstudiantes");
        }
    }
}
