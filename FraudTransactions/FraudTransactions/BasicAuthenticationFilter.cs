using FraudTransactions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace FraudTransactions
{
    /// <summary>
    /// Filter that will be used everytime some calls the web api http rest methods, the filter will check on the database if the user is valid
    /// </summary>
    public class BasicAuthenticationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext) {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string username = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

                //get user from db
                var  _repository = new AccountRepository();
                FraudTransactions.Models.User user = _repository.GetByPassword(username, password);

                if (user != null)
                {
                    HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(user), new string[] { });
                    base.OnActionExecuting(actionContext);
                }
                else
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}