using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using ISC064;

public class ClosingHub : Hub {
    public void Connect()
    {
        Clients.All.broadcast();
    }
    /// <summary>
    /// Untuk Closing
    /// </summary>
    /// <param name="User"></param>
    /// <param name="NoStock"></param>
    public void Hello(string User, string NoStock)
    {
        Clients.All.broadcastMsg(User, NoStock);
    }
    public override Task OnConnected()
    {
        string UserID = Context.Request.QueryString["UserID"];
        string NoStock = Context.Request.QueryString["NoStock"];

        Clients.Caller.broadcastMsg("Hello " + UserID, "You're Connected");
        var ConnectionID = Context.ConnectionId;

        if (UserID != null || UserID != "")
        {
            bool AdaKoneksi = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT_CLOSING WHERE "
                + " NoStock = '" + NoStock + "' "
                + " AND Connected = 1" // Masih aktif
                + " AND UserID <> '" + UserID + "'"
                ) > 0;
            bool SempatAktif = Db.SingleInteger("SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_UNIT_CLOSING WHERE "
                 + " NoStock = '" + NoStock + "' "
                 + " AND Connected = 0"
                 + " AND UserID = '" + UserID + "'"
                 ) > 0;

            if (!AdaKoneksi) // cek apakah ada user selain user sekarang yang aktif
            {
                Db.Execute("INSERT INTO ISC064_MARKETINGJUAL..MS_UNIT_CLOSING "
                    + "(UserID, NoStock, Connected)"
                    + " VALUES"
                    + "('" + UserID + "', '" + NoStock + "', 1)"
                    );
            }
            else if (SempatAktif)
            {
                Db.Execute("UPDATE ISC064_MARKETINGJUAL..MS_UNIT_CLOSING "
                    + " SET Connected = 1" //true
                    + " WHERE "
                    + " NoStock = '" + NoStock + "'"
                    + " AND UserID = '" + UserID + "'"
                    );
            }
        }

        return base.OnConnected();
    }
    public override Task OnDisconnected(bool stopCalled)
    {
        var NoStock = Context.Request.QueryString["NoStock"];
        var UserID = Context.Request.QueryString["UserID"];
        var ConnectionID = Context.ConnectionId;

        //delete aja biar ga banyak sampah data
        Db.Execute("DELETE FROM ISC064_MARKETINGJUAL..MS_UNIT_CLOSING WHERE NoStock = '" + NoStock + "' AND UserID = '" + UserID + "'");
        //Db.Execute("UPDATE ISC064A_MARKETINGJUAL..MS_UNIT_CLOSING "
        //    + " SET Connected = 0" //false
        //    + " WHERE ConnectionID = '" + ConnectionID + "'"
        //    + " AND NoStock = '" + NoStock + "'"
        //    + " AND UserID = '" + UserID + "'"
        //    );

        return base.OnDisconnected(stopCalled);
    }
}
