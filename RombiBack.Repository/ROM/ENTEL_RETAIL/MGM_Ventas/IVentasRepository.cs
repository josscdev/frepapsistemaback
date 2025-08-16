using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Ventas
{
    public interface IVentasRepository
    {
        Task<List<TipoDocumentoResponse>> GetTipoDocumento(int idemppaisnegcue);
        Task<List<TipoBiometriaResponse>> GetTipoBiometria(int idemppaisnegcue);
        Task<List<SubproductoResponse>> GetSubproducto(int idemppaisnegcue);
        Task<List<OperadorReponse>> GetOperador(int idemppaisnegcue);
        Task<List<TipoEquipoResponse>> GetTipoEquipo(int idemppaisnegcue);
        Task<List<TipoSeguroResponse>> GetTipoSeguro(int idemppaisnegcue);

        Task<List<TipoEtiquetaResponse>> GetTipoEtiqueta(int idemppaisnegcue);
        Task<List<TipoPagoResponse>> GetTipoPago(int idemppaisnegcue);
        Task<List<PlanesResponse>> GetPlanes(int idemppaisnegcue);
        Task<List<ModeloResponse>> GetModelo(int idemppaisnegcue);
        Task<List<BundleResponse>> GetBundle(int idemppaisnegcue);
        Task<List<TipoAccesorioResponse>> GetTipoAccesorio(int idemppaisnegcue);
        Task<List<IMEIequiposResponse>> GetIMEISequipos(int idusuario);
        Task<List<VentasDetalleTotal>> GetVentasAdmin(FiltrarVentasPerfiles filtros);
        Task<List<VentasDetalleTotal>> GetVentasJefe(FiltrarVentasPerfiles filtros);
        Task<List<VentasDetalleTotal>> GetVentasSuper(FiltrarVentasPerfiles filtros);
        Task<List<VentasDetalleTotal>> GetVentasPromotor(FiltrarVentasPerfiles filtros);
        Task <Respuesta> DeleteVentasDetalle(int idventasdetalle ,string usuarioanulacion);

        Task<VentasResult> PostVentas(Ventas ventas);
        Task<ActualizarNombreVoucherResponse> UpdateNombreVoucherRetail(ActualizarNombreVoucherRequest request);
        Task<VentasResult> UpdateVentasDetalle(VentasDetalle request);
        Task<VentasResult> UpdateVoucherVentas(Ventas venta);
    }
}
