using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.DBModel
{
    class UserContext : DbContext
    {
        public UserContext() :
            base("name=IronCore")
        {
        }
        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
