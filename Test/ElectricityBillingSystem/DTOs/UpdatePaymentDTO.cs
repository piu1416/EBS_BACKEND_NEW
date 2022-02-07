namespace ElectricityBillingSystem.DTO
{
    public class UpdatePaymentDTO
    {
        public long PaymentId { get; set; }  //Primary Key
        public long BillId { get; set; }   //BillID
        public string PaymentStatus { get; set; }   //Mode Of Payment

    }
}
