using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Accesorios;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Modelo
{
    public class ModeloRepository : IModeloRepository
    {
        private readonly DataAcces _dbConnection;

        public ModeloRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }



        public async Task<List<Modelo>> GetModeloRomWeb(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETMODELO_MNG", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<Modelo> response = new List<Modelo>();

                                while (await reader.ReadAsync())
                                {
                                    Modelo modeloresponse = new Modelo();

                                    modeloresponse.idmodelo = reader.IsDBNull(reader.GetOrdinal("idmodelo"))
                                                                        ? null
                                                                        : reader.GetInt32(reader.GetOrdinal("idmodelo"));
                                    modeloresponse.nombremodelo = reader.IsDBNull(reader.GetOrdinal("nombremodelo"))
                                                                         ? null
                                                                         : reader.GetString(reader.GetOrdinal("nombremodelo"));
                                    modeloresponse.nombremarca = reader.IsDBNull(reader.GetOrdinal("nombremarca"))
                                                                         ? null
                                                                         : reader.GetString(reader.GetOrdinal("nombremarca"));
                                    modeloresponse.nombregamma = reader.IsDBNull(reader.GetOrdinal("nombregamma"))
                                                                         ? null
                                                                         : reader.GetString(reader.GetOrdinal("nombregamma"));
                                    modeloresponse.idemppaisnegcue = reader.IsDBNull(reader.GetOrdinal("idemppaisnegcue"))
                                                                           ? null
                                                                           : reader.GetInt32(reader.GetOrdinal("idemppaisnegcue"));
                                    modeloresponse.estado = reader.IsDBNull(reader.GetOrdinal("estado"))
                                                                           ? null
                                                                           : reader.GetInt32(reader.GetOrdinal("estado"));
                                    modeloresponse.usuariocreacion = reader.IsDBNull(reader.GetOrdinal("usuariocreacion"))
                                                                           ? null
                                                                           : reader.GetString(reader.GetOrdinal("usuariocreacion"));
                                    modeloresponse.fechacreacion = reader.IsDBNull(reader.GetOrdinal("fechacreacion"))
                                                                          ? null
                                                                          : reader.GetDateTime(reader.GetOrdinal("fechacreacion"));
                                    modeloresponse.usuariomodificacion = reader.IsDBNull(reader.GetOrdinal("usuariomodificacion"))
                                                                            ? null
                                                                            : reader.GetString(reader.GetOrdinal("usuariomodificacion"));
                                    modeloresponse.fechamodificacion = reader.IsDBNull(reader.GetOrdinal("fechamodificacion"))
                                                                           ? null
                                                                           : reader.GetDateTime(reader.GetOrdinal("fechamodificacion"));


                                    response.Add(modeloresponse);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<Modelo>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Por ejemplo, podrías registrar el error y devolver un mensaje de error adecuado
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }
        }






        public async Task<Respuesta> PostModeloRomWeb(Modelo modelo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_POSTMODELO", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idmodelo", SqlDbType.Int).Value = modelo.idmodelo;
                        cmd.Parameters.Add("@nombremodelo", SqlDbType.VarChar).Value = modelo.nombremodelo;
                        cmd.Parameters.Add("@nombremarca", SqlDbType.VarChar).Value = modelo.nombremarca;
                        cmd.Parameters.Add("@nombregamma", SqlDbType.VarChar).Value = modelo.nombregamma;
                        cmd.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = modelo.idemppaisnegcue;
                        cmd.Parameters.Add("@usuariocreacion", SqlDbType.VarChar).Value = modelo.usuariocreacion;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            Respuesta respuesta = new Respuesta();

                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));

                                // Manejar múltiples filas si es necesario
                                // Por ejemplo, almacenar cada resultado en una lista
                            }

                            return respuesta;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("Ya existe un modelo con la misma descripción.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el modelo.");
                }
            }
        }


        public async Task<Respuesta> DeleteModeloRomWeb(Modelo modelo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_DELETEMODELO", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idmodelo", SqlDbType.VarChar).Value = modelo.idmodelo;
                        cmd.Parameters.Add("@nombremodelo", SqlDbType.VarChar).Value = modelo.nombremodelo;
                        cmd.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = modelo.idemppaisnegcue;
                        cmd.Parameters.Add("@usuariomodificacion", SqlDbType.VarChar).Value = modelo.usuariomodificacion;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            Respuesta respuesta = new Respuesta();

                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));

                                // Manejar múltiples filas si es necesario
                                // Por ejemplo, almacenar cada resultado en una lista
                            }

                            return respuesta;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("ERROR");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error ");
                }
            }
        }
    }
}

