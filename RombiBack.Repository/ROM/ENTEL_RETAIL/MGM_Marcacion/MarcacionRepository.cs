using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Incentivos.Dto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Marcacion
{
    public class MarcacionRepository:IMarcacionRepository
    {
        private readonly DataAcces _dbConnection;

        public MarcacionRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public Respuesta PostEmpleadoAsistenciaMarcacion(string docusuario, DateTime fechamarcacion)
        {
            var respuesta = new Respuesta();
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionBIOMETRIATAWA()))
                using (SqlCommand command = new SqlCommand(@"
                   INSERT INTO MANTENIMIENTO.EMPLEADO_ASITENCIA
	                SELECT TOP 1 ID 
	                    ,COORDENADAS = NULL
	                    ,fechamarcacion = @fechaMarcacion
	                    ,estado = 1
	                    ,fecharegistro = getdate()
	                    ,terminal_registro = null
	                    ,COORDENADAS_registro = null
	                FROM DBO.VIEW_EMPLEADO WHERE nroDoc IN (@nroDoc)", connection))
                {
                    // Configuración de los parámetros
                    command.Parameters.AddWithValue("@nroDoc", docusuario);
                    command.Parameters.AddWithValue("@fechaMarcacion", fechamarcacion);

                    connection.Open(); // Abrir la conexión sincrónicamente
                    int rowsAffected = command.ExecuteNonQuery(); // Ejecutar la consulta de inserción

                    if (rowsAffected > 0)
                    {
                        respuesta.Success = true;
                        respuesta.Mensaje = $"INSERTADO la hora {fechamarcacion:HH:mm:ss} y la fecha {fechamarcacion:yyyy-MM-dd} para el DNI {docusuario}";
                        respuesta.InsertedRecordsCount = rowsAffected;
                    }
                    else
                    {
                        respuesta.Success = false;
                        respuesta.Mensaje = "No se encontró un empleado con el DNI especificado.";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Success = false;
                respuesta.Mensaje = "Error al insertar en EMPLEADO_ASITENCIA: " + ex.Message;
                respuesta.Errors = new List<string> { ex.Message };
            }

            return respuesta;
        }


        public UserDataDTOResponse GetUsuarioMarcacion(string docusuario)
        {
            var usuarioResponse = new UserDataDTOResponse();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    using (SqlCommand command = new SqlCommand("USP_GETUSUARIOMARCACION", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DNI", docusuario);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Si se encuentran resultados, leer y mapear datos del usuario
                                while (reader.Read())
                                {
                                    usuarioResponse.usuario = docusuario;
                                    usuarioResponse.nombres = reader["NOMBRES"].ToString();
                                    usuarioResponse.apellidopaterno = reader["APELLIDOPATERNO"].ToString();
                                    usuarioResponse.apellidomaterno = reader["APELLIDOMATERNO"].ToString();
                                }
                                usuarioResponse.Success = true;
                                usuarioResponse.Mensaje = "Usuario encontrado";
                            }
                            else
                            {
                                // Si no hay resultados, retornar mensaje de "Usuario no encontrado"
                                usuarioResponse.Success = false;
                                usuarioResponse.Mensaje = "Usuario no encontrado";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                usuarioResponse.Success = false;
                usuarioResponse.Mensaje = "Error al buscar el usuario: " + ex.Message;
            }

            return usuarioResponse;
        }

    }
}
