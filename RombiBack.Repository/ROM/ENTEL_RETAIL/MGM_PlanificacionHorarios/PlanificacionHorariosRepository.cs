using Microsoft.EntityFrameworkCore;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Abstraction;
using System.Reflection.PortableExecutable;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios
{
    public class PlanificacionHorariosRepository : IPlanificacionHorariosRepository
    {
        private readonly DataAcces _dbConnection;

        public PlanificacionHorariosRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<TurnosSupervisor>> GetTurnosSupervisor(string usuario, int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTURNOSSUPERVISOR", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.VarChar).Value = idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<TurnosSupervisor> response = new List<TurnosSupervisor>();

                                while (await reader.ReadAsync())
                                {
                                    TurnosSupervisor trn = new TurnosSupervisor();
                                    trn.idturnos = reader.GetInt32(reader.GetOrdinal("idturnos"));
                                    trn.usuario = reader.GetString(reader.GetOrdinal("usuario"));
                                    trn.horarioentrada = reader.GetString(reader.GetOrdinal("horarioentrada"));
                                    trn.horariosalida = reader.GetString(reader.GetOrdinal("horariosalida"));
                                    trn.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                    trn.idtipoturno = reader.GetInt32(reader.GetOrdinal("idtipoturno"));
                                    trn.estado = reader.GetInt32(reader.GetOrdinal("estado"));
                                    trn.fecha_creacion = reader.GetDateTime(reader.GetOrdinal("fecha_creacion"));
                                    trn.fecha_modificacion = reader.IsDBNull(reader.GetOrdinal("fecha_modificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fecha_modificacion"));
                                    trn.usuario_creacion = reader.GetString(reader.GetOrdinal("usuario_creacion"));
                                    trn.usuario_modificacion = reader.GetString(reader.GetOrdinal("usuario_modificacion"));

                                    response.Add(trn);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<TurnosSupervisor>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Por ejemplo, podrías registrar el error y devolver un mensaje de error adecuado
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }
        }

        public async Task<Respuesta> PostTurnosSupervisor(TurnosSupervisorRequest turnosSupervisor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_POSTTURNOSUPERVISOR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnosSupervisor.usuario;
                        cmd.Parameters.Add("@horarioentrada", SqlDbType.VarChar).Value = turnosSupervisor.horarioentrada;
                        cmd.Parameters.Add("@horariosalida", SqlDbType.VarChar).Value = turnosSupervisor.horariosalida;
                        cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = turnosSupervisor.descripcion;
                        cmd.Parameters.Add("@idtipoturno", SqlDbType.Int).Value = turnosSupervisor.idtipoturno;
                        cmd.Parameters.Add("@usuario_creacion", SqlDbType.VarChar).Value = turnosSupervisor.usuario_creacion;
                        cmd.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = turnosSupervisor.idemppaisnegcue;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            
                            Respuesta respuesta = new Respuesta();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));
                             
                                // Puedes manejar múltiples filas si es necesario
                                // Por ejemplo, almacenar cada resultado en una lista
                            }

                            return respuesta;
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }


        public async Task<Respuesta> PutTurnosSupervisor(TurnosSupervisor turnossuper)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_PUTTURNOSSUPERVISOR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure; 
                        cmd.Parameters.Add("@idturnos", SqlDbType.Int).Value = turnossuper.idturnos;
                        cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnossuper.usuario;
                        cmd.Parameters.Add("@horarioentrada", SqlDbType.VarChar).Value = turnossuper.horarioentrada;
                        cmd.Parameters.Add("@horariosalida", SqlDbType.VarChar).Value = turnossuper.horariosalida;
                        cmd.Parameters.Add("@usuario_modificacion", SqlDbType.VarChar).Value = turnossuper.usuario_modificacion;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            Respuesta respuesta = new Respuesta();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));

                                // Puedes manejar múltiples filas si es necesario
                                // Por ejemplo, almacenar cada resultado en una lista
                            }

                            return respuesta;
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }

        public async Task<Respuesta> DeleteTurnosSupervisor(TurnosSupervisor turnossuper)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_DELETETURNOSSUPERVISOR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idturnos", SqlDbType.Int).Value = turnossuper.idturnos;
                        cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnossuper.usuario;
                        cmd.Parameters.Add("@usuario_modificacion", SqlDbType.VarChar).Value = turnossuper.usuario_modificacion;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            Respuesta respuesta = new Respuesta();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));

                                // Puedes manejar múltiples filas si es necesario
                                // Por ejemplo, almacenar cada resultado en una lista
                            }

                            return respuesta;
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }



        public async Task<List<SupervisorPdvResponse>> GetSupervisorPDV(string usuario, int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETSUPERVISORPDV", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<SupervisorPdvResponse> response = new List<SupervisorPdvResponse>();

                                while (await reader.ReadAsync())
                                {
                                    SupervisorPdvResponse supervisorpdv = new SupervisorPdvResponse();
                                    supervisorpdv.usuario = reader.GetString(reader.GetOrdinal("usuario"));
                                    supervisorpdv.idpuntoventarol = reader.GetInt32(reader.GetOrdinal("idpuntoventa_rol"));
                                    supervisorpdv.puntoventa = reader.GetString(reader.GetOrdinal("puntoventa"));
                                   

                                    response.Add(supervisorpdv);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<SupervisorPdvResponse>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Por ejemplo, podrías registrar el error y devolver un mensaje de error adecuado
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }
        }



        public async Task<List<TurnosSupervisor>> GetTurnosDisponiblePDV(TurnosDisponiblesPdvRequest turnodispo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTURNOSDISPONIBLESPDV", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnodispo.usuario;
                        command.Parameters.Add("@idpdv", SqlDbType.Int).Value = turnodispo.idpdv;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = turnodispo.idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<TurnosSupervisor> response = new List<TurnosSupervisor>();

                                while (await reader.ReadAsync())
                                {
                                    TurnosSupervisor supervisorpdv = new TurnosSupervisor();
                                    supervisorpdv.idturnos = reader.GetInt32(reader.GetOrdinal("idturnos"));
                                    supervisorpdv.usuario = reader.GetString(reader.GetOrdinal("usuario"));
                                    supervisorpdv.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                    supervisorpdv.horarioentrada = reader.GetString(reader.GetOrdinal("horarioentrada"));
                                    supervisorpdv.horariosalida = reader.GetString(reader.GetOrdinal("horariosalida"));
                                    supervisorpdv.estado = reader.GetInt32(reader.GetOrdinal("estado"));


                                    response.Add(supervisorpdv);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<TurnosSupervisor>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Por ejemplo, podrías registrar el error y devolver un mensaje de error adecuado
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }
        }

        public async Task<Respuesta> PostTurnosPDV(List<TurnosPdvRequest> turnosPdvList)
        {
            try
            {
                Respuesta ultimaRespuesta = new Respuesta(); // Inicializamos la respuesta fuera del bucle

                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    foreach (var turnospdv in turnosPdvList)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_POSTTURNOSPDV", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnospdv.usuario;
                            cmd.Parameters.Add("@idpdv", SqlDbType.Int).Value = turnospdv.idpdv;
                            cmd.Parameters.Add("@puntoventa", SqlDbType.VarChar).Value = turnospdv.puntoventa;
                            cmd.Parameters.Add("@idturnos", SqlDbType.Int).Value = turnospdv.idturnos;
                            cmd.Parameters.Add("@usuario_creacion", SqlDbType.Int).Value = turnospdv.usuario_creacion;
                            cmd.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = turnospdv.idemppaisnegcue;

                            using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                            {
                                while (await rdr.ReadAsync())
                                {
                                    ultimaRespuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));
                                }
                            }
                        }
                    }
                }
                // Devolver la última respuesta obtenida
                return ultimaRespuesta;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }



        public async Task<List<TurnosSupervisor>> GetTurnosAsignadosPDV(TurnosDisponiblesPdvRequest turnodispo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTURNOSASIGNADOSPDV", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnodispo.usuario;
                        command.Parameters.Add("@idpdv", SqlDbType.Int).Value = turnodispo.idpdv;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = turnodispo.idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<TurnosSupervisor> response = new List<TurnosSupervisor>();

                                while (await reader.ReadAsync())
                                {
                                    TurnosSupervisor supervisorpdv = new TurnosSupervisor();
                                    supervisorpdv.idpdvturno = reader.GetInt32(reader.GetOrdinal("idpdvturno"));
                                    supervisorpdv.idturnos = reader.GetInt32(reader.GetOrdinal("idturnos"));
                                    supervisorpdv.usuario = reader.GetString(reader.GetOrdinal("usuario"));
                                    supervisorpdv.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                    supervisorpdv.horarioentrada = reader.GetString(reader.GetOrdinal("horarioentrada"));
                                    supervisorpdv.horariosalida = reader.GetString(reader.GetOrdinal("horariosalida"));
                                    //supervisorpdv.estado = reader.GetInt32(reader.GetOrdinal("estado"));


                                    response.Add(supervisorpdv);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<TurnosSupervisor>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Por ejemplo, podrías registrar el error y devolver un mensaje de error adecuado
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }
        }

        public async Task<Respuesta> DeleteTurnosPDV(TurnosPdvRequest turnospdv)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_DELETETURNOSPDV", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idpdvturno", SqlDbType.Int).Value = turnospdv.idpdvturno;
                        cmd.Parameters.Add("@usuario_modificacion", SqlDbType.Int).Value = turnospdv.usuario_modificacion;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            Respuesta respuesta = new Respuesta();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));

                            }

                            return respuesta;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }

        public async Task<List<FechasSemana>> GetRangoSemana(string perfil)
        {
            try
            {
                using (var connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("USP_GETRANGOSEMANA_PERFIL", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@perfil", SqlDbType.VarChar).Value = perfil;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                var resultados = new List<FechasSemana>();

                                while (await reader.ReadAsync())
                                {
                                    var fechaSemana = new FechasSemana
                                    {
                                        lunes = reader.GetString(reader.GetOrdinal("lunes")),
                                        domingo = reader.GetString(reader.GetOrdinal("domingo"))
                                    };

                                    resultados.Add(fechaSemana);
                                }

                                return resultados;
                            }
                            else
                            {
                                return new List<FechasSemana>();
                            }
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


        public async Task<List<PromotorSupervisorPdvResponse>> GetPromotorSupervisorPDV(SupervisorPdvResponse promotorsuperpdv)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("USP_GETPROMOTORSUPERVISORPDV", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@dnisupervisor", SqlDbType.VarChar).Value = promotorsuperpdv.dnisupervisor;
                        command.Parameters.Add("@idpuntoventarol", SqlDbType.Int).Value = promotorsuperpdv.idpuntoventarol;
                        command.Parameters.Add("@fechainicio", SqlDbType.VarChar).Value = promotorsuperpdv.fechainicio;
                        command.Parameters.Add("@fechafin", SqlDbType.VarChar).Value = promotorsuperpdv.fechafin;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = promotorsuperpdv.idemppaisnegcue;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<PromotorSupervisorPdvResponse> response = new List<PromotorSupervisorPdvResponse>();

                                while (await reader.ReadAsync())
                                {
                                    PromotorSupervisorPdvResponse supervisorpdv = new PromotorSupervisorPdvResponse();
                                    supervisorpdv.dnipromotor = reader.GetString(reader.GetOrdinal("dnipromotor"));
                                    supervisorpdv.nombrepromotor = reader.GetString(reader.GetOrdinal("nombrepromotor"));
                                    supervisorpdv.apellidopaternopromotor = reader.GetString(reader.GetOrdinal("apellidopaternopromotor"));
                                    supervisorpdv.apellidomaternopromotor = reader.GetString(reader.GetOrdinal("apellidomaternopromotor"));
                                    supervisorpdv.idpuntoventarol = reader.GetInt32(reader.GetOrdinal("idpuntoventa_rol"));
                                    supervisorpdv.puntoventa = reader.GetString(reader.GetOrdinal("puntoventa"));
                                   
                                    response.Add(supervisorpdv);
                                }
                                return response;
                            }
                            else
                            {
                                return new List<PromotorSupervisorPdvResponse>(); 
                            }
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

        public async Task<List<DiasSemana>> GetDiasSemana(FechasSemana fechassemana)
        {
            try
            {
                List<DiasSemana> listaDiasSemana = new List<DiasSemana>();

                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("USP_GETDIASSEMAMA", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar).Value = fechassemana.lunes;
                        cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = fechassemana.domingo;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                DiasSemana diaSemana = new DiasSemana();

                                diaSemana.dia = rdr.GetString(rdr.GetOrdinal("dia"));
                                diaSemana.fecha = rdr.GetString(rdr.GetOrdinal("fecha"));

                                listaDiasSemana.Add(diaSemana);
                            }
                        }
                    }
                }

                return listaDiasSemana;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }

        public async Task<List<TurnosSupervisorPdvHorariosResponse>> GetTurnosSupervisorPDVHorarios(TurnosDisponiblesPdvRequest superpdv)
        {
            try
            {
                
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("USP_GETTURNOSSUPERVISORPDVHORARIOS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = superpdv.usuario;
                        command.Parameters.Add("@idpdv", SqlDbType.Int).Value = superpdv.idpdv;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = superpdv.idemppaisnegcue;
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<TurnosSupervisorPdvHorariosResponse> response = new List<TurnosSupervisorPdvHorariosResponse>();

                                while (await reader.ReadAsync())
                                {
                                    TurnosSupervisorPdvHorariosResponse superpdvh = new TurnosSupervisorPdvHorariosResponse();
                                    superpdvh.usuario = reader.GetString(reader.GetOrdinal("usuario"));
                                    superpdvh.idturnos = reader.GetInt32(reader.GetOrdinal("idturnos"));
                                    superpdvh.horarioentrada = reader.GetString(reader.GetOrdinal("horarioentrada"));
                                    superpdvh.horariosalida = reader.GetString(reader.GetOrdinal("horariosalida"));
                                    superpdvh.estadopdvturno = reader.GetInt32(reader.GetOrdinal("estadopdvturno"));
                                    superpdvh.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                    superpdvh.idtipoturno = reader.GetInt32(reader.GetOrdinal("idtipoturno"));
                                    superpdvh.estadoturno = reader.GetInt32(reader.GetOrdinal("estadoturno"));

                                    response.Add(superpdvh);
                                }
                                return response;
                            }
                            else
                            {
                                return new List<TurnosSupervisorPdvHorariosResponse>();
                            }
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

        public async Task<Respuesta> PostHorarioPlanificado(List<HorarioPlanificadoRequest> horarioPlanificados)
        {
            try
            {
                Respuesta ultimaRespuesta = new Respuesta(); // Inicializamos la respuesta fuera del bucle

                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    foreach (var horarioplan in horarioPlanificados)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_POSTHORARIOPLANIFICADO", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@dnipromotor", SqlDbType.VarChar).Value = horarioplan.dnipromotor;
                            cmd.Parameters.Add("@dnisupervisor", SqlDbType.VarChar).Value = horarioplan.dnisupervisor;
                            cmd.Parameters.Add("@idpdv", SqlDbType.Int).Value = horarioplan.idpdv;
                            cmd.Parameters.Add("@puntoventa", SqlDbType.VarChar).Value = horarioplan.puntoventa;
                            cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = horarioplan.fecha;
                            cmd.Parameters.Add("@horarioentrada", SqlDbType.VarChar).Value = horarioplan.horarioentrada;
                            cmd.Parameters.Add("@horariosalida", SqlDbType.VarChar).Value = horarioplan.horariosalida;
                            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = horarioplan.descripcion;
                            cmd.Parameters.Add("@usuario_creacion", SqlDbType.VarChar).Value = horarioplan.usuario_creacion;
                            cmd.Parameters.Add("@activarcbo", SqlDbType.Int).Value = horarioplan.activarcbo;
                            cmd.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = horarioplan.idemppaisnegcue;

                            using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                            {
                                while (await rdr.ReadAsync())
                                {
                                    ultimaRespuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));
                                }
                            }
                        }
                    }
                }
                // Devolver la última respuesta obtenida
                return ultimaRespuesta;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }

        }

        public async Task<List<RespuestaValidacion>> ValidarHorariosPlanificado(List<ListaHorarioPlanificadoRequest> horarioPlanificados)
        {
            var respuestas = new List<RespuestaValidacion>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    foreach (var horario in horarioPlanificados)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_VALIDARLISTAHORARIO", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@dnipromotor", SqlDbType.VarChar).Value = horario.dnipromotor;
                            cmd.Parameters.Add("@dnisupervisor", SqlDbType.VarChar).Value = horario.dnisupervisor;
                            cmd.Parameters.Add("@puntoventa", SqlDbType.VarChar).Value = horario.puntoventa;
                            cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = horario.fecha;
                            cmd.Parameters.Add("@horarioentrada", SqlDbType.VarChar).Value = horario.horarioentrada;
                            cmd.Parameters.Add("@horariosalida", SqlDbType.VarChar).Value = horario.horariosalida;
                            cmd.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = horario.idemppaisnegcue;
                            cmd.Parameters.Add("@usuario_creacion", SqlDbType.VarChar).Value = horario.usuario_creacion;

                            using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                            {
                                while (await rdr.ReadAsync())
                                {
                                    var respuesta = new RespuestaValidacion
                                    {
                                        estado = rdr["estado"].ToString(),
                                        mensaje = rdr["mensaje"].ToString()
                                    };

                                    if (respuesta.estado == "success")
                                    {
                                        respuesta.dnipromotor = rdr["dnipromotor"].ToString();
                                        respuesta.dnisupervisor = rdr["dnisupervisor"].ToString();
                                        respuesta.puntoventa = rdr["puntoventa"].ToString();
                                        respuesta.fecha = Convert.ToDateTime(rdr["fecha"]);
                                        respuesta.horarioentrada = rdr["horarioentrada"].ToString();
                                        respuesta.horariosalida = rdr["horariosalida"].ToString();
                                        respuesta.idemppaisnegcue = Convert.ToInt32(rdr["idemppaisnegcue"]);
                                        respuesta.usuario_creacion = rdr["usuario_creacion"].ToString();
                                        respuesta.idpdv = Convert.ToInt32(rdr["idpdv"]);
                                        respuesta.activarcbo = Convert.ToInt32(rdr["activarcbo"]);
                                        respuesta.descripcion = rdr["descripcion"].ToString();
                                    }

                                    respuestas.Add(respuesta);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Loguear la excepción (según el mecanismo de logging que uses)
                Console.WriteLine($"Error en la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Loguear la excepción genérica
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

            return respuestas;
        }



        public async Task<List<HorarioPlanificadoPromotorResponse>> GetHorarioPlanificado(List<HorarioPlanificadoPromotorRequest> horarioPlanificadopromotor)
        {
            try
            {
                List<HorarioPlanificadoPromotorResponse> response = new List<HorarioPlanificadoPromotorResponse>();

                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    foreach (var horarioplanpromo in horarioPlanificadopromotor)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_GETHORARIOPLANIFICADO", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@inicio", SqlDbType.VarChar).Value = horarioplanpromo.inicio;
                            cmd.Parameters.Add("@fin", SqlDbType.VarChar).Value = horarioplanpromo.fin;
                            cmd.Parameters.Add("@idpdv", SqlDbType.Int).Value = horarioplanpromo.idpdv;
                            cmd.Parameters.Add("@dnipromotor", SqlDbType.VarChar).Value = horarioplanpromo.dnipromotor;
                            cmd.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = horarioplanpromo.idemppaisnegcue;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    HorarioPlanificadoPromotorResponse horarioapln = new HorarioPlanificadoPromotorResponse();
                                    horarioapln.idhorarioplanificado = reader.GetInt32(reader.GetOrdinal("idhorarioplanificado"));
                                    horarioapln.dnipromotor = reader.GetString(reader.GetOrdinal("dnipromotor"));
                                    horarioapln.idpdv = reader.GetInt32(reader.GetOrdinal("idpdv"));
                                    horarioapln.puntoventa = reader.GetString(reader.GetOrdinal("puntoventa"));
                                    horarioapln.fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("fecha")));
                                    horarioapln.horarioentrada = reader.GetString(reader.GetOrdinal("horarioentrada"));
                                    horarioapln.horariosalida = reader.GetString(reader.GetOrdinal("horariosalida"));
                                    horarioapln.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                    horarioapln.usuario_creacion = reader.GetString(reader.GetOrdinal("usuario_creacion"));
                                    horarioapln.activarcbo = reader.GetInt32(reader.GetOrdinal("activarcbo"));
                                    horarioapln.estado = reader.GetInt32(reader.GetOrdinal("estado"));

                                    response.Add(horarioapln);
                                }
                            }
                        }
                    }
                }

                return response;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }

 
        public async Task<List<ReportGetSemanaResponse>> ReportGetSemanaActual(HorarioPlanificadoRequest reporte)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("USP_REPORTGETSEMANA_ACTUAL", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@dnisupervisor", SqlDbType.VarChar).Value = reporte.usuario;
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<ReportGetSemanaResponse> response = new List<ReportGetSemanaResponse>();

                                while (await reader.ReadAsync())
                                {
                                    ReportGetSemanaResponse reportsemactl = new ReportGetSemanaResponse();
                                    reportsemactl.idhorarioplanificado = reader.GetInt32(reader.GetOrdinal("idhorarioplanificado"));
                                    reportsemactl.dnipromotor = reader.GetString(reader.GetOrdinal("dnipromotor"));
                                    reportsemactl.idpdv = reader.GetInt32(reader.GetOrdinal("idpdv"));
                                    reportsemactl.puntoventa = reader.GetString(reader.GetOrdinal("puntoventa"));
                                    reportsemactl.fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("fecha")));
                                    reportsemactl.horarioentrada = reader.GetString(reader.GetOrdinal("horarioentrada"));
                                    reportsemactl.horariosalida = reader.GetString(reader.GetOrdinal("horariosalida"));
                                    reportsemactl.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                    reportsemactl.dnisupervisor = reader.GetString(reader.GetOrdinal("usuario_creacion"));
                                    reportsemactl.estado = reader.GetInt32(reader.GetOrdinal("estado"));

                                    response.Add(reportsemactl);
                                }
                                return response;
                            }
                            else
                            {
                                return new List<ReportGetSemanaResponse>();
                            }
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


        public async Task<List<ReportGetSemanaResponse>> ReportGetSemanaAnterior(HorarioPlanificadoRequest reporte)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("USP_REPORTGETSEMANA_ANTERIOR", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@dnisupervisor", SqlDbType.VarChar).Value = reporte.usuario;
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<ReportGetSemanaResponse> response = new List<ReportGetSemanaResponse>();

                                while (await reader.ReadAsync())
                                {
                                    ReportGetSemanaResponse reportsemactl = new ReportGetSemanaResponse();
                                    reportsemactl.idhorarioplanificado = reader.GetInt32(reader.GetOrdinal("idhorarioplanificado"));
                                    reportsemactl.dnipromotor = reader.GetString(reader.GetOrdinal("dnipromotor"));
                                    reportsemactl.idpdv = reader.GetInt32(reader.GetOrdinal("idpdv"));
                                    reportsemactl.puntoventa = reader.GetString(reader.GetOrdinal("puntoventa"));
                                    reportsemactl.fecha = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("fecha")));
                                    reportsemactl.horarioentrada = reader.GetString(reader.GetOrdinal("horarioentrada"));
                                    reportsemactl.horariosalida = reader.GetString(reader.GetOrdinal("horariosalida"));
                                    reportsemactl.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                    reportsemactl.dnisupervisor = reader.GetString(reader.GetOrdinal("usuario_creacion"));
                                    reportsemactl.estado = reader.GetInt32(reader.GetOrdinal("estado"));

                                    response.Add(reportsemactl);
                                }
                                return response;
                            }
                            else
                            {
                                return new List<ReportGetSemanaResponse>();
                            }
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

        public async Task<List<JefesResponse>> GetJefes(int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETJEFES", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<JefesResponse> response = new List<JefesResponse>();

                                while (await reader.ReadAsync())
                                {
                                    JefesResponse jefesresponse = new JefesResponse();
                                    jefesresponse.dnijefe = reader.GetString(reader.GetOrdinal("dnijefe"));
                                    jefesresponse.nombrejefe = reader.GetString(reader.GetOrdinal("nombrejefe"));
                                    jefesresponse.apellidopaternojefe = reader.GetString(reader.GetOrdinal("apellidopaternojefe"));
                                    jefesresponse.apellidomaternojefe = reader.GetString(reader.GetOrdinal("apellidomaternojefe"));


                                    response.Add(jefesresponse);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<JefesResponse>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Por ejemplo, podrías registrar el error y devolver un mensaje de error adecuado
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }
        }

        public async Task<List<SupervisoresResponse>> GetSupervisores(string dnijefe, int idemppaisnegcue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETSUPERVISORES", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@dnijefe", SqlDbType.VarChar).Value = dnijefe;
                        command.Parameters.Add("@idemppaisnegcue", SqlDbType.Int).Value = idemppaisnegcue;


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<SupervisoresResponse> response = new List<SupervisoresResponse>();

                                while (await reader.ReadAsync())
                                {
                                    SupervisoresResponse supervisorresponse = new SupervisoresResponse();
                                    supervisorresponse.dnisupervisor = reader.GetString(reader.GetOrdinal("dnisupervisor"));
                                    supervisorresponse.nombresupervisor = reader.GetString(reader.GetOrdinal("nombresupervisor"));
                                    supervisorresponse.apellidopaternosupervisor = reader.GetString(reader.GetOrdinal("apellidopaternosupervisor"));
                                    supervisorresponse.apellidomaternosupervisor = reader.GetString(reader.GetOrdinal("apellidomaternosupervisor"));


                                    response.Add(supervisorresponse);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<SupervisoresResponse>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Por ejemplo, podrías registrar el error y devolver un mensaje de error adecuado
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }
        }
    }
}
