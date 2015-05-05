using Domain.Model.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Domain.Model.DAL
{
    public class TemperatureDAL
    {
        private static readonly string ConnectionString;

        static TemperatureDAL()
        {
            ConnectionString = WebConfigurationManager.ConnectionStrings["TemperatureControlConnectionString"].ConnectionString;
        }
        public IEnumerable<Temperature> GetTemperatures()
        {
            try
            {
                var temperatures = new List<Temperature>(50);

                using (var conn = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand("app.ups_ReadTemperature", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var tempIDIndex = reader.GetOrdinal("TempID");
                        var timestampIndex = reader.GetOrdinal("Timestamp");
                        var roomIDIndex = reader.GetOrdinal("RoomID");
                        var tempIndex = reader.GetOrdinal("Temp");

                        while (reader.Read())
                        {
                            temperatures.Add(new Temperature
                            {
                                TempID = reader.GetInt32(tempIDIndex),
                                Timestamp = reader.GetDateTime(timestampIndex),
                                RoomID = reader.GetByte(roomIDIndex),
                                Temp = reader.GetInt16(tempIndex)
                            });
                        }
                    }
                }

                temperatures.TrimExcess();

                return temperatures;
            }
            catch
            {
                throw new ApplicationException("Ett fel inträffade då temperaturerna hämtades från databasen.");
            }
        }
        public IEnumerable<Temperature> GetTemperaturesByRoomID(int roomID)
        {
            try
            {
                var temperatures = new List<Temperature>(50);

                using (var conn = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand("app.ups_ReadTemperature", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RoomID", SqlDbType.TinyInt, 1).Value = roomID;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var tempIDIndex = reader.GetOrdinal("TempID");
                        var timestampIndex = reader.GetOrdinal("Timestamp");
                        var roomIDIndex = reader.GetOrdinal("RoomID");
                        var tempIndex = reader.GetOrdinal("Temp");

                        while (reader.Read())
                        {
                            temperatures.Add(new Temperature
                            {
                                TempID = reader.GetInt32(tempIDIndex),
                                Timestamp = reader.GetDateTime(timestampIndex),
                                RoomID = reader.GetByte(roomIDIndex),
                                Temp = reader.GetInt16(tempIndex)
                            });
                        }
                    }
                }

                temperatures.TrimExcess();

                return temperatures;
            }
            catch
            {
                throw new ApplicationException("Ett fel inträffade då temperaturerna hämtades från databasen.");
            }
        }
        public Temperature GetLatestTemperatureByID(int roomID)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand("app.ups_ReadLatestTemperature", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RoomID", SqlDbType.Int, 4).Value = roomID;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var tempIDIndex = reader.GetOrdinal("TempID");
                        var timestampIndex = reader.GetOrdinal("Timestamp");
                        var roomIDIndex = reader.GetOrdinal("RoomID");
                        var tempIndex = reader.GetOrdinal("Temp");

                        if (reader.Read())
                        {
                            return new Temperature
                            {
                                TempID = reader.GetInt32(tempIDIndex),
                                Timestamp = reader.GetDateTime(timestampIndex),
                                RoomID = reader.GetByte(roomIDIndex),
                                Temp = reader.GetInt16(tempIndex)
                            };
                        }
                    }
                }
                return null;
            }
            catch
            {
                throw new ApplicationException("Ett fel inträffade då temperaturen hämtades från databasen.");
            }
        }
        public void InsertTemperature(Temperature temperature)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand("app.usp_InsertTemperature", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RoomID", SqlDbType.TinyInt, 1).Value = temperature.RoomID;
                    cmd.Parameters.Add("@Temp", SqlDbType.Int, 4).Value = temperature.Temp;

                    //cmd.Parameters.Add("@MemberID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    //temperature.MemberID = (int)cmd.Parameters["@MemberID"].Value;
                }
            }
            catch
            {
                throw new ApplicationException("Ett fel inträffade då temperaturen skulle läggas till i databasen.");
            }
        }
    }
}
