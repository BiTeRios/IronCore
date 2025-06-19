using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronCore.Models
{
	public class ProgramViewModel
	{
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Duration { get; set; }

        public string Trainer { get; set; }

        public string SuitableFor { get; set; }
    }
}