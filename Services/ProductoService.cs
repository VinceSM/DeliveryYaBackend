using AutoMapper;
using DeliveryYaBackend.Data;
using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IRepository<CategoriaProducto> _categoriaProductoRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ProductoService(
            IProductoRepository productoRepository,
            IRepository<CategoriaProducto> categoriaProductoRepository,
            IMapper mapper,
            AppDbContext context)
        {
            _productoRepository = productoRepository;
            _categoriaProductoRepository = categoriaProductoRepository;
            _mapper = mapper;
            _context = context;
        }

        // ✅ Crear producto y asociarlo a una categoría
        public async Task<ProductoResponse> CreateAsync(CreateProductoRequest request)
        {
            var producto = _mapper.Map<Producto>(request);
            producto.createdAt = DateTime.UtcNow;

            await _productoRepository.AddAsync(producto);
            await _productoRepository.SaveChangesAsync();

            // Asociar con la categoría
            var categoriaProducto = new CategoriaProducto
            {
                CategoriaIdCategoria = request.CategoriaId,
                ProductoIdProducto = producto.idproducto
            };

            await _categoriaProductoRepository.AddAsync(categoriaProducto);
            await _categoriaProductoRepository.SaveChangesAsync();

            return _mapper.Map<ProductoResponse>(producto);
        }

        // ✅ Actualizar producto
        public async Task<ProductoResponse?> UpdateAsync(UpdateProductoRequest request)
        {
            var producto = await _productoRepository.GetByIdAsync(request.IdProducto);
            if (producto == null)
                return null;

            _mapper.Map(request, producto);
            producto.updatedAt = DateTime.UtcNow;

            _productoRepository.Update(producto);
            await _productoRepository.SaveChangesAsync();

            return _mapper.Map<ProductoResponse>(producto);
        }

        // ✅ Eliminar producto
        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null)
                return false;

            producto.deletedAt = DateTime.UtcNow;
            _productoRepository.Update(producto);
            await _productoRepository.SaveChangesAsync();

            return true;
        }

        // ✅ Obtener producto por ID
        public async Task<ProductoResponse?> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null || producto.deletedAt != null)
                return null;

            return _mapper.Map<ProductoResponse>(producto);
        }

        public async Task<IEnumerable<ProductoResponse>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllAsync();
            productos = productos.Where(p => p.deletedAt == null);
            return _mapper.Map<IEnumerable<ProductoResponse>>(productos);
        }

        public async Task<IEnumerable<ProductoResponse>> GetByCategoriaAsync(int categoriaId)
        {
            var productos = await _productoRepository.GetProductosPorCategoriaAsync(categoriaId);
            return _mapper.Map<IEnumerable<ProductoResponse>>(productos);
        }

        public async Task<IEnumerable<ProductoResponse>> SearchByNameAsync(string nombre)
        {
            var productos = await _productoRepository.SearchByNameAsync(nombre);
            return _mapper.Map<IEnumerable<ProductoResponse>>(productos);
        }

    }
}
