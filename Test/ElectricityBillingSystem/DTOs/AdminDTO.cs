namespace ElectricityBillingSystem.DTO
{
    public class AdminDTO
    {
        public long AdminId { get; set; } //Primary key
        public string Role { get; set; }   //Role of the person
        public string AdminName { get; set; }   //Name of the admin
        public string AdminEmail { get; set; }   //EmailId of the admin Used for LogIn purpose
        public string AdminContactNo { get; set; } //Contanct number of the admin
    }
}
