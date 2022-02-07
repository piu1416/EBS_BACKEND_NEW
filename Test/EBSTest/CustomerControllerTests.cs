using ElectricityBillingSystem.Controllers;
using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace EBSTest
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private Mock<ICustomerRepository> mockCustomerRepo;
        private Mock<IBillRepository> mockBillRepo;
        private Mock<IPaymentRepository> mockPaymentRepo;

        public CustomerControllerTests()
        {
            mockCustomerRepo = new Mock<ICustomerRepository>();
            mockBillRepo = new Mock<IBillRepository>();
            mockPaymentRepo = new Mock<IPaymentRepository>();
        }

        [Test]
        public void GetAllBillsCustomerTest()
        {
            //Arrange
            var bills = new List<Bill> {
                new Bill(){ BillId = 1, CustomerName = "Akash",CustomerType = "Household",
                                PaymentStatus = "Due",Units =50,
                                CustomerId=1,ElectricityBoardId =1
                },
                new Bill(){ BillId = 2, CustomerName = "Sunit",CustomerType = "Household",
                                PaymentStatus = "Due",Units =1000,
                                CustomerId=2,ElectricityBoardId =2
                }
            };
            //var billDTO = new List<BillDTO>() {};
            mockBillRepo.Setup(x => x.GetAllBillsCustomer(1)).Returns(bills);
            var controller = new CustomerController(mockCustomerRepo.Object, mockBillRepo.Object, mockPaymentRepo.Object);

            //Act
            IActionResult result = controller.GetAllBillsCustomer(1);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetPaymentByCustomerIdTest()
        {
            //Arrange
            var payments = new List<Payment> {
                new Payment(){ PaymentId=1, BillId = 1, CustomerId=1, BillAmount= 100,PaymentMethod= "CARD" },
            new Payment() { PaymentId = 2, BillId = 2, CustomerId = 2, BillAmount = 300, PaymentMethod = "CARD" }
        };
            var paymentsDTO = new List<PaymentDTO>() { };
            mockPaymentRepo.Setup(x => x.GetPaymentByCustomerId(1)).Returns(payments);
            var controller = new CustomerController(mockCustomerRepo.Object, mockBillRepo.Object, mockPaymentRepo.Object);

            //Act
            IActionResult result = controller.GetPaymentByCustomerId(1);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetBillByIdTest()
        {
            //Arrange
            var bill = new List<Bill> {
                new Bill(){ BillId = 1, CustomerName = "Akash",CustomerType = "Household",
                                PaymentStatus = "Due",Units =50,
                                CustomerId=1,ElectricityBoardId =1 },
            new Bill() { BillId = 2, CustomerName = "Sunit",CustomerType = "Household",
                                PaymentStatus = "Due",Units =1000,
                                CustomerId=2,ElectricityBoardId =2 }
        };
            var billDTO = new BillDTO()
            {
            };
            mockBillRepo.Setup(x => x.GetBillById(1)).Returns(billDTO);
            var controller = new CustomerController(mockCustomerRepo.Object, mockBillRepo.Object, mockPaymentRepo.Object);

            //Act
            IActionResult result = controller.GetBillById(1);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}
