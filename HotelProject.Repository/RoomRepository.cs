using HotelProject.Data;
using HotelProject.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelProject.Repository
{
    public class RoomRepository
    {
        public async Task<List<Room>> GetRooms()
        {
            List<Room> result = new();
            const string sqlExpression = "sp_GetAllRooms";
            using (SqlConnection connection = new(ApplicationDbContext.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new(sqlExpression, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            result.Add(new Room()
                            {
                                Id = reader.GetInt32(0),
                                Name = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty,
                                IsFree = !reader.IsDBNull(2) ? reader.GetBoolean(2) : false,
                                HotelId = reader.GetInt32(3),
                                DailyPrice = reader.GetDecimal(4)
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
                return result;
            }
        }

        public async Task AddRoom(Room room)
        {
            string sqlExpression = "sp_AddRoom";

            using (SqlConnection connection = new(ApplicationDbContext.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new(sqlExpression, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("name", room.Name);
                    command.Parameters.AddWithValue("isFree", room.IsFree ? "1" : "0");
                    command.Parameters.AddWithValue("hotelId", room.HotelId);
                    command.Parameters.AddWithValue("dailyPrice", room.DailyPrice);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Query didn't effect any data");
                    }

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task UpdateRoom(Room room)
        {
            string sqlExpression = "sp_UpdateRoom";

            using (SqlConnection connection = new(ApplicationDbContext.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new(sqlExpression, connection);

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("name", room.Name);
                    command.Parameters.AddWithValue("isFree", room.IsFree);
                    command.Parameters.AddWithValue("hotelId", room.HotelId);
                    command.Parameters.AddWithValue("dailyPrice", room.DailyPrice);


                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Query didn't effect any data");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public async Task DeleteRoom(int id)
        {
            string sqlExpression = @$"sp_DeleteRoom";

            using (SqlConnection connection = new(ApplicationDbContext.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new(sqlExpression, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("roomId", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Query didn't effect any data");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }

        }
    }
}
