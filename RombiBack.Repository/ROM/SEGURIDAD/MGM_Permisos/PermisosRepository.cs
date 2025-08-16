using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Perfiles;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos;
using RombiBack.Security.Model.UserAuth;
using RombiBack.Security.Model.UserAuth.Modules;

namespace RombiBack.Repository.ROM.SEGURIDAD.MGM_Permisos
{
    public class PermisosRepository : IPermisosRepository
    {
        private readonly DataAcces _dbConnection;

        public PermisosRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<List<CodigosResponse>> GetCodigos(CodigosRequest request)
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
                            List<CodigosResponse> response = new List<CodigosResponse>();

                            while (await reader.ReadAsync())
                            {
                                CodigosResponse codigoResponse = new CodigosResponse
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
        public async Task<List<AllUsersResponse>> GetAllUsers(AllUsersRequest request)
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
                            List<AllUsersResponse> response = new List<AllUsersResponse>();

                            while (await reader.ReadAsync())
                            {
                                AllUsersResponse allusersResponse = new AllUsersResponse
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


        public async Task<List<Perfiles>> GetPerfiles()
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
                                List<Perfiles> response = new List<Perfiles>();

                                while (await reader.ReadAsync())
                                {
                                    Perfiles perf = new Perfiles();
                                    perf.idperfiles = reader.GetInt32(reader.GetOrdinal("idperfiles"));
                                    perf.nombre = reader.GetString(reader.GetOrdinal("nombre"));

                                    response.Add(perf);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<Perfiles>(); // Devuelve una lista vacía en lugar de null
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

        public async Task<List<Respuesta>> ValidarEstructuraModulos(List<PermisosModulosRequest> requests)
        {
            List<Respuesta> respuestas = new List<Respuesta>();

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
                                    Respuesta respuesta = new Respuesta
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


        public async Task<RespuestaUsuario> InsertarOActualizarUsuario(InsertarUsuario request)
        {
            var respuesta = new RespuestaUsuario();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_INSERTARACTUALIZARUSUARIO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@docusuario", request.docusuario);
                        command.Parameters.AddWithValue("@nombres", request.nombres);
                        command.Parameters.AddWithValue("@apellidopaterno", request.apellidopaterno);
                        command.Parameters.AddWithValue("@apellidomaterno", request.apellidomaterno);
                        command.Parameters.AddWithValue("@correopersonal", (object?)request.correopersonal ?? DBNull.Value);
                        command.Parameters.AddWithValue("@correocorp", (object?)request.correocorp ?? DBNull.Value);
                        command.Parameters.AddWithValue("@celular", (object?)request.celular ?? DBNull.Value);
                        command.Parameters.AddWithValue("@sexo", (object?)request.sexo ?? DBNull.Value);
                        command.Parameters.AddWithValue("@fechanacimiento", (object?)request.fechanacimiento ?? DBNull.Value);
                        command.Parameters.AddWithValue("@direccion", (object?)request.direccion ?? DBNull.Value);
                        command.Parameters.AddWithValue("@usuario", request.usuario);
                        command.Parameters.AddWithValue("@clave", request.clave);
                        command.Parameters.AddWithValue("@idemppaisnegcue", request.idemppaisnegcue);
                        command.Parameters.AddWithValue("@estado", request.estado);
                        command.Parameters.AddWithValue("@fechaingreso", (object?)request.fechaingreso ?? DBNull.Value);
                        command.Parameters.AddWithValue("@usuariocreacion", request.usuariocreacion);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                respuesta.Estado = reader["Estado"]?.ToString();
                                respuesta.Mensaje = reader["Mensaje"]?.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Estado = "ERROR";
                respuesta.Mensaje = "Excepción: " + ex.Message;
            }

            return respuesta;
        }

        public async Task<object> GetUsuarioPorDocumento(string docusuario, string nombresocio)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETUSUARIOXDOC", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@docusuario", docusuario);
                        command.Parameters.AddWithValue("@nombresocio", nombresocio);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new RespuestaUsuarioXDocumento
                                {
                                    idusuario = reader.GetInt32(reader.GetOrdinal("idusuario")),
                                    docusuario = reader["docusuario"]?.ToString(),
                                    nombres = reader["nombres"]?.ToString(),
                                    apellidopaterno = reader["apellidopaterno"]?.ToString(),
                                    apellidomaterno = reader["apellidomaterno"]?.ToString(),
                                    correopersonal = reader["correopersonal"]?.ToString(),
                                    correocorp = reader["correocorp"]?.ToString(),
                                    celular = reader["celular"]?.ToString(),
                                    sexo = reader["sexo"]?.ToString(),
                                    fechanacimiento = reader["fechanacimiento"] != DBNull.Value
                                        ? (DateTime?)reader.GetDateTime(reader.GetOrdinal("fechanacimiento"))
                                        : null,
                                    direccion = reader["direccion"]?.ToString(),
                                    usuario = reader["usuario"]?.ToString(),
                                    clave = reader["clave"]?.ToString(),
                                    idemppaisnegcue = Convert.ToInt32(reader["idemppaisnegcue"]),
                                    estado = Convert.ToInt32(reader["estado"]),
                                    fechaingreso = reader["fechaingreso"] != DBNull.Value
                                        ? (DateTime?)reader.GetDateTime(reader.GetOrdinal("fechaingreso"))
                                        : null
                                };
                            }
                        }
                    }

                    // No se encontró resultado
                    return new RespuestaUsuario
                    {
                        Estado = "ERROR",
                        Mensaje = "No se encontró el usuario con los datos proporcionados."
                    };
                }
            }
            catch (Exception ex)
            {
                return new RespuestaUsuario
                {
                    Estado = "ERROR",
                    Mensaje = "Excepción: " + ex.Message
                };
            }
        }

        public async Task<List<RespuestaValidarUsuario>> ValidarUsuariosAsync(List<ListaValidarUsuario> usuarios)
        {
            var resultado = new List<RespuestaValidarUsuario>();

            using (var connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
            using (var command = new SqlCommand("USP_VALIDARUSUARIOS", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Crear DataTable como TVP
                var table = new DataTable();
                table.Columns.Add("docusuario", typeof(string));
                table.Columns.Add("nombres", typeof(string));
                table.Columns.Add("apellidopaterno", typeof(string));
                table.Columns.Add("apellidomaterno", typeof(string));
                table.Columns.Add("correopersonal", typeof(string));
                table.Columns.Add("correocorp", typeof(string));
                table.Columns.Add("celular", typeof(string));
                table.Columns.Add("sexo", typeof(string));
                table.Columns.Add("fechanacimiento", typeof(DateTime));
                table.Columns.Add("direccion", typeof(string));
                table.Columns.Add("usuario", typeof(string));
                table.Columns.Add("fechaingreso", typeof(DateTime));
                table.Columns.Add("idemppaisnegcue", typeof(int));
                table.Columns.Add("uuser", typeof(string));
                table.Columns.Add("clave", typeof(string));

                foreach (var u in usuarios)
                {
                    table.Rows.Add(
                        u.docusuario,
                        u.nombres,
                        u.apellidopaterno,
                        u.apellidomaterno,
                        u.correopersonal,
                        u.correocorp,
                        u.celular,
                        u.sexo,
                        u.fechanacimiento ?? (object)DBNull.Value,
                        u.direccion,
                        u.usuario,
                        u.fechaingreso ?? (object)DBNull.Value,
                        u.idemppaisnegcue,
                        u.uuser,
                        u.clave
                    );
                }

                var param = command.Parameters.AddWithValue("@Usuarios", table);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "dbo.UsuarioValidarType";

                await connection.OpenAsync();

                try
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resultado.Add(new RespuestaValidarUsuario
                            {
                                docusuario = reader["docusuario"]?.ToString(),
                                estado = reader["estado"]?.ToString(),
                                mensaje = reader["mensaje"]?.ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Puedes loguearlo, lanzarlo o transformarlo en un error controlado
                    throw new Exception("Error al ejecutar el procedimiento almacenado USP_VALIDARUSUARIOS: " + ex.Message, ex);
                }
            }

            return resultado;
        }

        public async Task<List<RespuestaValidarUsuario>> InsertarUsuariosMasivoAsync(List<ListaValidarUsuario> usuarios)
        {
            var resultado = new List<RespuestaValidarUsuario>();

            try
            {
                using (var connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (var command = new SqlCommand("USP_INSERTARACTUALIZARUSUARIOS_MASIVO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Crear DataTable con columnas en el MISMO ORDEN que el TYPE dbo.UsuarioValidarType
                    var table = new DataTable();
                    table.Columns.Add("docusuario", typeof(string));
                    table.Columns.Add("nombres", typeof(string));
                    table.Columns.Add("apellidopaterno", typeof(string));
                    table.Columns.Add("apellidomaterno", typeof(string));
                    table.Columns.Add("correopersonal", typeof(string));
                    table.Columns.Add("correocorp", typeof(string));
                    table.Columns.Add("celular", typeof(string));
                    table.Columns.Add("sexo", typeof(string));
                    table.Columns.Add("fechanacimiento", typeof(DateTime));
                    table.Columns.Add("direccion", typeof(string));
                    table.Columns.Add("usuario", typeof(string)); // este es usuariocreacion/modificacion
                    table.Columns.Add("fechaingreso", typeof(DateTime));
                    table.Columns.Add("idemppaisnegcue", typeof(int));
                    table.Columns.Add("uuser", typeof(string)); // este va en campo usuario de la tabla
                    table.Columns.Add("clave", typeof(string));

                    foreach (var u in usuarios)
                    {
                        try
                        {
                            table.Rows.Add(
                                u.docusuario,
                                u.nombres,
                                u.apellidopaterno,
                                u.apellidomaterno,
                                u.correopersonal ?? (object)DBNull.Value,
                                u.correocorp ?? (object)DBNull.Value,
                                u.celular ?? (object)DBNull.Value,
                                u.sexo ?? (object)DBNull.Value,
                                u.fechanacimiento.HasValue ? (object)u.fechanacimiento.Value : DBNull.Value,
                                u.direccion ?? (object)DBNull.Value,
                                u.usuario, // este es el usuario que modifica o crea
                                u.fechaingreso.HasValue ? (object)u.fechaingreso.Value : DBNull.Value,
                                u.idemppaisnegcue,
                                u.uuser, // este es el nombre de usuario a registrar en tabla usuario
                                u.clave
                            );
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Error al insertar fila en DataTable: {ex.Message}. Datos: {JsonConvert.SerializeObject(u)}");
                        }
                    }

                    // Agregar parámetro de tipo TVP
                    var param = command.Parameters.AddWithValue("@Usuarios", table);
                    param.SqlDbType = SqlDbType.Structured;
                    param.TypeName = "dbo.UsuarioValidarType";

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resultado.Add(new RespuestaValidarUsuario
                            {
                                docusuario = reader["docusuario"]?.ToString(),
                                estado = reader["estado"]?.ToString(),
                                mensaje = reader["mensaje"]?.ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Add(new RespuestaValidarUsuario
                {
                    docusuario = null,
                    estado = "Error",
                    mensaje = $"Error al ejecutar el procedimiento almacenado: {ex.Message}"
                });
            }

            return resultado;
        }
    }
}
