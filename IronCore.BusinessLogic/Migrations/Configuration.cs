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
            AutomaticMigrationsEnabled = true;
            ContextKey = "IronCore.BusinessLogic.DBModel.UserContext";
        }

        protected override void Seed(IronCore.BusinessLogic.DBModel.UserContext ctx)
        {
            ctx.Products.AddOrUpdate(p => p.Title,
               new ProductDbModel
               {
                   Title = "Фитнес-резинка «Power Band»",
                   Description = "Набор эластичных лент 4–45 кг для разминки и подтягиваний.",
                   Price = 199.90m,
                   ImageUrl = "/Content/Uploads/power_band.jpg"
               },
               new ProductDbModel
               {
                   Title = "Бутылка-шейкер «SteelCore» 700 мл",
                   Description = "Нержавеющая сталь, сеточка-миксер в комплекте.",
                   Price = 89.50m,
                   ImageUrl = "/Content/Uploads/steel_shaker.jpg"
               });
        }
    }
}
