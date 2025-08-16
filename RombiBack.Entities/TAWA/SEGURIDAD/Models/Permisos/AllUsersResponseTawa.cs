using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.TAWA.SEGURIDAD.Models.Permisos
{
    public class AllUsersResponseTawa
    {
        public int? idusuario { get; set; }
        public string? docusuario { get; set; }
        public string? nombrecompleto { get; set; }
        public string? nombres { get; set; }
        public string? apellidopaterno { get; set; }
        public string? apellidomaterno { get; set; }
        public string? correo { get; set; }
        public string? usuario { get; set; }
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
