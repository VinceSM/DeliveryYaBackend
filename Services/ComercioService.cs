using DeliveryYaBackend.DTOs.Requests.Comercios;
using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.DTOs.Responses.Horarios;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Services
{
    public class ComercioService : IComercioService
    {
        private readonly IComercioRepository _comercioRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Producto> _productoRepository;
        private readonly IRepository<Horarios> _horariosRepository;
        private readonly IRepository<CategoriaProducto> _categoriaProductoRepository;
        private readonly IRepository<ComercioHorario> _comercioHorarioRepository;
        private readonly IRepository<ItemPedido> _itemPedidoRepository;
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<ComercioCategoria> _comercioCategoriaRepository;

        public ComercioService(
            IComercioRepository comercioRepository,
            IRepository<Categoria> categoriaRepository,
            IRepository<Producto> productoRepository,
            IRepository<Horarios> horariosRepository,
            IRepository<ComercioCategoria> comercioCategoriaRepository,
            IRepository<CategoriaProducto> categoriaProductoRepository,
            IRepository<ComercioHorario> comercioHorarioRepository,
            IRepository<ItemPedido> itemPedidoRepository,
            IRepository<Pedido> pedidoRepository
        )
        {
            _comercioRepository = comercioRepository;
            _categoriaRepository = categoriaRepository;
            _productoRepository = productoRepository;
            _horariosRepository = horariosRepository;
            _comercioCategoriaRepository = comercioCategoriaRepository;
            _categoriaProductoRepository = categoriaProductoRepository;
            _comercioHorarioRepository = comercioHorarioRepository;
            _itemPedidoRepository = itemPedidoRepository;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> ExistsAsync(int idComercio)
        {
            var comercio = await _comercioRepository.GetByIdAsync(idComercio);
            return comercio != null && comercio.deletedAt == null;
        }

        // ===========================
        // 🔹 OPERACIONES BÁSICAS
        // ===========================
        public async Task<ComercioResponse> CreateComercioAsync(ComercioRequest request)
        {
            var comercio = new Comercio
            {
                email = request.Email,
                password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                nombreComercio = request.NombreComercio,
                tipoComercio = request.TipoComercio,
                eslogan = request.Eslogan,
                fotoPortada = null,
                envio = 0,
                deliveryPropio = request.DeliveryPropio,
                celular = request.Celular,
                ciudad = request.Ciudad,
                calle = request.Calle,
                numero = request.Numero,
                sucursales = request.Sucursales,
                latitud = request.Latitud,
                longitud = request.Longitud,
                encargado = request.Encargado,
                cvu = request.Cvu,
                alias = request.Alias,
                destacado = false,
                comision = 0,
                createdAt = DateTime.UtcNow
            };

            await _comercioRepository.AddAsync(comercio);
            await _comercioRepository.SaveChangesAsync();

            return ToResponse(comercio);
        }

        public async Task<IEnumerable<Comercio>> GetAllComerciosAsync()
            => await _comercioRepository.GetAllActivosAsync();

        public async Task<Comercio> GetComercioByIdAsync(int id)
            => await _comercioRepository.GetByIdAsync(id);

        public async Task<Comercio> GetComercioByEmailAsync(string email)
            => await _comercioRepository.GetByEmailAsync(email);

        public async Task<bool> ComercioExistsAsync(string email)
            => await _comercioRepository.ExistsAsync(c => c.email == email);

        public async Task<IEnumerable<Comercio>> GetComerciosByNombreAsync(string nombre)
            => await _comercioRepository.GetByNombreAsync(nombre);

        public async Task<IEnumerable<Comercio>> GetComerciosByCiudadAsync(string ciudad)
            => await _comercioRepository.GetByCiudadAsync(ciudad);

        public async Task<ComercioResponse?> UpdateComercioAsync(int id, UpdateComercioRequest request)
        {
            var comercio = await _comercioRepository.GetByIdAsync(id);
            if (comercio == null || comercio.deletedAt != null)
                return null;

            comercio.nombreComercio = request.NombreComercio;
            comercio.tipoComercio = request.TipoComercio;
            comercio.eslogan = request.Eslogan;
            comercio.email = request.Email;
            comercio.calle = request.Calle;
            comercio.numero = request.Numero;
            comercio.ciudad = request.Ciudad;
            comercio.fotoPortada = request.FotoPortada;
            comercio.envio = request.Envio;
            comercio.deliveryPropio = request.DeliveryPropio;
            comercio.celular = request.Celular;
            comercio.encargado = request.Encargado;
            comercio.cvu = request.Cvu;
            comercio.alias = request.Alias;
            comercio.latitud = request.Latitud;
            comercio.longitud = request.Longitud;
            comercio.destacado = request.Destacado;
            comercio.comision = request.Comision;
            comercio.updatedAt = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(request.Password))
                comercio.password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            _comercioRepository.Update(comercio);
            await _comercioRepository.SaveChangesAsync();

            return ToResponse(comercio);
        }

        public async Task<bool> DeleteComercioAsync(int id)
        {
            var comercio = await _comercioRepository.GetByIdAsync(id);
            if (comercio == null) return false;

            comercio.deletedAt = DateTime.UtcNow;

            _comercioRepository.Update(comercio);
            return await _comercioRepository.SaveChangesAsync();
        }

        // ===========================
        // 🔹 DESTACADOS Y ESTADO
        // ===========================
        public async Task<IEnumerable<Comercio>> GetComerciosDestacadosAsync()
            => await _comercioRepository.GetDestacadosAsync();

        // ===========================
        // 🔹 PRODUCTOS DEL COMERCIO
        // ===========================
        public async Task<IEnumerable<Producto>> GetProductosByComercioAsync(int comercioId)
        {
            // 1️⃣ Busco las categorías del comercio
            var relacionesCategorias = await _comercioCategoriaRepository.FindAsync(cc => cc.ComercioIdComercio == comercioId);
            var categoriaIds = relacionesCategorias.Select(c => c.CategoriaIdCategoria).Distinct();

            // 2️⃣ Busco los productos de esas categorías
            var relacionesCatProd = await _categoriaProductoRepository.FindAsync(cp => categoriaIds.Contains(cp.CategoriaIdCategoria));
            var productoIds = relacionesCatProd.Select(cp => cp.ProductoIdProducto).Distinct();

            // 3️⃣ Devuelvo los productos activos
            var productos = new List<Producto>();
            foreach (var id in productoIds)
            {
                var producto = await _productoRepository.GetByIdAsync(id);
                if (producto != null && producto.deletedAt == null)
                    productos.Add(producto);
            }

            return productos;
        }

        public async Task<int> GetCantidadProductosByComercioAsync(int comercioId)
        {
            var productos = await GetProductosByComercioAsync(comercioId);
            return productos.Count();
        }

        // ===========================
        // 🔹 ESTADÍSTICAS Y REPORTES
        // ===========================
        public async Task<decimal> GetVentasTotalesByComercioAsync(int comercioId, DateTime? startDate, DateTime? endDate)
        {
            var items = await _itemPedidoRepository.FindAsync(ip => ip.ComercioIdComercio == comercioId);

            if (startDate.HasValue)
                items = items.Where(ip => ip.Pedido.fecha >= startDate.Value).ToList();

            if (endDate.HasValue)
                items = items.Where(ip => ip.Pedido.fecha <= endDate.Value).ToList();

            return items.Sum(ip => ip.precioFinal * ip.cantProducto);
        }

        public async Task<int> GetPedidosComercioAsync(int comercioId, DateTime? startDate, DateTime? endDate)
        {
            var items = await _itemPedidoRepository.FindAsync(ip => ip.ComercioIdComercio == comercioId);

            if (startDate.HasValue)
                items = items.Where(ip => ip.Pedido.fecha >= startDate.Value).ToList();

            if (endDate.HasValue)
                items = items.Where(ip => ip.Pedido.fecha <= endDate.Value).ToList();

            return items.Select(ip => ip.PedidoIdPedido).Distinct().Count();
        }

        // ===========================
        // 🔹 PANEL DETALLE
        // ===========================
        public async Task<ComercioPanelResponse?> GetComercioPanelDetalleAsync(int comercioId)
        {
            var comercio = await _comercioRepository.GetByIdAsync(comercioId);
            if (comercio == null || comercio.deletedAt != null)
                return null;

            var panel = new ComercioPanelResponse
            {
                Id = comercio.idcomercio,
                NombreComercio = comercio.nombreComercio,
                Email = comercio.email,
                FotoPortada = comercio.fotoPortada,
                Envio = comercio.envio,
                DeliveryPropio = comercio.deliveryPropio,
                Ciudad = comercio.ciudad,
                Calle = comercio.calle,
                Numero = comercio.numero,
                Sucursales = comercio.sucursales,
                Celular = comercio.celular,
                Encargado = comercio.encargado,
                Cvu = comercio.cvu,
                Alias = comercio.alias,
                Comision = comercio.comision
            };

            // 🕓 Horarios
            var relacionesHorarios = await _comercioHorarioRepository.FindAsync(ch => ch.ComercioIdComercio == comercioId);
            foreach (var rh in relacionesHorarios)
            {
                var horario = await _horariosRepository.GetByIdAsync(rh.HorariosIdHorarios);
                if (horario != null)
                {
                    panel.Horarios.Add(new HorarioResponse
                    {
                        Apertura = horario.apertura,
                        Cierre = horario.cierre,
                        Dias = horario.dias,
                        Abierto = horario.abierto
                    });
                }
            }

            // 📦 Categorías con productos
            var relacionesCategorias = await _comercioCategoriaRepository.FindAsync(cc => cc.ComercioIdComercio == comercioId);
            foreach (var rc in relacionesCategorias)
            {
                var categoria = await _categoriaRepository.GetByIdAsync(rc.CategoriaIdCategoria);
                if (categoria == null || categoria.deletedAt != null) continue;

                var categoriaResponse = new CategoriaProductoResponse
                {
                    Id = categoria.idcategoria,
                    Nombre = categoria.nombre
                };

                var relacionesCatProd = await _categoriaProductoRepository.FindAsync(cp => cp.CategoriaIdCategoria == categoria.idcategoria);
                foreach (var rel in relacionesCatProd)
                {
                    var producto = await _productoRepository.GetByIdAsync(rel.ProductoIdProducto);
                    if (producto != null && producto.deletedAt == null)
                    {
                        categoriaResponse.Productos.Add(new ProductoResponse
                        {
                            IdProducto = producto.idproducto,
                            Nombre = producto.nombre,
                            PrecioUnitario = producto.precioUnitario,
                            FotoPortada = producto.fotoPortada
                        });
                    }
                }

                panel.Categorias.Add(categoriaResponse);
            }

            return panel;
        }

        // ===========================
        // 🔹 Helper
        // ===========================
        private ComercioResponse ToResponse(Comercio comercio) => new ComercioResponse
        {
            Id = comercio.idcomercio,
            Email = comercio.email,
            NombreComercio = comercio.nombreComercio,
            TipoComercio = comercio.tipoComercio,
            Eslogan = comercio.eslogan,
            FotoPortada = comercio.fotoPortada,
            Envio = comercio.envio,
            DeliveryPropio = comercio.deliveryPropio,
            Celular = comercio.celular,
            Ciudad = comercio.ciudad,
            Calle = comercio.calle,
            Numero = comercio.numero,
            Sucursales = comercio.sucursales,
            Latitud = comercio.latitud,
            Longitud = comercio.longitud,
            Encargado = comercio.encargado,
            Cvu = comercio.cvu,
            Alias = comercio.alias,
            Destacado = comercio.destacado,
            Comision = comercio.comision
        };
    }
}
