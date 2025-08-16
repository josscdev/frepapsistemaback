using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_VentasTPF
{
    public interface IVentasTPFServices
    {
        Task<List<TipoDocumentoResponse>> GetTipoDocumentoTPF(int idemppaisnegcue);
        Task<List<TipoBiometriaResponse>> GetTipoBiometriaTPF(int idemppaisnegcue);
        Task<List<SubproductoResponse>> GetSubproductoTPF(int idemppaisnegcue);
        Task<List<OperadorReponse>> GetOperadorTPF(int idemppaisnegcue);
        Task<List<TipoEquipoResponse>> GetTipoEquipoTPF(int idemppaisnegcue);
        Task<List<TipoEtiquetaResponse>> GetTipoEtiquetaTPF(int idemppaisnegcue);
        Task<List<TipoPagoResponse>> GetTipoPagoTPF(int idemppaisnegcue);
        Task<List<TipoSeguroResponse>> GetTipoSeguroTPF(int idemppaisnegcue);

        Task<List<PlanesResponse>> GetPlanesTPF(int idemppaisnegcue);
        Task<List<ModeloResponse>> GetModeloTPF(int idemppaisnegcue);
        Task<List<BundleResponse>> GetBundleTPF(int idemppaisnegcue);
        Task<List<TipoAccesorioResponse>> GetTipoAccesorioTPF(int idemppaisnegcue);
        //Task<List<IMEIequiposResponse>> GetIMEISequiposTPF(int idusuario);
        Task<List<VentasDetalleTotal>> GetVentasAdminTPF(FiltrarVentasPerfiles filtros);
        Task<List<VentasDetalleTotal>> GetVentasJefeTPF(FiltrarVentasPerfiles filtros);
        Task<List<VentasDetalleTotal>> GetVentasSuperTPF(FiltrarVentasPerfiles filtros);
        Task<List<VentasDetalleTotal>> GetVentasPromotorTPF(FiltrarVentasPerfiles filtros);
        Task<Respuesta> DeleteVentasDetalleTPF(int idventasdetalle, string usuarioanulacion);

        Task<VentasResult> PostVentasTPF(Ventas ventas);
        Task<ActualizarNombreVoucherResponse> UpdateNombreVoucherTPF(ActualizarNombreVoucherRequest request);
        Task<VentasResult> UpdateVentasDetalleTPF(VentasDetalle request);
        Task<VentasResult> UpdateVoucherVentasTPF(Ventas venta);
    }
}
