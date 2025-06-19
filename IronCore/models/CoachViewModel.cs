using System.ComponentModel.DataAnnotations;

namespace IronCore.Models
{
    public class CoachViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Фото тренера")]
        public string ImagePath { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Display(Name = "Опыт работы (лет)")]
        public int ExperienceTime { get; set; }

        [Display(Name = "Квалификация")]
        public string Qualification { get; set; }

        [Display(Name = "Специализация")]
        public string Specialization { get; set; }

        [Display(Name = "О тренере")]
        public string Bio { get; set; }

        [Display(Name = "Отзывы клиентов")]
        public string Testimonials { get; set; }

        [Display(Name = "Telegram")]
        public string TelegramUrl { get; set; }

        [Display(Name = "Steam")]
        public string SteamUrl { get; set; }

        [Display(Name = "Instagram")]
        public string InstagramUrl { get; set; }
    }
}
