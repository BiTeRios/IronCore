namespace IronCore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDbChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDbModels", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.UserDbModels", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.UserDbModels", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.UserDbModels", "PhoneNumber", c => c.String(nullable: false));
            DropColumn("dbo.UserDbModels", "PasswordHash");
            DropColumn("dbo.UserDbModels", "IsEmailConfirmed");
            DropColumn("dbo.UserDbModels", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDbModels", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDbModels", "IsEmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDbModels", "PasswordHash", c => c.String());
            AlterColumn("dbo.UserDbModels", "PhoneNumber", c => c.String());
            AlterColumn("dbo.UserDbModels", "LastName", c => c.String());
            AlterColumn("dbo.UserDbModels", "FirstName", c => c.String());
            DropColumn("dbo.UserDbModels", "Balance");
        }
    }
}
