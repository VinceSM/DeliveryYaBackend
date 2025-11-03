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

        public ProductoService(
            IProductoRepository productoRepository,
            IRepository<CategoriaProducto> categoriaProductoRepository,
            IMapper mapper)
        {
            _productoRepository = productoRepository;
            _categoriaProductoRepository = categoriaProductoRepository;
            _mapper = mapper;
        }

        // Crear producto y asignar categoría
        public async Task<ProductoResponse> CreateAsync(CreateProductoRequest request)
        {
            var producto = _mapper.Map<Producto>(request);
            producto.createdAt = DateTime.UtcNow;

            await _productoRepository.AddAsync(producto);
            await _productoRepository.SaveChangesAsync();

            if (request.CategoriaId != 0)
            {
                var categoriaProducto = new CategoriaProducto
                {
                    CategoriaIdCategoria = request.CategoriaId,
                    ProductoIdProducto = producto.idproducto
                };
                await _categoriaProductoRepository.AddAsync(categoriaProducto);
                await _categoriaProductoRepository.SaveChangesAsync();
            }

            return _mapper.Map<ProductoResponse>(producto);
        }

        // Actualizar producto
        public async Task<ProductoResponse?> UpdateAsync(UpdateProductoRequest request)
        {
            var producto = await _productoRepository.GetByIdAsync(request.IdProducto);
            if (producto == null) return null;

            _mapper.Map(request, producto);
            producto.updatedAt = DateTime.UtcNow;

            _productoRepository.Update(producto);
            await _productoRepository.SaveChangesAsync();

            return _mapper.Map<ProductoResponse>(producto);
        }

        // Eliminar producto (soft delete)
        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null) return false;

            producto.deletedAt = DateTime.UtcNow;
            _productoRepository.Update(producto);
            await _productoRepository.SaveChangesAsync();

            return true;
        }

        // Obtener producto por ID
        public async Task<ProductoResponse?> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null || producto.deletedAt != null) return null;

            return _mapper.Map<ProductoResponse>(producto);
        }

        // Obtener todos los productos activos
        public async Task<IEnumerable<ProductoResponse>> GetAllAsync()
        {
            var productos = (await _productoRepository.GetAllAsync())
                             .Where(p => p.deletedAt == null);
            return _mapper.Map<IEnumerable<ProductoResponse>>(productos);
        }

        // Obtener productos por categoría
        public async Task<IEnumerable<ProductoResponse>> GetByCategoriaAsync(int categoriaId)
        {
            var relaciones = await _categoriaProductoRepository.FindAsync(cp => cp.CategoriaIdCategoria == categoriaId);
            var productos = new List<Producto>();

            foreach (var rel in relaciones)
            {
                var producto = await _productoRepository.GetByIdAsync(rel.ProductoIdProducto);
                if (producto != null && producto.deletedAt == null)
                    productos.Add(producto);
            }

            return _mapper.Map<IEnumerable<ProductoResponse>>(productos);
        }

        // Buscar productos por nombre
        public async Task<IEnumerable<ProductoResponse>> SearchByNameAsync(string nombre)
        {
            var productos = await _productoRepository.SearchByNameAsync(nombre);
            return _mapper.Map<IEnumerable<ProductoResponse>>(productos);
        }
    }

}
