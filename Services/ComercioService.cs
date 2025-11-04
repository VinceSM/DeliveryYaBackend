using DeliveryYaBackend.DTOs.Requests.Comercios;
using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.DTOs.Responses.Horarios;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;


namespace DeliveryYaBackend.Services
{
    public class ComercioService : IComercioService
    {
        private readonly IRepository<Comercio> _comercioRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Horarios> _horariosRepository;
        private readonly IRepository<Producto> _productoRepository;
        private readonly IRepository<CategoriaProducto> _categoriaProductoRepository;
        private readonly IRepository<ComercioHorario> _comercioHorarioRepository;
        private readonly IRepository<ItemPedido> _itemPedidoRepository;
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<ComercioCategoria> _comercioCategoriaRepository;

        public ComercioService(
            IRepository<Comercio> comercioRepository,
            IRepository<Horarios> horariosRepository,
            IRepository<Producto> productoRepository,
            IRepository<ComercioCategoria> comercioCategoriaRepository,
            IRepository<CategoriaProducto> categoriaProductoRepository,
            IRepository<ComercioHorario> comercioHorarioRepository,
            IRepository<ItemPedido> itemPedidoRepository,
            IRepository<Pedido> pedidoRepository
            )
        {

            _horariosRepository = horariosRepository;
            _productoRepository = productoRepository;
            _comercioCategoriaRepository = comercioCategoriaRepository;
            _categoriaProductoRepository = categoriaProductoRepository;
            _comercioHorarioRepository = comercioHorarioRepository;
            _itemPedidoRepository = itemPedidoRepository;
            _pedidoRepository = pedidoRepository;
        }

        // OPERACIONES BÁSICAS
        public async Task<ComercioResponse> CreateComercioAsync(ComercioRequest request)
        {
            var comercio = new Comercio
            {
                email = request.Email,
                password = request.Password,
                nombreComercio = request.NombreComercio,
                tipoComercio = request.TipoComercio,
                eslogan = request.Eslogan,
                fotoPortada = null, // opcional, se carga después
                envio = 0,             // opcional, se puede setear luego
                deliveryPropio = request.DeliveryPropio,
                celular = request.Celular,
                ciudad = request.Ciudad,
                calle = request.Calle,
                numero = request.Numero,
                sucursales = request.Sucursales,
                latitud = request.Latitud,         // ahora puede ser null
                longitud = request.Longitud,       // ahora puede ser null
                encargado = request.Encargado,
                cvu = request.Cvu,
                alias = request.Alias,
                destacado = false,
                comision = 0,
            };

            // 🔒 Hashear la contraseña antes de guardar
            comercio.password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _comercioRepository.AddAsync(comercio);
            await _comercioRepository.SaveChangesAsync();

            return ToResponse(comercio);
        }

        private ComercioResponse ToResponse(Comercio comercio)
        {
            return new ComercioResponse
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
                Comision = comercio.comision,
            };
        }

        public async Task<Comercio> GetComercioByEmailAsync(string email)
        {
            var comercios = await _comercioRepository.FindAsync(c => c.email == email);
            return comercios.FirstOrDefault();
        }

        public async Task<bool> ComercioExistsAsync(string email)
        {
            var comercios = await _comercioRepository.FindAsync(c => c.email == email);
            return comercios.Any();
        }

        public async Task<Comercio> GetComercioByIdAsync(int id)
        {
            return await _comercioRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Comercio>> GetAllComerciosAsync()
        {
            return await _comercioRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Comercio>> GetComerciosByNombreAsync(string nombre)
        {
            return await _comercioRepository.FindAsync(c => c.nombreComercio.Contains(nombre));
        }

        public async Task<IEnumerable<Comercio>> GetComerciosByCiudadAsync(string ciudad)
        {
            return await _comercioRepository.FindAsync(c => c.ciudad.Equals(ciudad, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<ComercioResponse?> UpdateComercioAsync(int id, UpdateComercioRequest request)
        {
            var comercio = await _comercioRepository.GetByIdAsync(id);
            if (comercio == null) return null;

            comercio.nombreComercio = request.NombreComercio;
            comercio.tipoComercio = request.TipoComercio;
            comercio.eslogan = request.Eslogan;
            comercio.email = request.Email;
            comercio.calle = request.Calle;
            comercio.numero = request.Numero;
            comercio.sucursales = request.Sucursales;
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

            // Solo hashear si la contraseña viene en el request
            if (!string.IsNullOrEmpty(request.Password))
            {
                comercio.password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            comercio.updatedAt = DateTime.UtcNow;

            _comercioRepository.Update(comercio);
            await _comercioRepository.SaveChangesAsync();

            return ToResponse(comercio);
        }

        public async Task<bool> DeleteComercioAsync(int id)
        {
            var comercio = await _comercioRepository.GetByIdAsync(id);
            if (comercio == null) return false;

            _comercioRepository.Remove(comercio);
            await _comercioRepository.SaveChangesAsync();

            return true;
        }

        // GESTIÓN DE DESTACADOS Y ESTADO

        public async Task<IEnumerable<Comercio>> GetComerciosDestacadosAsync()
        {
            return await _comercioRepository.FindAsync(c => c.destacado == true);
        }

        // HORARIOS DE COMERCIOS
        public async Task<bool> AddHorarioToComercioAsync(int comercioId, int horarioId)
        {
            var existingRelation = await _comercioHorarioRepository.FindAsync(ch =>
                ch.ComercioIdComercio == comercioId && ch.HorariosIdHorarios == horarioId);

            if (existingRelation.Any()) return true;

            var comercioHorario = new ComercioHorario
            {
                ComercioIdComercio = comercioId,
                HorariosIdHorarios = horarioId
            };

            await _comercioHorarioRepository.AddAsync(comercioHorario);
            return await _comercioHorarioRepository.SaveChangesAsync();
        }

        public async Task<bool> RemoveHorarioFromComercioAsync(int comercioId, int horarioId)
        {
            var relaciones = await _comercioHorarioRepository.FindAsync(ch =>
                ch.ComercioIdComercio == comercioId && ch.HorariosIdHorarios == horarioId);

            var relacion = relaciones.FirstOrDefault();
            if (relacion == null) return false;

            _comercioHorarioRepository.Remove(relacion);
            return await _comercioHorarioRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Horarios>> GetHorariosByComercioAsync(int comercioId)
        {
            var relaciones = await _comercioHorarioRepository.FindAsync(ch => ch.ComercioIdComercio == comercioId);
            var horarios = new List<Horarios>();

            foreach (var relacion in relaciones)
            {
                var horario = await _horariosRepository.GetByIdAsync(relacion.HorariosIdHorarios);
                if (horario != null)
                {
                    horarios.Add(horario);
                }
            }

            return horarios;
        }

        public async Task<bool> CheckComercioAbiertoAsync(int comercioId)
        {
            var horarios = await GetHorariosByComercioAsync(comercioId);
            var ahora = DateTime.Now.TimeOfDay;
            var diaActual = DateTime.Now.DayOfWeek.ToString();

            foreach (var horario in horarios)
            {
                if (horario.dias != null && horario.dias.Contains(diaActual) &&
                    horario.apertura.HasValue && horario.cierre.HasValue &&
                    ahora >= horario.apertura.Value && ahora <= horario.cierre.Value &&
                    horario.abierto)
                {
                    return true;
                }
            }

            return false;
        }

        // PRODUCTOS DE COMERCIOS
        public async Task<IEnumerable<Producto>> GetProductosByComercioAsync(int comercioId)
        {
            // Esta implementación depende de cómo relates productos con comercios
            // Puede ser through categorías compartidas o through items de pedido
            var itemsPedido = await _itemPedidoRepository.FindAsync(ip => ip.ComercioIdComercio == comercioId);
            var productoIds = itemsPedido.Select(ip => ip.ProductoIdProducto).Distinct();

            var productos = new List<Producto>();
            foreach (var productoId in productoIds)
            {
                var producto = await _productoRepository.GetByIdAsync(productoId);
                if (producto != null)
                {
                    productos.Add(producto);
                }
            }

            return productos;
        }

        public async Task<int> GetCantidadProductosByComercioAsync(int comercioId)
        {
            var productos = await GetProductosByComercioAsync(comercioId);
            return productos.Count();
        }

        // ESTADÍSTICAS Y REPORTES
        public async Task<decimal> GetVentasTotalesByComercioAsync(int comercioId, DateTime? startDate, DateTime? endDate)
        {
            var itemsQuery = _itemPedidoRepository.FindAsync(ip => ip.ComercioIdComercio == comercioId);
            var items = await itemsQuery;

            if (startDate.HasValue)
            {
                items = items.Where(ip => ip.Pedido.fecha >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                items = items.Where(ip => ip.Pedido.fecha <= endDate.Value).ToList();
            }

            return items.Sum(ip => ip.precioFinal * ip.cantProducto);
        }

        public async Task<int> GetPedidosComercioAsync(int comercioId, DateTime? startDate, DateTime? endDate)
        {
            var itemsQuery = _itemPedidoRepository.FindAsync(ip => ip.ComercioIdComercio == comercioId);
            var items = await itemsQuery;

            if (startDate.HasValue)
            {
                items = items.Where(ip => ip.Pedido.fecha >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                items = items.Where(ip => ip.Pedido.fecha <= endDate.Value).ToList();
            }

            return items.Select(ip => ip.PedidoIdPedido).Distinct().Count();
        }

        public async Task<ComercioPanelResponse?> GetComercioPanelDetalleAsync(int comercioId)
        {
            var comercio = await _comercioRepository.GetByIdAsync(comercioId);
            if (comercio == null) return null;

            var panelResponse = new ComercioPanelResponse
            {
                Id = comercio.idcomercio,
                NombreComercio = comercio.nombreComercio,
                Email = comercio.email,
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
                Comision = comercio.comision
            };

            // Horarios
            var relacionesHorarios = await _comercioHorarioRepository.FindAsync(ch => ch.ComercioIdComercio == comercioId);
            foreach (var rh in relacionesHorarios)
            {
                var horario = await _horariosRepository.GetByIdAsync(rh.HorariosIdHorarios);
                if (horario != null)
                {
                    panelResponse.Horarios.Add(new HorarioResponse
                    {
                        Apertura = horario.apertura,
                        Cierre = horario.cierre,
                        Dias = horario.dias,
                        Abierto = horario.abierto
                    });
                }
            }

            // Categorías con productos
            var relacionesCategorias = await _comercioCategoriaRepository.FindAsync(cc => cc.ComercioIdComercio == comercioId);
            foreach (var rc in relacionesCategorias)
            {
                var categoria = await _categoriaRepository.GetByIdAsync(rc.CategoriaIdCategoria);
                if (categoria != null)
                {
                    var categoriaResponse = new CategoriaProductoResponse
                    {
                        Id = categoria.idcategoria,
                        Nombre = categoria.nombre
                    };

                    // Productos de la categoría
                    var productosRelacionados = await _productoRepository.FindAsync(p =>
                        _categoriaProductoRepository.FindAsync(cp => cp.CategoriaIdCategoria == categoria.idcategoria && cp.ProductoIdProducto == p.idproducto).Result.Any()
                    );

                    foreach (var producto in productosRelacionados)
                    {
                        categoriaResponse.Productos.Add(new ProductoResponse
                        {
                            IdProducto = producto.idproducto,
                            Nombre = producto.nombre,
                            PrecioUnitario = producto.precioUnitario,
                            FotoPortada = producto.fotoPortada
                        });
                    }

                    panelResponse.Categorias.Add(categoriaResponse);
                }
            }

            return panelResponse;
        }
    }
}