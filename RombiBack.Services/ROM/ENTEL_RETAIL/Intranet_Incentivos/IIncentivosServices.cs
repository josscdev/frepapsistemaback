using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Incentivos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRestNetCore.DTO.DtoIncentivo;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.Intranet_Incentivos
{
    public interface IIncentivosServices
    {
        UserDTO ValidateUser(string dni, string password);
        IEnumerable<IncentivoVistaDTO> GetGeneralIncentivosVistasWithDNIConfirmationFalse(string dni);

        IEnumerable<IncentivoVistaDTO> GetIncentivosPremios(string dni);

        void UpdateConfirmacionEntrega(string dni, int idIncentivo);
    }
}
