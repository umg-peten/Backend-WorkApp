using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.IServices;
using WorkApp.Models;

namespace WorkApp.Services
{
    public class PositionService : IPositionService
    {
        private readonly IConnection _connection;

        public PositionService(IConnection connection)
        {
            this._connection = connection;
        }
        public List<Position> GetPositionByDepartment(int idDepartment)
        {
            List<Position> positions = new List<Position>();
            
            using(var connection = _connection.GetSqlConnection())
            {
                string query = "SELECT * FROM Positions where idDepartment = @idDepartment";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idDepartment", idDepartment);
                cmd.Connection.Open();

                var rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Position pos = new Position();
                        pos.id = rdr.GetInt32(0);
                        pos.name = rdr.GetString(1);
                        pos.description = rdr.GetString(2);
                        positions.Add(pos);
                    }
                }

            }
            return positions;
        }
    }
}
