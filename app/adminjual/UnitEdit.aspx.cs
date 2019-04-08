using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.AspNet.SignalR;

namespace ISC064.ADMINJUAL
{
	public partial class UnitEdit : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Act.Sec("ED:"+Request.PhysicalPath))
			{
				ok.Enabled = false;
				save.Enabled = false;
			}

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Fill();
                Bind(); //tanggal dan bulan
                string strSql = "SELECT * FROM MS_UNIT WHERE NoStock = '" + NoStock + "'";

                DataTable rs = Db.Rs(strSql);
                jenis.SelectedValue = rs.Rows[0]["Jenis"].ToString();
                tipe.SelectedValue = rs.Rows[0]["JenisProperti"].ToString();
            }

			FeedBack();
			Js.Confirm(this, "Lanjutkan proses edit data unit properti?\\n"
				+ "PRICE LIST yang sudah di-set harus di-set ulang.");
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Edit Berhasil...";
			}
		}

        private void Bind()
        {
            //NumberFormat.js
            Js.NumberFormat(luas);

			DataTable rs;
			string strSql;

            strSql = "SELECT * FROM REF_JENIS WHERE Project = '" + project.SelectedValue + "' ORDER BY SN";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Jenis"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                ListItem li = new ListItem();
                li.Text = t;
                li.Value = v;
                li.Attributes.Add("class", "radio");
                jenis.Items.Add(li);                
            }

            strSql = "SELECT * FROM REF_LOKASI WHERE Project = '" + project.SelectedValue + "' ORDER BY SN";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Lokasi"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                lokasi.Items.Add(new ListItem(" " + t, v));
            }

            //strSql = "SELECT * FROM REF_JENISPROPERTI WHERE Project = '" + project.SelectedValue + "' ORDER BY SN";
            //rs = Db.Rs(strSql);
            //for (int i = 0; i < rs.Rows.Count; i++)
            //{
            //    string v = rs.Rows[i]["JenisProperti"].ToString();
            //    string t = v + " - " + rs.Rows[i]["Nama"].ToString();
            //    ListItem li = new ListItem();
            //    li.Text = t;
            //    li.Value = v;
            //    li.Attributes.Add("class", "radio");
            //    tipe.Items.Add(li);                
            //}

            var hub = GlobalHost.ConnectionManager.GetHubContext<UnitHub>();
            hub.Clients.All.broadcastStatus(NoStock);
        }
		
		private void Fill()
		{
			aKey.HRef = "javascript:openModal('UnitEditKey.aspx?NoStock=" + NoStock + "','350','150')";
			aStatus.HRef = "javascript:openModal('UnitStatus.aspx?NoStock=" + NoStock + "','350','220')";
			btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_UNIT_LOG&Pk="+NoStock+"'";
			btndel.Attributes["onclick"] = "location.href='UnitDel.aspx?NoStock="+NoStock+"'";
		
			string strSql = "SELECT * FROM MS_UNIT WHERE NoStock = '" + NoStock + "'";
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");	
			else
			{
				nostock.Text = rs.Rows[0]["NoStock"].ToString();

                string stat = rs.Rows[0]["Status"].ToString();
                if (stat == "A")
                {
                    status.ForeColor = Color.Green;
                    status.Text = "Available";
                }
                else if (stat == "B")
                {
                    status.ForeColor = Color.Red;
                    status.Text = "Sold";
                }
                else if (stat == "H")
                {
                    status.ForeColor = Color.Orange;
                    status.Text = "Hold Internal";
                }

                //lokasi.Text = rs.Rows[0]["Lokasi"].ToString();
                lokasi.Items.Add(new ListItem(
                    "Tidak Berubah : " + rs.Rows[0]["Lokasi"]
                    , rs.Rows[0]["Lokasi"].ToString()));
                lokasi.SelectedValue = rs.Rows[0]["Lokasi"].ToString();
                kategori.SelectedValue = rs.Rows[0]["Kategori"].ToString();
                nomor.Text = rs.Rows[0]["Nomor"].ToString();
                lantai.Text = rs.Rows[0]["Lantai"].ToString();
                luas.Text = Cf.Num(rs.Rows[0]["Luas"]);
                luassg.Text = Cf.Num(rs.Rows[0]["LuasSG"]);
                luasnett.Text = Cf.Num(rs.Rows[0]["LuasNett"]);
                luaslbh.Text = Cf.Num(rs.Rows[0]["LuasLebih"]);

                plmin.Text = Cf.Num(rs.Rows[0]["PriceListMin"]);
                if (Convert.ToInt16(rs.Rows[0]["DefaultPL"]) < 2)
                {
                    pl.Text = Cf.Num(rs.Rows[0]["PriceList"]);
                }
                else
                {
                    pl.Text = Cf.Num(rs.Rows[0]["PricelistKavling"]);

                }
                plrumah.Text = Cf.Num(rs.Rows[0]["PriceList"]);
                plkav.Text = Cf.Num(rs.Rows[0]["PricelistKavling"]);
                bphtb.Text = Cf.Num(rs.Rows[0]["BiayaBPHTB"]);
                bsurat.Text = Cf.Num(rs.Rows[0]["BiayaSurat"]);
                bproses.Text = Cf.Num(rs.Rows[0]["BiayaProses"]);
                blain.Text = Cf.Num(rs.Rows[0]["BiayaLainLain"]);
                htanah.Text = Cf.Num(rs.Rows[0]["HargaTanah"]);

                //tanggal input, edit dan follow-up
                tglInput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
                tglEdit.Text = Cf.Date(rs.Rows[0]["TglEdit"]);

				//jenis.Items.Add(new ListItem(rs.Rows[0]["Jenis"].ToString()));
                jenis.CssClass = "igroup-radio";
                //jenis.SelectedValue = rs.Rows[0]["Jenis"].ToString();

                //tipe.Items.Add(new ListItem(rs.Rows[0]["JenisProperti"].ToString()));
                tipe.CssClass = "igroup-radio";
                tipe.SelectedValue = rs.Rows[0]["JenisProperti"].ToString();
                ppn.SelectedValue = rs.Rows[0]["SifatPPN"].ToString();
                project.Items.Add(new ListItem("Sekarang : " + rs.Rows[0]["Project"].ToString(), rs.Rows[0]["Project"].ToString()));
                project.SelectedValue = rs.Rows[0]["Project"].ToString();
            }
        }

		private bool valid()
		{
			string s = "";
			bool x = true;

            x = Cf.ValidMandatory(this, "Unit",project.SelectedValue);

            if (Cf.isEmpty(nomor))
            {
                x = false;
                if (s == "") s = nomor.ID;
                nomorc.Text = "Kosong";
            }
            else
                nomorc.Text = "";

            if (Cf.isEmpty(lantai))
            {
                x = false;
                if (s == "") s = lantai.ID;
                lantaic.Text = "Kosong";
            }
            else
                lantaic.Text = "";

            if (jenis.SelectedValue == "" || tipe.SelectedValue == "") x = false;

            if (!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Unit Properti tidak boleh kosong.\\n"
					+ "2. Luas dan Luas Semi Gross harus berupa angka.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool Save()
		{
			if(valid())
			{
				string Jenis = jenis.SelectedValue;
                string Lokasi = lokasi.SelectedValue;// Cf.Str(lokasi.Text);
                string Nomor = Cf.Str(nomor.Text);
                string Lantai = Cf.Str(lantai.Text);
                decimal Luas = Convert.ToDecimal(luas.Text);
                decimal LuasSG = Convert.ToDecimal(luassg.Text);
                decimal LuasNett = Convert.ToDecimal(luasnett.Text);
                decimal LuasLebih = Convert.ToDecimal(luaslbh.Text);

                string ParamID = "FormatLantai" + project.SelectedValue;
                string ParamID2 = "FormatUnit" + project.SelectedValue;

                string strSql = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
                string strSql2 = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID2 + "'");


                string NoUnit = Lokasi + strSql + Lantai + strSql2 + Nomor;

                string flag1 = Db.SingleTime(
					"SELECT TglEdit FROM MS_UNIT WHERE NoStock = '" + NoStock + "'").ToString();

				DataTable rsBef = Db.Rs("SELECT "
					+ " NoStock AS [No. Stock]"
					+ ",Jenis"
                    + ",Project"
                    + ",Lokasi AS [Lokasi]"
                    + ",NoUnit AS [Unit]"
                    + ",Kategori AS [Kategori Unit]"
                    + ",Lantai"
                    + ",Nomor"
                    + ",Luas AS [Luas]"
                    + ",LuasLebih AS [Luas Lebih Tanah]"
                    + ",LuasSG AS [Luas Tanah]"
                    + ",LuasNett AS [Luas Bangunan]"
                    + " FROM MS_UNIT"
					+ " WHERE NoStock = '" + NoStock + "'"
					);

                Db.Execute("EXEC spUnitEdit"
                    + " '" + NoStock + "'"
                    + ",'" + Jenis + "'"
                    + ",'" + Lokasi + "'"
                    + ",'" + NoUnit + "'"
                    + ", " + LuasSG
                    + ",'" + Lantai + "'"
                    + ",'" + Nomor + "'"
                    );

                string Project = project.SelectedValue;
                Db.Execute("UPDATE MS_UNIT SET JenisProperti='" + tipe.SelectedValue + "'"
                    + ", LuasSG =" + LuasSG
                    + ", LuasNett =" + LuasNett
                    + ", LuasLebih =" + LuasLebih
                    + ", Project = '" + Project + "'"
                    + " WHERE NoStock='" + NoStock + "'");

                DataTable kon = Db.Rs("SELECT * FROM MS_KONTRAK WHERE NoStock = '" + NoStock + "' AND Status = 'A'");
                if (kon.Rows.Count > 0)
                {
                    string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                    string Pers = Db.SingleString("SELECT Pers FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
                    string NamaPers = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PERS WHERE Pers = '" + Pers + "'");
                    Db.Execute("UPDATE MS_KONTRAK SET Revisi = Revisi + 1, Luas=" + LuasSG
                        + ", Project = '" + Project + "', NamaProject='" + NamaProject + "',Pers='" + Pers + "',NamaPers = '" + NamaPers + "'"
                        + " WHERE NoKontrak = '" + kon.Rows[0]["NoKontrak"].ToString() + "'"
                        );
                }

				string flag2 = flag1;
				DataTable rsflag = Db.Rs("SELECT TglEdit FROM MS_UNIT WHERE NoStock = '"+NoStock+"'");
				if(rsflag.Rows.Count!=0)
					flag2 = rsflag.Rows[0][0].ToString();

                if (flag1 == flag2)
                {
                    //nounitc.Text = "Duplikat";
                    Js.Alert(
                        this
                        , "Unit Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Nomor Unit sudah dipakai.\\n"
                        , "document.getElementById('nounit').focus();"
                        + "document.getElementById('nounit').select();"
                        );

                    return false;
                }
                else
                {
                    DataTable rsAft = Db.Rs("SELECT "
                        + " NoStock AS [No. Stock]"
                        + ",Jenis"
                        + ",Project"
                        + ",Lokasi AS [Lokasi]"
                        + ",NoUnit AS [Unit]"
                        + ",Kategori AS [Kategori Unit]"
                        + ",Lantai"
                        + ",Nomor"
                        + ",Luas AS [Luas]"
                        + ",LuasLebih AS [Luas Lebih Tanah]"
                        + ",LuasSG AS [Luas Tanah]"
                        + ",LuasNett AS [Luas Bangunan]"
                        + " FROM MS_UNIT"
						+ " WHERE NoStock = '" + NoStock + "'"
						);

					//Logfile
					string Ket = Cf.LogCompare(rsBef,rsAft);

                    Db.Execute("EXEC spLogUnit"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoStock + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_UNIT_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_UNIT_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

					return true;
				}
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
			if(Save()) Response.Redirect("UnitEdit.aspx?done=1&NoStock="+NoStock);
		}

		private string NoStock
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoStock"]);
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            lokasi.Items.Clear();
            tipe.Items.Clear();
            jenis.Items.Clear();
            Bind();            
            jenis.SelectedIndex = 0;
        }
    }
}
