using Microsoft.AspNetCore.Mvc;
using Banking.Facade;
using Banking.ViewModels;

namespace Banking.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransaction _Itransaction;

        public TransactionController(ITransaction Itransaction)
        {
            _Itransaction = Itransaction;
        }
        public ViewResult Transactions(string accountNumber, int Id)
        {
            TransactionViewModel transaction = new TransactionViewModel();
            transaction.Transactions = _Itransaction.GetTransactionsByAccountNumber(accountNumber);
            transaction.Id = Id;
            return View(transaction);
        }
    }
}