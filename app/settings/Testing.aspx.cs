using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ISC064;
using System.Text.RegularExpressions;
using Microsoft.AspNet.SignalR;

public partial class Testing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var context = GlobalHost.ConnectionManager.GetHubContext<ClosingUnit>();
        context.Clients.All.invokeStatus("0000100");
    }
}