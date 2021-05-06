using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage ="Please enter new password."),DataType(DataType.Password),Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Please enter new password."), DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please enter new password."), DataType(DataType.Password),Compare("NewPassword",ErrorMessage ="Password should be match."), Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
