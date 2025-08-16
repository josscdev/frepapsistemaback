using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos
{
    public class CodigosResponse
    {
        public int? idemppaisnegcue { get; set; }
        public int? idempresa { get; set; }
        public string? nombreempresa { get; set; }
        public int? idpais { get; set; }
        public string? nombrepais { get; set; }
        public int? idnegocio { get; set; }
        public string? nombrenegocio { get; set; }
        public int? idcuenta { get; set; }
        public string? nombrecuenta { get; set; }
        public int? estado { get; set; }

    }
}
