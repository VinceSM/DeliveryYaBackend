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
                nombre = request.nombre,
                descripcion = request.descripcion,
                fotoPortada = request.fotoPortada,
                unidadMedida = request.unidadMedida,
                precioUnitario = request.precioUnitario,
                oferta = request.oferta,
                stock = request.stock,
            };

            var creado = await _repository.CreateProductoAsync(producto, idCategoria);

            return new ProductoResponse
            {
                idproducto = creado.idproducto,
                nombre = creado.nombre,
                descripcion = creado.descripcion,
                precioUnitario = creado.precioUnitario,
                unidadMedida = creado.unidadMedida,
                oferta = creado.oferta,
                stock = creado.stock,
                fotoPortada = creado.fotoPortada,
                createdAt = creado.createdAt
            };
        }

        // 🔹 Obtener todos los productos por categoría
        public async Task<IEnumerable<ProductoResponse>> GetProductosPorCategoriaAsync(int idCategoria)
        {
            var productos = await _repository.GetProductosByCategoriaAsync(idCategoria);

            return productos.Select(p => new ProductoResponse
            {
                idproducto = p.idproducto,
                nombre = p.nombre,
                descripcion = p.descripcion,
                precioUnitario = p.precioUnitario,
                unidadMedida = p.unidadMedida,
                oferta = p.oferta,
                stock = p.stock,
                fotoPortada = p.fotoPortada,
                createdAt = p.createdAt
            });
        }

        // 🔹 Buscar productos por nombre
        public async Task<IEnumerable<ProductoResponse>> GetProductosPorNombreAsync(string nombre)
        {
            var productos = await _repository.GetProductosByNombreAsync(nombre);

            return productos.Select(p => new ProductoResponse
            {
                idproducto = p.idproducto,
                nombre = p.nombre,
                descripcion = p.descripcion,
                precioUnitario = p.precioUnitario,
                unidadMedida = p.unidadMedida,
                oferta = p.oferta,
                stock = p.stock,
                fotoPortada = p.fotoPortada,
                createdAt = p.createdAt
            });
        }

        // 🔹 Obtener un producto por ID
        public async Task<ProductoResponse?> GetProductoPorIdAsync(int idProducto)
        {
            var producto = await _repository.GetProductoByIdAsync(idProducto);
            if (producto == null) return null;

            return new ProductoResponse
            {
                idproducto = producto.idproducto,
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                precioUnitario = producto.precioUnitario,
                unidadMedida = producto.unidadMedida,
                oferta = producto.oferta,
                stock = producto.stock,
                fotoPortada = producto.fotoPortada,
                updatedAt = producto.updatedAt
            };
        }

        // 🔹 Actualizar producto
        public async Task<ProductoResponse?> ActualizarProductoAsync(int idProducto, UpdateProductoRequest request)
        {
            var productoExistente = await _repository.GetProductoByIdAsync(idProducto);
            if (productoExistente == null) return null;

            productoExistente.nombre = request.nombre;
            productoExistente.descripcion = request.descripcion;
            productoExistente.precioUnitario = request.precioUnitario;
            productoExistente.unidadMedida = request.unidadMedida;
            productoExistente.oferta = request.oferta;
            productoExistente.fotoPortada = request.fotoPortada;
            productoExistente.stock = request.stock;

            var actualizado = await _repository.UpdateProductoAsync(productoExistente);

            return new ProductoResponse
            {
                idproducto = actualizado.idproducto,
                nombre = actualizado.nombre,
                descripcion = actualizado.descripcion,
                precioUnitario = actualizado.precioUnitario,
                unidadMedida = actualizado.unidadMedida,
                oferta = actualizado.oferta,
                stock = actualizado.stock,
                fotoPortada = actualizado.fotoPortada,
                updatedAt = actualizado.updatedAt
            };
        }

        // 🔹 Eliminar producto (lógico)
        public async Task<bool> EliminarProductoAsync(int idProducto)
        {
            return await _repository.DeleteProductoAsync(idProducto);
        }
    }
}
