using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model.UserAuth.Modules
{
    public class ModuloDTOResponse
    {
        public int? idcodmod { get; set; }

        public int? idmodulo { get; set; }
        public string? nombremodulo { get; set; }
        public string? iconomodulo { get; set; }
        public string? rutamodulo { get; set; }
        public int? nivelmodulo { get; set; }
        public int? ordenmodulo { get; set; }
        public int? estadomodulo { get; set; }
        public string? estadomodulopermiso { get; set; }
        public int? idperfilmodulo { get; set; }
        public string? nombreperfilmodulo { get; set; }

        public List<SubModuloDTOResponse>? submodules { get; set; }
    }
}
