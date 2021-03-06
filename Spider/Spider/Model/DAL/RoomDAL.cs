﻿using Spider.Model.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Model.DAL
{
    class RoomDAL
    {
        private static readonly string ConnectionString;
        static RoomDAL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["TemperatureControlConnectionString"].ConnectionString;
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

                        while (reader.Read())
                        {
                            rooms.Add(new Room
                                {
                                    RoomID = reader.GetInt32(roomIDIndex),
                                    RoomDescription = reader.GetString(roomDescriptionIndex),
                                    Heating = reader.GetBoolean(heatingIndex),
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
        public void UpdateRoom(Room room)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand("appSchema.usp_UpdateRoom", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RoomID", SqlDbType.TinyInt, 1).Value = room.RoomID;
                    cmd.Parameters.Add("@Heating", SqlDbType.Bit, 1).Value = room.Heating;
                    cmd.Parameters.Add("@RoomDescription", SqlDbType.VarChar, 50).Value = room.RoomDescription;
                    
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
