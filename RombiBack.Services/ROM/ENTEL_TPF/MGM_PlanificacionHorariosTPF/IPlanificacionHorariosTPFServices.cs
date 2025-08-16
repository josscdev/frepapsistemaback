using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_PlanificacionHorariosTPF
{
    public interface IPlanificacionHorariosTPFServices
    {
        Task<List<TurnosSupervisor>> GetTurnosSupervisorTPF(string usuario, int idemppaisnegcue);
        Task<Respuesta> PostTurnosSupervisorTPF(TurnosSupervisorRequest turnossuper);
        Task<Respuesta> PutTurnosSupervisorTPF(TurnosSupervisor turnossuper);
        Task<Respuesta> DeleteTurnosSupervisorTPF(TurnosSupervisor turnossuper);
        Task<List<SupervisorPdvResponse>> GetSupervisorPDVTPF(string usuario, int idemppaisnegcue);
        Task<List<TurnosSupervisor>> GetTurnosDisponiblePDVTPF(TurnosDisponiblesPdvRequest turnodispo);
        Task<Respuesta> PostTurnosPDVTPF(List<TurnosPdvRequest> turnosPdvList);
        Task<List<TurnosSupervisor>> GetTurnosAsignadosPDVTPF(TurnosDisponiblesPdvRequest turnodispo);
        Task<Respuesta> DeleteTurnosPDVTPF(TurnosPdvRequest turnospdv);
        Task<List<FechasSemana>> GetRangoSemanaTPF(string perfil);
        Task<List<PromotorSupervisorPdvResponse>> GetPromotorSupervisorPDVTPF(SupervisorPdvResponse promotorsuperpdv);
        //Task<FechasSemana> GetDiasSemana(FechasSemana fechassemana);
        Task<List<DiasSemana>> GetDiasSemanaTPF(FechasSemana fechassemana);
        Task<List<TurnosSupervisorPdvHorariosResponse>> GetTurnosSupervisorPDVHorariosTPF(TurnosDisponiblesPdvRequest superpdv);
        Task<Respuesta> PostHorarioPlanificadoTPF(List<HorarioPlanificadoRequest> horarioPlanificados);
        Task<List<RespuestaValidacion>> ValidarHorariosPlanificadoTPF(List<ListaHorarioPlanificadoRequest> horarioPlanificados);
        Task<List<HorarioPlanificadoPromotorResponse>> GetHorarioPlanificadoTPF(List<HorarioPlanificadoPromotorRequest> horarioPlanificadopromotor);
        Task<List<ReportGetSemanaResponse>> ReportGetSemanaActualTPF(HorarioPlanificadoRequest reporte);
        Task<List<ReportGetSemanaResponse>> ReportGetSemanaAnteriorTPF(HorarioPlanificadoRequest reporte);

        Task<List<JefesResponse>> GetJefesTPF(int idemppaisnegcue);

        Task<List<SupervisoresResponse>> GetSupervisoresTPF(string dnijefe, int idemppaisnegcue);
    }
}
