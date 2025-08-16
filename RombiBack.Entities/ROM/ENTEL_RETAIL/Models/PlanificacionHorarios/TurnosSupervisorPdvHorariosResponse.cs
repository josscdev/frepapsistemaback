using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios
{
    public class TurnosSupervisorPdvHorariosResponse
    {
        public string usuario { get; set; }
        public int idturnos { get; set; }
        public string horarioentrada { get; set; }
        public string horariosalida { get; set; }
        public int estadopdvturno { get; set; }
        public string descripcion { get; set; }
        public int idtipoturno { get; set; }
        public int estadoturno { get; set; }
    }
}
