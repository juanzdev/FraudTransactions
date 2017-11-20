using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FraudTransactions.Startup))]
namespace FraudTransactions
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
