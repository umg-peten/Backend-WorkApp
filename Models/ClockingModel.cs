using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class ClockingModel
    {
        public int Id { get; set; }
        public int IdEmployee { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
    }
}
