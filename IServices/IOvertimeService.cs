using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Models;

namespace WorkApp.IServices
{
    public interface  IOvertimeService
    {
        public Overtime GetOvertimeByIdEmployee(int id);
        public int AddOvertime(Overtime overtime);
    }
}
