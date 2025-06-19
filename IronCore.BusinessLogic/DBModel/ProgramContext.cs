using IronCore.Domain.Entities.Order;
using IronCore.Domain.Entities.Program;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.DBModel
{
    public class ProgramContext : DbContext
    {
        public ProgramContext() : base("name=IronCore") { }
        public DbSet<ProgramDbModel> Programs { get; set; }
    }
}
