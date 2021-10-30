using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Models;

namespace WorkApp.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Sex { get; set; }
        public string Dpi { get; set; }
        public SalaryModel Salary  { get; set; }
        public Position Position { get; set; }

    }
}
