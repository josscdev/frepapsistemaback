using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Inventario;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_InventarioTPF;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_InventarioTPF
{
    public class InventarioTPFServices  : IInventarioTPFServices
    {
        private readonly IInventarioTPFRepository _inventarioTPFRepository;
        private readonly INotificationService _notifier;

        public InventarioTPFServices(IInventarioTPFRepository inventarioTPFRepository, INotificationService notifier)
        {
            _inventarioTPFRepository = inventarioTPFRepository;
            _notifier = notifier;
        }

        public async Task<List<Inventario.ListarInventario>> GetInventarioTPF(Inventario.InventarioFiltroListar request)
        {
            var respuesta = await _inventarioTPFRepository.GetInventarioTPF(request);
            //await _notifier.NotificarATodos($"Nuevo producto:");
            return respuesta;
        }
        public async Task<Inventario.InventarioResultado> PostInventarioMasivoTPF(Inventario.InsertarInventario inventario)
        {
            var respuesta = await _inventarioTPFRepository.PostInventarioMasivoTPF(inventario);
            return respuesta;
        }
        public async Task<Inventario.InventarioResultado> DeleteInventarioTPF(int idinventario, string usuario)
        {
            var respuesta = await _inventarioTPFRepository.DeleteInventarioTPF(idinventario, usuario);
            return respuesta;
        }
        public async Task<List<Inventario.ListarInventarioDetalle>> GetInventarioDetalleTPF(int idinventario)
        {
            var respuesta = await _inventarioTPFRepository.GetInventarioDetalleTPF(idinventario);
            return respuesta;
        }
        public async Task<Inventario.InventarioResultado> DeleteInventarioDetalleTPF(int idinventariodetalle, string usuario)
        {
            var respuesta = await _inventarioTPFRepository.DeleteInventarioDetalleTPF(idinventariodetalle, usuario);
            return respuesta;
        }
        public async Task<Inventario.InventarioResultado> PostInventarioDetalleTPF(Inventario.InsertarSoloInventarioDetalle inventario)
        {
            var respuesta = await _inventarioTPFRepository.PostInventarioDetalleTPF(inventario);
            return respuesta;
        }
    }
}
