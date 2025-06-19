using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Entities.Program
{
    public class ProgramDbModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } 

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } 

        [MaxLength(200)]
        public string SuitableFor { get; set; } 

        [MaxLength(100)]
        public string Duration { get; set; } 

        [MaxLength(200)]
        public string Trainer { get; set; } 
    }
}
