using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles
{
    public class ValidacionBundle
    {

        public int? idventas { get; set; }
        public int? idventasdetalle { get; set; }
        public string? fechaoperacion { get; set; }
        public string? doccliente { get; set; }
        public string? numcelularcontrato { get; set; }
        public string? docpromotorasesor { get; set; }
        public string? nombrepromotor { get; set; }
        //public string? dnipromotor { get; set; }
        public int? idsubproducto { get; set; }
        public string? nombresubproducto { get; set; }
        public int? idplan { get; set; }
        public string? nombreplan { get; set; }
        public int? idmodelo { get; set; }
        public string? nombremodelo { get; set; }
        public int? idbundle { get; set; }
        public string? codigobundle { get; set; }
        public string? nombrebundle { get; set; }
        public string? nombreobsequio { get; set; }
        public string? nombrepdffirma { get; set; }
        //public string? strcodigoauthbundle { get; set; }
        public int? idemppaisnegcue { get; set; }
        public string? fechacreacion { get; set; }
        public string? numeroorden { get; set; }
        public int? idpdv { get; set; }
        public string? nombrepdv { get; set; }
        public int? flagcodigoauthbundle { get; set; }
        public string? usuariocreacion { get; set; }

    }
}
