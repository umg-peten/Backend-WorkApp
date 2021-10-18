using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.IServices
{
    public interface IConnection
    {
        public string GetConnectionString();
        public SqlConnection GetSqlConnection();
    }
}
