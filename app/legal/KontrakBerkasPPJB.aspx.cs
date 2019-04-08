using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{

    public partial class KontrakBerkasPPJB : System.Web.UI.Page
    {
        string foCheck, focounterCheck;
        protected DataTable rs;
        protected DataTable rs5;
        protected DataTable rs6;
        protected DataTable rs3;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                //ok.Enabled = true;
                save.Enabled = true;
            }
            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&ppjb=1');";
                //Fill();
                if (Request.QueryString["NoKontrak"] != null)
                {
                    //dariReminder.Checked = true;
                    nokontrak.Text = Request.QueryString["NoKontrak"];
                    LoadKontrak();

                    //cancel.Attributes["onclick"] = "location.href='ReminderPPJB.aspx'";
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }
                if (frm.Visible) Js.Confirm(this, "Lanjutkan proses pencatatan berkas PPJB?");
            }
            Fill();
            FeedBack();
            //Js.Confirm(this, "Lanjutkan proses pencatatan berkas ppjb?\\n");

        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    //feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                    //    + "Edit Berhasil...";
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popKontrakPPJBEdit('" + Request.QueryString["done"] + "')\">"
                        + "Kelengkapan Berkas Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A'");// AND PPJB <> 'D'");

            if (c == 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Prosedur PPJB sudah dijalankan.\\n"
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
                Fill2();

                Js.Focus(this, save);
                //Js.Confirm(this, "Lanjutkan proses pencatatan berkas ppjb");
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

                Fill2();

                Js.Focus(this, save);
                Js.Confirm(this, "Lanjutkan proses pencatatan berkas ppjb");
            }
        }

        private void Fill()
        {
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            string strSql = "SELECT DISTINCT"
                + " A.Nama"
                + ",A.SN"
                + " FROM REF_BERKAS_PPJB A WHERE Project = '" + Project + "'";

            rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Daftar unit untuk kondisi price list yang dipilih tidak ada.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;
                TextBox tgl;
                CheckBox cb;
                HtmlInputButton btn;

                l = new Label();
                l.Text = "<tr>"
                    //+ "<td><a show-modal='#ModalPopUp' modal-title='Edit Unit' modal-url='UnitEdit.aspx?NoStock=" + rs.Rows[i]["NoStock"] + "'>" + rs.Rows[i]["NoStock"] + "</a></td>"
                    + "<td>" + rs.Rows[i]["Nama"] + "</td>"
                    + "<td>:</td>";
                //+ "<td align='right'>" + Cf.Num(rs.Rows[i]["Luas"]) + "</td>"
                //+ "<td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                RadioButtonList r1 = new RadioButtonList();
                r1.ID = "rb_" + i;
                r1.Items.Add(new ListItem("Tidak Ada", "T"));
                r1.Items.Add(new ListItem("Ada", "A"));
                r1.RepeatDirection = RepeatDirection.Horizontal;
                r1.CellPadding = 10;
                if (NoKontrak != "") r1.SelectedIndex = Db.SingleBool("Select ISNULL((SELECT CAST(ISNULL (Value,0) AS bit) From MS_BERKAS_PPJB WHERE NoKontrak =  '" + NoKontrak + "' AND NoBerkas='" + (i + 1) + "'),0)") ? 1 : 0;
                list.Controls.Add(r1);

                l = new Label();
                l.Text = "</td></tr>";
                list.Controls.Add(l);

            }
        }
        public void Fill2()
        {

            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT A.NoKontrak,A.PersenLunas,A.Project, B.*"
                + " FROM MS_KONTRAK A LEFT JOIN MS_PPJB B ON A.NoKontrak = B.NoKontrak "
                + "WHERE A.NoKontrak = '" + NoKontrak + "'";
            //string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";

            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]);
                string ParamID = "FormatPPJB" + rs.Rows[0]["Project"];
                string minppjb = Db.SingleString("SELECT ISNULL(Value,'') FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");

                if ((decimal)rs.Rows[0]["PersenLunas"] < Convert.ToDecimal(minppjb))
                    lunasinfo.Text = "PELUNASAN BELUM MENCAPAI " + minppjb.ToString() + "%";

                try
                {
                    tglkp.Text = Cf.Day(rs.Rows[0]["TglLengkapPPJB"]);
                }
                catch { }
            }
        }

        private void AutoID()
        {
            int c = Db.SingleInteger("SELECT COUNT(NoBerkas) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_BERKAS_PPJB");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                noberkas.Text = c.ToString();

                if (isUnique()) hasfound = true;
            }
        }

        private bool isUnique()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_BERKAS_PPJB WHERE NoBerkas = '" + NoBerkas + "'");

            if (c != 0)
                x = false;

            return x;
        }

        private bool Save()
        {
            if (validBerkas())
            {
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");                

                string strSql = "SELECT DISTINCT"
                   + " A.Nama"
                   + ",A.SN"
                   + " FROM REF_BERKAS_PPJB A WHERE Project = '" + Project + "'";

                rs5 = Db.Rs(strSql);
                for (int i = 0; i < rs5.Rows.Count; i++)
                {
                    RadioButtonList rbl = (RadioButtonList)list.FindControl("rb_" + i);
                    int c = Db.SingleInteger("SELECT COUNT(*) FROM MS_BERKAS_PPJB WHERE NoKontrak = '" + NoKontrak + "' AND NoBerkas = '" + (i + 1) + "'");
                    if (c == 0)
                    {
                        {
                            string sql = "INSERT INTO MS_BERKAS_PPJB (NoKontrak,NoBerkas, Nama, Value)"
                             + "VALUES('" + NoKontrak + "','" + (i + 1) + "','" + rs5.Rows[i][0] + "','" + rbl.SelectedIndex + "')";
                            Db.Execute(sql);
                        }
                    }
                    else
                    {
                        {
                            Db.Execute("UPDATE MS_BERKAS_PPJB SET Value = '" + rbl.SelectedIndex + "' WHERE NoKontrak = '" + NoKontrak + "' AND NoBerkas = '" + (i + 1) + "'");
                        }
                    }
                    if(tglkp.Text != "")
                    {
                        DateTime TglLengkap = Convert.ToDateTime(tglkp.Text);
                        Db.Execute("UPDATE MS_BERKAS_PPJB SET TglLengkap = '" + TglLengkap + "' WHERE NoKontrak = '" + NoKontrak + "' AND NoBerkas = '" + (i + 1) + "'");
                    }
                }

                DataTable rsBef = Db.Rs("SELECT "
                            + " NoKontrak AS [No. Kontrak]"
                            + ", NoPPJB"
                            + ", NoPPJBm"
                            + ", PPJB"
                            + ", PPJBu"
                            //+ ", TglPPJB"
                            + ",CONVERT(varchar,TglPPJB,106) AS [Tanggal PPJB]"
                            + ", TglCetakPPJB"
                            + ", TglTTDPPJB"
                            + ", TglLengkapPPJB"
                            + ", Biaya"
                            + ", KetPPJB"
                            + ", KTPMilik"
                            + ", KTPIstri"
                            + ", KK"
                            + ", SNKH"
                            + ", SKK"
                            + ", RK"
                            + ", BT"
                            + ", KW"
                            + ", DU"
                            + ", DL"
                            + ", SM"
                            + " FROM MS_PPJB"
                            + " WHERE NoKontrak = '" + NoKontrak + "'"
                            );

                string status = "S"; // B = Belum, S = Target, D = PPJB, T = Tanda Tangan

                Db.Execute("UPDATE MS_PPJB SET"
                         + " PPJB ='" + status + "'"
                         + " WHERE NoKontrak = '" + NoKontrak + "'"
                           );

                if (tglkp.Text != "")
                {
                    Db.Execute("UPDATE MS_PPJB SET TglLengkapPPJB='" + Convert.ToDateTime(tglkp.Text) + "' WHERE NoKontrak = '" + NoKontrak + "'");
                }

                DataTable rsAft = Db.Rs("SELECT "
                   + " NoKontrak AS [No. Kontrak]"
                   //+ ",CONVERT(varchar,TglKontrak,106) AS [Tanggal Kontrak]"
                   + ", NoPPJB"
                   + ", NoPPJBm"
                   + ", PPJB"
                   + ", PPJBu"
                   //+ ", TglPPJB"
                   + ",CONVERT(varchar,TglPPJB,106) AS [Tanggal PPJB]"
                   + ", TglCetakPPJB"
                   + ", TglTTDPPJB"
                   + ", TglLengkapPPJB"
                   + ", Biaya"
                   + ", KetPPJB"
                   + ", KTPMilik"
                   + ", KTPIstri"
                   + ", KK"
                   + ", SNKH"
                   + ", SKK"
                   + ", RK"
                   + ", BT"
                   + ", KW"
                   + ", DU"
                   + ", DL"
                   + ", SM"
                   + " FROM MS_PPJB"
                   + " WHERE NoKontrak = '" + NoKontrak + "'"
                   );
                //Logfile
                string Ket = Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogKontrak"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoKontrak + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");                
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                return true;
            }
            else
            {
                return false;
            }

        }
        private bool validBerkas()
        {
            string s = "";
            bool x = true;

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Harap memilih kondisi dari tiap tiap kelengkapan data"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("KontrakBerkasPPJB.aspx?done=" + NoKontrak);
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
            }
        }

        private string NoBerkas
        {
            get
            {
                return Cf.Pk(noberkas.Text);
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