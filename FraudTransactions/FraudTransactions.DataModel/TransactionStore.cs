using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace FraudTransactions.DataModel
{
    public class TransactionStore : DbContext
    {
        public TransactionStore(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
        public DbSet<Transaction> Transactions { get; set; }
    }
}