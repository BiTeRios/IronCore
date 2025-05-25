using IronCore.Domain.Entities.Session;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=IronCore") { }
        public virtual DbSet<Session> Sessions { get; set; }
    }
}
