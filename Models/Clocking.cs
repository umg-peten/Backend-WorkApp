using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class Clocking
    {
        public int id { get; set; }
        public int idEmployee { get; set; }
        public int clockInOut { get; set; }
        public string clockInOutDate { get; set; }
    }
}
