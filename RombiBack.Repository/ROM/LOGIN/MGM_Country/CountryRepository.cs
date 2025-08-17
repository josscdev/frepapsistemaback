using Npgsql;
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
            var countries = new List<Country>();

            try
            {
                await using var conn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await conn.OpenAsync();

                const string sql = "select idpais, nombrepais from pais";

                await using var cmd = new NpgsqlCommand(sql, conn);
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    countries.Add(new Country
                    {
                        idpais = reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0),
                        nombrepais = reader.IsDBNull(1) ? null : reader.GetString(1)
                    });
                }
            }
            catch (Exception ex)
            {
                // loguea ex
                throw;
            }

            return countries;
        }





    }
}
