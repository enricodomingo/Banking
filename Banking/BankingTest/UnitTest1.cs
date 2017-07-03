using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Banking.Facade;
using Banking.Controllers;
using Banking.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public List<Account> SetAccount()
        {
            var _account = new List<Account>();
            _account.Add(new Account
            {
                AccountName = "sample",
                Id = 1,
                AccountNumber = "123",
                Password = "sample"
            });
            _account.Add(new Account
            {
                AccountName = "samples",
                Id = 2,
                AccountNumber = "1234",
                Password = "samples"
            });
            _account.Add(new Account
            {
                AccountName = "sampless",
                Id = 3,
                AccountNumber = "12345",
                Password = "sampless"
            });
            return _account;
        }

        [TestMethod]
        public async Task TestForDeposit()
        {
            var test = SetAccount();
            Mock<IAccount> mock = new Mock<IAccount>();
            var testController = new AccountController(mock.Object);

            foreach(var account in test)
            {
                mock.Setup(s => s.GetAccountById(account.Id)).Returns(account);
                IActionResult result = await testController.Deposit(account, account.Id, 100, account.Password);
                Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            }

        }

        [TestMethod]
        public async Task TestforNewAccount()
        {
            var test = SetAccount();
            Mock<IAccount> mock = new Mock<IAccount>();
            var testController = new AccountController(mock.Object);
            foreach (var account in test)
            {
                mock.Setup(s => s.GetAccountById(account.Id)).Returns(account);
                IActionResult result = await testController.NewAccount(account);
                Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            }
        }

        [TestMethod]
        public async Task TestForWithdraw()
        {
            var test = SetAccount();
            Mock<IAccount> mock = new Mock<IAccount>();
            var testController = new AccountController(mock.Object);
            foreach (var account in test)
            {
                mock.Setup(s => s.GetAccountById(account.Id)).Returns(account);
                IActionResult result = await testController.Withdraw(account.Id, 100, account.Password);
                Assert.IsInstanceOfType(result, typeof(ViewResult));
            }
        }

        [TestMethod]
        public async Task TestForTransfer()
        {
            var test = SetAccount();
            Mock<IAccount> mock = new Mock<IAccount>();
            mock.As<IAccount>();
            var testController = new AccountController(mock.Object);
            foreach (var account in test)
            {
                mock.Setup(s => s.GetAccountById(account.Id)).Returns(account);
                IActionResult result = await testController.Transfer(account.Id, account.AccountNumber, 100, account.Password);
                Assert.IsInstanceOfType(result, typeof(ViewResult));
            }
                
        }
    }
}
