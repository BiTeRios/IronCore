using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IronCore.Models
{
	public class Trainer
	{
        [Key]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Введите имя тренера")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию тренера")]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Specialization { get; set; }

        public string Bio { get; set; }

        public string PhotoPath { get; set; }
    }
}