using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas
{
    public class VentasDetalleTotal
    {
        public int? idventasdetalle { get; set; }
        public int? idventas { get; set; }
        public DateTime?  fechaoperacion { get; set; }
        public string? docpromotorasesor { get; set; }
        public int? idpdv { get; set; }
        public string? nombrepdv { get; set; }

        public int? idtipodocumento { get; set; }
        public string? nombretipodocumento { get; set; }

        public string? doccliente { get; set; }
        public int? idtipobiometria { get; set; }
        public string? nombretipobiometria { get; set; }
        public string? numcelularcontrato { get; set; }
        public string? correocliente { get; set; }
        public string? observacion { get; set; }
        public string? nombrevoucher { get; set; }
        public string? numeroruc { get; set; }
        public string? codcomprobante { get; set; }
        public string? numeroserie { get; set; }
        public string? numero { get; set; }
        public DateTime? fechaemisionvoucher { get; set; }
        public decimal? montovoucher { get; set; }
        public string? tramaqrcode { get; set; }

        public int? idsubproducto { get; set; }
        public string? nombresubproducto { get; set; }
        public int? idoperador { get; set; }
        public string? nombreoperador { get; set; }
        public int? idtipoequipo { get; set; }
        public string? nombretipoequipo { get; set; }

        public int? idtiposeguro { get; set; }
        public string? nombretiposeguro { get; set; }
        public decimal? montoupselling { get; set; }
        public int? idtipoetiqueta { get; set; }
        public string? nombretipoetiqueta { get; set; }
        public int? idtipopago { get; set; }
        public string? nombretipopago { get; set; }
        public int? idplan { get; set; }
        public string? nombreplan { get; set; }
        public int? idmodelo { get; set; }
        public string? nombremodelo { get; set; }
        public string? imeiequipo { get; set; }
        public string? imeisim { get; set; }
        public string? iccid { get; set; }
        public int? idbundle { get; set; }
        public string? nombrebundle { get; set; }
        public decimal? pagocaja { get; set; }
        public string? numerocelular { get; set; }
        public int? idtipoaccesorio { get; set; }
        public string? nombretipoaccesorio { get; set; }
        public int? cantidadaccesorio { get; set; }
        public decimal? pagoaccesorio { get; set; }
        public string? imeiaccesorio { get; set; }
        public string? numeroorden { get; set; }
        public string? ventasromimeicoincide { get; set; }
        public string? codigoauthbundle { get; set; }
        public int? flagcodigoauthbundle { get; set; }
        public int? idemppaisnegcue { get; set; }
        public string? nombrecuenta { get; set; }
        public int? estado { get; set; }
        public string? usuariocreacion { get; set; }

        public DateTime? fechacreacion { get; set; }
        public string? usuariomodificacion { get; set; }

        public DateTime? fechamodificacion { get; set; }
        public string? usuarioanulacion { get; set; }
  
        public DateTime? fechaanulacion { get; set; }  // Puede ser nulo
    }

}
