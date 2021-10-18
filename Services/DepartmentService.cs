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
        private readonly ILogsWS _logWS;

        private LogsWSModel logsWSModel = new LogsWSModel();

        public DepartmentService(IConnection connection, ILogsWS logsWS)
        {
            this._connection = connection;
            this._logWS = logsWS;
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

                    logsWSModel.Message = "Ok";
                    logsWSModel.Section = "Services/DepartmentService/AddDepartment";
                    logsWSModel.Success = true;
                    _logWS.InsertLog(logsWSModel);

                }
            }catch(Exception e)
            {
                logsWSModel.Message = e.Message + " " + e.StackTrace;
                logsWSModel.Section = "Services/DepartmentService/AddDepartment";
                logsWSModel.Success = false;
                _logWS.InsertLog(logsWSModel);
            }

            return success;
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();

            try
            {
                using (var sqlConnection = _connection.GetSqlConnection())
                {
                    string query = "SELECT * FROM Departments";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    cmd.Connection.Open();
                    var rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Department department = new Department();
                            department.id = (int)rdr["id"];
                            department.name = (string)rdr["name"];
                            department.description = (string)rdr["description"];
                            departments.Add(department);
                        }
                    }

                    logsWSModel.Message = "Ok";
                    logsWSModel.Section = "Services/DepartmentService/GetAllDepartments";
                    logsWSModel.Success = true;
                    _logWS.InsertLog(logsWSModel);

                }
            }catch(Exception e)
            {
                logsWSModel.Message = e.Message + " " + e.StackTrace;
                logsWSModel.Section = "Services/DepartmentService/GetAllDepartments";
                logsWSModel.Success = false;
                _logWS.InsertLog(logsWSModel);
            }

            return departments;
        }

        public Department GetDepartmentById(int id)
        {
            Department department = new Department();
            try
            {
                using (var sqlConnection = _connection.GetSqlConnection())
                {
                    string query = "SELECT * FROM Departments WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection.Open();
                    var rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            department.id = (int)rdr["id"];
                            department.name = (string)rdr["name"];
                            department.description = (string)rdr["description"];
                        }
                    }

                    logsWSModel.Message = "Ok";
                    logsWSModel.Section = "Services/DepartmentService/GetAllDepartments";
                    logsWSModel.Success = true;
                    _logWS.InsertLog(logsWSModel);

                }
            }
            catch (Exception e)
            {
                logsWSModel.Message = e.Message + " " + e.StackTrace;
                logsWSModel.Section = "Services/DepartmentService/GetAllDepartments";
                logsWSModel.Success = false;
                _logWS.InsertLog(logsWSModel);
            }

            return department;
        }
    }
}
