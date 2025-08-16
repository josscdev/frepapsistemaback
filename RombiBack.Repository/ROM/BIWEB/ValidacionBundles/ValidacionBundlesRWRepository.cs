using RombiBack.Abstraction;
using RombiBack.Entities.ROM.BIWEB.ValidacionBundles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.BIWEB.ValidacionBundles
{
    public class ValidacionBundlesRWRepository : IValidacionBundlesRWRepository
    {
        private readonly DataAcces _dbConnection;

        public ValidacionBundlesRWRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ValidacionBundlesRW> GetBundlesVentas(int intIdVentasPrincipal)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_GETBUNDLESVENTAS2", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@intIdVentasPrincipal", SqlDbType.Int).Value = intIdVentasPrincipal;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            ValidacionBundlesRW respuesta = new ValidacionBundlesRW();
                            while (await reader.ReadAsync())
                            {

                                respuesta.intidventasprincipal = reader.GetInt32(reader.GetOrdinal("intidventasprincipal"));
                                respuesta.intventasromid = reader.GetInt32(reader.GetOrdinal("intventasromid"));
                                respuesta.strdtevestasromfeope = reader.GetString(reader.GetOrdinal("dtevestasromfeope"));
                                respuesta.strdnicliente = reader.GetString(reader.GetOrdinal("strdnicliente"));
                                respuesta.strcelularcontrato = reader.GetString(reader.GetOrdinal("strcelularcontrato"));
                                respuesta.strventasromusucr = reader.GetString(reader.GetOrdinal("dnipromotor"));
                                respuesta.nombrepromotor = reader.GetString(reader.GetOrdinal("nombrepromotor"));
                                respuesta.dnipromotor = reader.GetString(reader.GetOrdinal("dnipromotor"));
                                respuesta.intproductoid = reader.GetInt32(reader.GetOrdinal("intproductoid"));
                                respuesta.strproductodesc = reader.GetString(reader.GetOrdinal("strproductodesc"));
                                respuesta.intplanid = reader.GetInt32(reader.GetOrdinal("intplanid"));
                                respuesta.strplandesc = reader.GetString(reader.GetOrdinal("strplandesc"));
                                respuesta.intmodeloequipoid = reader.GetInt32(reader.GetOrdinal("intmodeloequipoid"));
                                respuesta.strmodeloequipodesc = reader.GetString(reader.GetOrdinal("strmodeloequipodesc"));
                                respuesta.intbundleid = reader.GetInt32(reader.GetOrdinal("intbundleid"));
                                respuesta.codigo = reader.GetString(reader.GetOrdinal("codigo"));
                                respuesta.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                //respuesta.strcodigoauthbundle = reader.GetString(reader.GetOrdinal("strcodigoauthbundle"));
                                respuesta.dteventasromfecre = reader.GetString(reader.GetOrdinal("dteventasromfecre"));
                                respuesta.strnumorden = reader.GetString(reader.GetOrdinal("strnumorden"));
                                respuesta.idpuntoventa = reader.GetInt32(reader.GetOrdinal("idpuntoventa"));
                                respuesta.puntoventa = reader.GetString(reader.GetOrdinal("puntoventa"));
                                respuesta.flagcodigoauthbundle = reader.GetInt32(reader.GetOrdinal("flagcodigoauthbundle"));

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

        public async Task<RespuestaRW> ValidarCodigoAuthBundle(int intventasromid, string strcodigoauthbundle)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_VALIDARCODIGOAUTHBUNDLE2", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@intventasromid", SqlDbType.VarChar).Value = intventasromid;
                        cmd.Parameters.Add("@strcodigoauthbundle", SqlDbType.VarChar).Value = strcodigoauthbundle;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            RespuestaRW respuesta = new RespuestaRW();
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

        public async Task<RespuestaRW> PostBundlesFirma(ValidacionBundlesRW validacionbundle)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_POSTBUNDLESFIRMA2", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@intidventasprincipal", SqlDbType.Int).Value = validacionbundle.intidventasprincipal;
                        cmd.Parameters.Add("@intventasromid", SqlDbType.Int).Value = validacionbundle.intventasromid;
                        cmd.Parameters.Add("@dnipromotor", SqlDbType.VarChar).Value = validacionbundle.dnipromotor;
                        cmd.Parameters.Add("@strdnicliente", SqlDbType.VarChar).Value = validacionbundle.strdnicliente;
                        cmd.Parameters.Add("@intbundleid", SqlDbType.Int).Value = validacionbundle.intbundleid;
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = validacionbundle.codigo;
                        cmd.Parameters.Add("@nombreobsequio", SqlDbType.VarChar).Value = validacionbundle.nombreobsequio ?? (object)DBNull.Value;
                        cmd.Parameters.Add("@usuario_creacion", SqlDbType.VarChar).Value = validacionbundle.usuario_creacion;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            RespuestaRW respuesta = new RespuestaRW();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));
                                respuesta.Codigo = rdr.IsDBNull(rdr.GetOrdinal("Codigo")) ? null : rdr.GetString(rdr.GetOrdinal("Codigo"));
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

        public async Task<RespuestaRW> ValidarSubidaS3(int intventasromid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_VALIDARSUBIDAS32", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@intventasromid", SqlDbType.Int).Value = intventasromid;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            RespuestaRW respuesta = new RespuestaRW();
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
