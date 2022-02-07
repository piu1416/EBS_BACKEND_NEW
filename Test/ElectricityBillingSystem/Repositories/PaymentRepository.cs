using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricityBillingSystem.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private EBS_DbContext _eBS_DbContext;

        //Constructor
        public PaymentRepository(EBS_DbContext eBS_PaymentDBContext)
        {
            _eBS_DbContext = eBS_PaymentDBContext;
        }
        //Getting List of Payment for a specific Customer
        public List<Payment> GetPaymentByCustomerId(long customerId)
        {
            return _eBS_DbContext.Payment.Where(b => b.CustomerId == customerId).ToList();
        }

        //Getting payment deatils using  ID
        public Payment GetPaymentById(long paymentId)
        {
            return _eBS_DbContext.Payment.SingleOrDefault(b => b.PaymentId == paymentId);
        }

        //Getting list of all the payments
        public List<Payment> GetPayments()
        {
            return _eBS_DbContext.Payment.ToList();
        }

        //Making a payment
        public Feedback MakePayment(MakePaymentDTO makePaymentDTO)
        {
            Payment payment = new Payment();
            Feedback feedback = null;
            try
            {
                Bill bill = _eBS_DbContext.Bill.SingleOrDefault(s => s.BillId == makePaymentDTO.BillId);
                if (bill != null && bill.CustomerId == makePaymentDTO.CustomerId)
                {

                    //Check if Payment already exists by matching BillId
                    Payment payment1 = _eBS_DbContext.Payment.SingleOrDefault(s => s.BillId == makePaymentDTO.BillId);
                    if (payment1 == null)
                    {
                        //Add Payment
                        payment.BillId = makePaymentDTO.BillId;
                        payment.CustomerId = makePaymentDTO.CustomerId;
                        payment.BillAmount = bill.BillAmount;
                        payment.PaymentDate = DateOnly(DateTime.Now);
                        payment.PaymentMethod = makePaymentDTO.PaymentMethod;
                        _eBS_DbContext.Payment.Add(payment);
                        _eBS_DbContext.SaveChanges();
                        bill.PaymentStatus = PaymentStatus.PROCESSING.ToString();
                        _eBS_DbContext.Bill.Update(bill);
                        _eBS_DbContext.SaveChanges();
                        feedback = new Feedback() { Result = true, Message = "Payment Added" };
                    }
                    else
                    {
                        feedback = new Feedback() { Result = false, Message = "Payment for this Bill already Done!" };

                    }
                }
                else { feedback = new Feedback() { Result = false, Message = "Invalid Bill ID!" }; }
            }
            catch (Exception ex)
            {
                feedback = new Feedback() { Result = false, Message = ex.Message };

            }
            return feedback;
        }

        //Custom Date Formatter
        private string DateOnly(DateTime dateTime)
        {
            string s = dateTime.ToString("dd MMM yyyy");
            return s;

        }
    }
}
