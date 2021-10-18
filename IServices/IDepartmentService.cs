using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Models;

namespace WorkApp.IServices
{
    public interface IDepartmentService
    {
        public bool AddDepartment(Department department);
    }
}
