using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class EmailConfirmModel
    {
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsEmailSent { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
