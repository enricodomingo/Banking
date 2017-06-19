using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Banking.Models
{
    public class Account
    {
        [BindNever]
        public int Id { get; set; }

        [Display(Name="Account Number")]
        [Required(ErrorMessage="Please enter account number!")]
        public string AccountNumber { get; set; }

        [Display(Name="Account Name")]
        [Required(ErrorMessage="Please enter account name!")]
        public string AccountName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage="Please enter password!")]
        public string Password { get; set; }

        [Display(Name="Balance")]
        [ConcurrencyCheck]
        public decimal Balance { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
