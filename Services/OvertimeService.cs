using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.IServices;
using WorkApp.Models;

namespace WorkApp.Services
{
    public class OvertimeService : IOvertimeService
    {
        private readonly IConnection _connection;

        public OvertimeService(IConnection connection)
        {
            this._connection = connection;
        }
        public bool AddOvertime(Overtime overtime)
        {
            throw new NotImplementedException();
        }

        public Overtime GetOvertimeByIdEmployee(int id)
        {
            throw new NotImplementedException();
        }
    }
}
