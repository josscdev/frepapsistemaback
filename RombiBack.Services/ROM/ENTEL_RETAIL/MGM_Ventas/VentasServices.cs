using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Ventas;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Ventas
{
    public class VentasServices: IVentasServices
    {
        private readonly IVentasRepository _ventasRepository;
        private readonly IMapper _mapper;

        public VentasServices(IVentasRepository ventasRepository, IMapper mapper)
        {
            _ventasRepository = ventasRepository;
            _mapper = mapper;
        }
        public async Task<List<TipoDocumentoResponse>> GetTipoDocumento(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetTipoDocumento(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<TipoBiometriaResponse>> GetTipoBiometria(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetTipoBiometria(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<SubproductoResponse>> GetSubproducto(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetSubproducto(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<OperadorReponse>> GetOperador(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetOperador(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<TipoEquipoResponse>> GetTipoEquipo(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetTipoEquipo(idemppaisnegcue);
            return respuesta;
        }

        public async Task<List<TipoSeguroResponse>> GetTipoSeguro(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetTipoSeguro(idemppaisnegcue);
            return respuesta;
        }

        public async Task<List<TipoEtiquetaResponse>> GetTipoEtiqueta(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetTipoEtiqueta(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<TipoPagoResponse>> GetTipoPago(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetTipoPago(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<PlanesResponse>> GetPlanes(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetPlanes(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<ModeloResponse>> GetModelo(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetModelo(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<BundleResponse>> GetBundle(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetBundle(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<TipoAccesorioResponse>> GetTipoAccesorio(int idemppaisnegcue)
        {
            var respuesta = await _ventasRepository.GetTipoAccesorio(idemppaisnegcue);
            return respuesta;
        }
        public async Task<List<IMEIequiposResponse>> GetIMEISequipos(int idusuario)
        {
            var respuesta = await _ventasRepository.GetIMEISequipos(idusuario);
            return respuesta;
        }

        public async Task<List<VentasDetalleTotal>> GetVentasAdmin(FiltrarVentasPerfiles filtros)
        {
            var respuesta = await _ventasRepository.GetVentasAdmin(filtros);
            return respuesta;
        }

        public async Task<List<VentasDetalleTotal>> GetVentasJefe(FiltrarVentasPerfiles filtros)
        {
            var respuesta = await _ventasRepository.GetVentasJefe(filtros);
            return respuesta;
        }

        public async Task<List<VentasDetalleTotal>> GetVentasSuper(FiltrarVentasPerfiles filtros)
        {
            var respuesta = await _ventasRepository.GetVentasSuper(filtros);
            return respuesta;
        }

        public async Task<List<VentasDetalleTotal>> GetVentasPromotor(FiltrarVentasPerfiles filtros)
        {
            var respuesta = await _ventasRepository.GetVentasPromotor(filtros);
            return respuesta;
        }

        //public async Task<List<VentaDetalle>> GetVentasJefe(FiltrarVentasPerfiles filtros)
        //{
        //    var respuesta = await _ventasRepository.GetVentasAdmin(filtros);
        //    return respuesta;
        //}
        //public async Task<List<VentaDetalle>> GetVentasSuper(FiltrarVentasPerfiles filtros)
        //{
        //    var respuesta = await _ventasRepository.GetVentasAdmin(filtros);
        //    return respuesta;
        //}

        //public async Task<List<VentaDetalle>> GetVentasPromotor(FiltrarVentasPerfiles filtros)
        //{
        //    var respuesta = await _ventasRepository.GetVentasAdmin(filtros);
        //    return respuesta;
        //}

        public async Task<VentasResult> PostVentas(Ventas ventas)
        {
            return await _ventasRepository.PostVentas(ventas);
        }

        public async Task<ActualizarNombreVoucherResponse> UpdateNombreVoucherRetail(ActualizarNombreVoucherRequest request)
        {
            return await _ventasRepository.UpdateNombreVoucherRetail(request);
        }

        public async Task<Respuesta> DeleteVentasDetalle(int idventasdetalle, string usuarioanulacion)
        {
            return await _ventasRepository.DeleteVentasDetalle(idventasdetalle,usuarioanulacion);
        }

        public async Task<VentasResult> UpdateVentasDetalle(VentasDetalle request)
        {
            return await _ventasRepository.UpdateVentasDetalle(request);
        }

        public async Task<VentasResult> UpdateVoucherVentas(Ventas request)
        {
            return await _ventasRepository.UpdateVoucherVentas(request);
        }
    }
}
