using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.IServices;
using WorkApp.Models;

namespace WorkApp.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IConnection _connection;

        public DepartmentService(IConnection connection)
        {
            this._connection = connection;
        }
        public bool AddDepartment(Department department)
        {
            bool success = false;
            try
            {
                using (var sqlConnection = _connection.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand("usp_add_department", sqlConnection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", department.name);
                    cmd.Parameters.AddWithValue("@description", department.description);
                    cmd.Connection.Open();

                    int rdr = cmd.ExecuteNonQuery();
                    
                    if (rdr > 0) success = true;

                }
            }catch(Exception e)
            {
                
            }

            return success;
        }
    }
}
