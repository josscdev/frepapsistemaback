using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios
{
    public class HorarioPlanificadoPromotorRequest
    {

        public string? inicio { get; set; }
        public string? fin { get; set; }
        public int? idpdv { get; set; }
        public string? dnipromotor { get; set; }
        public int? idemppaisnegcue { get; set; }

    }
}
