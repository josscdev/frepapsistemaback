using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Allocation
{
    public class NombreExcel
    {
        public string? nombrearchivo { get; set; }
        public string? usuariocreacion { get; set; }
        public DateTime? fechacreacion { get; set; }
        public string? usuariomodificacion { get; set; }
        public DateTime? fechamodificacion { get; set; }
        public string? UsuarioEliminacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
