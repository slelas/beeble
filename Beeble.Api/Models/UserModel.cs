using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Beeble.Api.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

	    [Required]
	    [Display(Name = "Name")]
	    public string Name { get; set; }

	    [Required]
	    [Display(Name = "Lastname")]
	    public string Lastname { get; set; }

	    [Required]
	    [Display(Name = "Oib")]
	    public string Oib { get; set; }

	    [Required]
	    [Display(Name = "Address")]
	    public string Address { get; set; }

	    [Required]
	    [Display(Name = "City")]
	    public string City { get; set; }

	    [Required]
	    [Display(Name = "Phone Number")]
	    public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

    }
}