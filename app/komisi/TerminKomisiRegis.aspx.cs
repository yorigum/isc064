using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.KOMISI
{
    public partial class TerminKomisiRegis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Bind();

                Baris = 5;
            }

            fillbaris();

            FeedBack();
        }
        private void Bind()
        {
            Js.Focus(this, nama);
            Act.ProjectList(project);
            LibMkt.ListTipeSales(tipe, project.SelectedValue);
        }
        protected void fillbaris()
        {
            for (int i = 1; i <= Baris; i++)
            {
                tambah(i);
            }
        }
        protected int Baris
        {
            get { return Convert.ToInt32(Session["RowsSkom"]); }
            set { Session["RowsSkom"] = value; }
        }
        protected void add_Click(object sender, EventArgs e)
        {
            Add(1);
        }
        protected void del_Click(object sender, EventArgs e)
        {
            TableRow r = (TableRow)list.FindControl("baris_" + (--Baris));

            list.Controls.Remove(r);
        }
        protected void Add(int c)
        {
            for (short i = 1; i <= c; i++)
            {
                Baris++;
                tambah(Baris);
            }

        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popSkom2('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
            }
        }
        protected void tambah(int index)
        {
            TableRow r = new TableRow();
            r.ID = "baris_" + index;
            TableCell c;
            RadioButtonList rbl;
            TextBox tb;
            HtmlInputButton btn;
            Label l;
            CheckBox cb;
            DropDownList ddl;

            ddl = new DropDownList();
            ddl.ID = "lvlsales_" + index;

            if (tipe.SelectedIndex == 0)
            {
                LibMkt.ListLvlSales(ddl, 0, project.SelectedValue);
            }
            else
            {
                LibMkt.ListLvlSales(ddl, Convert.ToInt32(tipe.SelectedValue), project.SelectedValue);             
            }
            
            c = new TableCell();
            c.Controls.Add(ddl);
            r.Cells.Add(c);

            tb = new TextBox();
            tb.ID = "nama_" + index;
            tb.CssClass = "form-control";
            tb.Width = 200;

            c = new TableCell();
            c.Controls.Add(tb);
            r.Cells.Add(c);

            tb = new TextBox();
            tb.ID = "persencair_" + index;
            Js.NumberFormat2(tb);

            c = new TableCell();
            c.Controls.Add(tb);
            r.Cells.Add(c);

            cb = new CheckBox();
            cb.ID = "lunas_" + index;

            tb = new TextBox();
            tb.ID = "persenlunas_" + index;
            Js.NumberFormat2(tb);

            c = new TableCell();
            c.Controls.Add(cb);
            c.Controls.Add(tb);
            r.Cells.Add(c);

            cb = new CheckBox();
            cb.ID = "bf_" + index;

            tb = new TextBox();
            tb.ID = "persenbf_" + index;
            Js.NumberFormat2(tb);

            c = new TableCell();
            c.Controls.Add(cb);
            c.Controls.Add(tb);
            r.Cells.Add(c);

            cb = new CheckBox();
            cb.ID = "dp_" + index;

            tb = new TextBox();
            tb.ID = "persendp_" + index;
            Js.NumberFormat2(tb);

            c = new TableCell();
            c.Controls.Add(cb);
            c.Controls.Add(tb);
            r.Cells.Add(c);

            cb = new CheckBox();
            cb.ID = "ang_" + index;

            tb = new TextBox();
            tb.ID = "persenang_" + index;
            Js.NumberFormat2(tb);

            c = new TableCell();
            c.Controls.Add(cb);
            c.Controls.Add(tb);
            r.Cells.Add(c);

            cb = new CheckBox();
            cb.ID = "ppjb_" + index;

            c = new TableCell();
            c.Controls.Add(cb);
            r.Cells.Add(c);

            cb = new CheckBox();
            cb.ID = "ajb_" + index;

            c = new TableCell();
            c.Controls.Add(cb);
            r.Cells.Add(c);

            cb = new CheckBox();
            cb.ID = "akad_" + index;

            if (carabayar.SelectedIndex > 0)
            {
                if (carabayar.SelectedValue == "KPR")
                {
                    cb.Enabled = true;
                }
                else
                    cb.Enabled = false;
            }
            c = new TableCell();
            c.Controls.Add(cb);
            r.Cells.Add(c);

            rbl = new RadioButtonList();
            rbl.ID = "tipecair_" + index;
            rbl.RepeatDirection = RepeatDirection.Horizontal;
            rbl.Items.Add(new ListItem("Semua", "0"));
            rbl.Items.Add(new ListItem("Salah Satu", "1"));
            rbl.SelectedIndex = 0;

            c = new TableCell();
            c.Controls.Add(rbl);
            r.Cells.Add(c);

            c = new TableCell();
            l = new Label();
            l.Text = "<i class='fa fa-trash'></i>";
            l.CssClass = "btn btn-cal";
            StringBuilder x = new StringBuilder();
            x.Append("ClearTermin('nama_" + index + "','persencair_" + index + "','lunas_" + index + "','persenlunas_" + index + "','bf_" + index + "','persenbf_" + index + "','dp_" + index + "','persendp_" + index + "','ang_" + index + "','persenang_" + index + "','ppjb_" + index + "','ajb_" + index + "','akad_" + index + "');");
            l.Attributes["onclick"] = x.ToString();
            c.Controls.Add(l);
            r.Cells.Add(c);

            list.Controls.Add(r);
        }

        private bool valid()
        {

            string s = "";
            bool x = true;

            //nama
            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            if (tipe.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = tipe.ID;
                tipec.Text = "Pilih";
            }


            //cek existing termin
            int cek = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKOM_TERM WHERE CaraBayar = '" + carabayar.SelectedValue + "' AND SalesTipe = " + Convert.ToInt32(tipe.SelectedValue)
                + " AND Inaktif = 0"
                );

            if (cek > 0)
            {
                x = false;
            }

            //int ID = Db.SingleInteger("SELECT ID FROM REF_AGENT_TIPE WHERE Project = '" + project.SelectedValue + "'");
            //int tipesales = Db.SingleInteger("SELECT Count(Tipe) FROM REF_AGENT_LEVEL WHERE Tipe = '" + ID + "'");
            int tipesales = Db.SingleInteger("SELECT Count(ID) FROM REF_AGENT_TIPE WHERE ID = '" + tipe.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");

            if (tipesales == 0)
            {
                x = false;
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Nama tidak boleh kosong.\\n"
                    + "2. Termin yang terdaftar harus unik.\\n"
                    + "3. Tipe marketing harus dipilih.\\n"
                    + "4. Tipe marketing tidak tersedia untuk project yang dipilih.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );
            }
            return x;
        }
        protected bool validform1
        {
            get
            {
                bool x = true;
                StringBuilder err = new StringBuilder();

                //Satuan Unit
                for (int i = 1; i <= Baris; i++)
                {
                    if (!Response.IsClientConnected) break;

                    DropDownList lvlsales = (DropDownList)list.FindControl("lvlsales_" + i);
                    TextBox nama = (TextBox)list.FindControl("nama_" + i);
                    TextBox persencair = (TextBox)list.FindControl("persencair_" + i);
                    CheckBox lunas = (CheckBox)list.FindControl("lunas_" + i);
                    TextBox persenlunas = (TextBox)list.FindControl("persenlunas_" + i);
                    CheckBox bf = (CheckBox)list.FindControl("bf_" + i);
                    TextBox persenbf = (TextBox)list.FindControl("persenbf_" + i);
                    CheckBox dp = (CheckBox)list.FindControl("dp_" + i);
                    TextBox persendp = (TextBox)list.FindControl("persendp_" + i);
                    CheckBox ang = (CheckBox)list.FindControl("ang_" + i);
                    TextBox persenang = (TextBox)list.FindControl("persenang_" + i);
                    CheckBox ppjb = (CheckBox)list.FindControl("ppjb_" + i);
                    CheckBox ajb = (CheckBox)list.FindControl("ajb_" + i);
                    CheckBox akad = (CheckBox)list.FindControl("akad_" + i);
                    RadioButtonList tipecair = (RadioButtonList)list.FindControl("tipecair_" + i);

                    if (nama.Text + persencair.Text != "" && lvlsales.Text != "")
                    {
                        bool xx = true;
                        if (!Cf.isMoney(persencair))
                        {
                            x = xx = false;
                            Cf.MarkError(persencair);
                        }
                        //if (lvlsales.SelectedValue == "")
                        //{
                        //    x = xx = false;
                        //    Cf.MarkError(lvlsales);
                        //}
                        if (lunas.Checked)
                        {
                            if (!Cf.isMoney(persenlunas))
                            {
                                x = xx = false;
                                Cf.MarkError(persenlunas);
                            }
                        }
                        if (bf.Checked)
                        {
                            if (!Cf.isMoney(persenbf))
                            {
                                x = xx = false;
                                Cf.MarkError(persenbf);
                            }
                        }
                        if (dp.Checked)
                        {
                            if (!Cf.isMoney(persendp))
                            {
                                x = xx = false;
                                Cf.MarkError(persendp);
                            }
                        }
                        if (ang.Checked)
                        {
                            if (!Cf.isMoney(persenang))
                            {
                                x = xx = false;
                                Cf.MarkError(persenang);
                            }
                        }
                        if (!xx) err.Append("Termin Komisi - Baris ke-" + i + "\\n");
                    }
                }

                if (!x) Js.Alert(Page, err.ToString(), "");
                return x;
            }
        }
        protected bool validcair
        {
            get
            {
                bool x = true;
                StringBuilder err = new StringBuilder();
                string level = "";                

                DataTable rs = Db.Rs("SELECT * FROM REF_AGENT_LEVEL WHERE Tipe = " + tipe.SelectedValue + "AND Project = '" + project.SelectedValue + "'");
                for (int a = 0; a < rs.Rows.Count; a++)
                {
                    decimal t = 0;

                    for (int i = 1; i <= Baris; i++)
                    {
                        if (!Response.IsClientConnected) break;
                        DropDownList lvlsales = (DropDownList)list.FindControl("lvlsales_" + i);
                        TextBox nama = (TextBox)list.FindControl("nama_" + i);
                        TextBox persencair = (TextBox)list.FindControl("persencair_" + i);
                        CheckBox lunas = (CheckBox)list.FindControl("lunas_" + i);
                        TextBox persenlunas = (TextBox)list.FindControl("persenlunas_" + i);
                        CheckBox bf = (CheckBox)list.FindControl("bf_" + i);
                        TextBox persenbf = (TextBox)list.FindControl("persenbf_" + i);
                        CheckBox dp = (CheckBox)list.FindControl("dp_" + i);
                        TextBox persendp = (TextBox)list.FindControl("persendp_" + i);
                        CheckBox ang = (CheckBox)list.FindControl("ang_" + i);
                        TextBox persenang = (TextBox)list.FindControl("persenang_" + i);
                        CheckBox ppjb = (CheckBox)list.FindControl("ppjb_" + i);
                        CheckBox ajb = (CheckBox)list.FindControl("ajb_" + i);
                        CheckBox akad = (CheckBox)list.FindControl("akad_" + i);
                        RadioButtonList tipecair = (RadioButtonList)list.FindControl("tipecair_" + i);

                        if (nama.Text + persencair.Text != "" && lvlsales.Text != "")
                        {
                            if (lvlsales.SelectedValue == rs.Rows[a]["LevelID"].ToString())
                            {                                
                                t += Convert.ToDecimal(persencair.Text);
                            }
                        }

                        if (t > 100)
                        {
                            x = false;
                        }
                        
                    }
                }

                if (!x) Js.Alert(Page, "Persentase Pencairan > 100", "");

                return x;
            }
        }
        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid() && validform1 && validcair)
            {
                Save();
            }
        }
        protected void Save()
        {
            string Nama = Cf.Str(nama.Text);
            string CaraBayar = carabayar.SelectedValue;
            int SalesTipe = Convert.ToInt32(tipe.SelectedValue);

            Db.Execute("EXEC spSkomTermBaru"
                + " '" + Nama + "'"
                + ",'" + CaraBayar + "'"
                + ", " + SalesTipe
                );

            int Nomor = Db.SingleInteger("SELECT TOP 1 NoTermin FROM REF_SKOM_TERM"
                + " ORDER BY NoTermin DESC"
                );

            Db.Execute("UPDATE REF_SKOM_TERM SET "
            + " Project = '" + project.SelectedValue + "'"
            + " WHERE NoTermin = '" + Nomor + "'");

            DataTable rsHeader = Db.Rs("SELECT "
                + " NoTermin"
                + ",Nama"
                + ",Project"
                + ",CaraBayar AS [Cara Bayar]"
                + ",SalesTipe"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM "
                + " WHERE NoTermin = " + Nomor);

            DataTable rsSkemaBef = Db.Rs("SELECT "
                + " NoTermin"
                + ",Nama"
                + ",CaraBayar AS [Cara Bayar]"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM "
                + " WHERE NoTermin = " + Nomor);

            DataTable rsDetailBef = Db.Rs("SELECT "
                + " CONVERT(VARCHAR, SN) "
                + " + '.  ' + Nama  + '  '"
                + " + '  Cair ' + CONVERT(VARCHAR, PersenCair, 1) + ' %  ' "
                + " + '  BF ' + CONVERT(VARCHAR, PersenBF, 1) + ' %  ' "
                + " + '  DP ' + CONVERT(VARCHAR, PersenDP, 1) + ' %  ' "
                + " + '  ANG ' + CONVERT(VARCHAR, PersenANG, 1) + ' %  ' "
                + " + '  Syarat Cair (' + CONVERT(VARCHAR, TipeCair) + ')  ' "
                + " + '  PPJB (' + CONVERT(VARCHAR, PPJB, 1) + ')  ' "
                + " + '  AJB (' + CONVERT(VARCHAR, AJB, 1) + ')  ' "
                + " + '  AKAD (' + CONVERT(VARCHAR, AKAD, 1) + ')  ' "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM_DETAIL WHERE NoTermin = " + Nomor);

            SaveRumus(Nomor);

            DataTable rsSkemaAft = Db.Rs("SELECT "
                + " NoTermin"
                + ",Nama"
                + ",CaraBayar AS [Cara Bayar]"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM "
                + " WHERE NoTermin = " + Nomor);

            DataTable rsDetailAft = Db.Rs("SELECT "
                + " CONVERT(VARCHAR, SN) "
                + " + '.  ' + Nama  + '  '"
                + " + '  Cair ' + CONVERT(VARCHAR, PersenCair, 1) + ' %  ' "
                + " + '  BF ' + CONVERT(VARCHAR, PersenBF, 1) + ' %  ' "
                + " + '  DP ' + CONVERT(VARCHAR, PersenDP, 1) + ' %  ' "
                + " + '  ANG ' + CONVERT(VARCHAR, PersenANG, 1) + ' %  ' "
                + " + '  Syarat Cair (' + CONVERT(VARCHAR, TipeCair) + ')  ' "
                + " + '  PPJB (' + CONVERT(VARCHAR, PPJB, 1) + ')  ' "
                + " + '  AJB (' + CONVERT(VARCHAR, AJB, 1) + ')  ' "
                + " + '  AKAD (' + CONVERT(VARCHAR, AKAD, 1) + ')  ' "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM_DETAIL WHERE NoTermin = " + Nomor);

            string Ket = Cf.LogCapture(rsHeader)
                + "<br>---SKEMA---<br>"
                + Cf.LogCompare(rsSkemaBef, rsSkemaAft)
                + "<br>---DETAIL---<br>"
                + Cf.LogCompare(rsDetailBef, rsDetailAft);

            Db.Execute("EXEC spLogSkomTerm"
                + " 'DAFTAR'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + Nomor.ToString().PadLeft(5, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_SKOM_TERM_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE REF_SKOM_TERM_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

            Response.Redirect("TerminKomisi.aspx?done=" + Nomor);
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

        protected void gantitipe(object sender, EventArgs e)
        {
            for (int i = 1; i <= Baris; i++)
            {
                if (!Response.IsClientConnected) break;

                DropDownList lvlsales = (DropDownList)list.FindControl("lvlsales_" + i);
                lvlsales.Items.Clear();
                LibMkt.ListLvlSales(lvlsales, Convert.ToInt32(tipe.SelectedValue), project.SelectedValue);
            }
        }
        void SaveRumus(int Nomor)
        {
            int i = 1;
            foreach (var r in list.Controls)
            {
                DropDownList lvlsales = (DropDownList)list.FindControl("lvlsales_" + i);
                TextBox nama = (TextBox)list.FindControl("nama_" + i);
                TextBox persencair = (TextBox)list.FindControl("persencair_" + i);
                CheckBox lunas = (CheckBox)list.FindControl("lunas_" + i);
                TextBox persenlunas = (TextBox)list.FindControl("persenlunas_" + i);
                CheckBox bf = (CheckBox)list.FindControl("bf_" + i);
                TextBox persenbf = (TextBox)list.FindControl("persenbf_" + i);
                CheckBox dp = (CheckBox)list.FindControl("dp_" + i);
                TextBox persendp = (TextBox)list.FindControl("persendp_" + i);
                CheckBox ang = (CheckBox)list.FindControl("ang_" + i);
                TextBox persenang = (TextBox)list.FindControl("persenang_" + i);
                CheckBox ppjb = (CheckBox)list.FindControl("ppjb_" + i);
                CheckBox ajb = (CheckBox)list.FindControl("ajb_" + i);
                CheckBox akad = (CheckBox)list.FindControl("akad_" + i);
                RadioButtonList tipecair = (RadioButtonList)list.FindControl("tipecair_" + i);

                if (nama.Text + persencair.Text != "")
                {
                    int SalesLevel = Convert.ToInt32(lvlsales.SelectedValue);
                    string Nama = Cf.Str(nama.Text);
                    decimal PersenCair = persencair.Text != "" ? Convert.ToDecimal(persencair.Text) : 0;
                    bool Lunas = lunas.Checked;
                    decimal PersenLunas = persenlunas.Text != "" ? Convert.ToDecimal(persenlunas.Text) : 0;
                    bool BF = bf.Checked;
                    decimal PersenBF = persenbf.Text != "" ? Convert.ToDecimal(persenbf.Text) : 0;
                    bool DP = dp.Checked;
                    decimal PersenDP = persendp.Text != "" ? Convert.ToDecimal(persendp.Text) : 0;
                    bool ANG = ang.Checked;
                    decimal PersenANG = persenang.Text != "" ? Convert.ToDecimal(persenang.Text) : 0;
                    bool PPJB = ppjb.Checked;
                    bool AJB = ajb.Checked;
                    bool AKAD = akad.Checked;
                    byte TipeCair = Convert.ToByte(tipecair.SelectedValue);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spSkomTermTambah "
                        + Nomor
                        + ",'" + Nama + "'"
                        + ", " + PersenCair
                        + ", " + Lunas
                        + ", " + PersenLunas
                        + ", " + BF
                        + ", " + PersenBF
                        + ", " + DP
                        + ", " + PersenDP
                        + ", " + ANG
                        + ", " + PersenANG
                        + ", " + PPJB
                        + ", " + AJB
                        + ", " + AKAD
                        + ", " + TipeCair
                        + ", " + SalesLevel
                        );

                }
                i++;
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipe.Items.Clear();
            tipe.Items.Add(new ListItem("Tipe Marketing : ", "0"));
            LibMkt.ListTipeSales(tipe, project.SelectedValue);
            for (int i = 1; i <= Baris; i++)
            {
                if (!Response.IsClientConnected) break;

                DropDownList lvlsales = (DropDownList)list.FindControl("lvlsales_" + i);
                lvlsales.Items.Clear();
                LibMkt.ListLvlSales(lvlsales, 0, project.SelectedValue);
            }
        }
        

    }
}
