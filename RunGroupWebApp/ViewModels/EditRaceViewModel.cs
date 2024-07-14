using System;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.ViewModels
{
	public class EditRaceViewModel
	{
        public int id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public string? URL { get; set; }

        public RaceCategory RaceCategory { get; set; }

        public int addressID { get; set; }

        public Adress address { get; set; }
    }
}

