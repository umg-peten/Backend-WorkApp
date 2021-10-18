using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.IServices;
using WorkApp.Models;

namespace WorkApp.Services
{
    public class LogWS : ILogsWS
    {
        private readonly IConnection _connection;
        public LogWS(IConnection connection)
        {
            this._connection = connection;
        }

        public void InsertLog(LogsWSModel log)
        {
            using (var sqlConnection = _connection.GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("usp_insert_log", sqlConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@message", log.Message);
                cmd.Parameters.AddWithValue("@section", log.Section);
                cmd.Parameters.AddWithValue("@success", log.Success);
                cmd.Parameters.AddWithValue("@dateBegin", log.DateBegin);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
