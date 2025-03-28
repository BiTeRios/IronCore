﻿using IronCore.Domain.Enums.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Entities.User
{
    public class UserDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "username not valid")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "password")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "password not valid")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "email")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "email not valid")]
        public string Email { get; set; }

        public URole Role { get; set; }




        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public DateTime LastLoginDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
