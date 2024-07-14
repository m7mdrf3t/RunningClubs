using RunGroupWebApp.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroupWebApp.Models
{
	public class Race
	{
        [Key]
        public int id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        [ForeignKey("Adress")]
        public int AdressId { get; set; }
        public Adress? adress { get; set; }
        public RaceCategory clubcategory { get; set; }

        [ForeignKey("AppUser")]
        public string? AppuserID { get; set; }
        public AppUser? appuser { get; set; }
    }
}

