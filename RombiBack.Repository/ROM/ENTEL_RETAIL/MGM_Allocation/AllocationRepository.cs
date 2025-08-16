using RombiBack.Entities.ROM.ENTEL_RETAIL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas;
using Microsoft.AspNetCore.Rewrite;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using StackExchange.Redis;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Allocation;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Allocation
{
    public class AllocationRepository:IAllocationRepository
    {
        private readonly DataAcces _dbConnection;

        public AllocationRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public List<Roles> GetAllRolPromotor(string usuario, int idemppaisnegcue, string tipoperiodo)
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


        public List<Roles> GetRolUsuarioPDV(string usuario, int idemppaisnegcue, string tipoperiodo)
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

        public List<Respuesta> ValidarBotonRegistroVentas(string usuario, int idemppaisnegcue)
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

        public List<Roles> GetRolTipoFuncionalidad(int idemppaisnegcue)
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

                                idtipofuncionalidad = reader["idtipofuncionalidad"] != DBNull.Value ?  Convert.ToInt32(reader["idtipofuncionalidad"]) : 0,
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

        public List<Roles> GetRolTipoEstado(int idemppaisnegcue)
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


        public List<Roles> GetRolTipoTrabajo(int idemppaisnegcue)
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

        public List<Roles> GetRolTipoLicencia(int idemppaisnegcue)
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



        public List<Roles> GetRolPromotorDocUsuario(string usuario, int idemppaisnegcue, string tipoperiodo,string usuarioperfil)
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

        public List<Roles> GetRolPdv(int idemppaisnegcue)
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



        public List<Respuesta> PostRoles(List<Roles> roles)
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
                    SqlParameter tvpParam = command.Parameters.AddWithValue("@RolData", CreateRolDataTable(roles));
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

        public List<Respuesta> PutRoles(List<Roles> roles)
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
                    SqlParameter tvpParam = command.Parameters.AddWithValue("@RolEditData", EditRolDataTable(roles));
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


        private DataTable CreateRolDataTable(List<Roles> roles)
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


        private DataTable EditRolDataTable(List<Roles> roles)
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



        public List<UsuariosEntel> GetUsuariosEntel(int idemppaisnegcue)
        {
            var usuarios = new List<UsuariosEntel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_GETUSUARIOENTEL", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usuarios.Add(new UsuariosEntel
                                {
                                    idusuarioentel = reader.IsDBNull(reader.GetOrdinal("idusuarioentel")) ? 0 : reader.GetInt32(reader.GetOrdinal("idusuarioentel")),
                                    docusuario = reader.IsDBNull(reader.GetOrdinal("docusuario")) ? string.Empty : reader.GetString(reader.GetOrdinal("docusuario")),
                                    usuarioredtde = reader.IsDBNull(reader.GetOrdinal("usuarioredtde")) ? string.Empty : reader.GetString(reader.GetOrdinal("usuarioredtde")),
                                    usuarioportal = reader.IsDBNull(reader.GetOrdinal("usuarioportal")) ? string.Empty : reader.GetString(reader.GetOrdinal("usuarioportal")),
                                    correocorp = reader.IsDBNull(reader.GetOrdinal("correocorp")) ? string.Empty : reader.GetString(reader.GetOrdinal("correocorp")),
                                    celular = reader.IsDBNull(reader.GetOrdinal("celular")) ? string.Empty : reader.GetString(reader.GetOrdinal("celular")),
                                    idemppaisnegcue = reader.IsDBNull(reader.GetOrdinal("idemppaisnegcue")) ? 0 : reader.GetInt32(reader.GetOrdinal("idemppaisnegcue")),
                                    estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("estado")),
                                    //usuariocreacion = reader.IsDBNull(reader.GetOrdinal("usuariocreacion")) ? string.Empty : reader.GetString(reader.GetOrdinal("usuariocreacion")),
                                    //fechacreacion = reader.IsDBNull(reader.GetOrdinal("fechacreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechacreacion")),
                                    //usuariomodificacion = reader.IsDBNull(reader.GetOrdinal("usuariomodificacion")) ? string.Empty : reader.GetString(reader.GetOrdinal("usuariomodificacion")),
                                    //fechamodificacion = reader.IsDBNull(reader.GetOrdinal("fechamodificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechamodificacion")),
                                    //usuarioanulacion = reader.IsDBNull(reader.GetOrdinal("usuarioanulacion")) ? string.Empty : reader.GetString(reader.GetOrdinal("usuarioanulacion")),
                                    //fechaanulacion = reader.IsDBNull(reader.GetOrdinal("fechaanulacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaanulacion"))

                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios de Entel: " + ex.Message);
            }

            return usuarios;
        }
        public List<Respuesta> PostUsuarioEntel(List<UsuariosEntel> usuariosentel)
        {
            var respuestas = new List<Respuesta>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction()) // Inicia una transacción
                    {
                        try
                        {
                            foreach (var userentel in usuariosentel)
                            {
                                using (SqlCommand command = new SqlCommand("USP_POSTUSUARIOENTEL", connection, transaction))
                                {
                                    command.CommandType = CommandType.StoredProcedure;

                                    // Parámetros de entrada
                                    command.Parameters.AddWithValue("@docusuario", userentel.docusuario);
                                    command.Parameters.AddWithValue("@usuarioredtde", userentel.usuarioredtde);
                                    command.Parameters.AddWithValue("@usuarioportal", userentel.usuarioportal);
                                    command.Parameters.AddWithValue("@correocorp", userentel.correocorp);
                                    command.Parameters.AddWithValue("@celular", userentel.celular);
                                    command.Parameters.AddWithValue("@estado", userentel.estado);
                                    command.Parameters.AddWithValue("@idemppaisnegcue", userentel.idemppaisnegcue);
                                    command.Parameters.AddWithValue("@usuariocreacion", userentel.usuariocreacion);

                                    // Parámetro de salida
                                    SqlParameter outputParam = new SqlParameter("@mensajeSalida", SqlDbType.VarChar, 255)
                                    {
                                        Direction = ParameterDirection.Output
                                    };
                                    command.Parameters.Add(outputParam);

                                    command.ExecuteNonQuery(); // Ejecutar SP

                                    // Obtener mensaje de salida
                                    string mensajeSalida = outputParam.Value.ToString();

                                    respuestas.Add(new Respuesta
                                    {
                                        Success = mensajeSalida.Contains("correctamente"), // Si el mensaje indica éxito
                                        Message = mensajeSalida
                                    });
                                }
                            }

                            transaction.Commit(); // Confirmar transacción si todo está bien
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Revertir transacción en caso de error
                            respuestas.Add(new Respuesta
                            {
                                Success = false,
                                Message = "Error en la operación: " + ex.Message
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuestas.Add(new Respuesta
                {
                    Success = false,
                    Message = "Error en la conexión: " + ex.Message
                });
            }

            return respuestas;
        }

        public Respuesta PutUsuarioEntel(UsuariosEntel usuarioEntel)
        {
            var respuesta = new Respuesta();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("USP_PUTUSUARIOENTEL", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                // Parámetros de entrada
                                command.Parameters.AddWithValue("@idusuarioentel", usuarioEntel.idusuarioentel);
                                command.Parameters.AddWithValue("@docusuario", usuarioEntel.docusuario);
                                command.Parameters.AddWithValue("@usuarioredtde", usuarioEntel.usuarioredtde);
                                command.Parameters.AddWithValue("@usuarioportal", usuarioEntel.usuarioportal);
                                command.Parameters.AddWithValue("@correocorp", usuarioEntel.correocorp);
                                command.Parameters.AddWithValue("@idemppaisnegcue ", usuarioEntel.idemppaisnegcue);
                                command.Parameters.AddWithValue("@celular", usuarioEntel.celular);
                                command.Parameters.AddWithValue("@usuariomodificacion", usuarioEntel.usuariomodificacion);

                                // Parámetro de salida
                                SqlParameter outputParam = new SqlParameter("@mensajeSalida", SqlDbType.VarChar, 255)
                                {
                                    Direction = ParameterDirection.Output
                                };
                                command.Parameters.Add(outputParam);

                                command.ExecuteNonQuery();

                                // Obtener mensaje de salida
                                string mensajeSalida = outputParam.Value.ToString();
                                respuesta.Success = mensajeSalida.Contains("correctamente");
                                respuesta.Message = mensajeSalida;
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            respuesta.Success = false;
                            respuesta.Message = "Error en la operación: " + ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Success = false;
                respuesta.Message = "Error en la conexión: " + ex.Message;
            }

            return respuesta;
        }

        public Respuesta DeleteUsuarioEntel(int idUsuarioEntel, string usuarioEliminacion)
        {
            var respuesta = new Respuesta();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("USP_DELETEUSUARIOENTEL", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                // Parámetros de entrada
                                command.Parameters.AddWithValue("@idusuarioentel", idUsuarioEntel);
                                command.Parameters.AddWithValue("@usuarioeliminacion", usuarioEliminacion);

                                // Parámetro de salida
                                SqlParameter outputParam = new SqlParameter("@mensajeSalida", SqlDbType.VarChar, 255)
                                {
                                    Direction = ParameterDirection.Output
                                };
                                command.Parameters.Add(outputParam);

                                command.ExecuteNonQuery();

                                // Obtener mensaje de salida
                                string mensajeSalida = outputParam.Value.ToString();
                                respuesta.Success = mensajeSalida.Contains("correctamente");
                                respuesta.Message = mensajeSalida;
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            respuesta.Success = false;
                            respuesta.Message = "Error en la operación: " + ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Success = false;
                respuesta.Message = "Error en la conexión: " + ex.Message;
            }

            return respuesta;
        }


        public NombreExcel GetNombreExcel(int idemppaisnegcue, string vista)
        {
            NombreExcel resultado = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_GETNOMBREEXCEL", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idemppaisnegcue", idemppaisnegcue);
                        command.Parameters.AddWithValue("@vista", vista);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                resultado = new NombreExcel
                                {
                                    nombrearchivo = reader.IsDBNull(reader.GetOrdinal("nombrearchivo")) ? string.Empty : reader.GetString(reader.GetOrdinal("nombrearchivo")),
                                    usuariocreacion = reader.IsDBNull(reader.GetOrdinal("usuariocreacion")) ? string.Empty : reader.GetString(reader.GetOrdinal("usuariocreacion")),
                                    //fechacreacion = reader.IsDBNull(reader.GetOrdinal("fechacreacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechacreacion")),
                                    //usuariomodificacion = reader.IsDBNull(reader.GetOrdinal("usuariomodificacion")) ? string.Empty : reader.GetString(reader.GetOrdinal("usuariomodificacion")),
                                    //fechamodificacion = reader.IsDBNull(reader.GetOrdinal("fechamodificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechamodificacion")),
                                    //UsuarioEliminacion = reader.IsDBNull(reader.GetOrdinal("usuarioeliminacion")) ? string.Empty : reader.GetString(reader.GetOrdinal("usuarioeliminacion")),
                                    //FechaEliminacion = reader.IsDBNull(reader.GetOrdinal("fechaeliminacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fechaeliminacion"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos de NombreExcel: " + ex.Message);
            }

            return resultado;
        }
    }



}
