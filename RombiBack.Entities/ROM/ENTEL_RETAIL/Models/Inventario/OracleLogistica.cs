using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.OracleLogistica
{

    public class ResultadoCargaMasiva
    {
        public string? Estado { get; set; }
        public string? Mensaje { get; set; }
    }

    public class OracleLogistica
    {
        public string? PdvRom { get; set; }
        public string? Supervisor { get; set; }
        public string? Pareto { get; set; }
        public string? GestorRom { get; set; }
        public string? Categoria { get; set; }
        public string? Territorio { get; set; }
        public string? TipoArticulo { get; set; }
        public string? CodigoOracle { get; set; }
        public string? Descripcion { get; set; }
        public string? SerieSim { get; set; }
        public string? SerieFicticio { get; set; }
        public string? Valorizado { get; set; }
        public string? FechaRecepcion { get; set; }
        public string? StatusActual { get; set; }
        public string? TipoAlmacen { get; set; }
        public string? DiasStock { get; set; }
        public string? LargoSim { get; set; }
        public string? LargoFicticio { get; set; }
        public string? ModeloUnico { get; set; }
        public string SubInventario { get; set; }
    }

    public class FiltroInsertarConciliar
    {
        public string fechainicioinv { get; set; }
        public string fechafininv { get; set; }
        public string fechainiciolog { get; set; }
        public string fechafinlog { get; set; }
        public int idemppaisnegcue { get; set; }
        public string usuario { get; set; }
    }

    public class MatchResumenDto
    {
        public int? idconciloracleinv { get; set; }
        public string? pdvrom { get; set; }
        public int? idpdv { get; set; }
        public string? nombrepdv { get; set; }
        public string? docpromotorasesor { get; set; }
        public string? imeiequipo { get; set; }
        public string? imeiserieficticio { get; set; }
        public DateTime? fechacargaol { get; set; }
        public DateTime? fecharegistroinv { get; set; }
        public string? imeiflagestado { get; set; }
        public DateTime? fechaconciliacion { get; set; }
        public int? idemppaisnegcue { get; set; }
        public int? estado { get; set; }
        public string? usuariocreacion { get; set; }
        public DateTime? fechacreacion { get; set; }
        public string? usuariomodificacion { get; set; }
        public DateTime? fechamodificacion { get; set; }
        public string? usuarioanulacion { get; set; }
        public DateTime? fechaanulacion { get; set; }
    }
    

}
