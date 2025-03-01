﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFlight
{
    public class Passenger
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public DateOnly birthDate { get; set; }
        public string email { get; set; }

        public Passenger(string firstname, string surname, DateOnly birthDate, string email)
        {
            this.id = id;
            this.firstname = firstname;
            this.surname = surname;
            this.birthDate = birthDate;
            this.email = email;
        }
    }
}
