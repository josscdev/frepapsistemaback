using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RombiBack.Entities.ROM.FREPAPMODULE.MODULOREGISTROS;

namespace RombiBack.Services.ROM.FREPAPMODULE.MODULOREGISTROS
{
    public interface IAfiliacionesServices
    {
        Task<IEnumerable<ListarAfiliacion>> ListarAfiliaciones(FiltroAfiliacion filtro);
        Task<IEnumerable<ListarOpcionUbigeo>> ListarUbigeos(int? idemppaisnegcue, int? pais);
        Task<RegistrarAfiliacionResult> RegistrarAfiliacion(
          AfiliacionCreateDto model,
          IFormFile? foto,
          IFormFile? fichaafiliacionfile,
          IFormFile? hojadevida,
          IFormFile? copiadocumento);
        Task<IEnumerable<ListarEstadoCivil>> ListarEstadosCiviles(int idemppaisnegcue);
        Task<IEnumerable<ListarTipoDocumento>> ListarTiposDocumento(int idemppaisnegcue);
        Task<RespuestaAfiliacionDesactivar> PostDesactivarAfiliacion(FiltroAfiliacionDesactivar request);

        Task<AfiliacionReadDto?> GetAfiliacionById(int idafiliacion);
        Task<RegistrarAfiliacionResult> ActualizarAfiliacion(
            int idafiliacion,
            AfiliacionUpdateDto model,
            IFormFile? foto,
            IFormFile? fichaafiliacionfile,
            IFormFile? hojadevida,
            IFormFile? copiadocumento // futuro
        );
    }

    public sealed class RegistrarAfiliacionResult
    {
        public bool Ok { get; set; }
        public Dictionary<string, string?> Files { get; set; } = new();
        public string Error { get; internal set; }
    }
}
