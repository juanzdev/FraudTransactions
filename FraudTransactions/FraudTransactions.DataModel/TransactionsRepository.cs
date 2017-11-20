using FraudTransactions.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FraudTransactions.DataModel
{
    /// <summary>
    /// Repository for all the operations regarding transactions operations, it uses EF
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        private TransactionStore db = new TransactionStore(System.Configuration.ConfigurationManager.ConnectionStrings["TransactionsDBEntities1"].ConnectionString);

        public TransactionRepository()
        {
        }

        public IEnumerable<FraudTransactions.Models.Transaction> GetLast10()
        {
            var tt = db.Transactions.OrderByDescending(c => c.Id).Take(10);
            List<FraudTransactions.Models.Transaction> transactions = new List<Models.Transaction>();
            foreach (var t in tt)
            {
                transactions.Add(new Models.Transaction()
                {
                    Id = t.Id,
                    step = t.step,
                    type = t.type,
                    amount = t.amount,
                    nameOrig = t.nameOrig,
                    oldBalanceOrig = t.oldBalanceOrig,
                    newBalanceOrig = t.newBalanceOrig,
                    nameDest = t.nameDest,
                    oldBalanceDest = t.oldBalanceDest,
                    newBalanceDest = t.newBalanceDest,
                    isFraud = t.isFraud,
                    isFlaggedFraud = t.isFlaggedFraud

                });
            }
            return transactions;
            
        }

        public FraudTransactions.Models.Transaction Get(int id)
        {
            FraudTransactions.DataModel.Transaction transactionsDbSet = db.Transactions.Find(id);
            FraudTransactions.Models.Transaction transaction =
            new Models.Transaction()
            {
                Id = transactionsDbSet.Id,
                step = transactionsDbSet.step,
                type = transactionsDbSet.type,
                amount = transactionsDbSet.amount,
                nameOrig = transactionsDbSet.nameOrig,
                oldBalanceOrig = transactionsDbSet.oldBalanceOrig,
                newBalanceOrig = transactionsDbSet.newBalanceOrig,
                nameDest = transactionsDbSet.nameDest,
                oldBalanceDest = transactionsDbSet.oldBalanceDest,
                newBalanceDest = transactionsDbSet.newBalanceDest,
                isFraud = transactionsDbSet.isFraud,
                isFlaggedFraud = transactionsDbSet.isFlaggedFraud

            };
            return transaction;
        }

        public List<FraudTransactions.Models.Transaction> GetBySearch(bool isFraud,string nameDest,string op,string term)
        {
            List<FraudTransactions.Models.Transaction> transactions = new List<Models.Transaction>();
            List<FraudTransactions.DataModel.Transaction> transactionsDbSet = null;
            var nd = nameDest ?? "";
            if (term == "ISFRAUD")
            {
                transactionsDbSet = db.Transactions.Where(x => x.isFraud == isFraud).ToList();
            }
            if (term == "NAMEDEST")
            {
                transactionsDbSet = db.Transactions.Where(x => x.nameDest == nd).ToList();
            }
            if (term == "BOTH")
            {
                if (op == "AND")
                {
                    transactionsDbSet = db.Transactions.Where(x => x.isFraud == isFraud && x.nameDest == nd).ToList();
                }
                else
                {
                    transactionsDbSet = db.Transactions.Where(x => x.isFraud == isFraud || x.nameDest == nd).ToList();
                }

            }

            foreach (var tran in transactionsDbSet)
            {
                transactions.Add(new Models.Transaction()
                {
                    Id = tran.Id,
                    step = tran.step,
                    type = tran.type,
                    amount = tran.amount,
                    nameOrig = tran.nameOrig,
                    oldBalanceOrig = tran.oldBalanceOrig,
                    newBalanceOrig = tran.newBalanceOrig,
                    nameDest = tran.nameDest,
                    oldBalanceDest = tran.oldBalanceDest,
                    newBalanceDest = tran.newBalanceDest,
                    isFraud = tran.isFraud,
                    isFlaggedFraud = tran.isFlaggedFraud
                });
            }
            
            return transactions;
        }

        public FraudTransactions.Models.Transaction Add(FraudTransactions.Models.Transaction item)
        {
            FraudTransactions.DataModel.Transaction transactionDb = new FraudTransactions.DataModel.Transaction();
            transactionDb.Id = item.Id;
            transactionDb.step = item.step;
            transactionDb.type = item.type;
            transactionDb.amount = item.amount;
            transactionDb.nameOrig = item.nameOrig;
            transactionDb.oldBalanceOrig = item.oldBalanceOrig;
            transactionDb.newBalanceOrig = item.newBalanceOrig;
            transactionDb.nameDest = item.nameDest;
            transactionDb.oldBalanceDest = item.oldBalanceDest;
            transactionDb.newBalanceDest = item.newBalanceDest;
            transactionDb.isFraud = item.isFraud;
            transactionDb.isFlaggedFraud = item.isFlaggedFraud;
            db.Transactions.Add(transactionDb);
            db.SaveChanges();
            return item;
        }

        public void Remove(int id)
        {
            FraudTransactions.DataModel.Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
        }

        public bool Update(FraudTransactions.Models.Transaction item)
        {
            FraudTransactions.DataModel.Transaction transactionDb = new FraudTransactions.DataModel.Transaction();
            transactionDb.Id = item.Id;
            transactionDb.step = item.step;
            transactionDb.type = item.type;
            transactionDb.amount = item.amount;
            transactionDb.nameOrig = item.nameOrig;
            transactionDb.oldBalanceOrig = item.oldBalanceOrig;
            transactionDb.newBalanceOrig = item.newBalanceOrig;
            transactionDb.nameDest = item.nameDest;
            transactionDb.oldBalanceDest = item.oldBalanceDest;
            transactionDb.newBalanceDest = item.newBalanceDest;
            transactionDb.isFraud = item.isFraud;
            transactionDb.isFlaggedFraud = item.isFlaggedFraud;
            db.Entry(transactionDb).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }

}