using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class SkomEdit : System.Web.UI.Page
	{
		protected DataTable rs;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("Nomor");

			if(!Act.Sec("ED:"+Request.PhysicalPath))
			{
				ok.Enabled = false;
				save.Enabled = false;
			}

			if(!Page.IsPostBack)
			{
				FillHeader();
                
			}
            FilltbTermRows();
			FillTable();
			FeedBack();
            ActionHandler();
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Edit Berhasil...";
                if (Request.QueryString["delete"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Hapus Berhasil...";
                
			}
		}

		private void FillHeader()
		{

			btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_SKOM_LOG&Pk="+Nomor.PadLeft(3,'0')+"'";
			btndel.Attributes["onclick"] = "location.href='SkomDel.aspx?Nomor="+Nomor+"'";

			DataTable rsHeader = Db.Rs("SELECT * FROM REF_SKOM WHERE Nomor = " + Nomor);
			if(rsHeader.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				nama.Text = rsHeader.Rows[0]["Nama"].ToString();
                dari.Text = Cf.Day(rsHeader.Rows[0]["PeriodeStart"]);
                sampai.Text = Cf.Day(rsHeader.Rows[0]["PeriodeEnd"]);
                NilaiKomisi.Text = Cf.Num(rsHeader.Rows[0]["NilaiKomisi"]);
				if(rsHeader.Rows[0]["Status"].ToString() == "A")
				{
					aktif.Checked = true;
					inaktif.Checked = false;
				}
				else
				{
					aktif.Checked = false;
					inaktif.Checked = true;
				}
			}
		}

		private void FillTable()
		{
			rs = Db.Rs("SELECT * FROM REF_SKOM_DETAIL WHERE Nomor = '" + Nomor + "' order by tipe asc,baris asc");
            bool New = false;
            int index = 0, baris = Convert.ToInt32(rs.Rows[rs.Rows.Count - 1]["Baris"]) + 1;
			for(int i=0;i<rs.Rows.Count;i++)
			{
                var d = rs.Rows[i];
                if (!Response.IsClientConnected) {
                    break;
                }

                    TableRow r = new TableRow();
                    TableCell c = new TableCell();
                    c.Text = d["Target"].ToString() == "1" ? Db.SingleString("Select Nama from ms_agent_level where Tipe='Internal' and LevelID='"+ d["Tipe"] +"'") : "" ;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = "Target " + d["Target"] ;
                    r.Cells.Add(c);

                    //baris
                    TextBox tb = new TextBox();
                    tb.ID = "Baris_" + index;
                    tb.Width = 150;
                    tb.Text = Cf.Num(d["Baris"]);
                    c = new TableCell();
                    c.Controls.Add(tb);
                    c.Attributes.Add("style","display:none;");
                    r.Cells.Add(c);

                    //No Target
                    tb = new TextBox();
                    tb.ID = "targetNo_" + index;
                    tb.Width = 150;
                    tb.Text = Cf.Num(d["Target"]);
                    c = new TableCell();
                    c.Controls.Add(tb);
                    c.Attributes.Add("style", "display:none;");
                    r.Cells.Add(c);

                    //TIpe
                    tb = new TextBox();
                    tb.ID = "tipeLevel_" + index;
                    tb.Width = 150;
                    tb.Text = d["Tipe"].ToString();
                    c = new TableCell();
                    c.Controls.Add(tb);
                    c.Attributes.Add("style", "display:none;");
                    r.Cells.Add(c);

                    //Nilai Target
                    tb = new TextBox();
                    tb.ID = "Nilai_target_" + index ;
                    tb.Width = 150;
                    tb.Text = Cf.Num(d["NilaiTarget"]);
                    tb.Attributes.Add("style", "text-align:right;");
                    c = new TableCell();
                    c.Controls.Add(tb);
                    r.Cells.Add(c);

                    RadioButtonList rbl = new RadioButtonList();
                    rbl.ID = "rb_tipe_target" + index;
                    rbl.Items.Add(new ListItem("Unit", "U"));
                    rbl.Items.Add(new ListItem("LumSum", "V"));
                    rbl.RepeatDirection = RepeatDirection.Horizontal;
                    rbl.SelectedIndex = d["TipeNilaiTarget"].ToString() == "U" ? 0 : 1;
                    c = new TableCell();
                    c.Controls.Add(rbl);
                    r.Cells.Add(c);

                    tb = new TextBox();
                    tb.ID = "Nilai_komisi_" + index;
                    tb.Width = 150;
                    tb.Text = Cf.Num(d["Nominal"]);
                    tb.Attributes.Add("style", "text-align:right;");
                    c = new TableCell();
                    c.Controls.Add(tb);
                    r.Cells.Add(c);

                    rbl = new RadioButtonList();
                    rbl.ID = "rb_tipe_komisi" + index;
                    rbl.Items.Add(new ListItem("%", "%"));
                    rbl.Items.Add(new ListItem("LumSum", "V"));
                    rbl.RepeatDirection = RepeatDirection.Horizontal;
                    rbl.SelectedIndex = d["TipeNominal"].ToString() == "%" ? 0 : 1;
                    c = new TableCell();
                    c.Controls.Add(rbl);
                    r.Cells.Add(c);

                    tb = new TextBox();
                    tb.ID = "Nilai_overriding_" + index;
                    tb.Width = 150;
                    tb.Text = Cf.Num(d["NilaiOverriding"]);
                    tb.Attributes.Add("style", "text-align:right;");
                    c = new TableCell();
                    c.Controls.Add(tb);
                    r.Cells.Add(c);

                    rbl = new RadioButtonList();
                    rbl.ID = "rb_tipe_overriding_" + index;
                    rbl.Items.Add(new ListItem("%", "%"));
                    rbl.Items.Add(new ListItem("LumSum", "V"));
                    rbl.RepeatDirection = RepeatDirection.Horizontal;
                    rbl.SelectedIndex = d["TipeNilaiOverriding"].ToString().Contains("%") ? 0 : 1;
                    c = new TableCell();
                    c.Controls.Add(rbl);
                    r.Cells.Add(c);

                    tbRumus .Rows.Add(r);
                    index++;
                    //if (i < rs.Rows.Count - 1)
                    //{
                    //    New = Convert.ToInt32(rs.Rows[i]["Target"]) > Convert.ToInt32(rs.Rows[i + 1]["Target"]) ? true : false;
                    //    if (New)
                    //    {
                    //    c = new TableCell();
                    //    c.Text = "<a href='SkomEdit.aspx?Act=del&Tipe=Skema&Nomor=" + Nomor + "&Baris=" + d["Baris"] + "'> delete...</a>";
                    //    r.Cells.Add(c);
                    //    addNewRowTarget(Convert.ToInt32(rs.Rows[i]["Target"]) + 1, index, baris, d["Tipe"]);
                    //    baris++;
                    //    index++;
                    //    }
                    //}
                    //else if (i == rs.Rows.Count - 1)
                    //{
                    //    c = new TableCell();
                    //    c.Text = "<a href='SkomEdit.aspx?Act=del&Tipe=Skema&Nomor=" + Nomor + "&Baris=" + d["Baris"] + "'> delete...</a>";
                    //    r.Cells.Add(c);
                    //    addNewRowTarget(Convert.ToInt32(rs.Rows[i]["Target"]) + 1, index, baris, d["Tipe"]);
                    //    baris++;
                    //    index++;
                    //}                 
			}
                            
		}
        void addNewRowTarget(object Num,int i,int baris,object tipe) {
            TableRow r = new TableRow();
            TableCell c = new TableCell();
            c.Text = "Tambah Target baru";//text-align:right;color:red;
            c.Attributes.Add("style", "text-align:right;color:red;");
            r.Attributes.Add("style", "background-color:#AAFA96;");
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Target " + Num;
            r.Cells.Add(c);

            //baris
            TextBox tb = new TextBox();
            tb.ID = "Baris_" + i;
            tb.Width = 150;
            tb.Text = baris.ToString();
            c = new TableCell();
            c.Controls.Add(tb);
            c.Attributes.Add("style", "display:none;");
            r.Cells.Add(c);

            //No Target
            tb = new TextBox();
            tb.ID = "targetNo_" + i;
            tb.Width = 150;
            tb.Text = Num.ToString();
            c = new TableCell();
            c.Controls.Add(tb);
            c.Attributes.Add("style", "display:none;");
            r.Cells.Add(c);

            //TIpe
            tb = new TextBox();
            tb.ID = "tipeLevel_" + i;
            tb.Width = 150;
            tb.Text = tipe.ToString();
            c = new TableCell();
            c.Controls.Add(tb);
            c.Attributes.Add("style", "display:none;");
            r.Cells.Add(c);

            //Nilai Target
            tb = new TextBox();
            tb.ID = "Nilai_target_" + i;
            tb.Width = 150;
            tb.Attributes.Add("style", "text-align:right;");
            c = new TableCell();
            c.Controls.Add(tb);
            r.Cells.Add(c);

            RadioButtonList rbl = new RadioButtonList();
            rbl.ID = "rb_tipe_target" + i;
            rbl.Items.Add(new ListItem("Unit", "U"));
            rbl.Items.Add(new ListItem("LumSum", "V"));
            rbl.RepeatDirection = RepeatDirection.Horizontal;
            rbl.SelectedIndex =  0 ;
            c = new TableCell();
            c.Controls.Add(rbl);
            r.Cells.Add(c);

            tb = new TextBox();
            tb.ID = "Nilai_komisi_" + i;
            Response.Write((TextBox)tbRumus.FindControl("Nilai_komisi_" + i));
            tb.Width = 150;
            tb.Attributes.Add("style", "text-align:right;");
            c = new TableCell();
            c.Controls.Add(tb);
            r.Cells.Add(c);

            rbl = new RadioButtonList();
            rbl.ID = "rb_tipe_komisi" + i;
            rbl.Items.Add(new ListItem("%", "%"));
            rbl.Items.Add(new ListItem("LumSum", "V"));
            rbl.RepeatDirection = RepeatDirection.Horizontal;
            rbl.SelectedIndex = 0 ;
            c = new TableCell();
            c.Controls.Add(rbl);
            r.Cells.Add(c);

            tb = new TextBox();
            tb.ID = "Nilai_overriding_" + i;
            tb.Width = 150;
            tb.Attributes.Add("style", "text-align:right;");
            c = new TableCell();
            c.Controls.Add(tb);
            r.Cells.Add(c);

            rbl = new RadioButtonList();
            rbl.ID = "rb_tipe_overriding_" + i;
            rbl.Items.Add(new ListItem("%", "%"));
            rbl.Items.Add(new ListItem("LumSum", "V"));
            rbl.RepeatDirection = RepeatDirection.Horizontal;
            rbl.SelectedIndex = 0;
            c = new TableCell();
            c.Controls.Add(rbl);
            r.Cells.Add(c);

            tbRumus.Rows.Add(r);
        }
		private bool valid()
		{
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x1 = Sampai;
                Sampai = Dari;
                Dari = x1;
            }

			bool x = true;
			string s = "";

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
            if (!validTermin()) { x = false; }
                

            int count = Db.SingleInteger("use ISC064_marketingjual;"
                                    + " Select Count(*) from REF_SKOM where Status='A' and Tipe='I' and Nomor!='"+ Nomor +"' and PeriodeStart<='" + Dari + "' and PeriodeEnd>='" + Dari + "'"
                                    + " or ((PeriodeStart >= '" + Dari + "' and periodeEnd >='" + Dari + "') and "
                                    + " (PeriodeStart<='" + Sampai + "' and PeriodeEnd<='" + Sampai + "') and Nomor!='" + Nomor + "' AND Status='A' and Tipe='I' )"
                                     );
            //if (count > 0)
            //{
            //    x = false;
            //    daric.Text = "Tanggal ini berada dirange periode yang telah terdaftar.";
            //}
            count = Db.SingleInteger("use ISC064_marketingjual;"
                                    + " Select Count(*) from REF_SKOM where Status='A' and Tipe='I' and Nomor!='" + Nomor + "' and PeriodeStart<='" + Sampai + "' and PeriodeEnd>='" + Sampai + "'"
                                    + " or ((PeriodeStart >= '" + Dari + "' and periodeEnd >='" + Dari + "') and "
                                    + " (PeriodeStart<='" + Sampai + "' and PeriodeEnd<='" + Sampai + "') and Nomor!='" + Nomor + "' AND Status='A' and Tipe='I' )"
                                     );
            //if (count > 0)
            //{
            //    x = false;
            //    sampaic.Text = "Tanggal ini berada dirange periode yang telah terdaftar.";
            //}
			if(!x)
			{
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Nama tidak boleh kosong.\\n"
					+ "2. Nilai harus berupa angka.\\n"
					+ "3. Jadwal (FIX), Format tanggal : Bulan / Tanggal / Tahun.\\n"
					+ "4. Jadwal (M/D), Interval harus berupa angka.\\n"
					, "document.getElementById('" + s + "').focus();"
					+ "document.getElementById('" + s + "').select();"
					);
			}

			return x;
		}
        bool validTermin()
        {
            DataTable rs = Db.Rs("Select LevelID,Nama from ms_agent_level where tipe='Internal' order by LevelID asc");
            decimal[] PersenTemin = new decimal[rs.Rows.Count];
            //set persen termin = 0 gak null// kalo null gak bisa di +=
            for (int i = 0; i < PersenTemin.Length; i++)
            {
                PersenTemin[i] = 0;
            }
            if (tbTerm.Rows.Count > 4) {
                for (int i = 0; i < tbTerm.Rows.Count - 3; i++)
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
                            if (Cf.isMoney(lv))
                            {
                                PersenTemin[k] += Convert.ToDecimal(lv.Text);
                            }
                        }

                    }
                }
            }
            bool x = true;
            for (int i = 0; i < PersenTemin.Length; i++)
            {
                if (PersenTemin[i] < 100 || PersenTemin[i] > 100)
                {
                    x = false;
                }
            }
            return x;
        }
		private bool Save()
		{
			if(valid())
			{


                EditLoger(Nomor);
                Js.Close(this);
                
				return true;
			}
			else
				return false;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Save()) Js.Close(this);
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(Save()) Response.Redirect("SkomEdit.aspx?Nomor=" + Nomor + "&done=1");
		}

		private void UpdateSkema()
		{
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x1 = Sampai;
                Sampai = Dari;
                Dari = x1;
            }

			string Nama = Cf.Str(nama.Text);
			
			string Status = "";
			if(aktif.Checked)
				Status = "A";
			else if(inaktif.Checked)
				Status = "I";

			Db.Execute("EXEC spSkomEdit"
				+ "  " + Nomor
				+ ",'" + Nama + "'"
                + ",'" + Status + "'"
                + ",'" + Dari + "'"
                + ",'" + Sampai + "'"
				);
            Db.Execute("update ref_skom set NIlaiKomisi='" + NilaiKomisi.Text + "' Where Nomor ='" + Nomor + "'");
		}
		private void UpdateSkemaDetail()
		{
            for (int i = 0; i < tbRumus.Rows.Count - 2; i++)
			{
				if(!Response.IsClientConnected) break;

                    TextBox tbBaris = (TextBox)tbRumus.FindControl("Baris_" + i);
                    TextBox tbLevel = (TextBox)tbRumus.FindControl("tipeLevel_" + i);
                    TextBox tbTargetNo = (TextBox)tbRumus.FindControl("targetNo_" + i);
                    TextBox tbNominal = (TextBox)tbRumus.FindControl("Nilai_komisi_" + i);
                    tbNominal.Text = tbNominal.Text == "" ? "0" : !Cf.isMoney(tbNominal) ? "0" : tbNominal.Text;
                    RadioButtonList rblTipeNominal = (RadioButtonList)tbRumus.FindControl("rb_tipe_komisi" + i);
                    TextBox tbTarget = (TextBox)tbRumus.FindControl("Nilai_target_" + i);
                    tbTarget.Text = tbTarget.Text == "" ? "0" : !Cf.isMoney(tbTarget) ? "0" : tbTarget.Text;
                    RadioButtonList rblTipeTarget = (RadioButtonList)tbRumus.FindControl("rb_tipe_target" + i);
                    TextBox tbOverriding = (TextBox)tbRumus.FindControl("Nilai_overriding_" + i);
                    tbOverriding.Text = tbOverriding.Text == "" ? "0" : !Cf.isMoney(tbOverriding) ? "0" : tbOverriding.Text;
                    RadioButtonList rblTipeOverriding = (RadioButtonList)tbRumus.FindControl("rb_tipe_overriding_" + i);


                    //Response.Write("<br><br><br><br>" + Nomor + "-" + tbBaris.Text + "-" + tbTarget.Text + "-" + rblTipeNominal.SelectedValue + "-");
                    int count = Db.SingleInteger("Select Count(*) from ref_skom_detail where nomor='"+ Nomor +"' and baris='"+ tbBaris.Text +"'");
                    if (count > 0)
                    {
                        Db.Execute("update ref_skom_detail set "
                                + " NilaiTarget='" + tbTarget.Text + "'"
                                + ",TipeNilaiTarget='" + rblTipeTarget.SelectedValue + "'"
                                + ",Nominal='" + tbNominal.Text + "'"
                                + ",TipeNominal='" + rblTipeNominal.SelectedValue + "'"
                                + ",NilaiOverriding='" + tbOverriding.Text + "'"
                                + ",TipeNilaiOverriding='" + rblTipeOverriding.SelectedValue + "'"
                                + " where Nomor='" + Nomor + "' and Baris='" + tbBaris.Text + "'"
                                    );
                        //decimal nmr = Convert.ToDecimal(tbNominal.Text);
                        //decimal cn = Db.SingleDecimal("Select SUM("+nmr+") from ref_skom_detail where nomor = '" + Nomor + "'");
                        //Db.Execute("UPDATE REF_SKOM SET NilaiKomisi = " + cn + " Where Nomor = '" + Nomor + "'");
                    }
                    else {
                        if (Cf.isMoney(tbNominal)) {
                            if (Convert.ToDecimal(tbNominal.Text) > 0) {
                                Db.Execute("EXEC spSkomTambahIntern "
                                            + Nomor
                                            + ",'" + tbLevel.Text + "'"
                                            + ",''"
                                            + ", '" + tbNominal.Text + "'" //Nominal Komisi
                                            + ",'" + rblTipeNominal.SelectedValue + "'" // TIpe nominal Komisi
                                            + ",'" + tbTargetNo.Text + "'" // Nomor Target
                                            + ",'" + tbTarget.Text + "'" //Target Komisi
                                            + ",'" + rblTipeTarget.SelectedValue + "'" //TIpe Target Komisi
                                            + ",'" + tbOverriding.Text + "'" //Overriding
                                            + ",'" + rblTipeOverriding.SelectedValue + "'" // Tipe Overriding
                                            );
                            }
                        }
                        
                    }
                   

				
			}
		}

        void ProsesTerm(int Nomor)
        {
            DataTable rs = Db.Rs("Select levelID,Nama from ms_agent_level where tipe='Internal'");
            for (int i = 0; i < tbTerm.Rows.Count - 3; i++)
            {
               
                TextBox tbNama = new TextBox();
                tbNama = (TextBox)tbTerm.FindControl("tm_" + i);

                //Response.Write(tbNama.Text);

                if (tbNama.Text != "" || tbNama.Text.Length > 2)
                {
                    TextBox Baris = (TextBox)tbTerm.FindControl("tm_baris_" +i);
                    string PersenLv = "";
                    for (int k = 0; k < rs.Rows.Count; k++)
                    {
                        TextBox lv = (TextBox)tbTerm.FindControl("tm_lv" + rs.Rows[k][0] + "_" + i);
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

                    //Response.Write(tbNama.Text + "<br />");

                    int ada = Db.SingleInteger("Select count(*) from ref_skom_term where Nomor='" + Nomor + "' and Baris='" + Convert.ToInt32(Baris.Text) + "'");
                    if (ada > 0)
                    {
                        EditTerm(Nomor, Convert.ToInt32(Baris.Text), tbNama.Text
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
                    else {
                        insertTerm(Nomor, tbNama.Text
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
        }
		private string Nomor
		{
			get
			{
				return Cf.Pk(Request.QueryString["Nomor"]);
			}
        }
        #region Fungsi Update Termin
        void EditTerm(int nomor,int baris, string Nama,
                        string Lv,
                        bool bLns, decimal NLns,
                        bool bBF, decimal NBF,
                        bool bDP, decimal NDP,
                        bool bANG, decimal NANG,
                        bool bPPJB, bool bAkad, bool Mode)
        {

            Db.Execute("EXEC spSkomTermEdit "
                    + " '" + nomor + "'"
                    + " ,'" + baris + "'"
                    + " ,'" + Nama + "'"
                    + " ,'" + Lv + "'"
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
        #endregion
        #region Insert Termin
        void insertTerm(int nomor, string Nama,
                        string PersenLv,
                        bool bLns, decimal NLns,
                        bool bBF, decimal NBF,
                        bool bDP, decimal NDP,
                        bool bANG, decimal NANG,
                        bool bPPJB, bool bAkad, bool Mode)
        {

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
        #endregion
        #region Fill Table tbTerm Header
        void FillTbTermHeader()
        {
            DataTable rs = Db.Rs("Select LevelID,Nama from ms_agent_level where tipe='Internal' order by levelid");

            TableRow r = new TableRow();
            TableHeaderCell hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "<span style='font-size:large;'>Termin Cair</span>";
            hc.ColumnSpan = 10 + rs.Rows.Count;
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

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.RowSpan = 2;
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
            hc.Text = "DP";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "ANG";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "PPJB";
            r.Cells.Add(hc);

            hc = new TableHeaderCell();
            hc.BackColor = Color.LightGray;
            hc.Text = "AKAD";
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
#endregion
        #region Fill Table tbTerm Rows
        protected void FilltbTermRows()
        {
            DataTable rs = Db.Rs("Select LevelID,Nama from ms_agent_level where tipe='Internal' order by Levelid asc");

            FillTbTermHeader();
            DataTable rst = Db.Rs("Select * from [REF_SKOM_TERM] where Nomor='"+ Nomor +"'");
            for (int index = 0; index < rst.Rows.Count; index++)
            {
                var d = rst.Rows[index];

                TableRow r = new TableRow();
                TableCell c = new TableCell();
                TextBox tb = new TextBox();
                CheckBox cb = new CheckBox();


                tb = new TextBox();
                tb.ID = "tm_baris_" + index;
                tb.Text = d["Baris"].ToString();
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(tb);
                c.Attributes.Add("style","display:none;");
                r.Cells.Add(c);

                tb = new TextBox();
                tb.ID = "tm_" + index;
                tb.Text = d["Nama"].ToString();
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(tb);
                r.Cells.Add(c);

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    tb = new TextBox();
                    tb.ID = "tm_lv" + rs.Rows[i][0] + "_" + index;
                    tb.Width = 40;
                    string[] persen = d["PersenLv"].ToString().Split(';');
                    tb.Text = persen[i];
                    c = new TableCell();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.Controls.Add(tb);
                    r.Cells.Add(c);
                }

                cb = new CheckBox();
                cb.ID = "tm_lns_cb_" + index;
                cb.Checked = (bool)d["Lunas"];
                tb = new TextBox();
                tb.ID = "tm_lns_" + index;
                tb.Width = 40;
                tb.Text = Cf.Num(d["NilaiLunas"]);
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                c.Controls.Add(tb);
                r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "tm_bf_cb_" + index;
                cb.Checked = (bool)d["BF"];
                tb = new TextBox();
                tb.ID = "tm_bf_" + index;
                tb.Width = 40;
                tb.Text = Cf.Num(d["NilaiBF"]);
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                c.Controls.Add(tb);
                r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "tm_dp_cb_" + index;
                cb.Checked = (bool)d["DP"];
                tb = new TextBox();
                tb.ID = "tm_dp_" + index;
                tb.Width = 40;
                tb.Text = Cf.Num(d["NilaiDP"]);
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                c.Controls.Add(tb);
                r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "tm_ang_cb_" + index;
                cb.Checked = (bool)d["ANG"];
                tb = new TextBox();
                tb.ID = "tm_ang_" + index;
                tb.Width = 40;
                tb.Text = Cf.Num(d["NilaiANG"]);
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                c.Controls.Add(tb);
                r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "tm_ppjb_cb_" + index;
                cb.Checked = (bool)d["PPJB"];
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "tm_akad_cb_" + index;
                cb.Checked = (bool)d["AKAD"];
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(cb);
                r.Cells.Add(c);

                RadioButton rbMode = new RadioButton();
                rbMode.GroupName = "mode_" + index;
                rbMode.ID = "tm_mode_all_" + index;
                rbMode.Checked = (bool)d["Mode"];
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(rbMode);
                r.Cells.Add(c);

                rbMode = new RadioButton();
                rbMode.GroupName = "mode_" + index;
                rbMode.ID = "tm_mode_satuan_" + index;
                rbMode.Checked = (bool)d["Mode"] == false ? true : false;
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Controls.Add(rbMode);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a href=\"javascript:hapusbarisTerm('" + Nomor + "', '" + d["Baris"] + "')\">Delete...</a> ";
                r.Cells.Add(c);

                tbTerm.Rows.Add(r);
            }
            int last = rst.Rows.Count < 1 ? 0 : Convert.ToInt32(rst.Rows[rst.Rows.Count - 1]["Baris"]);
            addtbTermBaris(rst.Rows.Count,last +1);
        }
        #endregion
        #region Fill Table tbTerm New Row

        void addtbTermBaris(int index, object baris)
        {
            DataTable rs = Db.Rs("Select LevelID from ms_agent_level where tipe='Internal' order by levelid asc");

            TableRow r = new TableRow();
            TableCell c = new TableCell();
            TextBox tb = new TextBox();
            CheckBox cb = new CheckBox();

            tb = new TextBox();
            tb.ID = "tm_baris_" + index;
            tb.Text = baris.ToString();
            c = new TableCell();
            c.HorizontalAlign = HorizontalAlign.Center;
            c.Attributes.Add("style", "display:none;");
            c.Controls.Add(tb);

            r.Cells.Add(c);

            tb = new TextBox();
            tb.ID = "tm_" + index;
            c = new TableCell();
            c.HorizontalAlign = HorizontalAlign.Center;
            c.Controls.Add(tb);
            r.Cells.Add(c);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                tb = new TextBox();
                tb.ID = "tm_lv" + rs.Rows[i][0] + "_" + index;
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
        #endregion

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

        void ActionHandler() {

            string act = "";
            if (Request.QueryString["Act"] != null) {
                act = Request.QueryString["Act"];
                if (act == "del")
                {
                    string Nomor = Request.QueryString["Nomor"] == null ? "" : Request.QueryString["Nomor"];
                    string Baris = Request.QueryString["Baris"] == null ? "" : Request.QueryString["Baris"];
                            
                    switch (Request.QueryString["Tipe"]) { 
                            
                        case "Term":
                            Delete(Nomor,Baris,"TERMIN");
                            break;
                        case "Skema":
                            Delete(Nomor, Baris, "RUMUS");
                            break;
                    }
                    Response.Redirect("SkomEdit.aspx?Nomor=" + Nomor + "&delete=ok");
                            
                }
               
            }
        }

        void Delete(string Nomor,string Baris,string Tipe) {
            DataTable rsHeader = Db.Rs("SELECT "
                + " Nomor"
                + ",Nama"
                + ",Status"
                + " FROM REF_SKOM "
                + " WHERE Nomor = " + Nomor);

            DataTable rsAft = new DataTable();
            DataTable rsBef = new DataTable();

            if (Tipe == "TERMIN") {
                rsBef = Db.Rs("SELECT [Baris] "
                         + "  ,[Nama] "
                         + " ,[PersenLv] "
                         + " ,[Lunas] "
                         + " ,[NilaiLunas] "
                         + " ,[BF] "
                         + " ,[NilaiBF] "
                         + " ,[DP] "
                         + " ,[NilaiDP] "
                         + " ,[ANG] "
                         + " ,[NilaiANG] "
                         + " ,[PPJB] "
                         + " ,[Akad] "
                         + " ,[Mode]"
                         + "   FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_TERM] where Nomor='" + Nomor + "' and Baris='" + Baris + "'"

                    );
                Db.Execute("Delete ref_skom_term where Nomor='" + Nomor + "' and baris='" + Baris + "'");
               
            }
            if (Tipe == "RUMUS") {
                rsBef = Db.Rs("SELECT [Tipe] "
                         + " ,[Baris] "
                         + " ,[Nama] "
                         + " ,[Nominal] "
                         + " ,case when [TipeNominal]='%' then 'Persen' else 'Nominal' end as [Tipe Nominal] "
                         + " FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_DETAIL] where Nomor='" + Nomor + "' and baris='" + Baris + "'");
                Db.Execute("Delete ref_skom_detail where Nomor='" + Nomor + "' and baris='" + Baris + "'");
                
            }
            string Ket = Cf.LogCapture(rsHeader)
                + "<br>---DELETE "+ Tipe +"---<br>"
                + Cf.LogCapture(rsBef);

            Db.Execute("EXEC spLogSkom"
                + " 'DELETE'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + Nomor.PadLeft(3, '0') + "'"
                );
        }
        
        void EditLoger(string Nomor) {
            DataTable rsHeader = Db.Rs("SELECT "
                + " Nomor"
                + ",Nama"
                + ",Status"
                + " FROM REF_SKOM "
                + " WHERE Nomor = " + Nomor);
            
            DataTable rsSkemaBef = Db.Rs("SELECT [Nama] as [Nama Komisi] "
                                  + " ,case when [Status]='A' then 'Aktif' else 'Inaktif' end as [Status] "
                                  + " ,case when [Tipe]='I' then 'Internal' else 'External' end as [Tipe] "
                                  + " ,[PeriodeStart] "
                                  + " ,[PeriodeEnd] "
                                  + " FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM] where Nomor='"+ Nomor +"'");
            UpdateSkema();
            DataTable rsSkemaAft = Db.Rs("SELECT [Nama] as [Nama Komisi] "
                                  + " ,case when [Status]='A' then 'Aktif' else 'Inaktif' end as [Status] "
                                  + " ,case when [Tipe]='I' then 'Internal' else 'External' end as [Tipe] "
                                  + " ,[PeriodeStart] "
                                  + " ,[PeriodeEnd] "
                                  + " FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM] where Nomor='" + Nomor + "'");

            DataTable rsDetailBef = Db.Rs("SELECT [Baris] "
                                  + " ,[Tipe] "
                                  + " ,[Nama] "
                                  + " ,[Nominal] "
                                  + " ,case when [TipeNominal]='%' then 'Persen' else 'Nominal' end as [Tipe Nominal] "
                                  + "  FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_DETAIL] where Nomor='"+ Nomor +"'");
            UpdateSkemaDetail();
            DataTable rsDetailAft = Db.Rs("SELECT [Baris] "
                                  + " ,[Tipe] "
                                  + " ,[Nama] "
                                  + " ,[Nominal] "
                                  + " ,case when [TipeNominal]='%' then 'Persen' else 'Nominal' end as [Tipe Nominal] "
                                  + "  FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_DETAIL] where Nomor='" + Nomor + "'");
            DataTable rsTerminBef = Db.Rs("SELECT [Baris] "
                                  + " ,[Nama] "
                                  + " ,[PersenLv] "
                                  + " ,[Lunas] "
                                  + " ,[NilaiLunas] "
                                  + " ,[BF] "
                                  + " ,[NilaiBF] "
                                  + " ,[DP] "
                                  + " ,[NilaiDP] "
                                  + " ,[ANG] "
                                  + " ,[NilaiANG] "
                                  + " ,[PPJB] "
                                  + " ,[Akad] "
                                  + " ,[Mode] "
                                  + "  FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_TERM] where Nomor='"+ Nomor +"'");

            ProsesTerm(Convert.ToInt32(Nomor));
            DataTable rsTerminAft = Db.Rs("SELECT [Baris] "
                                 + " ,[Nama] "
                                 + " ,[PersenLv] "
                                 + " ,[Lunas] "
                                 + " ,[NilaiLunas] "
                                 + " ,[BF] "
                                 + " ,[NilaiBF] "
                                 + " ,[DP] "
                                 + " ,[NilaiDP] "
                                 + " ,[ANG] "
                                 + " ,[NilaiANG] "
                                 + " ,[PPJB] "
                                 + " ,[Akad] "
                                 + " ,[Mode] "
                                 + "  FROM [ISC064_MARKETINGJUAL].[dbo].[REF_SKOM_TERM] where Nomor='" + Nomor + "'");
            
            string Ket = Cf.LogCapture(rsHeader)
                + "<br>---EDIT SKEMA---<br>"
                + Cf.LogCompare(rsSkemaBef, rsSkemaAft)
                + "<br>---EDIT RUMUS---<br>"
                + Cf.LogCompare(rsDetailBef, rsDetailAft)
                + "<br>---EDIT TERMIN---<br>"
                + Cf.LogCompare(rsTerminBef, rsTerminAft);
            
            Db.Execute("EXEC spLogSkom"
                + " 'EDIT'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + Nomor.PadLeft(3, '0') + "'"
                );
        }
        
}
}
