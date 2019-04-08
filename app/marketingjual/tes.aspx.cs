using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class tes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(TableStock.WarnaUnit("0000184"));
            //Response.Write(TableStock.Href("0000184"));
            var hub = GlobalHost.ConnectionManager.GetHubContext<UnitHub>();
            hub.Clients.All.broadcastStatus("0000184");
        }
    }
}
