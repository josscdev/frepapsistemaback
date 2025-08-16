using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Products
{
    public interface IProductoServices /*: IServices<Producto>*/
    {
        Task<List<ProductoDTO>> ObtenerTodo();



    }
}
