﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Client> Clients { get; set; }

        public User(string name, string email, string password)
        {
            Name = name;
            Password = password;
            Email = email;
        }
    }
}
