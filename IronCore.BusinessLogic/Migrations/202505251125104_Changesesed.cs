namespace IronCore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changesesed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserDbModels", "Credential");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDbModels", "Credential", c => c.String());
        }
    }
}
