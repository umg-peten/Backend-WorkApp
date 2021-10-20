using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Dtos;
using WorkApp.Helpers;
using WorkApp.IServices;
using WorkApp.Models;

namespace WorkApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConnection _connection;
        private readonly ILogsWS _logsWS;
        private readonly ITokenHandler _token;
        private LogsWSModel logModel = new LogsWSModel();
        public AuthenticationService(IConnection connection, ILogsWS logsWS, ITokenHandler token)
        {
            this._connection = connection;
            this._logsWS = logsWS;
            this._token = token;
        }

        public AuthenticatedUserDto Authentication(AuthDto auth)
        {
            AuthenticatedUserDto user = new AuthenticatedUserDto();
            try
            {
                using(var connection = _connection.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand("usp_authentication", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", auth.Username);
                    cmd.Parameters.AddWithValue("@pw", Encrypter.EncryptString(auth.Password));
                    cmd.Connection.Open();

                    var rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            user.IdUser = rdr.GetInt32(0);
                            user.Name = rdr.GetString(1);
                            user.LastName = rdr.GetString(2);
                            user.Sex = rdr.GetString(3);
                            user.Username = rdr.GetString(4);
                            user.ImgProfile = rdr.GetString(5);
                            user.Email = rdr.GetString(6);
                            user.Department = new Department
                            {
                                name = rdr.GetString(7),
                                id = rdr.GetInt32(8)
                            };
                            user.Position = new Position
                            {
                                name = rdr.GetString(9),
                                id = rdr.GetInt32(10)
                            };
                            user.Token = _token.GenerateToken(user);
                        }
                    }
                    logModel.Message = "Ok";
                    logModel.Section = "Services/AuthenticationService/Authentication";
                    logModel.Success = true;
                    _logsWS.InsertLog(logModel);
                    return user;
                }
            }
            catch (Exception e)
            {
                logModel.Message = e.Message + " " + e.StackTrace;
                logModel.Section = "Services/AuthenticationService/Authentication";
                logModel.Success = false;
                _logsWS.InsertLog(logModel);
                user.Token = null;
                return user;
            }
        }
    }
}
