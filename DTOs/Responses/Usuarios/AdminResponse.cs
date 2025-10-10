namespace DeliveryYaBackend.DTOs.Responses.Usuarios
{
    public class AdminResponse
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = null!;
        // ⚠️ No incluimos Password en el Response
    }
}
