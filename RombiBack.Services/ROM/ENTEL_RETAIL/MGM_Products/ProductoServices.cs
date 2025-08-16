using RombiBack.Abstraction;
using RombiBack.Repository;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Mappers;
using AutoMapper;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Products
{
    public class ProductoServices : IProductoServices
    {
        private readonly IProductoRepository _productRepository;

        private readonly IMapper _mapper;

        public ProductoServices(IProductoRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<ProductoDTO> Add()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductoDTO>> ObtenerTodo()
        {
            var products = await _productRepository.ObtenerTodo();
            return products.Select(ProductMapper.MapToProductListDTO).ToList();

            //var productos = await _productRepository.ObtenerTodo();
            //return _mapper.Map<List<ProductoDTO>>(productos);
        }



        //public async Task<List<Producto>> ObtenerTodo()
        //{
        //    return await _productRepository.ObtenerTodo();
        //}



    }
}