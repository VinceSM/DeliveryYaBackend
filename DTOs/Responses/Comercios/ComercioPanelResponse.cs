using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.DTOs.Responses.Horarios;

public class ComercioPanelResponse
{
    public int Id { get; set; }
    public string? NombreComercio { get; set; }
    public string? Email { get; set; }
    public string? FotoPortada { get; set; }
    public decimal? Envio { get; set; }
    public bool? DeliveryPropio { get; set; }
    public bool? PagoEfectivo { get; set; }
    public bool? PagoTarjeta { get; set; }
    public bool? PagoTransferencia { get; set; }
    public string? Celular { get; set; }
    public string? Ciudad { get; set; }
    public string? Calle { get; set; }
    public int Numero { get; set; }
    public int Sucursales { get; set; }
    public decimal? Latitud { get; set; }
    public decimal? Longitud { get; set; }
    public string? Encargado { get; set; }
    public string? Cvu { get; set; }
    public string? Alias { get; set; }
    public decimal Comision { get; set; }

    public List<HorarioResponse> Horarios { get; set; } = new List<HorarioResponse>();
    public List<CategoriaProductoResponse> Categorias { get; set; } = new List<CategoriaProductoResponse>();
}