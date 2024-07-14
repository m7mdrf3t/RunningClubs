using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RunGroupWebApp.Models
{
	public class AppUser : IdentityUser
	{
		[Key]
		public string? id { get; set; }
		public int? pace { get; set; }
		public int? Millage { get; set; }

        public string? ProfileImageUrl { get; set; }

        public string? city { get; set; }
        public string? state { get; set; }

        [ForeignKey("Adress")]
		public int? AddressID { get; set; }
		public Adress? Adress { get; set; }

		public ICollection<Race>? races { get; set; }
		public ICollection<Club>? clubs {get; set;}
	}
}

