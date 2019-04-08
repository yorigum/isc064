using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using System.Web.Routing;

[assembly: OwinStartup(typeof(eProperty.StartUp))]

namespace eProperty
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            //app.MapSignalR();
        }
    }
}
