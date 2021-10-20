﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Dpi { get; set; }
        public string PhoneNumer { get; set; }
        public string Sex { get; set; }
        public Position Position { get; set; }

    }
}
