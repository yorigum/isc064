using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.LEGAL
{

    public partial class KontrakIMBProses : System.Web.UI.Page
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
                "SELECT COUNT(*) FROM MS_IMB WHERE NoKontrak = '" + NoKontrak + "' AND (StatusIMB='D' OR StatusIMB='T')");
            
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

                //InitForm();
                Fill();

                //Js.Focus(this, luas);
                //Js.Confirm(this, "Lanjutkan proses serah terima unit properti?");
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

                //InitForm();
                Fill();

                //Js.Focus(this, luas);
                //Js.Confirm(this, "Lanjutkan proses serah terima unit properti?");
            }
        }

        //private void InitForm()
        //{
        //    //kalkulator
        //    luas.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
        //    luas.Attributes["onkeyup"] = "CalcType(this,tempnum);";
        //    luas.Attributes["onblur"] = "CalcBlur(this);";

        //    nilaibiaya.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
        //    nilaibiaya.Attributes["onkeyup"] = "CalcType(this,tempnum);";
        //    nilaibiaya.Attributes["onblur"] = "CalcBlur(this);";
        //}

        protected void Fill()
        {
            string strSql = "SELECT "
                + " A.NoKontrak"
                + ", A.NoUnit"
                + ", B.Nama AS Cs"
                + ", C.TglIMB"
                + ", C.TglProsesIMB"
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

                tbTglProses.Text = Cf.Day(rsHeader.Rows[0]["TglProsesIMB"]);
                tbKeteranganIMB.Text = rsHeader.Rows[0]["KetImb"].ToString();

                //if (Convert.ToInt32(rsHeader.Rows[0]["StatusIMB"]) == 0)
                //{
                //    rblStatus.SelectedIndex = 0;
                //    selesai.Visible = false;
                //}
                //else if (Convert.ToInt32(rsHeader.Rows[0]["StatusIMB"]) == 1)
                //{
                //    rblStatus.SelectedIndex = 1;
                //    selesai.Visible = true;
                //}
            }
        }


        private void Save()
        {
            string sql = "", sql1;           

            DateTime Tgl = Convert.ToDateTime(tbTglProses.Text);
            string keteranganimb = Cf.Str(tbKeteranganIMB.Text);
            string status = "D";

            sql = "UPDATE MS_IMB"
                + " SET KetIMB = '" + keteranganimb + "'"
                + ", StatusIMB= '"+ status+"'"
                + ", TglProsesIMB = '" + Tgl + "'"
                + " WHERE NoKontrak = '" + NoKontrak + "'";

            Db.Execute(sql);

            DataTable rs = Db.Rs("SELECT"
                                  + " A.NoKontrak AS [No. Kontrak]"
                                  + ",B.NoUnit AS [Unit]"
                                  + ",C.Nama AS [Customer]"
                                  + ",CONVERT(varchar, A.TglTargetIMB, 106) AS [Tanggal Target IMB]"
                                  + ",CONVERT(varchar, A.TglProsesIMB, 106) AS [Tanggal IMB]"
                                  + ", A.KetIMB AS [Keterangan IMB]"
                                  + ", A.NoIMB AS [No. IMB]"
                                  + ",PersenLunas AS [Prosentase Pelunasan]"
                                  + ", case when A.StatusIMB='S' then 'Target IMB' when A.StatusIMB='D' then 'Proses IMB' when A.StatusIMB='B' then 'Belum IMB' else 'Tanda Tangan IMB' end as [Status IMB]"
                                  + " FROM MS_IMB A INNER JOIN MS_KONTRAK B"
                                  + " ON A.NoKontrak = B.NoKontrak"
                                  + " INNER JOIN MS_CUSTOMER C ON B.NoCustomer = C.NoCustomer"
                                  + " WHERE A.NoKontrak = '" + NoKontrak + "'");

            string ket = Cf.LogCapture(rs);

            Db.Execute("EXEC spLogKontrak "
                   + " 'PRO-IMB'"
                   + ",'" + Act.UserID + "'"
                   + ",'" + Act.IP + "'"
                   + ",'" + ket + "'"
                   + ",'" + NoKontrak + "'"
                   );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Response.Redirect("KontrakIMBProses.aspx?done=" + NoKontrak);
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