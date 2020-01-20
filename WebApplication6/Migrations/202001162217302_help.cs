namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class help : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estudiantes", "Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Estudiantes", "Id", "dbo.AniosACursars", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}
