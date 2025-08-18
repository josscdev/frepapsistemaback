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
using Npgsql;
using NpgsqlTypes;

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
                await using var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await connection.OpenAsync();

                // 🔹 Ejecutamos la FUNCTION como un SELECT
                const string sql = @"select * 
                             from intranet.usp_rombilogin(@idempresa, @idpais, @user, @password)";

                await using var command = new NpgsqlCommand(sql, connection);

                command.Parameters.Add(new NpgsqlParameter("@idempresa", NpgsqlDbType.Integer) { Value = request.idempresa });
                command.Parameters.Add(new NpgsqlParameter("@idpais", NpgsqlDbType.Integer) { Value = request.idpais });
                command.Parameters.Add(new NpgsqlParameter("@user", NpgsqlDbType.Varchar) { Value = request.user ?? string.Empty });
                command.Parameters.Add(new NpgsqlParameter("@password", NpgsqlDbType.Varchar) { Value = request.password ?? string.Empty });

                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    // 🚀 Mapeamos los valores que devuelve la función
                    return new UserDTOResponse
                    {
                        Resultado = reader["resultado"] is DBNull ? null : reader.GetString(reader.GetOrdinal("resultado")),
                        Accede = reader["accede"] is DBNull ? 0 : reader.GetInt32(reader.GetOrdinal("accede")),
                        Perfil = reader["perfil"] is DBNull ? null : reader.GetString(reader.GetOrdinal("perfil")),
                        idusuario = reader["idusuario"] is DBNull ? (int?)null : reader.GetInt32(reader.GetOrdinal("idusuario")),
                        idusuarioromweb = reader["idusuarioromweb"] is DBNull ? (int?)null : reader.GetInt32(reader.GetOrdinal("idusuarioromweb"))
                    };
                }

                // 🔹 Si no devolvió filas, acceso denegado
                return new UserDTOResponse
                {
                    Resultado = "Acceso denegado",
                    Accede = 0,
                    Perfil = null,
                    idusuario = null,
                    idusuarioromweb = null
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en RombiLoginMain: " + ex.Message);
                throw;
            }
        }

        public async Task<UserDataDTOResponse> GetUserData(UserDTORequest request)
        {
            const string sql = @"
                                SELECT usuario, nombres, apellidopaterno, apellidomaterno
                                FROM intranet.usp_getuserdata(@usuario)
                                LIMIT 1;";

            try
            {
                await using var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await connection.OpenAsync();

                await using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("usuario", NpgsqlDbType.Varchar, (object?)request.user ?? DBNull.Value);

                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    // fila encontrada
                    int oUsuario = reader.GetOrdinal("usuario");
                    int oNombres = reader.GetOrdinal("nombres");
                    int oApellidoPaterno = reader.GetOrdinal("apellidopaterno");
                    int oApellidoMaterno = reader.GetOrdinal("apellidomaterno");

                    return new UserDataDTOResponse
                    {
                        usuario = reader.IsDBNull(oUsuario) ? string.Empty : reader.GetString(oUsuario),
                        nombres = reader.IsDBNull(oNombres) ? string.Empty : reader.GetString(oNombres),
                        apellidopaterno = reader.IsDBNull(oApellidoPaterno) ? string.Empty : reader.GetString(oApellidoPaterno),
                        apellidomaterno = reader.IsDBNull(oApellidoMaterno) ? string.Empty : reader.GetString(oApellidoMaterno),
                    };
                }

                // sin filas → devuelve objeto vacío (o retorna null si prefieres)
                return new UserDataDTOResponse
                {
                    usuario = string.Empty,
                    nombres = string.Empty,
                    apellidopaterno = string.Empty,
                    apellidomaterno = string.Empty
                };
            }
            catch (NpgsqlException npgEx)
            {
                Console.WriteLine($"PostgreSQL error [{npgEx.SqlState}]: {npgEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en GetUserData: " + ex.Message);
                throw;
            }
        }

        public async Task<List<BusinessAccountResponse>> GetBusinessUser(UserDTORequest request)
        {
            var result = new List<BusinessAccountResponse>();

            const string sql = @"
                                SELECT idpais, idnegocio, nombrenegocio
                                FROM usp_getnegociouser(@paisid, @empresaid, @usuario);";

            try
            {
                await using var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await connection.OpenAsync();

                await using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("paisid", NpgsqlDbType.Integer, request.idpais);
                command.Parameters.AddWithValue("empresaid", NpgsqlDbType.Integer, request.idempresa);
                command.Parameters.AddWithValue("usuario", NpgsqlDbType.Varchar, (object?)request.user ?? DBNull.Value);

                await using var reader = await command.ExecuteReaderAsync();

                int ordIdPais = reader.GetOrdinal("idpais");
                int ordIdNegocio = reader.GetOrdinal("idnegocio");
                int ordNombreNegocio = reader.GetOrdinal("nombrenegocio");

                while (await reader.ReadAsync())
                {
                    result.Add(new BusinessAccountResponse
                    {
                        idpais = reader.IsDBNull(ordIdPais) ? 0 : reader.GetInt32(ordIdPais),
                        idnegocio = reader.IsDBNull(ordIdNegocio) ? 0 : reader.GetInt32(ordIdNegocio),
                        nombrenegocio = reader.IsDBNull(ordNombreNegocio) ? string.Empty : reader.GetString(ordNombreNegocio)
                    });
                }
            }
            catch (NpgsqlException npgEx)
            {
                Console.WriteLine($"PostgreSQL error [{npgEx.SqlState}]: {npgEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetBusinessUser: {ex.Message}\n{ex.StackTrace}");
                throw;
            }

            return result; // lista vacía si no hubo filas
        }

        public async Task<List<BusinessAccountResponse>> GetBusinessAccountUser(UserDTORequest request)
        {
            var result = new List<BusinessAccountResponse>();

            const string sql = @"
                                SELECT idpais, idnegocio, nombrenegocio, idcuenta, nombrecuenta
                                FROM intranet.usp_getcuentauser(@empresaid, @idnegocio, @usuario);";

            try
            {
                await using var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await connection.OpenAsync();

                await using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("empresaid", NpgsqlDbType.Integer, request.idempresa);
                command.Parameters.AddWithValue("idnegocio", NpgsqlDbType.Integer, request.idnegocio);
                command.Parameters.AddWithValue("usuario", NpgsqlDbType.Varchar, (object?)request.user ?? DBNull.Value);

                await using var reader = await command.ExecuteReaderAsync();

                int ordIdPais = reader.GetOrdinal("idpais");
                int ordIdNegocio = reader.GetOrdinal("idnegocio");
                int ordNombreNegocio = reader.GetOrdinal("nombrenegocio");
                int ordIdCuenta = reader.GetOrdinal("idcuenta");
                int ordNombreCuenta = reader.GetOrdinal("nombrecuenta");

                while (await reader.ReadAsync())
                {
                    result.Add(new BusinessAccountResponse
                    {
                        idpais = reader.IsDBNull(ordIdPais) ? 0 : reader.GetInt32(ordIdPais),
                        idnegocio = reader.IsDBNull(ordIdNegocio) ? 0 : reader.GetInt32(ordIdNegocio),
                        nombrenegocio = reader.IsDBNull(ordNombreNegocio) ? string.Empty : reader.GetString(ordNombreNegocio),
                        idcuenta = reader.IsDBNull(ordIdCuenta) ? 0 : reader.GetInt32(ordIdCuenta),
                        nombrecuenta = reader.IsDBNull(ordNombreCuenta) ? string.Empty : reader.GetString(ordNombreCuenta)
                    });
                }
            }
            catch (NpgsqlException npgEx)
            {
                Console.WriteLine($"PostgreSQL error [{npgEx.SqlState}]: {npgEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetBusinessAccountUser: {ex.Message}\n{ex.StackTrace}");
                throw;
            }

            return result; // lista vacía si no hay filas
        }

        public async Task<List<ModuloDTOResponse>> GetPermissions(UserDTORequest request)
        {
            var permissions = new List<ModuloDTOResponse>();

            // Trae todas las columnas que tu DTO necesita desde la función PG
            const string sql = @"
                                SELECT
                                  idmodulo, nombremodulo, iconomodulo, rutamodulo, nivelmodulo, ordenmodulo, estadomodulo,
                                  idperfilmodulo, nombreperfilmodulo,
                                  idsubmodulo, nombresubmodulo, iconosubmodulo, rutasubmodulo, nivelsubmodulo, ordensubmodulo, estadosubmodulo,
                                  idperfilsubmodulo, nombreperfilsubmodulo,
                                  iditemmodulo, nombreitemmodulo, iconoitemmodulo, rutaitemmodulo, nivelitemmodulo, ordenitemmodulo, estadoitemmodulo,
                                  idperfilitemmodulo, nombreperfilitemmodulo
                                FROM usp_getmenupermissionsuser(@idpais, @idempresa, @idnegocio, @idcuenta, @usuario);";

            try
            {
                await using var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await connection.OpenAsync();

                await using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("idpais", NpgsqlDbType.Integer, request.idpais);
                command.Parameters.AddWithValue("idempresa", NpgsqlDbType.Integer, request.idempresa);
                command.Parameters.AddWithValue("idnegocio", NpgsqlDbType.Integer, request.idnegocio);
                command.Parameters.AddWithValue("idcuenta", NpgsqlDbType.Integer, request.idcuenta);
                command.Parameters.AddWithValue("usuario", NpgsqlDbType.Varchar, (object?)request.user ?? DBNull.Value);

                var moduloDictionary = new Dictionary<int, ModuloDTOResponse>();

                await using var reader = await command.ExecuteReaderAsync();

                // Ordinales (micro-optimización y evita repetir GetOrdinal)
                int oIdModulo = reader.GetOrdinal("idmodulo");
                int oNombreModulo = reader.GetOrdinal("nombremodulo");
                int oIconoModulo = reader.GetOrdinal("iconomodulo");
                int oRutaModulo = reader.GetOrdinal("rutamodulo");
                int oNivelModulo = reader.GetOrdinal("nivelmodulo");
                int oOrdenModulo = reader.GetOrdinal("ordenmodulo");
                int oEstadoModulo = reader.GetOrdinal("estadomodulo");
                int oIdPerfilModulo = reader.GetOrdinal("idperfilmodulo");           // <-- corregido
                int oNombrePerfilModulo = reader.GetOrdinal("nombreperfilmodulo");

                int oIdSubModulo = reader.GetOrdinal("idsubmodulo");
                int oNombreSubModulo = reader.GetOrdinal("nombresubmodulo");
                int oIconoSubModulo = reader.GetOrdinal("iconosubmodulo");
                int oRutaSubModulo = reader.GetOrdinal("rutasubmodulo");
                int oNivelSubModulo = reader.GetOrdinal("nivelsubmodulo");
                int oOrdenSubModulo = reader.GetOrdinal("ordensubmodulo");
                int oEstadoSubModulo = reader.GetOrdinal("estadosubmodulo");
                int oIdPerfilSubModulo = reader.GetOrdinal("idperfilsubmodulo");
                int oNombrePerfilSubModulo = reader.GetOrdinal("nombreperfilsubmodulo");

                int oIdItem = reader.GetOrdinal("iditemmodulo");
                int oNombreItem = reader.GetOrdinal("nombreitemmodulo");
                int oIconoItem = reader.GetOrdinal("iconoitemmodulo");
                int oRutaItem = reader.GetOrdinal("rutaitemmodulo");
                int oNivelItem = reader.GetOrdinal("nivelitemmodulo");
                int oOrdenItem = reader.GetOrdinal("ordenitemmodulo");
                int oEstadoItem = reader.GetOrdinal("estadoitemmodulo");
                int oIdPerfilItem = reader.GetOrdinal("idperfilitemmodulo");
                int oNombrePerfilItem = reader.GetOrdinal("nombreperfilitemmodulo");

                while (await reader.ReadAsync())
                {
                    int idmodulo = reader.IsDBNull(oIdModulo) ? 0 : reader.GetInt32(oIdModulo);

                    if (!moduloDictionary.TryGetValue(idmodulo, out var module))
                    {
                        module = new ModuloDTOResponse
                        {
                            idmodulo = idmodulo,
                            nombremodulo = reader.IsDBNull(oNombreModulo) ? null : reader.GetString(oNombreModulo),
                            iconomodulo = reader.IsDBNull(oIconoModulo) ? null : reader.GetString(oIconoModulo),
                            rutamodulo = reader.IsDBNull(oRutaModulo) ? null : reader.GetString(oRutaModulo),
                            nivelmodulo = reader.IsDBNull(oNivelModulo) ? 0 : reader.GetInt32(oNivelModulo),
                            ordenmodulo = reader.IsDBNull(oOrdenModulo) ? 0 : reader.GetInt32(oOrdenModulo),
                            estadomodulo = reader.IsDBNull(oEstadoModulo) ? 0 : reader.GetInt32(oEstadoModulo),
                            idperfilmodulo = reader.IsDBNull(oIdPerfilModulo) ? 0 : reader.GetInt32(oIdPerfilModulo),
                            nombreperfilmodulo = reader.IsDBNull(oNombrePerfilModulo) ? null : reader.GetString(oNombrePerfilModulo),
                            submodules = new List<SubModuloDTOResponse>()
                        };
                        moduloDictionary.Add(idmodulo, module);
                    }

                    var submodule = new SubModuloDTOResponse
                    {
                        idsubmodulo = reader.IsDBNull(oIdSubModulo) ? 0 : reader.GetInt32(oIdSubModulo),
                        nombresubmodulo = reader.IsDBNull(oNombreSubModulo) ? null : reader.GetString(oNombreSubModulo),
                        iconosubmodulo = reader.IsDBNull(oIconoSubModulo) ? null : reader.GetString(oIconoSubModulo),
                        rutasubmodulo = reader.IsDBNull(oRutaSubModulo) ? null : reader.GetString(oRutaSubModulo),
                        nivelsubmodulo = reader.IsDBNull(oNivelSubModulo) ? 0 : reader.GetInt32(oNivelSubModulo),
                        ordensubmodulo = reader.IsDBNull(oOrdenSubModulo) ? 0 : reader.GetInt32(oOrdenSubModulo),
                        estadosubmodulo = reader.IsDBNull(oEstadoSubModulo) ? 0 : reader.GetInt32(oEstadoSubModulo),
                        idperfilsubmodulo = reader.IsDBNull(oIdPerfilSubModulo) ? 0 : reader.GetInt32(oIdPerfilSubModulo),
                        nombreperfilsubmodulo = reader.IsDBNull(oNombrePerfilSubModulo) ? null : reader.GetString(oNombrePerfilSubModulo),
                        items = new List<ItemModuloDTOResponse>()
                    };

                    var item = new ItemModuloDTOResponse
                    {
                        iditemmodulo = reader.IsDBNull(oIdItem) ? 0 : reader.GetInt32(oIdItem),
                        nombreitemmodulo = reader.IsDBNull(oNombreItem) ? null : reader.GetString(oNombreItem),
                        iconoitemmodulo = reader.IsDBNull(oIconoItem) ? null : reader.GetString(oIconoItem),
                        rutaitemmodulo = reader.IsDBNull(oRutaItem) ? null : reader.GetString(oRutaItem),
                        nivelitemmodulo = reader.IsDBNull(oNivelItem) ? 0 : reader.GetInt32(oNivelItem),
                        ordenitemmodulo = reader.IsDBNull(oOrdenItem) ? 0 : reader.GetInt32(oOrdenItem),
                        estadoitemmodulo = reader.IsDBNull(oEstadoItem) ? 0 : reader.GetInt32(oEstadoItem),
                        idperfilitemmodulo = reader.IsDBNull(oIdPerfilItem) ? 0 : reader.GetInt32(oIdPerfilItem),
                        nombreperfilitemmodulo = reader.IsDBNull(oNombrePerfilItem) ? null : reader.GetString(oNombrePerfilItem)
                    };

                    // Agregar al árbol (evita duplicar submódulos)
                    var existingSubmodule = module.submodules.FirstOrDefault(s => s.idsubmodulo == submodule.idsubmodulo);
                    if (existingSubmodule != null)
                    {
                        existingSubmodule.items.Add(item);
                    }
                    else
                    {
                        module.submodules.Add(submodule);
                        submodule.items.Add(item);
                    }
                }

                permissions.AddRange(moduloDictionary.Values);
            }
            catch (Npgsql.NpgsqlException npgEx)
            {
                Console.WriteLine($"PostgreSQL error [{npgEx.SqlState}]: {npgEx.Message}");
                throw;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error en GetPermissions: {ex.Message}\n{ex.StackTrace}");
                throw;
            }

            return permissions;
        }

        public async Task<IdCodigo?> GetIdCodigo(CodigosRequest request)
        {
            try
            {
                // Si tu función es RETURNS TABLE (...) usamos SELECT ... LIMIT 1
                const string sql = @"
                                    SELECT idemppaisnegcue
                                    FROM intranet.usp_getidemppaisnegcue(@idpais, @idempresa, @idnegocio, @idcuenta)
                                    LIMIT 1;";

                await using var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await connection.OpenAsync();

                await using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("idpais", NpgsqlDbType.Integer, request.idpais);
                command.Parameters.AddWithValue("idempresa", NpgsqlDbType.Integer, request.idempresa);
                command.Parameters.AddWithValue("idnegocio", NpgsqlDbType.Integer, request.idnegocio);
                command.Parameters.AddWithValue("idcuenta", NpgsqlDbType.Integer, request.idcuenta);

                var scalar = await command.ExecuteScalarAsync();

                if (scalar == null || scalar is DBNull)
                    return null;

                return new IdCodigo { idemppaisnegcue = Convert.ToInt32(scalar) };
            }
            catch (NpgsqlException npgEx)
            {
                Console.WriteLine($"PostgreSQL error [{npgEx.SqlState}]: {npgEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetIdCodigo: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }


        public async Task<IdCodigo?> GetIdpdv(CodigosRequest request)
        {
            try
            {
                const string sql = @"
                                    SELECT intranet.usp_getidpdvrol(@usuario, @idemppaisnegcue) AS idpdv;";

                await using var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await connection.OpenAsync();

                await using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("usuario", NpgsqlDbType.Varchar, (object?)request.usuario ?? DBNull.Value);
                command.Parameters.AddWithValue("idemppaisnegcue", NpgsqlDbType.Integer, request.idemppaisnegcue);

                var scalar = await command.ExecuteScalarAsync();

                if (scalar == null || scalar is DBNull)
                    return null;

                return new IdCodigo { idpdv = Convert.ToInt32(scalar) };
            }
            catch (Npgsql.NpgsqlException npgEx)
            {
                Console.WriteLine($"PostgreSQL error [{npgEx.SqlState}]: {npgEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetIdpdv: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }


        //NO SE USA
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
                const string sql = @"
                                    SELECT ruta
                                    FROM intranet.usp_getrutaprincipal(@idemppaisnegcue)
                                    LIMIT 1;";

                await using var connection = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await connection.OpenAsync();

                await using var command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("idemppaisnegcue", NpgsqlDbType.Integer, idemppaisnegcue);

                var scalar = await command.ExecuteScalarAsync();
                return scalar == null || scalar is DBNull ? string.Empty : (string)scalar;
            }
            catch (NpgsqlException npgEx)
            {
                Console.WriteLine($"PostgreSQL error [{npgEx.SqlState}]: {npgEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetRutaPrincipal: {ex.Message}\n{ex.StackTrace}");
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
