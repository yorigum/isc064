using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakDendaBASTPutih : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&ppjb=1');";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    dariReminder.Checked = true;
                    nokontrak.Text = Request.QueryString["NoKontrak"];
                    LoadKontrak();

                    //cancel.Attributes["onclick"] = "location.href='ReminderPPJB.aspx'";
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pemutihan denda BAST?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Pemutihan Denda BAST Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A' AND DendaST > RealisasiDendaST + PemutihanDendaST");

            if (c == 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Tidak ada denda BAST yang belum direalisasi.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        private void LoadKontrak()
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Focus(this, save);
                Js.Confirm(this, "Lanjutkan proses pemutihan denda BAST?");
            }
            else
            {
                backbtn.Visible = true;
                Js.Focus(this, nokontrak);
                frm.Visible = false;
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                Js.Focus(this, save);
                Js.Confirm(this, "Lanjutkan proses pemutihan denda BAST?");
            }
        }

        private void Fill()
        {
            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                targetst.Text = Cf.Day(rs.Rows[0]["TargetST"]);
                dendabast.Text = Cf.Num(rs.Rows[0]["DendaST"]);
                realisasi.Text = Cf.Num(rs.Rows[0]["RealisasiDendaST"]);
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            DataTable rsBef = Db.Rs("SELECT"
                + " PemutihanDendaST AS [Pemutihan Denda BAST]"
                + " FROM MS_KONTRAK"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

            decimal Putih = Convert.ToDecimal(dendabast.Text) - Convert.ToDecimal(realisasi.Text);
            Db.Execute("UPDATE MS_KONTRAK"
                + " SET PemutihanDendaST = " + Putih
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            DataTable rsAft = Db.Rs("SELECT"
                + " PemutihanDendaST AS [Pemutihan Denda BAST]"
                + " FROM MS_KONTRAK"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

            DataTable rs = Db.Rs("SELECT"
                + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                + ",MS_KONTRAK.NoUnit AS [Unit]"
                + ",MS_CUSTOMER.Nama AS [Customer]"
                + ",DendaST AS [Denda BAST]"
                + ",RealisasiDendaST AS [Realisasi Denda BAST]"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

            string ket = Cf.LogCapture(rs)
                + Cf.LogCompare(rsBef, rsAft)
                ;

            Db.Execute("EXEC spLogKontrak "
                + " 'PD-ST'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + ket + "'"
                + ",'" + NoKontrak + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Response.Redirect("KontrakDendaBASTPutih.aspx?done=" + NoKontrak);
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}
