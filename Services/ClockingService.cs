using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.IServices;
using WorkApp.Models;

namespace WorkApp.Services
{
    public class ClockingService : IClockingService
    {
        private readonly IConnection _connection;

        public ClockingService(IConnection connection)
        {
            this._connection = connection;
        }
        public bool AddClocking(int idEmployee)
        {
            using(var connection = _connection.GetSqlConnection())
            {
                SqlCommand command = new SqlCommand("usp_addClocking", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idEmployee", idEmployee);
                command.Connection.Open();

                int rowsAffected = Int32.Parse(command.ExecuteScalar().ToString());

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<ClockingModel> GetClockingsById(int idEmployee)
        {
            using(var connection = _connection.GetSqlConnection())
            {
                List<ClockingModel> clockings = new List<ClockingModel>();
                SqlCommand command = new SqlCommand("usp_getClockingByIdemployee", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idEmployee", idEmployee);

                command.Connection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ClockingModel clocking = new ClockingModel();
                        clocking.Id = reader.GetInt32(0);
                        clocking.IdEmployee = reader.GetInt32(1);
                        clocking.Type = reader.GetString(2);
                        clocking.Date = reader.GetString(3);
                        clockings.Add(clocking);
                    }
                    return clockings;
                }
                else
                {
                    return null;
                }

            }
        }
    }
}
