using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class Position
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Department Department { get; set; }
    }
}
