namespace DeliveryYaBackend.DTOs.Responses.Usuarios
{
    public class ClienteDetailResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Dni { get; set; }
        public DateOnly Nacimiento { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public string? Ciudad { get; set; }
        public string? Calle { get; set; }
        public int Numero { get; set; }
        public int TotalPedidos { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}