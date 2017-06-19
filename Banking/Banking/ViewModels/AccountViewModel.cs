using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Models;

namespace Banking.ViewModels
{
    public class AccountViewModel
    {
        public IEnumerable<Account> Accounts { get; set; }
        public Account Account { get; set; }
    }
}
