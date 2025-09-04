using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
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
    }
}
