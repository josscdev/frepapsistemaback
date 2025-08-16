using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Inventario
{
    public interface IInventarioServices
    {
        Task<List<Inventario.ListarInventario>> GetInventario(Inventario.InventarioFiltroListar request);
        Task<Inventario.InventarioResultado> PostInventarioMasivo(Inventario.InsertarInventario inventario);
        Task<Inventario.InventarioResultado> DeleteInventario(int idinventario, string usuario);
        Task<List<Inventario.ListarInventarioDetalle>> GetInventarioDetalle(int idinventario);
        Task<Inventario.InventarioResultado> DeleteInventarioDetalle(int idinventariodetalle, string usuario);
        Task<Inventario.InventarioResultado> PostInventarioDetalle(Inventario.InsertarSoloInventarioDetalle inventario);
    }
}
