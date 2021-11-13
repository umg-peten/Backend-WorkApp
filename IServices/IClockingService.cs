using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Models;

namespace WorkApp.IServices
{
    public interface IClockingService
    {
        public bool AddClocking(int idEmployee);
        public List<ClockingModel> GetClockingsById(int idEmployee);
    }
}
