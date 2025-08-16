using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios
{
    public class TurnosSupervisor
    {
        public int? idpdvturno { get; set; }

        public int? idturnos { get; set; }
        public string? usuario { get; set; }
        public string? horarioentrada { get; set; }
        public string? horariosalida { get; set; }
        public string? descripcion { get; set; }
        public int? idtipoturno { get; set; }
        public int? estado { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public string? usuario_creacion { get; set; }
        public string? usuario_modificacion { get; set; }
        public int? idemppaisnegcue { get; set; }
    }
}
