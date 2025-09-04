using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.FREPAPMODULE.MODULOREGISTROS;

namespace RombiBack.Services.ROM.FREPAPMODULE.MODULOREGISTROS
{
    public interface IAfiliacionesServices
    {
        Task<IEnumerable<ListarAfiliacion>> ListarAfiliaciones(FiltroAfiliacion filtro);
        Task<IEnumerable<ListarOpcionUbigeo>> ListarUbigeos(int? idemppaisnegcue, int? pais);
    }
}
