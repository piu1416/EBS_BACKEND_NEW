using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;

namespace ElectricityBillingSystem.Repositories
{
    public interface ICustomerRepository
    {
        Feedback Register(CustomerRegDTO customerRegDTO);
        CustomerDTO ViewCustomerById(long customerId);
        Feedback ChangePassword(ChangePasswordDTO changePasswordDTO);
        Feedback ForgetPassword(ForgetPasswordDTO forgetPasswordDTO);
        Customer ValidateCustomer(LoginModel login);
        string GetTokenForCustomer(Customer customer);
    }
}
