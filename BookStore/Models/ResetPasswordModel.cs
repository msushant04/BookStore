using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserId { get; set; }
        public string Token { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password should be match")]
        public string ConfirmPassword { get; set; }
    }
}
