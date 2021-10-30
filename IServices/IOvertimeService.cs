using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Dtos;
using WorkApp.Models;

namespace WorkApp.IServices
{
    public interface  IOvertimeService
    {
        public List<OvertimeDto> GetOvertime(int type, int? user, string datebegin, string dateend);
        public int AddOvertime(Overtime overtime);
    }
}
