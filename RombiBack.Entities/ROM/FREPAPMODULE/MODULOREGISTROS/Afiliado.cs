using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RombiBack.Entities.ROM.FREPAPMODULE.MODULOREGISTROS
{
    // FiltroAfiliacion.cs
    public class FiltroAfiliacion
    {
        public string? region { get; set; }
        public string? provincia { get; set; }
        public string? distrito { get; set; }
        public string? perfil { get; set; }   // "ADMIN" | "USUARIO"
        public string? usuario { get; set; }  // se compara con usuariocreacion
    }

    // ListarAfiliacion.cs
    public class ListarAfiliacion
    {
        public int idafiliacion { get; set; }
        public string? numficha { get; set; }
        public string? docafiliado { get; set; }
        public string? nombres { get; set; }
        public string? apellidopaterno { get; set; }
        public string? apellidomaterno { get; set; }
        public DateTime? fechaafiliacion { get; set; }
        public int? edadafiliado { get; set; }
        public int? estado { get; set; }
        public string? estado_text { get; set; }
        public string? codubicacion { get; set; }
        public string? region { get; set; }
        public string? provincia { get; set; }
        public string? distrito { get; set; }
        public string? usuariocreacion { get; set; }
        public DateTime? fechacreacion { get; set; }
        public string? usuariomodificacion { get; set; }
        public DateTime? fechamodificacion { get; set; }
        public string? usuarioanulacion { get; set; }
        public DateTime? fechaanulacion { get; set; }
    }

    public class ListarOpcionUbigeo
    {
        public string codubicacion { get; set; } = string.Empty;
        public string rr { get; set; } = string.Empty;
        public string pp { get; set; } = string.Empty;
        public string dd { get; set; } = string.Empty;
        public string? region { get; set; }
        public string? subregion { get; set; }
        public string? localidad { get; set; }
    }
    public class FiltroUbigeo
    {
        public int? idemppaisnegcue { get; set; }
        public int? pais { get; set; }
    }

    public sealed class AfiliacionCreateDto
    {
        public string? numficha { get; set; }
        public string? fechaafiliacion { get; set; }   // "YYYY-MM-DD"
        public string? nombres { get; set; } = default!;
        public string? apellidopaterno { get; set; } = default!;
        public string? apellidomaterno { get; set; }

        public string? idtipodocumento { get; set; } = default!;
        public string? docafiliado { get; set; }

        public string? fechanacimiento { get; set; }   // "YYYY-MM-DD"
        public int? edadafiliado { get; set; }

        public string? rr { get; set; }   // "RR"
        public string? pp { get; set; }   // "RRPP"
        public string? dd { get; set; }   // "RRPPDD"
        public string? ubigeo { get; set; }

        public string? avenida { get; set; }
        public string? numero { get; set; }
        public string? urbanizacion { get; set; }

        public string? telefono { get; set; }
        public string? correo { get; set; } = default!;
        public string? estado_text { get; set; } = "ACTIVO";
        public int? estado { get; set; }
        public string? observacion { get; set; }
    }

    public class RegistrarAfiliacionForm
    {
        // === Campos del DTO ===
        public string? numficha { get; set; }
        public string? fechaafiliacion { get; set; }
        public string? nombres { get; set; } = default!;
        public string? apellidopaterno { get; set; } = default!;
        public string? apellidomaterno { get; set; }
        public string? idtipodocumento { get; set; } = default!;
        public string? docafiliado { get; set; }
        public string? fechanacimiento { get; set; }
        public int? edadafiliado { get; set; }
        public string? rr { get; set; }
        public string? pp { get; set; }
        public string? dd { get; set; }
        public string? ubigeo { get; set; }
        public string? avenida { get; set; }
        public string? numero { get; set; }
        public string? urbanizacion { get; set; }
        public string? telefono { get; set; }
        public string? correo { get; set; } = default!;
        public string? estado_text { get; set; } = "ACTIVO";
        public int? estado { get; set; }
        public string? observacion { get; set; }

        // === Archivos ===
        public IFormFile? foto { get; set; }
        public IFormFile? fichaafiliacionfile { get; set; }
        public IFormFile? hojadevida { get; set; }
        public IFormFile? copiadocumento { get; set; }
    }

}
