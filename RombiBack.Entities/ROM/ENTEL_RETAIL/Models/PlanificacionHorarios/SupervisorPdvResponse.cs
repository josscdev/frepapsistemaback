using RombiBack.Entities.ROM.LOGIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios
{
    public class SupervisorPdvResponse
    {
        public string? dnisupervisor { get; set; }
        public string? usuario { get; set; }
        public int? idpuntoventarol { get; set; }

        public string? puntoventa { get; set; }

        public string? fechainicio { get; set; }

        public string? fechafin { get; set; }
        public int? idemppaisnegcue { get; set; }
    }
}
