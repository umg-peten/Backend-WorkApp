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
        public EmployeeDto GetEmployeeById(int id);
        public List<EmployeeDto> GetAllEmployees();
        public int UpdateEmployee(UpdateEmployeeDto employee);
        public bool ExistEmployeeById(int id);
        public bool ExistDpi(string dpi);

    }
}
