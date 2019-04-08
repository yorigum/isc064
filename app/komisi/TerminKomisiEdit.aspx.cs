using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.KOMISI
{
    public partial class TerminKomisiEdit : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("Nomor");

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                FillHeader();
            }

            fillbaris();

            if (!Page.IsPostBack)
            {
                FillTable();
            }

            FeedBack();
        }

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

        protected void fillbaris()
        {
            for (int i = 1; i <= Baris; i++)
            {
                tambah(i);
            }
        }

        protected int Baris
        {
            get { return Convert.ToInt32(Session["RowsSkom_"]); }
            set { Session["RowsSkom_"] = value; }
        }

        protected void add_Click(object sender, EventArgs e)
        {
            Add(1);
        }

        protected void del_Click(object sender, EventArgs e)
        {
            TableRow r = (TableRow)list.FindControl("baris_" + (Baris--));

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

        protected void tambah(int index)
        {
            TableRow r = new TableRow();
            r.ID = "baris_" + index;
            TableCell c;
            DropDownList ddl;
            RadioButtonList rbl;
            TextBox tb;
            CheckBox cb;

            ddl = new DropDownList();
            ddl.ID = "lvlsales_" + index;
            LibMkt.ListLvlSales(ddl, Convert.ToInt32(tipe.SelectedValue), project.SelectedValue);

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

            list.Controls.Add(r);
        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
                if (Request.QueryString["delete"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Hapus Berhasil...";

            }
        }

        private void FillHeader()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_SKOM_TERM_LOG&Pk=" + Nomor.PadLeft(5, '0') + "'";
            btndel.Attributes["onclick"] = "location.href='TerminKomisiDel.aspx?Nomor=" + Nomor + "'";
            Act.ProjectList(project);            

            DataTable rsHeader = Db.Rs("SELECT * FROM REF_SKOM_TERM WHERE NoTermin = " + Nomor);
            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nama.Text = rsHeader.Rows[0]["Nama"].ToString();
                carabayar.SelectedValue = rsHeader.Rows[0]["CaraBayar"].ToString();
                project.SelectedValue = rsHeader.Rows[0]["Project"].ToString();
                LibMkt.ListTipeSales(tipe, project.SelectedValue);
                tipe.SelectedValue = rsHeader.Rows[0]["SalesTipe"].ToString();

                if (rsHeader.Rows[0]["Inaktif"].ToString() == "False")
                {
                    aktif.Checked = true;
                    inaktif.Checked = false;
                }
                else
                {
                    aktif.Checked = false;
                    inaktif.Checked = true;
                }

                int Count = Db.SingleInteger("SELECT COUNT(NoTermin) FROM REF_SKOM_TERM_DETAIL WHERE NoTermin = " + Nomor);
                Baris = Count;

                if (Count == 0)
                    Baris = 5;
            }
        }

        private void FillTable()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_SKOM_TERM_DETAIL WHERE NoTermin = " + Nomor);
            if (rs.Rows.Count > 0)
            {
                for (int i = 1; i <= rs.Rows.Count; i++)
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

                    lvlsales.SelectedValue = rs.Rows[i - 1]["SalesLevel"].ToString();
                    nama.Text = rs.Rows[i - 1]["Nama"].ToString();
                    persencair.Text = Cf.Num(rs.Rows[i - 1]["PersenCair"]);
                    lunas.Checked = Convert.ToBoolean(rs.Rows[i - 1]["Lunas"]);
                    persenlunas.Text = Cf.Num(rs.Rows[i - 1]["PersenLunas"]);
                    bf.Checked = Convert.ToBoolean(rs.Rows[i - 1]["BF"]);
                    persenbf.Text = Cf.Num(rs.Rows[i - 1]["PersenBF"]);
                    dp.Checked = Convert.ToBoolean(rs.Rows[i - 1]["DP"]);
                    persendp.Text = Cf.Num(rs.Rows[i - 1]["PersenDP"]);
                    ang.Checked = Convert.ToBoolean(rs.Rows[i - 1]["ANG"]);
                    persenang.Text = Cf.Num(rs.Rows[i - 1]["PersenANG"]);
                    ppjb.Checked = Convert.ToBoolean(rs.Rows[i - 1]["PPJB"]);
                    ajb.Checked = Convert.ToBoolean(rs.Rows[i - 1]["AJB"]);
                    akad.Checked = Convert.ToBoolean(rs.Rows[i - 1]["AKAD"]);
                    tipecair.SelectedValue = rs.Rows[i - 1]["TipeCair"].ToString();
                }
            }
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            //nama
            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            //cek existing termin
            int cek = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKOM_TERM WHERE CaraBayar = '" + carabayar.SelectedValue + "' AND SalesTipe = " +Convert.ToInt32(tipe.SelectedValue)
                + " AND NoTermin != " + Nomor
                + " AND Inaktif = 0"
                );
            ;

            if (cek > 0)
            {
                x = false;
            }

            int tipesales = Db.SingleInteger("SELECT Count(ID) FROM REF_AGENT_TIPE WHERE ID = '" + tipe.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");

            if (tipesales == 0)
            {
                x = false;
                Cf.MarkError(tipe);
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

                    if (nama.Text + persencair.Text + lvlsales.SelectedValue != "")
                    {
                        bool xx = true;
                        if (!Cf.isMoney(persencair))
                        {
                            x = xx = false;
                            Cf.MarkError(persencair);
                        }
                        if (lvlsales.SelectedValue == "")
                        {
                            x = xx = false;
                            Cf.MarkError(lvlsales);
                        }
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
        private bool Save()
        {
            if (valid() && validform1 && validcair)
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
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("TerminKomisiEdit.aspx?Nomor=" + Nomor + "&done=1");
        }

        private void UpdateSkema()
        {
            string Nama = Cf.Str(nama.Text);
            string CaraBayar = carabayar.SelectedValue;
            int SalesTipe = Convert.ToInt32(tipe.SelectedValue);
            bool Inaktif = inaktif.Checked ? true : false;

            Db.Execute("EXEC spSkomTermEdit"
                + "  " + Nomor
                + ",'" + Nama + "'"
                + ",'" + Inaktif + "'"
                + ",'" + CaraBayar + "'"
                + ", " + SalesTipe
                );

            //Delete all Detail
            Db.Execute("DELETE FROM REF_SKOM_TERM_DETAIL WHERE NoTermin = " + Nomor);
        }
        private void UpdateSkemaDetail()
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
        private string Nomor
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nomor"]);
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

        void EditLoger(string Nomor)
        {
            DataTable rsHeader = Db.Rs("SELECT "
                + " NoTermin"
                + ",Nama"
                + ",CaraBayar AS [Cara Bayar]"
                + ",Inaktif AS [Status Inaktif]"
                + ",SalesTipe"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM "
                + " WHERE NoTermin = " + Nomor);

            DataTable rsSkemaBef = Db.Rs("SELECT "
                + " NoTermin"
                + ",Nama"
                + ",CaraBayar AS [Cara Bayar]"
                + ",(SELECT Tipe FROM REF_AGENT_TIPE WHERE ID = SalesTipe) AS [Tipe Sales] "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM "
                + " WHERE NoTermin = " + Nomor);
            
            DataTable rsDetailBef = Db.Rs("SELECT "
                + " CONVERT(VARCHAR, SN) "
                + " + '.  ' + Nama  + '  '"
                + " + '  ' + (SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = SalesLevel) + '  '"
                + " + '  Cair ' + CONVERT(VARCHAR, PersenCair, 1) + ' %  ' "
                + " + '  Lunas ' + CONVERT(VARCHAR, PersenLunas, 1) + ' %  ' "
                + " + '  BF ' + CONVERT(VARCHAR, PersenBF, 1) + ' %  ' "
                + " + '  DP ' + CONVERT(VARCHAR, PersenDP, 1) + ' %  ' "
                + " + '  ANG ' + CONVERT(VARCHAR, PersenANG, 1) + ' %  ' "
                + " + '  Syarat Cair (' + CONVERT(VARCHAR,TipeCair) + ')  ' "
                + " + '  PPJB (' + CONVERT(VARCHAR, PPJB) + ')  ' "
                + " + '  AJB (' + CONVERT(VARCHAR, AJB) + ')  ' "
                + " + '  AKAD (' + CONVERT(VARCHAR, AKAD) + ')  ' "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM_DETAIL WHERE NoTermin = " + Nomor);

            UpdateSkema();

            DataTable rsSkemaAft = Db.Rs("SELECT "
                + " NoTermin"
                + ",Nama"
                + ",CaraBayar AS [Cara Bayar]"
                + ",Inaktif AS [Status Inaktif]"
                + ",(SELECT Tipe FROM REF_AGENT_TIPE WHERE ID = SalesTipe) AS [Tipe Sales] "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM "
                + " WHERE NoTermin = " + Nomor);

            UpdateSkemaDetail();

            DataTable rsDetailAft = Db.Rs("SELECT "
                + " CONVERT(VARCHAR, SN) "
                + " + '.  ' + Nama  + '  '"
                + " + '  ' + (SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = SalesLevel) + '  '"
                + " + '  Cair ' + CONVERT(VARCHAR, PersenCair, 1) + ' %  ' "
                + " + '  BF ' + CONVERT(VARCHAR, PersenBF, 1) + ' %  ' "
                + " + '  DP ' + CONVERT(VARCHAR, PersenDP, 1) + ' %  ' "
                + " + '  ANG ' + CONVERT(VARCHAR, PersenANG, 1) + ' %  ' "
                + " + '  Syarat Cair (' + CONVERT(VARCHAR,TipeCair) + ')  ' "
                + " + '  PPJB (' + CONVERT(VARCHAR, PPJB) + ')  ' "
                + " + '  AJB (' + CONVERT(VARCHAR, AJB) + ')  ' "
                + " + '  AKAD (' + CONVERT(VARCHAR, AKAD) + ')  ' "
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_TERM_DETAIL WHERE NoTermin = " + Nomor);

            string Ket = Cf.LogCapture(rsHeader)
                + "<br>---EDIT SKEMA---<br>"
                + Cf.LogCompare(rsSkemaBef, rsSkemaAft)
                + "<br>---EDIT DETAIL---<br>"
                + Cf.LogList(rsDetailBef, rsDetailAft, "RUMUS UNIT");

            Db.Execute("EXEC spLogSkomTerm"
                + " 'EDIT'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + Nomor.PadLeft(5, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_SKOM_TERM_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM REF_SKOM_TERM WHERE NoTermin = " + Nomor);
            Db.Execute("UPDATE REF_SKOM_TERM_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

        }
    }
}
