using Microsoft.Extensions.Configuration;
using RombiBack.Abstraction;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Bundles;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Bundles
{
    public class BundleRepository : IBundleRepository
    {
        private readonly DataAcces _dbConnection;
        private readonly string _AWSMessageCA;

        public BundleRepository(DataAcces dbConnection, IConfiguration configuration)
        {
            _dbConnection = dbConnection;
            var awsOptions = configuration.GetSection("AWSCredentials");
            _AWSMessageCA = awsOptions["AWSMessageCA"];
        }
        public async Task<List<ListarBundle>> GetBundles(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETBUNDLESMANT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<ListarBundle> response = new List<ListarBundle>();

                            while (await reader.ReadAsync())
                            {
                                ListarBundle bundles = new ListarBundle
                                {
                                    idbundle = reader["idbundle"] != DBNull.Value ? Convert.ToInt32(reader["idbundle"]) : 0,
                                    codigobundle = reader["codigobundle"]?.ToString() ?? "",
                                    nombrebundle = reader["nombrebundle"]?.ToString() ?? "",
                                    flagauthmessage = reader["flagauthmessage"] != DBNull.Value ? Convert.ToInt32(reader["flagauthmessage"]) : 0,
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(bundles);
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

        public async Task<ResultadoBundle> UpdateBundle(ActualizarBundle actualizarbundle)
        {
            ResultadoBundle resultado = new ResultadoBundle();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_PUTBUNDLESMANT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@idbundle", SqlDbType.Int).Value = actualizarbundle.idbundle;
                        command.Parameters.Add("@estadobundle", SqlDbType.Int).Value = actualizarbundle.estadobundle;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar, 15).Value = actualizarbundle.usuario;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                resultado.Estado = reader["Estado"]?.ToString();
                                resultado.Mensaje = reader["Mensaje"]?.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Estado = "ERROR";
                resultado.Mensaje = "Excepción: " + ex.Message;
            }

            return resultado;
        }

    }
}
