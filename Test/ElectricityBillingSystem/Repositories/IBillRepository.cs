using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using System.Collections.Generic;

namespace ElectricityBillingSystem.Repositories
{
    public interface IBillRepository
    {
        Feedback CreateBill(CreateBillDTO createBillDTO);
        List<Bill> GetAllBillsAdmin();
        List<Bill> GetAllBillsCustomer(long customerId);
        Feedback UpdatePaymentStatus(UpdatePaymentDTO updatePaymentDTO);
        BillDTO GetBillById(long billId);
    }
}
