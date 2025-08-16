using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Mappers
{
    public static class ProductMapper
    {
        public static ProductoDTO MapToProductListDTO(Producto entity)
    {
        return new ProductoDTO
        {
            intModeloEquipoID = entity.intModeloEquipoID,
            strModeloEquipoDesc = entity.strModeloEquipoDesc,
        };
    }
    }
}
