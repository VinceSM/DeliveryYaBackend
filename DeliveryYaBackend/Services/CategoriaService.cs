using DeliveryYaBackend.Data.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Producto> _productoRepository;
        private readonly IRepository<Comercio> _comercioRepository;
        private readonly IRepository<CategoriaProducto> _categoriaProductoRepository;
        private readonly IRepository<ComercioCategoria> _comercioCategoriaRepository;

        public CategoriaService(
            IRepository<Categoria> categoriaRepository,
            IRepository<Producto> productoRepository,
            IRepository<Comercio> comercioRepository,
            IRepository<CategoriaProducto> categoriaProductoRepository,
            IRepository<ComercioCategoria> comercioCategoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _productoRepository = productoRepository;
            _comercioRepository = comercioRepository;
            _categoriaProductoRepository = categoriaProductoRepository;
            _comercioCategoriaRepository = comercioCategoriaRepository;
        }

        // OPERACIONES BÁSICAS
        public async Task<Categoria> CreateCategoriaAsync(Categoria categoria)
        {
            // Validar que no exista una categoría con el mismo nombre
            var existe = await CategoriaExistsAsync(categoria.nombre);
            if (existe)
            {
                throw new Exception("Ya existe una categoría con ese nombre");
            }

            await _categoriaRepository.AddAsync(categoria);
            await _categoriaRepository.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            return await _categoriaRepository.GetByIdAsync(id);
        }

        public async Task<Categoria> GetCategoriaByNombreAsync(string nombre)
        {
            var categorias = await _categoriaRepository.FindAsync(c => c.nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            return categorias.FirstOrDefault();
        }

        public async Task<IEnumerable<Categoria>> GetAllCategoriasAsync()
        {
            return await _categoriaRepository.GetAllAsync();
        }

        public async Task<bool> UpdateCategoriaAsync(Categoria categoria)
        {
            var existingCategoria = await _categoriaRepository.GetByIdAsync(categoria.idcategoria);
            if (existingCategoria == null) return false;

            // Validar que el nuevo nombre no exista (si cambió)
            if (!existingCategoria.nombre.Equals(categoria.nombre, StringComparison.OrdinalIgnoreCase))
            {
                var existe = await CategoriaExistsAsync(categoria.nombre);
                if (existe)
                {
                    throw new Exception("Ya existe una categoría con ese nombre");
                }
            }

            _categoriaRepository.Update(categoria);
            return await _categoriaRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null) return false;

            // Verificar si la categoría tiene productos asociados
            var tieneProductos = await CategoriaHasProductosAsync(id);
            if (tieneProductos)
            {
                throw new Exception("No se puede eliminar la categoría porque tiene productos asociados");
            }

            // Verificar si la categoría tiene comercios asociados
            var tieneComercios = await CategoriaHasComerciosAsync(id);
            if (tieneComercios)
            {
                throw new Exception("No se puede eliminar la categoría porque tiene comercios asociados");
            }

            _categoriaRepository.Remove(categoria);
            return await _categoriaRepository.SaveChangesAsync();
        }

        // RELACIÓN CON PRODUCTOS
        public async Task<bool> AddProductoToCategoriaAsync(int categoriaId, int productoId)
        {
            // Verificar si ya existe la relación
            var existingRelation = await _categoriaProductoRepository.FindAsync(cp =>
                cp.CategoriaIdCategoria == categoriaId && cp.ProductoIdProducto == productoId);

            if (existingRelation.Any()) return true;

            var categoriaProducto = new CategoriaProducto
            {
                CategoriaIdCategoria = categoriaId,
                ProductoIdProducto = productoId
            };

            await _categoriaProductoRepository.AddAsync(categoriaProducto);
            return await _categoriaProductoRepository.SaveChangesAsync();
        }

        public async Task<bool> RemoveProductoFromCategoriaAsync(int categoriaId, int productoId)
        {
            var relaciones = await _categoriaProductoRepository.FindAsync(cp =>
                cp.CategoriaIdCategoria == categoriaId && cp.ProductoIdProducto == productoId);

            var relacion = relaciones.FirstOrDefault();
            if (relacion == null) return false;

            _categoriaProductoRepository.Remove(relacion);
            return await _categoriaProductoRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int categoriaId)
        {
            var relaciones = await _categoriaProductoRepository.FindAsync(cp => cp.CategoriaIdCategoria == categoriaId);
            var productos = new List<Producto>();

            foreach (var relacion in relaciones)
            {
                var producto = await _productoRepository.GetByIdAsync(relacion.ProductoIdProducto);
                if (producto != null)
                {
                    productos.Add(producto);
                }
            }

            return productos;
        }

        public async Task<int> GetCantidadProductosByCategoriaAsync(int categoriaId)
        {
            var productos = await GetProductosByCategoriaAsync(categoriaId);
            return productos.Count();
        }

        // RELACIÓN CON COMERCIOS
        public async Task<bool> AddComercioToCategoriaAsync(int categoriaId, int comercioId)
        {
            var existingRelation = await _comercioCategoriaRepository.FindAsync(cc =>
                cc.CategoriaIdCategoria == categoriaId && cc.ComercioIdComercio == comercioId);

            if (existingRelation.Any()) return true;

            var comercioCategoria = new ComercioCategoria
            {
                CategoriaIdCategoria = categoriaId,
                ComercioIdComercio = comercioId
            };

            await _comercioCategoriaRepository.AddAsync(comercioCategoria);
            return await _comercioCategoriaRepository.SaveChangesAsync();
        }

        public async Task<bool> RemoveComercioFromCategoriaAsync(int categoriaId, int comercioId)
        {
            var relaciones = await _comercioCategoriaRepository.FindAsync(cc =>
                cc.CategoriaIdCategoria == categoriaId && cc.ComercioIdComercio == comercioId);

            var relacion = relaciones.FirstOrDefault();
            if (relacion == null) return false;

            _comercioCategoriaRepository.Remove(relacion);
            return await _comercioCategoriaRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comercio>> GetComerciosByCategoriaAsync(int categoriaId)
        {
            var relaciones = await _comercioCategoriaRepository.FindAsync(cc => cc.CategoriaIdCategoria == categoriaId);
            var comercios = new List<Comercio>();

            foreach (var relacion in relaciones)
            {
                var comercio = await _comercioRepository.GetByIdAsync(relacion.ComercioIdComercio);
                if (comercio != null)
                {
                    comercios.Add(comercio);
                }
            }

            return comercios;
        }

        public async Task<int> GetCantidadComerciosByCategoriaAsync(int categoriaId)
        {
            var comercios = await GetComerciosByCategoriaAsync(categoriaId);
            return comercios.Count();
        }

        // BÚSQUEDAS Y FILTROS
        public async Task<IEnumerable<Categoria>> GetCategoriasWithProductosAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            var categoriasConProductos = new List<Categoria>();

            foreach (var categoria in categorias)
            {
                var productos = await GetProductosByCategoriaAsync(categoria.idcategoria);
                if (productos.Any())
                {
                    // Usar dynamic o crear DTO para incluir productos
                    categoriasConProductos.Add(categoria);
                }
            }

            return categoriasConProductos;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasWithComerciosAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            var categoriasConComercios = new List<Categoria>();

            foreach (var categoria in categorias)
            {
                var comercios = await GetComerciosByCategoriaAsync(categoria.idcategoria);
                if (comercios.Any())
                {
                    categoriasConComercios.Add(categoria);
                }
            }

            return categoriasConComercios;
        }

        public async Task<IEnumerable<Categoria>> SearchCategoriasAsync(string searchTerm)
        {
            return await _categoriaRepository.FindAsync(c =>
                c.nombre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        // VALIDACIONES
        public async Task<bool> CategoriaExistsAsync(string nombre)
        {
            var categorias = await _categoriaRepository.FindAsync(c =>
                c.nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            return categorias.Any();
        }

        public async Task<bool> CategoriaHasProductosAsync(int categoriaId)
        {
            var productos = await GetProductosByCategoriaAsync(categoriaId);
            return productos.Any();
        }

        public async Task<bool> CategoriaHasComerciosAsync(int categoriaId)
        {
            var comercios = await GetComerciosByCategoriaAsync(categoriaId);
            return comercios.Any();
        }
    }
}