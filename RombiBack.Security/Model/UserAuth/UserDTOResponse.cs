using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model.UserAuth
{
    public class UserDTOResponse
    {
        public string Resultado { get; set; }
        public int Accede { get; set; }
        public string? Perfil { get; set; }
        public int? idusuario { get; set;}
        public int? idusuarioromweb { get; set; }


    }
}
