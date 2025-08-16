using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.LOGIN.Business
{
    public class Business
    {

        public string? EMPRESAID { get; set; }
        public string? COD_NEGOCIO { get; set; }
        public string? DESC_NEGOCIO { get; set; }
        public string? FLAG { get; set; }
        public string? USUA_CREACION { get; set; }
        public DateTime? FECHA_CREACION { get; set; }
        public string? USUA_MODIFICACION { get; set; }
        public DateTime? FECHA_MODIFICACION { get; set; }
        public int? USUA_ANULACION { get; set; }
        public DateTime? FECHA_ANULACION { get; set; }
    }
}
