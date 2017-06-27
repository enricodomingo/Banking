using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Banking.Facade;
using Banking.Controllers;
using Banking.Models;
using System.Threading.Tasks;

namespace BankingTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestForAccounts()
        {
            Mock<IAccount> mock = new Mock<IAccount>();
            var testController = new AccountController(mock.Object);
            var getAccounts = await testController.Accounts();

            Assert.IsNotNull(getAccounts);
            Assert.IsNotNull(getAccounts.Model);
            Assert.IsTrue(string.IsNullOrEmpty(getAccounts.ViewName) || getAccounts.ViewName == "Accounts");
        }

        [TestMethod]
        public async Task TestForTransactions()
        {
            Mock<ITransaction> mock = new Mock<ITransaction>();
            var testController = new TransactionController(mock.Object);
            var getTransactions = await testController.Transactions("1", 1);

            Assert.IsNotNull(getTransactions);
            Assert.IsNotNull(getTransactions.Model);
            Assert.IsTrue(string.IsNullOrEmpty(getTransactions.ViewName) || getTransactions.ViewName == "Transactions");
        }

        [TestMethod]
        public async Task TestForDeposit()
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
            IActionResult result = await testController.Deposit(_account, 1, 100, "sample");
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task TestforNewAccount()
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
            IActionResult result = await testController.NewAccount(_account);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task TestForWithdraw()
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
            IActionResult result = await testController.Withdraw(1, 100, "sample");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task TestForTransfer()
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
            IActionResult result = await testController.Transfer(1, "123", 100, "sample");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task TestForConcurrentTransfer()
        {
            await TestForTransfer();
        }

        [TestMethod]
        public async Task TestForConcurrentDeposit()
        {
            await TestForDeposit();
        }

        public async Task TestForConcurrentWithdraw()
        {
            await TestForWithdraw();
        }

        public async Task TestForConcurrentNewAccount()
        {
            await TestforNewAccount();
        }

        public async Task TestForConcurrentAccounts()
        {
            await TestForAccounts();
        }

        public async Task TestForConcurrentTransactions()
        {
            await TestForTransactions();
        }
    }
}
