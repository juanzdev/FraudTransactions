using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FraudTransactions.Models
{
    public class Transaction
    {
        public int step { get; set; }
        public string type { get; set; }
        public int amount { get; set; }
        public string nameOrig { get; set; }
        public int oldBalanceOrig { get; set; }
        public int newBalanceOrig { get; set; }
        public string nameDest { get; set; }
        public int oldBalanceDest { get; set; }
        public int newBalanceDest { get; set; }
        public bool isFraud { get; set; }
        public bool isFlaggedFraud { get; set; }
        public int Id { get; set; }
    }
}