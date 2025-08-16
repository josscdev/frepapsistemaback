using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.TAWA.SEGURIDAD.Models.Permisos
{
    public class AllUsersRequestTawa
    {
        public int? idempresa { get; set; }
        public int? idpais { get; set; }
        public int? idnegocio { get; set; }
        public int? idcuenta { get; set; }
        public string? usuario { get; set; }
    }
}
