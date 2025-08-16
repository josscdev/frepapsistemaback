using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Inventario;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_Allocation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_TPF.MGM_InventarioTPF
{
    public class InventarioTPFRepository : IInventarioTPFRepository
    {
        private readonly DataAcces _dbConnection;

        public InventarioTPFRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<Inventario.ListarInventario>> GetInventarioTPF(Inventario.InventarioFiltroListar request)
        {
            var inventarioList = new List<Inventario.ListarInventario>();

            using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("USP_GETINVENTARIO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@idpdv", request.idpdv);
                    command.Parameters.AddWithValue("@fechainicio", request.fechainicio);
                    command.Parameters.AddWithValue("@fechafin", request.fechafin);
                    command.Parameters.AddWithValue("@docusuario", request.docusuario);
                    command.Parameters.AddWithValue("@idemppaisnegcue", request.idemppaisnegcue);
                    command.Parameters.AddWithValue("@perfil", request.perfil);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            inventarioList.Add(new Inventario.ListarInventario
                            {
                                idinventario = reader["idinventario"] != DBNull.Value ? Convert.ToInt32(reader["idinventario"]) : 0,
                                docpromotorasesor = reader["docpromotorasesor"]?.ToString(),
                                fecharegistro = Convert.ToDateTime(reader["fecharegistro"]),
                                nombrepdv = reader["nombrepdv"]?.ToString(),
                                idpdv = reader["idpdv"] != DBNull.Value ? Convert.ToInt32(reader["idpdv"]) : 0,
                                nombrecompleto = reader["nombrecompleto"]?.ToString(),
                            });
                        }
                    }
                }
            }

            return inventarioList;
        }

        public async Task<Inventario.InventarioResultado> PostInventarioMasivoTPF(Inventario.InsertarInventario inventario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_POSTINVENTARIO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@docpromotorasesor", SqlDbType.VarChar, 15) { Value = (object)inventario.docpromotorasesor ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idpdv", SqlDbType.Int) { Value = (object)inventario.idpdv ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idemppaisnegcue", SqlDbType.Int) { Value = (object)inventario.idemppaisnegcue ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@usuario", SqlDbType.VarChar, 15) { Value = (object)inventario.usuario ?? DBNull.Value });

                        SqlParameter tvpParam = command.Parameters.AddWithValue("@Detalles", CreateInventarioDetalleDataTable(inventario.detalles));
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.InventarioDetalleType";

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var result = new Inventario.InventarioResultado
                                {
                                    Estado = reader["Estado"].ToString(),
                                    Mensaje = reader["Mensaje"]?.ToString(),
                                    idinventario = reader["idinventario"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idinventario")) : 0
                                };

                                return result;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new Inventario.InventarioResultado { Estado = "Error", Mensaje = "Error: " + ex.Message };
            }

            return new Inventario.InventarioResultado { Estado = "Error", Mensaje = "Unknown error occurred." };
        }

        private DataTable CreateInventarioDetalleDataTable(List<Inventario.InsertarInventarioDetalle> detalles)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("imeiequipo", typeof(string));
            dt.Columns.Add("idemppaisnegcue", typeof(int));
            dt.Columns.Add("usuario", typeof(string));
            foreach (var detalle in detalles)
            {
                dt.Rows.Add(
                    detalle.imeiequipo,
                    detalle.idemppaisnegcue,
                    detalle.usuario
                    );
            }
            return dt;
        }

        public async Task<Inventario.InventarioResultado> DeleteInventarioTPF(int idinventario, string usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("USP_DELETEINVENTARIO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@idinventario", SqlDbType.Int) { Value = idinventario });
                        command.Parameters.Add(new SqlParameter("@usuario", SqlDbType.VarChar, 15) { Value = usuario });
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var result = new Inventario.InventarioResultado
                                {
                                    Estado = reader["Estado"].ToString(),
                                    Mensaje = reader["Mensaje"]?.ToString()
                                };
                                return result;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new Inventario.InventarioResultado { Estado = "Error", Mensaje = "Error: " + ex.Message };
            }
            return new Inventario.InventarioResultado { Estado = "Error", Mensaje = "Unknown error occurred." };
        }

        public async Task<List<Inventario.ListarInventarioDetalle>> GetInventarioDetalleTPF(int idinventario)
        {
            var inventarioDetalleList = new List<Inventario.ListarInventarioDetalle>();

            using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("USP_GETINVENTARIODETALLE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@idinventario", idinventario);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            inventarioDetalleList.Add(new Inventario.ListarInventarioDetalle
                            {
                                idinventariodetalle = reader["idinventariodetalle"] != DBNull.Value ? Convert.ToInt32(reader["idinventariodetalle"]) : 0,
                                idinventario = reader["idinventario"] != DBNull.Value ? Convert.ToInt32(reader["idinventario"]) : 0,
                                imeiequipo = reader["imeiequipo"]?.ToString(),
                                idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                usuariocreacion = reader["usuariocreacion"]?.ToString(),
                                fechacreacion = reader["fechacreacion"] != DBNull.Value ? Convert.ToDateTime(reader["fechacreacion"]) : (DateTime?)null,
                                usuariomodificacion = reader["usuariomodificacion"]?.ToString(),
                                fechamodificacion = reader["fechamodificacion"] != DBNull.Value ? Convert.ToDateTime(reader["fechamodificacion"]) : (DateTime?)null,
                                usuarioanulacion = reader["usuarioanulacion"]?.ToString(),
                                fechaanulacion = reader["fechaanulacion"] != DBNull.Value ? Convert.ToDateTime(reader["fechaanulacion"]) : (DateTime?)null,
                                estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                            });
                        }
                    }
                }
            }

            return inventarioDetalleList;
        }

        public async Task<Inventario.InventarioResultado> DeleteInventarioDetalleTPF(int idinventariodetalle, string usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("USP_DELETEINVENTARIODETALLE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@idinventariodetalle", SqlDbType.Int) { Value = idinventariodetalle });
                        command.Parameters.Add(new SqlParameter("@usuario", SqlDbType.VarChar, 15) { Value = usuario });
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var result = new Inventario.InventarioResultado
                                {
                                    Estado = reader["Estado"].ToString(),
                                    Mensaje = reader["Mensaje"]?.ToString()
                                };
                                return result;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new Inventario.InventarioResultado { Estado = "Error", Mensaje = "Error: " + ex.Message };
            }
            return new Inventario.InventarioResultado { Estado = "Error", Mensaje = "Unknown error occurred." };
        }

        public async Task<Inventario.InventarioResultado> PostInventarioDetalleTPF(Inventario.InsertarSoloInventarioDetalle inventario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_POSTINVENTARIODETALLE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@idinventario", SqlDbType.Int) { Value = (object)inventario.idinventario ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@usuario", SqlDbType.VarChar, 15) { Value = (object)inventario.usuario ?? DBNull.Value });

                        SqlParameter tvpParam = command.Parameters.AddWithValue("@Detalles", CreateInventarioDetalleDataTable(inventario.detalles));
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.InventarioDetalleType";

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var result = new Inventario.InventarioResultado
                                {
                                    Estado = reader["Estado"].ToString(),
                                    Mensaje = reader["Mensaje"]?.ToString(),
                                    idinventario = reader["idinventario"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idinventario")) : 0
                                };

                                return result;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new Inventario.InventarioResultado { Estado = "Error", Mensaje = "Error: " + ex.Message };
            }

            return new Inventario.InventarioResultado { Estado = "Error", Mensaje = "Unknown error occurred." };
        }
    }
}
