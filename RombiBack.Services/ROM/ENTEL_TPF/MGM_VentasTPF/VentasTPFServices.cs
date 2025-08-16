using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Ventas;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_Allocation;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_VentasTPF;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_Allocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_VentasTPF
{
    public class VentasTPFServices : IVentasTPFServices
    {
        private readonly IVentasTPFRepository _ventasTPFRepository;


        public VentasTPFServices(IVentasTPFRepository ventasTPFRepository, IMapper mapper)
        {
            _ventasTPFRepository = ventasTPFRepository;
        }
        
        public async Task<List<TipoDocumentoResponse>> GetTipoDocumentoTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetTipoDocumentoTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<TipoBiometriaResponse>> GetTipoBiometriaTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetTipoBiometriaTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<SubproductoResponse>> GetSubproductoTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetSubproductoTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<OperadorReponse>> GetOperadorTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetOperadorTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<TipoEquipoResponse>> GetTipoEquipoTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetTipoEquipoTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<TipoSeguroResponse>> GetTipoSeguroTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetTipoSeguroTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<TipoEtiquetaResponse>> GetTipoEtiquetaTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetTipoEtiquetaTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<TipoPagoResponse>> GetTipoPagoTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetTipoPagoTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<PlanesResponse>> GetPlanesTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetPlanesTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<ModeloResponse>> GetModeloTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetModeloTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<BundleResponse>> GetBundleTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetBundleTPF(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<TipoAccesorioResponse>> GetTipoAccesorioTPF(int idemppaisnegcue)
        {
            var respuesta = await _ventasTPFRepository.GetTipoAccesorioTPF(idemppaisnegcue);
            return respuesta;
        }    
        public async Task<List<VentasDetalleTotal>> GetVentasAdminTPF(FiltrarVentasPerfiles filtros)
        {
            var respuesta = await _ventasTPFRepository.GetVentasAdminTPF(filtros);
            return respuesta;
        }
        public async Task<List<VentasDetalleTotal>> GetVentasJefeTPF(FiltrarVentasPerfiles filtros)
        {
            var respuesta = await _ventasTPFRepository.GetVentasJefeTPF(filtros);
            return respuesta;
        }
        public async Task<List<VentasDetalleTotal>> GetVentasSuperTPF(FiltrarVentasPerfiles filtros)
        {
            var respuesta = await _ventasTPFRepository.GetVentasSuperTPF(filtros);
            return respuesta;
        }
        public async Task<List<VentasDetalleTotal>> GetVentasPromotorTPF(FiltrarVentasPerfiles filtros)
        {
            var respuesta = await _ventasTPFRepository.GetVentasPromotorTPF(filtros);
            return respuesta;
        }
        public async Task<VentasResult> PostVentasTPF(Ventas ventas)
        {
            return await _ventasTPFRepository.PostVentasTPF(ventas);
        }
        public async Task<ActualizarNombreVoucherResponse> UpdateNombreVoucherTPF(ActualizarNombreVoucherRequest request)
        {
            return await _ventasTPFRepository.UpdateNombreVoucherTPF(request);
        }
        public async Task<Respuesta> DeleteVentasDetalleTPF(int idventasdetalle, string usuarioanulacion)
        {
            return await _ventasTPFRepository.DeleteVentasDetalleTPF(idventasdetalle, usuarioanulacion);
        }
        public async Task<VentasResult> UpdateVentasDetalleTPF(VentasDetalle request)
        {
            return await _ventasTPFRepository.UpdateVentasDetalleTPF(request);
        }
        public async Task<VentasResult> UpdateVoucherVentasTPF(Ventas request)
        {
            return await _ventasTPFRepository.UpdateVoucherVentasTPF(request);
        }


    }
}
