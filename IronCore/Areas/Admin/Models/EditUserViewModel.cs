using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IronCore.Areas.Admin.Models
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(30, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }

}