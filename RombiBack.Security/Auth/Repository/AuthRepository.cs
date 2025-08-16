using AutoMapper;
using RombiBack.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRestNetCore.DTO.DtoIncentivo;
using RombiBack.Security.Model.UserAuth;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;
using RombiBack.Security.Model.UserAuth.Modules;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos;
using System.Runtime.Intrinsics.Arm;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models;

namespace RombiBack.Security.Auth.Repsitory
{
    public class AuthRepository:IAuthRepository
    {

        private readonly DataAcces _dbConnection;

        public AuthRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<UserDTOResponse> RombiLoginMain(UserDTORequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_ROMBILOGIN", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idempresa", SqlDbType.Int).Value = request.idempresa; // Ajustar el tamaño del parámetro
                        command.Parameters.Add("@idpais", SqlDbType.Int).Value = request.idpais; // Ajustar el tamaño del parámetro
                        command.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = request.user;
                        command.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = request.password;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Acceso concedido
                                UserDTOResponse userAuth = new UserDTOResponse
                                {
                                    Resultado = reader["Resultado"] != DBNull.Value ? reader["Resultado"].ToString() : null,
                                    Accede = reader["Accede"] != DBNull.Value ? Convert.ToInt32(reader["Accede"]) : 0,
                                    Perfil = reader["Perfil"] != DBNull.Value ? reader["Perfil"].ToString() : null,
                                    idusuario = reader["idusuario"] != DBNull.Value ? Convert.ToInt32(reader["idusuario"]) : (int?)null,
                                    idusuarioromweb = reader["idusuarioromweb"] != DBNull.Value ? Convert.ToInt32(reader["idusuarioromweb"]) : (int?)null,
                                };
                                return userAuth;
                            }
                            else
                            {
                                // Acceso denegado
                                return new UserDTOResponse
                                {
                                    Resultado = reader["Resultado"].ToString(),
                                    Accede = Convert.ToInt32(reader["Accede"]),
                                    Perfil = reader["Perfil"].ToString(),
                                    idusuario = Convert.ToInt32(reader["idusuario"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error en ValidateUser: " + ex.Message);
                throw; // Lanzar excepción para que la capa superior maneje el error
            }
        }

        public async Task<UserDataDTOResponse> GetUserData(UserDTORequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETUSERDATA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@usuario", SqlDbType.Char).Value = request.user; // Ajustar el tamaño del parámetro
                      

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Acceso concedido
                                UserDataDTOResponse userAuth = new UserDataDTOResponse
                                {
                                    usuario = reader["usuario"].ToString(),
                                    nombres = reader["nombres"].ToString(),
                                    apellidopaterno = reader["apellidopaterno"].ToString(),
                                    apellidomaterno = reader["apellidomaterno"].ToString(),

                                };
                                return userAuth;
                            }
                            else
                            {
                                // Acceso denegado
                                return new UserDataDTOResponse
                                {
                                    usuario = reader["usuario"].ToString(),
                                    nombres = reader["nombres"].ToString(),
                                    apellidopaterno = reader["apellidopaterno"].ToString(),
                                    apellidomaterno = reader["apellidomaterno"].ToString(),
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error en ValidateUser: " + ex.Message);
                throw; // Lanzar excepción para que la capa superior maneje el error
            }
        }

        public async Task<List<BusinessAccountResponse>> GetBusinessUser(UserDTORequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETNEGOCIOUSER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PAISID", SqlDbType.Int).Value = request.idpais;
                        command.Parameters.Add("@EMPRESAID", SqlDbType.Int).Value = request.idempresa;
                        command.Parameters.Add("@USUARIO", SqlDbType.Char, 50).Value = request.user;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<BusinessAccountResponse> response = new List<BusinessAccountResponse>();

                                while (await reader.ReadAsync())
                                {
                                    BusinessAccountResponse businessAccount = new BusinessAccountResponse();
                                    businessAccount.idpais = reader.GetInt32(reader.GetOrdinal("idpais"));
                                    businessAccount.idnegocio = reader.GetInt32(reader.GetOrdinal("idnegocio"));
                                    businessAccount.nombrenegocio = reader.GetString(reader.GetOrdinal("nombrenegocio"));

                                    response.Add(businessAccount);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<BusinessAccountResponse>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }
        }

        public async Task<List<BusinessAccountResponse>> GetBusinessAccountUser(UserDTORequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETCUENTAUSER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EMPRESAID", SqlDbType.Int).Value = request.idempresa;
                        command.Parameters.Add("@COD_NEGOCIO", SqlDbType.Int).Value = request.idnegocio;
                        command.Parameters.Add("@USUARIO", SqlDbType.Char, 50).Value = request.user;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<BusinessAccountResponse> response = new List<BusinessAccountResponse>();

                                while (await reader.ReadAsync())
                                {
                                    BusinessAccountResponse businessAccount = new BusinessAccountResponse();
                                    businessAccount.idpais = reader.GetInt32(reader.GetOrdinal("idpais"));
                                    businessAccount.idnegocio = reader.GetInt32(reader.GetOrdinal("idnegocio"));
                                    businessAccount.nombrenegocio = reader.GetString(reader.GetOrdinal("nombrenegocio"));
                                    businessAccount.idcuenta = reader.GetInt32(reader.GetOrdinal("idcuenta"));
                                    businessAccount.nombrecuenta = reader.GetString(reader.GetOrdinal("nombrecuenta"));

                                    response.Add(businessAccount);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<BusinessAccountResponse>(); // Devuelve una lista vacía en lugar de null
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

        public async Task<List<ModuloDTOResponse>> GetPermissions(UserDTORequest request)
        {
            List<ModuloDTOResponse> permissions = new List<ModuloDTOResponse>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETMENUPERMISSIONSUSER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@IDPAIS", SqlDbType.Int).Value = request.idpais;
                        command.Parameters.Add("@IDEMPRESA", SqlDbType.Int).Value = request.idempresa;
                        command.Parameters.Add("@IDNEGOCIO", SqlDbType.Int).Value = request.idnegocio;
                        command.Parameters.Add("@IDCUENTA", SqlDbType.Int).Value = request.idcuenta;
                        command.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = request.user;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            Dictionary<int, ModuloDTOResponse> moduloDictionary = new Dictionary<int, ModuloDTOResponse>();

                            while (await reader.ReadAsync())
                            {
                                int idmodulo = reader.IsDBNull(reader.GetOrdinal("idmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("idmodulo"));

                                if (!moduloDictionary.ContainsKey(idmodulo))
                                {
                                    ModuloDTOResponse module = new ModuloDTOResponse
                                    {
                                        idmodulo = idmodulo,
                                        nombremodulo = reader.IsDBNull(reader.GetOrdinal("nombremodulo")) ? null : reader.GetString(reader.GetOrdinal("nombremodulo")),
                                        iconomodulo = reader.IsDBNull(reader.GetOrdinal("iconomodulo")) ? null : reader.GetString(reader.GetOrdinal("iconomodulo")),
                                        rutamodulo = reader.IsDBNull(reader.GetOrdinal("rutamodulo")) ? null : reader.GetString(reader.GetOrdinal("rutamodulo")),
                                        nivelmodulo = reader.IsDBNull(reader.GetOrdinal("nivelmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("nivelmodulo")),
                                        ordenmodulo = reader.IsDBNull(reader.GetOrdinal("ordenmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("ordenmodulo")),
                                        estadomodulo = reader.IsDBNull(reader.GetOrdinal("estadomodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("estadomodulo")),
                                        idperfilmodulo = reader.IsDBNull(reader.GetOrdinal("idperfilmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("idperfilsubmodulo")),
                                        nombreperfilmodulo = reader.IsDBNull(reader.GetOrdinal("nombreperfilmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombreperfilmodulo")),
                                        submodules = new List<SubModuloDTOResponse>()
                                    };

                                    moduloDictionary.Add(idmodulo, module);
                                }

                                SubModuloDTOResponse submodule = new SubModuloDTOResponse
                                {
                                    idsubmodulo = reader.IsDBNull(reader.GetOrdinal("idsubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("idsubmodulo")),
                                    nombresubmodulo = reader.IsDBNull(reader.GetOrdinal("nombresubmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombresubmodulo")),
                                    iconosubmodulo = reader.IsDBNull(reader.GetOrdinal("iconosubmodulo")) ? null : reader.GetString(reader.GetOrdinal("iconosubmodulo")),
                                    rutasubmodulo = reader.IsDBNull(reader.GetOrdinal("rutasubmodulo")) ? null : reader.GetString(reader.GetOrdinal("rutasubmodulo")),
                                    nivelsubmodulo = reader.IsDBNull(reader.GetOrdinal("nivelsubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("nivelsubmodulo")),
                                    ordensubmodulo = reader.IsDBNull(reader.GetOrdinal("ordensubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("ordensubmodulo")),
                                    estadosubmodulo = reader.IsDBNull(reader.GetOrdinal("estadosubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("estadosubmodulo")),
                                    idperfilsubmodulo = reader.IsDBNull(reader.GetOrdinal("idperfilsubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("idperfilsubmodulo")),
                                    nombreperfilsubmodulo = reader.IsDBNull(reader.GetOrdinal("nombreperfilsubmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombreperfilsubmodulo")),
                                    items = new List<ItemModuloDTOResponse>()
                                };

                                ItemModuloDTOResponse item = new ItemModuloDTOResponse
                                {
                                    iditemmodulo = reader.IsDBNull(reader.GetOrdinal("iditemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("iditemmodulo")),
                                    nombreitemmodulo = reader.IsDBNull(reader.GetOrdinal("nombreitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombreitemmodulo")),
                                    iconoitemmodulo = reader.IsDBNull(reader.GetOrdinal("iconoitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("iconoitemmodulo")),
                                    rutaitemmodulo = reader.IsDBNull(reader.GetOrdinal("rutaitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("rutaitemmodulo")),
                                    nivelitemmodulo = reader.IsDBNull(reader.GetOrdinal("nivelitemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("nivelitemmodulo")),
                                    ordenitemmodulo = reader.IsDBNull(reader.GetOrdinal("ordenitemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("ordenitemmodulo")),
                                    estadoitemmodulo = reader.IsDBNull(reader.GetOrdinal("estadoitemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("estadoitemmodulo")),
                                    idperfilitemmodulo = reader.IsDBNull(reader.GetOrdinal("idperfilitemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("idperfilitemmodulo")),
                                    nombreperfilitemmodulo = reader.IsDBNull(reader.GetOrdinal("nombreperfilitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombreperfilitemmodulo")),
                                };

                                ModuloDTOResponse currentModule = moduloDictionary[idmodulo];
                                var existingSubmodule = currentModule.submodules.FirstOrDefault(s => s.idsubmodulo == submodule.idsubmodulo);
                                if (existingSubmodule != null)
                                {
                                    existingSubmodule.items.Add(item);
                                }
                                else
                                {
                                    currentModule.submodules.Add(submodule);
                                    submodule.items.Add(item);
                                }
                            }

                            permissions.AddRange(moduloDictionary.Values);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }

            return permissions;
        }

        public async Task<IdCodigo> GetIdCodigo(CodigosRequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETIDEMPPAISNEGCUE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idpais", SqlDbType.Int, 15).Value = request.idpais;
                        command.Parameters.Add("@idempresa", SqlDbType.Int, 15).Value = request.idempresa;
                        command.Parameters.Add("@idnegocio", SqlDbType.Int, 15).Value = request.idnegocio;
                        command.Parameters.Add("@idcuenta", SqlDbType.Int, 15).Value = request.idcuenta;


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())  // Mueve el lector a la primera fila
                            {
                                IdCodigo idemppaisnegcue = new IdCodigo
                                {
                                    idemppaisnegcue = Convert.ToInt32(reader["idemppaisnegcue"]) // Asegúrate de que haya datos antes de leer
                                };
                                return idemppaisnegcue;
                            }
                            else
                            {
                                return null; // No se encontraron resultados, retorna null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error en Obtener Idcodigo: " + ex.Message);
                throw; // Lanzar excepción para que la capa superior maneje el error
            }
        }


        public async Task<IdCodigo> GetIdpdv(CodigosRequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETIDPDVROL", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = request.idemppaisnegcue;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar, 15).Value = request.usuario;
                


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())  // Mueve el lector a la primera fila
                            {
                                IdCodigo idpdv = new IdCodigo
                                {
                                    idpdv = Convert.ToInt32(reader["idpdv"]) // Asegúrate de que haya datos antes de leer
                                };
                                return idpdv;
                            }
                            else
                            {
                                return null; // No se encontraron resultados, retorna null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error en Obtener Idcodigo: " + ex.Message);
                throw; // Lanzar excepción para que la capa superior maneje el error
            }
        }

        

        public List<RETAIL_AsistenciaBE> GetMarcacionPromotor(string usuario)
        {


            List<RETAIL_AsistenciaBE> lista = new List<RETAIL_AsistenciaBE>();
            using (SqlConnection cn = new SqlConnection(_dbConnection.GetConnectionROMBI()))
            {
               
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM V_COBERTURA_PROMOTOR_ROMWEB_ROMBI WHERE DNI='" + usuario + "' AND FECHA='" + (DateTime.Today).ToString("yyyy-MM-dd") + "'", cn))
                {
                    try
                    {

                        cn.Open();
                        cmd.CommandTimeout = 0;
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                           
                            while (rdr.Read())
                            {
                                if (rdr.HasRows)
                                    lista.Add(new RETAIL_AsistenciaBE()
                                    {
                                        dteFeHoraIngreso = rdr["HoraIngreso"].ToString(),
                                        dteFeHorSalida = rdr["HoraSalida"].ToString(),

                                        intFlagAsistencia = Convert.ToInt32(rdr["FLAGASISTENCIA"].ToString()),
                                    });

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (cn.State == ConnectionState.Open) { cn.Close(); cn.Dispose(); }
                        cmd.Dispose();
                    }
                }

            }
            return lista;
        }





        public async Task<string> GetRutaPrincipal(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETRUTAPRINCIPAL", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return !reader.IsDBNull(reader.GetOrdinal("ruta"))
                                    ? reader.GetString(reader.GetOrdinal("ruta"))
                                    : string.Empty; // Retorna "" si el campo es NULL
                            }
                            return string.Empty; // Retorna "" si no hay registros
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en GetRutaPrincipal: " + ex.Message);
                throw;
            }
        }



        //public async Task<UserAuth> ValidateUser(UserDTORequest request)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionAPP_BI()))
        //        {
        //            await connection.OpenAsync();

        //            using (SqlCommand command = new SqlCommand("USP_ValidateUserRombi", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@CodPais", SqlDbType.VarChar, 50).Value = request.codpais;
        //                command.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = request.user;
        //                command.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = request.password;

        //                using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        await reader.ReadAsync();
        //                        return GetUserFromReader(reader);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción de manera adecuada
        //        // Registrar o lanzar la excepción según corresponda
        //        Console.WriteLine($"Error en ValidateUser: {ex.Message}");
        //    }

        //    return null; // Usuario no encontrado
        //}

        //private UserAuth GetUserFromReader(SqlDataReader reader)
        //{
        //    return new UserAuth
        //    {
        //        idusuario = reader.GetInt32(reader.GetOrdinal("IDUSUARIO")),
        //        nombres = reader.GetString(reader.GetOrdinal("NOMBRES")),
        //        apellidopaterno = reader.GetString(reader.GetOrdinal("APELLIDOPATERNO")),
        //        apellidomaterno = reader.GetString(reader.GetOrdinal("APELLIDOMATERNO")),
        //        idjerarquia = reader.GetInt32(reader.GetOrdinal("IDJERARQUIA")),
        //        correo = reader.GetString(reader.GetOrdinal("CORREO")),
        //        usuario = reader.GetString(reader.GetOrdinal("USUARIO")),
        //        clave = reader.GetString(reader.GetOrdinal("CLAVE")),
        //        cod_pais = reader.GetString(reader.GetOrdinal("COD_PAIS")),
        //        cod_negocio = reader.GetString(reader.GetOrdinal("COD_NEGOCIO")),
        //        cod_cuenta = reader.GetString(reader.GetOrdinal("COD_CUENTA")),
        //        es_admin = reader.GetString(reader.GetOrdinal("ES_ADMIN"))
        //    };
        //}




    }
}
