using RombiBack.Abstraction;
using RombiBack.Entities.ROM.LOGIN.UserType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.LOGIN.MGM_UserType
{
    public interface IUsertypeRepository/*:IRepository<UserType>*/
    {
        Task<List<UserType>> GetAll();
    }
}
