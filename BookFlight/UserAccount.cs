﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFlight
{
    public class UserAccount
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public UserAccount(string username, string password, string email)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.email = email;
        }
    }
}
