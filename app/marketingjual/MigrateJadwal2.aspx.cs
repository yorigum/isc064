using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class MigrateJadwal2 : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
                
                Js.Confirm(this, "Lanjutkan proses migrate jadwal tagihan?");
            }

            FillTable();
        }

        private void Fill()
        {
            cancel.Attributes["onclick"] = "location.href='MigrateJadwal.aspx?NoKontrak=" + NoKontrak + "'";

            string strSql = "SELECT * "
                + " FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rsHeader = Db.Rs(strSql);

            if (rsHeader.Rows.Count > 0)
            {
                nokontrak.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
                agent.Text = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rsHeader.Rows[0]["NoAgent"] + "'");
                nounit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
                cust.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + rsHeader.Rows[0]["NoCustomer"] + "'");
                nilaikontrak.Text = Cf.Num(rsHeader.Rows[0]["NilaiKontrak"]);
                skema.Text = rsHeader.Rows[0]["Skema"].ToString();
            }
            else
            {
                cek.Text = "Kontrak No Exists";
                save.Enabled = false;
            }
        }

        private void FillTable()
        {
            list.Controls.Clear();

            rs = Db.Rs("SELECT * FROM MIGRATE_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND Approved = 0");
            Rpt.NoData(list, rs, "Tidak ada tagihan untuk kontrak tersebut.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                //No
                Label l;
                TextBox bx;
                RadioButton bf;
                RadioButton dp;
                RadioButton ang;
                RadioButton adm;
                RadioButton dll;
                CheckBox cb;
                HtmlInputButton btn;

                l = new Label();
                l.ID = "nourut_" + Convert.ToString(i);
                l.Attributes["title"] = rs.Rows[i]["NoUrut"].ToString();
                l.Text = "<tr>"
                    + "<td>" + rs.Rows[i]["NoUrut"].ToString() + ".</td>";
                list.Controls.Add(l);

                //Tipe
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bf = new RadioButton();
                bf.ID = "bf_" + i.ToString();
                bf.GroupName = "tipe" + i.ToString();
                bf.Text = "BF";
                if (rs.Rows[i]["Tipe"].ToString() == "BF")
                    bf.Checked = true;
                else
                    bf.Checked = false;
                bf.Font.Size = 8;
                list.Controls.Add(bf);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                dp = new RadioButton();
                dp.ID = "dp_" + i.ToString();
                dp.GroupName = "tipe" + i.ToString();
                dp.Text = "DP";
                if (rs.Rows[i]["Tipe"].ToString() == "DP")
                    dp.Checked = true;
                else
                    dp.Checked = false;
                dp.Font.Size = 8;
                list.Controls.Add(dp);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                ang = new RadioButton();
                ang.ID = "ang_" + i.ToString();
                ang.GroupName = "tipe" + i.ToString();
                ang.Text = "ANG";
                if (rs.Rows[i]["Tipe"].ToString() == "ANG")
                    ang.Checked = true;
                else
                    ang.Checked = false;
                ang.Font.Size = 8;
                list.Controls.Add(ang);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                adm = new RadioButton();
                adm.ID = "adm_" + i.ToString();
                adm.GroupName = "tipe" + i.ToString();
                adm.Text = "ADM";
                if (rs.Rows[i]["Tipe"].ToString() == "ADM")
                    adm.Checked = true;
                else
                    adm.Checked = false;
                adm.Font.Size = 8;
                list.Controls.Add(adm);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);
                if (Mi.DbPrefix == "ISC064_")
                {
                    dll = new RadioButton();
                    dll.ID = "dll_" + Convert.ToString(i);
                    dll.GroupName = "tipe" + Convert.ToString(i);
                    dll.Text = "DLL";
                    if (rs.Rows[i]["Tipe"].ToString() == "DLL")
                        dll.Checked = true;
                    else
                        dll.Checked = false;
                    dll.Font.Size = 8;
                    list.Controls.Add(dll);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);
                }

                //Nama
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "nama_" + Convert.ToString(i);
                bx.Width = 140;
                bx.CssClass = "txt";
                bx.Text = rs.Rows[i]["NamaTagihan"].ToString();
                bx.MaxLength = 50;
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                //Tgl
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bx = new TextBox();
                bx.ID = "tgl_" + Convert.ToString(i);
                bx.Width = 75;
                bx.CssClass = "txt_center";
                bx.Text = Cf.Day(rs.Rows[i]["TglJT"]);
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "&nbsp;";
                list.Controls.Add(l);

                btn = new HtmlInputButton();
                btn.Value = "...";
//                btn.Attributes["onclick"] = "openCalendar('tgl_" + i.ToString() + "')";
                btn.Attributes["class"] = "btn";
                list.Controls.Add(btn);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                //Nilai
                bx = new TextBox();
                bx.ID = "nilai_" + Convert.ToString(i);
                bx.CssClass = "txt_num";
                bx.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiTagihan"]), 0));
                bx.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                bx.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                bx.Attributes["onblur"] = "CalcBlur(this);";
                bx.Width = 90;
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                //Denda
                bx = new TextBox();
                bx.ID = "denda_" + Convert.ToString(i);
                bx.CssClass = "txt_num";
                bx.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Denda"]), 0));
                bx.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                bx.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                bx.Attributes["onblur"] = "CalcBlur(this);";
                bx.Width = 90;
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                cb = new CheckBox();
                cb.ID = "kpr_" + Convert.ToString(i);
                //cb.Checked = Convert.ToBoolean(rs.Rows[i]["KPR"]);
                list.Controls.Add(cb);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>"
                    + "<a style='font:8pt' href=\"javascript:hapus('" + NoKontrak + "','" + rs.Rows[i]["NoUrut"] + "')\">Delete...</a>"
                    + "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                l = new Label();
                l.ID = "err_" + Convert.ToString(i);
                l.CssClass = "err";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td></tr>";
                list.Controls.Add(l);
            }
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            int kontrak = Db.SingleInteger("SELECT COUNT(NoKontrak) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            if (kontrak == 0)
            {
                x = false;
                cek.Text = "No. Kontrak Not Exists ";
            }
            else
                cek.Text = "";

            rs = Db.Rs("SELECT * FROM MIGRATE_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND Approved = 0");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox nama = (TextBox)list.FindControl("nama_" + i);
                TextBox tgl = (TextBox)list.FindControl("tgl_" + i);
                TextBox nilai = (TextBox)list.FindControl("nilai_" + i);
                TextBox denda = (TextBox)list.FindControl("denda_" + i);
                Label err = (Label)list.FindControl("err_" + i);

                if (Cf.isEmpty(nama))
                {
                    x = false;
                    if (s == "") s = nama.ID;
                    err.Text = "Kosong";
                }
                else if (!Cf.isTgl(tgl))
                {
                    x = false;
                    if (s == "") s = tgl.ID;
                    err.Text = "Tanggal";
                }
                else if (!Cf.isMoney(nilai))
                {
                    x = false;
                    if (s == "") s = nilai.ID;
                    err.Text = "Angka";
                }
                else if (!Cf.isMoney(denda))
                {
                    x = false;
                    if (s == "") s = denda.ID;
                    err.Text = "Angka";
                }
                else
                    err.Text = "";
            }

            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script language='javascript'>"
                    + " document.getElementById('" + s + "').focus();"
                    + " document.getElementById('" + s + "').select();"
                    + "</script>"
                    );
            }

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                DataTable rsBef = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) + '   ' + CONVERT(VARCHAR,Denda,1) "
                    + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                rs = Db.Rs("SELECT * FROM MIGRATE_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' AND Approved = 0");
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    RadioButton bf = (RadioButton)list.FindControl("bf_" + i);
                    RadioButton dp = (RadioButton)list.FindControl("dp_" + i);
                    RadioButton ang = (RadioButton)list.FindControl("ang_" + i);
                    RadioButton adm = (RadioButton)list.FindControl("adm_" + i);
                    RadioButton dll = (RadioButton)list.FindControl("dll_" + i);

                    TextBox namatagihan = (TextBox)list.FindControl("nama_" + i);
                    TextBox tgljt = (TextBox)list.FindControl("tgl_" + i);
                    TextBox nilaitagihan = (TextBox)list.FindControl("nilai_" + i);
                    TextBox denda = (TextBox)list.FindControl("denda_" + i);
                    CheckBox kpr = (CheckBox)list.FindControl("kpr_" + i);

                    string Tipe = "";
                    if (bf.Checked)
                        Tipe = "BF";
                    else if (dp.Checked)
                        Tipe = "DP";
                    else if (ang.Checked)
                        Tipe = "ANG";
                    else if ((Mi.DbPrefix == "ISC064_") && (dll.Checked))
                        Tipe = "DLL";
                    else
                        Tipe = "ADM";

                    string Nama = Cf.Str(namatagihan.Text);
                    DateTime TglJT = Convert.ToDateTime(tgljt.Text);
                    decimal Nilai = Convert.ToDecimal(nilaitagihan.Text);
                    decimal Denda = Convert.ToDecimal(denda.Text);
                    bool KPR = kpr.Checked;

                    Db.Execute("EXEC spTagihanDaftarM "
                        + " '" + NoKontrak + "'"
                        + ",'" + Nama + "'"
                        + ",'" + TglJT + "'"
                        + ", " + Nilai
                        + ", " + Denda
                        + ",'" + Tipe + "'"
                        + ", " + KPR
                        );

                    Db.Execute("UPDATE MIGRATE_TAGIHAN SET Approved = 1 WHERE NoKontrak = '" + NoKontrak + "'");
                }

                DataTable rsTagihan = Db.Rs("SELECT "
                + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                //Logfile

                string Ket = Cf.LogCapture(rs)
                    + "<br>---EDIT TAGIHAN---<br>"
                    + Cf.LogList(rsBef, rsTagihan, "JADWAL TAGIHAN");

                Db.Execute("EXEC spLogKontrak"
                    + " 'EJT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoKontrak + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("MigrateJadwal.aspx?done=" + NoKontrak);
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
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
