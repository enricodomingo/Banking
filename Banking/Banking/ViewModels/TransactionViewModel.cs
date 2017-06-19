using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Models;

namespace Banking.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
