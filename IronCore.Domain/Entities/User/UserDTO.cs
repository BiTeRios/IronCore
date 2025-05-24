using IronCore.Domain.Enums.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Entities.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Credential { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public URole Level { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public decimal Balance { get; set; }
    }
}
