using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products
{
    public interface IProductoRepository /*: IRepository<Producto>*/
    {
        Task<List<Producto>> ObtenerTodo();

    }
}
