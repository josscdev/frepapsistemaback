using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Inventario
{
    public class Inventario
    {
        // ----------- Resultado Insertar -----------

        public class InventarioResultado {
            public string? Estado { get; set; }
            public string? Mensaje { get; set; }
            public int? idinventario { get; set; }
        }

        // ----------- Insertar -----------

        public class InsertarInventario
        {
            public string? docpromotorasesor { get; set; }
            public int? idpdv { get; set; }
            public int? idemppaisnegcue { get; set; }
            public string? usuario { get; set; }
            public List<InsertarInventarioDetalle>? detalles { get; set; }
        }

        public class InsertarInventarioDetalle
        {
            public int? idinventario { get; set; }
            public DateTime fecharegistro { get; set; }
            public string? imeiequipo { get; set; }
            public int? idemppaisnegcue { get; set; }
            public string? usuario { get; set; }
        }

        public class InsertarSoloInventarioDetalle
        {
            public int? idinventario { get; set; }
            public string? usuario { get; set; }
            public List<InsertarInventarioDetalle>? detalles { get; set; }
        }

        // ----------- Filtros Listar -----------

        public class InventarioFiltroListar
        {
            public int idpdv { get; set; }
            public string fechainicio { get; set; }
            public string fechafin { get; set; }
            public string docusuario { get; set; }
            public int idemppaisnegcue { get; set; }
            public string perfil { get; set; }
        }

        // ----------- Listar -----------

        public class ListarInventario
        {
            public int? idinventario { get; set; }
            public string? docpromotorasesor { get; set; }
            public int? idpdv { get; set; }
            public string? nombrepdv { get; set; }
            public DateTime fecharegistro { get; set; }
            public string? nombres { get; set; }
            public string? apellidopaterno { get; set; }
            public string? apellidomaterno { get; set; }
            public string? nombrecompleto { get; set; }
            public string? departamento { get; set; }
            public string? zona { get; set; }
            public int? idemppaisnegcue { get; set; }
            public string? usuariocreacion { get; set; }
            public DateTime? fechacreacion { get; set; }
            public string? usuariomodificacion { get; set; }
            public DateTime? fechamodificacion { get; set; }
            public string? usuarioanulacion { get; set; }
            public DateTime? fechanulacion { get; set; }
            public int? estado { get; set; }
        }

        public class ListarInventarioDetalle
        {
            public int? idinventariodetalle { get; set; }
            public int? idinventario { get; set; }
            public string? imeiequipo { get; set; }
            public int? idemppaisnegcue { get; set; }
            public string? usuariocreacion { get; set; }
            public DateTime? fechacreacion { get; set; }
            public string? usuariomodificacion { get; set; }
            public DateTime? fechamodificacion { get; set; }
            public string? usuarioanulacion { get; set; }
            public DateTime? fechaanulacion { get; set; }
            public int? estado { get; set; }
        }
    }
}
