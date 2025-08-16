using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas
{
    public class Ventas
    {
        public int? idventas { get; set; }
        public string? fechaoperacion { get; set; }
        public string? docpromotorasesor { get; set; }
        public int?  idpdv { get; set; }
        public int? idtipodocumento { get; set; }
        public string? doccliente { get; set; }
        public int? idtipobiometria { get; set; }
        public string? numcelularcontrato { get; set; }
        public string? correocliente { get; set; }
        public string? observacion { get; set; }
        public string? nombrevoucher { get; set; }
        public string? numeroruc { get; set; }
        public string? codcomprobante { get; set; }
        public string? numeroserie { get; set; }
        public string? numero { get; set; }
        public string? fechaemisionvoucher { get; set; }
        public decimal? montovoucher { get; set; }
        public string? tramaqrcode { get; set; }
        public int? idemppaisnegcue { get; set; }
        public int? estado { get; set; }
        public string? usuariocreacion { get; set; }
        public string? fechacreacion { get; set; }
        public string? usuariomodificacion { get; set; }
        public string? fechamodificacion { get; set; }
        public string? usuarioanulacion { get; set; }
        public string? fechaanulacion { get; set; }
        public List<VentasDetalle>? VentasDetalles { get; set; }
    }
}
