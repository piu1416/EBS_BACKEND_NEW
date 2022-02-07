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
    public class AdminControllerTests
    {
        private Mock<IAdminRepository> mockAdminRepo;
        private Mock<IBillRepository> mockBillRepo;
        private Mock<IPaymentRepository> mockPaymentRepo;

        public AdminControllerTests()
        {
            mockAdminRepo = new Mock<IAdminRepository>();
            mockBillRepo = new Mock<IBillRepository>();
            mockPaymentRepo = new Mock<IPaymentRepository>();
        }

        [Test]
        public void GetAllCustomersTest()
        {
            //Arrange
            var customers = new List<Customer>() {
                new Customer(){ CustomerId = 1, CustomerName = "Akash",Role = "CUSTOMER",CustomerQuestion = "Nickname",
                                CustomerAnswer = "ZZ",CustomerEmail = "aakash@ebs.com",Password = "admin@123",
                                CustomerContactNo = "7978025340",CustomerAddress ="Mumbai",ElectricityBoardId =1
                },
                new Customer(){ CustomerId = 1, CustomerName = "Akash",Role = "CUSTOMER",CustomerQuestion = "Nickname",
                                CustomerAnswer = "ZZ",CustomerEmail = "aakash@ebs.com",Password = "admin@123",
                                CustomerContactNo = "7978025340",CustomerAddress ="Mumbai",ElectricityBoardId =1
                }
            };
            var customerDTO = new List<CustomerDTO>() {
                new CustomerDTO(){ CustomerId = 1, CustomerName = "Akash",Role = "CUSTOMER",CustomerEmail = "aakash@ebs.com",
                                CustomerContactNo = "7978025340",CustomerAddress ="Mumbai",ElectricityBoardId =1
                },
                new CustomerDTO(){ CustomerId = 1, CustomerName = "Akash",Role = "CUSTOMER",CustomerEmail = "aakash@ebs.com",
                                CustomerContactNo = "7978025340",CustomerAddress ="Mumbai",ElectricityBoardId =1
                }
            };
            mockAdminRepo.Setup(x => x.GetAllCustomers()).Returns(customerDTO);
            var controller = new AdminController(mockAdminRepo.Object, mockBillRepo.Object, mockPaymentRepo.Object);

            //Act
            IActionResult result = controller.GetAllCustomers();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetAllBillsTest()
        {
            //Arrange
            var bill = new List<Bill> {
                new Bill(){ BillId = 1, CustomerName = "Akash",CustomerType = "Household",
                                PaymentStatus = "Due",Units =50,
                                CustomerId=1,ElectricityBoardId =1
                },
                new Bill(){ BillId = 1, CustomerName = "Akash",CustomerType = "Household",
                                PaymentStatus = "Due",Units =50,
                                CustomerId=1,ElectricityBoardId =1
                }
            };
            var billDTO = new List<BillDTO>()
            {
                //new BillDTO(){ CustomerId = 1, CustomerName = "Akash",Role = "CUSTOMER",CustomerEmail = "aakash@ebs.com",
                //                CustomerContanctNo = "7978025340",CustomerAddress ="Mumbai",ElectricityBoardId =1
                //},
                //new BillDTO(){ CustomerId = 1, CustomerName = "Akash",Role = "CUSTOMER",CustomerEmail = "aakash@ebs.com",
                //                CustomerContanctNo = "7978025340",CustomerAddress ="Mumbai",ElectricityBoardId =1
                //}
            };
            mockBillRepo.Setup(x => x.GetAllBillsAdmin()).Returns(bill);
            var controller = new AdminController(mockAdminRepo.Object, mockBillRepo.Object, mockPaymentRepo.Object);

            //Act
            IActionResult result = controller.GetAllBills();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetCustomerByIdTest()
        {
            //Arrange
            var customers = new List<Customer>() {
                new Customer(){ CustomerId = 1, CustomerName = "Akash",Role = "CUSTOMER",CustomerQuestion = "Nickname",
                                CustomerAnswer = "ZZ",CustomerEmail = "aakash@ebs.com",Password = "admin@123",
                                CustomerContactNo = "7978025340",CustomerAddress ="Mumbai",ElectricityBoardId =1
                },
                new Customer(){ CustomerId = 2, CustomerName = "Akash",Role = "CUSTOMER",CustomerQuestion = "Nickname",
                                CustomerAnswer = "ZZ",CustomerEmail = "aakash@ebs.com",Password = "admin@123",
                                CustomerContactNo = "7978025340",CustomerAddress ="Mumbai",ElectricityBoardId =1
                }
            };
            var customerDTO = new Customer()
            {

            };
            mockAdminRepo.Setup(x => x.GetCustomerById(1)).Returns(customerDTO);
            var controller = new AdminController(mockAdminRepo.Object, mockBillRepo.Object, mockPaymentRepo.Object);

            //Act
            IActionResult result = controller.GetCustomerById(1);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetBillByIdTest()
        {
            //Arrange
            var bill = new List<Bill>() {
                new Bill(){ CustomerName ="Parth", CustomerAddress="XYZ" , BillAmount = 230 , BillId = 12 , CustomerId = 123 , CustomerType="Household" , ElectricityBoardId = 111


                },
                new Bill(){CustomerName ="Parth", CustomerAddress="XYZ" , BillAmount = 230 , BillId = 12 , CustomerId = 123 , CustomerType="Household" , ElectricityBoardId = 111
                }
            };
            var BillDTO = new BillDTO()
            {

            };
            mockBillRepo.Setup(x => x.GetBillById(12)).Returns(BillDTO);
            var controller = new AdminController(mockAdminRepo.Object, mockBillRepo.Object, mockPaymentRepo.Object);

            //Act
            IActionResult result = controller.GetBillById(12);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetAllPaymentsTests()
        {
            //Arrange
            var payment = new List<Payment> {
                new Payment(){ BillId = 1,PaymentMethod = "Household",
                                BillAmount = 500,
                                PaymentId=1
                },
                new Payment(){ BillId = 1,PaymentMethod = "Household",
                                BillAmount = 500,
                                PaymentId=1
                }
            };
            var paymentDTO = new List<PaymentDTO>()
            {
                //new Payment(){ BillId = 1,PaymentMethod = "Household",
                //                 BillAmount = 500,
                //                 PaymentId=1
                // },
                // new Payment(){ BillId = 1,PaymentMethod = "Household",
                //                 BillAmount = 500,
                //                 PaymentId=1
                // }
            };
            mockPaymentRepo.Setup(x => x.GetPayments()).Returns(payment);
            var controller = new AdminController(mockAdminRepo.Object, mockBillRepo.Object, mockPaymentRepo.Object);

            //Act
            IActionResult result = controller.GetAllPayments();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetPaymentByIdTest()
        {
            //Arrange
            var payment = new List<Payment>() {
                new Payment(){  PaymentId = 1 , PaymentMethod = "Cash", BillAmount = 230 , BillId = 12 , CustomerId = 123 },
                new Payment(){ PaymentId = 1 , PaymentMethod = "Cash", BillAmount = 230 , BillId = 12 , CustomerId = 123 }

            };
            var PaymentDTO = new Payment()
            {

            };
            mockPaymentRepo.Setup(x => x.GetPaymentById(1)).Returns(PaymentDTO);
            var controller = new AdminController(mockAdminRepo.Object, mockBillRepo.Object, mockPaymentRepo.Object);

            //Act
            IActionResult result = controller.GetPaymentById(1);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetBillsByCustomerIdTest()
        {
            //Arrange
            var bill = new List<Bill>() {
                new Bill(){ CustomerName ="Parth", CustomerAddress="XYZ" , BillAmount = 230 , BillId = 12 , CustomerId = 123 , CustomerType="Household" , ElectricityBoardId = 111


                },
                new Bill(){CustomerName ="Parth", CustomerAddress="XYZ" , BillAmount = 230 , BillId = 12 , CustomerId = 123 , CustomerType="Household" , ElectricityBoardId = 111
                }
            };
            var billDTO = new List<BillDTO>()
            {

            };
            mockBillRepo.Setup(x => x.GetAllBillsCustomer(123)).Returns(bill);
            var controller = new AdminController(mockAdminRepo.Object, mockBillRepo.Object, mockPaymentRepo.Object);

            //Act
            IActionResult result = controller.GetBillsByCustomerId(123);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}
