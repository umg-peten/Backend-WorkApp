using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Models;

namespace WorkApp.IServices
{
    public interface IPositionService
    {
        public List<Position> GetPositionByDepartment(int idDepartment);
    }
}
