using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Dtos;

namespace WorkApp.IServices
{
    public interface IEmployeeService
    {
        public int AddEmployee(AddEmployeeDto employee);
    }
}
