using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.MARKETINGJUAL
{
    public partial class ToolTipSitePlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_UNIT where Nostock = '" + NoStock + "'");
            unit.Text = rs.Rows[0]["NoUnit"].ToString();
            namablok.Text = rs.Rows[0]["Lokasi"].ToString();
            tipe.Text = tipe.Text = rs.Rows[0]["JenisProperti"].ToString() + " " + rs.Rows[0]["Jenis"].ToString();
            lsg.Text = Cf.Num(rs.Rows[0]["LuasSG"]);
            //lsnett.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
            arahhadap.Text = Cf.Str(rs.Rows[0]["ArahHadap"]);
            string JenisProperti = rs.Rows[0]["JenisProperti"].ToString();
            bool SifatPPN = Convert.ToBoolean(rs.Rows[0]["SifatPPN"]);


            if (JenisProperti == "RUSUNAMI" && SifatPPN == true)
            {// select skema 2 only
                TableRow r2 = new TableRow();
                TableCell c2;
                c2 = new TableCell();
                c2.CssClass = "h";
                c2.ColumnSpan = 2;
                c2.Text = "Cara Bayar";
                c2.Width = 50; c2.Height = 30;
                c2.Attributes["style"] += "background-color:#1a1a00;color:white;";
                r2.Cells.Add(c2);
                listharga.Rows.Add(r2);


                DataTable rsPL = Db.Rs("SELECT * FROM MS_PRICELIST WHERE NoStock='" + NoStock + "' AND (NoSkema=2)");
                for (int i = 0; i < rsPL.Rows.Count; i++)
                {
                    TableRow r = new TableRow();
                    TableCell c;

                    c = new TableCell();
                    c.CssClass = "h";
                    c.Text = Db.SingleString("SELECT Nama FROM REF_SKEMA WHERE Nomor='" + rsPL.Rows[i]["NoSkema"].ToString() + "'");
                    c.Width = 250; c.Height = 30;
                    c.Attributes["style"] += "background-color:#1a1a00;color:white;";
                    r.Cells.Add(c);


                    c = new TableCell();
                    c.CssClass = "h";
                    c.Text = Cf.Num(rsPL.Rows[i]["Pricelist"]);
                    c.Width = 50; c.Height = 30;
                    c.Attributes["style"] += "background-color:#1a1a00;color:white;";
                    r.Cells.Add(c);

                    listharga.Rows.Add(r);
                }

            }
            else if (JenisProperti == "RUSUNAMI" && SifatPPN == false)
            {// select skema 2 only
                TableRow r2 = new TableRow();
                TableCell c2;
                c2 = new TableCell();
                c2.CssClass = "h";
                c2.ColumnSpan = 2;
                c2.Text = "Cara Bayar";
                c2.Width = 50; c2.Height = 30; c2.Attributes["style"] += "background-color:#1a1a00;color:white;";
                r2.Cells.Add(c2);
                listharga.Rows.Add(r2);


                DataTable rsPL = Db.Rs("SELECT * FROM MS_PRICELIST WHERE NoStock='" + NoStock + "' AND (NoSkema=1 OR NoSkema=2)");
                for (int i = 0; i < rsPL.Rows.Count; i++)
                {
                    TableRow r = new TableRow();
                    TableCell c;

                    c = new TableCell();
                    c.CssClass = "h";
                    c.Text = Db.SingleString("SELECT Nama FROM REF_SKEMA WHERE Nomor='" + rsPL.Rows[i]["NoSkema"].ToString() + "'");
                    c.Width = 250; c.Height = 30; c.Attributes["style"] += "background-color:#1a1a00;color:white;";
                    r.Cells.Add(c);


                    c = new TableCell();
                    c.CssClass = "h";
                    c.Text = Cf.Num(rsPL.Rows[i]["Pricelist"]);
                    c.Width = 50; c.Height = 30; c.Attributes["style"] += "background-color:#1a1a00;color:white;";
                    r.Cells.Add(c);

                    listharga.Rows.Add(r);
                }

            }

            //Apartemen
            if (JenisProperti == "APARTEMEN")
            {
                TableRow r2 = new TableRow();
                TableCell c2;
                c2 = new TableCell();
                c2.CssClass = "h";
                c2.ColumnSpan = 2;
                c2.Text = "Cara Bayar";
                c2.Width = 50; c2.Height = 30; c2.Attributes["style"] += "background-color:#1a1a00;color:white;";
                r2.Cells.Add(c2);
                listharga.Rows.Add(r2);


                DataTable rsPL = Db.Rs("SELECT * FROM MS_PRICELIST WHERE NoStock='" + NoStock + "' AND (NoSkema=3 OR NoSkema=4 OR NoSkema=5 OR NoSkema=6)");
                for (int i = 0; i < rsPL.Rows.Count; i++)
                {
                    TableRow r = new TableRow();
                    TableCell c;

                    c = new TableCell();
                    c.CssClass = "h";
                    c.Text = Db.SingleString("SELECT Nama FROM REF_SKEMA WHERE Nomor='" + rsPL.Rows[i]["NoSkema"].ToString() + "'");
                    c.Width = 250; c.Height = 30; c.Attributes["style"] += "background-color:#1a1a00;color:white;";
                    r.Cells.Add(c);


                    c = new TableCell();
                    c.CssClass = "h";
                    c.Text = Cf.Num(rsPL.Rows[i]["Pricelist"]);
                    c.Width = 50; c.Height = 30; c.Attributes["style"] += "background-color:#1a1a00;color:white;";
                    r.Cells.Add(c);

                    listharga.Rows.Add(r);
                }

            }




            Fill();
        }

        protected void Fill()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_PRICELIST WHERE NoStock = '" + NoStock + "'");
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoStock"]);
            }
        }
    }
}