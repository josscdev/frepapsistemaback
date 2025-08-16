using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos
{
    public class RespuestaUsuario
    {
        public string? Estado { get; set; }
        public string? Mensaje { get; set; }
    }
    public class InsertarUsuario
    {
        public string docusuario { get; set; }
        public string nombres { get; set; }
        public string apellidopaterno { get; set; }
        public string apellidomaterno { get; set; }
        public string? correopersonal { get; set; }
        public string? correocorp { get; set; }
        public string? celular { get; set; }
        public string? sexo { get; set; }
        public DateTime? fechanacimiento { get; set; }
        public string? direccion { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public int idemppaisnegcue { get; set; }
        public int estado { get; set; }
        public DateTime? fechaingreso { get; set; }
        public string usuariocreacion { get; set; }
    }

    public class RespuestaUsuarioXDocumento
    {
        public int idusuario { get; set; }
        public string docusuario { get; set; }
        public string nombres { get; set; }
        public string apellidopaterno { get; set; }
        public string apellidomaterno { get; set; }
        public string? correopersonal { get; set; }
        public string? correocorp { get; set; }
        public string? celular { get; set; }
        public string? sexo { get; set; }
        public DateTime? fechanacimiento { get; set; }
        public string? direccion { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public int idemppaisnegcue { get; set; }
        public int estado { get; set; }
        public DateTime? fechaingreso { get; set; }
    }

    public class RespuestaValidarUsuario
    {
        public string? docusuario { get; set; }
        public string? estado { get; set; }
        public string? mensaje { get; set; }
    }

    public class ListaValidarUsuario
    {
        public string docusuario { get; set; }
        public string nombres { get; set; }
        public string apellidopaterno { get; set; }
        public string apellidomaterno { get; set; }
        public string? correopersonal { get; set; }
        public string? correocorp { get; set; }
        public string? celular { get; set; }
        public string? sexo { get; set; }
        public DateTime? fechanacimiento { get; set; }
        public string? direccion { get; set; }
        public string usuario { get; set; } //usuario que modifica o crea
        public int idemppaisnegcue { get; set; }
        public DateTime? fechaingreso { get; set; }
        public string uuser { get; set; }
        public string clave { get; set; }
    }
}
