using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Perfiles;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios
{
    public class PlanificacionHorariosServices : IPlanificaionHorariosServices
    {
        private readonly IPlanificacionHorariosRepository _planificacionHorariosRepository;

        private readonly IMapper _mapper;

        public PlanificacionHorariosServices(IPlanificacionHorariosRepository planificacionHorariosRepository, IMapper mapper)
        {
            _planificacionHorariosRepository = planificacionHorariosRepository;
            _mapper = mapper;
        }

     
        public async Task<List<TurnosSupervisor>> GetTurnosSupervisor(string usuario, int idemppaisnegcue)
        {
            var turnosuper= await _planificacionHorariosRepository.GetTurnosSupervisor(usuario, idemppaisnegcue);
            return turnosuper;

        }

        public async Task<Respuesta> PostTurnosSupervisor(TurnosSupervisorRequest turnossuper)
        {
             var respuesta=await _planificacionHorariosRepository.PostTurnosSupervisor(turnossuper);
           return respuesta;
        }


        public async Task<Respuesta> PutTurnosSupervisor(TurnosSupervisor turnossuper)
        {
            var respuesta = await _planificacionHorariosRepository.PutTurnosSupervisor(turnossuper);
            return respuesta;
        }

        public async Task<Respuesta> DeleteTurnosSupervisor(TurnosSupervisor turnossuper)
        {
            var respuesta = await _planificacionHorariosRepository.DeleteTurnosSupervisor(turnossuper);
            return respuesta;
        }


        public async Task<List<SupervisorPdvResponse>> GetSupervisorPDV(string usuario, int idemppaisnegcue)
        {
            var respuesta = await _planificacionHorariosRepository.GetSupervisorPDV(usuario, idemppaisnegcue);
            return respuesta;
        }

        public async Task<List<TurnosSupervisor>> GetTurnosDisponiblePDV(TurnosDisponiblesPdvRequest turnodispo)
        {
            var respuesta = await _planificacionHorariosRepository.GetTurnosDisponiblePDV(turnodispo);
            return respuesta;
        }
        public async Task<Respuesta> PostTurnosPDV(List<TurnosPdvRequest> turnosPdvList)
        {
            var respuesta = await _planificacionHorariosRepository.PostTurnosPDV(turnosPdvList);
            return respuesta;
        }
        public async Task<List<TurnosSupervisor>> GetTurnosAsignadosPDV(TurnosDisponiblesPdvRequest turnodispo)
        {
            var respuesta = await _planificacionHorariosRepository.GetTurnosAsignadosPDV(turnodispo);
            return respuesta;
        }

        public async Task<Respuesta> DeleteTurnosPDV(TurnosPdvRequest turnospdv)
        {
            var respuesta = await _planificacionHorariosRepository.DeleteTurnosPDV(turnospdv);
            return respuesta;
        }

        public async Task<List<FechasSemana>> GetRangoSemana(string perfil)
        {
            var respuesta = await _planificacionHorariosRepository.GetRangoSemana(perfil);
            return respuesta;
        }

        public async Task<List<PromotorSupervisorPdvResponse>> GetPromotorSupervisorPDV(SupervisorPdvResponse promotorsuperpdv)
        {
            var respuesta = await _planificacionHorariosRepository.GetPromotorSupervisorPDV(promotorsuperpdv);
            return respuesta;
        }

        public async Task<List<DiasSemana>> GetDiasSemana(FechasSemana fechassemana)
        {
            var respuesta = await _planificacionHorariosRepository.GetDiasSemana(fechassemana);
            return respuesta;
        }

        public async Task<List<TurnosSupervisorPdvHorariosResponse>> GetTurnosSupervisorPDVHorarios(TurnosDisponiblesPdvRequest superpdv)
        {
            var respuesta = await _planificacionHorariosRepository.GetTurnosSupervisorPDVHorarios(superpdv);
            return respuesta;
        }

        public async Task<Respuesta> PostHorarioPlanificado(List<HorarioPlanificadoRequest> horarioPlanificados)
        {
            var respuesta = await _planificacionHorariosRepository.PostHorarioPlanificado(horarioPlanificados);
            return respuesta;
        }

        public async Task<List<RespuestaValidacion>> ValidarHorariosPlanificado(List<ListaHorarioPlanificadoRequest> horarioPlanificados)
        {
            return await _planificacionHorariosRepository.ValidarHorariosPlanificado(horarioPlanificados);
        }


        public async Task<List<HorarioPlanificadoPromotorResponse>> GetHorarioPlanificado(List<HorarioPlanificadoPromotorRequest> horarioPlanificadopromotor)
        {
            var respuesta = await _planificacionHorariosRepository.GetHorarioPlanificado(horarioPlanificadopromotor);
            return respuesta;
        }

        public async Task<List<ReportGetSemanaResponse>> ReportGetSemanaActual(HorarioPlanificadoRequest reporte)
        {
            var respuesta = await _planificacionHorariosRepository.ReportGetSemanaActual(reporte);
            return respuesta;
        }
        public async Task<List<ReportGetSemanaResponse>> ReportGetSemanaAnterior(HorarioPlanificadoRequest reporte)
        {
            var respuesta = await _planificacionHorariosRepository.ReportGetSemanaAnterior(reporte);
            return respuesta;
        }

        public async Task<List<JefesResponse>> GetJefes(int idemppaisnegcue)
        {
            var respuesta = await _planificacionHorariosRepository.GetJefes(idemppaisnegcue);
            return respuesta;
        }

        public async Task<List<SupervisoresResponse>> GetSupervisores(string dnijefe, int idemppaisnegcue)
        {
            var respuesta = await _planificacionHorariosRepository.GetSupervisores(dnijefe, idemppaisnegcue);
            return respuesta;
        }
    }
}
