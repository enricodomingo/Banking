using System;
using Microsoft.AspNetCore.Mvc;
using Banking.Models;
using Banking.Facade;
using Banking.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Banking.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _Iaccount;


        public AccountController(IAccount Iaccount)
        {
            _Iaccount = Iaccount;
        }

        public async Task<ViewResult> Accounts()
        {
            AccountViewModel accountsViewModel = new AccountViewModel();
            accountsViewModel.Accounts = _Iaccount.Accounts;

            return View(accountsViewModel);
        }

        public async Task<ViewResult> Deposit(int id)
        {
            var _account = _Iaccount.GetAccountById(id);
            return View(_account);
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(Account account, int id, decimal amount, string password)
        {
            var _account = _Iaccount.GetAccountById(id);
            if (_account.Password != password)
            {
                ModelState.AddModelError("Password", "Invalid Password!");
            }
            else if (amount <= 0)
            {
                ModelState.AddModelError("Balance", "Minimum deposit amount is P100.00");
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    _Iaccount.Deposit(id, amount);
                    return RedirectToAction("Accounts");
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return View(_account);
        }

        public async Task<ViewResult> Transfer(int id)
        {
            TransferViewModel transferViewModel = new TransferViewModel();
            transferViewModel.Account = _Iaccount.GetAccountById(id);
            return View(transferViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(int id, string transferAccount, decimal amount, string password)
        {
            var _account = _Iaccount.GetAccountById(id);
            var _transferTo = _Iaccount.GetAccountByAccountNumber(transferAccount);
            TransferViewModel transferViewModel = new TransferViewModel();
            transferViewModel.Account = _Iaccount.GetAccountById(id);

            if (transferViewModel.Account.Password != password)
            {
                ModelState.AddModelError("Account.Password", "Invalid Password!");
            }
            else if (_account.Balance < amount)
            {
                ModelState.AddModelError("Account.Balance", "Not enough Balance to make Transfer!");
            }
            else if (amount <= 0)
            {
                ModelState.AddModelError("Account.Balance", "Minimum transfer amount is P100.00");
            }
            else if (_transferTo == null)
            {
                ModelState.AddModelError("TransferToAccount", "Transfer To Account Number does not exists!");
            }
            else if (transferAccount == "")
            {
                ModelState.AddModelError("TransferToAccount", "Please enter account to transfer!");
            }
            else if (ModelState.IsValid)
            {
                _Iaccount.Transfer(id, amount, transferAccount);

                return RedirectToAction("Accounts");
            }
            return View(transferViewModel);
        }

        public async Task<ViewResult> Withdraw(int id)
        {
            var account = _Iaccount.GetAccountById(id);
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(int id, decimal amount, string password)
        {
            var _account = _Iaccount.GetAccountById(id);
            if (_account.Password != password)
            {
                ModelState.AddModelError("Password", "Invalid Password");
            }
            else if (_account.Balance < amount)
            {
                ModelState.AddModelError("Balance", "Not enough balance to make withdrawal.");
            }
            else if (amount <= 0)
            {
                ModelState.AddModelError("Balance", "Minimum withdrawal amount is P100.00");
            }
            else if (amount > 20001)
            {
                ModelState.AddModelError("Balance", "Maximum withdrawal amount is P20,000.00");
            }
            else if (ModelState.IsValid)
            {
                _Iaccount.Withdraw(id, amount);
                return RedirectToAction("Accounts");
            }
            return View(_account);
        }

        public async Task<IActionResult> NewAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewAccount(Account account)
        {
            if (ModelState.IsValid)
            {
                _Iaccount.NewAccount(account);
                return RedirectToAction("Accounts");
            }
            return View();
        }


    }
}