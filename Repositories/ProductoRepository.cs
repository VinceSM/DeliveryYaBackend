using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories;
using Microsoft.EntityFrameworkCore;

public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
{
    private readonly AppDbContext _context;

    public ProductoRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    // Obtener productos por categoría
    public async Task<IEnumerable<Producto>> GetProductosPorCategoriaAsync(int categoriaId)
    {
        var productos = await _context.CategoriaProductos
            .Include(cp => cp.Producto)
            .Where(cp => cp.CategoriaIdCategoria == categoriaId && cp.Producto!.deletedAt == null)
            .Select(cp => cp.Producto!)
            .ToListAsync();

        return productos;
    }

    // Buscar productos por nombre
    public async Task<IEnumerable<Producto>> SearchByNameAsync(string nombre)
    {
        return await _context.Productos
            .Where(p => p.deletedAt == null && p.nombre!.Contains(nombre))
            .ToListAsync();
    }
}