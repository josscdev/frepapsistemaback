using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Security.Model.UserAuth;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Reports;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanificacionHorariosController : ControllerBase
    {
        private readonly IPlanificaionHorariosServices _planificacionHorariosServices;

        public PlanificacionHorariosController(IPlanificaionHorariosServices planificacionHorariosServices)
        {
            _planificacionHorariosServices = planificacionHorariosServices;
        }

        //[HttpPost("GetTurnosSupervisor")]
        //public async Task<IActionResult> GetTurnosSupervisor([FromBody] TurnosSupervisor userdata)
        //{

        //    var turnosSupervisors = await _planificacionHorariosServices.GetTurnosSupervisor(userdata.usuario, (int)userdata.idemppaisnegcue);
        //    return Ok(turnosSupervisors);
        //}

        //[HttpPost("PostTurnosSupervisor")]
        //public async Task<IActionResult> PostTurnosSupervisor([FromBody] TurnosSupervisorRequest turnos)
        //{
        //    var respuesta= await _planificacionHorariosServices.PostTurnosSupervisor(turnos);
        //    return Ok(respuesta);
        //}

        //[HttpPost("PutTurnosSupervisor")]
        //public async Task<IActionResult> PutTurnosSupervisor([FromBody] TurnosSupervisor turnos)
        //{
        //    var respuesta = await _planificacionHorariosServices.PutTurnosSupervisor(turnos);
        //    return Ok(respuesta);
        //}

        //[HttpPost("DeleteTurnosSupervisor")]
        //public async Task<IActionResult> DeleteTurnosSupervisor([FromBody] TurnosSupervisor turnos)
        //{
        //    var respuesta = await _planificacionHorariosServices.DeleteTurnosSupervisor(turnos);
        //    return Ok(respuesta);
        //}


        //[HttpPost("GetSupervisorPDV")]
        //public async Task<IActionResult> GetSupervisorPDV([FromBody] JefeSuperRequest userdata)
        //{

        //    var pdvsupervisor = await _planificacionHorariosServices.GetSupervisorPDV(userdata.dnisupervisor, (int)userdata.idemppaisnegcue);
        //    return Ok(pdvsupervisor);
        //}

        //[HttpPost("GetTurnosDisponiblePDV")]
        //public async Task<IActionResult> GetTurnosDisponiblePDV([FromBody] TurnosDisponiblesPdvRequest turnosdispopdv)
        //{
        //    var turnosdispo = await _planificacionHorariosServices.GetTurnosDisponiblePDV(turnosdispopdv);
        //    return Ok(turnosdispo);
        //}


        //[HttpPost("PostTurnosPDV")]
        //public async Task<IActionResult> PostTurnosPDV([FromBody] List<TurnosPdvRequest> turnospdv)
        //{
        //    var turnospdvres = await _planificacionHorariosServices.PostTurnosPDV(turnospdv);
        //    return Ok(turnospdvres);
        //}

        //[HttpPost("GetTurnosAsignadosPDV")]
        //public async Task<IActionResult> GetTurnosAsignadosPDV([FromBody] TurnosDisponiblesPdvRequest turnosasig)
        //{
        //    var turnosasignados = await _planificacionHorariosServices.GetTurnosAsignadosPDV(turnosasig);
        //    return Ok(turnosasignados);
        //}

        //[HttpPost("DeleteTurnosPDV")]
        //public async Task<IActionResult> DeleteTurnosPDV([FromBody] TurnosPdvRequest turnospdv)
        //{
        //    var turnospdvres = await _planificacionHorariosServices.DeleteTurnosPDV(turnospdv);
        //    return Ok(turnospdvres);
        //}

        //[HttpPost("GetRangoSemana")]
        //public async Task<IActionResult> GetRangoSemana([FromBody] string perfil)
        //{
        //    var obtener = await _planificacionHorariosServices.GetRangoSemana(perfil);
        //    return Ok(obtener);
        //}


        //[HttpPost("GetPromotorSupervisorPDV")]
        //public async Task<IActionResult> GetPromotorSupervisorPDV([FromBody] SupervisorPdvResponse promotorsuperpdv)
        //{
        //    var planificacionHorariosSuper = await _planificacionHorariosServices.GetPromotorSupervisorPDV(promotorsuperpdv);
        //    return Ok(planificacionHorariosSuper);
        //}

        //[HttpPost("GetDiasSemana")]
        //public async Task<IActionResult> GetDiasSemana([FromBody] FechasSemana fechassemana)
        //{
        //    var obtener = await _planificacionHorariosServices.GetDiasSemana(fechassemana);
        //    return Ok(obtener);
        //}

        //[HttpPost("GetTurnosSupervisorPDVHorarios")]
        //public async Task<IActionResult> GetTurnosSupervisorPDVHorarios([FromBody] TurnosDisponiblesPdvRequest superpdv)
        //{
        //    var obtener = await _planificacionHorariosServices.GetTurnosSupervisorPDVHorarios(superpdv);
        //    return Ok(obtener);
        //}

        //[HttpPost("PostHorarioPlanificado")]
        //public async Task<IActionResult> PostHorarioPlanificado([FromBody] List<List<HorarioPlanificadoRequest>> horarioPlanificados)
        //{
        //    try
        //    {
        //        List<HorarioPlanificadoRequest> listaHorarios = new List<HorarioPlanificadoRequest>();

        //        foreach (var promotores in horarioPlanificados)
        //        {
        //            foreach (var horarioPlanificado in promotores)
        //            {
        //                listaHorarios.Add(horarioPlanificado);
        //            }
        //        }
        //        Console.WriteLine(listaHorarios);
        //        // Ahora tienes todos los objetos HorarioPlanificadoRequest en listaHorarios
        //        // Puedes hacer cualquier cosa que necesites con esta lista
        //        var obtener = await _planificacionHorariosServices.PostHorarioPlanificado(listaHorarios);
        //        return Ok(obtener);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al procesar los datos: {ex.Message}");
        //    }
        //}

        //[HttpPost("ValidarHorarioPlanificado")]
        //public async Task<IActionResult> ValidarHorariosPlanificado([FromBody] List<ListaHorarioPlanificadoRequest> horarioPlanificados)
        //{
        //    if (horarioPlanificados == null || !horarioPlanificados.Any())
        //    {
        //        return BadRequest(new { Estado = "error", Mensaje = "Debe enviar al menos un horario para validar." });
        //    }

        //    var respuesta = await _planificacionHorariosServices.ValidarHorariosPlanificado(horarioPlanificados);
        //    return Ok(respuesta);
        //}

        //[HttpPost("GetHorarioPlanificado")]
        //public async Task<IActionResult> GetHorarioPlanificado([FromBody] List<HorarioPlanificadoPromotorRequest> horarioplanificadopromotor)
        //{
        //    var turnospdvres = await _planificacionHorariosServices.GetHorarioPlanificado(horarioplanificadopromotor);
        //    return Ok(turnospdvres);
        //}

        //[HttpPost("ReportGetSemanaActual")]
        //public async Task<IActionResult> ReportGetSemanaActual([FromBody] HorarioPlanificadoRequest reporte)
        //{
        //    var reportesres = await _planificacionHorariosServices.ReportGetSemanaActual(reporte);
        //    return Ok(reportesres);
        //}

        //[HttpPost("ReportGetSemanaAnterior")]
        //public async Task<IActionResult> ReportGetSemanaAnterior([FromBody] HorarioPlanificadoRequest reporte)
        //{
        //    var reportesres = await _planificacionHorariosServices.ReportGetSemanaAnterior(reporte);
        //    return Ok(reportesres);
        //}


        //[HttpPost("GetJefes")]
        //public async Task<IActionResult> GetJefes([FromBody] JefeSuperRequest jefesuperrequest)
        //{
        //    var pdvsupervisor = await _planificacionHorariosServices.GetJefes((int)jefesuperrequest.idemppaisnegcue);
        //    return Ok(pdvsupervisor);
        //}


        //[HttpPost("GetSupervisores")]
        //public async Task<IActionResult> GetSupervisores([FromBody] JefeSuperRequest jefesuperrequest)
        //{
        //    var pdvsupervisor = await _planificacionHorariosServices.GetSupervisores(jefesuperrequest.dnijefe, (int)jefesuperrequest.idemppaisnegcue);
        //    return Ok(pdvsupervisor);
        //}
    }
}
