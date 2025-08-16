using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.OracleLogistica;

namespace RombiBack.Repository.ROM.ENTEL_TPF.MGM_InventarioTPF
{
    public class OracleLogisticaTPFRepository: IOracleLogisticaTPFRepository
    {
        private readonly DataAcces _dbConnection;
        public OracleLogisticaTPFRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ResultadoCargaMasiva> PostOracleLogisticaMasivoTPF(List<OracleLogistica> registros, string usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("USP_POSTORACLELOGISTICA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Param 1: Usuario
                        command.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.NVarChar, 50) { Value = (object)usuario ?? DBNull.Value });
                        // Param 2: TVP
                        var tvpParam = command.Parameters.AddWithValue("@Data", CreateOracleLogisticaDataTable(registros));
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.OracleLogisticaType";
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new ResultadoCargaMasiva
                                {
                                    Estado = reader["Estado"].ToString(),
                                    Mensaje = reader["Mensaje"]?.ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultadoCargaMasiva { Estado = "Error", Mensaje = $"Error: {ex.Message}" };
            }
            return null;
        }

        private DataTable CreateOracleLogisticaDataTable(List<OracleLogistica> registros)
        {
            var table = new DataTable();

            table.Columns.Add("PdvRom", typeof(string));
            table.Columns.Add("Supervisor", typeof(string));
            table.Columns.Add("Pareto", typeof(string));
            table.Columns.Add("GestorRom", typeof(string));
            table.Columns.Add("Categoria", typeof(string));
            table.Columns.Add("Territorio", typeof(string));
            table.Columns.Add("TipoArticulo", typeof(string));
            table.Columns.Add("CodigoOracle", typeof(string));
            table.Columns.Add("Descripcion", typeof(string));
            table.Columns.Add("SerieSim", typeof(string));
            table.Columns.Add("SerieFicticio", typeof(string));
            table.Columns.Add("Valorizado", typeof(string));
            table.Columns.Add("FechaRecepcion", typeof(string));
            table.Columns.Add("StatusActual", typeof(string));
            table.Columns.Add("TipoAlmacen", typeof(string));
            table.Columns.Add("DiasStock", typeof(string));
            table.Columns.Add("LargoSim", typeof(string));
            table.Columns.Add("LargoFicticio", typeof(string));
            table.Columns.Add("ModeloUnico", typeof(string));
            table.Columns.Add("SubInventario", typeof(string));

            foreach (var item in registros)
            {
                table.Rows.Add(
                    item.PdvRom, item.Supervisor, item.Pareto, item.GestorRom, item.Categoria, item.Territorio,
                    item.TipoArticulo, item.CodigoOracle, item.Descripcion, item.SerieSim, item.SerieFicticio,
                    item.Valorizado, item.FechaRecepcion, item.StatusActual, item.TipoAlmacen, item.DiasStock,
                    item.LargoSim, item.LargoFicticio, item.ModeloUnico, item.SubInventario
                );
            }

            return table;
        }

        public async Task<List<MatchResumenDto>> ObtenerConciliacionesAsyncTPF(FiltroInsertarConciliar filtro)
        {
            var lista = new List<MatchResumenDto>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_MATCHORACLEINVENTARIO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        // Parámetros tal cual como VARCHAR
                        command.Parameters.AddWithValue("@FechaInicioInv", filtro.fechainicioinv);
                        command.Parameters.AddWithValue("@FechaFinInv", filtro.fechafininv);
                        command.Parameters.AddWithValue("@FechaInicioLog", filtro.fechainiciolog);
                        command.Parameters.AddWithValue("@FechaFinLog", filtro.fechafinlog);
                        command.Parameters.AddWithValue("@IdEmpPaisNegcue", filtro.idemppaisnegcue);
                        command.Parameters.AddWithValue("@Usuario", filtro.usuario);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new MatchResumenDto
                                {
                                    idconciloracleinv = reader.GetInt32(reader.GetOrdinal("idconciloracleinv")),
                                    pdvrom = reader["pdvrom"]?.ToString(),
                                    idpdv = reader.GetInt32(reader.GetOrdinal("idpdv")),
                                    nombrepdv = reader["nombrepdv"]?.ToString(),
                                    docpromotorasesor = reader["docpromotorasesor"]?.ToString(),
                                    imeiequipo = reader["imeiequipo"]?.ToString(),
                                    imeiserieficticio = reader["imeiserieficticio"]?.ToString(),
                                    fechacargaol = reader["fechacargaol"] as DateTime?,
                                    fecharegistroinv = reader["fecharegistroinv"] as DateTime?,
                                    imeiflagestado = reader["imeiflagestado"]?.ToString(),
                                    fechaconciliacion = reader["fechaconciliacion"] as DateTime?,
                                    idemppaisnegcue = reader.GetInt32(reader.GetOrdinal("idemppaisnegcue")),
                                    estado = reader.GetInt32(reader.GetOrdinal("estado")),
                                    usuariocreacion = reader["usuariocreacion"]?.ToString(),
                                    fechacreacion = reader["fechacreacion"] as DateTime?,
                                    usuariomodificacion = reader["usuariomodificacion"]?.ToString(),
                                    fechamodificacion = reader["fechamodificacion"] as DateTime?,
                                    usuarioanulacion = reader["usuarioanulacion"]?.ToString(),
                                    fechaanulacion = reader["fechaanulacion"] as DateTime?
                                };

                                lista.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el SP USP_MATCHORACLEINVENTARIO", ex);
            }

            return lista;
        }
    }
}
