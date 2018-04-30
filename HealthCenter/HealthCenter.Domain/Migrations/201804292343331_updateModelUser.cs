namespace HealthCenter.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                        Telephone = c.String(maxLength: 20),
                        ImagePath = c.String(),
                        UserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.Email, unique: true, name: "User_Email_Index")
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        UserTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserTypeId)
                .Index(t => t.Name, unique: true, name: "UserType_Name_Index");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.UserTypes", "UserType_Name_Index");
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropIndex("dbo.Users", "User_Email_Index");
            DropTable("dbo.UserTypes");
            DropTable("dbo.Users");
        }
    }
}
