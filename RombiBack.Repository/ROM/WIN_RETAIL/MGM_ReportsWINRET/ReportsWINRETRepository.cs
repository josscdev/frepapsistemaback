using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.WIN_RETAIL.Models.Reports;

namespace RombiBack.Repository.ROM.WIN_RETAIL.MGM_ReportsWINRET
{
    public class ReportsWINRETRepository: IReportsWINRETRepository
    {
        private readonly DataAcces _dbConnection;
        public ReportsWINRETRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<ReportsWINRET>> GetReportesWINRET(string docusuario, int idemppaisnegcue)
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
                            List<ReportsWINRET> response = new List<ReportsWINRET>();
                            while (await reader.ReadAsync())
                            {
                                ReportsWINRET reporte = new ReportsWINRET()
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
