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
    [Authorize(Roles = "ADMIN,CUSTOMER")]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private IBillRepository _billRepository;
        private IPaymentRepository _paymentRepository;
        //Constructor
        public CustomerController(ICustomerRepository customerRepository, IBillRepository billRepository, IPaymentRepository paymentRepository)
        {
            _customerRepository = customerRepository;
            _billRepository = billRepository;
            _paymentRepository = paymentRepository;
        }

        [HttpGet]
        [Route("ViewCustomer/{customerId}")]
        public IActionResult ViewCustomer(long customerId)
        {
            CustomerDTO customerDTO = _customerRepository.ViewCustomerById(customerId);
            if (customerDTO == null)
            {
                return NotFound("Invalid Customer ID");

            }
            return Ok(customerDTO);
        }

        [HttpGet]
        [Route("GetAllBillsCustomer/{customerId}")]
        public IActionResult GetAllBillsCustomer(long customerId)
        {
            try
            {
                List<Bill> bills = _billRepository.GetAllBillsCustomer(customerId);
                if (bills != null) { return Ok(bills); }
                else { return NotFound("No Bill Data Available!"); }
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
                BillDTO billDTO = _billRepository.GetBillById(billId);
                return Ok(billDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPaymentByCustomerId/{customerId}")]
        public IActionResult GetPaymentByCustomerId(long customerId)
        {
            try
            {
                return Ok(_paymentRepository.GetPaymentByCustomerId(customerId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("MakePayment")]
        public IActionResult MakePayment([FromBody] MakePaymentDTO makePaymentDTO)
        {
            try
            {
                Feedback feedback = _paymentRepository.MakePayment(makePaymentDTO);
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
                    Feedback feedback = _customerRepository.ChangePassword(changePasswordDTO);
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

    }
}
