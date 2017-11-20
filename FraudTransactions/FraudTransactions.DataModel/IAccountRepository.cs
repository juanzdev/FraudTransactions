using FraudTransactions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FraudTransactions.DataModel
{
    public interface IAccountRepository
    {
        bool IsValidUser(string userName,string password);
        FraudTransactions.Models.User Add(FraudTransactions.Models.User user);
        FraudTransactions.Models.User Get(string userName);
        FraudTransactions.Models.User GetByPassword(string userName,string password);
    }
}