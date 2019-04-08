using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using ISC064;
using System.Drawing;

/// <summary>
/// Summary description for UnitHub
/// </summary>
public class UnitHub : Hub {
    public void Connect()
    {
        Clients.All.broadcast();
    }
    public void invokeStatus(string NoStockBaru)
    {
        NoStock = NoStockBaru;

        refreshStatusUnit();
    }
    public void refreshStatusUnit()
    {
        //Color Warna = TableStock.WarnaUnit(NoStock);
        //string Href = TableStock.Href(NoStock);
        Clients.All.broadcastStatus(NoStock);//, Warna.ToString(), Href);
    }
    public string NoStock
    {
        get; set;
    }
}