using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using ElectricityBillingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ElectricityBillingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMIN")]
    public class AdminController : ControllerBase
    {
        private IAdminRepository _adminRepository;
        private IBillRepository _billRepository;
        private IPaymentRepository _paymentRepository;
        //Constructor
        public AdminController(IAdminRepository adminRepository, IBillRepository billRepository, IPaymentRepository paymentRepository)
        {
            _adminRepository = adminRepository;
            _billRepository = billRepository;
            _paymentRepository = paymentRepository;
        }

        [HttpGet]
        [Route("ViewAdmin/{adminId}")]
        public IActionResult ViewAdmin(long adminId)
        {
            AdminDTO adminDTO = _adminRepository.ViewAdminById(adminId);
            if (adminDTO != null)
            {
                return Ok(adminDTO);
            }
            else
            {
                return BadRequest("Invalid Admin ID");
            }

        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                List<CustomerDTO> customers = _adminRepository.GetAllCustomers();
                if (customers != null) { return Ok(customers); }
                else { return NotFound("No Customer Data Available!"); }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAllBills")]
        public IActionResult GetAllBills()
        {
            try
            {
                List<Bill> bills = _billRepository.GetAllBillsAdmin();
                if (bills != null) { return Ok(bills); }
                else { return NotFound("No Bill Data Available!"); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAllPayments")]
        public IActionResult GetAllPayments()
        {
            try
            {
                List<Payment> payments = _paymentRepository.GetPayments();
                if (payments != null) { return Ok(payments); }
                else { return NotFound("No Payment Data Available!"); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetCustomerById/{customerId}")]
        public IActionResult GetCustomerById(long customerId)
        {
            try
            {
                Customer customer = _adminRepository.GetCustomerById(customerId);
                if (customer != null)
                {
                    return Ok(customer);
                }
                else
                {
                    return NotFound("No such customer Available!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetBillById/{billId}")]
        public IActionResult GetBillById(long billId)
        {
            try
            {
                BillDTO bill = _billRepository.GetBillById(billId);
                if (bill != null)
                {
                    return Ok(bill);
                }
                else
                {
                    return NotFound("No such bill Available!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetBillsByCustomerId/{customerId}")]
        public IActionResult GetBillsByCustomerId(long customerId)
        {
            try
            {
                List<Bill> bills = _billRepository.GetAllBillsCustomer(customerId);
                if (bills != null)
                {
                    return Ok(bills);
                }
                else
                {
                    return NotFound("No such Customer Available!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetPaymentById/{paymentId}")]
        public IActionResult GetPaymentById(long paymentId)
        {
            try
            {
                Payment payment = _paymentRepository.GetPaymentById(paymentId);
                if (payment != null)
                {
                    return Ok(payment);
                }
                else
                {
                    return NotFound("No such payment Available!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetPaymentsByCustomerId/{customerId}")]
        public IActionResult GetPaymentsByCustomerId(long customerId)
        {
            try
            {
                List<Payment> payments = _paymentRepository.GetPaymentByCustomerId(customerId);
                if (payments != null)
                {
                    return Ok(payments);
                }
                else
                {
                    return NotFound("No such Customer Available!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer([FromBody] CustomerRegDTO customerRegDTO)
        {
            Feedback feedback = _adminRepository.AddCustomer(customerRegDTO);
            if (feedback.Result == true) { return Ok(feedback); }
            else { return BadRequest(feedback); }

        }

        [HttpPost]
        [Route("CreateBill")]
        public IActionResult CreateBill([FromBody] CreateBillDTO createBillDTO)
        {
            try
            {
                Feedback feedback = _billRepository.CreateBill(createBillDTO);
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        [HttpPut]
        [Route("UpdateProfile")]
        public IActionResult UpdateProfile([FromBody] UpdateProfileDTO updateProfileDTO)
        {
            try
            {
                Feedback feedback = _adminRepository.UpdateProfile(updateProfileDTO);
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                if (changePasswordDTO.NewPassword == changePasswordDTO.ConfirmPassword)
                {
                    Feedback feedback = _adminRepository.ChangePassword(changePasswordDTO);
                    return Ok(feedback);
                }
                else
                {
                    Feedback feedback = new Feedback { Result = false, Message = "New password doesnot match Confirm password!" };
                    return Ok(feedback);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateProfileForCustomer")]
        public IActionResult UpdateProfileForCustomer([FromBody] UpdateProfileDTO updateProfileDTO)
        {
            try
            {
                if (updateProfileDTO.Email == null || updateProfileDTO.Name == null || updateProfileDTO.ContactNo == null)
                {
                    return BadRequest("Invalid client request");
                }
                Feedback feedback = _adminRepository.UpdateProfileForCustomer(updateProfileDTO);
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdatePaymentStatus")]
        public IActionResult UpdatePaymentStatus([FromBody] UpdatePaymentDTO updatePaymentDTO)
        {
            try
            {
                Feedback feedback = _billRepository.UpdatePaymentStatus(updatePaymentDTO);
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Deleteing a Customer
        [HttpDelete]
        [Route("DeleteCustomer/{customerId}")]
        public IActionResult DeleteCustomer(long customerId)
        {
            if (customerId != 0)
            {
                Feedback feedback = _adminRepository.DeleteCustomer(customerId);
                return Ok(feedback);
            }
            else
            {
                return BadRequest("Customer not entered");
            }

        }

        //Deleteing a Bill
        [HttpDelete]
        [Route("DeleteBill/{billId}")]
        public IActionResult DeleteBill(long billId)
        {
            if (billId != 0)
            {
                Feedback feedback = _adminRepository.DeleteBill(billId);
                return Ok(feedback);
            }
            else
            {
                return BadRequest("Bill not entered");
            }

        }

        //Deleteing a Payment
        [HttpDelete]
        [Route("DeletePayment/{paymentId}")]
        public IActionResult DeletePayment(long paymentId)
        {
            if (paymentId != 0)
            {
                Feedback feedback = _adminRepository.DeletePayment(paymentId);
                return Ok(feedback);
            }
            else
            {
                return BadRequest("Payment not entered");
            }

        }
    }
}
