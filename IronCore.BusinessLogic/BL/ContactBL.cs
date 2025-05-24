using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.Interfaces;
using IronCore.BusinessLogic.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Entities.Contact;

namespace IronCore.BusinessLogic.BL
{
    internal class ContactBL : ContactApi, IContact
    {
        public List<ContactDTO> GetAllContacts()
        {
            return GetAllContactsAPI().Select(MapToContact).ToList();
        }
        public ContactDTO GetContactById(int id)
        {
            var contactId = GetContactByIdAPI(id);
            return contactId != null ? MapToContact(contactId) : null;
        }
        public bool CreateContact(ContactDTO newContact)
        {
            if (string.IsNullOrWhiteSpace(newContact.Name) || string.IsNullOrWhiteSpace(newContact.Email) || string.IsNullOrWhiteSpace(newContact.PhoneNumber) || string.IsNullOrWhiteSpace(newContact.Message))
            {
                return false;
            }
            var dbContact = MapToDB(newContact);
            CreateContactAPI(dbContact);
            return true;
        }
        public bool DeleteContact(int id)
        {
            if (id <= 0) return false;
            try
            {
                DeleteContactAPI(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private ContactDbModel MapToDB(ContactDTO contact)
        {
            return new ContactDbModel
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Message = contact.Message
            };
        }
        private ContactDTO MapToContact(ContactDbModel db)
        {
            return new ContactDTO
            {
                Id = db.Id,
                Name = db.Name,
                Email = db.Email,
                PhoneNumber = db.PhoneNumber,
                Message = db.Message
            };
        }
    }
}
