namespace ElectricityBillingSystem.DTO
{
    public class PaymentDTO
    {
        public long PaymentId { get; set; }  //Primary Key
        public long BillId { get; set; }   //BillID

        public double BillAmount { get; set; }  //Bill Amount
        public string PaymentMethod { get; set; }   //Mode Of Payment

        public string PaymentDate { get; set; }   //Date Of Payment
    }
}
