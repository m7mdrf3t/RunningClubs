using System;
using RunGroupWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroupWebApp.ViewModels
{
	public class UserDetailViewModel
	{

        public string? Username { get; set; }
        public int? pace { get; set; }
        public int? Millage { get; set; }

        public string? ProfileImageUrl { get; set; }

        public string? city { get; set; }
        public string? state { get; set; }

    }
}

