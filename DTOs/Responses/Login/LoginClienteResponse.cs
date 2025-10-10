namespace DeliveryYaBackend.DTOs.Responses.Auth
{
    public class LoginClienteResponse
    {
        public string Token { get; set; }
        public int UserId { get; set; }          // idusertype
        public int ClienteId { get; set; }       // idcliente
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
    }
}