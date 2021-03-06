﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace ISC064.SECURITY
{
    public partial class PemanggilanTampil : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            tglspan.InnerText = Cf.TglNamaHari(DateTime.Now) + " , " + DateTime.Now.Day + " " + Cf.TglNamaBln(DateTime.Now.Month, false) + " " + DateTime.Now.Year;

            switch (Request.QueryString["act"])
            {
                case "listcounter":
                    ListCounter();
                    break;
                case "lastcall":
                    LastCall();
                    break;
                case "calllist":
                    CallList();
                    break;
                case "updatestatus":
                    UpdateStatus(Convert.ToInt32(Request.QueryString["id"]));
                    break;
            }
        }

        void ListCounter()
        {
            var rs = from DataRow r in Db.Rs("Select * from " + Mi.DbPrefix + "SECURITY..REF_ADMIN_LAUNCHING").Rows
                     let useravailable = Db.Rs("select * from ISC064_security..login where Status='A' and UserID='" + r["UserID"] + "' and TglExpire >= Getdate()").Rows.Count > 0
                     let Call = Db.Rs("Select top 1 * from " + Mi.DbPrefix + "MARKETINGJUAL..MS_LAUNCHING_CALL where CounterID='" + r["ID"] + "' order by id desc")
                     select new
                     {
                         Warna = !useravailable ? "grey" :
                                    Call.Rows.Count == 0 ? "grey" :
                                        (byte)Call.Rows[0]["Status"] == 0 ? "green" : // pemanggilan nup
                                            (byte)Call.Rows[0]["Status"] == 1 ? "blue" : // proses pilih unit
                                                (byte)Call.Rows[0]["Status"] == 2 ? "red" : // customer tidak datang
                                                    (byte)Call.Rows[0]["Status"] == 3 ? "orange" : // selesai pilih unit
                                                    "black"
                                        ,
                         Nama = r["Nama"],
                         Nomor = !useravailable ? "-" : Call.Rows.Count > 0 ? Call.Rows[0]["NUPID"] : "-"//Db.SingleString("SELECT ISNULL((Select top 1 NUPID from " + Mi.DbPrefix + "MARKETINGJUAL..MS_LAUNCHING_CALL where CounterID='"+ r["ID"] +"' order by id desc),'-')")
                     };

            Js.WriteJSON(rs);
        }

        void LastCall()
        {
            var rs = from DataRow r in Db.Rs("Select top 1 * from " + Mi.DbPrefix + "MARKETINGJUAL..MS_LAUNCHING_CALL order by id desc").Rows
                     select new
                     {
                         Nomor = r["NUPID"],
                         Counter = Db.SingleString("Select ISNULL(Nama,'UNKNOWN') from " + Mi.DbPrefix + "SECURITY..REF_ADMIN_LAUNCHING where id='" + r["CounterID"] + "'")
                     };

            Js.WriteJSON(rs);
        }

        void CallList()
        {
            var rs = from DataRow r in Db.Rs("Select * from " + Mi.DbPrefix + "MARKETINGJUAL..MS_LAUNCHING_CALL where iscalled=0 order by id asc").Rows
                     let nomor = Db.SingleInteger("Select Nomor from " + Mi.DbPrefix + "SECURITY..REF_ADMIN_LAUNCHING where ID=" + r["CounterID"])
                     let moneystr = Money.Str(Convert.ToDecimal(r["NUPID"])).ToLower().Replace("   ", " ").Replace("  ", " ").Replace(" ", " ")
                     let moneystr2 = Money.Str(Convert.ToDecimal(nomor)).ToLower().Replace("   ", " ").Replace("  ", " ").Replace(" ", " ")
                     select new
                     {
                         ID = r["ID"],
                         NUPID = r["NUPID"],
                         Text = "nomor-urut" + moneystr + " loket" + moneystr2 + " blank",
                         Counter = Db.SingleString("Select ISNULL(Nama,'UNKNOWN') from " + Mi.DbPrefix + "SECURITY..REF_ADMIN_LAUNCHING where id='" + r["CounterID"] + "'")
                     };
            Js.WriteJSON(rs);
        }

        void UpdateStatus(int id)
        {
            Db.Execute("Update " + Mi.DbPrefix + "MARKETINGJUAL..MS_LAUNCHING_CALL set isCalled=1 where ID='" + id + "'");
            Js.WriteJSON(true);
        }


    }
}