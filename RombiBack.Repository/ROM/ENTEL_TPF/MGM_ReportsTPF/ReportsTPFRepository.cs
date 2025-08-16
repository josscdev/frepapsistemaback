using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Abstraction;

namespace RombiBack.Repository.ROM.ENTEL_TPF.MGM_ReportsTPF
{
    public class ReportsTPFRepository:IReportsTPFRepository
    {

        private readonly DataAcces _dbConnection;
        public ReportsTPFRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<Reports>> GetReportesTPF(string docusuario, int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETREPORTES", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@docusuario", SqlDbType.VarChar).Value = docusuario;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<Reports> response = new List<Reports>();
                            while (await reader.ReadAsync())
                            {
                                Reports reporte = new Reports()
                                {
                                    idreporte = reader["idreporte"] != DBNull.Value ? Convert.ToInt32(reader["idreporte"]) : 0,
                                    docusuario = reader["docusuario"]?.ToString() ?? "",
                                    nombre = reader["nombre"]?.ToString() ?? "",
                                    url = reader["url"]?.ToString() ?? ""


                                };

                                response.Add(reporte);
                            }

                            return response;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
    }
}
