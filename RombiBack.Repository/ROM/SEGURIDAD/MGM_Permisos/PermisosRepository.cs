using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
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
                // OJO: usa tu cadena de conexión a Postgres
                // Ej.: Host=...;Port=5432;Database=...;Username=...;Password=...
                using (var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    // En Postgres las "SP" las migramos a funciones: se invocan con SELECT
                    const string sql = @"
                SELECT *
                FROM intranet.usp_getcodigos(@idempresa, @idpais, @idnegocio, @idcuenta);";

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        // Parámetros requeridos
                        command.Parameters.AddWithValue("idempresa", NpgsqlDbType.Integer, request.idempresa);
                        command.Parameters.AddWithValue("idpais", NpgsqlDbType.Integer, request.idpais);

                        // Parámetros opcionales (NULL cuando no vienen)
                        command.Parameters.Add("idnegocio", NpgsqlDbType.Integer).Value =
                            (object?)request.idnegocio ?? DBNull.Value;

                        command.Parameters.Add("idcuenta", NpgsqlDbType.Integer).Value =
                            (object?)request.idcuenta ?? DBNull.Value;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var response = new List<CodigosResponse>();

                            while (await reader.ReadAsync())
                            {
                                var item = new CodigosResponse
                                {
                                    idemppaisnegcue = reader.IsDBNull(reader.GetOrdinal("idemppaisnegcue")) ? 0 : reader.GetInt32(reader.GetOrdinal("idemppaisnegcue")),
                                    idempresa = reader.IsDBNull(reader.GetOrdinal("idempresa")) ? 0 : reader.GetInt32(reader.GetOrdinal("idempresa")),
                                    nombreempresa = reader["nombreempresa"] as string ?? string.Empty,
                                    idpais = reader.IsDBNull(reader.GetOrdinal("idpais")) ? 0 : reader.GetInt32(reader.GetOrdinal("idpais")),
                                    nombrepais = reader["nombrepais"] as string ?? string.Empty,
                                    idnegocio = reader.IsDBNull(reader.GetOrdinal("idnegocio")) ? 0 : reader.GetInt32(reader.GetOrdinal("idnegocio")),
                                    nombrenegocio = reader["nombrenegocio"] as string ?? string.Empty,
                                    idcuenta = reader.IsDBNull(reader.GetOrdinal("idcuenta")) ? 0 : reader.GetInt32(reader.GetOrdinal("idcuenta")),
                                    nombrecuenta = reader["nombrecuenta"] as string ?? string.Empty,
                                    estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("estado"))
                                };

                                response.Add(item);
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
                using (var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    // Postgres: llamar la función como SELECT
                    const string sql = @"
                SELECT *
                FROM intranet.usp_getallusers(
                    @idempresa, @idpais, @idnegocio, @idcuenta, @usuario
                );";

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        // parámetros, cuidando NULL
                        command.Parameters.Add("idempresa", NpgsqlDbType.Integer).Value =
                            (object?)request.idempresa ?? DBNull.Value;

                        command.Parameters.Add("idpais", NpgsqlDbType.Integer).Value =
                            (object?)request.idpais ?? DBNull.Value;

                        command.Parameters.Add("idnegocio", NpgsqlDbType.Integer).Value =
                            (object?)request.idnegocio ?? DBNull.Value;

                        command.Parameters.Add("idcuenta", NpgsqlDbType.Integer).Value =
                            (object?)request.idcuenta ?? DBNull.Value;

                        command.Parameters.Add("usuario", NpgsqlDbType.Varchar).Value =
                            (object?)request.usuario ?? DBNull.Value;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var response = new List<AllUsersResponse>();

                            while (await reader.ReadAsync())
                            {
                                var item = new AllUsersResponse
                                {
                                    idusuario = reader.GetInt32(reader.GetOrdinal("idusuario")),
                                    docusuario = reader["docusuario"]?.ToString() ?? "",
                                    nombrecompleto = reader["nombrecompleto"]?.ToString() ?? "",
                                    nombres = reader["nombres"]?.ToString() ?? "",
                                    apellidopaterno = reader["apellidopaterno"]?.ToString() ?? "",
                                    apellidomaterno = reader["apellidomaterno"]?.ToString() ?? "",
                                    correo = reader["correo"]?.ToString() ?? "",
                                    usuario = reader["usuario_out"]?.ToString() ?? "",   // ⚠️ en la función lo aliaste "usuario_out"
                                    idempresa = reader.GetInt32(reader.GetOrdinal("idempresa_out")),
                                    nombreempresa = reader["nombreempresa"]?.ToString() ?? "",
                                    idpais = reader.GetInt32(reader.GetOrdinal("idpais_out")),
                                    nombrepais = reader["nombrepais"]?.ToString() ?? "",
                                    idnegocio = reader.GetInt32(reader.GetOrdinal("idnegocio_out")),
                                    nombrenegocio = reader["nombrenegocio"]?.ToString() ?? "",
                                    idcuenta = reader.GetInt32(reader.GetOrdinal("idcuenta_out")),
                                    nombrecuenta = reader["nombrecuenta"]?.ToString() ?? "",
                                    estado = reader.GetInt32(reader.GetOrdinal("estado"))
                                };

                                response.Add(item);
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
            var permissions = new List<ModuloDTOResponse>();

            try
            {
                using (var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    const string sql = @"
                SELECT *
                FROM intranet.usp_getmodulospermisos(
                    @idpais, @idempresa, @idnegocio, @idcuenta, @usuario
                );";

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.Add("idpais", NpgsqlDbType.Integer).Value = request.idpais;
                        command.Parameters.Add("idempresa", NpgsqlDbType.Integer).Value = request.idempresa;
                        command.Parameters.Add("idnegocio", NpgsqlDbType.Integer).Value = request.idnegocio;
                        command.Parameters.Add("idcuenta", NpgsqlDbType.Integer).Value = request.idcuenta;
                        command.Parameters.Add("usuario", NpgsqlDbType.Varchar).Value = request.user;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var moduloDictionary = new Dictionary<int, ModuloDTOResponse>();

                            while (await reader.ReadAsync())
                            {
                                int idmodulo = reader.IsDBNull(reader.GetOrdinal("idmodulo"))
                                    ? 0 : reader.GetInt32(reader.GetOrdinal("idmodulo"));

                                if (!moduloDictionary.ContainsKey(idmodulo))
                                {
                                    var module = new ModuloDTOResponse
                                    {
                                        idmodulo = idmodulo,
                                        idcodmod = reader.IsDBNull(reader.GetOrdinal("idcodmod"))
                                            ? (int?)null : reader.GetInt32(reader.GetOrdinal("idcodmod")),
                                        nombremodulo = reader.IsDBNull(reader.GetOrdinal("nombremodulo"))
                                            ? null : reader.GetString(reader.GetOrdinal("nombremodulo")),
                                        estadomodulopermiso = reader.IsDBNull(reader.GetOrdinal("estadomodulopermiso"))
                                            ? null : reader.GetString(reader.GetOrdinal("estadomodulopermiso")),
                                        idperfilmodulo = reader.IsDBNull(reader.GetOrdinal("idperfilmodulo"))
                                            ? (int?)null : reader.GetInt32(reader.GetOrdinal("idperfilmodulo")),
                                        rutamodulo = reader.IsDBNull(reader.GetOrdinal("rutamodulo"))
                                            ? null : reader.GetString(reader.GetOrdinal("rutamodulo")),
                                        submodules = new List<SubModuloDTOResponse>()
                                    };

                                    moduloDictionary.Add(idmodulo, module);
                                }

                                var submodule = new SubModuloDTOResponse
                                {
                                    idsubmodulo = reader.IsDBNull(reader.GetOrdinal("idsubmodulo"))
                                        ? 0 : reader.GetInt32(reader.GetOrdinal("idsubmodulo")),
                                    idcodmodsubmod = reader.IsDBNull(reader.GetOrdinal("idcodmodsubmod"))
                                        ? (int?)null : reader.GetInt32(reader.GetOrdinal("idcodmodsubmod")),
                                    nombresubmodulo = reader.IsDBNull(reader.GetOrdinal("nombresubmodulo"))
                                        ? null : reader.GetString(reader.GetOrdinal("nombresubmodulo")),
                                    estadosubmodulopermiso = reader.IsDBNull(reader.GetOrdinal("estadosubmodulopermiso"))
                                        ? null : reader.GetString(reader.GetOrdinal("estadosubmodulopermiso")),
                                    idperfilsubmodulo = reader.IsDBNull(reader.GetOrdinal("idperfilsubmodulo"))
                                        ? (int?)null : reader.GetInt32(reader.GetOrdinal("idperfilsubmodulo")),
                                    rutasubmodulo = reader.IsDBNull(reader.GetOrdinal("rutasubmodulo"))
                                        ? null : reader.GetString(reader.GetOrdinal("rutasubmodulo")),
                                    items = new List<ItemModuloDTOResponse>()
                                };

                                var item = new ItemModuloDTOResponse
                                {
                                    iditemmodulo = reader.IsDBNull(reader.GetOrdinal("iditemmodulo"))
                                        ? 0 : reader.GetInt32(reader.GetOrdinal("iditemmodulo")),
                                    idcodmodsubmoditemmod = reader.IsDBNull(reader.GetOrdinal("idcodmodsubmoditemmod"))
                                        ? (int?)null : reader.GetInt32(reader.GetOrdinal("idcodmodsubmoditemmod")),
                                    nombreitemmodulo = reader.IsDBNull(reader.GetOrdinal("nombreitemmodulo"))
                                        ? null : reader.GetString(reader.GetOrdinal("nombreitemmodulo")),
                                    estadoitemmodulopermiso = reader.IsDBNull(reader.GetOrdinal("estadoitemmodulopermiso"))
                                        ? null : reader.GetString(reader.GetOrdinal("estadoitemmodulopermiso")),
                                    idperfilitemmodulo = reader.IsDBNull(reader.GetOrdinal("idperfilitemmodulo"))
                                        ? (int?)null : reader.GetInt32(reader.GetOrdinal("idperfilitemmodulo")),
                                    rutaitemmodulo = reader.IsDBNull(reader.GetOrdinal("rutaitemmodulo"))
                                        ? null : reader.GetString(reader.GetOrdinal("rutaitemmodulo"))
                                };

                                var currentModule = moduloDictionary[idmodulo];
                                var existingSubmodule = currentModule.submodules
                                    .FirstOrDefault(s => s.idsubmodulo == submodule.idsubmodulo);

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
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }

            return permissions;
        }


        public async Task<List<Perfiles>> GetPerfiles()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    const string sql = @"SELECT idperfiles, nombre FROM intranet.usp_getperfiles();";

                    using (var command = new NpgsqlCommand(sql, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var response = new List<Perfiles>();

                        while (await reader.ReadAsync())
                        {
                            var perf = new Perfiles
                            {
                                idperfiles = reader.GetInt32(reader.GetOrdinal("idperfiles")),
                                nombre = reader.IsDBNull(reader.GetOrdinal("nombre"))
                                             ? string.Empty
                                             : reader.GetString(reader.GetOrdinal("nombre"))
                            };

                            response.Add(perf);
                        }

                        return response; // si no hay filas, vuelve lista vacía
                    }
                }
            }
            catch
            {
                return null; // conserva tu contrato actual
            }
        }

        public async Task<List<Respuesta>> ValidarEstructuraModulos(List<PermisosModulosRequest> requests)
        {
            var respuestas = new List<Respuesta>();

            try
            {
                using (var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    // Llamamos a la función; uso notación con nombre para evitar errores por orden
                    const string sql = @"
                SELECT *
                FROM intranet.usp_validaestructuramodulos(
                    idcodmod                => @idcodmod,
                    idcodmodsubmod          => @idcodmodsubmod,
                    idcodmodsubmoditemmod   => @idcodmodsubmoditemmod,
                    idperfiles              => @idperfiles,
                    usuario                 => @usuario,
                    idempresa               => @idempresa,
                    idpais                  => @idpais,
                    idnegocio               => @idnegocio,
                    idcuenta                => @idcuenta,
                    checks                  => @checks,
                    usuario_creacion        => @usuario_creacion
                );";

                    foreach (var req in requests)
                    {
                        using (var command = new NpgsqlCommand(sql, connection))
                        {
                            command.Parameters.Add("idcodmod", NpgsqlDbType.Integer).Value = req.idcodmod;
                            command.Parameters.Add("idcodmodsubmod", NpgsqlDbType.Integer).Value = (object?)req.idcodmodsubmod ?? DBNull.Value;
                            command.Parameters.Add("idcodmodsubmoditemmod", NpgsqlDbType.Integer).Value = (object?)req.idcodmodsubmoditemmod ?? DBNull.Value;
                            command.Parameters.Add("idperfiles", NpgsqlDbType.Integer).Value = req.idperfiles;
                            command.Parameters.Add("usuario", NpgsqlDbType.Varchar).Value = req.usuario ?? (object)DBNull.Value;
                            command.Parameters.Add("idempresa", NpgsqlDbType.Integer).Value = req.idempresa;
                            command.Parameters.Add("idpais", NpgsqlDbType.Integer).Value = req.idpais;
                            command.Parameters.Add("idnegocio", NpgsqlDbType.Integer).Value = (object?)req.idnegocio ?? DBNull.Value;
                            command.Parameters.Add("idcuenta", NpgsqlDbType.Integer).Value = (object?)req.idcuenta ?? DBNull.Value;
                            command.Parameters.Add("checks", NpgsqlDbType.Integer).Value = req.checks;
                            command.Parameters.Add("usuario_creacion", NpgsqlDbType.Varchar).Value = req.usuario_creacion ?? (object)DBNull.Value;

                            using (var rdr = await command.ExecuteReaderAsync())
                            {
                                while (await rdr.ReadAsync())
                                {
                                    // La función retorna: mensaje, nombremodulos, nombreperfil (en minúsculas)
                                    var resp = new Respuesta
                                    {
                                        Mensaje = rdr["mensaje"]?.ToString() ?? string.Empty,
                                        NombreModulos = rdr["nombremodulos"]?.ToString() ?? string.Empty,
                                        NombrePerfil = rdr["nombreperfil"]?.ToString() ?? string.Empty
                                    };
                                    respuestas.Add(resp);
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
                using (var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    // Alias a "Estado" y "Mensaje" para mantener tu código tal cual
                    const string sql = @"
                SELECT 
                    estado_out AS ""Estado"",
                    mensaje    AS ""Mensaje""
                FROM intranet.usp_insertaractualizarusuario(
                    docusuario       => @docusuario,
                    nombres          => @nombres,
                    apellidopaterno  => @apellidopaterno,
                    apellidomaterno  => @apellidomaterno,
                    usuario          => @usuario,
                    clave            => @clave,
                    idemppaisnegcue  => @idemppaisnegcue,
                    estado           => @estado,
                    usuariocreacion  => @usuariocreacion,
                    correopersonal   => @correopersonal,
                    correocorp       => @correocorp,
                    celular          => @celular,
                    sexo             => @sexo,
                    fechanacimiento  => @fechanacimiento,
                    direccion        => @direccion,
                    fechaingreso     => @fechaingreso
                );";

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        // Requeridos (según la función en Postgres)
                        command.Parameters.Add("docusuario", NpgsqlDbType.Varchar).Value = request.docusuario ?? (object)DBNull.Value;
                        command.Parameters.Add("nombres", NpgsqlDbType.Varchar).Value = request.nombres ?? (object)DBNull.Value;
                        command.Parameters.Add("apellidopaterno", NpgsqlDbType.Varchar).Value = request.apellidopaterno ?? (object)DBNull.Value;
                        command.Parameters.Add("apellidomaterno", NpgsqlDbType.Varchar).Value = request.apellidomaterno ?? (object)DBNull.Value;
                        command.Parameters.Add("usuario", NpgsqlDbType.Varchar).Value = request.usuario ?? (object)DBNull.Value;
                        command.Parameters.Add("clave", NpgsqlDbType.Varchar).Value = request.clave ?? (object)DBNull.Value;
                        command.Parameters.Add("idemppaisnegcue", NpgsqlDbType.Integer).Value = request.idemppaisnegcue;
                        command.Parameters.Add("estado", NpgsqlDbType.Integer).Value = request.estado;
                        command.Parameters.Add("usuariocreacion", NpgsqlDbType.Varchar).Value = request.usuariocreacion ?? (object)DBNull.Value;

                        // Opcionales (NULL cuando no vienen)
                        command.Parameters.Add("correopersonal", NpgsqlDbType.Varchar).Value = (object?)request.correopersonal ?? DBNull.Value;
                        command.Parameters.Add("correocorp", NpgsqlDbType.Varchar).Value = (object?)request.correocorp ?? DBNull.Value;
                        command.Parameters.Add("celular", NpgsqlDbType.Varchar).Value = (object?)request.celular ?? DBNull.Value;
                        command.Parameters.Add("sexo", NpgsqlDbType.Char).Value = (object?)request.sexo ?? DBNull.Value;
                        command.Parameters.Add("fechanacimiento", NpgsqlDbType.Date).Value = (object?)request.fechanacimiento ?? DBNull.Value;
                        command.Parameters.Add("direccion", NpgsqlDbType.Varchar).Value = (object?)request.direccion ?? DBNull.Value;
                        command.Parameters.Add("fechaingreso", NpgsqlDbType.Date).Value = (object?)request.fechaingreso ?? DBNull.Value;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                respuesta.Estado = reader["Estado"]?.ToString();
                                respuesta.Mensaje = reader["Mensaje"]?.ToString();
                            }
                            else
                            {
                                // Por si la función no retornara filas (no debería pasar)
                                respuesta.Estado = "ERROR";
                                respuesta.Mensaje = "No se recibió respuesta del servidor.";
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
                using (var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    // Alias para mantener los mismos nombres de columnas que espera tu C#
                    const string sql = @"
                SELECT
                    idusuario,
                    docusuario_out AS docusuario,
                    nombres,
                    apellidopaterno,
                    apellidomaterno,
                    correopersonal,
                    correocorp,
                    celular,
                    sexo,
                    fechanacimiento,
                    direccion,
                    usuario_out AS usuario,
                    clave,
                    idemppaisnegcue,
                    estado,
                    fechaingreso
                FROM intranet.usp_getusuarioxdoc(
                    docusuario  => @docusuario,
                    nombresocio => @nombresocio
                );";

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.Add("docusuario", NpgsqlDbType.Varchar).Value = docusuario ?? (object)DBNull.Value;
                        command.Parameters.Add("nombresocio", NpgsqlDbType.Varchar).Value = nombresocio ?? (object)DBNull.Value;

                        using (var reader = await command.ExecuteReaderAsync())
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
                                    fechanacimiento = reader.IsDBNull(reader.GetOrdinal("fechanacimiento"))
                                        ? (DateTime?)null
                                        : reader.GetDateTime(reader.GetOrdinal("fechanacimiento")),
                                    direccion = reader["direccion"]?.ToString(),
                                    usuario = reader["usuario"]?.ToString(),
                                    clave = reader["clave"]?.ToString(),  // ya viene desencriptada desde la función
                                    idemppaisnegcue = reader.GetInt32(reader.GetOrdinal("idemppaisnegcue")),
                                    estado = reader.GetInt32(reader.GetOrdinal("estado")),
                                    fechaingreso = reader.IsDBNull(reader.GetOrdinal("fechaingreso"))
                                        ? (DateTime?)null
                                        : reader.GetDateTime(reader.GetOrdinal("fechaingreso"))
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
