namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionUser_Empleado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Id_Empleado", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Id_Empleado");
            AddForeignKey("dbo.AspNetUsers", "Id_Empleado", "dbo.Empleadoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Id_Empleado", "dbo.Empleadoes");
            DropIndex("dbo.AspNetUsers", new[] { "Id_Empleado" });
            DropColumn("dbo.AspNetUsers", "Id_Empleado");
        }
    }
}
