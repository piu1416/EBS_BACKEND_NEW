using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using System.Collections.Generic;

namespace ElectricityBillingSystem.Repositories
{
    public interface IAdminRepository
    {
        AdminDTO ViewAdminById(long adminId);
        List<CustomerDTO> GetAllCustomers();
        Feedback AddCustomer(CustomerRegDTO customerRegDTO);
        Feedback ChangePassword(ChangePasswordDTO changePasswordDTO);
        Feedback ForgetPassword(ForgetPasswordDTO forgetPasswordDTO);
        Admin ValidateAdmin(LoginModel login);
        Feedback UpdateProfile(UpdateProfileDTO updateProfileDTO);
        Feedback UpdateProfileForCustomer(UpdateProfileDTO updateProfileDTO);
        string GetTokenForAdmin(Admin admin);
        Customer GetCustomerById(long customerId);
        Feedback DeleteCustomer(long customerId);
        Feedback DeleteBill(long billId);
        Feedback DeletePayment(long paymentId);
    }
}
