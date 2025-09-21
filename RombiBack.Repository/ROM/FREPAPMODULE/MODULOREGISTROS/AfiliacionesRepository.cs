using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.FREPAPMODULE.MODULOREGISTROS;

namespace RombiBack.Repository.ROM.FREPAPMODULE.MODULOREGISTROS
{
    public class AfiliacionesRepository: IAfiliacionesRepository
    {
        private readonly DataAcces _dbConnection;
        public AfiliacionesRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<ListarAfiliacion>> ListarAfiliaciones(FiltroAfiliacion filtro)
        {
            var resultados = new List<ListarAfiliacion>();

            const string sql = @"
            SELECT *
            FROM intranet.usp_buscar_afiliacion(
                @region, @provincia, @distrito, @perfil, @usuario
            );";

            try
            {
                await using var cn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await cn.OpenAsync();

                await using var cmd = new NpgsqlCommand(sql, cn);

                // Pasar NULL a la función cuando vengan vacíos
                cmd.Parameters.AddWithValue("@region", (object?)ToDbNullIfEmpty(filtro.region) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@provincia", (object?)ToDbNullIfEmpty(filtro.provincia) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@distrito", (object?)ToDbNullIfEmpty(filtro.distrito) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@perfil", (object?)ToDbNullIfEmpty(filtro.perfil) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@usuario", (object?)ToDbNullIfEmpty(filtro.usuario) ?? DBNull.Value);

                await using var rd = await cmd.ExecuteReaderAsync();
                while (await rd.ReadAsync())
                {
                    var rutaFoto = GetString(rd, "fotoimg");
                    var item = new ListarAfiliacion
                    {
                        idafiliacion = GetInt(rd, "idafiliacion"),
                        fotoimg = ConvertFileToBase64(rutaFoto), // 👈 ahora devuelve Base64 o null
                        numficha = GetString(rd, "numficha"),
                        idtipodocumento = GetInt(rd, "idtipodocumento"),
                        nombretipodocumento = GetString(rd, "nombretipodocumento"),
                        abreviatura = GetString(rd, "abreviatura"),
                        docafiliado = GetString(rd, "docafiliado"),
                        nombres = GetString(rd, "nombres"),
                        apellidopaterno = GetString(rd, "apellidopaterno"),
                        apellidomaterno = GetString(rd, "apellidomaterno"),
                        fechaafiliacion = GetDateTime(rd, "fechaafiliacion"),
                        fechanacimiento = GetDateTime(rd, "fechanacimiento"),
                        lugarnacimiento = GetString(rd, "lugarnacimiento"),
                        sexo = GetString(rd, "sexo"),
                        edadafiliado = GetInt(rd, "edadafiliado"),
                        idestadocivil = GetInt(rd, "idestadocivil"),
                        nombreestadocivil = GetString(rd, "nombreestadocivil"),
                        estado = GetInt(rd, "estado"),
                        estado_text = GetString(rd, "estado_text"),
                        codubicacion = GetString(rd, "codubicacion"),
                        region = GetString(rd, "region"),
                        subregion = GetString(rd, "subregion"),
                        localidad = GetString(rd, "localidad"),
                        avenida = GetString(rd, "avenida"),
                        numero = GetString(rd, "numero"),
                        urbanizacion = GetString(rd, "urbanizacion"),
                        celular = GetString(rd, "celular"),
                        correo = GetString(rd, "correo"),
                        usuariocreacion = GetString(rd, "usuariocreacion"),
                        fechacreacion = GetDateTime(rd, "fechacreacion"),
                        usuariomodificacion = GetString(rd, "usuariomodificacion"),
                        fechamodificacion = GetDateTime(rd, "fechamodificacion"),
                        usuarioanulacion = GetString(rd, "usuarioanulacion"),
                        fechaanulacion = GetDateTime(rd, "fechaanulacion")
                    };

                    resultados.Add(item);
                }
            }
            catch (NpgsqlException npgEx)
            {
                // Aquí podrías loguear (serilog, etc.)
                throw new ApplicationException("Error de base de datos al listar afiliaciones.", npgEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error inesperado al listar afiliaciones.", ex);
            }

            return resultados;
        }

        private string? ConvertFileToBase64(string? path)
        {
            try
            {
                if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
                {
                    return null; // o podrías devolver una imagen base64 por defecto
                }

                var bytes = System.IO.File.ReadAllBytes(path);
                var ext = System.IO.Path.GetExtension(path).ToLower();

                string mimeType = ext switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".webp" => "image/webp",
                    _ => "application/octet-stream"
                };

                return $"data:{mimeType};base64,{Convert.ToBase64String(bytes)}";
            }
            catch
            {
                return null; // en caso de error, no rompas la carga
            }
        }

        // Helpers de lectura segura
        private static string? GetString(NpgsqlDataReader rd, string name)
        {
            var i = rd.GetOrdinal(name);
            return rd.IsDBNull(i) ? null : rd.GetString(i);
        }

        private static int GetInt(NpgsqlDataReader rd, string name)
        {
            var i = rd.GetOrdinal(name);
            // La función retorna int NO NULL para estos campos; si fuese null, devolvemos 0
            return rd.IsDBNull(i) ? 0 : rd.GetInt32(i);
        }

        private static DateTime? GetDateTime(NpgsqlDataReader rd, string name)
        {
            var i = rd.GetOrdinal(name);
            return rd.IsDBNull(i) ? (DateTime?)null : rd.GetDateTime(i);
        }

        private static string? ToDbNullIfEmpty(string? s)
            => string.IsNullOrWhiteSpace(s) ? null : s;

        public async Task<IEnumerable<ListarOpcionUbigeo>> ListarUbigeos(int? idemppaisnegcue, int? pais)
        {
            var list = new List<ListarOpcionUbigeo>();
            const string sql = "SELECT * FROM intranet.ubigeo_listar(@idemppaisnegcue,@pais);";

            try
            {
                await using var cn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await cn.OpenAsync();
                await using var cmd = new NpgsqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idemppaisnegcue", (object?)idemppaisnegcue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@pais", (object?)pais ?? DBNull.Value);

                await using var rd = await cmd.ExecuteReaderAsync();
                while (await rd.ReadAsync())
                {
                    list.Add(new ListarOpcionUbigeo
                    {
                        codubicacion = rd["codubicacion"].ToString()!,
                        rr = rd["rr"].ToString()!,
                        pp = rd["pp"].ToString()!,
                        dd = rd["dd"].ToString()!,
                        region = rd["region"] as string,
                        subregion = rd["subregion"] as string,
                        localidad = rd["localidad"] as string
                    });
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar ubigeo", ex);
            }

            return list;
        }


        public async Task<long> InsertAfiliacionAsync(AfiliacionCreateDto dto, string usuario)
        {
            const string sql = @"
                INSERT INTO intranet.afiliacion
                (
                    numficha, idtipodocumento, docafiliado, nombres, apellidopaterno, apellidomaterno,
                    fechanacimiento, edadafiliado, sexo, idestadocivil, lugarnacimiento,
                    codubicacion, avenida, numero, urbanizacion,
                    celular, correo, observacionficha,
                    fechaafiliacion, estado,
                    usuariocreacion, fechacreacion
                )
                VALUES
                (
                    @numficha, @idtipodocumento, @docafiliado, @nombres, @apellidopaterno, @apellidomaterno,
                    @fechanacimiento, @edadafiliado, @sexo, @idestadocivil, @lugarnacimiento,
                    @codubicacion, @avenida, @numero, @urbanizacion,
                    @celular, @correo, @observacionficha,
                    @fechaafiliacion, @estado,
                    @usuariocreacion, NOW()
                )
                RETURNING idafiliacion;";

            await using var cn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
            await using var cmd = new NpgsqlCommand(sql, cn);

            // helpers de parseo y nulos
            static object DbNull(string? s) => string.IsNullOrWhiteSpace(s) ? DBNull.Value : s!;
            static object DbNullInt(int? i) => i.HasValue ? i.Value : DBNull.Value;
            static object DbNullDate(string? ymd)
            {
                if (string.IsNullOrWhiteSpace(ymd)) return DBNull.Value;
                if (DateTime.TryParse(ymd, out var dt))
                    return dt.Date;
                return DBNull.Value;
            }

            var codubicacion = dto.dd ?? dto.pp ?? dto.rr;

            try
            {
                await cn.OpenAsync();

                cmd.Parameters.Add(new NpgsqlParameter("@numficha", NpgsqlDbType.Varchar) { Value = DbNull(dto.numficha) });
                cmd.Parameters.Add(new NpgsqlParameter("@idtipodocumento", NpgsqlDbType.Integer) { Value = dto.idtipodocumento });
                cmd.Parameters.Add(new NpgsqlParameter("@docafiliado", NpgsqlDbType.Varchar) { Value = DbNull(dto.docafiliado) });
                cmd.Parameters.Add(new NpgsqlParameter("@nombres", NpgsqlDbType.Varchar) { Value = dto.nombres });
                cmd.Parameters.Add(new NpgsqlParameter("@apellidopaterno", NpgsqlDbType.Varchar) { Value = dto.apellidopaterno });
                cmd.Parameters.Add(new NpgsqlParameter("@apellidomaterno", NpgsqlDbType.Varchar) { Value = DbNull(dto.apellidomaterno) });

                cmd.Parameters.Add(new NpgsqlParameter("@fechanacimiento", NpgsqlDbType.Date) { Value = DbNullDate(dto.fechanacimiento) });
                cmd.Parameters.Add(new NpgsqlParameter("@edadafiliado", NpgsqlDbType.Integer) { Value = dto.edadafiliado });
                cmd.Parameters.Add(new NpgsqlParameter("@sexo", NpgsqlDbType.Varchar) { Value = DbNull(dto.sexo) });
                cmd.Parameters.Add(new NpgsqlParameter("@idestadocivil", NpgsqlDbType.Integer) { Value = DbNullInt(dto.idestadocivil) });
                cmd.Parameters.Add(new NpgsqlParameter("@lugarnacimiento", NpgsqlDbType.Varchar) { Value = DbNull(dto.lugarnacimiento) });

                cmd.Parameters.Add(new NpgsqlParameter("@codubicacion", NpgsqlDbType.Varchar) { Value = DbNull(codubicacion) });
                cmd.Parameters.Add(new NpgsqlParameter("@avenida", NpgsqlDbType.Varchar) { Value = DbNull(dto.avenida) });
                cmd.Parameters.Add(new NpgsqlParameter("@numero", NpgsqlDbType.Varchar) { Value = DbNull(dto.numero) });
                cmd.Parameters.Add(new NpgsqlParameter("@urbanizacion", NpgsqlDbType.Varchar) { Value = DbNull(dto.urbanizacion) });

                cmd.Parameters.Add(new NpgsqlParameter("@celular", NpgsqlDbType.Varchar) { Value = DbNull(dto.telefono) });
                cmd.Parameters.Add(new NpgsqlParameter("@correo", NpgsqlDbType.Varchar) { Value = DbNull(dto.correo) });
                cmd.Parameters.Add(new NpgsqlParameter("@observacionficha", NpgsqlDbType.Varchar) { Value = DbNull(dto.observacion) });

                cmd.Parameters.Add(new NpgsqlParameter("@fechaafiliacion", NpgsqlDbType.Date) { Value = DbNullDate(dto.fechaafiliacion) });
                cmd.Parameters.Add(new NpgsqlParameter("@estado", NpgsqlDbType.Integer) { Value = dto.estado });

                cmd.Parameters.Add(new NpgsqlParameter("@usuariocreacion", NpgsqlDbType.Varchar) { Value = usuario });

                var id = await cmd.ExecuteScalarAsync();
                return (id is long l) ? l : Convert.ToInt64(id);
            }
            catch (NpgsqlException ex)
            {
                // Puedes loguear el error o lanzar una excepción custom
                throw new Exception("Error en la inserción de afiliación (PostgreSQL)", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado en InsertAfiliacionAsync", ex);
            }
        }


        // =============== UPDATE ARCHIVOS ===================
        public async Task UpdateArchivosAsync(
            long idafiliacion,
            string? fotoimg,
            string? fichaafiliacionpdf,
            string? hojadevidapdf
        // string? copiadocumentopdf
        )
        {
            // Si no existe alguna columna, quítala del SQL y de los parámetros
           const string sql = @"
            UPDATE intranet.afiliacion
            SET
                fotoimg = COALESCE(@fotoimg, fotoimg),
                fichaafiliacionpdf = COALESCE(@fichaafiliacionpdf, fichaafiliacionpdf),
                hojadevidapdf = COALESCE(@hojadevidapdf, hojadevidapdf)
                -- , copiadocumentopdf = COALESCE(@copiadocumentopdf, copiadocumentopdf)
            WHERE idafiliacion = @id;";

            await using var cn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
            await cn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, cn);
            cmd.Parameters.Add(new NpgsqlParameter("@id", NpgsqlDbType.Integer) { Value = (int)idafiliacion }); // serial4 en tu tabla
            cmd.Parameters.Add(new NpgsqlParameter("@fotoimg", NpgsqlDbType.Varchar) { Value = (object?)fotoimg ?? DBNull.Value });
            cmd.Parameters.Add(new NpgsqlParameter("@fichaafiliacionpdf", NpgsqlDbType.Varchar) { Value = (object?)fichaafiliacionpdf ?? DBNull.Value });
            cmd.Parameters.Add(new NpgsqlParameter("@hojadevidapdf", NpgsqlDbType.Varchar) { Value = (object?)hojadevidapdf ?? DBNull.Value });
            // cmd.Parameters.Add(new NpgsqlParameter("@copiadocumentopdf", NpgsqlDbType.Varchar) { Value = (object?)copiadocumentopdf ?? DBNull.Value });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<ListarEstadoCivil>> ListarEstadosCiviles(int idemppaisnegcue)
        {
            var resultados = new List<ListarEstadoCivil>();

            const string sql = @"
                                SELECT *
                                FROM intranet.usp_getlistarestadosciviles(@idemppaisnegcue);";

            try
            {
                await using var cn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await cn.OpenAsync();

                await using var cmd = new NpgsqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idemppaisnegcue", (object)idemppaisnegcue ?? DBNull.Value);

                await using var rd = await cmd.ExecuteReaderAsync();
                while (await rd.ReadAsync())
                {
                    var item = new ListarEstadoCivil
                    {
                        idestadocivil = GetInt(rd, "idestadocivil"),
                        nombreestadocivil = GetString(rd, "nombreestadocivil"),
                        idemppaisnegcue = GetInt(rd, "idemppaisnegcue"),
                        estado = GetInt(rd, "estado")
                    };

                    resultados.Add(item);
                }
            }
            catch (NpgsqlException npgEx)
            {
                throw new ApplicationException("Error de base de datos al listar estados civiles.", npgEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error inesperado al listar estados civiles.", ex);
            }

            return resultados;
        }


        public async Task<AfiliacionReadDto?> GetAfiliacionByIdAsync(int idafiliacion)
        {
            const string sql = @"
        SELECT
            idafiliacion, numficha, idtipodocumento, docafiliado, nombres, apellidopaterno, apellidomaterno,
            fechanacimiento, edadafiliado, sexo, idestadocivil, lugarnacimiento,
            codubicacion, avenida, numero, urbanizacion,
            celular, correo, observacionficha,
            fechaafiliacion, estado,
            fotoimg, fichaafiliacionpdf, hojadevidapdf
        FROM intranet.afiliacion
        WHERE idafiliacion = @id;";

            await using var cn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
            await cn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", idafiliacion);

            await using var rd = await cmd.ExecuteReaderAsync();
            if (!await rd.ReadAsync()) return null;

            string? ToYmd(object? o) => (o is DateTime dt) ? dt.ToString("yyyy-MM-dd") : null;

            // 👉 helper local para convertir archivos a data URI
            static string? ToDataUri(string? path)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path)) return null;

                    var bytes = System.IO.File.ReadAllBytes(path);
                    var ext = Path.GetExtension(path).ToLowerInvariant();

                    var mime = ext switch
                    {
                        ".jpg" or ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".webp" => "image/webp",
                        ".gif" => "image/gif",
                        ".pdf" => "application/pdf",
                        _ => "application/octet-stream"
                    };

                    return $"data:{mime};base64,{Convert.ToBase64String(bytes)}";
                }
                catch
                {
                    return null;
                }
            }

            return new AfiliacionReadDto
            {
                idafiliacion = rd.GetInt32(rd.GetOrdinal("idafiliacion")),
                numficha = rd["numficha"] as string,
                idtipodocumento = rd["idtipodocumento"] as int?,
                docafiliado = rd["docafiliado"] as string,
                nombres = rd["nombres"]?.ToString() ?? "",
                apellidopaterno = rd["apellidopaterno"]?.ToString() ?? "",
                apellidomaterno = rd["apellidomaterno"] as string,
                fechanacimiento = ToYmd(rd["fechanacimiento"]),
                edadafiliado = rd["edadafiliado"] as int?,
                sexo = rd["sexo"] as string,
                idestadocivil = rd["idestadocivil"] as int?,
                lugarnacimiento = rd["lugarnacimiento"] as string,
                codubicacion = rd["codubicacion"] as string,
                avenida = rd["avenida"] as string,
                numero = rd["numero"] as string,
                urbanizacion = rd["urbanizacion"] as string,
                telefono = rd["celular"] as string,
                correo = rd["correo"] as string,
                observacion = rd["observacionficha"] as string,
                fechaafiliacion = ToYmd(rd["fechaafiliacion"]),
                estado = (int)(rd["estado"] ?? 0),

                // 👉 Aquí la conversión a base64
                fotoimg = ToDataUri(rd["fotoimg"] as string),
                fichaafiliacionpdf = ToDataUri(rd["fichaafiliacionpdf"] as string),
                hojadevidapdf = ToDataUri(rd["hojadevidapdf"] as string)
            };
        }


        public async Task<int> UpdateAfiliacionAsync(int idafiliacion, AfiliacionUpdateDto dto, string usuario)
        {
            const string sql = @"
        UPDATE intranet.afiliacion
        SET
            numficha         = COALESCE(@numficha, numficha),
            idtipodocumento  = COALESCE(@idtipodocumento, idtipodocumento),
            docafiliado      = COALESCE(@docafiliado, docafiliado),
            nombres          = COALESCE(@nombres, nombres),
            apellidopaterno  = COALESCE(@apellidopaterno, apellidopaterno),
            apellidomaterno  = COALESCE(@apellidomaterno, apellidomaterno),
            fechanacimiento  = COALESCE(@fechanacimiento, fechanacimiento),
            edadafiliado     = COALESCE(@edadafiliado, edadafiliado),
            sexo             = COALESCE(@sexo, sexo),
            idestadocivil    = COALESCE(@idestadocivil, idestadocivil),
            lugarnacimiento  = COALESCE(@lugarnacimiento, lugarnacimiento),
            codubicacion     = COALESCE(@codubicacion, codubicacion),
            avenida          = COALESCE(@avenida, avenida),
            numero           = COALESCE(@numero, numero),
            urbanizacion     = COALESCE(@urbanizacion, urbanizacion),
            celular          = COALESCE(@celular, celular),
            correo           = COALESCE(@correo, correo),
            observacionficha = COALESCE(@observacionficha, observacionficha),
            fechaafiliacion  = COALESCE(@fechaafiliacion, fechaafiliacion),
            estado           = COALESCE(@estado, estado),
            -- 👇 nombres correctos de columnas:
            usuariomodificacion = @usuario,
            fechamodificacion   = NOW()
        WHERE idafiliacion = @id;";

            static object DbNull(string? s) => string.IsNullOrWhiteSpace(s) ? DBNull.Value : s!;
            static object DbNullInt(int? i) => i.HasValue ? i.Value : DBNull.Value;
            static object DbNullDate(string? ymd)
                => string.IsNullOrWhiteSpace(ymd) ? DBNull.Value
                   : (DateTime.TryParse(ymd, out var dt) ? dt.Date : DBNull.Value);

            var codubicacion = dto.dd ?? dto.pp ?? dto.rr;

            await using var cn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
            await cn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", idafiliacion);
            cmd.Parameters.Add(new NpgsqlParameter("@numficha", NpgsqlDbType.Varchar) { Value = DbNull(dto.numficha) });
            cmd.Parameters.Add(new NpgsqlParameter("@idtipodocumento", NpgsqlDbType.Integer) { Value = DbNullInt(dto.idtipodocumento) });
            cmd.Parameters.Add(new NpgsqlParameter("@docafiliado", NpgsqlDbType.Varchar) { Value = DbNull(dto.docafiliado) });
            cmd.Parameters.Add(new NpgsqlParameter("@nombres", NpgsqlDbType.Varchar) { Value = DbNull(dto.nombres) });
            cmd.Parameters.Add(new NpgsqlParameter("@apellidopaterno", NpgsqlDbType.Varchar) { Value = DbNull(dto.apellidopaterno) });
            cmd.Parameters.Add(new NpgsqlParameter("@apellidomaterno", NpgsqlDbType.Varchar) { Value = DbNull(dto.apellidomaterno) });
            cmd.Parameters.Add(new NpgsqlParameter("@fechanacimiento", NpgsqlDbType.Date) { Value = DbNullDate(dto.fechanacimiento) });
            cmd.Parameters.Add(new NpgsqlParameter("@edadafiliado", NpgsqlDbType.Integer) { Value = DbNullInt(dto.edadafiliado) });
            cmd.Parameters.Add(new NpgsqlParameter("@sexo", NpgsqlDbType.Varchar) { Value = DbNull(dto.sexo) });
            cmd.Parameters.Add(new NpgsqlParameter("@idestadocivil", NpgsqlDbType.Integer) { Value = DbNullInt(dto.idestadocivil) });
            cmd.Parameters.Add(new NpgsqlParameter("@lugarnacimiento", NpgsqlDbType.Varchar) { Value = DbNull(dto.lugarnacimiento) });
            cmd.Parameters.Add(new NpgsqlParameter("@codubicacion", NpgsqlDbType.Varchar) { Value = DbNull(codubicacion) });
            cmd.Parameters.Add(new NpgsqlParameter("@avenida", NpgsqlDbType.Varchar) { Value = DbNull(dto.avenida) });
            cmd.Parameters.Add(new NpgsqlParameter("@numero", NpgsqlDbType.Varchar) { Value = DbNull(dto.numero) });
            cmd.Parameters.Add(new NpgsqlParameter("@urbanizacion", NpgsqlDbType.Varchar) { Value = DbNull(dto.urbanizacion) });
            cmd.Parameters.Add(new NpgsqlParameter("@celular", NpgsqlDbType.Varchar) { Value = DbNull(dto.telefono) });
            cmd.Parameters.Add(new NpgsqlParameter("@correo", NpgsqlDbType.Varchar) { Value = DbNull(dto.correo) });
            cmd.Parameters.Add(new NpgsqlParameter("@observacionficha", NpgsqlDbType.Varchar) { Value = DbNull(dto.observacion) });
            cmd.Parameters.Add(new NpgsqlParameter("@fechaafiliacion", NpgsqlDbType.Date) { Value = DbNullDate(dto.fechaafiliacion) });
            cmd.Parameters.Add(new NpgsqlParameter("@estado", NpgsqlDbType.Integer) { Value = DbNullInt(dto.estado) });
            cmd.Parameters.AddWithValue("@usuario", usuario);

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows;
        }



        public async Task<IEnumerable<ListarTipoDocumento>> ListarTiposDocumento(int idemppaisnegcue)
        {
            var resultados = new List<ListarTipoDocumento>();

            const string sql = @"
                                SELECT *
                                FROM intranet.usp_getlistartipodocumentos(@idemppaisnegcue);";

            try
            {
                await using var cn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await cn.OpenAsync();

                await using var cmd = new NpgsqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idemppaisnegcue", (object)idemppaisnegcue ?? DBNull.Value);

                await using var rd = await cmd.ExecuteReaderAsync();
                while (await rd.ReadAsync())
                {
                    var item = new ListarTipoDocumento
                    {
                        idtipodocumento = rd["idtipodocumento"] == DBNull.Value ? 0 : Convert.ToInt32(rd["idtipodocumento"]),
                        nombretipodocumento = rd["nombretipodocumento"] == DBNull.Value ? null : rd["nombretipodocumento"].ToString(),
                        abreviatura = rd["abreviatura"] == DBNull.Value ? null : rd["abreviatura"].ToString(),
                        idemppaisnegcue = rd["idemppaisnegcue"] == DBNull.Value ? 0 : Convert.ToInt32(rd["idemppaisnegcue"]),
                        estado = rd["estado"] == DBNull.Value ? 0 : Convert.ToInt32(rd["estado"])
                    };
                    resultados.Add(item);
                }
            }
            catch (NpgsqlException npgEx)
            {
                throw new ApplicationException("Error de base de datos al listar tipos de documento.", npgEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error inesperado al listar tipos de documento.", ex);
            }

            return resultados;
        }

        public async Task<RespuestaAfiliacionDesactivar> PostDesactivarAfiliacion(FiltroAfiliacionDesactivar filtro)
        {
            const string sql = @"
                        SELECT *
                        FROM intranet.usp_postdesactivarafiliacion(@idafiliacion, @usuarioanulacion);";

            try
            {
                await using var cn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
                await cn.OpenAsync();

                await using var cmd = new NpgsqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idafiliacion", (object)filtro.idafiliacion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@usuarioanulacion", (object)filtro.usuarioanulacion ?? DBNull.Value);

                await using var rd = await cmd.ExecuteReaderAsync();

                if (await rd.ReadAsync())
                {
                    return new RespuestaAfiliacionDesactivar
                    {
                        success = rd["success"] != DBNull.Value && Convert.ToBoolean(rd["success"]),
                        message = rd["message"] == DBNull.Value ? null : rd["message"].ToString()
                    };
                }

                return new RespuestaAfiliacionDesactivar
                {
                    success = false,
                    message = "No se recibió respuesta de la base de datos"
                };
            }
            catch (NpgsqlException npgEx)
            {
                return new RespuestaAfiliacionDesactivar
                {
                    success = false,
                    message = $"Error de BD: {npgEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new RespuestaAfiliacionDesactivar
                {
                    success = false,
                    message = $"Error inesperado: {ex.Message}"
                };
            }
        }

    }
}
