using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model.UserAuth
{
    public class UserAuth
    {
        public int idusuario { get; set; }
        public string nombres { get; set; }
        public string apellidopaterno { get; set; }
        public string apellidomaterno { get; set; }
        public int idjerarquia { get; set; }
        public string correo { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string cod_pais { get; set; }
        public string cod_negocio { get; set; }
        public string cod_cuenta { get; set; }
        public string es_admin { get; set; }
    }
}
