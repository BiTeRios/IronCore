namespace IronCore.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ULoginDatas");
        }
    }
}
