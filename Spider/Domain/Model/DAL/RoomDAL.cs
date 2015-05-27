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
    public class RoomDAL
    {
        private static readonly string ConnectionString;
        static RoomDAL()
        {
            ConnectionString = WebConfigurationManager.ConnectionStrings["TemperatureControlConnectionString"].ConnectionString;
        }
        public IEnumerable<Room> GetRooms()
        {
            try
            {
                var rooms = new List<Room>(10);

                using (var conn = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand("app.ups_ReadRoom", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var roomIDIndex = reader.GetOrdinal("RoomID");
                        var roomDescriptionIndex = reader.GetOrdinal("RoomDescription");
                        var heatingIndex = reader.GetOrdinal("Heating");
                        var lastTemperatureIndex = reader.GetOrdinal("LastTemperature");
                        var automaticControlIndex = reader.GetOrdinal("AutomaticControl");

                        while (reader.Read())
                        {
                            rooms.Add(new Room
                            {
                                RoomID = reader.GetByte(roomIDIndex),
                                RoomDescription = reader.GetString(roomDescriptionIndex),
                                Heating = reader.GetBoolean(heatingIndex),
                                LastTemperature = reader.GetInt32(lastTemperatureIndex),
                                AutomaticControl = reader.GetBoolean(automaticControlIndex)
                            });
                        }
                    }
                }

                rooms.TrimExcess();

                return rooms;
            }
            catch
            {
                throw new ApplicationException("Ett fel inträffade då rummen hämtades från databasen.");
            }
        }

        public Room GetRoomById(int roomID)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand("app.ups_ReadRoom", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RoomID", SqlDbType.Int, 4).Value = roomID;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var roomIDIndex = reader.GetOrdinal("RoomID");
                        var roomDescriptionIndex = reader.GetOrdinal("RoomDescription");
                        var heatingIndex = reader.GetOrdinal("Heating");
                        var lastTemperatureIndex = reader.GetOrdinal("LastTemperature");
                        var automaticControlIndex = reader.GetOrdinal("AutomaticControl");

                        if (reader.Read())
                        {
                            return new Room
                            {
                                RoomID = reader.GetByte(roomIDIndex),
                                RoomDescription = reader.GetString(roomDescriptionIndex),
                                Heating = reader.GetBoolean(heatingIndex),
                                LastTemperature = reader.GetInt32(lastTemperatureIndex),
                                AutomaticControl = reader.GetBoolean(automaticControlIndex)
                            };
                        }
                    }
                }
                return null;
            }
            catch
            {
                throw new ApplicationException("Ett fel inträffade då rummet hämtades från databasen.");
            }
        }

        public void UpdateRoom(Room room)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand("app.usp_UpdateRoom", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RoomID", SqlDbType.TinyInt, 1).Value = room.RoomID;
                    cmd.Parameters.Add("@Heating", SqlDbType.Bit, 1).Value = room.Heating;
                    cmd.Parameters.Add("@RoomDescription", SqlDbType.VarChar, 50).Value = room.RoomDescription;
                    cmd.Parameters.Add("@LastTemperature", SqlDbType.Int, 4).Value = room.LastTemperature;
                    cmd.Parameters.Add("@AutomaticControl", SqlDbType.Bit, 1).Value = room.AutomaticControl;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new ApplicationException("Ett fel inträffade då rummet skulle uppdateras i databasen.");
            }
        }
    }
}
