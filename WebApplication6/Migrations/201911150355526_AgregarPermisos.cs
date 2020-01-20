namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarPermisos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permisos",
                c => new
                    {
                        Id_Permiso = c.Int(nullable: false, identity: true),
                        NombrePermiso = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Permiso);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Permisos");
        }
    }
}
