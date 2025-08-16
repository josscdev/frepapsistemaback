using RombiBack.Abstraction;
using RombiBack.Entities.ROM.LOGIN.UserType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN.MGM_UserType
{
    public interface IUsertypeServices/*:IServices<UserType>*/
    {
        Task<List<UserType>> GetAll();
    }
}
