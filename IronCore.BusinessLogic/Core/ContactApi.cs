using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.Core
{
    public class ContactApi
    {
        public List<ContactDbModel> GetAllContactsAPI()
        {
            using (var context = new ContactContext())
            {
                return context.Contacts.ToList();
            }
        }
        public ContactDbModel GetContactByIdAPI(int id)
        {
            using (var context = new ContactContext())
            {
                return context.Contacts.FirstOrDefault(n => n.Id == id);
            }
        }
        public void CreateContactAPI(ContactDbModel newContact)
        {
            using(var context = new ContactContext())
            {
                context.Contacts.Add(newContact);
                context.SaveChanges();
            }
        }
        public void DeleteContactAPI(int id)
        {
            using (var context = new ContactContext())
            {
                var contacts = context.Contacts.Find(id);
                if (contacts == null) throw new ArgumentException("News not found");

                context.Contacts.Remove(contacts);
                context.SaveChanges();
            }
        }
    }
}
