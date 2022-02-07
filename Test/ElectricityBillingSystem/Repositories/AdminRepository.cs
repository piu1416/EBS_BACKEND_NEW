using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ElectricityBillingSystem.Repositories
{

    public class AdminRepository : IAdminRepository
    {
        private EBS_DbContext _eBS_DbContext;

        //Constructor
        public AdminRepository(EBS_DbContext context)
        {
            this._eBS_DbContext = context;
        }

        //Adding a new Customer
        public Feedback AddCustomer(CustomerRegDTO customerRegDTO)
        {
            Feedback feedback = null;
            try
            {
                //Validating password with confirmpasssword
                if (customerRegDTO.Password == customerRegDTO.ConfirmPassword)
                {
                    Customer customer = new Customer();
                    //Checking if Customer already exists by matching Email, ElectricityBoardId and Contact number
                    Customer customer1 = _eBS_DbContext.Customer.SingleOrDefault(s => s.ElectricityBoardId == customerRegDTO.ElectricityBoardId);
                    Customer customer2 = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerEmail == customerRegDTO.CustomerEmail);
                    Customer customer3 = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerContactNo == customerRegDTO.CustomerContactNo);
                    if (customer1 == null)
                    {
                        if (customer2 == null)
                        {
                            if (customer3 == null)
                            {
                                //Adding customer details to database
                                Role role = Role.CUSTOMER;
                                customer.Role = role.ToString();
                                customer.CustomerType = customerRegDTO.CustomerType;
                                customer.CustomerName = customerRegDTO.CustomerName;
                                customer.CustomerEmail = customerRegDTO.CustomerEmail;
                                customer.Password = customerRegDTO.Password;
                                customer.CustomerAddress = customerRegDTO.CustomerAddress;
                                customer.ElectricityBoardId = customerRegDTO.ElectricityBoardId;
                                customer.CustomerQuestion = customerRegDTO.CustomerQuestion;
                                customer.CustomerAnswer = customerRegDTO.CustomerAnswer;
                                customer.CustomerContactNo = customerRegDTO.CustomerContactNo;
                                _eBS_DbContext.Customer.Add(customer);
                                _eBS_DbContext.SaveChanges();
                                feedback = new Feedback() { Result = true, Message = "Customer Added" };
                            }
                            else
                            {
                                feedback = new Feedback() { Result = false, Message = "Contact number is already registered!" };
                            }
                        }
                        else { feedback = new Feedback() { Result = false, Message = "Email is already registered!" }; }
                    }
                    else
                    {
                        feedback = new Feedback() { Result = false, Message = "Electricity Board ID is already registered!" };

                    }
                }
                else { feedback = new Feedback() { Result = false, Message = "Password doesnot match with Confirm Password!" }; }


            }
            catch (Exception ex)
            {
                feedback = new Feedback() { Result = false, Message = ex.Message };

            }
            return feedback;
        }

        //Changing Current Password
        public Feedback ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            //Checking if Admin already exists by matching Email
            Admin admin1 = _eBS_DbContext.Admin.SingleOrDefault(s => s.AdminEmail == changePasswordDTO.Email);
            if (admin1 != null)
            {
                //Matching the old Password
                if (changePasswordDTO.OldPassword == admin1.Password)
                {
                    //Updating the new password
                    admin1.Password = changePasswordDTO.NewPassword;
                    _eBS_DbContext.Admin.Update(admin1);
                    _eBS_DbContext.SaveChanges();
                    Feedback feedback = new Feedback { Result = true, Message = "Password Changed" };
                    return feedback;
                }
                else
                {
                    Feedback feedback = new Feedback { Result = false, Message = "Incorrect old Password" };
                    return feedback;
                }
            }
            else
            {
                Feedback feedback = new Feedback { Result = false, Message = "Admin Email not registered!" };
                return feedback;
            }
        }

        //Changing Current Password using Forget password
        public Feedback ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            //Checking if Admin already exists by matching Email
            Admin admin1 = _eBS_DbContext.Admin.SingleOrDefault(s => s.AdminEmail == forgetPasswordDTO.Email);
            if (admin1 != null)
            {
                //Matching the old Password
                if (forgetPasswordDTO.Answer == admin1.AdminAnswer)
                {
                    //Updating the new password
                    admin1.Password = forgetPasswordDTO.NewPassword;
                    _eBS_DbContext.Admin.Update(admin1);
                    _eBS_DbContext.SaveChanges();
                    Feedback feedback = new Feedback { Result = true, Message = "Password has been reset!" };
                    return feedback;
                }
                else
                {
                    Feedback feedback = new Feedback { Result = false, Message = "Incorrect Answer!" };
                    return feedback;
                }
            }
            else
            {
                Feedback feedback = new Feedback { Result = false, Message = "Admin Email not registered!" };
                return feedback;
            }
        }

        //Updating profile details
        public Feedback UpdateProfile(UpdateProfileDTO updateProfileDTO)
        {
            Feedback feedback = null;
            try
            {
                //Check if Admin already exists with the ID
                Admin admin = _eBS_DbContext.Admin.SingleOrDefault(s => s.AdminId == updateProfileDTO.Id);
                //Checking if Email and Contact number already exists
                Admin admin1 = _eBS_DbContext.Admin.SingleOrDefault(s => s.AdminContactNo == updateProfileDTO.ContactNo);
                Admin admin2 = _eBS_DbContext.Admin.SingleOrDefault(s => s.AdminEmail == updateProfileDTO.Email);
                if (admin != null)
                {
                    if (admin1 == null || admin1.AdminId == updateProfileDTO.Id)
                    {
                        if (admin2 == null || admin2.AdminId == updateProfileDTO.Id)
                        {
                            //Updating profile Details
                            admin.AdminName = updateProfileDTO.Name;
                            admin.AdminContactNo = updateProfileDTO.ContactNo;
                            admin.AdminEmail = updateProfileDTO.Email;
                            _eBS_DbContext.Admin.Update(admin);
                            _eBS_DbContext.SaveChanges();
                            feedback = new Feedback() { Result = true, Message = "Profile Updated!" };
                        }
                        else { feedback = new Feedback() { Result = true, Message = "EmailID is already taken!" }; }
                    }
                    else { feedback = new Feedback() { Result = true, Message = "Contact number is already taken!" }; }
                }
                else
                {
                    feedback = new Feedback() { Result = false, Message = "Invalid Admin ID!" };

                }

            }
            catch (Exception ex)
            {
                feedback = new Feedback() { Result = false, Message = ex.Message };

            }
            return feedback;
        }

        //Updating profile details for Customer
        public Feedback UpdateProfileForCustomer(UpdateProfileDTO updateProfileDTO)
        {
            Feedback feedback = null;
            try
            {
                //Check if Customer already exists with the ID
                Customer customer = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerId == updateProfileDTO.Id);
                //Checking if Email and Contact number already exists
                Customer customer1 = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerContactNo == updateProfileDTO.ContactNo);
                Customer customer2 = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerEmail == updateProfileDTO.Email);
                if (customer != null)
                {
                    if (customer1 == null || customer1.CustomerId == updateProfileDTO.Id)
                    {
                        if (customer2 == null || customer2.CustomerId == updateProfileDTO.Id)
                        {
                            //Updating profile Details
                            customer.CustomerName = updateProfileDTO.Name;
                            customer.CustomerContactNo = updateProfileDTO.ContactNo;
                            customer.CustomerEmail = updateProfileDTO.Email;
                            _eBS_DbContext.Customer.Update(customer);
                            _eBS_DbContext.SaveChanges();
                            feedback = new Feedback() { Result = true, Message = "Profile Updated!" };
                        }
                        else { feedback = new Feedback() { Result = true, Message = "EmailID is already taken!" }; }
                    }
                    else { feedback = new Feedback() { Result = true, Message = "Contact number is already taken!" }; }
                }
                else
                {
                    feedback = new Feedback() { Result = false, Message = "Invalid Customer ID!" };

                }

            }
            catch (Exception ex)
            {
                feedback = new Feedback() { Result = false, Message = ex.Message };

            }
            return feedback;
        }

        //Validating login Credentials 
        public Admin ValidateAdmin(LoginModel login)
        {
            return _eBS_DbContext.Admin.SingleOrDefault(u => u.AdminEmail == login.email && u.Password == login.password);
        }

        //Getting Admin details using ID
        public AdminDTO ViewAdminById(long adminId)
        {
            //Checking if ID is valid and data for the ID is present
            Admin admin = _eBS_DbContext.Admin.SingleOrDefault(s => s.AdminId == adminId);
            if (admin != null)
            {
                //Hiding out important Informations
                AdminDTO adminDTO = new AdminDTO();
                adminDTO.AdminId = admin.AdminId;
                adminDTO.Role = admin.Role;
                adminDTO.AdminName = admin.AdminName;
                adminDTO.AdminEmail = admin.AdminEmail;
                adminDTO.AdminContactNo = admin.AdminContactNo;
                if (adminDTO != null)
                {
                    return adminDTO;
                }
                else { return null; }
            }
            else { return null; }

        }

        //Getting token for Authorization
        public string GetTokenForAdmin(Admin admin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                new Claim(ClaimTypes.Name, admin.AdminEmail.ToString()),
                new Claim(ClaimTypes.Role, admin.Role)
            };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        //Getting list of all customers
        public List<CustomerDTO> GetAllCustomers()
        {
            List<Customer> customers = _eBS_DbContext.Customer.ToList();
            List<CustomerDTO> customersDTO = new List<CustomerDTO>();

            if (customers != null)
            {
                foreach (Customer x in customers)
                {
                    CustomerDTO customerDTO = new CustomerDTO();
                    customerDTO.CustomerId = x.CustomerId;
                    customerDTO.Role = x.Role;
                    customerDTO.ElectricityBoardId = x.ElectricityBoardId;
                    customerDTO.CustomerName = x.CustomerName;
                    customerDTO.CustomerType = x.CustomerType;
                    customerDTO.CustomerEmail = x.CustomerEmail;
                    customerDTO.CustomerAddress = x.CustomerAddress;
                    customerDTO.CustomerContactNo = x.CustomerContactNo;
                    customersDTO.Add(customerDTO);
                }
                return customersDTO;
            }
            else
            {
                return null;
            }
        }

        //Getting customer deatils by ID
        public Customer GetCustomerById(long customerId)
        {
            Customer customer = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerId == customerId);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                return null;
            }
        }

        //Deleting customer deatils by ID
        public Feedback DeleteCustomer(long customerId)
        {
            try
            {
                //Check if Customer exists or not
                Customer customer = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerId == customerId);
                if (customer != null)
                {
                    //Deleted Customer
                    _eBS_DbContext.Customer.Remove(customer);
                    _eBS_DbContext.SaveChanges();
                    var fb = new Feedback() { Result = true, Message = "Customer Removed" };
                    return fb;
                }
                else
                {
                    var fb = new Feedback() { Result = false, Message = "Customer doesn't exists" };
                    return fb;
                }
            }
            catch (Exception ex)
            {
                var fb = new Feedback() { Result = false, Message = ex.Message };
                return fb;
            }
        }

        //Deleting customer deatils by ID
        public Feedback DeleteBill(long billId)
        {
            try
            {
                //Check if Bill exists or not
                Bill bill = _eBS_DbContext.Bill.SingleOrDefault(s => s.BillId == billId);
                if (bill != null)
                {
                    //Deleted Bill
                    _eBS_DbContext.Bill.Remove(bill);
                    _eBS_DbContext.SaveChanges();
                    var fb = new Feedback() { Result = true, Message = "Bill Removed" };
                    return fb;
                }
                else
                {
                    var fb = new Feedback() { Result = false, Message = "Bill doesn't exists" };
                    return fb;
                }
            }
            catch (Exception ex)
            {
                var fb = new Feedback() { Result = false, Message = ex.Message };
                return fb;
            }
        }

        //Deleting customer deatils by ID
        public Feedback DeletePayment(long paymentId)
        {
            try
            {
                //Check if Payment exists or not
                Payment payment = _eBS_DbContext.Payment.SingleOrDefault(s => s.PaymentId == paymentId);
                if (payment != null)
                {
                    //Deleted Customer
                    _eBS_DbContext.Payment.Remove(payment);
                    _eBS_DbContext.SaveChanges();
                    var fb = new Feedback() { Result = true, Message = "Payment Removed" };
                    return fb;
                }
                else
                {
                    var fb = new Feedback() { Result = false, Message = "Payment doesn't exists" };
                    return fb;
                }
            }
            catch (Exception ex)
            {
                var fb = new Feedback() { Result = false, Message = ex.Message };
                return fb;
            }
        }
    }
}
