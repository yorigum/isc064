using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class AgentEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoAgent");

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Fill();
                BindTipe();
                BindLevel();
            }

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
            }
        }

        private void Fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_AGENT_LOG&Pk=" + NoAgent.PadLeft(5, '0') + "'";
            btndel.Attributes["onclick"] = "location.href='AgentDel.aspx?NoAgent=" + NoAgent + "'";

            string strSql = "SELECT * FROM MS_AGENT WHERE NoAgent = " + NoAgent;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                noagent.Text = rs.Rows[0]["NoAgent"].ToString().PadLeft(5, '0');
                kodesls1.Text = rs.Rows[0]["KodeSales"].ToString();
                kodesls.Text = rs.Rows[0]["KodeSales"].ToString();
                nama.Text = rs.Rows[0]["Nama"].ToString();
                principal.Text = rs.Rows[0]["Principal"].ToString();
                alamat.Text = rs.Rows[0]["Alamat"].ToString();
                telp.Text = rs.Rows[0]["Kontak"].ToString();
                hp.Text = rs.Rows[0]["Handphone"].ToString();
                wa.Text = rs.Rows[0]["Whatsapp"].ToString();
                npwp.Text = rs.Rows[0]["NPWP"].ToString();
                email1.Text = rs.Rows[0]["Email"].ToString();
                rekbank1.Text = rs.Rows[0]["RekBank"].ToString();
                //cabang1.Text = rs.Rows[0]["Cabang"].ToString();
                norek1.Text = rs.Rows[0]["Rekening"].ToString();
                atasnama1.Text = rs.Rows[0]["AtasNama"].ToString();
                level.Items.Add(rs.Rows[0]["SalesLevel"].ToString());
                level.SelectedValue = rs.Rows[0]["SalesLevel"].ToString();
                tipe.SelectedValue = rs.Rows[0]["SalesTipe"].ToString();
                project.SelectedValue = rs.Rows[0]["Project"].ToString();

                atasan.Items.Clear();
                if (rs.Rows[0]["LvlAtasan"].ToString() != "0")
                {
                    int a = Db.SingleInteger("SELECT ParentID FROM REF_AGENT_LEVEL WHERE LevelID = '" + rs.Rows[0]["SalesLevel"] + "'");

                    string strSql2 = "SELECT * FROM MS_AGENT WHERE SalesLevel ='" + a + "' AND SalesTipe='" + rs.Rows[0]["SalesTipe"].ToString() + "'";

                    DataTable rs2 = Db.Rs(strSql2);
                    atasan.Items.Add(new ListItem { Text = "Atasan :", Value = "0" });

                    for (int i = 0; i < rs2.Rows.Count; i++)
                    {
                        string v = rs2.Rows[i]["NoAgent"].ToString();
                        string t = rs2.Rows[i]["Nama"].ToString();
                        atasan.Items.Add(new ListItem(t, v));
                    }
                }
                else
                    trAtasan.Visible = false;
                //string strSql2 = "SELECT * FROM MS_AGENT WHERE SalesLevel ='" + rs.Rows[0]["LvlAtasan"] + "'";
                //DataTable rs2 = Db.Rs(strSql2);
                //atasan.Items.Add(new ListItem { Text = "Atasan :", Value = "0" });

                //for (int i = 0; i < rs2.Rows.Count; i++)
                //{
                //    string v = rs2.Rows[i]["NoAgent"].ToString();
                //    string t = rs2.Rows[i]["Nama"].ToString();
                //    atasan.Items.Add(new ListItem(t, v));
                //}
                atasan.SelectedValue = rs.Rows[0]["Atasan"].ToString();
                jabatan.Items.Add(new ListItem(rs.Rows[0]["Jabatan"].ToString()));
                jabatan.SelectedValue = rs.Rows[0]["Jabatan"].ToString();

                if ((string)rs.Rows[0]["Status"] == "A")
                    aktif.Checked = true;
                else
                    inaktif.Checked = true;

                tglInput.Text = Cf.Date(rs.Rows[0]["TglInput"]);
                tglEdit.Text = Cf.Date(rs.Rows[0]["TglEdit"]);
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;
            string Kodesls = kodesls.Text;
            int cek = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_AGENT WHERE KodeSales = '" + Kodesls.ToString().Replace(" ", "") + "' AND NoAgent != '" + noagent.Text + "'");
            if (cek > 0)
            {
                x = false;
                kodeslsc.Text = "Kode Sales Sudah digunakan";
            }
            else
                kodeslsc.Text = "";

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
            else
            {
                x = true;
            }

            x = Cf.ValidMandatory(this, "Sales", project.SelectedValue) ? x : false;

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Level tidak boleh kosong.\\n"
                    + "2. Atasan tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                string Kodesls = Cf.Str(kodesls.Text);
                string Nama = Cf.Str(nama.Text);
                string Alamat = Cf.Str(alamat.Text);
                string Kontak = Cf.Str(telp.Text);
                string Handphone = Cf.Str(hp.Text);
                string Whatsapp = Cf.Str(wa.Text);
                string NPWP = Cf.Str(npwp.Text);
                string Email = Cf.Str(email1.Text);
                string RekBank = Cf.Str(rekbank1.Text);
                string NoRek = Cf.Str(norek1.Text);
                string AtasNama = Cf.Str(atasnama1.Text);
                string Status = "";
                int Tipe = Convert.ToInt32(tipe.SelectedValue);
                int Level = Convert.ToInt32(level.SelectedValue);
                int Atasan = Convert.ToInt32(atasan.SelectedValue);
                if (aktif.Checked) Status = "A";
                if (inaktif.Checked) Status = "I";
                string Jabatan = Cf.Str(jabatan.SelectedItem.Text);

                DataTable rsBef = Db.Rs("SELECT "
                    + " NoAgent AS [No. Agent]"
                    + ",KodeSales AS [Kode Sales]"
                    + ",Nama AS [Nama Lengkap]"
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
                    + ",Status"
                    + ",SalesTipe"
                    + ",SalesLevel"
                    + ",Atasan"
                    + ",LvlAtasan"
                    + ",Project"
                    + ",Skema0 AS [Skema Komisi Standard]"
                    + " FROM MS_AGENT a"
                    + " WHERE NoAgent = " + NoAgent
                    );

                Db.Execute("EXEC spAgentEdit"
                    + "  " + NoAgent
                    + ",'" + Nama + "'"
                    + ",'" + Alamat + "'"
                    + ",'" + Kontak + "'"
                    + ",'" + NPWP + "'"
                    + ",'" + Tipe + "'"
                    + ",'" + Level + "'"
                    + ",'" + Atasan + "'"
                    + ",'" + Email + "'"
                    + ",'" + RekBank + "'"
                    + ",'" + NoRek + "'"
                    + ",'" + AtasNama + "'"
                    + ",'" + Status + "'"
                    //+ ",''"
                    );

                int b = Db.SingleInteger("SELECT ParentID FROM REF_AGENT_LEVEL WHERE LevelID = '" + level.SelectedValue + "'");
                int LvlAtasan = 0;
                if (b == 0)
                {
                    LvlAtasan = 0;
                    Atasan = 0;
                }
                else
                {
                    LvlAtasan = Db.SingleInteger("SELECT SalesLevel FROM MS_AGENT WHERE SalesLevel ='" + b + "'");
                }

                Db.Execute("UPDATE MS_AGENT SET KodeSales = '" + Kodesls.ToString().Replace(" ", "") + "' "
                    + ", Handphone = '" + Handphone + "'"
                    + ", Whatsapp = '" + Whatsapp + "'"
                    + ",Project = '" + project.SelectedValue + "'"
                    + ",LvlAtasan = '" + LvlAtasan + "'"
                    + ",Atasan = '" + Atasan + "'"
                    + "WHERE NoAgent = " + NoAgent
                    );

                DataTable rsAft = Db.Rs("SELECT "
                    + " NoAgent AS [No. Agent]"
                    + ",KodeSales AS [Kode Sales]"
                    + ",Nama AS [Nama Lengkap]"
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
                    + ",Status"
                    + ",SalesTipe"
                    + ",SalesLevel"
                    + ",Atasan"
                    + ",LvlAtasan"
                    + ",Project"
                    + ",Skema0 AS [Skema Komisi Standard]"
                    + " FROM MS_AGENT a"
                    + " WHERE NoAgent = " + NoAgent
                    );

                //Logfile
                string Ket = "Nama Lengkap : " + Nama + "<br>"
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogAgent"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoAgent.PadLeft(5, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_AGENT_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_AGENT_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("AgentEdit.aspx?done=1&NoAgent=" + NoAgent);
        }

        private string NoAgent
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoAgent"]);
            }
        }

        protected void tipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLevel();
        }

        protected void level_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (level.SelectedIndex == 0)
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

        private void BindTipe()
        {
            tipe.Items.Clear();
            string strSql = "SELECT * FROM REF_AGENT_TIPE WHERE Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs(strSql);
            tipe.Items.Add(new ListItem { Text = "Tipe :", Value = "0" });
            int Tipe = Db.SingleInteger("SELECT SalesTipe FROM MS_AGENT WHERE NoAgent = " + NoAgent);
            string NamaTipe = Db.SingleString("SELECT Tipe FROM REF_AGENT_TIPE WHERE ID = " + Tipe);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["ID"].ToString();
                string t = rs.Rows[i]["Tipe"].ToString();
                tipe.Items.Add(new ListItem(t, v));
            }
            tipe.Items.Add(new ListItem("Sekarang : " + NamaTipe, Tipe.ToString()));
            tipe.SelectedValue = Tipe.ToString();
        }

        private void BindLevel()
        {
            level.Items.Clear();
            DataTable rs = Db.Rs("SELECT * FROM REF_AGENT_LEVEL WHERE Project = '" + project.SelectedValue + "' AND Tipe = '" + tipe.SelectedValue + "'");
            level.Items.Add(new ListItem { Text = "Jabatan :", Value = "0" });
            int Level = Db.SingleInteger("SELECT SalesLevel FROM MS_AGENT WHERE NoAgent = " + NoAgent);
            string NamaLevel = Db.SingleString("SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = " + Level);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["LevelID"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                level.Items.Add(new ListItem(t, v));
            }
            level.Items.Add(new ListItem("Sekarang : " + NamaLevel, Level.ToString()));
            level.SelectedValue = Level.ToString();
        }

        void BindAtasan()
        {
            atasan.Items.Clear();
            int a = Db.SingleInteger("SELECT ParentID FROM REF_AGENT_LEVEL WHERE LevelID = '" + level.SelectedValue + "'");

            string strSql = "SELECT * FROM MS_AGENT WHERE SalesLevel ='" + a + "'";
            DataTable rs = Db.Rs(strSql);
            atasan.Items.Add(new ListItem { Text = "Atasan :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = rs.Rows[i]["Nama"].ToString();
                atasan.Items.Add(new ListItem(t, v));
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
            BindLevel();
            BindTipe();
        }
    }
}
