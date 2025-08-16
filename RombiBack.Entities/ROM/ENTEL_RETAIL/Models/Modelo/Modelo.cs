using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo
{
    public class Modelo
    {
        public int? idmodelo { get; set; }

        public string? nombremodelo { get; set; }
        public string? nombremarca { get; set; }
        public string? nombregamma { get; set; }
        public int? idemppaisnegcue { get; set; }
        public int? estado { get; set; }
        public string? usuariocreacion { get; set; }
        public DateTime? fechacreacion { get; set; }
        public string? usuariomodificacion { get; set; }
        public DateTime? fechamodificacion { get; set; }
    }
}
