namespace DeliveryYaBackend.DTOs.Responses.Usuarios
{
    public class ClienteResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; } // idusertype
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}