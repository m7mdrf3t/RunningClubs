using System;
namespace RunGroupWebApp.ViewModels
{
	public class UserVeiwModel
	{
		public string id { get; set; }
		public string? username { get; set; }
		public int? millage { get; set; }
		public int? pace { get; set; }

		public string? city { get; set; }
        public string? state { get; set; }

		public string ProfileImageUrl { get; set; }
	}
}

