using FraudTransactions.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FraudTransactions.DataModel
{
    public class AccountRepository : IAccountRepository
    {
        private AccountStore db = new AccountStore(System.Configuration.ConfigurationManager.ConnectionStrings["TransactionsDBEntities1"].ConnectionString);

        public AccountRepository()
        {
        }

      
        public bool IsValidUser(string userName,string password)
        {
            return db.Users.Any(x => x.UserName == userName && x.Password==password);
        }
        public FraudTransactions.Models.User Add(FraudTransactions.Models.User user)
        {
            FraudTransactions.DataModel.User userDb = new FraudTransactions.DataModel.User();
            userDb.Password = user.Password;
            userDb.UserName = user.UserName;
            userDb.RoleID = user.RoleId;
            db.Users.Add(userDb);
            db.SaveChanges();

            FraudTransactions.Models.User newUser = new Models.User();
            newUser.Id = userDb.Id;
            newUser.Password = userDb.Password;
            newUser.UserName = userDb.UserName;
            newUser.RoleId = userDb.RoleID;
            return newUser;
        }
        public FraudTransactions.Models.User Get(string userName)
        {
            var usr =  db.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (usr == null)
                return null;
            FraudTransactions.Models.User user = new Models.User();
            FraudTransactions.DataModel.User userDb = new FraudTransactions.DataModel.User();
            user.Id = usr.Id;
            user.Password = usr.Password;
            user.UserName = usr.UserName;
            user.RoleId = usr.RoleID;
            user.Roles = usr.Role.Role1;
            return user;
        }

    }

}