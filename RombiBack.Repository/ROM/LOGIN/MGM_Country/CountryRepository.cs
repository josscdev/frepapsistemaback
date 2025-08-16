using RombiBack.Abstraction;
using RombiBack.Entities.ROM.LOGIN.Company;
using RombiBack.Entities.ROM.LOGIN.Country;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.LOGIN.MGM_Country
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataAcces _dbConnection;

        public CountryRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
      

        public async Task<List<Country>> GetAll()
        {
            List<Country> countries = new List<Country>();

            try
            {
                using (SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    // Abre la conexión antes de usarla
                    await sql.OpenAsync();

                    // Crea y ejecuta la consulta SQL
                    string query = "SELECT idpais, nombrepais FROM [PAIS]";
                    using (SqlCommand cmd = new SqlCommand(query, sql))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            // Comprueba si hay filas devueltas
                            if (reader.HasRows)
                            {
                                // Itera sobre las filas y mapea los resultados a la lista de empresas
                                while (await reader.ReadAsync())
                                {
                                    Country country = new Country
                                    {
                                        idpais = reader["idpais"] != DBNull.Value ? Convert.ToInt32(reader["idpais"]) : (int?)null,
                                        nombrepais = reader["nombrepais"] != DBNull.Value ? reader["nombrepais"].ToString() : null
                                    };
                                    countries.Add(country);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario (registrar, relanzar, etc.)
            }

            return countries;
        }




       
    }
}
