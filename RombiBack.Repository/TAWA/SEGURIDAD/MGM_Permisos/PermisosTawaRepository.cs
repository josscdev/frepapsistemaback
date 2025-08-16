using RombiBack.Abstraction;
using RombiBack.Security.Model.UserAuth.Modules;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.TAWA.SEGURIDAD.Models.Permisos;
using RombiBack.Entities.TAWA.SEGURIDAD.Models.Perfiles;

namespace RombiBack.Repository.TAWA.SEGURIDAD.MGM_Permisos
{
    public class PermisosTawaRepository : IPermisosTawaRepository
    {
        private readonly DataAcces _dbConnection;

        public PermisosTawaRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<CodigosResponseTawa>> GetCodigos(CodigosRequestTawa request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETCODIGOS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idempresa", SqlDbType.Int).Value = request.idempresa;
                        command.Parameters.Add("@idpais", SqlDbType.Int).Value = request.idpais;
                        command.Parameters.Add("@idnegocio", SqlDbType.Int).Value = request.idnegocio ?? (object)DBNull.Value;
                        command.Parameters.Add("@idcuenta", SqlDbType.Int).Value = request.idcuenta ?? (object)DBNull.Value;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<CodigosResponseTawa> response = new List<CodigosResponseTawa>();

                            while (await reader.ReadAsync())
                            {
                                CodigosResponseTawa codigoResponse = new CodigosResponseTawa
                                {
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idemppaisnegcue")) : 0,
                                    idempresa = reader["idempresa"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idempresa")) : 0,
                                    nombreempresa = reader["nombreempresa"]?.ToString() ?? "",
                                    idpais = reader["idpais"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idpais")) : 0,
                                    nombrepais = reader["nombrepais"]?.ToString() ?? "",
                                    idnegocio = reader["idnegocio"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idnegocio")) : 0,
                                    nombrenegocio = reader["nombrenegocio"]?.ToString() ?? "",
                                    idcuenta = reader["idcuenta"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idcuenta")) : 0,
                                    nombrecuenta = reader["nombrecuenta"]?.ToString() ?? "",
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(codigoResponse);
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
        public async Task<List<AllUsersResponseTawa>> GetAllUsers(AllUsersRequestTawa request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETALLUSERS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idempresa", SqlDbType.Int).Value = request.idempresa ?? (object)DBNull.Value;
                        command.Parameters.Add("@idpais", SqlDbType.Int).Value = request.idpais ?? (object)DBNull.Value;
                        command.Parameters.Add("@idnegocio", SqlDbType.Int).Value = request.idnegocio ?? (object)DBNull.Value;
                        command.Parameters.Add("@idcuenta", SqlDbType.Int).Value = request.idcuenta ?? (object)DBNull.Value;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = request.usuario ?? (object)DBNull.Value;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<AllUsersResponseTawa> response = new List<AllUsersResponseTawa>();

                            while (await reader.ReadAsync())
                            {
                                AllUsersResponseTawa allusersResponse = new AllUsersResponseTawa
                                {
                                    idusuario = reader.GetInt32(reader.GetOrdinal("idusuario")),
                                    docusuario = reader.GetString(reader.GetOrdinal("docusuario")),
                                    nombrecompleto = reader.GetString(reader.GetOrdinal("nombrecompleto")),
                                    nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                    apellidopaterno = reader.GetString(reader.GetOrdinal("apellidopaterno")),
                                    apellidomaterno = reader.GetString(reader.GetOrdinal("apellidomaterno")),
                                    correo = reader.GetString(reader.GetOrdinal("correo")),
                                    usuario = reader.GetString(reader.GetOrdinal("usuario")),
                                    idempresa = reader.GetInt32(reader.GetOrdinal("idempresa")),
                                    nombreempresa = reader.GetString(reader.GetOrdinal("nombreempresa")),
                                    idpais = reader.GetInt32(reader.GetOrdinal("idpais")),
                                    nombrepais = reader.GetString(reader.GetOrdinal("nombrepais")),
                                    idnegocio = reader.GetInt32(reader.GetOrdinal("idnegocio")),
                                    nombrenegocio = reader.GetString(reader.GetOrdinal("nombrenegocio")),
                                    idcuenta = reader.GetInt32(reader.GetOrdinal("idcuenta")),
                                    nombrecuenta = reader.GetString(reader.GetOrdinal("nombrecuenta")),
                                    estado = reader.GetInt32(reader.GetOrdinal("estado"))
                                };

                                response.Add(allusersResponse);
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


        public async Task<List<ModuloDTOResponse>> GetModulosPermisos(UserDTORequest request)
        {
            List<ModuloDTOResponse> permissions = new List<ModuloDTOResponse>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETMODULOSPERMISOS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@IDPAIS", SqlDbType.VarChar, 50).Value = request.idpais;
                        command.Parameters.Add("@IDEMPRESA", SqlDbType.VarChar, 50).Value = request.idempresa;
                        command.Parameters.Add("@IDNEGOCIO", SqlDbType.VarChar, 50).Value = request.idnegocio;
                        command.Parameters.Add("@IDCUENTA", SqlDbType.VarChar, 50).Value = request.idcuenta;
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
                                        idcodmod = reader.IsDBNull(reader.GetOrdinal("idcodmod")) ? null : reader.GetInt32(reader.GetOrdinal("idcodmod")),

                                        nombremodulo = reader.IsDBNull(reader.GetOrdinal("nombremodulo")) ? null : reader.GetString(reader.GetOrdinal("nombremodulo")),
                                        estadomodulopermiso = reader.IsDBNull(reader.GetOrdinal("estadomodulopermiso")) ? null : reader.GetString(reader.GetOrdinal("estadomodulopermiso")),
                                        idperfilmodulo = reader.IsDBNull(reader.GetOrdinal("idperfilmodulo")) ? null : reader.GetInt32(reader.GetOrdinal("idperfilmodulo")),
                                        rutamodulo = reader.IsDBNull(reader.GetOrdinal("rutamodulo")) ? null : reader.GetString(reader.GetOrdinal("rutamodulo")),

                                        submodules = new List<SubModuloDTOResponse>()
                                    };

                                    moduloDictionary.Add(idmodulo, module);
                                }

                                SubModuloDTOResponse submodule = new SubModuloDTOResponse
                                {
                                    idsubmodulo = reader.IsDBNull(reader.GetOrdinal("idsubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("idsubmodulo")),
                                    idcodmodsubmod = reader.IsDBNull(reader.GetOrdinal("idcodmodsubmod")) ? null : reader.GetInt32(reader.GetOrdinal("idcodmodsubmod")),

                                    nombresubmodulo = reader.IsDBNull(reader.GetOrdinal("nombresubmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombresubmodulo")),
                                    estadosubmodulopermiso = reader.IsDBNull(reader.GetOrdinal("estadosubmodulopermiso")) ? null : reader.GetString(reader.GetOrdinal("estadosubmodulopermiso")),
                                    idperfilsubmodulo = reader.IsDBNull(reader.GetOrdinal("idperfilsubmodulo")) ? null : reader.GetInt32(reader.GetOrdinal("idperfilsubmodulo")),
                                    rutasubmodulo = reader.IsDBNull(reader.GetOrdinal("rutasubmodulo")) ? null : reader.GetString(reader.GetOrdinal("rutasubmodulo")),

                                    items = new List<ItemModuloDTOResponse>()
                                };

                                ItemModuloDTOResponse item = new ItemModuloDTOResponse
                                {
                                    iditemmodulo = reader.IsDBNull(reader.GetOrdinal("iditemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("iditemmodulo")),
                                    idcodmodsubmoditemmod = reader.IsDBNull(reader.GetOrdinal("idcodmodsubmoditemmod")) ? null : reader.GetInt32(reader.GetOrdinal("idcodmodsubmoditemmod")),

                                    nombreitemmodulo = reader.IsDBNull(reader.GetOrdinal("nombreitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombreitemmodulo")),
                                    estadoitemmodulopermiso = reader.IsDBNull(reader.GetOrdinal("estadoitemmodulopermiso")) ? null : reader.GetString(reader.GetOrdinal("estadoitemmodulopermiso")),
                                    idperfilitemmodulo = reader.IsDBNull(reader.GetOrdinal("idperfilitemmodulo")) ? null : reader.GetInt32(reader.GetOrdinal("idperfilitemmodulo")),
                                    rutaitemmodulo = reader.IsDBNull(reader.GetOrdinal("rutaitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("rutaitemmodulo")),

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


        public async Task<List<PerfilesTawa>> GetPerfiles()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETPERFILES", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<PerfilesTawa> response = new List<PerfilesTawa>();

                                while (await reader.ReadAsync())
                                {
                                    PerfilesTawa perf = new PerfilesTawa();
                                    perf.idperfiles = reader.GetInt32(reader.GetOrdinal("idperfiles"));
                                    perf.nombre = reader.GetString(reader.GetOrdinal("nombre"));

                                    response.Add(perf);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<PerfilesTawa>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<RespuestaTawa>> ValidarEstructuraModulos(List<PermisosModulosRequestTawa> requests)
        {
            List<RespuestaTawa> respuestas = new List<RespuestaTawa>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    foreach (var request in requests)
                    {
                        using (SqlCommand command = new SqlCommand("USP_VALIDARESTRUCTURAMODULOS", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@idcodmod", SqlDbType.Int).Value = request.idcodmod;
                            command.Parameters.Add("@idcodmodsubmod", SqlDbType.Int).Value = request.idcodmodsubmod ?? (object)DBNull.Value;
                            command.Parameters.Add("@idcodmodsubmoditemmod", SqlDbType.Int).Value = request.idcodmodsubmoditemmod ?? (object)DBNull.Value;
                            command.Parameters.Add("@idperfiles", SqlDbType.Int).Value = request.idperfiles;
                            command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = request.usuario;
                            command.Parameters.Add("@idempresa", SqlDbType.Int).Value = request.idempresa;
                            command.Parameters.Add("@idpais", SqlDbType.Int).Value = request.idpais;
                            command.Parameters.Add("@idnegocio", SqlDbType.Int).Value = request.idnegocio ?? (object)DBNull.Value;
                            command.Parameters.Add("@idcuenta", SqlDbType.Int).Value = request.idcuenta ?? (object)DBNull.Value;
                            command.Parameters.Add("@checks", SqlDbType.Int).Value = request.checks;
                            command.Parameters.Add("@usuario_creacion", SqlDbType.VarChar).Value = request.usuario_creacion;

                            using (SqlDataReader rdr = await command.ExecuteReaderAsync())
                            {
                                while (await rdr.ReadAsync())
                                {
                                    RespuestaTawa respuesta = new RespuestaTawa
                                    {
                                        Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje")),
                                        NombreModulos = rdr.GetString(rdr.GetOrdinal("NombreModulos")),
                                        NombrePerfil = rdr.GetString(rdr.GetOrdinal("NombrePerfil"))
                                    };

                                    respuestas.Add(respuesta);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }

            return respuestas;
        }
    }
}
