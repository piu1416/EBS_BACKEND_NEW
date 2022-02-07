using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricityBillingSystem.Repositories
{
    public class BillRepository : IBillRepository
    {
        private EBS_DbContext _eBS_DbContext;
        //Constructor
        public BillRepository(EBS_DbContext billDatabaseContext)
        {
            _eBS_DbContext = billDatabaseContext;
        }

        //Creating bill for customer
        public Feedback CreateBill(CreateBillDTO createBillDTO)
        {
            Feedback feedback = null;
            try
            {
                Bill bill = new Bill();
                Customer customer = _eBS_DbContext.Customer.SingleOrDefault(s => s.CustomerId == createBillDTO.CustomerID);
                if (customer != null)
                {
                    if (createBillDTO.Units > 0)
                    {
                        bill.Units = createBillDTO.Units;
                        bill.DuePaymentDate = DateOnly(DateTime.Now.AddDays(30));
                        bill.PaymentStatus = PaymentStatus.DUE.ToString();
                        bill.BillAmount = CalculateBillAmount(createBillDTO.Units, customer.CustomerType);
                        bill.CustomerId = createBillDTO.CustomerID;
                        bill.CustomerName = customer.CustomerName;
                        bill.CustomerAddress = customer.CustomerAddress;
                        bill.CustomerType = customer.CustomerType;
                        bill.ElectricityBoardId = customer.ElectricityBoardId;
                        bill.isPaid = false;
                        bill.monthAndYearOfBill = monthAndYearOfBill();
                        _eBS_DbContext.Bill.Add(bill);
                        _eBS_DbContext.SaveChanges();
                        feedback = new Feedback() { Result = true, Message = "Bill Added" };
                    }
                    else
                    {
                        feedback = new Feedback() { Result = false, Message = "Units can not be negative!" };
                    }
                }
                else
                {
                    feedback = new Feedback() { Result = false, Message = "Invalid Customer ID !" };
                }
            }
            catch (Exception ex)
            {
                feedback = new Feedback() { Result = false, Message = ex.Message };
            }
            return feedback;
        }

        //Getting all Bills
        public List<Bill> GetAllBillsAdmin()
        {
            return _eBS_DbContext.Bill.ToList();
        }

        //Getting all Bills for a specific customer
        public List<Bill> GetAllBillsCustomer(long CustomerId)
        {
            return _eBS_DbContext.Bill.Where(b => b.CustomerId == CustomerId).ToList();
        }

        //Getting a Bills by its ID
        public BillDTO GetBillById(long billId)
        {

            Bill bill = _eBS_DbContext.Bill.SingleOrDefault(b => b.BillId == billId);
            if (bill != null)
            {
                BillDTO billDTO = new BillDTO();
                billDTO.ElectricityBoardId = bill.ElectricityBoardId;
                billDTO.CustomerName = bill.CustomerName;
                billDTO.PaymentStatus = bill.PaymentStatus;
                billDTO.Amount = bill.BillAmount;
                billDTO.DuePaymentDate = bill.DuePaymentDate;
                billDTO.BillId = bill.BillId;
                billDTO.CustomerID = bill.CustomerId;
                billDTO.CustomerType = bill.CustomerType;
                billDTO.Units = bill.Units;
                billDTO.isPaid = bill.isPaid;
                return billDTO;
            }
            else { return null; }
        }

        //Updating payment details to bill
        public Feedback UpdatePaymentStatus(UpdatePaymentDTO updatePaymentDTO)
        {
            Feedback feedback = null;
            try
            {
                Bill bill = _eBS_DbContext.Bill.SingleOrDefault(s => s.BillId == updatePaymentDTO.BillId);
                if (bill != null)
                {
                    Payment payment = _eBS_DbContext.Payment.SingleOrDefault(s => s.PaymentId == updatePaymentDTO.PaymentId);
                    if (payment != null)
                    {
                        if (payment.BillId == updatePaymentDTO.BillId)
                        {
                            bill.PaymentId = payment.PaymentId;
                            bill.PaymentStatus = updatePaymentDTO.PaymentStatus;
                            bill.PaymentMethod = payment.PaymentMethod;
                            bill.PaymentDate = payment.PaymentDate;
                            bill.isPaid = true;
                            _eBS_DbContext.Bill.Update(bill);
                            _eBS_DbContext.SaveChanges();
                            payment.isUpdatedtoBill = true;
                            _eBS_DbContext.Payment.Update(payment);
                            _eBS_DbContext.SaveChanges();
                            feedback = new Feedback() { Result = true, Message = "Payment Updated!" };
                        }
                        else { feedback = new Feedback() { Result = false, Message = "Payment doesnot belongs to this BillID!" }; }
                    }
                    else
                    { feedback = new Feedback() { Result = false, Message = "Invalid Payment ID!" }; }

                }
                else
                {
                    feedback = new Feedback() { Result = false, Message = "Invalid Bill ID!" };
                }
            }
            catch (Exception ex)
            {
                feedback = new Feedback() { Result = false, Message = ex.Message };
            }
            return feedback;
        }

        //Calculating Bill amount for different customer types
        public double CalculateBillAmount(double units, string customerType)
        {
            double billAmount = 0;
            if (customerType == "HOUSEHOLD" && units != 0)
            {
                billAmount = units * 4;
            }
            else
            {
                billAmount = units * 7;
            }
            return billAmount;
        }

        //Month and year of the Bill
        public string monthAndYearOfBill()
        {
            DateTime dt = DateTime.Now;
            string s = dt.ToString("MMM yyyy");
            return s;

        }

        //Custom Date formatter
        private string DateOnly(DateTime dateTime)
        {
            string s = dateTime.ToString("dd MMM yyyy");
            return s;

        }
    }
}
