using FraudTransactions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FraudTransactions.DataModel
{
    public interface ITransactionRepository
    {
        IEnumerable<FraudTransactions.Models.Transaction> GetLast10();
        FraudTransactions.Models.Transaction Get(int id);
        List<FraudTransactions.Models.Transaction> GetBySearch(bool isFraud,string nameDest,string op,string term);
        FraudTransactions.Models.Transaction Add(FraudTransactions.Models.Transaction item);
        void Remove(int id);
        bool Update(FraudTransactions.Models.Transaction item);
    }
}