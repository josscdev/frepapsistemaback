using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Allocation
{
    public class Roles
    {
        public int? idrol { get; set; }
        public string? docusuario { get; set; }
        public string? nombres { get; set; }
        public int? idpdv { get; set; }
        public string? nombrepdv { get; set; }
        public DateTime? fechainicio { get; set; }
        public DateTime? fechafin { get; set; }
        public string? usuarioredtde { get; set; }
        public string? usuarioportal { get; set; }
        public int? idtipolicencia { get; set; }
        public string? nombretipolicencia { get; set; }
        public string? observacionlicencia { get; set; }
        public int? idtipoestado { get; set; }
        public string? nombretipoestado { get; set; }
        public int? idtipotrabajo { get; set; }
        public string? nombretipotrabajo { get; set; }

        public int? idtipofuncionalidad { get; set; }
        public string? nombretipofuncionalidad { get; set; }
        public string? referente { get; set; }
        public string? gestante { get; set; }
        public DateTime? fechacarnetsanidad { get; set; }
        public int? idemppaisnegcue { get; set; }
        public DateTime? fechacese { get; set; }
        public int? estado { get; set; }
        public string? usuariocreacion { get; set; }
        public DateTime? fechacreacion { get; set; } // Puedes usar DateTime.Now por defecto
        public string? usuariomodificacion { get; set; }
        public DateTime? fechamodificacion { get; set; } // Puedes usar DateTime.Now por defecto
    }
}
