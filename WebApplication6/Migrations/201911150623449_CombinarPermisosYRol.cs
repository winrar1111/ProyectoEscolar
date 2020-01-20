namespace WebApplication6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CombinarPermisosYRol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermisosYRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Rol = c.String(maxLength: 128),
                        Id_Permiso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.Id_Rol)
                .ForeignKey("dbo.Permisos", t => t.Id_Permiso, cascadeDelete: true)
                .Index(t => t.Id_Rol)
                .Index(t => t.Id_Permiso);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermisosYRoles", "Id_Permiso", "dbo.Permisos");
            DropForeignKey("dbo.PermisosYRoles", "Id_Rol", "dbo.AspNetRoles");
            DropIndex("dbo.PermisosYRoles", new[] { "Id_Permiso" });
            DropIndex("dbo.PermisosYRoles", new[] { "Id_Rol" });
            DropTable("dbo.PermisosYRoles");
        }
    }
}
