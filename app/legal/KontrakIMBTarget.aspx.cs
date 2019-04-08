using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.LEGAL
{
    public partial class KontrakIMBTarget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&imb=1');";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    dariReminder.Checked = true;
                    nokontrak.Text = Request.QueryString["NoKontrak"];
                    LoadKontrak();

                    cancel.Attributes["onclick"] = "location.href='ReminderST.aspx'";
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan dengan proses edit IMB?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popKontrakIMBEdit('" + Request.QueryString["done"] + "')\">"
                        + "IMB Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND ST <> 'D'");

            if (c == 0)
                x = false;

            int a = Db.SingleInteger(
                 "SELECT COUNT(*) FROM MS_IMB WHERE NoKontrak = '" + NoKontrak + "' AND (StatusIMB=1 OR StatusIMB=2 OR StatusIMB=3)");// AND ST <> 'D'");
            if (a > 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Prosedur IMB sudah dijalankan.\\n"
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
            }
        }

        protected void Fill()
        {
            //cancel.Attributes["onclick"] = "location.href='KontrakProses.aspx?NoKontrak=" + NoKontrak + "'";


            string strSql = "SELECT "
                    + " A.NoKontrak"
                    + ", A.NoUnit"
                    + ", B.Nama AS Cs"
                    + ", C.TglIMB"
                    + ", C.TglTargetIMB"
                    + ", C.KetIMB"
                    + ", C.NoIMB"
                    + " FROM MS_KONTRAK A INNER JOIN MS_CUSTOMER B ON A.NoCustomer = B.NoCustomer"
                    + " LEFT JOIN MS_IMB C ON A.NoKontrak = C.NoKontrak"
                    + " WHERE A.NoKontrak = '" + NoKontrak + "'";

            DataTable rsHeader = Db.Rs(strSql);

            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nokontrak2.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
                unit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
                customer.Text = rsHeader.Rows[0]["Cs"].ToString();

                tbTglTarget.Text = Cf.Day(rsHeader.Rows[0]["TglTargetIMB"]);
                tbKeteranganIMB.Text = rsHeader.Rows[0]["KetImb"].ToString();
            }
        }


        private void Save()
        {

            int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_IMB WHERE NoKontrak = '" + NoKontrak + "'");
            if (c == 0)
            {
                string sql = "", sql1;
                DateTime Tgl = Convert.ToDateTime(tbTglTarget.Text);
                string keteranganimb = Cf.Str(tbKeteranganIMB.Text);
                string status = "S";

                sql = "INSERT INTO MS_IMB (NoKontrak,NoIMB, StatusIMB, KetIMB,TglTargetIMB)"
                      + "VALUES('" + NoKontrak + "','" + "" + "','" + status + "','" + keteranganimb + "','" + Tgl + "')";
                Db.Execute(sql);

                DataTable rs = Db.Rs("SELECT"
                      + " A.NoKontrak AS [No. Kontrak]"
                      + ",B.NoUnit AS [Unit]"
                      + ",C.Nama AS [Customer]"
                      + ",CONVERT(varchar, A.TglTargetIMB, 106) AS [Tanggal Target IMB]"
                      + ",PersenLunas AS [Prosentase Pelunasan]"
                      + ", KetIMB AS [Keterangan IMB]"
                      + ", case when A.StatusIMB='S' then 'Target IMB' when A.StatusIMB='D' then 'IMB' when A.StatusIMB='B' then 'Belum IMB' else 'Tanda Tangan IMB' end as [Status IMB]"
                      + " FROM MS_IMB A INNER JOIN MS_KONTRAK B"
                      + " ON A.NoKontrak = B.NoKontrak"
                      + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                      + " WHERE A.NoKontrak = '" + NoKontrak + "'");
                string ket = Cf.LogCapture(rs);

                Db.Execute("EXEC spLogKontrak "
                       + " 'T-IMB'"
                       + ",'" + Act.UserID + "'"
                       + ",'" + Act.IP + "'"
                       + ",'" + ket + "'"
                       + ",'" + NoKontrak + "'"
                       );
                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            }
            else
            {
                string sql = "", sql1;

                DateTime Tgl = Convert.ToDateTime(tbTglTarget.Text);
                string keteranganimb = Cf.Str(tbKeteranganIMB.Text);
                string status = "S";

                sql = "UPDATE MS_IMB"
                    + " SET KetIMB = '" + keteranganimb + "'"
                    + ", StatusIMB = '" + status + "'"
                    + ", TglTargetIMB = '" + Tgl + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'";

                Db.Execute(sql);

                DataTable rs = Db.Rs("SELECT"
                      + " A.NoKontrak AS [No. Kontrak]"
                      + ",B.NoUnit AS [Unit]"
                      + ",C.Nama AS [Customer]"
                      + ",CONVERT(varchar, A.TglTargetIMB, 106) AS [Tanggal Target IMB]"
                      + ",PersenLunas AS [Prosentase Pelunasan]"
                      + ", KetIMB AS [Keterangan IMB]"
                      + ", case when A.StatusIMB='S' then 'Target IMB' when A.StatusIMB='D' then 'IMB' when A.StatusIMB='B' then 'Belum IMB' else 'Tanda Tangan IMB' end as [Status IMB]"
                      + " FROM MS_IMB A INNER JOIN MS_KONTRAK B"
                      + " ON A.NoKontrak = B.NoKontrak"
                      + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                      + " WHERE A.NoKontrak = '" + NoKontrak + "'");

                string ket = Cf.LogCapture(rs);

                Db.Execute("EXEC spLogKontrak "
                       + " 'T-IMB'"
                       + ",'" + Act.UserID + "'"
                       + ",'" + Act.IP + "'"
                       + ",'" + ket + "'"
                       + ",'" + NoKontrak + "'"
                       );
                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            }
            Response.Redirect("KontrakIMBTarget.aspx?done=" + NoKontrak);
        }

        protected void save_Click(object sender, EventArgs e)
        {
            Save();
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