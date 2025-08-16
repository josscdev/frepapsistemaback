using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRestNetCore.DTO.DtoIncentivo;
using RombiBack.Abstraction;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.Intranet_Incentivos
{
    public class IncentivosRepository : IIncentivosRepository
    {
        private readonly DataAcces _dbConnection;
        public IncentivosRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<IncentivoVistaDTO> GetGeneralIncentivosVistasWithDNIConfirmationFalse(string dni)
        {
            List<IncentivoVistaDTO> incentivosVistas = new List<IncentivoVistaDTO>();

            using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionENTEL_RETAIL()))
            {
                connection.Open();

                string storedProcedureName = "SP_ListarIncentivos";

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@P_DNIPROMOTOR", dni);
                    command.Parameters.AddWithValue("@P_IDESTADO", 3);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IncentivoVistaDTO incentivoVista = new IncentivoVistaDTO
                            {
                                id = reader.GetInt32(reader.GetOrdinal("Id")),
                                PeriodoIncentivo = reader.GetString(reader.GetOrdinal("PeriodoIncentivo")),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("FechaFin")),
                                DniPromotor = reader.GetString(reader.GetOrdinal("DniPromotor")),
                                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                                PUNTOVENTA = reader.GetString(reader.GetOrdinal("PuntoVenta")),
                                IdIncentivo = reader.GetInt32(reader.GetOrdinal("IdIncentivo")),
                                NombreIncentivo = reader.GetString(reader.GetOrdinal("NombreIncentivo")),
                                Empresa = reader.GetString(reader.GetOrdinal("Empresa")),
                                TipoIncentivo = reader.GetString(reader.GetOrdinal("TipoIncentivo")),
                                Monto = reader.GetDecimal(reader.GetOrdinal("Monto")),
                                EstadoIncentivo = reader.GetString(reader.GetOrdinal("EstadoIncentivo"))
                            };

                            incentivosVistas.Add(incentivoVista);
                        }
                    }
                }
            }

            return incentivosVistas;
        }

        public IEnumerable<IncentivoVistaDTO> GetIncentivosPremios(string dni)
        {
            List<IncentivoVistaDTO> incentivosVistas = new List<IncentivoVistaDTO>();

            using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionENTEL_RETAIL()))
            {
                connection.Open();

                string storedProcedureName = "SP_ListarIncentivosPremios";

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@P_DNIPROMOTOR", dni);
                    command.Parameters.AddWithValue("@P_IDESTADO", 3);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IncentivoVistaDTO incentivoVista = new IncentivoVistaDTO
                            {
                                id = reader.GetInt32(reader.GetOrdinal("Id")),
                                PeriodoIncentivo = reader.GetString(reader.GetOrdinal("PeriodoIncentivo")),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("FechaFin")),
                                DniPromotor = reader.GetString(reader.GetOrdinal("DniPromotor")),
                                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                                PUNTOVENTA = reader.GetString(reader.GetOrdinal("PuntoVenta")),
                                IdIncentivo = reader.GetInt32(reader.GetOrdinal("IdIncentivo")),
                                NombreIncentivo = reader.GetString(reader.GetOrdinal("NombreIncentivo")),
                                Empresa = reader.GetString(reader.GetOrdinal("Empresa")),
                                Premio = reader.GetString(reader.GetOrdinal("Premio")),
                                EstadoIncentivo = reader.GetString(reader.GetOrdinal("EstadoIncentivo"))
                            };

                            incentivosVistas.Add(incentivoVista);
                        }
                    }
                }
            }

            return incentivosVistas;
        }

        public void UpdateConfirmacionEntrega(string dni, int idIncentivo)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionENTEL_RETAIL()))
            {
                connection.Open();

                string query = @"UPDATE IncentivosPagos
                         SET ConfirmacionEntrega = 3,
                             IdEstadoAdministrativo = 4,
                             FechaConfirmacion = GETDATE()
                         WHERE DniPromotor = @dni
                         AND Id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@dni", dni);
                    command.Parameters.AddWithValue("@id", idIncentivo);
                    command.ExecuteNonQuery();
                }
            }
        }

        public User ValidateUser(string dni, string password)
        {
            User usuarioRetorno = null;

            using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionAPP_BI()))
            {
                connection.Open();

                string storedProcedureName = "SEG_ValidateUser";

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@USUARIO", dni);
                    command.Parameters.AddWithValue("@CLAVE", password);

                    using (SqlDataReader rdr = command.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            usuarioRetorno = new User
                            {
                                IDUSUARIO = int.Parse(rdr["IDUSUARIO"].ToString()),
                                NOMBRES = rdr["NOMBRES"].ToString(),
                                APELLIDOPATERNO = rdr["APELLIDOPATERNO"].ToString(),
                                APELLIDOMATERNO = rdr["APELLIDOMATERNO"].ToString(),
                                JERARQUIA = rdr["JERARQUIA"].ToString(),
                                IDJERARQUIA = int.Parse(rdr["IDJERARQUIA"].ToString()),
                                CORREO = rdr["CORREO"] != null ? rdr["CORREO"].ToString() : "",
                                USUARIO = rdr["USUARIO"].ToString(),
                                COD_NEGOCIO = rdr["COD_NEGOCIO"].ToString(),
                                COD_CUENTA = rdr["COD_CUENTA"].ToString(),
                                COD_PAIS = rdr["COD_PAIS"].ToString(),
                                CLAVE = rdr["CLAVE"].ToString(),
                                ES_ADMIN = rdr["ES_ADMIN"].ToString(),
                                TOKEN = rdr["TOKEN"].ToString()
                            };
                        }
                    }
                }
            return usuarioRetorno;

        }

    }

    }

}
