using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using ISC064;

public class ClosingUnit : Hub
{
    /// <summary>
    /// Made by Fahrizal
    /// Lock closing, dengan menggunakan signalr supaya database terupdate secara realtime
    /// Apabila user langsung close tab browser, signalr langsung delete row di database, agar user lain bisa langsung closing unit tsb
    /// </summary>


    public void Hello(string User, string NoStock)
    {
        //Clients(ConnectionID);
        Clients.All.broadcastMsg(User, NoStock);
    }

    public override Task OnConnected()
    {
        var NoStock1 = Context.Request.QueryString["NoStock"];
        var User = Context.User.Identity.Name;
        var ConnectionID = Context.ConnectionId;

        bool AdaKoneksi = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT_CLOSING WHERE "
            + " ConnectionID <> '" + ConnectionID + "'"
            + " AND NoStock = '" + NoStock1 + "' "
            + " AND Connected = 1" // Masih aktif
            + " AND User <> '" + User + "'"
            ) > 0;

        if (!AdaKoneksi) // cek apakah ada user selain user sekarang yang aktif
        {
            Db.Execute("INSERT INTO ISC064_MARKETINGJUAL..MS_UNIT_CLOSING "
                + "(ConnectionID, User, NoStock, Connected)"
                + " VALUES"
                + "('" + ConnectionID + "', '" + User + "', '" + NoStock1 + "', 1)"
                + " ON DUPLICATE KEY UPDATE Connected = 1"
                );
        }

        return base.OnConnected();
    }
    public override Task OnDisconnected(bool stopCalled)
    {
        var NoStock1 = Context.Request.QueryString["NoStock"];
        var User = Context.User.Identity.Name;
        var ConnectionID = Context.ConnectionId;

        Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_UNIT_CLOSING "
            + " SET Connected = 0" //false
            + " WHERE ConnectionID = '" + ConnectionID + "'"
            + " AND NoStock = '" + NoStock1 + "'"
            + " AND User = '" + User + "'"
            );

        return base.OnDisconnected(stopCalled);
    }
    #region Status Unit Tabel Stok
    /// <summary>
    /// Untuk Status unit di table stock, otomatis update setiap ada perubahan status
    /// </summary>
    /// <param name="NoStockBaru"></param>
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
    #endregion
}
