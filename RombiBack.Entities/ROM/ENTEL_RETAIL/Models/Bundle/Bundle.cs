using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Bundles
{
    public class ResultadoBundle
    {
        public string? Estado { get; set; }
        public string? Mensaje { get; set; }
    }
    public class ActualizarBundle
    {
        public int idbundle { get; set; }
        public int estadobundle { get; set; }
        public string usuario { get; set; }
    }
    public class ListarBundle
    {
        public int? idbundle { get; set; }
        public string? codigobundle { get; set; }
        public string? nombrebundle { get; set; }
        public int? flagauthmessage { get; set; }
        public int? idcodigo { get; set; }
        public int? estado { get; set; }
        public int? idemppaisnegcue { get; set; }
    }


}
