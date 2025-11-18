namespace DeliveryYaBackend.DTOs.Requests.Comercios
{
    public class ActualizarMetodosPagoRequest
    {
        public bool PagoEfectivo { get; set; }
        public bool PagoTarjeta { get; set; }
        public bool PagoTransferencia { get; set; }
    }
}
