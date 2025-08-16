using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model.UserAuth
{
    public class BusinessAccountResponse
    {
        public int idpais { get; set; }
        public int idnegocio { get; set; }
        public string nombrenegocio { get; set; }

        public int idcuenta { get; set; }
        public string nombrecuenta { get; set; }
    }
}
