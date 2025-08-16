using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Allocation;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_TPF.MGM_Allocation
{
    public class AllocationTPFRepository: IAllocationTPFRepository
    {
        private readonly DataAcces _dbConnection;

        public AllocationTPFRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Roles> GetAllRolPromotorTPF(string usuario, int idemppaisnegcue, string tipoperiodo)
        {
            var listaPromotores = new List<Roles>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_GETALLROLPROMOTOR", connection))
                {
                    connection.Open(); // Abrir la conexión sincrónicamente

                    command.CommandTimeout = 0; // Sin límite de tiempo
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);
                    command.Parameters.AddWithValue("@tipoperiodo", tipoperiodo);

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader()) // Lectura sincrónica
                    {
                        while (reader.Read())
                        {
                            var promotorRol = new Roles
                            {
                                nombres = reader["nombres"].ToString(),
                                docusuario = reader["docusuario"].ToString(),
                            };
                            listaPromotores.Add(promotorRol);
                        }
                    }
                }

                return listaPromotores;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener los roles de promotores: " + ex.Message);
            }
        }

        public List<Roles> GetRolUsuarioPDVTPF(string usuario, int idemppaisnegcue, string tipoperiodo)
        {
            var listaPromotores = new List<Roles>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_GETROLUSUARIOPDV", connection))
                {
                    connection.Open(); // Abrir la conexión sincrónicamente

                    command.CommandTimeout = 0; // Sin límite de tiempo
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                    command.Parameters.AddWithValue("@docusuario", usuario);
                    command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);
                    command.Parameters.AddWithValue("@tipoperiodo", tipoperiodo);

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader()) // Lectura sincrónica
                    {
                        while (reader.Read())
                        {
                            var promotorRol = new Roles
                            {
                                docusuario = reader["docusuario"].ToString(),
                                idpdv = reader["idpdv"] != DBNull.Value ? Convert.ToInt32(reader["idpdv"]) : 0,
                                nombrepdv = reader["nombrepdv"].ToString(),
                                fechainicio = reader["fechainicio"] != DBNull.Value ? Convert.ToDateTime(reader["fechainicio"]) : (DateTime?)null,
                                fechafin = reader["fechafin"] != DBNull.Value ? Convert.ToDateTime(reader["fechafin"]) : (DateTime?)null,
                                idtipolicencia = reader["idtipolicencia"] != DBNull.Value ? Convert.ToInt32(reader["idtipolicencia"]) : 0,
                                nombretipolicencia = reader["nombretipolicencia"].ToString(),

                            };
                            listaPromotores.Add(promotorRol);
                        }
                    }
                }

                return listaPromotores;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener los roles de promotores: " + ex.Message);
            }
        }

        public List<Respuesta> ValidarBotonRegistroVentasTPF(string usuario, int idemppaisnegcue)
        {
            var respuestas = new List<Respuesta>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_VALIDARBOTONREGISTROVENTAS", connection))
                {
                    connection.Open();

                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    command.Parameters.AddWithValue("@docusuario", usuario);
                    command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Extraer los valores de la consulta
                            var estadoasignacion = reader["estado_asignacion"].ToString();
                            var idRol = reader["idrol"] as int?;
                            var fechainicio = reader["fechainicio"] as DateTime?;
                            var fechafin = reader["fechafin"] as DateTime?;

                            // Validar si hay asignación basada en las fechas
                            var success = fechainicio.HasValue && fechafin.HasValue;

                            // Construir el objeto de respuesta
                            var respuesta = new Respuesta
                            {
                                Success = success,
                                Message = estadoasignacion,
                                Data = new Roles
                                {
                                    idrol = idRol,
                                    fechainicio = fechainicio,
                                    fechafin = fechafin
                                }
                            };
                            respuestas.Add(respuesta);
                        }
                    }
                }

                // Si no se devuelven resultados, agregar una respuesta por defecto indicando "NO ASIGNADO"
                if (respuestas.Count == 0)
                {
                    respuestas.Add(new Respuesta
                    {
                        Success = false,
                        Message = "NO ASIGNADO",
                        Data = null
                    });
                }

                return respuestas;
            }
            catch (Exception ex)
            {
                // En caso de excepción, devolver una lista con un solo elemento de error
                return new List<Respuesta>
                {
                    new Respuesta
                    {
                        Success = false,
                        Message = "Error: " + ex.Message,
                        Data = null
                    }
                };
            }
        }

        public List<Roles> GetRolTipoFuncionalidadTPF(int idemppaisnegcue)
        {
            var listaPromotores = new List<Roles>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_GETROLTIPOFUNCIONALIDAD", connection))
                {
                    connection.Open(); // Abrir la conexión sincrónicamente

                    command.CommandTimeout = 0; // Sin límite de tiempo
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                    command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader()) // Lectura sincrónica
                    {
                        while (reader.Read())
                        {
                            var promotorRol = new Roles
                            {

                                idtipofuncionalidad = reader["idtipofuncionalidad"] != DBNull.Value ? Convert.ToInt32(reader["idtipofuncionalidad"]) : 0,
                                nombretipofuncionalidad = reader["nombretipofuncionalidad"].ToString(),
                            };
                            listaPromotores.Add(promotorRol);
                        }
                    }
                }

                return listaPromotores;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener los roles de promotores: " + ex.Message);
            }
        }

        public List<Roles> GetRolTipoEstadoTPF(int idemppaisnegcue)
        {
            var listaPromotores = new List<Roles>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_GETROLTIPOESTADO", connection))
                {
                    connection.Open(); // Abrir la conexión sincrónicamente

                    command.CommandTimeout = 0; // Sin límite de tiempo
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                    command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader()) // Lectura sincrónica
                    {
                        while (reader.Read())
                        {
                            var promotorRol = new Roles
                            {

                                idtipoestado = reader["idtipoestado"] != DBNull.Value ? Convert.ToInt32(reader["idtipoestado"]) : 0,
                                nombretipoestado = reader["nombretipoestado"].ToString(),
                            };
                            listaPromotores.Add(promotorRol);
                        }
                    }
                }

                return listaPromotores;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener los roles de promotores: " + ex.Message);
            }
        }


        public List<Roles> GetRolTipoTrabajoTPF(int idemppaisnegcue)
        {
            var listaPromotores = new List<Roles>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_GETROLTIPOTRABAJO", connection))
                {
                    connection.Open(); // Abrir la conexión sincrónicamente

                    command.CommandTimeout = 0; // Sin límite de tiempo
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                    command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader()) // Lectura sincrónica
                    {
                        while (reader.Read())
                        {
                            var promotorRol = new Roles
                            {

                                idtipotrabajo = reader["idtipotrabajo"] != DBNull.Value ? Convert.ToInt32(reader["idtipotrabajo"]) : 0,
                                nombretipotrabajo = reader["nombretipotrabajo"].ToString(),
                            };
                            listaPromotores.Add(promotorRol);
                        }
                    }
                }

                return listaPromotores;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener los roles de promotores: " + ex.Message);
            }
        }

        public List<Roles> GetRolTipoLicenciaTPF(int idemppaisnegcue)
        {
            var listaPromotores = new List<Roles>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_GETROLTIPOLICENCIA", connection))
                {
                    connection.Open(); // Abrir la conexión sincrónicamente

                    command.CommandTimeout = 0; // Sin límite de tiempo
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                    command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader()) // Lectura sincrónica
                    {
                        while (reader.Read())
                        {
                            var promotorRol = new Roles
                            {

                                idtipolicencia = reader["idtipolicencia"] != DBNull.Value ? Convert.ToInt32(reader["idtipolicencia"]) : 0,
                                nombretipolicencia = reader["nombretipolicencia"].ToString(),
                            };
                            listaPromotores.Add(promotorRol);
                        }
                    }
                }

                return listaPromotores;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener los roles de promotores: " + ex.Message);
            }
        }

        public List<Roles> GetRolPromotorDocUsuarioTPF(string usuario, int idemppaisnegcue, string tipoperiodo, string usuarioperfil)
        {
            var listaPromotores = new List<Roles>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_GETROLPROMOTORDOCUSUARIO", connection))
                {
                    connection.Open(); // Abrir la conexión

                    command.CommandTimeout = 0; // Sin límite de tiempo
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);
                    command.Parameters.AddWithValue("@tipoperiodo", tipoperiodo);
                    command.Parameters.AddWithValue("@usuarioperfil", usuarioperfil);

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var promotorRol = new Roles
                            {
                                idrol = reader["idrol"] != DBNull.Value ? Convert.ToInt32(reader["idrol"]) : 0,
                                docusuario = reader["docusuario"].ToString(),
                                nombres = reader["nombres"].ToString(),
                                idpdv = reader["idpdv"] != DBNull.Value ? Convert.ToInt32(reader["idpdv"]) : 0,
                                nombrepdv = reader["nombrepdv"].ToString(),
                                fechainicio = reader["fechainicio"] != DBNull.Value ? Convert.ToDateTime(reader["fechainicio"]) : (DateTime?)null,
                                fechafin = reader["fechafin"] != DBNull.Value ? Convert.ToDateTime(reader["fechafin"]) : (DateTime?)null,
                                usuarioredtde = reader["usuarioredtde"].ToString(),
                                usuarioportal = reader["usuarioportal"].ToString(),
                                idtipolicencia = reader["idtipolicencia"] != DBNull.Value ? Convert.ToInt32(reader["idtipolicencia"]) : 0,
                                nombretipolicencia = reader["nombretipolicencia"].ToString(),
                                observacionlicencia = reader["observacionlicencia"].ToString(),
                                idtipoestado = reader["idtipoestado"] != DBNull.Value ? Convert.ToInt32(reader["idtipoestado"]) : 0,
                                nombretipoestado = reader["nombretipoestado"].ToString(),
                                idtipotrabajo = reader["idtipotrabajo"] != DBNull.Value ? Convert.ToInt32(reader["idtipotrabajo"]) : 0,
                                nombretipotrabajo = reader["nombretipotrabajo"].ToString(),
                                idtipofuncionalidad = reader["idtipofuncionalidad"] != DBNull.Value ? Convert.ToInt32(reader["idtipofuncionalidad"]) : 0,
                                nombretipofuncionalidad = reader["nombretipofuncionalidad"].ToString(),
                                referente = reader["referente"].ToString(),
                                gestante = reader["gestante"].ToString(),
                                fechacarnetsanidad = reader["fechacarnetsanidad"] != DBNull.Value ? Convert.ToDateTime(reader["fechacarnetsanidad"]) : (DateTime?)null,
                                idemppaisnegcue = reader["idemppaisnegcue"] != DBNull.Value ? Convert.ToInt32(reader["idemppaisnegcue"]) : 0,
                                fechacese = reader["fechacese"] != DBNull.Value ? Convert.ToDateTime(reader["fechacese"]) : (DateTime?)null,
                                estado = reader["estado"] != DBNull.Value ? Convert.ToInt32(reader["estado"]) : 0,
                                usuariocreacion = reader["usuariocreacion"].ToString(),
                                fechacreacion = reader["fechacreacion"] != DBNull.Value ? Convert.ToDateTime(reader["fechacreacion"]) : (DateTime?)null,
                                usuariomodificacion = reader["usuariomodificacion"].ToString(),
                                fechamodificacion = reader["fechamodificacion"] != DBNull.Value ? Convert.ToDateTime(reader["fechamodificacion"]) : (DateTime?)null,

                            };

                            listaPromotores.Add(promotorRol);
                        }
                    }
                }

                return listaPromotores;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener los roles de promotores: " + ex.Message);
            }
        }

        public List<Roles> GetRolPdvTPF(int idemppaisnegcue)
        {
            var listaPromotores = new List<Roles>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_GETROLPDV", connection))
                {
                    connection.Open(); // Abrir la conexión sincrónicamente

                    command.CommandTimeout = 0; // Sin límite de tiempo
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros
                    command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader()) // Lectura sincrónica
                    {
                        while (reader.Read())
                        {
                            var promotorRol = new Roles
                            {

                                idpdv = reader["idpdv"] != DBNull.Value ? Convert.ToInt32(reader["idpdv"]) : 0,
                                nombrepdv = reader["nombrepdv"].ToString(),
                            };
                            listaPromotores.Add(promotorRol);
                        }
                    }
                }

                return listaPromotores;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener los roles de promotores: " + ex.Message);
            }
        }



        public List<Respuesta> PostRolesTPF(List<Roles> roles)
        {
            var respuestas = new List<Respuesta>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_POSTROLES", connection))
                {
                    connection.Open(); // Abrir la conexión de manera sincrónica

                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro de tipo tabla para RolData
                    SqlParameter tvpParam = command.Parameters.AddWithValue("@RolData", CreateRolDataTableTPF(roles));
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.RetRolType"; // Especificar el nombre del tipo

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader()) // Ejecución y lectura sincrónica
                    {
                        while (reader.Read())
                        {
                            var mensaje = reader["MENSAJE"].ToString();
                            var respuesta = new Respuesta
                            {
                                Success = !mensaje.ToLower().Contains("error") && !mensaje.ToLower().Contains("solapamiento"),
                                Message = mensaje
                            };
                            respuestas.Add(respuesta);
                        }
                    }
                }

                // Si no hay mensajes devueltos por el procedimiento almacenado, se asume éxito
                if (respuestas.Count == 0)
                {
                    respuestas.Add(new Respuesta
                    {
                        Success = true,
                        Message = "Registros insertados correctamente en ret_rol."
                    });
                }

                return respuestas;
            }
            catch (Exception ex)
            {
                // En caso de excepción, devolver una lista con un solo elemento de error
                return new List<Respuesta>
        {
            new Respuesta
            {
                Success = false,
                Message = "Error: " + ex.Message
            }
        };
            }
        }

        public List<Respuesta> PutRolesTPF(List<Roles> roles)
        {
            var respuestas = new List<Respuesta>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                using (SqlCommand command = new SqlCommand("USP_PUTROLES", connection))
                {
                    connection.Open(); // Abrir la conexión de manera sincrónica

                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro de tipo tabla para RolData
                    SqlParameter tvpParam = command.Parameters.AddWithValue("@RolEditData", EditRolDataTableTPF(roles));
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.RetRolEditType"; // Especificar el nombre del tipo

                    // Ejecutar el procedimiento almacenado y leer los resultados
                    using (var reader = command.ExecuteReader()) // Ejecución y lectura sincrónica
                    {
                        while (reader.Read())
                        {
                            var mensaje = reader["MENSAJE"].ToString();
                            var respuesta = new Respuesta
                            {
                                Success = !mensaje.ToLower().Contains("error") && !mensaje.ToLower().Contains("solapamiento"),
                                Message = mensaje
                            };
                            respuestas.Add(respuesta);
                        }
                    }
                }

                // Si no hay mensajes devueltos por el procedimiento almacenado, se asume éxito
                if (respuestas.Count == 0)
                {
                    respuestas.Add(new Respuesta
                    {
                        Success = true,
                        Message = "Registros insertados correctamente en ret_rol."
                    });
                }

                return respuestas;
            }
            catch (Exception ex)
            {
                // En caso de excepción, devolver una lista con un solo elemento de error
                return new List<Respuesta>
        {
            new Respuesta
            {
                Success = false,
                Message = "Error: " + ex.Message
            }
        };
            }
        }


        private DataTable CreateRolDataTableTPF(List<Roles> roles)
        {
            DataTable table = new DataTable();

            // Definición de las columnas según la nueva estructura
            table.Columns.Add("docusuario", typeof(string));
            table.Columns.Add("idpdv", typeof(int));
            table.Columns.Add("puntoventa", typeof(string));
            table.Columns.Add("fechainicio", typeof(DateTime));
            table.Columns.Add("fechafin", typeof(DateTime));
            table.Columns.Add("usuarioredtde", typeof(string));
            table.Columns.Add("usuarioportal", typeof(string));
            table.Columns.Add("idtipolicencia", typeof(int));
            table.Columns.Add("observacionlicencia", typeof(string));
            table.Columns.Add("idtipoestado", typeof(int));
            table.Columns.Add("idtipotrabajo", typeof(int));
            table.Columns.Add("idtipofuncionalidad", typeof(int));
            table.Columns.Add("referente", typeof(string));
            table.Columns.Add("gestante", typeof(string));
            table.Columns.Add("fechacarnetsanidad", typeof(DateTime));
            table.Columns.Add("idemppaisnegcue", typeof(int));
            table.Columns.Add("fechacese", typeof(DateTime));
            table.Columns.Add("estado", typeof(int));
            table.Columns.Add("usuariocreacion", typeof(string));
            table.Columns.Add("fechacreacion", typeof(DateTime));

            foreach (var rol in roles)
            {
                table.Rows.Add(
                    rol.docusuario ?? (object)DBNull.Value,
                    rol.idpdv == 0 ? (object)DBNull.Value : rol.idpdv,
                    rol.nombrepdv ?? (object)DBNull.Value,
                    rol.fechainicio ?? (object)DBNull.Value,
                    rol.fechafin ?? (object)DBNull.Value,
                    rol.usuarioredtde ?? (object)DBNull.Value,
                    rol.usuarioportal ?? (object)DBNull.Value,
                    rol.idtipolicencia == 0 ? (object)DBNull.Value : rol.idtipolicencia,
                    rol.observacionlicencia ?? (object)DBNull.Value,
                    rol.idtipoestado == 0 ? (object)DBNull.Value : rol.idtipoestado,
                    rol.idtipotrabajo == 0 ? (object)DBNull.Value : rol.idtipotrabajo,
                    rol.idtipofuncionalidad == 0 ? (object)DBNull.Value : rol.idtipofuncionalidad,
                    rol.referente ?? (object)DBNull.Value,
                    rol.gestante ?? (object)DBNull.Value,
                    rol.fechacarnetsanidad ?? (object)DBNull.Value,
                    rol.idemppaisnegcue == 0 ? (object)DBNull.Value : rol.idemppaisnegcue,
                    rol.fechacese ?? (object)DBNull.Value,
                    rol.estado == 0 ? (object)DBNull.Value : rol.estado,
                    rol.usuariocreacion ?? (object)DBNull.Value,
                    DateTime.Now // o rol.fechacreacion si existe en tu objeto
                );
            }

            return table;
        }


        private DataTable EditRolDataTableTPF(List<Roles> roles)
        {
            DataTable table = new DataTable();

            // Definición de las columnas según la nueva estructura
            table.Columns.Add("idrol", typeof(int));
            table.Columns.Add("docusuario", typeof(string));
            table.Columns.Add("idpdv", typeof(int));
            table.Columns.Add("fechainicio", typeof(DateTime));
            table.Columns.Add("fechafin", typeof(DateTime));
            table.Columns.Add("usuarioredtde", typeof(string));
            table.Columns.Add("usuarioportal", typeof(string));
            table.Columns.Add("idtipolicencia", typeof(int));
            table.Columns.Add("observacionlicencia", typeof(string));
            table.Columns.Add("idtipoestado", typeof(int));
            table.Columns.Add("idtipotrabajo", typeof(int));
            table.Columns.Add("idtipofuncionalidad", typeof(int));
            table.Columns.Add("referente", typeof(string));
            table.Columns.Add("gestante", typeof(string));
            table.Columns.Add("fechacarnetsanidad", typeof(DateTime));
            table.Columns.Add("idemppaisnegcue", typeof(int));
            table.Columns.Add("fechacese", typeof(DateTime));
            table.Columns.Add("estado", typeof(int));
            table.Columns.Add("usuariomodificacion", typeof(string));
            table.Columns.Add("fechamodificacion", typeof(DateTime));

            foreach (var rol in roles)
            {
                table.Rows.Add(
                    rol.idrol,
                    rol.docusuario ?? (object)DBNull.Value,
                    rol.idpdv == 0 ? (object)DBNull.Value : rol.idpdv,
                    rol.fechainicio ?? (object)DBNull.Value,
                    rol.fechafin ?? (object)DBNull.Value,
                    rol.usuarioredtde ?? (object)DBNull.Value,
                    rol.usuarioportal ?? (object)DBNull.Value,
                    rol.idtipolicencia == 0 ? (object)DBNull.Value : rol.idtipolicencia,
                    rol.observacionlicencia ?? (object)DBNull.Value,
                    rol.idtipoestado == 0 ? (object)DBNull.Value : rol.idtipoestado,
                    rol.idtipotrabajo == 0 ? (object)DBNull.Value : rol.idtipotrabajo,
                    rol.idtipofuncionalidad == 0 ? (object)DBNull.Value : rol.idtipofuncionalidad,
                    rol.referente ?? (object)DBNull.Value,
                    rol.gestante ?? (object)DBNull.Value,
                    rol.fechacarnetsanidad ?? (object)DBNull.Value,
                    rol.idemppaisnegcue == 0 ? (object)DBNull.Value : rol.idemppaisnegcue,
                    rol.fechacese ?? (object)DBNull.Value,
                    rol.estado,
                    rol.usuariomodificacion ?? (object)DBNull.Value,
                    DateTime.Now // o rol.fechacreacion si existe en tu objeto
                );
            }

            return table;
        }



    }


}
