using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Dtos;
using WorkApp.Models;

namespace WorkApp.IServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConnection _connection;
        private readonly ILogsWS _logsWS;
        private LogsWSModel logModel = new LogsWSModel();
        public EmployeeService(IConnection connection, ILogsWS logsWS)
        {
            this._connection = connection;
            this._logsWS = logsWS;
        }
        public int AddEmployee(AddEmployeeDto employee)
        {
            int idEmployee = 0;
            try
            {
                using (var connection = _connection.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand("usp_add_employee", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@lastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@birthdate", employee.Birthdate);
                    cmd.Parameters.AddWithValue("@dpi", employee.Dpi);
                    cmd.Parameters.AddWithValue("@phoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@sex", employee.Sex);
                    cmd.Parameters.AddWithValue("@idPosition", employee.IdPosition);
                    cmd.Parameters.AddWithValue("@salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@bonification", employee.Bonification);
                    cmd.Parameters.AddWithValue("@personalExpenses", employee.PersonalExpenses);
                    cmd.Connection.Open();

                    var rs = int.Parse(cmd.ExecuteScalar().ToString()); //Execute Scalar devuelve la primera fila y columna del SP
                    idEmployee = rs;

                    logModel.Message = "Ok";
                    logModel.Section = "Services/EmployeeService/AddEmployee";
                    logModel.Success = true;
                    _logsWS.InsertLog(logModel);
                }
            }
            catch (Exception e)
            {
                logModel.Message = e.Message + " " + e.StackTrace + "\n " + e.Source;
                logModel.Section = "Services/EmployeeService/AddEmployee";
                logModel.Success = false;
                _logsWS.InsertLog(logModel);
                idEmployee = 0;
            }

            return idEmployee;
        }

        public EmployeeDto GetEmployeeById(int id)
        {

            EmployeeDto employee = new EmployeeDto();
            try
            {
                using (var connection = _connection.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand("usp_get_employe_by_id", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection.Open();

                    var rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            employee.Id = rdr.GetInt32(0);
                            employee.Name = rdr.GetString(1);
                            employee.LastName = rdr.GetString(2);
                            employee.Birthdate = rdr.GetDateTime(3).ToString();
                            employee.PhoneNumber = rdr.GetString(4);
                            employee.Sex = rdr.GetString(5);
                            employee.Salary = new SalaryModel
                            {
                                IdEmployee = id,
                                Id = rdr.GetInt32(6),
                                Salary = Double.Parse(rdr.GetSqlMoney(7).ToString()),
                                SalaryDate = rdr.GetDateTime(8).ToString(),
                                Bonification = Double.Parse(rdr.GetSqlMoney(15).ToString()),
                                PersonalExpenses = Double.Parse(rdr.GetSqlMoney(16).ToString()),
                            };
                            employee.Position = new Position
                            {
                                id = rdr.GetInt32(9),
                                name = rdr.GetString(10),
                                description = rdr.GetString(11),
                                Department = new Department
                                {
                                    id = rdr.GetInt32(12),
                                    name = rdr.GetString(13),
                                    description = rdr.GetString(14)
                                }
                            };
                        }
                    }
                    logModel.Message = "Ok";
                    logModel.Section = "Services/EmployeeService/AddService";
                    logModel.Success = true;
                    _logsWS.InsertLog(logModel);
                    return employee;
                }
            }
            catch (Exception e)
            {
                logModel.Message = e.Message + " " + e.StackTrace;
                logModel.Section = "Services/EmployeeService/AddService";
                logModel.Success = false;
                _logsWS.InsertLog(logModel);
                return null;   
            }
        }


        public List<EmployeeDto> GetAllEmployees()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            try
            {
                using (var connection = _connection.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand("usp_get_all_employees", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    var rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            EmployeeDto employee = new EmployeeDto();
                            employee.Id = rdr.GetInt32(0);
                            employee.Name = rdr.GetString(1);
                            employee.LastName = rdr.GetString(2);
                            employee.Birthdate = rdr.GetDateTime(3).ToString();
                            employee.PhoneNumber = rdr.GetString(4);
                            employee.Sex = rdr.GetString(5);
                            employee.Salary = new SalaryModel
                            {
                                IdEmployee = rdr.GetInt32(0),
                                Id = rdr.GetInt32(6),
                                Salary = Double.Parse(rdr.GetSqlMoney(7).ToString()),
                                SalaryDate = rdr.GetDateTime(8).ToString(),
                                Bonification = Double.Parse(rdr.GetSqlMoney(15).ToString()),
                                PersonalExpenses = Double.Parse(rdr.GetSqlMoney(16).ToString())
                            };
                            employee.Position = new Position
                            {
                                id = rdr.GetInt32(9),
                                name = rdr.GetString(10),
                                description = rdr.GetString(11),
                                Department = new Department
                                {
                                    id = rdr.GetInt32(12),
                                    name = rdr.GetString(13),
                                    description = rdr.GetString(14)
                                }
                            };
                            employees.Add(employee);
                        }
                    }
                    logModel.Message = "Ok";
                    logModel.Section = "Services/EmployeeService/AddService";
                    logModel.Success = true;
                    _logsWS.InsertLog(logModel);
                    return employees;
                }
            }
            catch (Exception e)
            {
                logModel.Message = e.Message + " " + e.StackTrace;
                logModel.Section = "Services/EmployeeService/AddService";
                logModel.Success = false;
                _logsWS.InsertLog(logModel);
                return null;
            }
        }

        public int UpdateEmployee(UpdateEmployeeDto employee)
        {
            int idEmployee = 0;
            try
            {
                using (var connection = _connection.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand("usp_update_employee", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", employee.Id);
                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@lastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@birthdate", employee.Birthdate);
                    cmd.Parameters.AddWithValue("@dpi", employee.Dpi);
                    cmd.Parameters.AddWithValue("@phoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@sex", employee.Sex);
                    cmd.Parameters.AddWithValue("@idPosition", employee.IdPosition);
                    cmd.Parameters.AddWithValue("@salaryIn", employee.Salary);
                    cmd.Parameters.AddWithValue("@bonification", employee.Bonification);
                    cmd.Parameters.AddWithValue("@personalExpenses", employee.PersonalExpenses);
                    cmd.Connection.Open();

                    var rs = int.Parse(cmd.ExecuteScalar().ToString()); //Execute Scalar devuelve la primera fila y columna del SP
                    idEmployee = rs;

                    logModel.Message = "Ok";
                    logModel.Section = "Services/EmployeeService/UpdateEmployee";
                    logModel.Success = true;
                    _logsWS.InsertLog(logModel);
                }
            }
            catch (Exception e)
            {
                logModel.Message = e.Message + " " + e.StackTrace + "\n " + e.Source;
                logModel.Section = "Services/EmployeeService/UpdateEmployee";
                logModel.Success = false;
                _logsWS.InsertLog(logModel);
                idEmployee = 0;
            }

            return idEmployee;

        }

        public bool ExistEmployeeById(int id)
        {
            bool existEmployee = false;

            using(var connection = _connection.GetSqlConnection())
            {
                string query = "SELECT COUNT(1) FROM Employee WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.Connection.Open();

                int rdr = Int32.Parse(cmd.ExecuteScalar().ToString());

                if (rdr > 0) existEmployee = true;
            }

            return existEmployee;
        }

        public bool ExistDpi(string dpi)
        {
            bool existEmployee = false;

            using (var connection = _connection.GetSqlConnection())
            {
                string query = "SELECT COUNT(1) FROM Employee WHERE dpi = @dpi";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@dpi", dpi);

                cmd.Connection.Open();

                int rdr = Int32.Parse(cmd.ExecuteScalar().ToString());

                if (rdr > 0) existEmployee = true;
            }

            return existEmployee;
        }
    }
}
