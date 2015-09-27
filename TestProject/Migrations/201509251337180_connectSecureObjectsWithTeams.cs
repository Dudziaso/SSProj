namespace TestProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class connectSecureObjectsWithTeams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecurityTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        Number = c.String(),
                        IsReady = c.Boolean(nullable: false),
                        LastSeen = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecurityTeamSecureObjects",
                c => new
                    {
                        SecurityTeam_Id = c.Int(nullable: false),
                        SecureObject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SecurityTeam_Id, t.SecureObject_Id })
                .ForeignKey("dbo.SecurityTeams", t => t.SecurityTeam_Id, cascadeDelete: true)
                .ForeignKey("dbo.SecureObjects", t => t.SecureObject_Id, cascadeDelete: true)
                .Index(t => t.SecurityTeam_Id)
                .Index(t => t.SecureObject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecurityTeamSecureObjects", "SecureObject_Id", "dbo.SecureObjects");
            DropForeignKey("dbo.SecurityTeamSecureObjects", "SecurityTeam_Id", "dbo.SecurityTeams");
            DropIndex("dbo.SecurityTeamSecureObjects", new[] { "SecureObject_Id" });
            DropIndex("dbo.SecurityTeamSecureObjects", new[] { "SecurityTeam_Id" });
            DropTable("dbo.SecurityTeamSecureObjects");
            DropTable("dbo.SecurityTeams");
        }
    }
}
