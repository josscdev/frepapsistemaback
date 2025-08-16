using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_PlanificacionHorarios;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_PlanificacionHorariosTPF
{
    public class PlanificacionHorariosTPFServices : IPlanificacionHorariosTPFServices
    {
        private readonly IPlanificacionHorariosTPFRepository _planificacionHorariosTPFRepository;

        private readonly IMapper _mapper;

        public PlanificacionHorariosTPFServices(IPlanificacionHorariosTPFRepository planificacionHorariosTPFRepository, IMapper mapper)
        {
            _planificacionHorariosTPFRepository = planificacionHorariosTPFRepository;
            _mapper = mapper;
        }


        public async Task<List<TurnosSupervisor>> GetTurnosSupervisorTPF(string usuario, int idemppaisnegcue)
        {
            var turnosuper = await _planificacionHorariosTPFRepository.GetTurnosSupervisorTPF(usuario, idemppaisnegcue);
            return turnosuper;

        }

        public async Task<Respuesta> PostTurnosSupervisorTPF(TurnosSupervisorRequest turnossuper)
        {
            var respuesta = await _planificacionHorariosTPFRepository.PostTurnosSupervisorTPF(turnossuper);
            return respuesta;
        }


        public async Task<Respuesta> PutTurnosSupervisorTPF(TurnosSupervisor turnossuper)
        {
            var respuesta = await _planificacionHorariosTPFRepository.PutTurnosSupervisorTPF(turnossuper);
            return respuesta;
        }

        public async Task<Respuesta> DeleteTurnosSupervisorTPF(TurnosSupervisor turnossuper)
        {
            var respuesta = await _planificacionHorariosTPFRepository.DeleteTurnosSupervisorTPF(turnossuper);
            return respuesta;
        }


        public async Task<List<SupervisorPdvResponse>> GetSupervisorPDVTPF(string usuario, int idemppaisnegcue)
        {
            var respuesta = await _planificacionHorariosTPFRepository.GetSupervisorPDVTPF(usuario, idemppaisnegcue);
            return respuesta;
        }

        public async Task<List<TurnosSupervisor>> GetTurnosDisponiblePDVTPF(TurnosDisponiblesPdvRequest turnodispo)
        {
            var respuesta = await _planificacionHorariosTPFRepository.GetTurnosDisponiblePDVTPF(turnodispo);
            return respuesta;
        }
        public async Task<Respuesta> PostTurnosPDVTPF(List<TurnosPdvRequest> turnosPdvList)
        {
            var respuesta = await _planificacionHorariosTPFRepository.PostTurnosPDVTPF(turnosPdvList);
            return respuesta;
        }
        public async Task<List<TurnosSupervisor>> GetTurnosAsignadosPDVTPF(TurnosDisponiblesPdvRequest turnodispo)
        {
            var respuesta = await _planificacionHorariosTPFRepository.GetTurnosAsignadosPDVTPF(turnodispo);
            return respuesta;
        }

        public async Task<Respuesta> DeleteTurnosPDVTPF(TurnosPdvRequest turnospdv)
        {
            var respuesta = await _planificacionHorariosTPFRepository.DeleteTurnosPDVTPF(turnospdv);
            return respuesta;
        }

        public async Task<List<FechasSemana>> GetRangoSemanaTPF(string perfil)
        {
            var respuesta = await _planificacionHorariosTPFRepository.GetRangoSemanaTPF(perfil);
            return respuesta;
        }

        public async Task<List<PromotorSupervisorPdvResponse>> GetPromotorSupervisorPDVTPF(SupervisorPdvResponse promotorsuperpdv)
        {
            var respuesta = await _planificacionHorariosTPFRepository.GetPromotorSupervisorPDVTPF(promotorsuperpdv);
            return respuesta;
        }

        public async Task<List<DiasSemana>> GetDiasSemanaTPF(FechasSemana fechassemana)
        {
            var respuesta = await _planificacionHorariosTPFRepository.GetDiasSemanaTPF(fechassemana);
            return respuesta;
        }

        public async Task<List<TurnosSupervisorPdvHorariosResponse>> GetTurnosSupervisorPDVHorariosTPF(TurnosDisponiblesPdvRequest superpdv)
        {
            var respuesta = await _planificacionHorariosTPFRepository.GetTurnosSupervisorPDVHorariosTPF(superpdv);
            return respuesta;
        }

        public async Task<Respuesta> PostHorarioPlanificadoTPF(List<HorarioPlanificadoRequest> horarioPlanificados)
        {
            var respuesta = await _planificacionHorariosTPFRepository.PostHorarioPlanificadoTPF(horarioPlanificados);
            return respuesta;
        }

        public async Task<List<RespuestaValidacion>> ValidarHorariosPlanificadoTPF(List<ListaHorarioPlanificadoRequest> horarioPlanificados)
        {
            return await _planificacionHorariosTPFRepository.ValidarHorariosPlanificadoTPF(horarioPlanificados);
        }


        public async Task<List<HorarioPlanificadoPromotorResponse>> GetHorarioPlanificadoTPF(List<HorarioPlanificadoPromotorRequest> horarioPlanificadopromotor)
        {
            var respuesta = await _planificacionHorariosTPFRepository.GetHorarioPlanificadoTPF(horarioPlanificadopromotor);
            return respuesta;
        }

        public async Task<List<ReportGetSemanaResponse>> ReportGetSemanaActualTPF(HorarioPlanificadoRequest reporte)
        {
            var respuesta = await _planificacionHorariosTPFRepository.ReportGetSemanaActualTPF(reporte);
            return respuesta;
        }
        public async Task<List<ReportGetSemanaResponse>> ReportGetSemanaAnteriorTPF(HorarioPlanificadoRequest reporte)
        {
            var respuesta = await _planificacionHorariosTPFRepository.ReportGetSemanaAnteriorTPF(reporte);
            return respuesta;
        }

        public async Task<List<JefesResponse>> GetJefesTPF(int idemppaisnegcue)
        {
            var respuesta = await _planificacionHorariosTPFRepository.GetJefesTPF(idemppaisnegcue);
            return respuesta;
        }

        public async Task<List<SupervisoresResponse>> GetSupervisoresTPF(string dnijefe, int idemppaisnegcue)
        {
            var respuesta = await _planificacionHorariosTPFRepository.GetSupervisoresTPF(dnijefe, idemppaisnegcue);
            return respuesta;
        }
    }
}
