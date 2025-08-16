using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using TwoFactorAuthNet;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Ventas
{
    public class VentasRepository:IVentasRepository
    {
        private readonly DataAcces _dbConnection;
        private readonly string _AWSMessageCA;

        public VentasRepository(DataAcces dbConnection, IConfiguration configuration)
        {
            _dbConnection = dbConnection;
            var awsOptions = configuration.GetSection("AWSCredentials");
            _AWSMessageCA = awsOptions["AWSMessageCA"];
        }
        public async Task<List<TipoDocumentoResponse>> GetTipoDocumento(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTIPODOCUMENTO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<TipoDocumentoResponse> response = new List<TipoDocumentoResponse>();
                            while(await reader.ReadAsync())
                            {
                                TipoDocumentoResponse tipoDocumentoResponse = new TipoDocumentoResponse()
                                {
                                    idtipodocumento = reader["idtipodocumento"] != DBNull.Value ? Convert.ToInt32(reader["idtipodocumento"]) : 0,
                                    nombretipodocumento = reader["nombretipodocumento"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(tipoDocumentoResponse);
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
        public async Task<List<TipoBiometriaResponse>> GetTipoBiometria(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTIPOBIOMETRIA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<TipoBiometriaResponse> response = new List<TipoBiometriaResponse>();
                            while(await reader.ReadAsync())
                            {
                                TipoBiometriaResponse tipoBiometriaResponse = new TipoBiometriaResponse()
                                {
                                    idtipobiometria = reader["idtipobiometria"] != DBNull.Value ? Convert.ToInt32(reader["idtipobiometria"]) : 0,
                                    nombretipobiometria = reader["nombretipobiometria"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(tipoBiometriaResponse);
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
        public async Task<List<SubproductoResponse>> GetSubproducto(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETSUBPRODUCTO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<SubproductoResponse> response = new List<SubproductoResponse>();
                            while(await reader.ReadAsync())
                            {
                                SubproductoResponse subproductoResponse = new SubproductoResponse()
                                {
                                    idsubproducto = reader["idsubproducto"] != DBNull.Value ? Convert.ToInt32(reader["idsubproducto"]) : 0,
                                    nombresubproducto = reader["nombresubproducto"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(subproductoResponse);
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
        public async Task<List<OperadorReponse>> GetOperador(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETOPERADOR", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<OperadorReponse> response = new List<OperadorReponse>();
                            while(await reader.ReadAsync())
                            {
                                OperadorReponse operadorReponse = new OperadorReponse()
                                {
                                    idoperador = reader["idoperador"] != DBNull.Value ? Convert.ToInt32(reader["idoperador"]) : 0,
                                    nombreoperador = reader["nombreoperador"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(operadorReponse);
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
        public async Task<List<TipoEquipoResponse>> GetTipoEquipo(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTIPOEQUIPO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<TipoEquipoResponse> response = new List<TipoEquipoResponse>();
                            while(await reader.ReadAsync())
                            {
                                TipoEquipoResponse tipoEquipoResponse = new TipoEquipoResponse()
                                {
                                    idtipoequipo = reader["idtipoequipo"] != DBNull.Value ? Convert.ToInt32(reader["idtipoequipo"]) : 0,
                                    nombretipoequipo = reader["nombretipoequipo"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(tipoEquipoResponse);
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

        public async Task<List<TipoSeguroResponse>> GetTipoSeguro(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTIPOSEGURO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<TipoSeguroResponse> response = new List<TipoSeguroResponse>();
                            while (await reader.ReadAsync())
                            {
                                TipoSeguroResponse tipoEtiquetaResponse = new TipoSeguroResponse()
                                {
                                    idtiposeguro = reader["idtiposeguro"] != DBNull.Value ? Convert.ToInt32(reader["idtiposeguro"]) : 0,
                                    nombretiposeguro = reader["nombretiposeguro"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(tipoEtiquetaResponse);
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

        public async Task<List<TipoEtiquetaResponse>> GetTipoEtiqueta(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTIPOETIQUETA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<TipoEtiquetaResponse> response = new List<TipoEtiquetaResponse>();
                            while(await reader.ReadAsync())
                            {
                                TipoEtiquetaResponse tipoEtiquetaResponse = new TipoEtiquetaResponse()
                                {
                                    idtipoetiqueta = reader["idtipoetiqueta"] != DBNull.Value ? Convert.ToInt32(reader["idtipoetiqueta"]) : 0,
                                    nombretipoetiqueta = reader["nombretipoetiqueta"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(tipoEtiquetaResponse);
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
        public async Task<List<TipoPagoResponse>> GetTipoPago(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTIPOPAGO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<TipoPagoResponse> response = new List<TipoPagoResponse>();
                            while(await reader.ReadAsync())
                            {
                                TipoPagoResponse tipoPagoResponse = new TipoPagoResponse()
                                {
                                    idtipopago = reader["idtipopago"] != DBNull.Value ? Convert.ToInt32(reader["idtipopago"]) : 0,
                                    nombretipopago = reader["nombretipopago"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(tipoPagoResponse);
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
        public async Task<List<PlanesResponse>> GetPlanes(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETPLANES", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<PlanesResponse> response = new List<PlanesResponse>();
                            while(await reader.ReadAsync())
                            {
                                PlanesResponse planesResponse = new PlanesResponse()
                                {
                                    idplan = reader["idplan"] != DBNull.Value ? Convert.ToInt32(reader["idplan"]) : 0,
                                    nombreplan = reader["nombreplan"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(planesResponse);
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
        public async Task<List<ModeloResponse>> GetModelo(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETMODELO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<ModeloResponse> response = new List<ModeloResponse>();
                            while(await reader.ReadAsync())
                            {
                                ModeloResponse modeloResponse = new ModeloResponse()
                                {
                                    idmodelo = reader["idmodelo"] != DBNull.Value ? Convert.ToInt32(reader["idmodelo"]) : 0,
                                    nombremodelo = reader["nombremodelo"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(modeloResponse);
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
        public async Task<List<BundleResponse>> GetBundle(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETBUNDLEALL", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<BundleResponse> response = new List<BundleResponse>();
                            while(await reader.ReadAsync())
                            {
                                BundleResponse bundleResponse = new BundleResponse()
                                {
                                    idbundle = reader["idbundle"] != DBNull.Value ? Convert.ToInt32(reader["idbundle"]) : 0,
                                    nombrebundle = reader["nombrebundle"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(bundleResponse);
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
        public async Task<List<TipoAccesorioResponse>> GetTipoAccesorio(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTIPOACCESORIO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<TipoAccesorioResponse> response = new List<TipoAccesorioResponse>();
                            while(await reader.ReadAsync())
                            {
                                TipoAccesorioResponse tipoAccesorioResponse = new TipoAccesorioResponse()
                                {
                                    idtipoaccesorio = reader["idtipoaccesorio"] != DBNull.Value ? Convert.ToInt32(reader["idtipoaccesorio"]) : 0,
                                    nombretipoaccesorio = reader["nombretipoaccesorio"]?.ToString() ?? "",
                                    idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                    estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0
                                };

                                response.Add(tipoAccesorioResponse);
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
        public async Task<List<IMEIequiposResponse>> GetIMEISequipos(int idusuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETIMEIEQUIPOS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@intIDUsuario", SqlDbType.Int).Value = idusuario;

                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<IMEIequiposResponse> response = new List<IMEIequiposResponse>();
                            while(await reader.ReadAsync())
                            {
                                IMEIequiposResponse imeiequiposResponse = new IMEIequiposResponse()
                                {
                                    iemiequipo = reader["IMEIEquipo"]?.ToString() ?? "",
                                    iemiequipoDescripcion = reader["IMEIEquipoDescripcion"]?.ToString() ?? "",
                                    flagpicking = reader["FlagPicking"] != DBNull.Value ? Convert.ToInt32(reader["FlagPicking"]) : 0,
                                };

                                response.Add(imeiequiposResponse);
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

        public async Task<List<VentasDetalleTotal>> GetVentasAdmin(FiltrarVentasPerfiles filtros)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETVENTASADMINPDV3", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.Add("@idjefe", SqlDbType.VarChar).Value = filtros.idjefe;
                        command.Parameters.Add("@fechainicial", SqlDbType.Date).Value = filtros.fechainicial;
                        command.Parameters.Add("@fechafinal", SqlDbType.Date).Value = filtros.fechafinal;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = filtros.@idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<VentasDetalleTotal> response = new List<VentasDetalleTotal>();
                            while (await reader.ReadAsync())
                            {
                                VentasDetalleTotal ventasresponse = new VentasDetalleTotal();


                                ventasresponse.idventasdetalle = reader.IsDBNull(reader.GetOrdinal("idventasdetalle")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idventasdetalle"));
                                ventasresponse.idventas = reader.IsDBNull(reader.GetOrdinal("idventas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idventas"));
                                ventasresponse.fechaoperacion = reader.IsDBNull(reader.GetOrdinal("fechaoperacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaoperacion"));
                                ventasresponse.docpromotorasesor = reader.IsDBNull(reader.GetOrdinal("docpromotorasesor")) ? null : reader.GetString(reader.GetOrdinal("docpromotorasesor"));
                                ventasresponse.idpdv = reader.IsDBNull(reader.GetOrdinal("idpdv")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idpdv"));
                                ventasresponse.nombrepdv = reader.IsDBNull(reader.GetOrdinal("nombrepdv")) ? null : reader.GetString(reader.GetOrdinal("nombrepdv"));
                                ventasresponse.idtipodocumento = reader.IsDBNull(reader.GetOrdinal("idtipodocumento")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipodocumento"));
                                ventasresponse.nombretipodocumento = reader.IsDBNull(reader.GetOrdinal("nombretipodocumento")) ? null : reader.GetString(reader.GetOrdinal("nombretipodocumento"));
                                ventasresponse.doccliente = reader.IsDBNull(reader.GetOrdinal("doccliente")) ? null : reader.GetString(reader.GetOrdinal("doccliente"));
                                ventasresponse.idtipobiometria = reader.IsDBNull(reader.GetOrdinal("idtipobiometria")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipobiometria"));
                                ventasresponse.nombretipobiometria = reader.IsDBNull(reader.GetOrdinal("nombretipobiometria")) ? null : reader.GetString(reader.GetOrdinal("nombretipobiometria"));
                                ventasresponse.numcelularcontrato = reader.IsDBNull(reader.GetOrdinal("numcelularcontrato")) ? null : reader.GetString(reader.GetOrdinal("numcelularcontrato"));
                                ventasresponse.correocliente = reader.IsDBNull(reader.GetOrdinal("correocliente")) ? null : reader.GetString(reader.GetOrdinal("correocliente"));
                                ventasresponse.observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString(reader.GetOrdinal("observacion"));
                                ventasresponse.nombrevoucher = reader.IsDBNull(reader.GetOrdinal("nombrevoucher")) ? null : reader.GetString(reader.GetOrdinal("nombrevoucher"));
                                ventasresponse.numeroruc = reader.IsDBNull(reader.GetOrdinal("numeroruc")) ? null : reader.GetString(reader.GetOrdinal("numeroruc"));
                                ventasresponse.codcomprobante = reader.IsDBNull(reader.GetOrdinal("codcomprobante")) ? null : reader.GetString(reader.GetOrdinal("codcomprobante"));
                                ventasresponse.numeroserie = reader.IsDBNull(reader.GetOrdinal("numeroserie")) ? null : reader.GetString(reader.GetOrdinal("numeroserie"));
                                ventasresponse.numero = reader.IsDBNull(reader.GetOrdinal("numero")) ? null : reader.GetString(reader.GetOrdinal("numero"));
                                ventasresponse.fechaemisionvoucher = reader.IsDBNull(reader.GetOrdinal("fechaemisionvoucher")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaemisionvoucher"));
                                ventasresponse.montovoucher = reader.IsDBNull(reader.GetOrdinal("montovoucher")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("montovoucher"));
                                ventasresponse.tramaqrcode = reader.IsDBNull(reader.GetOrdinal("tramaqrcode")) ? null : reader.GetString(reader.GetOrdinal("tramaqrcode"));
                                ventasresponse.idsubproducto = reader.IsDBNull(reader.GetOrdinal("idsubproducto")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idsubproducto"));
                                ventasresponse.nombresubproducto = reader.IsDBNull(reader.GetOrdinal("nombresubproducto")) ? null : reader.GetString(reader.GetOrdinal("nombresubproducto"));
                                ventasresponse.idoperador = reader.IsDBNull(reader.GetOrdinal("idoperador")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idoperador"));
                                ventasresponse.nombreoperador = reader.IsDBNull(reader.GetOrdinal("nombreoperador")) ? null : reader.GetString(reader.GetOrdinal("nombreoperador"));
                                ventasresponse.idtipoequipo = reader.IsDBNull(reader.GetOrdinal("idtipoequipo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoequipo"));
                                ventasresponse.nombretipoequipo = reader.IsDBNull(reader.GetOrdinal("nombretipoequipo")) ? null : reader.GetString(reader.GetOrdinal("nombretipoequipo"));
                                ventasresponse.idtiposeguro = reader.IsDBNull(reader.GetOrdinal("idtiposeguro")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtiposeguro"));
                                ventasresponse.nombretiposeguro = reader.IsDBNull(reader.GetOrdinal("nombretiposeguro")) ? null : reader.GetString(reader.GetOrdinal("nombretiposeguro"));
                                ventasresponse.montoupselling = reader.IsDBNull(reader.GetOrdinal("montoupselling")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("montoupselling"));

                                ventasresponse.idtipoetiqueta = reader.IsDBNull(reader.GetOrdinal("idtipoetiqueta")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoetiqueta"));
                                ventasresponse.nombretipoetiqueta = reader.IsDBNull(reader.GetOrdinal("nombretipoetiqueta")) ? null : reader.GetString(reader.GetOrdinal("nombretipoetiqueta"));
                                ventasresponse.idtipopago = reader.IsDBNull(reader.GetOrdinal("idtipopago")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipopago"));
                                ventasresponse.nombretipopago = reader.IsDBNull(reader.GetOrdinal("nombretipopago")) ? null : reader.GetString(reader.GetOrdinal("nombretipopago"));
                                ventasresponse.idplan = reader.IsDBNull(reader.GetOrdinal("idplan")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idplan"));
                                ventasresponse.nombreplan = reader.IsDBNull(reader.GetOrdinal("nombreplan")) ? null : reader.GetString(reader.GetOrdinal("nombreplan"));
                                ventasresponse.idmodelo = reader.IsDBNull(reader.GetOrdinal("idmodelo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idmodelo"));
                                ventasresponse.nombremodelo = reader.IsDBNull(reader.GetOrdinal("nombremodelo")) ? null : reader.GetString(reader.GetOrdinal("nombremodelo"));
                                ventasresponse.imeiequipo = reader.IsDBNull(reader.GetOrdinal("imeiequipo")) ? null : reader.GetString(reader.GetOrdinal("imeiequipo"));
                                ventasresponse.imeisim = reader.IsDBNull(reader.GetOrdinal("imeisim")) ? null : reader.GetString(reader.GetOrdinal("imeisim"));
                                ventasresponse.iccid = reader.IsDBNull(reader.GetOrdinal("iccid")) ? null : reader.GetString(reader.GetOrdinal("iccid"));
                                ventasresponse.idbundle = reader.IsDBNull(reader.GetOrdinal("idbundle")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idbundle"));
                                ventasresponse.nombrebundle = reader.IsDBNull(reader.GetOrdinal("nombrebundle")) ? null : reader.GetString(reader.GetOrdinal("nombrebundle"));
                                ventasresponse.pagocaja = reader.IsDBNull(reader.GetOrdinal("pagocaja")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("pagocaja"));
                                ventasresponse.numerocelular = reader.IsDBNull(reader.GetOrdinal("numerocelular")) ? null : reader.GetString(reader.GetOrdinal("numerocelular"));
                                ventasresponse.idtipoaccesorio = reader.IsDBNull(reader.GetOrdinal("idtipoaccesorio")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoaccesorio"));
                                ventasresponse.nombretipoaccesorio = reader.IsDBNull(reader.GetOrdinal("nombretipoaccesorio")) ? null : reader.GetString(reader.GetOrdinal("nombretipoaccesorio"));
                                ventasresponse.cantidadaccesorio = reader.IsDBNull(reader.GetOrdinal("cantidadaccesorio")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidadaccesorio"));
                                ventasresponse.pagoaccesorio = reader.IsDBNull(reader.GetOrdinal("pagoaccesorio")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("pagoaccesorio"));
                                ventasresponse.imeiaccesorio = reader.IsDBNull(reader.GetOrdinal("imeiaccesorio")) ? null : reader.GetString(reader.GetOrdinal("imeiaccesorio"));
                                ventasresponse.numeroorden = reader.IsDBNull(reader.GetOrdinal("numeroorden")) ? null : reader.GetString(reader.GetOrdinal("numeroorden"));
                                ventasresponse.ventasromimeicoincide = reader.IsDBNull(reader.GetOrdinal("ventasromimeicoincide")) ? (string?)null : reader.GetString(reader.GetOrdinal("ventasromimeicoincide"));
                                ventasresponse.codigoauthbundle = reader.IsDBNull(reader.GetOrdinal("codigoauthbundle")) ? null : reader.GetString(reader.GetOrdinal("codigoauthbundle"));
                                //ventasresponse.flagcodigoauthbundle = reader.IsDBNull(reader.GetOrdinal("flagcodigoauthbundle")) ? (int?)null : reader.GetInt32(reader.GetInt32("flagcodigoauthbundle"));
                                ventasresponse.idemppaisnegcue = reader.IsDBNull(reader.GetOrdinal("idemppaisnegcue")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idemppaisnegcue"));
                                ventasresponse.nombrecuenta = reader.IsDBNull(reader.GetOrdinal("nombrecuenta")) ? null : reader.GetString(reader.GetOrdinal("nombrecuenta"));
                                ventasresponse.estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado"));
                                ventasresponse.usuariocreacion = reader.IsDBNull(reader.GetOrdinal("usuariocreacion")) ? null : reader.GetString(reader.GetOrdinal("usuariocreacion"));
                                ventasresponse.fechacreacion = reader.IsDBNull(reader.GetOrdinal("fechacreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechacreacion"));
                                ventasresponse.usuariomodificacion = reader.IsDBNull(reader.GetOrdinal("usuariomodificacion")) ? null : reader.GetString(reader.GetOrdinal("usuariomodificacion"));
                                ventasresponse.fechamodificacion = reader.IsDBNull(reader.GetOrdinal("fechamodificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechamodificacion"));
                                ventasresponse.usuarioanulacion = reader.IsDBNull(reader.GetOrdinal("usuarioanulacion")) ? null : reader.GetString(reader.GetOrdinal("usuarioanulacion"));
                                ventasresponse.fechaanulacion = reader.IsDBNull(reader.GetOrdinal("fechaanulacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaanulacion"));


                                response.Add(ventasresponse);
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

        public async Task<List<VentasDetalleTotal>> GetVentasJefe(FiltrarVentasPerfiles filtros)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETVENTASJEFEPDV3", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idjefe", SqlDbType.VarChar).Value = filtros.idjefe;
                        command.Parameters.Add("@fechainicial", SqlDbType.Date).Value = filtros.fechainicial;
                        command.Parameters.Add("@fechafinal", SqlDbType.Date).Value = filtros.fechafinal;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = filtros.@idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<VentasDetalleTotal> response = new List<VentasDetalleTotal>();
                            while (await reader.ReadAsync())
                            {
                                VentasDetalleTotal ventasresponse = new VentasDetalleTotal();


                                ventasresponse.idventasdetalle = reader.IsDBNull(reader.GetOrdinal("idventasdetalle")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idventasdetalle"));
                                ventasresponse.idventas = reader.IsDBNull(reader.GetOrdinal("idventas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idventas"));
                                ventasresponse.fechaoperacion = reader.IsDBNull(reader.GetOrdinal("fechaoperacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaoperacion"));
                                ventasresponse.docpromotorasesor = reader.IsDBNull(reader.GetOrdinal("docpromotorasesor")) ? null : reader.GetString(reader.GetOrdinal("docpromotorasesor"));
                                ventasresponse.idpdv = reader.IsDBNull(reader.GetOrdinal("idpdv")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idpdv"));
                                ventasresponse.nombrepdv = reader.IsDBNull(reader.GetOrdinal("nombrepdv")) ? null : reader.GetString(reader.GetOrdinal("nombrepdv"));
                                ventasresponse.idtipodocumento = reader.IsDBNull(reader.GetOrdinal("idtipodocumento")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipodocumento"));
                                ventasresponse.nombretipodocumento = reader.IsDBNull(reader.GetOrdinal("nombretipodocumento")) ? null : reader.GetString(reader.GetOrdinal("nombretipodocumento"));
                                ventasresponse.doccliente = reader.IsDBNull(reader.GetOrdinal("doccliente")) ? null : reader.GetString(reader.GetOrdinal("doccliente"));
                                ventasresponse.idtipobiometria = reader.IsDBNull(reader.GetOrdinal("idtipobiometria")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipobiometria"));
                                ventasresponse.nombretipobiometria = reader.IsDBNull(reader.GetOrdinal("nombretipobiometria")) ? null : reader.GetString(reader.GetOrdinal("nombretipobiometria"));
                                ventasresponse.numcelularcontrato = reader.IsDBNull(reader.GetOrdinal("numcelularcontrato")) ? null : reader.GetString(reader.GetOrdinal("numcelularcontrato"));
                                ventasresponse.correocliente = reader.IsDBNull(reader.GetOrdinal("correocliente")) ? null : reader.GetString(reader.GetOrdinal("correocliente"));
                                ventasresponse.observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString(reader.GetOrdinal("observacion"));
                                ventasresponse.nombrevoucher = reader.IsDBNull(reader.GetOrdinal("nombrevoucher")) ? null : reader.GetString(reader.GetOrdinal("nombrevoucher"));
                                ventasresponse.numeroruc = reader.IsDBNull(reader.GetOrdinal("numeroruc")) ? null : reader.GetString(reader.GetOrdinal("numeroruc"));
                                ventasresponse.codcomprobante = reader.IsDBNull(reader.GetOrdinal("codcomprobante")) ? null : reader.GetString(reader.GetOrdinal("codcomprobante"));
                                ventasresponse.numeroserie = reader.IsDBNull(reader.GetOrdinal("numeroserie")) ? null : reader.GetString(reader.GetOrdinal("numeroserie"));
                                ventasresponse.numero = reader.IsDBNull(reader.GetOrdinal("numero")) ? null : reader.GetString(reader.GetOrdinal("numero"));
                                ventasresponse.fechaemisionvoucher = reader.IsDBNull(reader.GetOrdinal("fechaemisionvoucher")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaemisionvoucher"));
                                ventasresponse.montovoucher = reader.IsDBNull(reader.GetOrdinal("montovoucher")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("montovoucher"));
                                ventasresponse.tramaqrcode = reader.IsDBNull(reader.GetOrdinal("tramaqrcode")) ? null : reader.GetString(reader.GetOrdinal("tramaqrcode"));
                                ventasresponse.idsubproducto = reader.IsDBNull(reader.GetOrdinal("idsubproducto")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idsubproducto"));
                                ventasresponse.nombresubproducto = reader.IsDBNull(reader.GetOrdinal("nombresubproducto")) ? null : reader.GetString(reader.GetOrdinal("nombresubproducto"));
                                ventasresponse.idoperador = reader.IsDBNull(reader.GetOrdinal("idoperador")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idoperador"));
                                ventasresponse.nombreoperador = reader.IsDBNull(reader.GetOrdinal("nombreoperador")) ? null : reader.GetString(reader.GetOrdinal("nombreoperador"));
                                ventasresponse.idtipoequipo = reader.IsDBNull(reader.GetOrdinal("idtipoequipo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoequipo"));
                                ventasresponse.nombretipoequipo = reader.IsDBNull(reader.GetOrdinal("nombretipoequipo")) ? null : reader.GetString(reader.GetOrdinal("nombretipoequipo"));
                                ventasresponse.idtiposeguro = reader.IsDBNull(reader.GetOrdinal("idtiposeguro")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtiposeguro"));
                                ventasresponse.nombretiposeguro = reader.IsDBNull(reader.GetOrdinal("nombretiposeguro")) ? null : reader.GetString(reader.GetOrdinal("nombretiposeguro"));
                                ventasresponse.montoupselling = reader.IsDBNull(reader.GetOrdinal("montoupselling")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("montoupselling"));

                                ventasresponse.idtipoetiqueta = reader.IsDBNull(reader.GetOrdinal("idtipoetiqueta")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoetiqueta"));
                                ventasresponse.nombretipoetiqueta = reader.IsDBNull(reader.GetOrdinal("nombretipoetiqueta")) ? null : reader.GetString(reader.GetOrdinal("nombretipoetiqueta"));
                                ventasresponse.idtipopago = reader.IsDBNull(reader.GetOrdinal("idtipopago")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipopago"));
                                ventasresponse.nombretipopago = reader.IsDBNull(reader.GetOrdinal("nombretipopago")) ? null : reader.GetString(reader.GetOrdinal("nombretipopago"));
                                ventasresponse.idplan = reader.IsDBNull(reader.GetOrdinal("idplan")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idplan"));
                                ventasresponse.nombreplan = reader.IsDBNull(reader.GetOrdinal("nombreplan")) ? null : reader.GetString(reader.GetOrdinal("nombreplan"));
                                ventasresponse.idmodelo = reader.IsDBNull(reader.GetOrdinal("idmodelo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idmodelo"));
                                ventasresponse.nombremodelo = reader.IsDBNull(reader.GetOrdinal("nombremodelo")) ? null : reader.GetString(reader.GetOrdinal("nombremodelo"));
                                ventasresponse.imeiequipo = reader.IsDBNull(reader.GetOrdinal("imeiequipo")) ? null : reader.GetString(reader.GetOrdinal("imeiequipo"));
                                ventasresponse.imeisim = reader.IsDBNull(reader.GetOrdinal("imeisim")) ? null : reader.GetString(reader.GetOrdinal("imeisim"));
                                ventasresponse.iccid = reader.IsDBNull(reader.GetOrdinal("iccid")) ? null : reader.GetString(reader.GetOrdinal("iccid"));
                                ventasresponse.idbundle = reader.IsDBNull(reader.GetOrdinal("idbundle")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idbundle"));
                                ventasresponse.nombrebundle = reader.IsDBNull(reader.GetOrdinal("nombrebundle")) ? null : reader.GetString(reader.GetOrdinal("nombrebundle"));
                                ventasresponse.pagocaja = reader.IsDBNull(reader.GetOrdinal("pagocaja")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("pagocaja"));
                                ventasresponse.numerocelular = reader.IsDBNull(reader.GetOrdinal("numerocelular")) ? null : reader.GetString(reader.GetOrdinal("numerocelular"));
                                ventasresponse.idtipoaccesorio = reader.IsDBNull(reader.GetOrdinal("idtipoaccesorio")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoaccesorio"));
                                ventasresponse.nombretipoaccesorio = reader.IsDBNull(reader.GetOrdinal("nombretipoaccesorio")) ? null : reader.GetString(reader.GetOrdinal("nombretipoaccesorio"));
                                ventasresponse.cantidadaccesorio = reader.IsDBNull(reader.GetOrdinal("cantidadaccesorio")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidadaccesorio"));
                                ventasresponse.pagoaccesorio = reader.IsDBNull(reader.GetOrdinal("pagoaccesorio")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("pagoaccesorio"));
                                ventasresponse.imeiaccesorio = reader.IsDBNull(reader.GetOrdinal("imeiaccesorio")) ? null : reader.GetString(reader.GetOrdinal("imeiaccesorio"));
                                ventasresponse.numeroorden = reader.IsDBNull(reader.GetOrdinal("numeroorden")) ? null : reader.GetString(reader.GetOrdinal("numeroorden"));
                                ventasresponse.ventasromimeicoincide = reader.IsDBNull(reader.GetOrdinal("ventasromimeicoincide")) ? (string?)null : reader.GetString(reader.GetOrdinal("ventasromimeicoincide"));
                                ventasresponse.codigoauthbundle = reader.IsDBNull(reader.GetOrdinal("codigoauthbundle")) ? null : reader.GetString(reader.GetOrdinal("codigoauthbundle"));
                                //ventasresponse.flagcodigoauthbundle = reader.IsDBNull(reader.GetOrdinal("flagcodigoauthbundle")) ? (int?)null : reader.GetInt32(reader.GetInt32("flagcodigoauthbundle"));
                                ventasresponse.idemppaisnegcue = reader.IsDBNull(reader.GetOrdinal("idemppaisnegcue")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idemppaisnegcue"));
                                ventasresponse.nombrecuenta = reader.IsDBNull(reader.GetOrdinal("nombrecuenta")) ? null : reader.GetString(reader.GetOrdinal("nombrecuenta"));
                                ventasresponse.estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado"));
                                ventasresponse.usuariocreacion = reader.IsDBNull(reader.GetOrdinal("usuariocreacion")) ? null : reader.GetString(reader.GetOrdinal("usuariocreacion"));
                                ventasresponse.fechacreacion = reader.IsDBNull(reader.GetOrdinal("fechacreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechacreacion"));
                                ventasresponse.usuariomodificacion = reader.IsDBNull(reader.GetOrdinal("usuariomodificacion")) ? null : reader.GetString(reader.GetOrdinal("usuariomodificacion"));
                                ventasresponse.fechamodificacion = reader.IsDBNull(reader.GetOrdinal("fechamodificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechamodificacion"));
                                ventasresponse.usuarioanulacion = reader.IsDBNull(reader.GetOrdinal("usuarioanulacion")) ? null : reader.GetString(reader.GetOrdinal("usuarioanulacion"));
                                ventasresponse.fechaanulacion = reader.IsDBNull(reader.GetOrdinal("fechaanulacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaanulacion"));


                                response.Add(ventasresponse);
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

        public async Task<List<VentasDetalleTotal>> GetVentasSuper(FiltrarVentasPerfiles filtros)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETVENTASSUPERPDV3", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idsuper", SqlDbType.VarChar).Value = filtros.idsuper;
                        command.Parameters.Add("@fechainicial", SqlDbType.Date).Value = filtros.fechainicial;
                        command.Parameters.Add("@fechafinal", SqlDbType.Date).Value = filtros.fechafinal;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = filtros.@idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<VentasDetalleTotal> response = new List<VentasDetalleTotal>();
                            while (await reader.ReadAsync())
                            {
                                VentasDetalleTotal ventasresponse = new VentasDetalleTotal();


                                ventasresponse.idventasdetalle = reader.IsDBNull(reader.GetOrdinal("idventasdetalle")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idventasdetalle"));
                                ventasresponse.idventas = reader.IsDBNull(reader.GetOrdinal("idventas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idventas"));
                                ventasresponse.fechaoperacion = reader.IsDBNull(reader.GetOrdinal("fechaoperacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaoperacion"));
                                ventasresponse.docpromotorasesor = reader.IsDBNull(reader.GetOrdinal("docpromotorasesor")) ? null : reader.GetString(reader.GetOrdinal("docpromotorasesor"));
                                ventasresponse.idpdv = reader.IsDBNull(reader.GetOrdinal("idpdv")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idpdv"));
                                ventasresponse.nombrepdv = reader.IsDBNull(reader.GetOrdinal("nombrepdv")) ? null : reader.GetString(reader.GetOrdinal("nombrepdv"));
                                ventasresponse.idtipodocumento = reader.IsDBNull(reader.GetOrdinal("idtipodocumento")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipodocumento"));
                                ventasresponse.nombretipodocumento = reader.IsDBNull(reader.GetOrdinal("nombretipodocumento")) ? null : reader.GetString(reader.GetOrdinal("nombretipodocumento"));
                                ventasresponse.doccliente = reader.IsDBNull(reader.GetOrdinal("doccliente")) ? null : reader.GetString(reader.GetOrdinal("doccliente"));
                                ventasresponse.idtipobiometria = reader.IsDBNull(reader.GetOrdinal("idtipobiometria")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipobiometria"));
                                ventasresponse.nombretipobiometria = reader.IsDBNull(reader.GetOrdinal("nombretipobiometria")) ? null : reader.GetString(reader.GetOrdinal("nombretipobiometria"));
                                ventasresponse.numcelularcontrato = reader.IsDBNull(reader.GetOrdinal("numcelularcontrato")) ? null : reader.GetString(reader.GetOrdinal("numcelularcontrato"));
                                ventasresponse.correocliente = reader.IsDBNull(reader.GetOrdinal("correocliente")) ? null : reader.GetString(reader.GetOrdinal("correocliente"));
                                ventasresponse.observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString(reader.GetOrdinal("observacion"));
                                ventasresponse.nombrevoucher = reader.IsDBNull(reader.GetOrdinal("nombrevoucher")) ? null : reader.GetString(reader.GetOrdinal("nombrevoucher"));
                                ventasresponse.numeroruc = reader.IsDBNull(reader.GetOrdinal("numeroruc")) ? null : reader.GetString(reader.GetOrdinal("numeroruc"));
                                ventasresponse.codcomprobante = reader.IsDBNull(reader.GetOrdinal("codcomprobante")) ? null : reader.GetString(reader.GetOrdinal("codcomprobante"));
                                ventasresponse.numeroserie = reader.IsDBNull(reader.GetOrdinal("numeroserie")) ? null : reader.GetString(reader.GetOrdinal("numeroserie"));
                                ventasresponse.numero = reader.IsDBNull(reader.GetOrdinal("numero")) ? null : reader.GetString(reader.GetOrdinal("numero"));
                                ventasresponse.fechaemisionvoucher = reader.IsDBNull(reader.GetOrdinal("fechaemisionvoucher")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaemisionvoucher"));
                                ventasresponse.montovoucher = reader.IsDBNull(reader.GetOrdinal("montovoucher")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("montovoucher"));
                                ventasresponse.tramaqrcode = reader.IsDBNull(reader.GetOrdinal("tramaqrcode")) ? null : reader.GetString(reader.GetOrdinal("tramaqrcode"));
                                ventasresponse.idsubproducto = reader.IsDBNull(reader.GetOrdinal("idsubproducto")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idsubproducto"));
                                ventasresponse.nombresubproducto = reader.IsDBNull(reader.GetOrdinal("nombresubproducto")) ? null : reader.GetString(reader.GetOrdinal("nombresubproducto"));
                                ventasresponse.idoperador = reader.IsDBNull(reader.GetOrdinal("idoperador")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idoperador"));
                                ventasresponse.nombreoperador = reader.IsDBNull(reader.GetOrdinal("nombreoperador")) ? null : reader.GetString(reader.GetOrdinal("nombreoperador"));
                                ventasresponse.idtipoequipo = reader.IsDBNull(reader.GetOrdinal("idtipoequipo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoequipo"));
                                ventasresponse.nombretipoequipo = reader.IsDBNull(reader.GetOrdinal("nombretipoequipo")) ? null : reader.GetString(reader.GetOrdinal("nombretipoequipo"));
                                ventasresponse.idtiposeguro = reader.IsDBNull(reader.GetOrdinal("idtiposeguro")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtiposeguro"));
                                ventasresponse.nombretiposeguro = reader.IsDBNull(reader.GetOrdinal("nombretiposeguro")) ? null : reader.GetString(reader.GetOrdinal("nombretiposeguro"));
                                ventasresponse.montoupselling = reader.IsDBNull(reader.GetOrdinal("montoupselling")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("montoupselling"));

                                ventasresponse.idtipoetiqueta = reader.IsDBNull(reader.GetOrdinal("idtipoetiqueta")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoetiqueta"));
                                ventasresponse.nombretipoetiqueta = reader.IsDBNull(reader.GetOrdinal("nombretipoetiqueta")) ? null : reader.GetString(reader.GetOrdinal("nombretipoetiqueta"));
                                ventasresponse.idtipopago = reader.IsDBNull(reader.GetOrdinal("idtipopago")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipopago"));
                                ventasresponse.nombretipopago = reader.IsDBNull(reader.GetOrdinal("nombretipopago")) ? null : reader.GetString(reader.GetOrdinal("nombretipopago"));
                                ventasresponse.idplan = reader.IsDBNull(reader.GetOrdinal("idplan")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idplan"));
                                ventasresponse.nombreplan = reader.IsDBNull(reader.GetOrdinal("nombreplan")) ? null : reader.GetString(reader.GetOrdinal("nombreplan"));
                                ventasresponse.idmodelo = reader.IsDBNull(reader.GetOrdinal("idmodelo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idmodelo"));
                                ventasresponse.nombremodelo = reader.IsDBNull(reader.GetOrdinal("nombremodelo")) ? null : reader.GetString(reader.GetOrdinal("nombremodelo"));
                                ventasresponse.imeiequipo = reader.IsDBNull(reader.GetOrdinal("imeiequipo")) ? null : reader.GetString(reader.GetOrdinal("imeiequipo"));
                                ventasresponse.imeisim = reader.IsDBNull(reader.GetOrdinal("imeisim")) ? null : reader.GetString(reader.GetOrdinal("imeisim"));
                                ventasresponse.iccid = reader.IsDBNull(reader.GetOrdinal("iccid")) ? null : reader.GetString(reader.GetOrdinal("iccid"));
                                ventasresponse.idbundle = reader.IsDBNull(reader.GetOrdinal("idbundle")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idbundle"));
                                ventasresponse.nombrebundle = reader.IsDBNull(reader.GetOrdinal("nombrebundle")) ? null : reader.GetString(reader.GetOrdinal("nombrebundle"));
                                ventasresponse.pagocaja = reader.IsDBNull(reader.GetOrdinal("pagocaja")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("pagocaja"));
                                ventasresponse.numerocelular = reader.IsDBNull(reader.GetOrdinal("numerocelular")) ? null : reader.GetString(reader.GetOrdinal("numerocelular"));
                                ventasresponse.idtipoaccesorio = reader.IsDBNull(reader.GetOrdinal("idtipoaccesorio")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoaccesorio"));
                                ventasresponse.nombretipoaccesorio = reader.IsDBNull(reader.GetOrdinal("nombretipoaccesorio")) ? null : reader.GetString(reader.GetOrdinal("nombretipoaccesorio"));
                                ventasresponse.cantidadaccesorio = reader.IsDBNull(reader.GetOrdinal("cantidadaccesorio")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidadaccesorio"));
                                ventasresponse.pagoaccesorio = reader.IsDBNull(reader.GetOrdinal("pagoaccesorio")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("pagoaccesorio"));
                                ventasresponse.imeiaccesorio = reader.IsDBNull(reader.GetOrdinal("imeiaccesorio")) ? null : reader.GetString(reader.GetOrdinal("imeiaccesorio"));
                                ventasresponse.numeroorden = reader.IsDBNull(reader.GetOrdinal("numeroorden")) ? null : reader.GetString(reader.GetOrdinal("numeroorden"));
                                ventasresponse.ventasromimeicoincide = reader.IsDBNull(reader.GetOrdinal("ventasromimeicoincide")) ? (string?)null : reader.GetString(reader.GetOrdinal("ventasromimeicoincide"));
                                ventasresponse.codigoauthbundle = reader.IsDBNull(reader.GetOrdinal("codigoauthbundle")) ? null : reader.GetString(reader.GetOrdinal("codigoauthbundle"));
                                //ventasresponse.flagcodigoauthbundle = reader.IsDBNull(reader.GetOrdinal("flagcodigoauthbundle")) ? (int?)null : reader.GetInt32(reader.GetInt32("flagcodigoauthbundle"));
                                ventasresponse.idemppaisnegcue = reader.IsDBNull(reader.GetOrdinal("idemppaisnegcue")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idemppaisnegcue"));
                                ventasresponse.nombrecuenta = reader.IsDBNull(reader.GetOrdinal("nombrecuenta")) ? null : reader.GetString(reader.GetOrdinal("nombrecuenta"));
                                ventasresponse.estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado"));
                                ventasresponse.usuariocreacion = reader.IsDBNull(reader.GetOrdinal("usuariocreacion")) ? null : reader.GetString(reader.GetOrdinal("usuariocreacion"));
                                ventasresponse.fechacreacion = reader.IsDBNull(reader.GetOrdinal("fechacreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechacreacion"));
                                ventasresponse.usuariomodificacion = reader.IsDBNull(reader.GetOrdinal("usuariomodificacion")) ? null : reader.GetString(reader.GetOrdinal("usuariomodificacion"));
                                ventasresponse.fechamodificacion = reader.IsDBNull(reader.GetOrdinal("fechamodificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechamodificacion"));
                                ventasresponse.usuarioanulacion = reader.IsDBNull(reader.GetOrdinal("usuarioanulacion")) ? null : reader.GetString(reader.GetOrdinal("usuarioanulacion"));
                                ventasresponse.fechaanulacion = reader.IsDBNull(reader.GetOrdinal("fechaanulacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaanulacion"));


                                response.Add(ventasresponse);
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

        public async Task<List<VentasDetalleTotal>> GetVentasPromotor(FiltrarVentasPerfiles filtros)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETVENTASPROMOTORPDV3", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idpromotor", SqlDbType.VarChar).Value = filtros.idpromotor;
                        command.Parameters.Add("@fechainicial", SqlDbType.VarChar).Value = filtros.fechainicial;
                        command.Parameters.Add("@fechafinal", SqlDbType.Date).Value = filtros.fechafinal;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = filtros.@idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<VentasDetalleTotal> response = new List<VentasDetalleTotal>();
                            while (await reader.ReadAsync())
                            {
                                VentasDetalleTotal ventasresponse = new VentasDetalleTotal();


                                ventasresponse.idventasdetalle = reader.IsDBNull(reader.GetOrdinal("idventasdetalle")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idventasdetalle"));
                                ventasresponse.idventas = reader.IsDBNull(reader.GetOrdinal("idventas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idventas"));
                                ventasresponse.fechaoperacion = reader.IsDBNull(reader.GetOrdinal("fechaoperacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaoperacion"));
                                ventasresponse.docpromotorasesor = reader.IsDBNull(reader.GetOrdinal("docpromotorasesor")) ? null : reader.GetString(reader.GetOrdinal("docpromotorasesor"));
                                ventasresponse.idpdv = reader.IsDBNull(reader.GetOrdinal("idpdv")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idpdv"));
                                ventasresponse.nombrepdv = reader.IsDBNull(reader.GetOrdinal("nombrepdv")) ? null : reader.GetString(reader.GetOrdinal("nombrepdv"));
                                ventasresponse.idtipodocumento = reader.IsDBNull(reader.GetOrdinal("idtipodocumento")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipodocumento"));
                                ventasresponse.nombretipodocumento = reader.IsDBNull(reader.GetOrdinal("nombretipodocumento")) ? null : reader.GetString(reader.GetOrdinal("nombretipodocumento"));
                                ventasresponse.doccliente = reader.IsDBNull(reader.GetOrdinal("doccliente")) ? null : reader.GetString(reader.GetOrdinal("doccliente"));
                                ventasresponse.idtipobiometria = reader.IsDBNull(reader.GetOrdinal("idtipobiometria")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipobiometria"));
                                ventasresponse.nombretipobiometria = reader.IsDBNull(reader.GetOrdinal("nombretipobiometria")) ? null : reader.GetString(reader.GetOrdinal("nombretipobiometria"));
                                ventasresponse.numcelularcontrato = reader.IsDBNull(reader.GetOrdinal("numcelularcontrato")) ? null : reader.GetString(reader.GetOrdinal("numcelularcontrato"));
                                ventasresponse.correocliente = reader.IsDBNull(reader.GetOrdinal("correocliente")) ? null : reader.GetString(reader.GetOrdinal("correocliente"));
                                ventasresponse.observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString(reader.GetOrdinal("observacion"));
                                ventasresponse.nombrevoucher = reader.IsDBNull(reader.GetOrdinal("nombrevoucher")) ? null : reader.GetString(reader.GetOrdinal("nombrevoucher"));
                                ventasresponse.numeroruc = reader.IsDBNull(reader.GetOrdinal("numeroruc")) ? null : reader.GetString(reader.GetOrdinal("numeroruc"));
                                ventasresponse.codcomprobante = reader.IsDBNull(reader.GetOrdinal("codcomprobante")) ? null : reader.GetString(reader.GetOrdinal("codcomprobante"));
                                ventasresponse.numeroserie = reader.IsDBNull(reader.GetOrdinal("numeroserie")) ? null : reader.GetString(reader.GetOrdinal("numeroserie"));
                                ventasresponse.numero = reader.IsDBNull(reader.GetOrdinal("numero")) ? null : reader.GetString(reader.GetOrdinal("numero"));
                                ventasresponse.fechaemisionvoucher = reader.IsDBNull(reader.GetOrdinal("fechaemisionvoucher")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaemisionvoucher"));
                                ventasresponse.montovoucher = reader.IsDBNull(reader.GetOrdinal("montovoucher")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("montovoucher"));
                                ventasresponse.tramaqrcode = reader.IsDBNull(reader.GetOrdinal("tramaqrcode")) ? null : reader.GetString(reader.GetOrdinal("tramaqrcode"));
                                ventasresponse.idsubproducto = reader.IsDBNull(reader.GetOrdinal("idsubproducto")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idsubproducto"));
                                ventasresponse.nombresubproducto = reader.IsDBNull(reader.GetOrdinal("nombresubproducto")) ? null : reader.GetString(reader.GetOrdinal("nombresubproducto"));
                                ventasresponse.idoperador = reader.IsDBNull(reader.GetOrdinal("idoperador")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idoperador"));
                                ventasresponse.nombreoperador = reader.IsDBNull(reader.GetOrdinal("nombreoperador")) ? null : reader.GetString(reader.GetOrdinal("nombreoperador"));
                                ventasresponse.idtipoequipo = reader.IsDBNull(reader.GetOrdinal("idtipoequipo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoequipo"));
                                ventasresponse.nombretipoequipo = reader.IsDBNull(reader.GetOrdinal("nombretipoequipo")) ? null : reader.GetString(reader.GetOrdinal("nombretipoequipo"));
                                ventasresponse.idtiposeguro = reader.IsDBNull(reader.GetOrdinal("idtiposeguro")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtiposeguro"));
                                ventasresponse.nombretiposeguro = reader.IsDBNull(reader.GetOrdinal("nombretiposeguro")) ? null : reader.GetString(reader.GetOrdinal("nombretiposeguro"));
                                ventasresponse.montoupselling = reader.IsDBNull(reader.GetOrdinal("montoupselling")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("montoupselling"));

                                ventasresponse.idtipoetiqueta = reader.IsDBNull(reader.GetOrdinal("idtipoetiqueta")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoetiqueta"));
                                ventasresponse.nombretipoetiqueta = reader.IsDBNull(reader.GetOrdinal("nombretipoetiqueta")) ? null : reader.GetString(reader.GetOrdinal("nombretipoetiqueta"));
                                ventasresponse.idtipopago = reader.IsDBNull(reader.GetOrdinal("idtipopago")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipopago"));
                                ventasresponse.nombretipopago = reader.IsDBNull(reader.GetOrdinal("nombretipopago")) ? null : reader.GetString(reader.GetOrdinal("nombretipopago"));
                                ventasresponse.idplan = reader.IsDBNull(reader.GetOrdinal("idplan")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idplan"));
                                ventasresponse.nombreplan = reader.IsDBNull(reader.GetOrdinal("nombreplan")) ? null : reader.GetString(reader.GetOrdinal("nombreplan"));
                                ventasresponse.idmodelo = reader.IsDBNull(reader.GetOrdinal("idmodelo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idmodelo"));
                                ventasresponse.nombremodelo = reader.IsDBNull(reader.GetOrdinal("nombremodelo")) ? null : reader.GetString(reader.GetOrdinal("nombremodelo"));
                                ventasresponse.imeiequipo = reader.IsDBNull(reader.GetOrdinal("imeiequipo")) ? null : reader.GetString(reader.GetOrdinal("imeiequipo"));
                                ventasresponse.imeisim = reader.IsDBNull(reader.GetOrdinal("imeisim")) ? null : reader.GetString(reader.GetOrdinal("imeisim"));
                                ventasresponse.iccid = reader.IsDBNull(reader.GetOrdinal("iccid")) ? null : reader.GetString(reader.GetOrdinal("iccid"));
                                ventasresponse.idbundle = reader.IsDBNull(reader.GetOrdinal("idbundle")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idbundle"));
                                ventasresponse.nombrebundle = reader.IsDBNull(reader.GetOrdinal("nombrebundle")) ? null : reader.GetString(reader.GetOrdinal("nombrebundle"));
                                ventasresponse.pagocaja = reader.IsDBNull(reader.GetOrdinal("pagocaja")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("pagocaja"));
                                ventasresponse.numerocelular = reader.IsDBNull(reader.GetOrdinal("numerocelular")) ? null : reader.GetString(reader.GetOrdinal("numerocelular"));
                                ventasresponse.idtipoaccesorio = reader.IsDBNull(reader.GetOrdinal("idtipoaccesorio")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idtipoaccesorio"));
                                ventasresponse.nombretipoaccesorio = reader.IsDBNull(reader.GetOrdinal("nombretipoaccesorio")) ? null : reader.GetString(reader.GetOrdinal("nombretipoaccesorio"));
                                ventasresponse.cantidadaccesorio = reader.IsDBNull(reader.GetOrdinal("cantidadaccesorio")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("cantidadaccesorio"));
                                ventasresponse.pagoaccesorio = reader.IsDBNull(reader.GetOrdinal("pagoaccesorio")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("pagoaccesorio"));
                                     ventasresponse.imeiaccesorio = reader.IsDBNull(reader.GetOrdinal("imeiaccesorio")) ? null : reader.GetString(reader.GetOrdinal("imeiaccesorio"));
                                ventasresponse.numeroorden = reader.IsDBNull(reader.GetOrdinal("numeroorden")) ? null : reader.GetString(reader.GetOrdinal("numeroorden"));
                                ventasresponse.ventasromimeicoincide = reader.IsDBNull(reader.GetOrdinal("ventasromimeicoincide")) ? (string?)null : reader.GetString(reader.GetOrdinal("ventasromimeicoincide"));
                                ventasresponse.codigoauthbundle = reader.IsDBNull(reader.GetOrdinal("codigoauthbundle")) ? null : reader.GetString(reader.GetOrdinal("codigoauthbundle"));
                                //ventasresponse.flagcodigoauthbundle = reader.IsDBNull(reader.GetOrdinal("flagcodigoauthbundle")) ? (int?)null : reader.GetInt32(reader.GetInt32("flagcodigoauthbundle"));
                                ventasresponse.idemppaisnegcue = reader.IsDBNull(reader.GetOrdinal("idemppaisnegcue")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idemppaisnegcue"));
                                ventasresponse.nombrecuenta = reader.IsDBNull(reader.GetOrdinal("nombrecuenta")) ? null : reader.GetString(reader.GetOrdinal("nombrecuenta"));
                                ventasresponse.estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("estado"));
                                ventasresponse.usuariocreacion = reader.IsDBNull(reader.GetOrdinal("usuariocreacion")) ? null : reader.GetString(reader.GetOrdinal("usuariocreacion"));
                                ventasresponse.fechacreacion = reader.IsDBNull(reader.GetOrdinal("fechacreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechacreacion"));
                                ventasresponse.usuariomodificacion = reader.IsDBNull(reader.GetOrdinal("usuariomodificacion")) ? null : reader.GetString(reader.GetOrdinal("usuariomodificacion"));
                                ventasresponse.fechamodificacion = reader.IsDBNull(reader.GetOrdinal("fechamodificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechamodificacion"));
                                ventasresponse.usuarioanulacion = reader.IsDBNull(reader.GetOrdinal("usuarioanulacion")) ? null : reader.GetString(reader.GetOrdinal("usuarioanulacion"));
                                ventasresponse.fechaanulacion = reader.IsDBNull(reader.GetOrdinal("fechaanulacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaanulacion"));
                                

                                response.Add(ventasresponse);
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
        public async Task<VentasResult> PostVentas(Ventas venta)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_POSTVENTAS3", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@fechaoperacion", SqlDbType.VarChar, 15) { Value = (object)venta.fechaoperacion ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@docpromotorasesor", SqlDbType.VarChar, 15) { Value = (object)venta.docpromotorasesor ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idpdv", SqlDbType.Int) { Value = (object)venta.idpdv ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idtipodocumento", SqlDbType.Int) { Value = (object)venta.idtipodocumento ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@doccliente", SqlDbType.VarChar, 15) { Value = (object)venta.doccliente ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idtipobiometria", SqlDbType.Int) { Value = (object)venta.idtipobiometria ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@numcelularcontrato", SqlDbType.VarChar, 15) { Value = (object)venta.numcelularcontrato ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@correocliente", SqlDbType.VarChar, 100) { Value = (object)venta.correocliente ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@observacion", SqlDbType.VarChar, 100) { Value = (object)venta.observacion ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@nombrevoucher", SqlDbType.VarChar, 100) { Value = (object)venta.nombrevoucher ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@numeroruc", SqlDbType.VarChar, 100) { Value = (object)venta.numeroruc ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@codcomprobante", SqlDbType.VarChar, 100) { Value = (object)venta.codcomprobante ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@numeroserie", SqlDbType.VarChar, 100) { Value = (object)venta.numeroserie ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@numero", SqlDbType.VarChar, 100) { Value = (object)venta.numero ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@fechaemisionvoucher", SqlDbType.VarChar, 15) { Value = (object)venta.fechaemisionvoucher ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@montovoucher", SqlDbType.Decimal) { Value = (object)venta.montovoucher ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@tramaqrcode", SqlDbType.VarChar, -1) { Value = (object)venta.tramaqrcode ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idemppaisnegcue", SqlDbType.Int) { Value = (object)venta.idemppaisnegcue ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@usuariocreacion", SqlDbType.VarChar, 15) { Value = (object)venta.usuariocreacion ?? DBNull.Value });

                        // Add TVP parameter for VentasDetalle
                        SqlParameter tvpParam = command.Parameters.AddWithValue("@VentasDetalle", CreateVentasDetalleDataTable(venta.VentasDetalles));
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.VentasDetalleType4";

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var result = new VentasResult
                                {
                                    success = reader["success"] != DBNull.Value && Convert.ToBoolean(reader["success"]),
                                    message = reader["message"]?.ToString(),
                                    nombrevoucher = reader["nombrevoucher"]?.ToString(),
                                    idventa = reader["idventa"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idventa")) : 0,
                                    idventasdetalle = reader["idventasdetalle"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idventasdetalle")) : 0
                                };

                                // Lógica para verificar idventasdetalle y enviar mensajes si no es null
                                if (result.idventasdetalle != 0) // Assuming 0 is the default null value for int
                                {
                                    await EnviarMensajeExito((int)result.idventasdetalle);
                                }

                                return result;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new VentasResult { success = false, message = "Error: " + ex.Message };
            }

            return new VentasResult { success = false, message = "Unknown error occurred." };
        }

        private async Task EnviarMensajeExito(int idventasdetalle)
        {
            // Aquí puedes implementar la lógica de enviar el mensaje de éxito
            // Ejemplo: Obtener los detalles y enviar el mensaje correspondiente
            var tfa = new TwoFactorAuth("MiAplicacion");
            string newAuthCode = tfa.GetCode(tfa.CreateSecret());

            using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
            {
                await connection.OpenAsync();

                using (SqlCommand spCommand = new SqlCommand("USP_UPDATEAUTHCODIGOBUNDLE", connection))
                {
                    spCommand.CommandType = CommandType.StoredProcedure;
                    spCommand.Parameters.AddWithValue("@idventasdetalle", idventasdetalle);
                    spCommand.Parameters.AddWithValue("@nuevoauthcodigo", newAuthCode);

                    using (SqlDataReader rdr = await spCommand.ExecuteReaderAsync())
                    {
                        if (await rdr.ReadAsync())
                        {
                            var authMensaje = new AuthMensaje()
                            {
                                authcodigo = rdr["authcodigo"].ToString(),
                                celular = rdr["celular"].ToString(),
                                idventas = (int)rdr["idventas"]
                            };

                            SendSms(authMensaje.celular, authMensaje.authcodigo, (int)authMensaje.idventas);
                        }
                    }
                }
            }
        }

        public class AuthMensaje
        {
            public string? authcodigo { get; set; }
            public string? celular { get; set; }
            public int? idventas { get; set; }

        }

        // Método para enviar SMS (esto debe estar implementado en tu código)
        public void SendSms(string celular, string authCode, int idVentasPrincipal)
        {
            string apiUrl = "https://sms.app.helperworkin.xyz/api/sms/send";
            string caHeader = _AWSMessageCA;

            using (HttpClient client = new HttpClient())
            {
                // URL larga que quieres acortar
                string longUrl = $"https://intranet.grupotawa.com/RomBI/public/Rom/main/Entel/ConfirmacionBundle/{idVentasPrincipal}";
                string shortUrl = "";

                // Acortar la URL
                try
                {
                    string apiUrlShortener = "https://api.aws3.link/shorten";
                    client.DefaultRequestHeaders.Add("x-api-key", "udncfMFYIY4RmyLTQBTpO77TZJAPcRyW1rCSwR9v"); // Header con la API key 

                    // Crear manualmente el cuerpo de la solicitud como cadena JSON
                    string jsonBody = $@"
                    {{
                        ""longUrl"": ""{longUrl}"",
                        ""expireHours"": 24,
                        ""slugLength"": 4
                    }}";

                    // Configurar el contenido de la solicitud
                    HttpContent contentPOST = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Realizar la solicitud POST
                    HttpResponseMessage response = client.PostAsync(apiUrlShortener, contentPOST).Result; // Llamada sincrónica

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;
                        dynamic apiResponse = JsonConvert.DeserializeObject(jsonResponse);

                        // Obtener el shortUrl del JSON dinámico
                        shortUrl = apiResponse.shortUrl;

                        Console.WriteLine($"URL acortada: {shortUrl}");
                    }
                    else
                    {
                        // Manejar el error detalladamente
                        string responseContent = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"Error al acortar URL: {response.StatusCode}, Contenido: {responseContent}");
                        shortUrl = longUrl; // Retorna la URL original si hay error
                    }

                    // Enviar el mensaje SMS
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("ca", caHeader);

                    var payload = new
                    {
                        number = celular,
                        message = $"Entel Link: {shortUrl}, código: {authCode}"
                    };

                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response2 = client.PostAsync(apiUrl, content).Result;

                    if (response2.IsSuccessStatusCode)
                    {
                        Console.WriteLine("SMS enviado exitosamente.");
                    }
                    else
                    {
                        string error = $"Error al enviar SMS: {response2.StatusCode}";
                        Console.WriteLine(error);
                        LogErrorToDatabase(celular, authCode, idVentasPrincipal, error);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error al acortar URL: {e.Message}");
                    shortUrl = longUrl; // Retorna la URL original si hay error
                }
            }
        }

        private static async Task LogErrorToDatabase(string celular, string authCode, int idVentasPrincipal, string error)
        {
            // Implementa la lógica para registrar el error en la base de datos
        }

        public async Task<Respuesta> DeleteVentasDetalle(int idventasdetalle, string usuarioanulacion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_DELETEVENTASDETALLE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@iddetalle", SqlDbType.Int).Value = idventasdetalle;
                        command.Parameters.Add("@usuarioanulacion", SqlDbType.VarChar).Value = usuarioanulacion;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {

                            Respuesta respuesta = new Respuesta();

                            while (await reader.ReadAsync())
                            {
                                respuesta.Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));

                                // Manejar múltiples filas si es necesario
                                // Por ejemplo, almacenar cada resultado en una lista
                            }

                            return respuesta;
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

        public async Task<ActualizarNombreVoucherResponse> UpdateNombreVoucherRetail(ActualizarNombreVoucherRequest request)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("USP_UPDATENOMBREVOUCHER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idventas", request.idventa);
                    command.Parameters.AddWithValue("@nombrevoucher", request.nombrevoucher);

                    var responseMessage = string.Empty;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            responseMessage = reader["mensaje"].ToString();
                        }
                    }

                    return new ActualizarNombreVoucherResponse
                    {
                        mensaje = responseMessage
                    };
                }
            }
        }

        private DataTable CreateVentasDetalleDataTable(List<VentasDetalle> ventasDetalles)
        {
            DataTable table = new DataTable();
            table.Columns.Add("idsubproducto", typeof(int));
            table.Columns.Add("idoperador", typeof(int));
            table.Columns.Add("idtipoequipo", typeof(int));
            table.Columns.Add("idtiposeguro", typeof(int));
            table.Columns.Add("montoupselling",typeof(decimal));
            table.Columns.Add("idtipoetiqueta", typeof(int));
            table.Columns.Add("idtipopago", typeof(int));
            table.Columns.Add("idplan", typeof(int));
            table.Columns.Add("idmodelo", typeof(int));
            table.Columns.Add("imeiequipo", typeof(string));
            table.Columns.Add("imeisim", typeof(string));
            table.Columns.Add("iccid", typeof(string));
            table.Columns.Add("idbundle", typeof(int));
            table.Columns.Add("pagocaja", typeof(decimal));
            table.Columns.Add("numerocelular", typeof(string));
            table.Columns.Add("idtipoaccesorio", typeof(int));
            table.Columns.Add("cantidadaccesorio", typeof(int));
            table.Columns.Add("pagoaccesorio", typeof(decimal));
            table.Columns.Add("imeiaccesorio", typeof(string));
            table.Columns.Add("numeroorden", typeof(string));
            table.Columns.Add("ventasromimeicoincide", typeof(string));
            table.Columns.Add("codigoauthbundle", typeof(string));
            table.Columns.Add("flagcodigoauthbundle", typeof(int));
            table.Columns.Add("idemppaisnegcue", typeof(int));
            table.Columns.Add("estado", typeof(int));
            table.Columns.Add("usuariocreacion", typeof(string));
            table.Columns.Add("fechacreacion", typeof(string));


            foreach (var detalle in ventasDetalles)
            {
                table.Rows.Add(detalle.idsubproducto, detalle.idoperador, detalle.idtipoequipo, detalle.idtiposeguro,detalle.montoupselling,
                               detalle.idtipoetiqueta, detalle.idtipopago, detalle.idplan, detalle.idmodelo, detalle.imeiequipo,
                               detalle.imeisim, detalle.iccid, detalle.idbundle, detalle.pagocaja, detalle.numerocelular,
                               detalle.idtipoaccesorio, detalle.cantidadaccesorio, detalle.pagoaccesorio, detalle.imeiaccesorio,
                               detalle.numeroorden, detalle.ventasromimeicoincide, detalle.codigoauthbundle, detalle.flagcodigoauthbundle,
                               detalle.idemppaisnegcue, detalle.estado, detalle.usuariocreacion, detalle.fechacreacion);
            }

            return table;
        }

        public async Task<VentasResult> UpdateVentasDetalle(VentasDetalle request)
        {
            var respuesta = new VentasResult();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_UPDATEVENTASDETALLE3", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@idventasdetalle", SqlDbType.Int) { Value = (object)request.idventasdetalle ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idsubproducto", SqlDbType.Int) { Value = (object)request.idsubproducto ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idoperador", SqlDbType.Int) { Value = (object)request.idoperador ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idtipoequipo", SqlDbType.Int) { Value = (object)request.idtipoequipo ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idtiposeguro", SqlDbType.Int) { Value = (object)request.idtiposeguro ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@montoupselling", SqlDbType.Decimal) { Value = (object)request.montoupselling ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idtipoetiqueta", SqlDbType.Int) { Value = (object)request.idtipoetiqueta ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idtipopago", SqlDbType.Int) { Value = (object)request.idtipopago ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idplan", SqlDbType.Int) { Value = (object)request.idplan ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idmodelo", SqlDbType.Int) { Value = (object)request.idmodelo ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@imeiequipo", SqlDbType.VarChar, 20) { Value = (object)request.imeiequipo ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@imeisim", SqlDbType.VarChar, 20) { Value = (object)request.imeisim ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@iccid", SqlDbType.VarChar, 20) { Value = (object)request.iccid ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idbundle", SqlDbType.Int) { Value = (object)request.idbundle ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@pagocaja", SqlDbType.Decimal) { Value = (object)request.pagocaja ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@numerocelular", SqlDbType.VarChar, 15) { Value = (object)request.numerocelular ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@idtipoaccesorio", SqlDbType.Int) { Value = (object)request.idtipoaccesorio ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@cantidadaccesorio", SqlDbType.Int) { Value = (object)request.cantidadaccesorio ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@pagoaccesorio", SqlDbType.Decimal) { Value = (object)request.pagoaccesorio ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@imeiaccesorio", SqlDbType.VarChar, 20) { Value = (object)request.imeiaccesorio ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@numeroorden", SqlDbType.VarChar, 15) { Value = (object)request.numeroorden ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@usuariomodificacion", SqlDbType.VarChar, 15) { Value = (object)request.usuariomodificacion ?? DBNull.Value });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                respuesta.message = reader.GetString(reader.GetOrdinal("Message"));
                                respuesta.idventasdetalle = reader["idventasdetalle"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idventasdetalle")) : (int?)null;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                respuesta.message = $"Error de SQL: {ex.Message}";
            }
            catch (Exception ex)
            {
                respuesta.message = $"Error: {ex.Message}";
            }

            return respuesta;
        }

        public async Task<VentasResult> UpdateVoucherVentas(Ventas venta)
        {
            var respuesta = new VentasResult();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_UPDATEVOUCHERVENTAS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@idventas", SqlDbType.Int) { Value = (object)venta.idventas ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@nombrevoucher", SqlDbType.VarChar, 100) { Value = (object)venta.nombrevoucher ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@numeroruc", SqlDbType.VarChar, 100) { Value = (object)venta.numeroruc ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@codcomprobante", SqlDbType.VarChar, 100) { Value = (object)venta.codcomprobante ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@numeroserie", SqlDbType.VarChar, 100) { Value = (object)venta.numeroserie ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@numero", SqlDbType.VarChar, 100) { Value = (object)venta.numero ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@fechaemisionvoucher", SqlDbType.VarChar, 15) { Value = (object)venta.fechaemisionvoucher ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@montovoucher", SqlDbType.Decimal) { Value = (object)venta.montovoucher ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@tramaqrcode", SqlDbType.VarChar, -1) { Value = (object)venta.tramaqrcode ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@usuariomodificacion", SqlDbType.VarChar, 15) { Value = (object)venta.usuariomodificacion ?? DBNull.Value });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                respuesta.message = reader.GetString(reader.GetOrdinal("Message"));
                                respuesta.idventa = reader["idventas"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("idventas")) : (int?)null;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                respuesta.message = $"Error de SQL: {ex.Message}";
            }
            catch (Exception ex)
            {
                respuesta.message = $"Error: {ex.Message}";
            }

            return respuesta;
        }
    }
}
