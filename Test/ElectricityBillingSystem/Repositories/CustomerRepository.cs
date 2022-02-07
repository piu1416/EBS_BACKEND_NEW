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
    public class CustomerRepository : ICustomerRepository
    {
        private EBS_DbContext _eBS_DbContext;

        //Constructor
        public CustomerRepository(EBS_DbContext context)
        {
            _eBS_DbContext = context;
        }

        //Changing Current password
        public Feedback ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            Customer customer1 = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerEmail == changePasswordDTO.Email);
            if (customer1 != null)
            {
                if (changePasswordDTO.OldPassword == customer1.Password)
                {
                    customer1.Password = changePasswordDTO.NewPassword;
                    _eBS_DbContext.Customer.Update(customer1);
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
                Feedback feedback = new Feedback { Result = false, Message = "Customer Email not registered!" };
                return feedback;
            }
        }

        //Changing Current password using Forget Password
        public Feedback ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            Customer customer1 = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerEmail == forgetPasswordDTO.Email);
            if (customer1 != null)
            {
                if (forgetPasswordDTO.Answer == customer1.CustomerAnswer)
                {
                    customer1.Password = forgetPasswordDTO.NewPassword;
                    _eBS_DbContext.Customer.Update(customer1);
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
                Feedback feedback = new Feedback { Result = false, Message = "Customer Email not registered!" };
                return feedback;
            }
        }

        //Registering as a Customer
        public Feedback Register(CustomerRegDTO customerRegDTO)
        {
            Feedback feedback = null;
            try
            {
                if (customerRegDTO.Password == customerRegDTO.ConfirmPassword)
                {
                    Customer customer = new Customer();
                    //Check if Customer already exists by matching Email, ElectricityBoardId,Contact number
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
                                feedback = new Feedback() { Result = true, Message = "Account Created!" };
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
                else
                {
                    feedback = new Feedback() { Result = false, Message = "Password doesnot match with Confirm Password!" };
                }
            }
            catch (Exception ex)
            {
                feedback = new Feedback() { Result = false, Message = ex.Message };

            }
            return feedback;
        }

        //Validating Login Credentials
        public Customer ValidateCustomer(LoginModel login)
        {
            return _eBS_DbContext.Customer.SingleOrDefault(u => u.CustomerEmail == login.email && u.Password == login.password);
        }

        //Getting Customer details by ID
        public CustomerDTO ViewCustomerById(long CustomerId)
        {
            Customer customer = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerId == CustomerId); ;
            if (customer != null)
            {
                CustomerDTO customerDTO = new CustomerDTO();
                customerDTO.CustomerId = customer.CustomerId;
                customerDTO.Role = customer.Role;
                customerDTO.ElectricityBoardId = customer.ElectricityBoardId;
                customerDTO.CustomerName = customer.CustomerName;
                customerDTO.CustomerType = customer.CustomerType;
                customerDTO.CustomerEmail = customer.CustomerEmail;
                customerDTO.CustomerContactNo = customer.CustomerContactNo;
                customerDTO.CustomerAddress = customer.CustomerAddress;
                if (customerDTO != null)
                {
                    return customerDTO;
                }
                else { return null; }
            }
            else { return null; }
        }

        //Generating JWT Token
        public string GetTokenForCustomer(Customer customer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
                new Claim(ClaimTypes.Name, customer.CustomerEmail.ToString()),
                new Claim(ClaimTypes.Role, customer.Role)
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
    }
}
