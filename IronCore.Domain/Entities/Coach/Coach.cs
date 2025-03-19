using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace IronCore.Domain.Entities.Coach
{
    class Coach
    {

        public int CoachID { get; set; }
        public string ImagePath { get; set; }
        public string FullName { get; set; }
        public int ExperienceTime { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public string Bio { get; set; }
        public string Testimonials { get; set; }
        public string TelegramUrl { get; set; }
        public string SteamUrl { get; set; }
        public string InstagramUrl { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
