using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.COLLECTION
{
    public partial class PemutihanDenda2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            FillTable();
            //	FeedBack();
            if (frm.Visible)
                Js.Confirm(this, "Lanjutkan proses Pemutihan Denda?");
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");

            if (c == 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        private void next_Click(object sender, System.EventArgs e)
        {
            //FillTable();
            if (valid())
            {
                //pilih.Visible = false;
                frm.Visible = true;

                FillTable();

                Js.Confirm(this, "Lanjutkan proses Pemutihan Denda?");
            }
        }

        private void FillTable()
        {
            //Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);
            DataTable rsk = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            nokontrakl.Text = rsk.Rows[0]["NoKontrak"].ToString();
            unit.Text = rsk.Rows[0]["NoUnit"].ToString();
            customer.Text = Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = '" + rsk.Rows[0]["NoCustomer"].ToString() + "'");
            agent.Text = Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_AGENT WHERE NoaGENT = '" + rsk.Rows[0]["NoAgent"].ToString() + "'");

            list.Controls.Clear();
            DataTable rs = Db.Rs("SELECT * FROM ISC064_MARKETINGJUAL..MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' "
                + " AND Denda > 0 AND (Denda - DendaReal - NilaiPutihDenda) > 0 ORDER BY NoUrut");
            //			Rpt.NoData(list, rs, "Tidak ada tagihan untuk kontrak tersebut.");

            int nomer = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                //No
                Label l;
                TextBox bx;

                nomer++;
                l = new Label();
                l.Text = "<tr>"
                    + "<td>" + nomer + ".</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = ""
                    + "<td>" + rs.Rows[i]["NamaTagihan"] + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = ""
                    + "<td>" + rs.Rows[i]["Tipe"] + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = ""
                    + "<td>" + Cf.Day(rs.Rows[i]["TglJT"]) + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = ""
                    + "<td>" + Cf.Num(rs.Rows[i]["NilaiTagihan"]) + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = ""
                    + "<td>" + Cf.Num(Math.Round((decimal)rs.Rows[i]["Denda"])) + "</td>";
                list.Controls.Add(l);

                l = new Label();
                decimal Sisa =Math.Round(Convert.ToDecimal(rs.Rows[i]["Denda"]) - Convert.ToDecimal(rs.Rows[i]["DendaReal"]) - Convert.ToDecimal(rs.Rows[i]["NilaiPutihDenda"]) - Convert.ToDecimal(rs.Rows[i]["AlokasiBenefit"]));
                l.Text = ""
                    + "<td>" + Cf.Num(Sisa) + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>"
                    + "<a  onclick=\"return confirm('Apakah anda yakin ? PERHATIAN Bahwa Denda Akan Dihapus.')\" href=\"PemutihanDenda.aspx?NoUrut=" + rs.Rows[i]["NoUrut"] + "&NoKontrak=" + NoKontrak + "\">Pemutihan Denda...</a>"
                    + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</tr>";
                list.Controls.Add(l);
            }
        }

        protected void SubTotal(decimal t1, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "<strong>Grand Total</strong>";
            c.HorizontalAlign = HorizontalAlign.Left;
            c.ColumnSpan = 5;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(t1) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(t2) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<strong>" + Cf.Num(t3) + "</strong>";
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            //rpt.Rows.Add(r);
        }        
        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }
    }
}