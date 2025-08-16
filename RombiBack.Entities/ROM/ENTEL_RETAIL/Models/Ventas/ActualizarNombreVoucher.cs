using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas
{
    public class ActualizarNombreVoucherRequest
    {
        public int? idventa { get; set; }
        public string? nombrevoucher { get; set; }
    }

    public class ActualizarNombreVoucherResponse
    {
        public string? mensaje { get; set; }
    }

}
