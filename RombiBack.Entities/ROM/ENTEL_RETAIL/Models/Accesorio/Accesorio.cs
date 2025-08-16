using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Accesorios
{
    public class Accesorio
    {
        public int? idtipoaccesorio { get; set; }
        public string? nombretipoaccesorio { get; set; }
        public string? subtipoaccesorio { get; set; }
        public string? categoriaaccesorio { get; set; }
        public int? idemppaisnegcue { get; set; }
        public int? estado { get; set; }
        public string? usuariocreacion { get; set; }
        public DateTime? fechacreacion { get; set; }
        public string? usuariomodificacion { get; set; }
        public DateTime? fechamodificacion { get; set; }
    }
}
