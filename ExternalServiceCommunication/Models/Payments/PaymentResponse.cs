namespace ExternalServiceCommunication.Models.Name
{
    public class PaymentResponse
    {
        public string PaymentId { get; set; }
        public string Status { get; set; }
        public string CardType { get; set; }
        public string CardBrand { get; set; }
        public string LastFourDigits { get; set; }
    }
}
