using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;

namespace ISC064.KPA
{
    public partial class TagihanEdit : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
                //barutipe.Items.Clear();
            }

            FeedBack();

            FillTable();
			Js.Confirm(this, "Lanjutkan proses edit jadwal tagihan KPR?");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Edit Jadwal Tagihan KPR Berhasil...";
			}
		}
		
		private void Fill()
		{
			barunilaipersen.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            barunilaipersen.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            barunilaipersen.Attributes["onblur"] = "CalcBlur(this);";

            barunilailumpsum.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            barunilailumpsum.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            barunilailumpsum.Attributes["onblur"] = "CalcBlur(this);";

            //cancel.Attributes["onclick"] = "location.href='KontrakJadwalTagihan.aspx?NoKontrak="+NoKontrak+"'";

            string strSql = "SELECT "
                + " MS_KONTRAK.*"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN_KPA WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('BF','DP','ANG')) AS TotalTagihan"
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN_KPA WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('ADM')) AS TotalBiaya"
                + ",(SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN_KPA WHERE NoKontrak = MS_KONTRAK.NoKontrak AND NoTagihan <> 0 ) AS TotalPelunasanKPA"
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak AND Tipe IN ('BF','DP','ANG') AND KPR = 1) AS NilaiKPA"
                + ",(SELECT ISNULL(SUM(NilaiTagihanPersen),0) FROM MS_TAGIHAN_KPA WHERE NoKontrak = MS_KONTRAK.NoKontrak)  AS TagihanPersenKPA"
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN_KPA WHERE NoKontrak = MS_KONTRAK.NoKontrak)  AS TagihanLumpsumKPA"
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

                nilai.Text = Cf.Num(rsHeader.Rows[0]["NilaiKPA"]);
                nilailebih.Text = Cf.Num(rsHeader.Rows[0]["NilaiKelebihanKPA"]);
				totaltagihan.Text = Cf.Num(rsHeader.Rows[0]["TagihanLumpsumKPA"]);
                decimal outbe = Convert.ToDecimal(rsHeader.Rows[0]["NilaiKPA"]) + Convert.ToDecimal(rsHeader.Rows[0]["NilaiKelebihanKPA"]) - Convert.ToDecimal(rsHeader.Rows[0]["TagihanLumpsumKPA"]);
				outofbalance.Text = Cf.Num(outbe);
				if(outbe == 0) outtr.Visible = false;
				skema.Text = rsHeader.Rows[0]["Skema"].ToString();

                totpersen.Text = Cf.Num(rsHeader.Rows[0]["TagihanPersenKPA"]);
                totlumpsum.Text = Cf.Num(rsHeader.Rows[0]["TagihanLumpsumKPA"]);

                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                DataTable rsA = Db.Rs("SELECT * FROM REF_RETENSI WHERE Project = '" + Project + "' ORDER BY Kode");
                for (int a = 0; a < rsA.Rows.Count; a++)
                {
                    barutipe.Items.Add(new ListItem(rsA.Rows[a]["NamaKategori"].ToString(), Cf.Pk(rsA.Rows[a]["Kode"])));
                }
            }
        }

        private void FillTable()
        {
            list.Controls.Clear();
            string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
			rs = Db.Rs("SELECT * "
                + ",(SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_TAGIHAN_KPA.NoKontrak AND Tipe IN ('BF','DP','ANG') AND KPR = 1) AS NilaiKPA"
                + " FROM MS_TAGIHAN_KPA WHERE NoKontrak = '" + NoKontrak + "'");
            Rpt.NoData(list, rs, "Tidak ada tagihan untuk kontrak tersebut.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                //No
                Label l;
                TextBox bx;
                DropDownList tipe;
                RadioButtonList rbl;

                int CountPelunasan = Db.SingleInteger("select count(*) from ms_pelunasan_kpa where NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' and NoTagihan = '" + Convert.ToInt32(rs.Rows[i]["NoUrut"]) + "'");

                l = new Label();
                l.Text = "<tr>"
                    + "<td>" + rs.Rows[i]["NoUrut"].ToString() + ".</td>";
                list.Controls.Add(l);

                //Tipe
                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                tipe = new DropDownList();
                tipe.ID = "tipe_" + i;
                DataTable rsA = Db.Rs("SELECT * FROM REF_RETENSI WHERE Project = '" + Project + "' ORDER BY Kode");
                for (int a = 0; a < rsA.Rows.Count; a++)
                {
                    tipe.Items.Add(new ListItem(rsA.Rows[a]["NamaKategori"].ToString(), Cf.Pk(rsA.Rows[a]["Kode"])));
                }
                tipe.SelectedValue = rs.Rows[i]["Tipe"].ToString();
                list.Controls.Add(tipe);

                //Nama
                l = new Label();
                l.Text = "</td><td>";
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
                bx.CssClass = "tgl txt_center";
                bx.Text = Cf.Day(rs.Rows[i]["TglJT"]);
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "&nbsp;";
                list.Controls.Add(l);

                HtmlGenericControl c = new HtmlGenericControl("label");
                c.Attributes.Add("for", "tgl_" + i + "");
                c.Attributes.Add("class", "fa fa-calendar");
                c.Attributes.Add("class", "btn btn-cal");

                HtmlGenericControl italic = new HtmlGenericControl("i");
                italic.Attributes.Add("class", "fa fa-calendar");
                c.Controls.Add(italic);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(c);
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td style=display:none>";
                list.Controls.Add(l);

                rbl = new RadioButtonList();
                rbl.ID = "tipetarif_" + Convert.ToString(i);
                rbl.RepeatDirection = RepeatDirection.Horizontal;
                rbl.Items.Add(new ListItem("Persen", "Persen"));
                rbl.Items.Add(new ListItem("Nilai", "Nilai"));
                if (rs.Rows[i]["NilaiTagihanTipe"].ToString() == "Persen")
                {
                    rbl.SelectedIndex = 1;
                }
                else
                {
                    rbl.SelectedIndex = 0;
                }
                rbl.Attributes["style"] = "display:none";
                list.Controls.Add(rbl);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                //Nilai Kontrak
                bx = new TextBox();
                bx.ID = "nilaikpa_" + Convert.ToString(i);
                bx.Text = Cf.Num(rs.Rows[i]["NilaiKPA"]);
                bx.Attributes.Add("data-row", i.ToString());
                bx.Attributes["style"] = "display:none";
                list.Controls.Add(bx);

                //Nilai Persen
                bx = new TextBox();
				bx.ID = "nilaipersen_" + Convert.ToString(i);
				bx.CssClass = "txt_num persen";
                bx.Attributes.Add("data-row", i.ToString());
                bx.Text = Cf.Num(rs.Rows[i]["NilaiTagihanPersen"]);

                bx.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
				bx.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                bx.Width = 90;
				bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

				l = new Label();
				l.Text = "</td><td>";
				list.Controls.Add(l);
                
                //Nilai Lumpsum
                bx = new TextBox();
                bx.ID = "nilailumpsum_" + Convert.ToString(i);
                bx.CssClass = "txt_num nilai";
                bx.Attributes.Add("data-row", i.ToString());
                bx.Text = Cf.Num(rs.Rows[i]["NilaiTagihan"]);
                bx.Width = 90;
                bx.Attributes["style"] = "font:8pt";
                list.Controls.Add(bx);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                if (CountPelunasan != 0)
                {
                    l.Text = "<td> <font style='color:Green'>Sudah Cair</font> </td>";
                }
                else
                {
                    l.Text = "<td>"
                        + "<a style='font:8pt' href=\"javascript:hapus('" + NoKontrak + "','" + rs.Rows[i]["NoUrut"] + "')\">Delete...</a>"
                        + "</td>";
                }
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                //				l = new Label();
                //				string strAkunting = "";
                //				if(Func.CekAkunting(NoKontrak, Cf.Pk(rs.Rows[i]["NoUrut"])))
                //					strAkunting = "<span style='color: red;'>Akunting</span>";
                //				l.Text = strAkunting;
                //				list.Controls.Add(l);

                l = new Label();
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

		private bool validedit()
		{
            //edit value
			bool x = true;
			string s = "";

			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;
				
				TextBox nama = (TextBox) list.FindControl("nama_" + i);
				TextBox tgl = (TextBox) list.FindControl("tgl_" + i);
				TextBox nilaipersen = (TextBox) list.FindControl("nilaipersen_" + i);
                TextBox nilailumpsum = (TextBox)list.FindControl("nilailumpsum_" + i);
                Label err = (Label) list.FindControl("err_" + i);

				if(Cf.isEmpty(nama))
				{
					x = false;
					if(s=="") s = nama.ID;
					err.Text = "Kosong";
				}
				else if(!Cf.isTgl(tgl))
				{	
					x = false;
					if(s=="") s = tgl.ID;
					err.Text = "Tanggal";
				}
				else if(!Cf.isMoney(nilaipersen))
				{
					x = false;
					if(s=="") s = nilaipersen.ID;
					err.Text = "Angka";
				}
                else if (!Cf.isMoney(nilailumpsum))
                {
                    x = false;
                    if (s == "") s = nilailumpsum.ID;
                    err.Text = "Angka";
                }

                //new validation
                else if (Convert.ToDecimal(nilaipersen.Text) > 100)
                {
                    x = false;
                    if (s == "") s = nilaipersen.ID;
                    err.Text = "Tidak Boleh Lebih Dari 100%";
                }
                else if (Convert.ToDecimal(nilailumpsum.Text) > Convert.ToDecimal(nilai.Text))
                {
                    x = false;
                    if (s == "") s = nilailumpsum.ID;
                    err.Text = "Tidak Boleh Lebih Dari Nilai KPR";
                }
                else
					err.Text = "";
			}

			return x;
		}

        private bool validbaru()
        {
            //inputan baru
            bool x = true;
            string s = "";
            
            if (barunama.Text != "" || barutgl.Text != "" || barunilailumpsum.Text != "" || barunilaipersen.Text != "")
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
                else if (!Cf.isMoney(barunilailumpsum))
                {
                    x = false;
                    if (s == "") s = barunilailumpsum.ID;
                    baruc.Text = "Angka";
                }
                else if (!Cf.isMoney(barunilaipersen))
                {
                    x = false;
                    if (s == "") s = barunilaipersen.ID;
                    baruc.Text = "Angka";
                }

                //new validation
                else if (Convert.ToDecimal(barunilaipersen.Text) > 100)
                {
                    x = false;
                    if (s == "") s = barunilaipersen.ID;
                    baruc.Text = "Tidak Boleh Lebih Dari 100%";
                }

                //new validation
                else if (Convert.ToDecimal(totpersen.Text) > 100)
                {
                    x = false;
                    if (s == "") s = totpersen.ID;
                    totpersenc.Text = "Total Persen Tidak Boleh Lebih Dari 100%";
                }
                else if (Convert.ToDecimal(barunilailumpsum.Text) > Convert.ToDecimal(nilai.Text))
                {
                    x = false;
                    if (s == "") s = barunilailumpsum.ID;
                    baruc.Text = "Tidak Boleh Lebih Dari Nilai KPR";
                }
                else
                    baruc.Text = "";
            }

            return x;
        }

        private bool validnew()
        {
            //inputan baru
            bool x = true;
            string s = "";

            if (barunama.Text != "" || barutgl.Text != "" || barunilailumpsum.Text != "" || barunilaipersen.Text != "")
            {
                if (totpersen.Text != "" && totlumpsum.Text != "")
                {
                    if (Convert.ToDecimal(barunilaipersen.Text) > 100)
                    {
                        x = false;
                        if (s == "") s = barunilaipersen.ID;
                        noedit.Text = "Tidak Boleh Lebih Dari 100%";
                    }
                    else if (Convert.ToDecimal(barunilailumpsum.Text) > Convert.ToDecimal(nilai.Text))
                    {
                        x = false;
                        if (s == "") s = barunilaipersen.ID;
                        noedit.Text = "Tidak Boleh Lebih Dari Nilai KPR";
                    }
                }
            }

            return x;
        }

        private bool Save()
        {
			if(validbaru() && validedit() && validnew())
            {
                DataTable rsBef = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) "
                    + "FROM MS_TAGIHAN WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

                for (int i = 0; i < rs.Rows.Count; i++)
                {

                    TextBox namatagihan = (TextBox)list.FindControl("nama_" + i);
                    TextBox tgljt = (TextBox)list.FindControl("tgl_" + i);
					TextBox nilailumpsum = (TextBox) list.FindControl("nilailumpsum_" + i);
                    TextBox nilaipersen = (TextBox)list.FindControl("nilaipersen_" + i);
                    DropDownList tipe = (DropDownList)list.FindControl("tipe_" + i);
                    RadioButtonList tipetarif = (RadioButtonList)list.FindControl("tipetarif_" + i);

                    int NoUrut = Convert.ToInt32(rs.Rows[i]["NoUrut"]);
					string Tipe = tipe.SelectedValue.ToString();
					string Nama = Cf.Str(namatagihan.Text);
					DateTime TglJT = Convert.ToDateTime(tgljt.Text);
                    decimal Lumpsum = Convert.ToDecimal(nilailumpsum.Text);
                    decimal Persen = Convert.ToDecimal(nilaipersen.Text);
                    string TipeTarif = tipetarif.SelectedValue;
                    
                    string strSqlAnomali = "SELECT"
						+ " NamaTagihan AS [Nama Tagihan]"
						+ ", TglJT AS [Tgl. Jatuh Tempo]"
						+ ", Tipe AS [Tipe]"
                        + ", NilaiTagihan AS [Nilai Lumpsum]"
                        + ", NilaiTagihanPersen AS [Nilai Persen]"
                        + " FROM MS_TAGIHAN_KPA"
						+ " WHERE NoKontrak = '" + NoKontrak + "'"
						+ " AND NoUrut = " + NoUrut
						;
					DataTable AnomaliBef = Db.Rs(strSqlAnomali);

                    Db.Execute("EXEC spTagihanEditKPA "
                        + " '" + NoKontrak + "'"
                        + ", " + NoUrut
                        + ",'" + Nama + "'"
                        + ",'" + TglJT + "'"
                        + ", " + Lumpsum
                        + ",'" + Tipe + "'"
                        + ",'" + TipeTarif + "'"
                        + ",'" + Persen + "'"
                        );


                }

                Tambah();

				DataTable rsAft = Db.Rs("SELECT "
					+ "CONVERT(VARCHAR,NoUrut) + '.   ' + NamaTagihan + ' ('+Tipe+')   ' + CONVERT(VARCHAR,TglJT,106) + '   ' + CONVERT(VARCHAR,NilaiTagihan,1) + '   ' + CONVERT(VARCHAR,NilaiTagihanPersen,1) "
                    + " FROM MS_TAGIHAN_KPA WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");
                
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
					+ "<br>---EDIT TAGIHAN KPR---<br>"
					+ Cf.LogList(rsBef, rsAft, "JADWAL TAGIHAN");
				
				Db.Execute("EXEC spLogKontrak"
					+ " 'EJT-KPA'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + NoKontrak + "'"
					);

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);


                return true;
            }
            else
                return false;
        }

		private void Tambah()
		{
			if(barunama.Text!="" || barutgl.Text!="") //|| barunilai.Text != ""

            {
                string Nama = Cf.Str(barunama.Text);
                DateTime TglJT = Convert.ToDateTime(barutgl.Text);

                decimal NilaiLumpsum = 0;
                decimal NilaiPersen = 0;
                string Tipe = barutipe.SelectedValue.ToString();
                string TipeTarif = barutarif.SelectedValue;

                if(TipeTarif == "Persen")
                {
                    NilaiLumpsum = Convert.ToDecimal(barunilailumpsum.Text);
                    NilaiPersen = Convert.ToDecimal(barunilaipersen.Text);
                }
                else
                {
                    NilaiLumpsum = Convert.ToDecimal(barunilailumpsum.Text);
                    NilaiPersen = Convert.ToDecimal(barunilaipersen.Text);
                }

                Db.Execute("EXEC spTagihanDaftarKPA "
                    //+ " '" + NoKontrak + "'"
                    //+ ",'" + Nama + "'"
                    //+ ",'" + TglJT + "'"
                    //+ ",'" + NilaiLumpsum + "'"
                    //+ ",'" + Tipe + "'"
                    //+ ",'" + TipeTarif + "'"
                    //+ ",'" + NilaiPersen + "'"
                    //+ ",'' "
                    + " '" + NoKontrak + "'"
                    + ",'" + Nama + "'"
                    + ",'" + TglJT + "'"
                    + ",'" + NilaiLumpsum + "'"
                    + ",'" + Tipe + "'"
                    + ",'" + TipeTarif + "'"
                    + ",'" + NilaiPersen + "'"
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
