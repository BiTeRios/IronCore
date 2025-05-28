using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IronCore.Domain.Entities.Coach
{
    public class CoachDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string FullName { get; set; }
        [Display(Name = "ExperienceTime")]
        public int ExperienceTime { get; set; }
        [Display(Name = "Qualification")]
        public string Qualification { get; set; }
        [Display(Name = "Specialization")]
        public string Specialization { get; set; }
        [Display(Name = "Bio")]
        public string Bio { get; set; }
        [Display(Name = "Testimonials")]
        public string Testimonials { get; set; }
        [Display(Name = "TelegramUrl")]
        public string TelegramUrl { get; set; }
        [Display(Name = "SteamUrl")]
        public string SteamUrl { get; set; }
        [Display(Name = "InstagramUrl")]
        public string InstagramUrl { get; set; }
    }
}
