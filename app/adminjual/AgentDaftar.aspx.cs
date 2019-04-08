using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class AgentDaftar : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Bind();
                BindLevel();
                //BindGrade();
                Js.Focus(this, nama);
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
                        + "<a href=\"javascript:popEditAgent('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";

                    project.SelectedValue = Request.QueryString["project"];
                    tipe.Items.Clear();
                    level.Items.Clear();
                    baru.Items.Clear();
                    Bind();
                    BindLevel();
                    Fill();
                }
            }
        }

        private void Bind()
        {
            string strSql = "SELECT * FROM REF_AGENT_TIPE WHERE Project= '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs(strSql);
            tipe.Items.Add(new ListItem { Text = "Tipe :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["ID"].ToString();
                string t = rs.Rows[i]["Tipe"].ToString();
                tipe.Items.Add(new ListItem(t, v));
            }
        }



        private bool valid()
        {

            string s = "";
            bool x = true;

            if (tipe.SelectedIndex == 0 || level.SelectedIndex == 0) x = false;

            x = Cf.ValidMandatory(this, "Sales", project.SelectedValue) ? x : false;

            if (x == true)
            {
                string Kodesls = kodesls.Text;
                int a = Db.SingleInteger("SELECT ParentID FROM REF_AGENT_LEVEL WHERE LevelID = '" + level.SelectedValue + "'");
                if (a > 0)
                {
                    Kodesls = kodesls.Text;
                    if (Kodesls == "")
                    {
                        x = false;
                        kodeslsc.Text = "Harus Diisi";
                    }
                    else
                    {
                        kodeslsc.Text = "";
                    }

                    if (atasan.SelectedIndex <= 0)
                    {
                        x = false;
                        atasanc.Text = "Harus Dipilih";
                    }
                    else
                    {
                        atasanc.Text = "";
                    }
                }
                int cek = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_AGENT WHERE KodeSales = '" + Kodesls.ToString().Replace(" ", "") + "'");
                if (cek > 0)
                {
                    x = false;
                    kodeslsc.Text = s = "Kode marketing sudah digunakan.";
                }
                else
                    kodeslsc.Text = "";

                if (!Cf.isEmail(email1.Text))
                {
                    x = false;
                    if (s == "") s = email1.ID;
                    email1c.Text = "Format Email";
                }
                else
                    email1c.Text = "";
            }

            //int cekatasan = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT WHERE Atasan = '" + atasan.SelectedValue + "'");
            //if (cekatasan > 0)
            //{
            //    x = false;
            //    atasanc.Text = "Atasan Sudah digunakan";
            //}
            //else
            //    atasanc.Text = "harus pilih";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + s
                    + "1. Tipe harus dipilih.\\n"
                    + "2. Level harus dipilih.\\n"
                    + "3. Atasan tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                string KodeSales = kodesls.Text;
                string Nama = Cf.Str(nama.Text);
                int Tipe = Convert.ToInt32(tipe.SelectedValue);
                int Level = Convert.ToInt32(level.SelectedValue);
                int Atasan = 0;
                if (atasan.SelectedValue != "")
                {
                    Atasan = Convert.ToInt32(atasan.SelectedValue);
                }
                string Alamat = Cf.Str(alamat.Text);
                string Handphone = Cf.Str(hp.Text);
                string Whatsapp = Cf.Str(wa.Text);
                string Telpon = Cf.Str(telp.Text);
                string NPWP = Cf.Str(npwp.Text);
                string Email = Cf.Str(email1.Text);
                string RekBank = Cf.Str(rek.Text);
                string Bank = Cf.Str(rekbank1.Text);
                string AtasNama = Cf.Str(atasnama1.Text);
                //string Jabatan = Cf.Str(jabatan.SelectedItem.Text);

                Db.Execute("EXEC spAgentDaftar"
                    + " '" + Nama + "'"
                    + ",''"
                    + ",''"
                    + ",'" + Alamat + "'"
                    + ",'" + Telpon + "'"
                    + ",'" + NPWP + "'"
                    + ",'" + Tipe + "'"
                    + ",'" + Level + "'"
                    + ",''"
                    + ",'" + Atasan + "'"
                    + ",''"
                    + ",'" + RekBank + "'"
                    + ",''"
                    + ",'" + Bank + "'"
                    + ",'" + AtasNama + "'"
                    + ",''"
                    + ",'" + Level + "'"
                    );


                //get nomor agent terbaru
                noagent.Text = Db.SingleInteger(
                    "SELECT TOP 1 NoAgent FROM MS_AGENT ORDER BY NoAgent DESC")
                    .ToString().PadLeft(5, '0');

                int b = 0;
                int LvlAtasan = 0;

                if (Level > 1)
                {
                    b = Db.SingleInteger("SELECT ParentID FROM REF_AGENT_LEVEL WHERE LevelID = '" + level.SelectedValue + "'");
                    if (b > 0)
                    {
                        LvlAtasan = Db.SingleInteger("SELECT SalesLevel FROM MS_AGENT WHERE SalesLevel ='" + b + "'");
                    }
                }
                //Response.Write(strSql);

                Db.Execute("Update MS_AGENT SET KodeSales = '" + KodeSales.ToString().Replace(" ", "") + "',Whatsapp = '" + Whatsapp + "',Handphone = '" + Handphone + "',Email = '" + Email + "',LvlAtasan = '" + LvlAtasan + "',Project='" + project.SelectedValue + "' Where NoAgent = '" + noagent.Text + "'");


                DataTable rs = Db.Rs("SELECT "
                    + " NoAgent AS [No. Agent]"
                    + ",KodeSales AS [Kode Sales]"
                    + ",Nama AS [Nama Lengkap]"
                    //+ ",Principal AS [Nama Perusahaan]"
                    + ",Alamat"
                    + ",Kontak"
                    + ",Handphone"
                    + ",Whatsapp"
                    + ",NPWP"
                    + ",Email"
                    + ",RekBank"
                    + ",Rekening"
                    + ",AtasNama"
                    + ",Jabatan"
                    + ",Project"
                    + ",Skema0 AS [Skema Komisi Standard]"
                    + " FROM MS_AGENT a"
                    + " WHERE NoAgent = " + NoAgent
                    );

                Db.Execute("EXEC spLogAgent"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + NoAgent.PadLeft(5, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_AGENT_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_AGENT_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

                Response.Redirect("AgentDaftar.aspx?done=" + NoAgent + "&project=" + project.SelectedValue);
            }
        }

        private void Fill()
        {
            string strSql = "SELECT TOP 75 NoAgent, Nama "
                + " FROM MS_AGENT WHERE Project = '" + project.SelectedValue + "'"
                + " ORDER BY TglInput DESC, NoAgent DESC";

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v + ")";

                baru.Items.Add(new ListItem(t, v));
            }

            if (rs.Rows.Count != 0)
            {
                baru.SelectedIndex = 0;
                baru.Attributes["ondblclick"] = "popEditAgent("
                    + "this.options[this.selectedIndex].value)";
            }
        }

        private string NoAgent
        {
            get
            {
                return Cf.Pk(noagent.Text);
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

        protected void tipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLevel();
        }

        protected void level_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (level.SelectedIndex == 0) //VALIDASI 2 SPV DIDALAM 1 TIPE
            {
                trAtasan.Visible = false;
            }
            else if (level.SelectedIndex == 1)
            {
                trAtasan.Visible = false;
                //BindAtasan();
                //trAtasanM.Visible = true;
            }
            else
            {
                trAtasan.Visible = true;
                BindAtasan();
            }
            //BindGrade();
        }

        void BindLevel()
        {
            level.Items.Clear();
            DataTable rs = Db.Rs("SELECT * FROM REF_AGENT_LEVEL WHERE Tipe='" + tipe.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");
            level.Items.Add(new ListItem { Text = "Jabatan :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["LevelID"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                level.Items.Add(new ListItem(t, v));
            }
        }

        void BindAtasan()
        {
            atasan.Items.Clear();

            int a = Db.SingleInteger("SELECT ParentID FROM REF_AGENT_LEVEL WHERE LevelID = '" + level.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");

            string strSql = "SELECT * FROM MS_AGENT WHERE SalesLevel ='" + a + "' AND Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs(strSql);
            atasan.Items.Add(new ListItem { Text = "Atasan :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                atasan.Items.Add(new ListItem(t, v));
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipe.Items.Clear();
            level.Items.Clear();
            trAtasan.Visible = false;
            baru.Items.Clear();
            Fill();
            Bind();
        }
    }
}


