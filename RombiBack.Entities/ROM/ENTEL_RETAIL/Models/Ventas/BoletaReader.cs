using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas
{
    public class BoletaReader
    {
        public class BoletaData
        {
            public string numRuc { get; set; }
            public string codComp { get; set; }
            public string numeroSerie { get; set; }
            public string numero { get; set; }
            public string fechaEmision { get; set; }
            public string monto { get; set; }
            public string tramaQRCode { get; set; }
            public bool ironBarCode { get; set; }
            public string nombreBoletaArchivo { get; set; }
        }

        public class BoletaResponse
        {
            public BoletaData data { get; set; }
            public bool isSuccess { get; set; }
            public string message { get; set; }
        }
    }
}
