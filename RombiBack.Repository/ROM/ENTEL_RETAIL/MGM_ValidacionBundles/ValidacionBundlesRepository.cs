using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles.ValidacionBundle;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_ValidacionBundles
{
    public class ValidacionBundlesRepository : IValidacionBundlesRepository
    {
        private readonly DataAcces _dbConnection;

        public ValidacionBundlesRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ValidacionBundle> GetBundlesVentas(int idventas)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    //using (SqlCommand cmd = new SqlCommand("USP_GETBUNDLESVENTAS2", connection))
                    using (SqlCommand cmd = new SqlCommand("USP_GETBUNDLESVENTAS_V2", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idventas", SqlDbType.Int).Value = idventas;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            ValidacionBundle respuesta =  new ValidacionBundle();
                            while (await reader.ReadAsync())
                            {

                                respuesta.idventas = reader.GetInt32(reader.GetOrdinal("idventas"));
                                respuesta.idventasdetalle = reader.GetInt32(reader.GetOrdinal("idventasdetalle"));
                                respuesta.fechaoperacion = reader.GetString(reader.GetOrdinal("fechaoperacion"));
                                respuesta.doccliente = reader.GetString(reader.GetOrdinal("doccliente"));
                                respuesta.numcelularcontrato = reader.GetString(reader.GetOrdinal("numcelularcontrato"));
                                respuesta.docpromotorasesor = reader.GetString(reader.GetOrdinal("docpromotorasesor"));
                                respuesta.nombrepromotor = reader.GetString(reader.GetOrdinal("nombrepromotor"));
                                //respuesta.docpromotorasesor = reader.GetString(reader.GetOrdinal("docpromotorasesor"));
                                respuesta.idsubproducto = reader.GetInt32(reader.GetOrdinal("idsubproducto"));
                                respuesta.nombresubproducto = reader.GetString(reader.GetOrdinal("nombresubproducto"));
                                respuesta.idplan = reader.GetInt32(reader.GetOrdinal("idplan"));
                                respuesta.nombreplan = reader.GetString(reader.GetOrdinal("nombreplan"));
                                respuesta.idmodelo = reader.GetInt32(reader.GetOrdinal("idmodelo"));
                                respuesta.nombremodelo = reader.GetString(reader.GetOrdinal("nombremodelo"));
                                respuesta.idbundle = reader.GetInt32(reader.GetOrdinal("idbundle"));
                                respuesta.codigobundle = reader.GetString(reader.GetOrdinal("codigobundle"));
                                respuesta.nombrebundle = reader.GetString(reader.GetOrdinal("nombrebundle"));
                                //respuesta.codigoauthbundle = reader.GetString(reader.GetOrdinal("codigoauthbundle"));
                                respuesta.fechacreacion = reader.GetString(reader.GetOrdinal("fechacreacion"));
                                respuesta.numeroorden = reader.GetString(reader.GetOrdinal("numeroorden"));
                                respuesta.idpdv = reader.GetInt32(reader.GetOrdinal("idpdv"));
                                respuesta.nombrepdv = reader.GetString(reader.GetOrdinal("nombrepdv"));
                                respuesta.flagcodigoauthbundle = reader.GetInt32(reader.GetOrdinal("flagcodigoauthbundle"));
                                respuesta.idemppaisnegcue = reader.GetInt32(reader.GetOrdinal("idemppaisnegcue"));
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
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al obtener las ventas.");
                }
            }
        }

        public async Task<Respuesta> ValidarCodigoAuthBundle(int idventasdetalle, string codigoauthbundle)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    //using (SqlCommand cmd = new SqlCommand("USP_VALIDARCODIGOAUTHBUNDLE2", connection))
                    using (SqlCommand cmd = new SqlCommand("USP_VALIDARCODIGOAUTHBUNDLE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idventasdetalle", SqlDbType.VarChar).Value = idventasdetalle;
                        cmd.Parameters.Add("@codigoauthbundle", SqlDbType.VarChar).Value = codigoauthbundle;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            Respuesta respuesta = new Respuesta();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));
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
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }

        public async Task<Respuesta> PostBundlesFirma(ValidacionBundle validacionbundle)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    //using (SqlCommand cmd = new SqlCommand("USP_POSTBUNDLESFIRMA2", connection))
                    using (SqlCommand cmd = new SqlCommand("USP_POSTBUNDLESFIRMA", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idventas", SqlDbType.Int).Value = validacionbundle.idventas;
                        cmd.Parameters.Add("@idventasdetalle", SqlDbType.Int).Value = validacionbundle.idventasdetalle;
                        cmd.Parameters.Add("@docpromotorasesor", SqlDbType.VarChar).Value = validacionbundle.docpromotorasesor;
                        cmd.Parameters.Add("@doccliente", SqlDbType.VarChar).Value = validacionbundle.doccliente;
                        cmd.Parameters.Add("@idbundle", SqlDbType.Int).Value = validacionbundle.idbundle;
                        cmd.Parameters.Add("@codigobundle", SqlDbType.VarChar).Value = validacionbundle.codigobundle;
                        cmd.Parameters.Add("@nombreobsequio", SqlDbType.VarChar).Value = validacionbundle.nombreobsequio ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = validacionbundle.idemppaisnegcue;
                        cmd.Parameters.Add("@usuariocreacion", SqlDbType.VarChar).Value = validacionbundle.usuariocreacion;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            Respuesta respuesta = new Respuesta();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));
                                respuesta.CodigoBundle = rdr.IsDBNull(rdr.GetOrdinal("CodigoBundle")) ? null : rdr.GetString(rdr.GetOrdinal("CodigoBundle"));
                                respuesta.Nombrepdffirma = rdr.IsDBNull(rdr.GetOrdinal("Nombrepdffirma")) ? null : rdr.GetString(rdr.GetOrdinal("Nombrepdffirma"));
                                // Puedes manejar múltiples filas si es necesario
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
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }

        public async Task<Respuesta> ValidarSubidaS3(int idventasdetalle)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    //using (SqlCommand cmd = new SqlCommand("USP_VALIDARSUBIDAS32", connection))
                    using (SqlCommand cmd = new SqlCommand("USP_VALIDARSUBIDAS3", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idventasdetalle", SqlDbType.Int).Value = idventasdetalle;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            Respuesta respuesta = new Respuesta();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));
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
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }

    }
}
