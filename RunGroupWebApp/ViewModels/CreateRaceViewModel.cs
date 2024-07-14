using RunGroupWebApp.Data;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.ViewModels
{
	public class CreateRaceViewModel
	{
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public RaceCategory clubCategory { get; set; }
        public Adress address { get; set; }
        public string AppUserId { get; set; }

    }
}

