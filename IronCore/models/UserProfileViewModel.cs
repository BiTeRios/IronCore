using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronCore.Models
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int OrdersCount { get; set; }
    }

}