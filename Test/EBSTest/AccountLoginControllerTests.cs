using ElectricityBillingSystem.Controllers;
using ElectricityBillingSystem.DTO;
using ElectricityBillingSystem.Entities;
using ElectricityBillingSystem.Models;
using ElectricityBillingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EBSTest
{
    public class AccountLoginControllerTests
    {
        private Mock<IAdminRepository> mockAdminRepo;
        private Mock<ICustomerRepository> mockCustomerRepo;

        public AccountLoginControllerTests()
        {
            mockAdminRepo = new Mock<IAdminRepository>();
            mockCustomerRepo = new Mock<ICustomerRepository>();
        }

        [Test]
        public void LoginTest()
        {
            //Arrange
            var admin = new Admin()
            {
                AdminId = 1,
                AdminName = "Akash",
                Role = "ADMIN",
                AdminQuestion = "Nickname",
                AdminAnswer = "ZZ",
                AdminEmail = "aakash@ebs.com",
                Password = "admin@123",
                AdminContactNo = "7978025340"
            };
            var customer = new Customer()
            {
                CustomerId = 1,
                CustomerName = "Akash",
                Role = "CUSTOMER",
                CustomerQuestion = "Nickname",
                CustomerAnswer = "ZZ",
                CustomerEmail = "aakash@ebs.com",
                Password = "admin@123",
                CustomerContactNo = "7978025340",
                CustomerAddress = "Mumbai",
                ElectricityBoardId = 1
            };
            LoginModel model = new LoginModel { email = "aakash@ebs.com", password = "admin@123" };
            mockAdminRepo.Setup(x => x.ValidateAdmin(model)).Returns(admin);
            var controller = new AccountLogInController(mockCustomerRepo.Object, mockAdminRepo.Object);

            //Act
            IActionResult result = controller.Login(model);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void ForgetPasswordTest()
        {
            //Arrange
            var admin = new Admin()
            {
                AdminId = 1,
                AdminName = "Akash",
                Role = "ADMIN",
                AdminQuestion = "Nickname",
                AdminAnswer = "ZZ",
                AdminEmail = "aakash@ebs.com",
                Password = "admin@123",
                AdminContactNo = "7978025340"
            };
            var customer = new Customer()
            {
                CustomerId = 1,
                CustomerName = "Akash",
                Role = "CUSTOMER",
                CustomerQuestion = "Nickname",
                CustomerAnswer = "ZZ",
                CustomerEmail = "aakash@ebs.com",
                Password = "admin@123",
                CustomerContactNo = "7978025340",
                CustomerAddress = "Mumbai",
                ElectricityBoardId = 1
            };

            var feedback = new Feedback() { Result = true, Message = "Password has been reset!" };
            ForgetPasswordDTO model = new ForgetPasswordDTO { Role = "ADMIN", Email = "aakash@ebs.com", Answer = "GG", NewPassword = "admin@123", ConfirmPassword = "admin@123" };
            mockAdminRepo.Setup(x => x.ForgetPassword(model)).Returns(feedback);
            //mockCustomerRepo.Setup(x => x.ForgetPassword("SUNI@ebs.com", model)).Returns(feedback);
            var controller = new AccountLogInController(mockCustomerRepo.Object, mockAdminRepo.Object);

            //Act
            IActionResult result = controller.ForgetPassword(model);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void CustomerRegistrationTest()
        {
            //Arrange
            var customer = new CustomerRegDTO()
            {
                CustomerName = "Akash",
                CustomerType = "HOUSEHOLD",
                CustomerQuestion = "Nickname",
                CustomerAnswer = "ZZ",
                CustomerEmail = "aakash@ebs.com",
                Password = "admin@123",
                CustomerContactNo = "7978025340",
                CustomerAddress = "Mumbai",
                ElectricityBoardId = 1
            };
            var feedback = new Feedback() { Result = true, Message = "Account Created!" };
            mockCustomerRepo.Setup(x => x.Register(customer)).Returns(feedback);
            var controller = new AccountLogInController(mockCustomerRepo.Object, mockAdminRepo.Object);

            //Act
            IActionResult result = controller.CustomerRegistration(customer);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}