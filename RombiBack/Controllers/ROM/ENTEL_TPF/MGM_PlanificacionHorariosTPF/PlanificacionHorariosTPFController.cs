using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_PlanificacionHorariosTPF;

namespace RombiBack.Controllers.ROM.ENTEL_TPF.MGM_PlanificacionHorariosTPF
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanificacionHorariosTPFController : ControllerBase
    {
        private readonly IPlanificacionHorariosTPFServices _planificacionHorariosTPFServices;

        public PlanificacionHorariosTPFController(IPlanificacionHorariosTPFServices planificacionHorariosTPFServices)
        {
            _planificacionHorariosTPFServices = planificacionHorariosTPFServices;
        }

        [HttpPost("GetTurnosSupervisorTPF")]
        public async Task<IActionResult> GetTurnosSupervisorTPF([FromBody] TurnosSupervisor userdata)
        {

            var turnosSupervisors = await _planificacionHorariosTPFServices.GetTurnosSupervisorTPF(userdata.usuario, (int)userdata.idemppaisnegcue);
            return Ok(turnosSupervisors);
        }

        [HttpPost("PostTurnosSupervisorTPF")]
        public async Task<IActionResult> PostTurnosSupervisorTPF([FromBody] TurnosSupervisorRequest turnos)
        {
            var respuesta = await _planificacionHorariosTPFServices.PostTurnosSupervisorTPF(turnos);
            return Ok(respuesta);
        }

        [HttpPost("PutTurnosSupervisorTPF")]
        public async Task<IActionResult> PutTurnosSupervisorTPF([FromBody] TurnosSupervisor turnos)
        {
            var respuesta = await _planificacionHorariosTPFServices.PutTurnosSupervisorTPF(turnos);
            return Ok(respuesta);
        }

        [HttpPost("DeleteTurnosSupervisorTPF")]
        public async Task<IActionResult> DeleteTurnosSupervisorTPF([FromBody] TurnosSupervisor turnos)
        {
            var respuesta = await _planificacionHorariosTPFServices.DeleteTurnosSupervisorTPF(turnos);
            return Ok(respuesta);
        }


        [HttpPost("GetSupervisorPDVTPF")]
        public async Task<IActionResult> GetSupervisorPDVTPF([FromBody] JefeSuperRequest userdata)
        {

            var pdvsupervisor = await _planificacionHorariosTPFServices.GetSupervisorPDVTPF(userdata.dnisupervisor, (int)userdata.idemppaisnegcue);
            return Ok(pdvsupervisor);
        }

        [HttpPost("GetTurnosDisponiblePDVTPF")]
        public async Task<IActionResult> GetTurnosDisponiblePDVTPF([FromBody] TurnosDisponiblesPdvRequest turnosdispopdv)
        {
            var turnosdispo = await _planificacionHorariosTPFServices.GetTurnosDisponiblePDVTPF(turnosdispopdv);
            return Ok(turnosdispo);
        }


        [HttpPost("PostTurnosPDVTPF")]
        public async Task<IActionResult> PostTurnosPDVTPF([FromBody] List<TurnosPdvRequest> turnospdv)
        {
            var turnospdvres = await _planificacionHorariosTPFServices.PostTurnosPDVTPF(turnospdv);
            return Ok(turnospdvres);
        }

        [HttpPost("GetTurnosAsignadosPDVTPF")]
        public async Task<IActionResult> GetTurnosAsignadosPDVTPF([FromBody] TurnosDisponiblesPdvRequest turnosasig)
        {
            var turnosasignados = await _planificacionHorariosTPFServices.GetTurnosAsignadosPDVTPF(turnosasig);
            return Ok(turnosasignados);
        }

        [HttpPost("DeleteTurnosPDVTPF")]
        public async Task<IActionResult> DeleteTurnosPDVTPF([FromBody] TurnosPdvRequest turnospdv)
        {
            var turnospdvres = await _planificacionHorariosTPFServices.DeleteTurnosPDVTPF(turnospdv);
            return Ok(turnospdvres);
        }

        [HttpPost("GetRangoSemanaTPF")]
        public async Task<IActionResult> GetRangoSemanaTPF([FromBody] string perfil)
        {
            var obtener = await _planificacionHorariosTPFServices.GetRangoSemanaTPF(perfil);
            return Ok(obtener);
        }


        [HttpPost("GetPromotorSupervisorPDVTPF")]
        public async Task<IActionResult> GetPromotorSupervisorPDVTPF([FromBody] SupervisorPdvResponse promotorsuperpdv)
        {
            var planificacionHorariosSuper = await _planificacionHorariosTPFServices.GetPromotorSupervisorPDVTPF(promotorsuperpdv);
            return Ok(planificacionHorariosSuper);
        }

        [HttpPost("GetDiasSemanaTPF")]
        public async Task<IActionResult> GetDiasSemanaTPF([FromBody] FechasSemana fechassemana)
        {
            var obtener = await _planificacionHorariosTPFServices.GetDiasSemanaTPF(fechassemana);
            return Ok(obtener);
        }

        [HttpPost("GetTurnosSupervisorPDVHorariosTPF")]
        public async Task<IActionResult> GetTurnosSupervisorPDVHorariosTPF([FromBody] TurnosDisponiblesPdvRequest superpdv)
        {
            var obtener = await _planificacionHorariosTPFServices.GetTurnosSupervisorPDVHorariosTPF(superpdv);
            return Ok(obtener);
        }

        [HttpPost("PostHorarioPlanificadoTPF")]
        public async Task<IActionResult> PostHorarioPlanificadoTPF([FromBody] List<List<HorarioPlanificadoRequest>> horarioPlanificados)
        {
            try
            {
                List<HorarioPlanificadoRequest> listaHorarios = new List<HorarioPlanificadoRequest>();

                foreach (var promotores in horarioPlanificados)
                {
                    foreach (var horarioPlanificado in promotores)
                    {
                        listaHorarios.Add(horarioPlanificado);
                    }
                }
                Console.WriteLine(listaHorarios);
                // Ahora tienes todos los objetos HorarioPlanificadoRequest en listaHorarios
                // Puedes hacer cualquier cosa que necesites con esta lista
                var obtener = await _planificacionHorariosTPFServices.PostHorarioPlanificadoTPF(listaHorarios);
                return Ok(obtener);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar los datos: {ex.Message}");
            }
        }

        [HttpPost("ValidarHorarioPlanificadoTPF")]
        public async Task<IActionResult> ValidarHorariosPlanificadoTPF([FromBody] List<ListaHorarioPlanificadoRequest> horarioPlanificados)
        {
            if (horarioPlanificados == null || !horarioPlanificados.Any())
            {
                return BadRequest(new { Estado = "error", Mensaje = "Debe enviar al menos un horario para validar." });
            }

            var respuesta = await _planificacionHorariosTPFServices.ValidarHorariosPlanificadoTPF(horarioPlanificados);
            return Ok(respuesta);
        }

        [HttpPost("GetHorarioPlanificadoTPF")]
        public async Task<IActionResult> GetHorarioPlanificadoTPF([FromBody] List<HorarioPlanificadoPromotorRequest> horarioplanificadopromotor)
        {
            var turnospdvres = await _planificacionHorariosTPFServices.GetHorarioPlanificadoTPF(horarioplanificadopromotor);
            return Ok(turnospdvres);
        }

        [HttpPost("ReportGetSemanaActualTPF")]
        public async Task<IActionResult> ReportGetSemanaActualTPF([FromBody] HorarioPlanificadoRequest reporte)
        {
            var reportesres = await _planificacionHorariosTPFServices.ReportGetSemanaActualTPF(reporte);
            return Ok(reportesres);
        }

        [HttpPost("ReportGetSemanaAnteriorTPF")]
        public async Task<IActionResult> ReportGetSemanaAnteriorTPF([FromBody] HorarioPlanificadoRequest reporte)
        {
            var reportesres = await _planificacionHorariosTPFServices.ReportGetSemanaAnteriorTPF(reporte);
            return Ok(reportesres);
        }


        [HttpPost("GetJefesTPF")]
        public async Task<IActionResult> GetJefesTPF([FromBody] JefeSuperRequest jefesuperrequest)
        {
            var pdvsupervisor = await _planificacionHorariosTPFServices.GetJefesTPF((int)jefesuperrequest.idemppaisnegcue);
            return Ok(pdvsupervisor);
        }


        [HttpPost("GetSupervisores")]
        public async Task<IActionResult> GetSupervisoresTPF([FromBody] JefeSuperRequest jefesuperrequest)
        {
            var pdvsupervisor = await _planificacionHorariosTPFServices.GetSupervisoresTPF(jefesuperrequest.dnijefe, (int)jefesuperrequest.idemppaisnegcue);
            return Ok(pdvsupervisor);
        }
    }
}
