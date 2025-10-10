using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class StockService : IStockService
    {
        private readonly IRepository<Stock> _stockRepository;
        private readonly IRepository<Producto> _productoRepository;

        public StockService(
            IRepository<Stock> stockRepository,
            IRepository<Producto> productoRepository)
        {
            _stockRepository = stockRepository;
            _productoRepository = productoRepository;
        }

        // OPERACIONES BÁSICAS
        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _stockRepository.AddAsync(stock);
            await _stockRepository.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> GetStockByIdAsync(int id)
        {
            return await _stockRepository.GetByIdAsync(id);
        }

        public async Task<Stock> GetStockByProductoIdAsync(int productoId)
        {
            var producto = await _productoRepository.GetByIdAsync(productoId);
            if (producto == null) return null;

            return await _stockRepository.GetByIdAsync(producto.StockIdStock);
        }

        public async Task<IEnumerable<Stock>> GetAllStocksAsync()
        {
            return await _stockRepository.GetAllAsync();
        }

        public async Task<bool> UpdateStockAsync(Stock stock)
        {
            var existingStock = await _stockRepository.GetByIdAsync(stock.idstock);
            if (existingStock == null) return false;

            _stockRepository.Update(stock);
            return await _stockRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null) return false;

            // Verificar si el stock está siendo usado por algún producto
            var productos = await _productoRepository.FindAsync(p => p.StockIdStock == id);
            if (productos.Any())
            {
                throw new Exception("No se puede eliminar el stock porque está asociado a productos");
            }

            _stockRepository.Remove(stock);
            return await _stockRepository.SaveChangesAsync();
        }

        // GESTIÓN DE CANTIDADES DE STOCK
        public async Task<bool> UpdateCantidadStockAsync(int stockId, int nuevaCantidad)
        {
            var stock = await _stockRepository.GetByIdAsync(stockId);
            if (stock == null) return false;

            stock.stock = nuevaCantidad;
            _stockRepository.Update(stock);
            return await _stockRepository.SaveChangesAsync();
        }

        public async Task<bool> IncrementarStockAsync(int stockId, int cantidad)
        {
            var stock = await _stockRepository.GetByIdAsync(stockId);
            if (stock == null) return false;

            stock.stock += cantidad;
            _stockRepository.Update(stock);
            return await _stockRepository.SaveChangesAsync();
        }

        public async Task<bool> DecrementarStockAsync(int stockId, int cantidad)
        {
            var stock = await _stockRepository.GetByIdAsync(stockId);
            if (stock == null) return false;

            // Verificar que haya stock suficiente
            if (stock.stock < cantidad && !stock.stockIlimitado)
            {
                throw new Exception("Stock insuficiente para realizar esta operación");
            }

            if (!stock.stockIlimitado)
            {
                stock.stock -= cantidad;
            }

            _stockRepository.Update(stock);
            return await _stockRepository.SaveChangesAsync();
        }

        public async Task<bool> SetStockIlimitadoAsync(int stockId, bool ilimitado)
        {
            var stock = await _stockRepository.GetByIdAsync(stockId);
            if (stock == null) return false;

            stock.stockIlimitado = ilimitado;
            _stockRepository.Update(stock);
            return await _stockRepository.SaveChangesAsync();
        }

        // VALIDACIONES Y VERIFICACIONES
        public async Task<bool> VerificarStockSuficienteAsync(int stockId, int cantidadRequerida)
        {
            var stock = await _stockRepository.GetByIdAsync(stockId);
            if (stock == null) return false;

            return stock.stockIlimitado || stock.stock >= cantidadRequerida;
        }

        public async Task<int> GetStockDisponibleAsync(int stockId)
        {
            var stock = await _stockRepository.GetByIdAsync(stockId);
            if (stock == null) return 0;

            return stock.stockIlimitado ? int.MaxValue : stock.stock;
        }

        public async Task<bool> StockExistsForProductoAsync(int productoId)
        {
            var producto = await _productoRepository.GetByIdAsync(productoId);
            if (producto == null) return false;

            var stock = await _stockRepository.GetByIdAsync(producto.StockIdStock);
            return stock != null;
        }

        // OPERACIONES POR PRODUCTO
        public async Task<bool> ActualizarStockPorProductoAsync(int productoId, int nuevaCantidad)
        {
            var stock = await GetStockByProductoIdAsync(productoId);
            if (stock == null) return false;

            return await UpdateCantidadStockAsync(stock.idstock, nuevaCantidad);
        }

        public async Task<bool> IncrementarStockPorProductoAsync(int productoId, int cantidad)
        {
            var stock = await GetStockByProductoIdAsync(productoId);
            if (stock == null) return false;

            return await IncrementarStockAsync(stock.idstock, cantidad);
        }

        public async Task<bool> DecrementarStockPorProductoAsync(int productoId, int cantidad)
        {
            var stock = await GetStockByProductoIdAsync(productoId);
            if (stock == null) return false;

            return await DecrementarStockAsync(stock.idstock, cantidad);
        }

        public async Task<int> GetStockDisponiblePorProductoAsync(int productoId)
        {
            var stock = await GetStockByProductoIdAsync(productoId);
            if (stock == null) return 0;

            return await GetStockDisponibleAsync(stock.idstock);
        }

        // REPORTES Y ESTADÍSTICAS
        public async Task<IEnumerable<Stock>> GetStocksBajosAsync(int nivelMinimo = 10)
        {
            var stocks = await _stockRepository.FindAsync(s =>
                !s.stockIlimitado && s.stock <= nivelMinimo && s.stock > 0);
            return stocks;
        }

        public async Task<IEnumerable<Stock>> GetStocksAgotadosAsync()
        {
            var stocks = await _stockRepository.FindAsync(s =>
                !s.stockIlimitado && s.stock <= 0);
            return stocks;
        }

        public async Task<int> GetTotalProductosConStockAsync()
        {
            var stocks = await _stockRepository.FindAsync(s =>
                s.stockIlimitado || s.stock > 0);
            return stocks.Count();
        }

        public async Task<int> GetTotalProductosSinStockAsync()
        {
            var stocks = await _stockRepository.FindAsync(s =>
                !s.stockIlimitado && s.stock <= 0);
            return stocks.Count();
        }
    }
}