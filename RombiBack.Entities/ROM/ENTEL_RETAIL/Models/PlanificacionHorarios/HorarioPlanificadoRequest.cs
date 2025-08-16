using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios
{
    public class HorarioPlanificadoRequest
    {
        public int? idhorarioplanificado { get; set; }
        public string? dnisupervisor { get; set; }
        public string? dnipromotor { get; set; }
        public int? idpdv { get; set; }
        public string? puntoventa { get; set; }
        public string? fecha { get; set; }
        public string? horarioentrada { get; set; }
        public string? horariosalida { get; set; }
        public string? descripcion { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public string? usuario_creacion { get; set; }
        public string? usuario_modificacion { get; set; }
        public int? activarcbo { get; set; }
        public int? idemppaisnegcue { get; set; }

        //solo para los reportes 
        public string?usuario { get; set; }
    }

    public class ListaHorarioPlanificadoRequest
    {
        public string? dnisupervisor { get; set; }
        public string? dnipromotor { get; set; }
        public string? puntoventa { get; set; }
        public string? fecha { get; set; }
        public string? horarioentrada { get; set; }
        public string? horariosalida { get; set; }
        public int? idemppaisnegcue { get; set; }
        public string? usuario_creacion { get; set; }
    }

    public class RespuestaValidacion
    {
        public string? estado { get; set; }  // "success" o "error"
        public string? mensaje { get; set; }
        public string? dnipromotor { get; set; }
        public string? dnisupervisor { get; set; }
        public string? puntoventa { get; set; }
        public DateTime fecha { get; set; }
        public string? horarioentrada { get; set; }
        public string? horariosalida { get; set; }
        public int? idemppaisnegcue { get; set; }
        public string? usuario_creacion { get; set; }
        public int? idpdv { get; set; }
        public int? activarcbo { get; set; }
        public string? descripcion { get; set; }
    }
}
