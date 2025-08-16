using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas
{
    public class VentasResult
    {
        public bool? success { get; set; }
        public string? message { get; set; }
        public string? nombrevoucher { get; set; }
        public int? idventa { get; set; }
        public int? idventasdetalle { get; set; }
    }
}
