using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios
{
    public interface IPlanificaionHorariosServices
    {
        Task<List<TurnosSupervisor>> GetTurnosSupervisor(string usuario, int idemppaisnegcue);
        Task<Respuesta> PostTurnosSupervisor(TurnosSupervisorRequest turnossuper);
        Task<Respuesta> PutTurnosSupervisor(TurnosSupervisor turnossuper);
        Task<Respuesta> DeleteTurnosSupervisor(TurnosSupervisor turnossuper);
        Task<List<SupervisorPdvResponse>> GetSupervisorPDV(string usuario, int idemppaisnegcue);
        Task<List<TurnosSupervisor>> GetTurnosDisponiblePDV(TurnosDisponiblesPdvRequest turnodispo);
        Task<Respuesta> PostTurnosPDV(List<TurnosPdvRequest> turnosPdvList);
        Task<List<TurnosSupervisor>> GetTurnosAsignadosPDV(TurnosDisponiblesPdvRequest turnodispo);
        Task<Respuesta> DeleteTurnosPDV(TurnosPdvRequest turnospdv);
        Task<List<FechasSemana>> GetRangoSemana(string perfil);
        Task<List<PromotorSupervisorPdvResponse>> GetPromotorSupervisorPDV(SupervisorPdvResponse promotorsuperpdv);
        //Task<FechasSemana> GetDiasSemana(FechasSemana fechassemana);
        Task<List<DiasSemana>> GetDiasSemana(FechasSemana fechassemana);
        Task<List<TurnosSupervisorPdvHorariosResponse>> GetTurnosSupervisorPDVHorarios(TurnosDisponiblesPdvRequest superpdv);
        Task<Respuesta> PostHorarioPlanificado(List<HorarioPlanificadoRequest> horarioPlanificados);
        Task<List<RespuestaValidacion>> ValidarHorariosPlanificado(List<ListaHorarioPlanificadoRequest> horarioPlanificados);
        Task<List<HorarioPlanificadoPromotorResponse>> GetHorarioPlanificado(List<HorarioPlanificadoPromotorRequest> horarioPlanificadopromotor);
        Task<List<ReportGetSemanaResponse>> ReportGetSemanaActual(HorarioPlanificadoRequest reporte);
        Task<List<ReportGetSemanaResponse>> ReportGetSemanaAnterior(HorarioPlanificadoRequest reporte);

        Task<List<JefesResponse>> GetJefes(int idemppaisnegcue);

        Task<List<SupervisoresResponse>> GetSupervisores(string dnijefe, int idemppaisnegcue);

    }
}
