using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class UnitDaftar : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton cancel;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, lokasi);
                Act.ProjectList(project);
                InitForm();
                Fill();
            }

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        //+ "<a show-modal='#ModalPopUp' modal-title='Edit Unit' modal-url='UnitEdit.aspx?NoStock=" + Request.QueryString["done"] + "'>"
                        + "<a href=\"javascript:popEditUnit('" + Request.QueryString["done"] + "')\"'>"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
                    project.SelectedValue = Request.QueryString["project"];
                    jenis.Items.Clear();
                    lokasi.Items.Clear();
                    baru.Items.Clear();
                    InitForm();
                    Fill();
                }
            }
        }

        private void InitForm()
        {
            //kalkulator            
            DataTable rs;
            string strSql;

            //Jenis
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
                jenis.SelectedIndex = 0;
            }


            if (jenis.Items.Count == 0)
            {
                save.Enabled = false;
                norefjenis.Text = "<font style='padding-left:5px;'>"
                    + "Setup jenis unit properti (REF_JENIS) belum dilakukan.</font><br><br>";
            }

            //Lokasi
            strSql = "SELECT * FROM REF_LOKASI WHERE Project = '" + project.SelectedValue + "' ORDER BY SN";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Lokasi"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                lokasi.Items.Add(new ListItem(t, v));
            }
            lokasi.SelectedIndex = 0;

            if (lokasi.Items.Count == 0)
            {
                save.Enabled = false;
                noreflokasi.Text = "<font style='padding-left:5px;'>"
                    + "Setup lokasi unit properti (REF_LOKASI) belum dilakukan.</font><br><br>";
            }

            ////Jenis Properti
            strSql = "SELECT * FROM REF_JENISPROPERTI WHERE Project = '" + project.SelectedValue + "' ORDER BY SN";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["JenisProperti"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                ListItem li = new ListItem();
                li.Text = t;
                li.Value = v;
                li.Attributes.Add("class", "radio");
                tipe.Items.Add(li);
                tipe.Width = 350;
                tipe.SelectedIndex = 0;
            }


            if (rs.Rows.Count == 0)
            {
                save.Enabled = false;
                noreflokasi.Text = "<font style='padding-left:5px;'>"
                    + "Setup jenis properti (REF_JENISPROPERTI) belum dilakukan.</font><br><br>";
            }
        }

        private void AutoID()
        {
            int c = Db.SingleInteger("SELECT COUNT(NoStock) FROM MS_UNIT");

            bool hasfound = false;
            while (!hasfound)
            {
                if (!Response.IsClientConnected) break;

                c++;
                nostock.Text = c.ToString().PadLeft(7, '0');

                if (isUnique()) hasfound = true;
            }
        }

        private bool isUnique()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");

            if (c != 0)
                x = false;

            return x;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            x = Cf.ValidMandatory(this, "Unit", project.SelectedValue);

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

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Unit Properti tidak boleh kosong.\\n"
                    + "2. Luas harus berupa angka.\\n"
                    + "3. Luas Tanah harus berupa angka.\\n"
                    + "4. Luas Bangunan harus berupa angka.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;

        }


        protected void save_Click(object sender, System.EventArgs e)
        {

            if (valid())
            {

                //Generate nomor unik
                AutoID();

                string NoStock = Cf.Pk(nostock.Text);
                string Jenis = jenis.SelectedValue;
                string Lokasi = lokasi.SelectedValue;
                string Lantai = Cf.Str(lantai.Text);
                string Nomor = Cf.Str(nomor.Text);                
                decimal LuasLebih = Convert.ToDecimal(luaslbh.Text);
                decimal LuasSG = Convert.ToDecimal(luassg.Text);
                decimal LuasNett = Convert.ToDecimal(luasnett.Text);
                string Project = Cf.Pk(project.SelectedValue);

                string ParamID = "FormatLantai" + Project;
                string ParamID2 = "FormatUnit" + Project;

                string strSql = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID + "'");
                string strSql2 = Db.SingleString("SELECT Value FROM [ISC064_SECURITY].[dbo].[REF_PARAM] WHERE ParamID = '" + ParamID2 + "'");


                string NoUnit = Lokasi + strSql + Lantai + strSql2 + Nomor;

                Db.Execute("EXEC spUnitDaftar"
                    + " '" + NoStock + "'"
                    + ",'" + Jenis + "'"
                    + ",'" + Lokasi + "'"
                    + ",'" + NoUnit + "'"
                    + ", " + LuasSG
                    + ",'" + Lantai + "'"
                    + ",'" + Nomor + "'"
                    + ",'" + Project + "'"
                    );

                int SifatPPN;
                if (kategori.SelectedValue == "REAL ESTATE")
                {
                    SifatPPN = 1;
                }
                else if (kategori.SelectedValue == "KOMERSIL")
                {
                    SifatPPN = 1;
                }
                else
                {
                    SifatPPN = 0;
                }

                Db.Execute("UPDATE MS_UNIT SET JenisProperti='" + tipe.SelectedValue
                    + "', LuasSG =" + LuasSG
                    + ", LuasNett =" + LuasNett
                    + ", LuasLebih =" + LuasLebih
                    + ", Kategori = '" + kategori.SelectedValue + "'"
                    + ", SifatPPN = " + SifatPPN + " "
                    + " WHERE NoStock='" + NoStock + "'");

                int c = Db.SingleInteger("SELECT COUNT(NoStock) FROM MS_UNIT WHERE NoStock = '" + NoStock + "'");
                if (c == 0)
                {
                    nostock.Text = "#AUTO#";
                    //nounitc.Text = "Duplikat";

                    Js.Alert(
                        this
                        , "Unit Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Nomor Unit sudah dipakai.\\n"
                        , "document.getElementById('nounit').focus();"
                        + "document.getElementById('nounit').select();"
                        );
                }
                else
                {
                    DataTable rs = Db.Rs("SELECT "
                        + " NoStock AS [No. Stock]"
                        + ",Jenis AS [Jenis]"
                        + ",Project"
                        + ",Lokasi AS [Lokasi]"
                        + ",NoUnit AS [Unit]"
                        + ",Kategori AS [Kategori Unit]"
                        + ",Lantai AS [Blok]"
                        + ",Nomor"
                        + ",Luas AS [Luas]"
                        + ",LuasSG AS [Luas Tanah]"
                        + ",LuasNett AS [Luas Bangunan]"
                        + ",LuasLebih AS [Luas Lebih Tanah]"
                        + " FROM MS_UNIT"
                        + " WHERE NoStock = '" + NoStock + "'"
                        );

                    Db.Execute("EXEC spLogUnit"
                        + " 'DAFTAR'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Cf.LogCapture(rs) + "'"
                        + ",'" + NoStock + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_UNIT_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE MS_UNIT_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

                    Response.Redirect("UnitDaftar.aspx?done=" + NoStock + "&project=" + project.SelectedValue);
                }
            }
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 25 NoStock, NoUnit "
                + " FROM MS_UNIT WHERE Project = '" + project.SelectedValue + "'"
                + " ORDER BY TglInput DESC, NoStock DESC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoStock"].ToString();
                string t = v + " (" + rs.Rows[i]["NoUnit"] + ")";

                baru.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditUnit("
                    + "this.options[this.selectedIndex].value)";
            }
        }

        private string NoStock
        {
            get
            {
                return Cf.Pk(nostock.Text);
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
            jenis.Items.Clear();
            lokasi.Items.Clear();
            noreflokasi.Text = "";
            norefjenis.Text = "";
            baru.Items.Clear();
            save.Enabled = true;
            InitForm();
            Fill();
        }
    }
}
