using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class SalaryModel
    {
        public int Id { get; set; }
        public int IdEmployee { get; set; }
        public double Salary { get; set; }
        public string SalaryDate { get; set; }
    }
}
