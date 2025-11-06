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
        private readonly ICategoriaProductoRepository _categoriaProductoRepository;
        private readonly IComercioCategoriaRepository _comercioCategoriaRepository;
        private readonly IMapper _mapper;

        public ProductoService(
            IProductoRepository productoRepository,
            ICategoriaProductoRepository categoriaProductoRepository,
            IComercioCategoriaRepository comercioCategoriaRepository,
            IMapper mapper)
        {
            _productoRepository = productoRepository;
            _categoriaProductoRepository = categoriaProductoRepository;
            _comercioCategoriaRepository = comercioCategoriaRepository;
            _mapper = mapper;
        }

        // ✅ Crear producto y asociar con categoría y comercio
        public async Task<ProductoResponse> CreateAsync(CreateProductoRequest request)
        {
            if (request.CategoriaId == 0)
                throw new ArgumentException("Debe especificarse una categoría para el producto.");

            // 1️⃣ Mapear y crear el producto
            var producto = _mapper.Map<Producto>(request);
            producto.createdAt = DateTime.UtcNow;

            var productoCreado = await _categoriaProductoRepository.CreateProductoAsync(producto, request.CategoriaId);

            // 2️⃣ Asociar la categoría al comercio (si aplica)
            if (request.ComercioId > 0)
            {
                // Previene duplicados, el repo ya puede chequear internamente si existe
                await _comercioCategoriaRepository.AddCategoriaToComercioAsync(request.ComercioId, request.CategoriaId);
            }

            // 3️⃣ Devolver DTO
            return _mapper.Map<ProductoResponse>(productoCreado);
        }

        // ✏️ Actualizar producto
        public async Task<ProductoResponse?> UpdateAsync(UpdateProductoRequest request)
        {
            var producto = await _productoRepository.GetByIdAsync(request.IdProducto);
            if (producto == null || producto.deletedAt != null)
                return null;

            _mapper.Map(request, producto);
            producto.updatedAt = DateTime.UtcNow;

            _productoRepository.Update(producto);
            await _productoRepository.SaveChangesAsync();

            return _mapper.Map<ProductoResponse>(producto);
        }

        // 🗑️ Borrado lógico
        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null || producto.deletedAt != null)
                return false;

            producto.deletedAt = DateTime.UtcNow;
            _productoRepository.Update(producto);
            await _productoRepository.SaveChangesAsync();

            return true;
        }

        // 📄 Obtener producto por ID
        public async Task<ProductoResponse?> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null || producto.deletedAt != null)
                return null;

            return _mapper.Map<ProductoResponse>(producto);
        }

        // 📜 Listar productos activos
        public async Task<IEnumerable<ProductoResponse>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductoResponse>>(productos.Where(p => p.deletedAt == null));
        }

        // 🔎 Obtener productos por categoría
        public async Task<IEnumerable<ProductoResponse>> GetByCategoriaAsync(int categoriaId)
        {
            var productos = await _productoRepository.GetProductosPorCategoriaAsync(categoriaId);
            return _mapper.Map<IEnumerable<ProductoResponse>>(productos);
        }

        // 🔍 Buscar por nombre
        public async Task<IEnumerable<ProductoResponse>> SearchByNameAsync(string nombre)
        {
            var productos = await _productoRepository.SearchByNameAsync(nombre);
            return _mapper.Map<IEnumerable<ProductoResponse>>(productos);
        }
    }


}
