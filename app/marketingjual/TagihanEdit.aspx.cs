using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class TagihanEdit : System.Web.UI.Page
    {
        protected DataTable rs;
        protected DataTable rs2;
        Label l;
        TextBox bx;
        RadioButton bf;
        RadioButton dp;
        RadioButton ang;
        RadioButton adm;
        DropDownList jenis;
        CheckBox cb;
        HtmlButton bt;
        HtmlGenericControl div;
        HtmlGenericControl span;
        HtmlGenericControl label;
        HtmlGenericControl italic;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
            }

            FeedBack();

            FillTable();
            Js.Confirm(this, "Lanjutkan proses edit jadwal tagihan?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Jadwal Tagihan Berhasil...";
            }
        }

        private void Fill()
        {
            barunilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            barunilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            barunilai.Attributes["onblur"] = "CalcBlur(this);";
            //if(Mi.DbPrefix == "ISC064a_")
            //    barudll.Visible = true;
            //else
            barudll.Visible = false;

            cancel.Attributes["onclick"] = "location.href='KontrakJadwalTagihan.aspx?NoKontrak=" + NoKontrak + "'";

            string strSql = "SELECT "
                + " MS_KONTRAK.*"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('BF','DP','ANG')) AS TotalTagihan"
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('ADM')) AS TotalBiaya"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

            DataTable rsHeader = Db.Rs(strSql);

            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nokontrak.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
                unit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
                nama.Text = rsHeader.Rows[0]["Cs"].ToString();
                agent.Text = rsHeader.Rows[0]["Ag"].ToString();

                nilai.Text = Cf.Num(rsHeader.Rows[0]["NilaiKontrak"]);
                totaltagihan.Text = Cf.Num(rsHeader.Rows[0]["TotalTagihan"]);
                totalbiaya.Text = Cf.Num(rsHeader.Rows[0]["TotalBiaya"]);
                tagihanbiaya.Text = Cf.Num((decimal)rsHeader.Rows[0]["TotalTagihan"] + (decimal)rsHeader.Rows[0]["TotalBiaya"]);
                outofbalance.Text = Cf.Num(rsHeader.Rows[0]["OutBalance"]);
                if (outofbalance.Text == "0") outtr.Visible = false;
                skema.Text = rsHeader.Rows[0]["Skema"].ToString();
            }
        }

        private void FillTable()
        {
            list.Controls.Clear();

            rs = Db.Rs("SELECT * FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY TglJT");
            Rpt.NoData(list, rs, "Tidak ada tagihan untuk kontrak tersebut.");

            int b = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;
                b++;

                //No
                l = new Label();
                l.Text = "<tr>"
                    + "<td>" + (i + 1).ToString() + ".</td>";
                list.Controls.Add(l);

                //Tipe
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                bf = new RadioButton();
                bf.CssClass = "radio";
                bf.ID = "bf_" + i.ToString();
                bf.GroupName = "tipe" + i.ToString();
                bf.Text = "BF";
                if (rs.Rows[i]["Tipe"].ToString() == "BF")
                    bf.Checked = true;
                else
                    bf.Checked = false;
                bf.Font.Size = 8;
                bf.Attributes["OnClick"] += "nonaktif(bf_" + i + ",dp_" + i + ",ang_" + i + ",adm_" + i + ",jenis_" + i + ")";
                list.Controls.Add(bf);

                l = new Label();
                l.Text = "&nbsp;";
                list.Controls.Add(l);

                dp = new RadioButton();
                dp.CssClass = "radio";
                dp.ID = "dp_" + i.ToString();
                dp.GroupName = "tipe" + i.ToString();
                dp.Text = "DP";
                if (rs.Rows[i]["Tipe"].ToString() == "DP")
                    dp.Checked = true;
                else
                    dp.Checked = false;
                dp.Font.Size = 8;
                dp.Attributes["OnClick"] += "nonaktif(bf_" + i + ",dp_" + i + ",ang_" + i + ",adm_" + i + ",jenis_" + i + ")";
                list.Controls.Add(dp);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                ang = new RadioButton();
                ang.CssClass = "radio";
                ang.ID = "ang_" + i.ToString();
                ang.GroupName = "tipe" + i.ToString();
                ang.Text = "ANG";
                if (rs.Rows[i]["Tipe"].ToString() == "ANG")
                    ang.Checked = true;
                else
                    ang.Checked = false;
                ang.Font.Size = 8;
                ang.Attributes["OnClick"] += "nonaktif(bf_" + i + ",dp_" + i + ",ang_" + i + ",adm_" + i + ",jenis_" + i + ")";
                list.Controls.Add(ang);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);

                adm = new RadioButton();
                adm.CssClass = "radio";
                adm.ID = "adm_" + i.ToString();
                adm.GroupName = "tipe" + i.ToString();
                adm.Text = "ADM";
                if (rs.Rows[i]["Tipe"].ToString() == "ADM")
                    adm.Checked = true;
                else
                    adm.Checked = false;
                adm.Font.Size = 8;
                adm.Attributes["OnClick"] += "nonaktif(bf_" + i + ",dp_" + i + ",ang_" + i + ",adm_" + i + ",jenis_" + i + ")";
                list.Controls.Add(adm);

                l = new Label();
                l.Text = "&nbsp;&nbsp;";
                list.Controls.Add(l);
                //Jenis
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                jenis = new DropDownList();
                jenis.ID = "jenis_" + i;
                jenis.Items.Add("Jenis :");
                jenis.Items.Add("Denda Manual");
                jenis.Items.Add("PPJB");
                jenis.Items.Add("AJB");
                jenis.Items.Add("BAST");
                jenis.Items.Add("Pengalihan Hak");
                jenis.Items.Add("Pindah Unit");
                jenis.Items.Add("LAIN");
                jenis.Enabled = true;

                if (adm.Checked)
                {
                    if (rs.Rows[i]["Jenis"].ToString() == "DO")
                    {
                        jenis.Enabled = false;
                    }
                    else
                    {
                        jenis.Enabled = true;
                    }
                }
                list.Controls.Add(jenis);

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

                div = new HtmlGenericControl("div");

                bx = new TextBox();
                bx.ID = "tgl_" + Convert.ToString(i);
                bx.Width = 75;
                bx.CssClass = "tgl form-control form-control-small";
                bx.Text = Cf.Day(rs.Rows[i]["TglJT"]);
                bx.Attributes.Add("onblur", "updateSetelah(" + i + ");");
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);
                //Tanggal

                div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "input-group input-small");
                div.Attributes.Add("style", "margin-top: 0px; margin-left: 0px;");

                //tgl = new TextBox();
                //tgl.ID = "tgl_" + i;
                //tgl.CssClass = "tgl form-control form-control-small";
                //tgl.Text = Cf.Day(Convert.ToDateTime(DateTime.Today));
                //tgl.Attributes["style"] = "font:8pt; width:65%";
                div.Controls.Add(bx);

                span = new HtmlGenericControl("span");
                span.Attributes.Add("style", "height: 34px; display: block;");
                span.Attributes.Add("class", "input-group-btn");

                label = new HtmlGenericControl("label");
                label.Attributes.Add("for", bx.ID);
                label.Attributes.Add("class", "btn-a default btn-cal");

                italic = new HtmlGenericControl("i");
                italic.Attributes.Add("class", "fa fa-calendar");
                label.Controls.Add(italic);
                span.Controls.Add(label);

                div.Controls.Add(span);
                list.Controls.Add(div);

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
                bx.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
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
                cb.Checked = Convert.ToBoolean(rs.Rows[i]["KPR"]);
                list.Controls.Add(cb);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                decimal lunas = Db.SingleDecimal("SELECT COUNT (*) FROM MS_PELUNASAN WHERE NoKontrak = '" + NoKontrak + "' AND NoTagihan = '" + rs.Rows[i]["NoUrut"] + "' AND SudahCair =1");
                if (lunas == 0)
                {
                    l.Text = "<td>"
                   + "<a style='font:8pt' href=\"javascript:hapus('" + NoKontrak + "','" + rs.Rows[i]["NoUrut"] + "')\">Delete...</a>"
                   + "</td>";
                }
                else
                {
                    l.Text = "";
                }

                list.Controls.Add(l);

                //l = new Label();
                //l.Text = "<td>";
                //list.Controls.Add(l);

                ////				l = new Label();
                ////				string strAkunting = "";
                ////				if(Func.CekAkunting(NoKontrak, Cf.Pk(rs.Rows[i]["NoUrut"])))
                ////					strAkunting = "<span style='color: red;'>Akunting</span>";
                ////				l.Text = strAkunting;
                ////				list.Controls.Add(l);

                //l = new Label();
                //list.Controls.Add(l);

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
            int b = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox nama = (TextBox)list.FindControl("nama_" + i);
                TextBox tgl = (TextBox)list.FindControl("tgl_" + i);
                TextBox nilai = (TextBox)list.FindControl("nilai_" + i);
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
                else
                    err.Text = "";


            }

            if (barunama.Text != "" || barunilai.Text != "" || barutgl.Text != "")
            {
                if (Cf.isEmpty(barunama))
                {
                    x = false;
                    if (s == "") s = barunama.ID;
                    baruc.Text = "Kosong";
                }
                else if (!Cf.isTgl(barutgl))
                {
                    x = false;
                    if (s == "") s = barutgl.ID;
                    baruc.Text = "Tanggal";
                }
                else if (!Cf.isMoney(barunilai))
                {
                    x = false;
                    if (s == "") s = barunilai.ID;
                    baruc.Text = "Angka";
                }
                else
                    baruc.Text = "";
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

        private bool Save()
        {
            if (valid())
            {
                DataTable rsBef = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                    + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    if (!Response.IsClientConnected) break;

                    RadioButton bf = (RadioButton)list.FindControl("bf_" + i);
                    RadioButton dp = (RadioButton)list.FindControl("dp_" + i);
                    RadioButton ang = (RadioButton)list.FindControl("ang_" + i);
                    RadioButton adm = (RadioButton)list.FindControl("adm_" + i);
                    RadioButton dll = (RadioButton)list.FindControl("dll_" + i);

                    TextBox namatagihan = (TextBox)list.FindControl("nama_" + i);
                    TextBox tgljt = (TextBox)list.FindControl("tgl_" + i);
                    TextBox nilaitagihan = (TextBox)list.FindControl("nilai_" + i);
                    CheckBox kpr = (CheckBox)list.FindControl("kpr_" + i);
                    DropDownList jenis = (DropDownList)list.FindControl("jenis_" + i);
                    int NoUrut = Convert.ToInt32(rs.Rows[i]["NoUrut"]);

                    string Tipe = "";
                    if (bf.Checked)
                        Tipe = "BF";
                    else if (dp.Checked)
                        Tipe = "DP";
                    else if (ang.Checked)
                        Tipe = "ANG";
                    else
                        Tipe = "ADM";

                    string Jenis = "";

                    if (jenis.SelectedIndex == 0)
                    {
                        Jenis = rs.Rows[i]["Jenis"].ToString();
                    }
                    else
                    {
                        Jenis = jenis.SelectedItem.ToString();
                    }

                    string Nama = Cf.Str(namatagihan.Text);
                    DateTime TglJT = Convert.ToDateTime(tgljt.Text);
                    decimal Nilai = Convert.ToDecimal(nilaitagihan.Text);
                    bool KPR = kpr.Checked;

                    string strSqlAnomali = "SELECT"
                            + " NamaTagihan AS [Nama Tagihan]"
                            + ", TglJT AS [Tgl. Jatuh Tempo]"
                            + ", NilaiTagihan AS [Nilai]"
                            + ", Tipe AS [Tipe]"
                            + " FROM MS_TAGIHAN"
                            + " WHERE NoKontrak = '" + NoKontrak + "'"
                            + " AND NoUrut = " + NoUrut
                            ;
                    DataTable AnomaliBef = Db.Rs(strSqlAnomali);

                    Db.Execute("EXEC spTagihanEdit "
                        + " '" + NoKontrak + "'"
                        + ", " + NoUrut
                        + ",'" + Nama + "'"
                        + ",'" + TglJT + "'"
                        + ", " + Nilai
                        + ",'" + Tipe + "'"
                        );

                    //Response.Write(NoKontrak + " " + TglJT + " " + Nilai + "<br/>");

                    //Update manual
                    Db.Execute("UPDATE MS_TAGIHAN SET KPR = " + Cf.BoolToSql(KPR) + ", Jenis = '" + Jenis + "' WHERE NoKontrak = '" + NoKontrak + "' AND NoUrut = " + NoUrut);

                    /*Akunting*/
                    //					if(Tipe != rs.Rows[i]["Tipe"].ToString())
                    //					{
                    //						bool Akunting = Db.SingleBool("SELECT Akunting FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    //						
                    //						if(Akunting)
                    //						{
                    //							DataTable AnomaliAft = Db.Rs(strSqlAnomali);
                    //							string NoVoucher = Db.SingleString("SELECT NoVoucher FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    //							
                    //							Akun.InsertAnomali("JUAL", NoKontrak, Cf.LogCapture(AnomaliBef), Cf.LogCapture(AnomaliAft), "EDIT TAGIHAN", "", NoVoucher);
                    //						}
                    //					}
                    /**********/


                }

                Tambah();

                DataTable rsAft = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                    + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                DataTable rsDetail = Db.Rs("SELECT"
                    + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                    + ",MS_KONTRAK.NoUnit AS [Unit]"
                    + ",MS_CUSTOMER.Nama AS [Customer]"
                    + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                    + ",MS_KONTRAK.Skema AS [Skema]"
                    + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                    + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                    + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                string Ket = Cf.LogCapture(rsDetail)
                    + "<br>---EDIT TAGIHAN---<br>"
                    + Cf.LogList(rsBef, rsAft, "JADWAL TAGIHAN");

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

                Func.CekKomisi(NoKontrak);

                return true;

            }
            else
                return false;
        }

        private void Tambah()
        {
            if (barunama.Text != "" || barunilai.Text != "" || barutgl.Text != "")
            {
                string Tipe = "";
                if (barubf.Checked)
                    Tipe = "BF";
                else if (barudp.Checked)
                    Tipe = "DP";
                else if (baruang.Checked)
                    Tipe = "ANG";
                else
                    Tipe = "ADM";

                string Nama = Cf.Str(barunama.Text);
                DateTime TglJT = Convert.ToDateTime(barutgl.Text);
                decimal Nilai = Convert.ToDecimal(barunilai.Text);
                string Jenis = jenis2.SelectedValue;

                Db.Execute("EXEC spTagihanDaftar "
                    + " '" + NoKontrak + "'"
                    + ",'" + Nama + "'"
                    + ",'" + TglJT + "'"
                    + ", " + Nilai
                    + ",'" + Tipe + "'"
                    );

                //Update manual
                int NoUrut = Db.SingleInteger("SELECT ISNULL(MAX(NoUrut), 0) FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_TAGIHAN"
                    + " SET  "
                    + "KPR = " + Cf.BoolToSql(barukpr.Checked)
                    + ", Jenis = '" + Jenis + "'"
                    + " WHERE NoKontrak = '" + NoKontrak + "'"
                    + " AND NoUrut = " + NoUrut
                    );
            }
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("KontrakJadwalTagihan.aspx?NoKontrak=" + NoKontrak + "&done=1");
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("TagihanEdit.aspx?NoKontrak=" + NoKontrak + "&done=1");

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
