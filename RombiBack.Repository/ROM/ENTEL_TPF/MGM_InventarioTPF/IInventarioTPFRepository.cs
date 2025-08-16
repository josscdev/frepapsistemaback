using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Inventario;

namespace RombiBack.Repository.ROM.ENTEL_TPF.MGM_InventarioTPF
{
    public interface IInventarioTPFRepository
    {
        Task<List<Inventario.ListarInventario>> GetInventarioTPF(Inventario.InventarioFiltroListar request);
        Task<Inventario.InventarioResultado> PostInventarioMasivoTPF(Inventario.InsertarInventario inventario);
        Task<Inventario.InventarioResultado> DeleteInventarioTPF(int idinventario, string usuario);
        Task<List<Inventario.ListarInventarioDetalle>> GetInventarioDetalleTPF(int idinventario);
        Task<Inventario.InventarioResultado> DeleteInventarioDetalleTPF(int idinventariodetalle, string usuario);
        Task<Inventario.InventarioResultado> PostInventarioDetalleTPF(Inventario.InsertarSoloInventarioDetalle inventario);
    }
}
