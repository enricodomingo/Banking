using Banking.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Banking.Facade
{
    public interface ITransaction
    {
        Transaction GetTransactionById(int transactionId);
        IEnumerable<Transaction> Transactions { get; }
        void AddTransaction(Account account, decimal amount, string transactionType);
        IEnumerable<Transaction> GetTransactionsByAccountNumber(string accountNumber);
    }

    public class TransactionFacade : ITransaction
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionFacade(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Transaction GetTransactionById(int transactionId)
        {
            return _dbContext.Transactions.FirstOrDefault(t => t.Id == transactionId);
        }

        public IEnumerable<Transaction> Transactions
        {
            get { return _dbContext.Transactions; }
        }

        public void AddTransaction(Account account, decimal amount, string transactionType)
        {
            Transaction transaction = new Transaction
            {
                AccountNumber = account.AccountNumber,
                Amount = amount,
                TransactionType = transactionType,
                CreatedDate = DateTime.Now
            };
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Transaction> GetTransactionsByAccountNumber(string accountNumber)
        {
            return _dbContext.Transactions.Where(t => t.AccountNumber == accountNumber);
        }
    }
}
