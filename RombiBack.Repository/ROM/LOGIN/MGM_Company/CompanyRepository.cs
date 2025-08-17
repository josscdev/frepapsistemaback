using Npgsql;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.LOGIN.Company;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.LOGIN.Company
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataAcces _dbConnection;

        public CompanyRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<List<Companys>> GetCompany()
        {
            var companies = new List<Companys>();

            await using var conn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
            await conn.OpenAsync();

            const string sql = "select idempresa, nombreempresa from empresa";

            await using var cmd = new NpgsqlCommand(sql, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            // (Opcional) Obtén los ordinales una sola vez
            int ordId = reader.GetOrdinal("idempresa");
            int ordNombre = reader.GetOrdinal("nombreempresa");

            while (await reader.ReadAsync())
            {
                companies.Add(new Companys
                {
                    idempresa = reader.IsDBNull(ordId) ? (int?)null : reader.GetInt32(ordId),   // si es BIGINT, usa GetInt64 y castea
                    nombreempresa = reader.IsDBNull(ordNombre) ? null : reader.GetString(ordNombre)
                });
            }

            return companies;
        }
    }
}


