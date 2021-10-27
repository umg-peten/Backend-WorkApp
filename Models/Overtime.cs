using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class Overtime
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int IdEmployee { get; set; }
        public int IdUser { get; set; }

    }
}
