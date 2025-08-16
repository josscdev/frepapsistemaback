using Microsoft.Win32;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Inventario;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Perfiles;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Inventario
{
    public class InventarioServices : IInventarioServices
    {

        private readonly IInventarioRepository _inventarioRepository;

        private readonly INotificationService _notifier;


        public InventarioServices(IInventarioRepository inventarioRepository, INotificationService notifier)
        {
            _inventarioRepository = inventarioRepository;
            _notifier = notifier;

        }

        public async Task<List<Inventario.ListarInventario>> GetInventario(Inventario.InventarioFiltroListar request)
        {
            var respuesta = await _inventarioRepository.GetInventario(request);
            //await _notifier.NotificarATodos($"Nuevo producto:");

            return  respuesta;
        }

        public async Task<Inventario.InventarioResultado> PostInventarioMasivo(Inventario.InsertarInventario inventario)
        {
            var respuesta = await _inventarioRepository.PostInventarioMasivo(inventario);
            return respuesta;
        }

        public async Task<Inventario.InventarioResultado> DeleteInventario(int idinventario, string usuario)
        {
            var respuesta = await _inventarioRepository.DeleteInventario(idinventario, usuario);
            return respuesta;
        }

        public async Task<List<Inventario.ListarInventarioDetalle>> GetInventarioDetalle(int idinventario)
        {
            var respuesta = await _inventarioRepository.GetInventarioDetalle(idinventario);
            return respuesta;
        }

        public async Task<Inventario.InventarioResultado> DeleteInventarioDetalle(int idinventariodetalle, string usuario)
        {
            var respuesta = await _inventarioRepository.DeleteInventarioDetalle(idinventariodetalle, usuario);
            return respuesta;
        }

        public async Task<Inventario.InventarioResultado> PostInventarioDetalle(Inventario.InsertarSoloInventarioDetalle inventario)
        {
            var respuesta = await _inventarioRepository.PostInventarioDetalle(inventario);
            return respuesta;
        }
    }


      
    
}
