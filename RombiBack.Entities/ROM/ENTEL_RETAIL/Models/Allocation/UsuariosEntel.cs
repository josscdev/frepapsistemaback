using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Allocation
{
    public class UsuariosEntel
    {
        public int? idusuarioentel { get; set; }
        public string? docusuario { get; set; }
        public string? usuarioredtde { get; set; }
        public string? usuarioportal { get; set; }
        public string? correocorp { get; set; }
        public string? celular { get; set; }
        public int? idemppaisnegcue { get; set; }

        public int? estado { get; set; }
        public string? usuariocreacion { get; set; }
        public DateTime? fechacreacion { get; set; } // Puedes usar DateTime.Now por defecto
        public string? usuariomodificacion { get; set; }
        public DateTime? fechamodificacion { get; set; } // Puedes usar DateTime.Now por defecto
    }
}
