using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios
{
    public class TurnosPdvRequest
    {
        public string? usuario { get; set; }
        public int? idpdv { get; set; }
        public string? puntoventa { get; set; }
        public int? idturnos { get; set; }
        public int? estado { get; set; }

        public int? idpdvturno { get; set; }

        public string? usuario_creacion { get; set; }
        public string? usuario_modificacion { get; set; }

        public int? idemppaisnegcue { get; set; }
    }
}
