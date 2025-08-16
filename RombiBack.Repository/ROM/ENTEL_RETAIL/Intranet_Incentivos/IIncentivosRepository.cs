using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRestNetCore.DTO.DtoIncentivo;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.Intranet_Incentivos
{
    public interface IIncentivosRepository
    {
        User ValidateUser(string dni, string password);

        IEnumerable<IncentivoVistaDTO> GetGeneralIncentivosVistasWithDNIConfirmationFalse(string dni);

        IEnumerable<IncentivoVistaDTO> GetIncentivosPremios(string dni);

        void UpdateConfirmacionEntrega(string dni, int idIncentivo);

    }
}
