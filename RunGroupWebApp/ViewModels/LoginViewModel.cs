using System;
using System.ComponentModel.DataAnnotations;

namespace RunGroupWebApp.ViewModels
{
	public class LoginViewModel
	{
		[Display(Name ="Email")]
		[Required(ErrorMessage ="Email Format only")]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}

