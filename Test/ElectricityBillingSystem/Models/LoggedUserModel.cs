namespace ElectricityBillingSystem.Models
{
    public class LoggedUserModel
    {
        //UserID
        public long Id { get; set; }
        //User Email Address
        public string EmailID { get; set; }
        //User Role
        public string Role { get; set; }
        //Welcome message for sucessful login
        public string Message { get; set; }
        //JWT Token
        public string Token { get; set; }

    }
}
