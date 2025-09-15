using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.FREPAPMODULE.MODULOREGISTROS;

namespace RombiBack.Repository.ROM.FREPAPMODULE.MODULOREGISTROS
{
    public interface IAfiliacionesRepository
    {
        Task<IEnumerable<ListarAfiliacion>> ListarAfiliaciones(FiltroAfiliacion filtro);
        Task<IEnumerable<ListarOpcionUbigeo>> ListarUbigeos(int? idemppaisnegcue, int? pais);
        // ➕ Registrar afiliación (devuelve el ID generado)
        Task<long> InsertAfiliacionAsync(AfiliacionCreateDto dto, string usuario);
        Task UpdateArchivosAsync(
            long idafiliacion,
            string? fotoimg,
            string? fichaafiliacionpdf,
            string? hojadevidapdf
        // string? copiadocumentopdf // si creas esta columna, descomenta
        );

        Task<IEnumerable<ListarEstadoCivil>> ListarEstadosCiviles(int idemppaisnegcue);
        Task<IEnumerable<ListarTipoDocumento>> ListarTiposDocumento(int idemppaisnegcue);
        Task<RespuestaAfiliacionDesactivar> PostDesactivarAfiliacion(FiltroAfiliacionDesactivar request);
    }
}
