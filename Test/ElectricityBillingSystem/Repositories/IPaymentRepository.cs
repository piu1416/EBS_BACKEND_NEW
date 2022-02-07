using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using System.Collections.Generic;

namespace ElectricityBillingSystem.Repositories
{
    public interface IPaymentRepository
    {
        Payment GetPaymentById(long paymentId);
        List<Payment> GetPayments();
        Feedback MakePayment(MakePaymentDTO makePaymentDTO);
        List<Payment> GetPaymentByCustomerId(long customerId);

    }
}
