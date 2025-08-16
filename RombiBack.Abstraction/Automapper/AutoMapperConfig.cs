using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Incentivos.Dto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports.DTO;
using RombiBack.Entities.ROM.LOGIN.Company;
using RombiBack.Entities.ROM.LOGIN.Country;
using RombiBack.Entities.ROM.LOGIN.UserType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRestNetCore.DTO.DtoIncentivo;

namespace EccoBack.Abstraction.Automapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Producto, ProductoDTO>();
                cfg.CreateMap<Reports, ReportsDTO>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Companys, CompanyDTO>();
                //cfg.CreateMap<UserType, UserType>();
                //cfg.CreateMap<Country, Country>();

                // Agrega más configuraciones de mapeo si es necesario
            });

            return config.CreateMapper();
        }
    }


}
