using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model.UserAuth
{
    public class UserDataDTOResponse
    {
        public string? usuario { get; set; }
        public string? nombres { get; set; }
        public string? apellidopaterno { get; set; }
        public string? apellidomaterno { get; set; }
        
        public string? Mensaje { get; set;}
        public bool? Success { get; set; } // Indicates if the operation was successful

    }
}
