namespace IronCore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncUserModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDbModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Credential = c.String(),
                        PasswordHash = c.String(),
                        UserName = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        LastLogin = c.DateTime(nullable: false),
                        Level = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ULoginDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ULoginDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Credential = c.String(),
                        PasswordHash = c.String(),
                        LoginIp = c.String(),
                        LoginDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.UserDbModels");
        }
    }
}
