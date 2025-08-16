using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Security.Model.UserAuth.Modules;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models;

namespace RombiBack.Security.Auth.Repsitory
{
    public interface IAuthRepository
    {
        //Task<UserAuth> ValidateUser(UserDTORequest request);
        Task<UserDTOResponse> RombiLoginMain(UserDTORequest request);
        Task<UserDataDTOResponse> GetUserData(UserDTORequest request);
        Task<List<BusinessAccountResponse>> GetBusinessUser(UserDTORequest request);
        Task<List<BusinessAccountResponse>> GetBusinessAccountUser(UserDTORequest request);
        Task<List<ModuloDTOResponse>> GetPermissions(UserDTORequest request);
        Task<IdCodigo> GetIdCodigo(CodigosRequest request);
        Task<IdCodigo> GetIdpdv(CodigosRequest request);
        List<RETAIL_AsistenciaBE> GetMarcacionPromotor(string usuario );
        Task<string> GetRutaPrincipal(int idemppaisnegcue);


    }
}
