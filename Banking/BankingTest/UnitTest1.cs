using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Banking.Facade;
using Banking.Controllers;
using Banking.Models;

namespace BankingTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestForAccounts()
        {
            Mock<IAccount> mock = new Mock<IAccount>();
            var testController = new AccountController(mock.Object);
            var getAccounts = testController.Accounts();

            Assert.IsNotNull(getAccounts);
            Assert.IsNotNull(getAccounts.Model);
            Assert.IsTrue(string.IsNullOrEmpty(getAccounts.ViewName) || getAccounts.ViewName == "Accounts");
        }

        [TestMethod]
        public void TestForDeposit()
        {
            Mock<IAccount> mock = new Mock<IAccount>();
            var testController = new AccountController(mock.Object);
            Account _account = new Account()
            {
                AccountName = "sample",
                Id = 1,
                AccountNumber = "123",
                Password = "sample"
            };

            mock.Setup(s => s.GetAccountById(1)).Returns(_account);
            IActionResult result = testController.Deposit(_account, 1, 100, "sample");
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void TestForWithdraw()
        {
            Mock<IAccount> mock = new Mock<IAccount>();
            var testController = new AccountController(mock.Object);
            Account _account = new Account()
            {
                AccountName = "sample",
                Id = 1,
                AccountNumber = "123",
                Password = "sample"
            };
            mock.Setup(s => s.GetAccountById(1)).Returns(_account);
            IActionResult result = testController.Withdraw(1, 100, "sample");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestForTransfer()
        {
            Mock<IAccount> mock = new Mock<IAccount>();
            mock.As<IAccount>();
            var testController = new AccountController(mock.Object);
            Account _account = new Account()
            {
                AccountName = "sample",
                Id = 1,
                AccountNumber = "123",
                Password = "sample"
            };
            mock.Setup(s => s.GetAccountById(1)).Returns(_account);
            IActionResult result = testController.Transfer(1, "123", 100, "sample");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
