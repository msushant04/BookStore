using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class SignUpUserModel
    {
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="Please enter first name.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        //[Display(Name ="Date Of Birth")]
        //public DateTime? DOB { get; set; }
        [Required(ErrorMessage ="Please Enter your email")]
        [EmailAddress(ErrorMessage ="Please Enter Valid Email Addresss.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter your email")]
        [DataType(DataType.Password)]
        //[Range(8,10)]
        [Compare("ConfirmPassword", ErrorMessage = "Password should be match")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter your email")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
