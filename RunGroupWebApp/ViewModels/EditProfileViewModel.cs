using System;
namespace RunGroupWebApp.ViewModels
{
	public class EditProfileViewModel
	{
        public string? Username { get; set; }
        public int? pace { get; set; }
        public int? Millage { get; set; }
        public string? ProfileImage { get; set; }
        public IFormFile? Image { get; set; }

        public string? city { get; set; }
        public string? state { get; set; }
    }
}

