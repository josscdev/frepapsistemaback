using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos;
using RombiBack.Security.Auth.Repsitory;
using RombiBack.Security.Model.UserAuth;
using RombiBack.Security.Model.UserAuth.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Auth.Services
{
    public class AuthServices:IAuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly IDistributedCache _cache;
        //private readonly ILogger<CompanyServices> _logger;

        private IMapper _mapper;

        public AuthServices(/*ILogger<CompanyServices> logger,*/ IDistributedCache cache, IAuthRepository authRepository, IMapper mapper)
        {
            //_logger = logger;

            _cache = cache;
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<UserDTOResponse> RombiLoginMain(UserDTORequest request)
        {
            var validateUser = await _authRepository.RombiLoginMain(request);
            return validateUser;
        }

        public async Task<UserDataDTOResponse> GetUserData(UserDTORequest request)
        {
            var validateUser = await _authRepository.GetUserData(request);
            return validateUser;
        }

        public async Task<List<BusinessAccountResponse>> GetBusinessUser(UserDTORequest request)
        {
            var getBusinessUser = await _authRepository.GetBusinessUser(request);
            return getBusinessUser;
        }
        public async Task<List<BusinessAccountResponse>> GetBusinessAccountUser(UserDTORequest request)
        {
            var getBusinessAccountUser = await _authRepository.GetBusinessAccountUser(request);
            return getBusinessAccountUser;
        }


        public async Task<List<ModuloDTOResponse>> GetPermissions(UserDTORequest request)
        {
            var getPermissions = await _authRepository.GetPermissions(request);
            return getPermissions;
        }

        public async Task<IdCodigo> GetIdCodigo(CodigosRequest request)
        {
            var getPermissions = await _authRepository.GetIdCodigo(request);
            return getPermissions;
        }


        public async Task<IdCodigo> GetIdpdv(CodigosRequest request)
        {
            var getPermissions = await _authRepository.GetIdpdv(request);
            return getPermissions;
        }

        public List<RETAIL_AsistenciaBE> GetMarcacionPromotor(string usuario )
        {
            var getMarcacion = _authRepository.GetMarcacionPromotor(usuario);
            return getMarcacion;
        }

        public async Task<string> GetRutaPrincipal(int idemppaisnegcue)
        {
            var getMarcacion = await _authRepository.GetRutaPrincipal(idemppaisnegcue);
            return getMarcacion;
        }

    }
}
