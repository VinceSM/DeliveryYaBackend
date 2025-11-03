using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class CategoriaProductoService : ICategoriaProductoService
    {
        private readonly ICategoriaProductoRepository _repository;

        public CategoriaProductoService(ICategoriaProductoRepository repository)
        {
            _repository = repository;
        }

        // 🔹 Crear un producto dentro de una categoría
        public async Task<ProductoResponse> CrearProductoAsync(CreateProductoRequest request, int idCategoria)
        {
            var producto = new Producto
            {
                nombre = request.Nombre,
                descripcion = request.Descripcion,
                fotoPortada = request.FotoPortada,
                unidadMedida = request.UnidadMedida,
                precioUnitario = request.PrecioUnitario,
                oferta = request.Oferta,
                stock = request.Stock,
            };

            var creado = await _repository.CreateProductoAsync(producto, idCategoria);

            return new ProductoResponse
            {
                IdProducto = creado.idproducto,
                Nombre = creado.nombre,
                Descripcion = creado.descripcion,
                PrecioUnitario = creado.precioUnitario,
                UnidadMedida = creado.unidadMedida,
                Oferta = creado.oferta,
                Stock = creado.stock,
                FotoPortada = creado.fotoPortada,
                CreatedAt = creado.createdAt
            };
        }

        // 🔹 Obtener todos los productos por categoría
        public async Task<IEnumerable<ProductoResponse>> GetProductosPorCategoriaAsync(int idCategoria)
        {
            var productos = await _repository.GetProductosByCategoriaAsync(idCategoria);

            return productos.Select(p => new ProductoResponse
            {
                IdProducto = p.idproducto,
                Nombre = p.nombre,
                Descripcion = p.descripcion,
                PrecioUnitario = p.precioUnitario,
                UnidadMedida = p.unidadMedida,
                Oferta = p.oferta,
                Stock = p.stock,
                FotoPortada = p.fotoPortada,
                CreatedAt = p.createdAt
            });
        }

        // 🔹 Buscar productos por nombre
        public async Task<IEnumerable<ProductoResponse>> GetProductosPorNombreAsync(string nombre)
        {
            var productos = await _repository.GetProductosByNombreAsync(nombre);

            return productos.Select(p => new ProductoResponse
            {
                IdProducto = p.idproducto,
                Nombre = p.nombre,
                Descripcion = p.descripcion,
                PrecioUnitario = p.precioUnitario,
                UnidadMedida = p.unidadMedida,
                Oferta = p.oferta,
                Stock = p.stock,
                FotoPortada = p.fotoPortada,
                CreatedAt = p.createdAt
            });
        }

        // 🔹 Obtener un producto por ID
        public async Task<ProductoResponse?> GetProductoPorIdAsync(int idProducto)
        {
            var producto = await _repository.GetProductoByIdAsync(idProducto);
            if (producto == null) return null;

            return new ProductoResponse
            {
                IdProducto = producto.idproducto,
                Nombre = producto.nombre,
                Descripcion = producto.descripcion,
                PrecioUnitario = producto.precioUnitario,
                UnidadMedida = producto.unidadMedida,
                Oferta = producto.oferta,
                Stock = producto.stock,
                FotoPortada = producto.fotoPortada,
                UpdatedAt = producto.updatedAt
            };
        }

        // 🔹 Actualizar producto
        public async Task<ProductoResponse?> ActualizarProductoAsync(int idProducto, UpdateProductoRequest request)
        {
            var productoExistente = await _repository.GetProductoByIdAsync(idProducto);
            if (productoExistente == null) return null;

            productoExistente.nombre = request.Nombre;
            productoExistente.descripcion = request.Descripcion;
            productoExistente.precioUnitario = request.PrecioUnitario;
            productoExistente.unidadMedida = request.UnidadMedida;
            productoExistente.oferta = request.Oferta;
            productoExistente.fotoPortada = request.FotoPortada;
            productoExistente.stock = request.Stock;

            var actualizado = await _repository.UpdateProductoAsync(productoExistente);

            return new ProductoResponse
            {
                IdProducto = actualizado.idproducto,
                Nombre = actualizado.nombre,
                Descripcion = actualizado.descripcion,
                PrecioUnitario = actualizado.precioUnitario,
                UnidadMedida = actualizado.unidadMedida,
                Oferta = actualizado.oferta,
                Stock = actualizado.stock,
                FotoPortada = actualizado.fotoPortada,
                UpdatedAt = actualizado.updatedAt
            };
        }

        // 🔹 Eliminar producto (lógico)
        public async Task<bool> EliminarProductoAsync(int idProducto)
        {
            return await _repository.DeleteProductoAsync(idProducto);
        }
    }
}
