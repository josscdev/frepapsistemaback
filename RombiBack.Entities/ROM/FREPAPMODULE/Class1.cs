using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.FREPAPMODULE
{
    public class Afiliado
    {
        public Guid Id { get; set; }

        public string? NroAfili { get; set; }
        public DateTime? FechaAfiliacion { get; set; }

        public string Nombres { get; set; } = null!;
        public string ApPaterno { get; set; } = null!;
        public string? ApMaterno { get; set; }

        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; } = null!;

        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public int Estado { get; set; } 

        public string? Region { get; set; }
        public string? Provincia { get; set; }
        public string? Distrito { get; set; }

        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Observacion { get; set; }

        // --- Firma y Huella (BLOB + metadatos opcionales)
        public byte[]? Firma { get; set; }
        public string? FirmaContentType { get; set; }   // image/png, image/jpeg
        public long? FirmaSize { get; set; }

        public byte[]? Huella { get; set; }
        public string? HuellaContentType { get; set; }
        public long? HuellaSize { get; set; }
    }
}
