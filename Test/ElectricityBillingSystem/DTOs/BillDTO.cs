namespace ElectricityBillingSystem.DTO
{
    public class BillDTO
    {
        public long BillId { get; set; }
        public double Amount { get; set; }
        public double Units { get; set; }
        public string DuePaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public long ElectricityBoardId { get; set; }
        public bool isPaid { get; set; }
    }
}
