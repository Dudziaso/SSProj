namespace TestProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserPropertyToSecureObjects : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SecureObjects", "UserProfile_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.SecureObjects", "UserProfile_Id");
            AddForeignKey("dbo.SecureObjects", "UserProfile_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecureObjects", "UserProfile_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SecureObjects", new[] { "UserProfile_Id" });
            DropColumn("dbo.SecureObjects", "UserProfile_Id");
        }
    }
}
