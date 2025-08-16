using RombiBack.Abstraction;
using RombiBack.Entities.ROM.LOGIN.Country;
using RombiBack.Entities.ROM.LOGIN.UserType;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRestNetCore.DTO.DtoIncentivo;

namespace RombiBack.Repository.ROM.LOGIN.MGM_UserType
{
    public class UsertypeRepository : IUsertypeRepository
    {
        private readonly DataAcces _dbConnection;

        public UsertypeRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
 

        public async Task<List<UserType>> GetAll()
        {
            List<UserType> usertype = new List<UserType>();

            try
            {
                using (SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await sql.OpenAsync();

                    string query = "select * from SEG_TIPOUSUARIO";

                    using (SqlCommand cmd = new SqlCommand(query, sql))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                UserType user = new UserType
                                {
                               
                                    Id= reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : (int?)null,
                                    Nombre= reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() : null,
                                    Estado = reader["Estado"] != DBNull.Value ? Convert.ToInt32(reader["Estado"]) : (int?)null,
                                };
                                usertype.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                LogException(ex);
                NotifyUser("Error: Ya existe un registro con ese valor único.");

            }

            return usertype;
        }
        private void LogException(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        private void NotifyUser(string message)
        {
            Console.WriteLine(message);
        }
       
    }
}
