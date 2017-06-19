using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Banking.Models;

namespace Banking.ViewModels
{
    public class TransferViewModel
    {
        public Account Account { get; set; }
        public string TransferToAccount { get; set; }
    }
}
