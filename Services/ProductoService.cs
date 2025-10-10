using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IRepository<Producto> _productoRepository;
        private readonly IRepository<Stock> _stockRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<CategoriaProducto> _categoriaProductoRepository;
        private readonly IRepository<ItemPedido> _itemPedidoRepository;

        public ProductoService(
            IRepository<Producto> productoRepository,
            IRepository<Stock> stockRepository,
            IRepository<Categoria> categoriaRepository,
            IRepository<CategoriaProducto> categoriaProductoRepository,
            IRepository<ItemPedido> itemPedidoRepository)
        {
            _productoRepository = productoRepository;
            _stockRepository = stockRepository;
            _categoriaRepository = categoriaRepository;
            _categoriaProductoRepository = categoriaProductoRepository;
            _itemPedidoRepository = itemPedidoRepository;
        }

        // OPERACIONES BÁSICAS
        public async Task<Producto> CreateProductoAsync(Producto producto, Stock stock)
        {
            // Primero crear el Stock
            await _stockRepository.AddAsync(stock);
            await _stockRepository.SaveChangesAsync();

            // Luego crear el Producto
            producto.StockIdStock = stock.idstock;
            await _productoRepository.AddAsync(producto);
            await _productoRepository.SaveChangesAsync();

            return producto;
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto != null)
            {
                producto.Stock = await _stockRepository.GetByIdAsync(producto.StockIdStock);
            }
            return producto;
        }

        public async Task<Producto> GetProductoWithDetailsAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null) return null;

            // Cargar stock
            producto.Stock = await _stockRepository.GetByIdAsync(producto.StockIdStock);

            // Cargar categorías
            var categoriasProducto = await _categoriaProductoRepository.FindAsync(cp => cp.ProductoIdProducto == id);
            var categorias = new List<Categoria>();

            foreach (var cp in categoriasProducto)
            {
                var categoria = await _categoriaRepository.GetByIdAsync(cp.CategoriaIdCategoria);
                if (categoria != null)
                {
                    categorias.Add(categoria);
                }
            }

            // Usar dynamic para agregar categorías (opcional, o crear DTO)
            return producto;
        }

        public async Task<IEnumerable<Producto>> GetAllProductosAsync()
        {
            var productos = await _productoRepository.GetAllAsync();
            foreach (var producto in productos)
            {
                producto.Stock = await _stockRepository.GetByIdAsync(producto.StockIdStock);
            }
            return productos;
        }

        public async Task<IEnumerable<Producto>> GetProductosByNombreAsync(string nombre)
        {
            var productos = await _productoRepository.FindAsync(p => p.nombre.Contains(nombre));
            foreach (var producto in productos)
            {
                producto.Stock = await _stockRepository.GetByIdAsync(producto.StockIdStock);
            }
            return productos;
        }

        public async Task<bool> UpdateProductoAsync(Producto producto)
        {
            var existingProducto = await _productoRepository.GetByIdAsync(producto.idproducto);
            if (existingProducto == null) return false;

            _productoRepository.Update(producto);
            return await _productoRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null) return false;

            // Verificar si el producto tiene pedidos
            var itemsPedido = await _itemPedidoRepository.FindAsync(ip => ip.ProductoIdProducto == id);
            if (itemsPedido.Any())
            {
                throw new Exception("No se puede eliminar el producto porque tiene pedidos asociados");
            }

            _productoRepository.Remove(producto);

            // Opcional: eliminar el stock asociado
            var stock = await _stockRepository.GetByIdAsync(producto.StockIdStock);
            if (stock != null)
            {
                _stockRepository.Remove(stock);
            }

            return await _productoRepository.SaveChangesAsync();
        }

        // BÚSQUEDAS Y FILTROS
        public async Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int categoriaId)
        {
            var categoriasProducto = await _categoriaProductoRepository.FindAsync(cp => cp.CategoriaIdCategoria == categoriaId);
            var productos = new List<Producto>();

            foreach (var cp in categoriasProducto)
            {
                var producto = await GetProductoByIdAsync(cp.ProductoIdProducto);
                if (producto != null)
                {
                    productos.Add(producto);
                }
            }

            return productos;
        }

        public async Task<IEnumerable<Producto>> GetProductosByComercioAsync(int comercioId)
        {
            // Esta implementación depende de cómo relates productos con comercios
            // Puede ser through categorías o through una relación directa
            var productos = await _productoRepository.FindAsync(p => true); // Placeholder
            return productos.Where(p => p.Stock != null); // Ejemplo básico
        }

        public async Task<IEnumerable<Producto>> GetProductosEnOfertaAsync()
        {
            var productos = await _productoRepository.FindAsync(p => p.oferta == true);
            foreach (var producto in productos)
            {
                producto.Stock = await _stockRepository.GetByIdAsync(producto.StockIdStock);
            }
            return productos;
        }

        public async Task<IEnumerable<Producto>> GetProductosByPrecioRangeAsync(decimal minPrecio, decimal maxPrecio)
        {
            var productos = await _productoRepository.FindAsync(p => p.precioUnitario >= minPrecio && p.precioUnitario <= maxPrecio);
            foreach (var producto in productos)
            {
                producto.Stock = await _stockRepository.GetByIdAsync(producto.StockIdStock);
            }
            return productos;
        }

        // GESTIÓN DE STOCK Y PRECIOS
        public async Task<bool> UpdateProductoStockAsync(int productoId, int cantidad)
        {
            var producto = await GetProductoByIdAsync(productoId);
            if (producto == null || producto.Stock == null) return false;

            producto.Stock.stock = cantidad;
            _stockRepository.Update(producto.Stock);
            return await _stockRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateProductoPrecioAsync(int productoId, decimal nuevoPrecio)
        {
            var producto = await _productoRepository.GetByIdAsync(productoId);
            if (producto == null) return false;

            producto.precioUnitario = nuevoPrecio;
            _productoRepository.Update(producto);
            return await _productoRepository.SaveChangesAsync();
        }

        public async Task<bool> SetProductoOfertaAsync(int productoId, bool enOferta)
        {
            var producto = await _productoRepository.GetByIdAsync(productoId);
            if (producto == null) return false;

            producto.oferta = enOferta;
            _productoRepository.Update(producto);
            return await _productoRepository.SaveChangesAsync();
        }

        public async Task<int> GetStockDisponibleAsync(int productoId)
        {
            var producto = await GetProductoByIdAsync(productoId);
            if (producto == null || producto.Stock == null) return 0;

            return producto.Stock.stockIlimitado ? int.MaxValue : producto.Stock.stock;
        }

        // CATEGORÍAS
        public async Task<bool> AddCategoriaToProductoAsync(int productoId, int categoriaId)
        {
            // Verificar si ya existe la relación
            var existingRelation = await _categoriaProductoRepository.FindAsync(cp =>
                cp.ProductoIdProducto == productoId && cp.CategoriaIdCategoria == categoriaId);

            if (existingRelation.Any()) return true; // Ya existe, no hay problema

            var categoriaProducto = new CategoriaProducto
            {
                ProductoIdProducto = productoId,
                CategoriaIdCategoria = categoriaId
            };

            await _categoriaProductoRepository.AddAsync(categoriaProducto);
            return await _categoriaProductoRepository.SaveChangesAsync();
        }

        public async Task<bool> RemoveCategoriaFromProductoAsync(int productoId, int categoriaId)
        {
            var categoriasProducto = await _categoriaProductoRepository.FindAsync(cp =>
                cp.ProductoIdProducto == productoId && cp.CategoriaIdCategoria == categoriaId);

            var categoriaProducto = categoriasProducto.FirstOrDefault();
            if (categoriaProducto == null) return false;

            _categoriaProductoRepository.Remove(categoriaProducto);
            return await _categoriaProductoRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasByProductoAsync(int productoId)
        {
            var categoriasProducto = await _categoriaProductoRepository.FindAsync(cp => cp.ProductoIdProducto == productoId);
            var categorias = new List<Categoria>();

            foreach (var cp in categoriasProducto)
            {
                var categoria = await _categoriaRepository.GetByIdAsync(cp.CategoriaIdCategoria);
                if (categoria != null)
                {
                    categorias.Add(categoria);
                }
            }

            return categorias;
        }
    }
}