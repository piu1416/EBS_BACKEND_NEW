using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricityBillingSystem.Entities
{
    [Table("payment")]
    public class Payment
    {
        [Key]
        public long PaymentId { get; set; }  //Primary Key
        [ForeignKey("BillId")]
        public long BillId { get; set; }   //BillID
        [ForeignKey("CustomerId")]
        public long CustomerId { get; set; }   //CustomerID
        public double BillAmount { get; set; }  //Bill Amount
        [Required]
        public string PaymentMethod { get; set; }   //Mode Of Payment

        [Required(ErrorMessage = "Please Enter the Payment Date")]
        public string PaymentDate { get; set; }   //Date Of Payment

        public bool isUpdatedtoBill { get; set; } //Payment is made or not
    }
}
