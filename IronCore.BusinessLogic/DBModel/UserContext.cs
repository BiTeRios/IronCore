using IronCore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=IronCore") { }
        public DbSet<UserDbModel> Users { get; set; }
    }
}
