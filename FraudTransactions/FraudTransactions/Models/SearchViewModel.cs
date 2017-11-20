using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FraudTransactions.Models
{
    public class SearchViewModel
    {
        [Display(Name = "Search by Term:")]
        public string term { get; set; }
        [Display(Name = "Is Fraud?")]
        public bool isFraud { get; set; }
        [Display(Name = "Name Dest")]
        public string nameDest { get; set; }
        [Display(Name = "Operator")]
        public string op { get; set; }
    }
}