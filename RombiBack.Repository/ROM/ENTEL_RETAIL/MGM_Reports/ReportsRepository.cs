using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Reports
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly DataAcces _dbConnection;
        public ReportsRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
    

        public async Task<List<Reports>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionENTEL_RETAIL()))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT A.*, B.NOMBRE, B.ACTIVO " +
                                                          "FROM APP_BI.DBO.SEG_USUARIO_REPORTE A " +
                                                          "LEFT JOIN APP_BI.DBO.GOB_REPORTES_BI B ON A.IDREPORTE = B.CODIGO", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<Reports> result = new List<Reports>();

                        while (await reader.ReadAsync())
                        {
                            Reports report = new Reports
                            {
                                USUARIO = reader["USUARIO"] is DBNull ? null : reader["USUARIO"].ToString(),
                                //IDREPORTE = reader["IDREPORTE"] is DBNull ? (int?)null : Convert.ToInt32(reader["IDREPORTE"]),
                                //NOMBRE = reader["NOMBRE"] is DBNull ? null : reader["NOMBRE"].ToString(),
                                ACTIVO = reader["ACTIVO"] is DBNull ? (int?)null : Convert.ToInt32(reader["ACTIVO"])
                            };

                            result.Add(report);
                        }

                        return result;
                    }
                }
            }

        }

        public async Task<List<Reports>> GetReportes(string docusuario, int idemppaisnegcue)
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
