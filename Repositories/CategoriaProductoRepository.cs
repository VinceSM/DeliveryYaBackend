using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Repositories
{
    public class CategoriaProductoRepository : ICategoriaProductoRepository
    {
        private readonly AppDbContext _context;

        public CategoriaProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int idCategoria)
        {
            return await _context.CategoriaProductos
                .Where(cp => cp.CategoriaIdCategoria == idCategoria)
                .Include(cp => cp.Producto)
                .Select(cp => cp.Producto!)
                .Where(p => p.deletedAt == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetProductosByNombreAsync(string nombre)
        {
            return await _context.Productos
                .Where(p => p.nombre!.ToLower().Contains(nombre.ToLower()) && p.deletedAt == null)
                .ToListAsync();
        }

        public async Task<Producto?> GetProductoByIdAsync(int idProducto)
        {
            return await _context.Productos
                .FirstOrDefaultAsync(p => p.idproducto == idProducto && p.deletedAt == null);
        }

        public async Task<Producto> CreateProductoAsync(Producto producto, int idCategoria)
        {
            producto.createdAt = DateTime.UtcNow;

            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            var categoriaProducto = new CategoriaProducto
            {
                CategoriaIdCategoria = idCategoria,
                ProductoIdProducto = producto.idproducto
            };

            await _context.CategoriaProductos.AddAsync(categoriaProducto);
            await _context.SaveChangesAsync();

            return producto;
        }

        public async Task<Producto> UpdateProductoAsync(Producto producto)
        {
            producto.updatedAt = DateTime.UtcNow;
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> DeleteProductoAsync(int idProducto)
        {
            var producto = await _context.Productos.FindAsync(idProducto);
            if (producto == null) return false;

            producto.deletedAt = DateTime.UtcNow;
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
