using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas
{
    public class VentasDetalle
    {
        public int? idventasdetalle { get; set; }
        public int? idventas { get; set; }
        public int? idsubproducto { get; set; }
        public string? nombresubproducto { get; set; }
        public int? idoperador { get; set; }
        public string? nombreoperador { get; set; }
        public int? idtipoequipo { get; set; }
        public int? idtiposeguro { get; set; }
        public decimal? montoupselling { get; set; }
        public string? nombretipoequipo { get; set; }
        public int? idtipoetiqueta { get; set; }
        public string? nombretipoetiqueta { get; set; }
        public int? idtipopago { get; set; }
        public string? nombrepago { get; set; }
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
        public int? estado { get; set; }
        public string? usuariocreacion { get; set; }
        public string? fechacreacion { get; set; }
        public string? usuariomodificacion { get; set; }
        public string? fechamodificacion { get; set; }
        public string? usuarioanulacion { get; set; }
        public string? fechaanulacion { get; set; }
    }
}
