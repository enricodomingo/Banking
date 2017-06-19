using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Display(Name = "Account number")]
        [Required(ErrorMessage = "Please enter account number!")]
        public string AccountNumber { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
