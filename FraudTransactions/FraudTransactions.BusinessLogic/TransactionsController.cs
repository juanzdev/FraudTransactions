using FraudTransactions.DataModel;
using FraudTransactions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FraudTransactions.BusinessLogic
{
    public class TransactionsController : ApiController
    {
        static ITransactionRepository _repository;

        public TransactionsController(ITransactionRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<FraudTransactions.Models.Transaction> GetLast10()
        {
            return _repository.GetLast10();
        }

        [HttpGet]
        public FraudTransactions.Models.Transaction GetTransaction(int id)
        {
            FraudTransactions.Models.Transaction transaction = _repository.Get(id);
            if (transaction == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return transaction;
        }
        public FraudTransactions.Models.Transaction GetTransactionSearch(int id)
        {
            FraudTransactions.Models.Transaction transaction = _repository.Get(id);
            if (transaction == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return transaction;
        }
        public HttpResponseMessage PostTransaction(FraudTransactions.Models.Transaction transaction)
        {
            transaction = _repository.Add(transaction);
            var response = Request.CreateResponse<FraudTransactions.Models.Transaction>(HttpStatusCode.Created, transaction);
            string uri = Url.Route(null, new { id = transaction.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public void PutTransaction(int id, FraudTransactions.Models.Transaction transaction)
        {
            transaction.Id = id;
            if (!_repository.Update(transaction))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
        }

        public HttpResponseMessage DeleteTransaction(int id)
        {
            _repository.Remove(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
