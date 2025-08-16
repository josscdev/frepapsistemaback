using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios
{
    public class ReportGetSemanaResponse
    {
        public int? idhorarioplanificado { get; set; }
        public string? dnipromotor { get; set; }
        public int? idpdv { get; set; }
        public string? puntoventa { get; set; }
        public DateOnly? fecha { get; set; }
        public string? horarioentrada { get; set; }
        public string? horariosalida { get; set; }
        public string? descripcion { get; set; }
        //public DateTime fecha_creacion { get; set; }
        //public DateTime fecha_modificacion { get; set; }
        public string? dnisupervisor { get; set; }
        //public string? usuario_modificacion { get; set; }
        public int? estado { get; set; }

    }
}
