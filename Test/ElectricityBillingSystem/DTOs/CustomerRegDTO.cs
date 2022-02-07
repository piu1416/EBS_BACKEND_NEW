namespace ElectricityBillingSystem.DTO
{
    public class CustomerRegDTO
    {
        public long ElectricityBoardId { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContactNo { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerQuestion { get; set; }
        public string CustomerAnswer { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
