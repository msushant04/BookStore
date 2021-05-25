﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class UserEmailOptions
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> ToEmails { get; set; }
        public List<KeyValuePair<string,string>> Placeholders { get; set; }
    }
}
