using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.Services.Interfaces;

public class CategoriaProductoService : ICategoriaProductoService
{
    private readonly IProductoService _productoService;

    public CategoriaProductoService(IProductoService productoService)
    {
        _productoService = productoService;
    }

    // Crear producto dentro de una categoría usando ProductoService
    public async Task<ProductoResponse> CrearProductoAsync(CreateProductoRequest request, int idCategoria)
    {
        // Asignamos la categoría en el request
        request.CategoriaId = idCategoria;

        // Delegamos la creación al ProductoService
        return await _productoService.CreateAsync(request);
    }

    // Obtener productos por categoría
    public async Task<IEnumerable<ProductoResponse>> GetProductosPorCategoriaAsync(int idCategoria)
    {
        return await _productoService.GetByCategoriaAsync(idCategoria);
    }

    // Buscar productos por nombre
    public async Task<IEnumerable<ProductoResponse>> GetProductosPorNombreAsync(string nombre)
    {
        return await _productoService.SearchByNameAsync(nombre);
    }

    // Obtener producto por ID
    public async Task<ProductoResponse?> GetProductoPorIdAsync(int idProducto)
    {
        return await _productoService.GetByIdAsync(idProducto);
    }

    // Actualizar producto
    public async Task<ProductoResponse?> ActualizarProductoAsync(int idProducto, UpdateProductoRequest request)
    {
        request.IdProducto = idProducto;
        return await _productoService.UpdateAsync(request);
    }

    // Eliminar producto
    public async Task<bool> EliminarProductoAsync(int idProducto)
    {
        return await _productoService.DeleteAsync(idProducto);
    }
}
