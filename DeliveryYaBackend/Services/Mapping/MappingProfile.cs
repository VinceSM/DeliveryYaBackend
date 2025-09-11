using AutoMapper;
using DeliveryYaBackend.DTOs.Responses.Admin;
using DeliveryYaBackend.DTOs.Responses.Auth;
using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.DTOs.Responses.Pedidos;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.DTOs.Responses.Usuarios;
using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Auth
            CreateMap<Cliente, LoginClienteResponse>();
            CreateMap<Repartidor, LoginRepartidorResponse>();
            CreateMap<Comercio, LoginComercioResponse>();
            CreateMap<Admin, LoginAdminResponse>();

            // Pedidos
            CreateMap<Pedido, PedidoResponse>();
            CreateMap<Pedido, PedidoDetailResponse>();
            CreateMap<ItemPedido, ItemPedidoResponse>();
            CreateMap<EstadoPedido, EstadoPedidoResponse>();
            CreateMap<MetodoPagoPedido, MetodoPagoResponse>();

            // Productos
            CreateMap<Producto, ProductoResponse>();
            CreateMap<Producto, ProductoDetailResponse>();

            // Usuarios
            CreateMap<Cliente, ClienteResponse>();
            CreateMap<Cliente, ClienteDetailResponse>();
            CreateMap<Repartidor, RepartidorResponse>();
            CreateMap<Repartidor, RepartidorDetailResponse>();
            CreateMap<Vehiculo, VehiculoResponse>();

            // Comercios
            CreateMap<Comercio, ComercioResponse>();
            CreateMap<Comercio, ComercioDetailResponse>();

            // Categorías
            CreateMap<Categoria, CategoriaResponse>();
        }
    }
}