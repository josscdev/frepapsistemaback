using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas
{
    public class UploadImageRequest
    {
        public string? Base64Image { get; set; }
        public string? VoucherName { get; set; }
    }
}
