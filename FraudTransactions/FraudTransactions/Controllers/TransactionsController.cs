using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using FraudTransactions.Models;
namespace FraudTransactions.Controllers
{
    public class HttpClientHelper
    {
        public static HttpClient GetHttpClient()
        {
            var MyHttpClient = new HttpClient();
            dynamic _token = HttpContext.Current.Session["token"];
           // if (_token == null) throw new ArgumentNullException(nameof(_token));
           // MyHttpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _token.AccessToken));
            return MyHttpClient;
        }
    }
    public class TransactionsController : Controller
    {
        private string baseUrl = "http://localhost:17618/";
        [Authorize]
        public ActionResult Index()
        {
            var httpClient = HttpClientHelper.GetHttpClient();
            /*HttpResponseMessage response = httpClient.GetAsync(baseUrl + "api/TransactionsApi/GetLast10").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = response.Content.ReadAsAsync<IEnumerable<Transaction>>().Result;
                //string stateInfo = response.Content.ReadAsStringAsync().Result;
                ViewBag.transactions = resp;
            }*/
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            FraudTransactions.Models.Transaction tran = new Transaction();
            var httpClient = HttpClientHelper.GetHttpClient();
            HttpResponseMessage response = httpClient.GetAsync(baseUrl + "api/TransactionsApi/GetTransaction/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = response.Content.ReadAsAsync<Transaction>().Result;
                //string stateInfo = response.Content.ReadAsStringAsync().Result;
                tran.Id = resp.Id;
                tran.step = resp.step;
                tran.type = resp.type;
                tran.amount = resp.amount;
                tran.nameOrig = resp.nameOrig;
                tran.oldBalanceOrig = resp.oldBalanceOrig;
                tran.newBalanceOrig = resp.newBalanceOrig;
                tran.nameDest = resp.nameDest;
                tran.oldBalanceDest = resp.oldBalanceDest;
                tran.newBalanceDest = resp.newBalanceDest;
                tran.isFraud = resp.isFraud;
                tran.isFlaggedFraud = resp.isFlaggedFraud;
            }
            return View(tran);
        }
        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            var httpClient = HttpClientHelper.GetHttpClient();
            HttpResponseMessage response = httpClient.PutAsJsonAsync(baseUrl + "api/TransactionsApi/PutTransaction/", transaction).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = response.Content.ReadAsAsync<Transaction>().Result;
                //string stateInfo = response.Content.ReadAsStringAsync().Result;
               // ViewBag.transaction = resp;
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Assistant,Administrator")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Transaction tran)
        {
            var httpClient = HttpClientHelper.GetHttpClient();

            HttpResponseMessage response = httpClient.PostAsJsonAsync(baseUrl + "api/TransactionsApi/PostTransaction/",tran).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = response.Content.ReadAsAsync<Transaction>().Result;
                //string stateInfo = response.Content.ReadAsStringAsync().Result;
                //ViewBag.transaction = resp;
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager,Superintendent,Administrator")]
        [HttpGet]
        public ActionResult Search(bool isFraud,string nameDest,string op, string term)
        {
            var httpClient = HttpClientHelper.GetHttpClient();

            HttpResponseMessage response = httpClient.GetAsync(baseUrl + "api/TransactionsApi/GetTransactionSearch?isFraud=" + isFraud +"&nameDest="+nameDest + "&op=" + op + "&term=" + term).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = response.Content.ReadAsAsync<IEnumerable<Transaction>>().Result;
                //string stateInfo = response.Content.ReadAsStringAsync().Result;
                ViewBag.transactions = resp;
            }
            //is search flag
            return View("Index");
        }
    }
}