using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.FREPAPMODULE.MODULOREGISTROS;
using RombiBack.Repository.ROM.FREPAPMODULE.MODULOREGISTROS;

namespace RombiBack.Services.ROM.FREPAPMODULE.MODULOREGISTROS
{
    public class AfiliacionesServices: IAfiliacionesServices
    {
        private readonly IAfiliacionesRepository _afiliacionesRepository;

        public AfiliacionesServices(IAfiliacionesRepository afiliacionesRepository)
        {
            _afiliacionesRepository = afiliacionesRepository;
        }

        public Task<IEnumerable<ListarAfiliacion>> ListarAfiliaciones(FiltroAfiliacion filtro)
        {
            // Aquí podrías aplicar reglas adicionales (normalizar perfil/usuario, etc.)
            return _afiliacionesRepository.ListarAfiliaciones(filtro);
        }

        public Task<IEnumerable<ListarOpcionUbigeo>> ListarUbigeos(int? idemppaisnegcue, int? pais)
        => _afiliacionesRepository.ListarUbigeos(idemppaisnegcue, pais);
    }
}
