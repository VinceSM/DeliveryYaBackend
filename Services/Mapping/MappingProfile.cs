using AutoMapper;
using DeliveryYaBackend.DTOs.Requests.Categorias;
using DeliveryYaBackend.DTOs.Requests.Comercios;
using DeliveryYaBackend.DTOs.Requests.Horarios;
using DeliveryYaBackend.DTOs.Requests.Pedidos;
using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.DTOs.Responses.Auth;
using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.DTOs.Responses.Horarios;
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
            CreateMap<Pedido, PedidoResumenResponse>();
            //CreateMap<ItemPedido, ItemPedidoResponse>();
            //CreateMap<ItemCarritoResponse, Pedido>();
            //CreateMap<UpdatePedidoRequest, Pedido>();
            //CreateMap<CrearPedidoRequest, Pedido>();

            // Productos
            CreateMap<CreateProductoRequest, Producto>();
            CreateMap<UpdateProductoRequest, Producto>();
            CreateMap<Producto, ProductoResponse>();

            // Usuarios
            CreateMap<Cliente, ClienteResponse>();

            // Comercios
            CreateMap<ComercioRequest, Comercio>();
            CreateMap<UpdateComercioRequest, Comercio>();
            CreateMap<Comercio, ComercioResponse>();
            CreateMap<Comercio, ComercioDetalleResponse>();

            // Categorías
            CreateMap<CreateCategoriaRequest, Categoria>();
            CreateMap<UpdateCategoriaRequest, Categoria>();
            CreateMap<Categoria, CategoriaResponse>();

            // Horarios
            CreateMap<CreateHorarioRequest, Horarios>();
            CreateMap<UpdateHorarioRequest, Horarios>();
            CreateMap<Horarios, HorarioResponse>();
        }
    }
}