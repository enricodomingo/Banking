using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Banking.Models;
using System.Threading.Tasks;

namespace Banking.Facade
{
    public interface IAccount
    {
        Account GetAccountById(int accountId);
        Account GetAccountByAccountNumber(string accountNumber);
        Task<IEnumerable<Account>> Accounts { get; }
        void NewAccount(Account account);
        void Deposit(int id, decimal amount);
        void Withdraw(int id, decimal amount);
        void Transfer(int id, decimal amount, string accountNumber);
    }

    public class AccountFacade : IAccount
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITransaction _Itransaction;

        public AccountFacade(ApplicationDbContext dbContext, ITransaction Itransaction)
        {
            _dbContext = dbContext;
            _Itransaction = Itransaction;
        }

        public Account GetAccountByAccountNumber(string accountNumber)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public Account GetAccountById(int accountId)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId);
        }

        public Task<IEnumerable<Account>> Accounts
        {
            get {
                var assets = Task.Factory.StartNew(() => (IEnumerable<Account>)_dbContext.Accounts);
                return assets;
            }
        }

        public void NewAccount(Account account)
        {
            try
            {
                if (account != null)
                {
                    account.CreatedDate = DateTime.Now;
                    _dbContext.Accounts.Add(account);
                    _dbContext.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Deposit(int id, decimal amount)
        {
            try
            {
                var account = GetAccountById(id);
                if (account != null)
                {
                    account.Balance += amount;
                    _Itransaction.AddTransaction(account, amount, "Deposit");
                    _dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Account Number does not exists!");
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Withdraw(int id, decimal amount)
        {
            try
            {
                var account = GetAccountById(id);
                if (account != null)
                {
                    account.Balance -= amount;
                    _Itransaction.AddTransaction(account, amount, "Withdraw");
                    _dbContext.SaveChanges();
                }
                 else
                {
                    Console.WriteLine("Account Number does not exists!");
                }
                
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Transfer(int id, decimal amount, string accountNumber)
        {
            try
            {
                var transferfromAccount = GetAccountById(id);
                var transfertoAccount = GetAccountByAccountNumber(accountNumber);
                if ((transferfromAccount != null) && (transfertoAccount != null))
                {
                    transferfromAccount.Balance -= amount;
                    transfertoAccount.Balance += amount;
                    _Itransaction.AddTransaction(transferfromAccount, amount, "Transfer");
                    _dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Account Number does not exists!");
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
