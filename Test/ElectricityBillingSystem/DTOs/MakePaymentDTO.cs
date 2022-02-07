namespace ElectricityBillingSystem.DTO
{
    public class MakePaymentDTO
    {
        public long BillId { get; set; }   //BillID
        public long CustomerId { get; set; }   //CustomerID
        public string PaymentMethod { get; set; }   //Mode Of Payment

    }
}
