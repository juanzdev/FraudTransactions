using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace FraudTransactions.DataModel
{
    public class AccountStore : DbContext
    {
        public AccountStore(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}