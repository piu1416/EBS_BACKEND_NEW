using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricityBillingSystem.Entities
{
    //Admin Entity 
    [Table("admin")]
    public class Admin
    {
        [Key]
        public long AdminId { get; set; } //Primary key

        [Required]
        public string Role { get; set; }   //Role of the person

        [Required]
        public string AdminQuestion { get; set; } //Security Question of admin

        [Required]
        public string AdminAnswer { get; set; }   //Security answer of admin

        [Required]
        public string AdminName { get; set; }   //Name of the admin

        [Required]
        [EmailAddress]
        public string AdminEmail { get; set; }   //EmailId of the admin Used for LogIn purpose

        [Required(ErrorMessage = "Please create a password")]
        public string Password { get; set; } //Password of the admin Used for LogIn purpose

        [Required]
        [Phone]
        public string AdminContactNo { get; set; } //Contanct number of the admin
    }
}
