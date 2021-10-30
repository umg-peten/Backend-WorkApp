using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Dtos;
using WorkApp.IServices;
using WorkApp.Models;

namespace WorkApp.Services
{
    public class OvertimeService : IOvertimeService
    {
        private readonly IConnection _connection;
        private readonly ILogsWS _logWs;
        private LogsWSModel logModel;
        public OvertimeService(IConnection connection, ILogsWS logsWs)
        {
            this._connection = connection;
            this._logWs = logsWs;
        }
        public int AddOvertime(Overtime overtime)
        {
            logModel = new LogsWSModel();
            int rows = 0;
            try
            {
                using (var connection = _connection.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand("usp_add_overtime", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idEmployee", overtime.IdEmployee);
                    cmd.Parameters.AddWithValue("@count", overtime.Count);
                    cmd.Parameters.AddWithValue("@description", overtime.Description);
                    cmd.Parameters.AddWithValue("@date", overtime.Date);
                    cmd.Parameters.AddWithValue("@idUser", overtime.IdUser);

                    cmd.Connection.Open();

                    rows = Int32.Parse(cmd.ExecuteScalar().ToString());

                    logModel.Message = "Ok";
                    logModel.Section = "Services/OvertimeService/AddOvertime";
                    logModel.Success = true;
                    _logWs.InsertLog(logModel);

                }
            } catch (Exception e)
            {
                rows = 0;

                logModel.Message = e.Message + " " + e.StackTrace;
                logModel.Section = "Services/OvertimeService/AddOvertime";
                logModel.Success = false;
                _logWs.InsertLog(logModel);
            }
            return rows;
        }

        public List<OvertimeDto> GetOvertime(int type, int? user, string datebegin, string dateend)
        {
            List<OvertimeDto> overtimes = new List<OvertimeDto>();
            using(var connection = _connection.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("usp_get_overtime", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@typeId", type);
                cmd.Parameters.AddWithValue("@idEmployee", user);
                cmd.Parameters.AddWithValue("@dateBeginIN", datebegin);
                cmd.Parameters.AddWithValue("@dateEndIN", dateend);
                cmd.Connection.Open();

                var rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        OvertimeDto overtime = new OvertimeDto();
                        overtime.Id = rdr.GetInt32(0);
                        overtime.Name = rdr.GetString(1);
                        overtime.Lastname = rdr.GetString(2);
                        overtime.Overtime = rdr.GetInt32(3);
                        overtime.AmountOfOvertime = rdr.GetString(4);
                        overtime.Description = rdr.GetString(5);
                        overtime.Date = rdr.GetString(6);

                        overtimes.Add(overtime);
                    }
                }

            }

            return overtimes;
        }
    }
}
