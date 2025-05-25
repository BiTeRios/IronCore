using IronCore.Domain.Entities.Contact;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.DBModel
{
    class ContactContext : DbContext
    {
        public ContactContext() : base("name=IronCore") { }
        public DbSet<ContactDbModel> Contacts { get; set; }
    }
}
