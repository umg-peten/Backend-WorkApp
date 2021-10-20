using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Dtos
{
    public class AddEmployeeDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Dpi { get; set; }
        public int Sex { get; set; }
        public int IdPosition { get; set; }
        public double Salary { get; set; }
    }
}
