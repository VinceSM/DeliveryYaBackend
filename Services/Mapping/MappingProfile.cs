using AutoMapper;
using DeliveryYaBackend.DTOs.Requests.Categorias;
using DeliveryYaBackend.DTOs.Requests.Comercios;
using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.DTOs.Responses.Auth;
using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.DTOs.Responses.Pedidos;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.DTOs.Responses.Usuarios;
using DeliveryYaBackend.Models;
using Microsoft.AspNetCore.Identity;

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
            CreateMap<Admin, AdminResponse>();

            // Pedidos
            CreateMap<Pedido, PedidoResponse>();
            CreateMap<Pedido, PedidoDetailResponse>();
            CreateMap<ItemPedido, ItemPedidoResponse>();
            CreateMap<EstadoPedido, EstadoPedidoResponse>();
            CreateMap<MetodoPagoPedido, MetodoPagoResponse>();

            // Productos
            CreateMap<CreateProductoRequest, Producto>();
            CreateMap<UpdateProductoRequest, Producto>();
            CreateMap<Producto, ProductoResponse>();

            // Usuarios
            CreateMap<Cliente, ClienteResponse>();
            CreateMap<Repartidor, RepartidorResponse>();

            // Comercios
            CreateMap<ComercioRequest, Comercio>();
            CreateMap<UpdateComercioRequest, Comercio>();
            CreateMap<Comercio, ComercioResponse>();
            CreateMap<Comercio, ComercioDetailResponse>();

            // Categorías
            CreateMap<CreateCategoriaRequest, Categoria>();
            CreateMap<UpdateCategoriaRequest, Categoria>();
            CreateMap<Categoria, CategoriaResponse>();
        }
    }
}