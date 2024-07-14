using System;
using System.ComponentModel.DataAnnotations;

namespace RunGroupWebApp.ViewModels
{
	public class RegisterViewModel
	{
		[Display(Name ="UserName")]
		[Required(ErrorMessage ="You Must Enter User Name")]
		public string UserName { get; set; }

        [Display(Name = "EmailAddress")]
        [Required(ErrorMessage = "You Must Enter Email Address")]
		[DataType(DataType.EmailAddress)]

        public string EmailAdress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You Must Enter Email Password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }


        [Display(Name = "Email Password")]
        [Required(ErrorMessage = "You Must Enter Email Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords are not the same")]
        public string ConfirmaPassword { get; set; }
    }
}

