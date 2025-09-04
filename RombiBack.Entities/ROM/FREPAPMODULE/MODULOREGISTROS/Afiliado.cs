using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int edadafiliado { get; set; }
        public int estado { get; set; }
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
}
