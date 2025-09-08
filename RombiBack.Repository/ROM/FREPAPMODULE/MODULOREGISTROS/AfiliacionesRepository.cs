using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    var item = new ListarAfiliacion
                    {
                        idafiliacion = GetInt(rd, "idafiliacion"),
                        numficha = GetString(rd, "numficha"),
                        docafiliado = GetString(rd, "docafiliado"),
                        nombres = GetString(rd, "nombres"),
                        apellidopaterno = GetString(rd, "apellidopaterno"),
                        apellidomaterno = GetString(rd, "apellidomaterno"),
                        fechaafiliacion = GetDateTime(rd, "fechaafiliacion"),
                        edadafiliado = GetInt(rd, "edadafiliado"),
                        estado = GetInt(rd, "estado"),
                        estado_text = GetString(rd, "estado_text"),
                        codubicacion = GetString(rd, "codubicacion"),
                        region = GetString(rd, "region"),
                        provincia = GetString(rd, "subregion"),
                        distrito = GetString(rd, "localidad"),
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
                    numficha, docafiliado, nombres, apellidopaterno, apellidomaterno,
                    fechanacimiento, edadafiliado,
                    codubicacion, avenida, numero, urbanizacion,
                    celular, correo, observacionficha,
                    fechaafiliacion, estado,
                    usuariocreacion, fechacreacion
                )
                VALUES
                (
                    @numficha, @docafiliado, @nombres, @apellidopaterno, @apellidomaterno,
                    @fechanacimiento, @edadafiliado,
                    @codubicacion, @avenida, @numero, @urbanizacion,
                    @celular, @correo, @observacionficha,
                    @fechaafiliacion, @estado,
                    @usuariocreacion, NOW()
                )
                RETURNING idafiliacion;";

            await using var cn = new NpgsqlConnection(_dbConnection.GetConnectionROMBI());
            await cn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, cn);

            // helpers de parseo y nulos
            static object DbNull(string? s) => string.IsNullOrWhiteSpace(s) ? DBNull.Value : s!;
            static object DbNullInt(int? i) => i.HasValue ? i.Value : DBNull.Value;
            static object DbNullDate(string? ymd)
            {
                if (string.IsNullOrWhiteSpace(ymd)) return DBNull.Value;
                // acepta "YYYY-MM-DD"
                if (DateTime.TryParse(ymd, out var dt))
                    return dt.Date;
                return DBNull.Value;
            }

            var codubicacion = dto.dd ?? dto.pp ?? dto.rr; // usa el más específico disponible

            cmd.Parameters.Add(new NpgsqlParameter("@numficha", NpgsqlDbType.Varchar) { Value = DbNull(dto.numficha) });
            cmd.Parameters.Add(new NpgsqlParameter("@docafiliado", NpgsqlDbType.Varchar) { Value = DbNull(dto.docafiliado) });
            cmd.Parameters.Add(new NpgsqlParameter("@nombres", NpgsqlDbType.Varchar) { Value = dto.nombres });
            cmd.Parameters.Add(new NpgsqlParameter("@apellidopaterno", NpgsqlDbType.Varchar) { Value = dto.apellidopaterno });
            cmd.Parameters.Add(new NpgsqlParameter("@apellidomaterno", NpgsqlDbType.Varchar) { Value = DbNull(dto.apellidomaterno) });

            cmd.Parameters.Add(new NpgsqlParameter("@fechanacimiento", NpgsqlDbType.Date) { Value = DbNullDate(dto.fechanacimiento) });
            cmd.Parameters.Add(new NpgsqlParameter("@edadafiliado", NpgsqlDbType.Integer) { Value = dto.edadafiliado });

            cmd.Parameters.Add(new NpgsqlParameter("@codubicacion", NpgsqlDbType.Varchar) { Value = DbNull(codubicacion) });
            cmd.Parameters.Add(new NpgsqlParameter("@avenida", NpgsqlDbType.Varchar) { Value = DbNull(dto.avenida) });
            cmd.Parameters.Add(new NpgsqlParameter("@numero", NpgsqlDbType.Varchar) { Value = DbNull(dto.numero) });
            cmd.Parameters.Add(new NpgsqlParameter("@urbanizacion", NpgsqlDbType.Varchar) { Value = DbNull(dto.urbanizacion) });

            cmd.Parameters.Add(new NpgsqlParameter("@celular", NpgsqlDbType.Varchar) { Value = DbNull(dto.telefono) });
            cmd.Parameters.Add(new NpgsqlParameter("@correo", NpgsqlDbType.Varchar) { Value = dto.correo });
            cmd.Parameters.Add(new NpgsqlParameter("@observacionficha", NpgsqlDbType.Varchar) { Value = DbNull(dto.observacion) });

            cmd.Parameters.Add(new NpgsqlParameter("@fechaafiliacion", NpgsqlDbType.Date) { Value = DbNullDate(dto.fechaafiliacion) });
            cmd.Parameters.Add(new NpgsqlParameter("@estado", NpgsqlDbType.Integer) { Value = dto.estado });

            cmd.Parameters.Add(new NpgsqlParameter("@usuariocreacion", NpgsqlDbType.Varchar) { Value = usuario });

            var id = await cmd.ExecuteScalarAsync();
            return (id is long l) ? l : Convert.ToInt64(id);
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
    }
}
