using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.Domain.Entities.Program
{
    public class ProgramDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SuitableFor { get; set; }
        public string Duration { get; set; }
        public string Trainer { get; set; }
    }
}
