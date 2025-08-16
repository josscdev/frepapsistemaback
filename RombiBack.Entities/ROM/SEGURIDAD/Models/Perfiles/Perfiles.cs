using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.SEGURIDAD.Models.Perfiles
{
    public class Perfiles
    {
        public int idperfiles { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int estado { get; set; }
        public string usuario_creacion { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public string usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
    }
}
