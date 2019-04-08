using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class Skom : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Js.Focus(this, nama);
				FillTable();
			}

            loadTbRumus();
            tmbh();
			FeedBack();
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "<a href=\"javascript:popSkom('"+Request.QueryString["done"]+"')\">"
						+ "Pendaftaran Berhasil..."
						+ "</a>";
			}
		}

		private void FillTable()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			
			//Isi skema aktif
			DataTable rs = Db.Rs("SELECT * FROM REF_SKOM WHERE Status = 'A'");
			Rpt.NoData(sb, rs, "<font style='font:8pt'>Tidak terdapat skema komisi dengan status aktif.</font>");
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				sb.Append("<li>"
					+ "<a href=\"javascript:popSkom2('" + rs.Rows[i]["Nomor"].ToString() + "')\">"
					+ rs.Rows[i]["Nama"] + " ("+rs.Rows[i]["Nomor"].ToString().PadLeft(3,'0')+")"
					+ "</a>"
					+ "</li>"
					);
			}

			aktif.InnerHtml = sb.ToString();
			
			//Isi skema inaktif
			sb = new System.Text.StringBuilder();

			rs = Db.Rs("SELECT * FROM REF_SKOM WHERE Status = 'I'");
			Rpt.NoData(sb, rs, "<font style='font:8pt'>Tidak terdapat skema komisi dengan status inaktif.</font>");
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				if(!Response.IsClientConnected) break;

				sb.Append("<li>"
					+ "<a href=\"javascript:popSkom2('" + rs.Rows[i]["Nomor"].ToString() + "')\">"
					+ rs.Rows[i]["Nama"].ToString() + " (" + rs.Rows[i]["Nomor"].ToString().PadLeft(3,'0') + ")"
					+ "</a>"
					+ "</li>"
					);
			}

			inaktif.InnerHtml = sb.ToString();
		}

		private bool valid()
		{
            
			string s = "";
			bool x = true;

            if (!Cf.isTgl(dari))
            {
                x = false;
                if (s == "") s = dari.ID;
                daric.Text = "Tanggal";
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                x = false;
                if (s == "") s = sampai.ID;
                sampaic.Text = "Tanggal";
            }
            else
                sampaic.Text = "";

			//nama
			if(Cf.isEmpty(nama))
			{
				x = false;
				if(s=="") s = nama.ID;
				namac.Text = "Kosong";
			}
			else
				namac.Text = "";
            if (!Cf.isMoney(NilaiKomisi)) {
                x = false;
                s = NilaiKomisi.ID;
            }
            if (Cf.isTgl(dari) && Cf.isTgl(sampai)) {
                DateTime Dari = Convert.ToDateTime(dari.Text);
                DateTime Sampai = Convert.ToDateTime(sampai.Text);
                if (Dari > Sampai)
                {
                    DateTime x1 = Sampai;
                    Sampai = Dari;
                    Dari = x1;
                }


                int count = Db.SingleInteger("use ISC064_marketingjual;"
                                        + " Select Count(*) from REF_SKOM where Status='A' and Tipe='I' and PeriodeStart<='" + Dari + "' and PeriodeEnd>='" + Dari + "'"
                                        + " or ((PeriodeStart >= '" + Dari + "' and periodeEnd >='" + Dari + "') and "
                                        + " (PeriodeStart<='" + Sampai + "' and PeriodeEnd<='" + Sampai + "') and Status='A' and Tipe='I')"
                                         );
                //if (count > 0)
                //{
                //    x = false;
                //    daric.Text = "Tanggal ini berada dirange periode yang telah terdaftar.";
                //}
                count = Db.SingleInteger("use ISC064_marketingjual;"
                                        + " Select Count(*) from REF_SKOM where Status='A' and Tipe='I' and PeriodeStart<='" + Sampai + "' and PeriodeEnd>='" + Sampai + "'"
                                        + " or ((PeriodeStart >= '" + Dari + "' and periodeEnd >='" + Dari + "') and "
                                        + " (PeriodeStart<='" + Sampai + "' and PeriodeEnd<='" + Sampai + "') and Status='A' and Tipe='I') "
                                         );
                //if (count > 0)
                //{
                //    x = false;
                //    sampaic.Text = "Tanggal ini berada dirange periode yang telah terdaftar.";
                //}
            }

            // validasi termin
            if (!validTermin()) {
                x = false;
                s = "tm_lv1_0"; 
                this.RegisterStartupScript(
                     "focusScript"
                     , "<script language='javascript'>"
                     + " alert('Persen Termin tidak 100%');"
                     + "</script>"
                     );
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
            else {
            }

			return x;
		}
        bool validTermin() {
            DataTable rs = Db.Rs("Select LevelID,Nama from ms_agent_level where tipe='Internal' order by LevelID asc");
            decimal[] PersenTemin = new decimal[rs.Rows.Count];
            //set persen termin = 0 gak null// kalo null gak bisa di +=
            for (int i = 0; i < PersenTemin.Length; i++) {
                PersenTemin[i] = 0;
            }
                for (int i = 0; i < 5; i++)
                {
                    TextBox tbNama = new TextBox();
                    tbNama = (TextBox)tbTerm.FindControl("tm_" + i);

                    if (tbNama.Text != "" || tbNama.Text.Length > 2)
                    {

                        string PersenLv = "";
                        for (int k = 0; k < rs.Rows.Count; k++)
                        {
                            TextBox lv = (TextBox)tbTerm.FindControl("tm_lv" + rs.Rows[i][0] + "_" + i);
                            lv.Text = !Cf.isMoney(lv) ? "0" : lv.Text;
                            PersenLv += lv.Text + ";";
                            if (Cf.isMoney(lv)) {
                                PersenTemin[k] += Convert.ToDecimal(lv.Text);
                            }
                        }

                    }
                }
                bool x = true;
                for (int i = 0; i < PersenTemin.Length; i++) {
                    if (PersenTemin[i] < 100 || PersenTemin[i] > 100) {
                        x = false;
                    }
                }
                    return x;
        }
		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
                DateTime Dari = Convert.ToDateTime(dari.Text);
                DateTime Sampai = Convert.ToDateTime(sampai.Text);
                if (Dari > Sampai)
                {
                    DateTime x = Sampai;
                    Sampai = Dari;
                    Dari = x;
                }
				string Nama = Cf.Str(nama.Text);

				Db.Execute("EXEC spSkomBaru"
					+ " '" + Nama + "'"
                    + ",'I'"
                    + ",'" + Dari + "'"
                    + ",'" + Sampai + "'"
					);

				int Nomor = Db.SingleInteger("SELECT TOP 1 Nomor FROM REF_SKOM"
					+ " ORDER BY Nomor DESC"
					);
                Db.Execute("update ref_skom set NilaiKomisi='"+ NilaiKomisi.Text +"' Where Nomor ='"+Nomor+"'");
                SaveRumus(Nomor);
                ProsesTerm(Nomor);
				
				Response.Redirect("Skom.aspx?done=" + Nomor);
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
        protected void tmbh()
        {
            DataTable rs = Db.Rs("Select LevelID,Nama from ms_agent_level where tipe='Internal' order by LevelID");

            FillTbTermHeader();
            //Response.Write(tbTerm.Rows.Count);
            for (int index = 0; index < 5;index++ )
            {
                TableRow r = new TableRow();
                TableCell c = new TableCell();
                TextBox tb = new TextBox();
                CheckBox cb = new CheckBox();


                tb.ID = "tm_" + index;
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(tb);
                r.Cells.Add(c);

                for (int i = 0; i < rs.Rows.Count; i++) {
                    tb = new TextBox();
                    tb.ID = "tm_lv"+ rs.Rows[i][0] +"_" + index;
                    tb.Width = 40;
                    c = new TableCell();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.Controls.Add(tb);
                    r.Cells.Add(c);
                }

                cb = new CheckBox();
                cb.ID = "tm_lns_cb_" + index;
                tb = new TextBox();
                tb.ID = "tm_lns_" + index;
                tb.Width = 40;
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                c.Controls.Add(tb);
                r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "tm_bf_cb_" + index;
                tb = new TextBox();
                tb.ID = "tm_bf_" + index;
                tb.Width = 40;
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                c.Controls.Add(tb);
                r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "tm_dp_cb_" + index;
                tb = new TextBox();
                tb.ID = "tm_dp_" + index;
                tb.Width = 40;
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                c.Controls.Add(tb);
                r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "tm_ang_cb_" + index;
                tb = new TextBox();
                tb.ID = "tm_ang_" + index;
                tb.Width = 40;
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                c.Controls.Add(tb);
                r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "tm_ppjb_cb_" + index;
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "tm_akad_cb_" + index;
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                r.Cells.Add(c);

                RadioButton rbMode = new RadioButton();
                rbMode.GroupName = "mode_" + index;
                rbMode.ID = "tm_mode_all_" + index;
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(rbMode);
                r.Cells.Add(c);

                rbMode = new RadioButton();
                rbMode.GroupName = "mode_" + index;
                rbMode.ID = "tm_mode_satuan_" + index;
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(rbMode);
                r.Cells.Add(c);

                tbTerm.Rows.Add(r);
            }
        }
        void ProsesTerm(int Nomor) {
            DataTable rs = Db.Rs("Select LevelID,Nama from ms_agent_level where tipe='Internal' order by LevelID asc");
            for (int i = 0; i < 5; i++)
            {
                TextBox tbNama = new TextBox();
                tbNama = (TextBox)tbTerm.FindControl("tm_" + i);

                //Response.Write(tb.Text);

                if (tbNama.Text != "" || tbNama.Text.Length > 2)
                {

                    string PersenLv = "";
                    for (int k = 0; k < rs.Rows.Count; k++) {
                        TextBox lv = (TextBox)tbTerm.FindControl("tm_lv" + rs.Rows[i][0] + "_" + i);
                        lv.Text = !Cf.isMoney(lv) ? "0" : lv.Text;
                        PersenLv += lv.Text + ";";
                    }


                    CheckBox cbLns = (CheckBox)tbTerm.FindControl("tm_lns_cb_" + i);
                    TextBox lns = (TextBox)tbTerm.FindControl("tm_lns_" + i);
                    lns.Text = !Cf.isMoney(lns) ? "0" : lns.Text;

                    CheckBox cbBF = (CheckBox)tbTerm.FindControl("tm_bf_cb_" + i);
                    TextBox BF = (TextBox)tbTerm.FindControl("tm_bf_" + i);
                    BF.Text = !Cf.isMoney(BF) ? "0" : BF.Text;

                    CheckBox cbDP = (CheckBox)tbTerm.FindControl("tm_dp_cb_" + i);
                    TextBox DP = (TextBox)tbTerm.FindControl("tm_dp_" + i);
                    DP.Text = !Cf.isMoney(DP) ? "0" : DP.Text;

                    CheckBox cbANG = (CheckBox)tbTerm.FindControl("tm_ang_cb_" + i);
                    TextBox ANG = (TextBox)tbTerm.FindControl("tm_ang_" + i);
                    ANG.Text = !Cf.isMoney(ANG) ? "0" : ANG.Text;


                    CheckBox cbPPJB = (CheckBox)tbTerm.FindControl("tm_ppjb_cb_" + i);
                    CheckBox cbAkad = (CheckBox)tbTerm.FindControl("tm_akad_cb_" + i);
                    RadioButton rbMode = (RadioButton)tbNama.FindControl("tm_mode_all_" + i);

                    insertTerm(Nomor,tbNama.Text
                              , PersenLv
                              , cbLns.Checked
                              , Convert.ToDecimal(lns.Text)
                              , cbBF.Checked
                              , Convert.ToDecimal(BF.Text)
                              , cbDP.Checked
                              , Convert.ToDecimal(DP.Text)
                              , cbANG.Checked
                              , Convert.ToDecimal(ANG.Text)
                              , cbPPJB.Checked
                              , cbAkad.Checked
                              , rbMode.Checked
                               );
                }
            }
        }

        void insertTerm(int nomor,string Nama,
                        string PersenLv,
                        bool bLns,decimal NLns,
                        bool bBF,decimal NBF,
                        bool bDP,decimal NDP, 
                        bool bANG,decimal NANG,
                        bool bPPJB,bool bAkad,bool Mode) {

                            Db.Execute("EXEC spSkomTermBaru "
                                    + " '" + nomor + "'"
                                    + " ,'" + Nama + "'"
                                    + " ,'" + PersenLv + "'"
                                    + " ,'" + bLns + "'"
                                    + " ,'" + NLns + "'"
                                    + " ,'" + bBF + "'"
                                    + " ,'" + NBF + "'"
                                    + " ,'" + bDP + "'"
                                    + " ,'" + NDP + "'"
                                    + " ,'" + bANG + "'"
                                    + " ,'" + NANG + "'"
                                    + " ,'" + bPPJB + "'"
                                    + " ,'" + bAkad + "'"
                                    + " ,'" + Mode + "'"
                                        );
        }

        void loadTbRumus() {
            DataTable rs = Db.Rs("Select LevelID,Nama from ms_agent_level where tipe='Internal' order by levelID asc");

            for (int i = 0; i < rs.Rows.Count; i++) {
                var d = rs.Rows[i];
                for (int j = 0; j < 1; j++) {
                    TableRow r = new TableRow();
                    TableCell c = new TableCell();
                    c.Text = j == 0 ? d[1].ToString() : "";
                    r.Cells.Add(c);

                    //c = new TableCell();
                    //c.Text = "Target " + (j + 1);
                    //r.Cells.Add(c);

                    //Nilai Target
                    TextBox tb = new TextBox();
                    tb.ID = "Nilai_target_" + d[0].ToString() + "_" + j;
                    tb.Width = 150;
                    tb.Text = "1";
                    c = new TableCell();
                    c.Controls.Add(tb);
                    r.Cells.Add(c);

                    RadioButtonList rbl = new RadioButtonList();
                    rbl.ID = "rb_tipe_target" + d[0].ToString() + "_" + j;
                    rbl.Items.Add(new ListItem("Unit", "U"));
                    rbl.Items.Add(new ListItem("LumSum", "V"));
                    rbl.RepeatDirection = RepeatDirection.Horizontal;
                    rbl.SelectedIndex = 0;
                    c = new TableCell();
                    c.Controls.Add(rbl);
                    r.Cells.Add(c);

                    tb = new TextBox();
                    tb.ID = "Nilai_komisi_" + d[0].ToString() + "_" + j;
                    tb.Width = 150;
                    tb.Text = "0";
                    tb.Attributes.Add("style", "text-align:right;");
                    c = new TableCell();
                    c.Controls.Add(tb);
                    r.Cells.Add(c);

                    rbl = new RadioButtonList();
                    rbl.ID = "rb_tipe_komisi" + d[0].ToString() + "_" + j;
                    rbl.Items.Add(new ListItem("%", "%"));
                    rbl.Items.Add(new ListItem("LumSum", "V"));
                    rbl.RepeatDirection = RepeatDirection.Horizontal;
                    rbl.SelectedIndex = 0;
                    c = new TableCell();
                    c.Controls.Add(rbl);
                    r.Cells.Add(c);

                    tb = new TextBox();
                    tb.ID = "Nilai_overriding_" + d[0].ToString() + "_" + j;
                    tb.Width = 150;
                    tb.Text = "0";
                    tb.Attributes.Add("style", "text-align:right;");
                    c = new TableCell();
                    c.Controls.Add(tb);
                    r.Cells.Add(c);

                    rbl = new RadioButtonList();
                    rbl.ID = "rb_tipe_overriding_" + d[0].ToString() + "_" + j;
                    rbl.Items.Add(new ListItem("%", "%"));
                    rbl.Items.Add(new ListItem("LumSum", "V"));
                    rbl.RepeatDirection = RepeatDirection.Horizontal;
                    rbl.SelectedIndex = 1;
                    c = new TableCell();
                    c.Controls.Add(rbl);
                    r.Cells.Add(c);


                    tbRumus.Rows.Add(r);
                }

            }
        }

        void SaveRumus(int Nomor) {
            DataTable rs = Db.Rs("Select LevelID,Nama from ms_agent_level where tipe='Internal' order by LevelID asc");
            
            for (int i = 0; i < rs.Rows.Count; i++) {
                
                var d = rs.Rows[i];
                for (int j = 0; j < 1; j++) {
                    TextBox tbNominal = (TextBox)tbRumus.FindControl("Nilai_komisi_" + d[0].ToString() + "_" + j);
                    RadioButtonList rblTipeNomilal = (RadioButtonList)tbRumus.FindControl("rb_tipe_komisi" + d[0].ToString() + "_" + j);
                    TextBox tbTarget = (TextBox)tbRumus.FindControl("Nilai_target_" + d[0].ToString() + "_" + j);
                    RadioButtonList rblTipeTarget = (RadioButtonList)tbRumus.FindControl("rb_tipe_target" + d[0].ToString() + "_" + j);
                    TextBox tbOverriding = (TextBox)tbRumus.FindControl("Nilai_overriding_" + d[0].ToString() + "_" + j);
                    RadioButtonList rblTipeOverriding = (RadioButtonList)tbRumus.FindControl("rb_tipe_overriding_" + d[0].ToString() + "_" + j);
                    TableCell Namas = (TableCell)tbRumus.FindControl("Namas_" + d[1].ToString() + "_" + i);
                    string Namas1 = d[1].ToString();

                    if (Cf.isMoney(tbTarget))
                    {
                        //Response.Write(Nomor +"<br/>"+ d[0] + "<br/>" + JenisSkema.Text +"<br/>"+ tbNominal.Text);
                        if (Convert.ToDecimal(tbTarget.Text) > 0)
                        {
                            Db.Execute("EXEC spSkomTambahIntern "
                                    + Nomor
                                    + ",'" + d[0] + "'"
                                    + ",'" + Cf.Str(Namas1) + "'"
                                    + ", '" + tbNominal.Text + "'" //Nominal Komisi
                                    + ",'" + rblTipeNomilal.SelectedValue + "'" // TIpe nominal Komisi
                                    + ",'" + (j + 1) + "'" // Nomor Target
                                    + ",'" + tbTarget.Text + "'" //Target Komisi
                                    + ",'" + rblTipeTarget.SelectedValue + "'" //TIpe Target Komisi
                                    + ",'" + tbOverriding.Text + "'" //Overriding
                                    + ",'" + rblTipeOverriding.SelectedValue + "'" // Tipe Overriding
                                    );

                        }

                    }
                    else
                    {

                    }
                }
            }
        }
     
        void FillTbTermHeader() {
            DataTable rs = Db.Rs("Select LevelID,Nama from ms_agent_level where tipe='Internal' order by LevelID asc");

            TableRow r = new TableRow();
            TableHeaderCell hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "<span style='font-size:large;'>Termin Cair</span>";
            hc.ColumnSpan = 9 + rs.Rows.Count;
            r.Cells.Add(hc);

            tbTerm.Rows.Add(r);

            r = new TableRow();
            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "Nama";
            hc.RowSpan = 2;
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "Persen Cair";
            hc.ColumnSpan = rs.Rows.Count;
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "Syarat Cair";
            hc.ColumnSpan = 6;
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "Tipe Syarat Cair";
            hc.ColumnSpan = 2;
            r.Cells.Add(hc);

            tbTerm.Rows.Add(r);

            r = new TableRow();
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                hc = new TableHeaderCell();
                hc.BackColor = Color.LightGray;
                hc.Text =  rs.Rows[i][1].ToString() + " (%)";
                r.Cells.Add(hc);
            }

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "Lunas (%)";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "BF";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "DP (%)";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "ANG (%)";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "PPJB (%)";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "AKAD (%)";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "Seluruh";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "Salah Satu";
            r.Cells.Add(hc);

            tbTerm.Rows.Add(r);
        }
    }
}
