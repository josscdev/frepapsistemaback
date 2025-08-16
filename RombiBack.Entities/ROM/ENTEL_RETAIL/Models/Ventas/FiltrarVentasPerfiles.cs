using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas
{
    public class FiltrarVentasPerfiles
    {
        public string? idjefe { get; set; }  
        public string? idsuper { get; set; }
        public int? idemppaisnegcue { get; set; }
        public string? idpromotor { get; set; } 
        public string? fechainicial { get; set; }  
        public string? fechafinal { get; set; } 
    }
}
