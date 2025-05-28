namespace IronCore.BusinessLogic.Migrations
{
    using IronCore.Domain.Entities.Product;
    using IronCore.Domain.Entities.User;
    using IronCore.Domain.Enums.User;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<IronCore.BusinessLogic.DBModel.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IronCore.BusinessLogic.DBModel.UserContext";
        }

        protected override void Seed(IronCore.BusinessLogic.DBModel.UserContext ctx)
        {
        }
    }
}
