using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.IServices;

namespace WorkApp.Helpers
{
    public class Connection : IConnection
    {
        private readonly IConfiguration _config;
        public Connection(IConfiguration config)
        {
            this._config = config;
        }

        public string GetConnectionString()
        {

            string connectionString = ConfigurationExtensions.GetConnectionString(this._config, "LocalDB");
            return connectionString;
        }

        public SqlConnection GetSqlConnection()
        {
            string connectionString = ConfigurationExtensions.GetConnectionString(this._config, "LocalDB");
            SqlConnection sqlConnection = new SqlConnection(Helpers.Encrypter.Decrypt(connectionString));
            return sqlConnection;
        }
    }
}
