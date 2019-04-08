using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.KOMISI
{
    public partial class SkemaCFEdit : System.Web.UI.Page
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
            fillbaris2();

            if (!Page.IsPostBack)
            {
                FillTable();
                FillTable2();
            }
            FeedBack();
        }
        protected void fillbaris()
        {
            for (int i = 1; i <= Baris; i++)
            {
                tambah(i);
            }
        }
        protected void fillbaris2()
        {
            for (int i = 1; i <= Baris2; i++)
            {
                tambah2(i);
            }
        }
        protected int Baris
        {
            get { return Convert.ToInt32(Session["RowsSkom_"]); }
            set { Session["RowsSkom_"] = value; }
        }
        protected int Baris2
        {
            get { return Convert.ToInt32(Session["RowsSkom2_"]); }
            set { Session["RowsSkom2_"] = value; }
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
        protected void add2_Click(object sender, EventArgs e)
        {
            Add2(1);
        }

        protected void del2_Click(object sender, EventArgs e)
        {
            TableRow r = (TableRow)list2.FindControl("baristermin_" + (Baris2--));

            list2.Controls.Remove(r);
        }
        protected void Add2(int c)
        {
            for (short i = 1; i <= c; i++)
            {
                Baris2++;
                tambah2(Baris2);
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

            ddl = new DropDownList();
            ddl.ID = "lvlsales_" + index;
            LibMkt.ListLvlSales(ddl, Convert.ToInt32(tipe.SelectedValue), project.SelectedValue);

            c = new TableCell();
            c.Controls.Add(ddl);
            r.Cells.Add(c);

            rbl = new RadioButtonList();
            rbl.ID = "tipetarif_" + index;
            rbl.RepeatDirection = RepeatDirection.Horizontal;
            rbl.Items.Add(new ListItem("%", "%"));
            rbl.Items.Add(new ListItem("Rp", "RP"));
            rbl.SelectedIndex = 0;

            c = new TableCell();
            c.Controls.Add(rbl);
            r.Cells.Add(c);

            tb = new TextBox();
            tb.ID = "tarif_" + index;
            Js.NumberFormat2(tb);

            c = new TableCell();
            c.Controls.Add(tb);
            r.Cells.Add(c);

            rbl = new RadioButtonList();
            rbl.ID = "potongkomisi_" + index;
            rbl.RepeatDirection = RepeatDirection.Horizontal;
            rbl.Items.Add(new ListItem("Tidak", "0"));
            rbl.Items.Add(new ListItem("Ya", "1"));
            rbl.SelectedIndex = 0;

            c = new TableCell();
            c.Controls.Add(rbl);
            r.Cells.Add(c);

            list.Controls.Add(r);
        }
        protected void tambah2(int index)
        {
            TableRow r = new TableRow();
            r.ID = "baristermin_" + index;
            TableCell c = new TableCell();
            TextBox tb;
            DropDownList ddl;
            RadioButtonList rbl;

            ddl = new DropDownList();
            ddl.ID = "lvlsaless_" + index;
            LibMkt.ListLvlSales(ddl, Convert.ToInt32(tipe.SelectedValue), project.SelectedValue);

            c = new TableCell();
            c.Controls.Add(ddl);
            r.Cells.Add(c);

            rbl = new RadioButtonList();
            rbl.ID = "tipetarget_" + index;
            rbl.RepeatDirection = RepeatDirection.Horizontal;
            rbl.Items.Add(new ListItem("Unit", "UNIT"));
            rbl.Items.Add(new ListItem("Nilai", "NILAI"));
            rbl.SelectedIndex = 0;

            c = new TableCell();
            c.Controls.Add(rbl);
            r.Cells.Add(c);

            tb = new TextBox();
            tb.ID = "bawah_" + index;
            Js.NumberFormat2(tb);

            c = new TableCell();
            c.Controls.Add(tb);
            r.Cells.Add(c);

            tb = new TextBox();
            tb.ID = "atas_" + index;
            Js.NumberFormat2(tb);

            c = new TableCell();
            c.Controls.Add(tb);
            r.Cells.Add(c);

            rbl = new RadioButtonList();
            rbl.ID = "tipetariff_" + index;
            rbl.RepeatDirection = RepeatDirection.Horizontal;
            rbl.Items.Add(new ListItem("%", "%"));
            rbl.Items.Add(new ListItem("Rp", "RP"));
            rbl.SelectedIndex = 0;

            c = new TableCell();
            c.Controls.Add(rbl);
            r.Cells.Add(c);

            tb = new TextBox();
            tb.ID = "tariff_" + index;
            Js.NumberFormat2(tb);

            c = new TableCell();
            c.Controls.Add(tb);
            r.Cells.Add(c);

            rbl = new RadioButtonList();
            rbl.ID = "potongkomisii_" + index;
            rbl.RepeatDirection = RepeatDirection.Horizontal;
            rbl.Items.Add(new ListItem("Tidak", "0"));
            rbl.Items.Add(new ListItem("Ya", "1"));
            rbl.SelectedIndex = 0;

            c = new TableCell();
            c.Controls.Add(rbl);
            r.Cells.Add(c);

            list2.Controls.Add(r);
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
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_SKOM_CF_LOG&Pk=" + Nomor.PadLeft(5, '0') + "'";
            btndel.Attributes["onclick"] = "location.href='SkemaCFDel.aspx?Nomor=" + Nomor + "'";            
            Act.ProjectList(project);

            DataTable rsHeader = Db.Rs("SELECT * FROM REF_SKOM_CF WHERE NoSkema = " + Nomor);
            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nama.Text = rsHeader.Rows[0]["Nama"].ToString();
                dari.Text = Cf.Day(rsHeader.Rows[0]["Dari"]);
                sampai.Text = Cf.Day(rsHeader.Rows[0]["Sampai"]);                
                rumus.SelectedValue = rsHeader.Rows[0]["Rumus"].ToString();
                dasarhitung.SelectedValue = rsHeader.Rows[0]["DasarHitung"].ToString();
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

                if (rumus.SelectedValue == "UNIT")
                {
                    tbRumus1.Visible = true;
                    tbRumus2.Visible = false;
                }
                else
                {
                    tbRumus1.Visible = false;
                    tbRumus2.Visible = true;
                }

                int Count = Db.SingleInteger("SELECT COUNT(NoSkema) FROM REF_SKOM_CF_DETAIL WHERE NoSkema = " + Nomor);
                Baris = Count;

                int Count2 = Db.SingleInteger("SELECT COUNT(NoSkema) FROM REF_SKOM_CF_DETAIL2 WHERE NoSkema = " + Nomor);
                Baris2 = Count2;

                if (Count == 0)
                    Baris = 5;

                if (Count2 == 0)
                    Baris2 = 5;
            }

            if (rumus.SelectedValue == "UNIT")
            {
                tbRumus1.Visible = true;
                tbRumus2.Visible = false;
            }
            else
            {
                tbRumus1.Visible = false;
                tbRumus2.Visible = true;
            }
        }

        private void FillTable()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_SKOM_CF_DETAIL WHERE NoSkema = " + Nomor);
            if (rs.Rows.Count > 0)
            {
                for (int i = 1; i <= rs.Rows.Count; i++)
                {
                    DropDownList lvlsales = (DropDownList)list.FindControl("lvlsales_" + i);
                    RadioButtonList tipetarif = (RadioButtonList)list.FindControl("tipetarif_" + i);
                    TextBox tarif = (TextBox)list.FindControl("tarif_" + i);
                    RadioButtonList potongkomisi = (RadioButtonList)list.FindControl("potongkomisi_" + i);

                    lvlsales.SelectedValue = rs.Rows[i - 1]["SalesLevel"].ToString();
                    tipetarif.SelectedValue = rs.Rows[i - 1]["TipeTarif"].ToString();
                    tarif.Text = Cf.Num(rs.Rows[i - 1]["Nilai"]);
                    potongkomisi.SelectedValue = Convert.ToInt16(rs.Rows[i - 1]["PotongKomisi"]).ToString();
                }
            }
        }
        private void FillTable2()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_SKOM_CF_DETAIL2 WHERE NoSkema = " + Nomor);
            if (rs.Rows.Count > 0)
            {
                for (int i = 1; i <= rs.Rows.Count; i++)
                {
                    DropDownList lvlsales = (DropDownList)list2.FindControl("lvlsaless_" + i);
                    RadioButtonList tipetarget = (RadioButtonList)list2.FindControl("tipetarget_" + i);
                    TextBox bawah = (TextBox)list2.FindControl("bawah_" + i);
                    TextBox atas = (TextBox)list2.FindControl("atas_" + i);
                    RadioButtonList tipetarif = (RadioButtonList)list2.FindControl("tipetariff_" + i);
                    TextBox tarif = (TextBox)list2.FindControl("tariff_" + i);
                    RadioButtonList potongkomisi = (RadioButtonList)list.FindControl("potongkomisii_" + i);

                    lvlsales.SelectedValue = rs.Rows[i - 1]["SalesLevel"].ToString();
                    tipetarget.SelectedValue = rs.Rows[i - 1]["TipeTarget"].ToString();
                    bawah.Text = Cf.Num(rs.Rows[i - 1]["TargetBawah"]);
                    atas.Text = Cf.Num(rs.Rows[i - 1]["TargetAtas"]);
                    tipetarif.SelectedValue = rs.Rows[i - 1]["TipeTarif"].ToString();
                    tarif.Text = Cf.Num(rs.Rows[i - 1]["Nilai"]);
                    potongkomisi.SelectedValue = Convert.ToInt16(rs.Rows[i - 1]["PotongKomisi"]).ToString();
                }
            }
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
            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            if (!Cf.isTgl(dari.Text))
            {
                x = false;
                if (s == "") s = dari.ID;
                daric.Text = "Tanggal";
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai.Text))
            {
                x = false;
                if (s == "") s = sampai.ID;
                sampaic.Text = "Tanggal";
            }
            else
                sampaic.Text = "";

            if (tipe.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = tipe.ID;
                tipec.Text = "Pilih";
            }
            else
                tipec.Text = "";

            //cek existing periode komisi
            int cek = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKOM_CF WHERE SalesTipe = " + Convert.ToInt32(tipe.SelectedValue)
                + " AND NoSkema != " + Nomor
                + " AND Inaktif = 0"
                + " AND "
                + " ("
                + "     (CONVERT(VARCHAR,Dari,112) >= '" + Cf.Tgl112(Dari) + "' AND CONVERT(VARCHAR,Sampai,112) <= '" + Cf.Tgl112(Sampai) + "') OR"
                + "     (CONVERT(VARCHAR,Dari,112) >= '" + Cf.Tgl112(Dari) + "' AND CONVERT(VARCHAR,Dari,112) <= '" + Cf.Tgl112(Sampai) + "') OR"
                + "     (CONVERT(VARCHAR,Sampai,112) >= '" + Cf.Tgl112(Dari) + "' AND CONVERT(VARCHAR,Sampai,112) <= '" + Cf.Tgl112(Sampai) + "') OR"
                + "     ('" + Cf.Tgl112(Dari) + "' >=  CONVERT(VARCHAR,Dari,112) AND '" + Cf.Tgl112(Dari) + "' <=  CONVERT(VARCHAR,Sampai,112)) OR"
                + "     ('" + Cf.Tgl112(Sampai) + "' >=  CONVERT(VARCHAR,Dari,112) AND '" + Cf.Tgl112(Sampai) + "' <=  CONVERT(VARCHAR,Sampai,112))"
                + " )"
                );
            ;

            if (cek > 0)
            {
                x = false;
                Cf.MarkError(dari);
                Cf.MarkError(sampai);
            }


            if (!x)
            {
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Nama tidak boleh kosong.\\n"
                    + "2. Nilai harus berupa angka.\\n"
                    + "3. Tipe marketing harus dipilih.\\n"
                    + "4. Periode skema terbalik.\\n"
                    + "5. Periode skema harus berupa tanggal.\\n"
                    + "6. Periode skema sudah terdaftar.\\n"
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

                    DropDownList tipesales = (DropDownList)list.FindControl("tipesales_" + i);
                    RadioButtonList tipetarif = (RadioButtonList)list.FindControl("tipetarif_" + i);
                    TextBox tarif = (TextBox)list.FindControl("tarif_" + i);
                    RadioButtonList potongkomisi = (RadioButtonList)list.FindControl("potongkomisi_" + i);

                    if (tarif.Text != "")
                    {
                        bool xx = true;
                        if (!Cf.isMoney(tarif))
                        {
                            x = xx = false;
                            Cf.MarkError(tarif);
                        }
                        if (!xx) err.Append("Skema Komisi - Baris ke-" + i + "\\n");
                    }
                }

                if (!x) Js.Alert(Page, err.ToString(), "");
                return x;
            }
        }
        protected bool validform2
        {
            get
            {
                bool x = true;
                StringBuilder err = new StringBuilder();

                //Kumulatif / Progresif
                for (int i = 1; i <= Baris2; i++)
                {
                    if (!Response.IsClientConnected) break;

                    DropDownList tipesales = (DropDownList)list2.FindControl("tipesaless_" + i);
                    RadioButtonList tipetarget = (RadioButtonList)list2.FindControl("tipetarget_" + i);
                    TextBox bawah = (TextBox)list2.FindControl("bawah_" + i);
                    TextBox atas = (TextBox)list2.FindControl("atas_" + i);
                    RadioButtonList tipetarif = (RadioButtonList)list2.FindControl("tipetariff_" + i);
                    TextBox tarif = (TextBox)list2.FindControl("tariff_" + i);
                    RadioButtonList potongkomisi = (RadioButtonList)list.FindControl("potongkomisii_" + i);

                    if (bawah.Text != "")
                    {
                        bool xx = true;
                        if (!Cf.isMoney(bawah))
                        {
                            x = xx = false;
                            Cf.MarkError(bawah);
                        }
                        if (!xx) err.Append("Skema Komisi - Baris ke-" + i + "\\n");
                    }
                    if (atas.Text != "")
                    {
                        bool xx = true;
                        if (!Cf.isMoney(atas))
                        {
                            x = xx = false;
                            Cf.MarkError(atas);
                        }
                        if (!xx) err.Append("Skema Komisi - Baris ke-" + i + "\\n");
                    }
                    if (tarif.Text != "")
                    {
                        bool xx = true;
                        if (!Cf.isMoney(tarif))
                        {
                            x = xx = false;
                            Cf.MarkError(tarif);
                        }
                        if (!xx) err.Append("Skema Komisi - Baris ke-" + i + "\\n");
                    }
                }

                if (!x) Js.Alert(Page, err.ToString(), "");
                return x;
            }
        }
        private bool Save()
        {
            if (valid())
            {
                if (rumus.SelectedValue == "UNIT")
                {
                    if (validform1)
                    {
                        EditLoger(Nomor);
                        Js.Close(this);
                    }
                }
                else
                {
                    if (validform2)
                    {
                        EditLoger(Nomor);
                        Js.Close(this);
                    }
                }

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
            if (Save()) Response.Redirect("SkemaCFEdit.aspx?Nomor=" + Nomor + "&done=1");
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
            int SalesTipe = Convert.ToInt32(tipe.SelectedValue);
            string Rumus = rumus.SelectedValue;
            string DasarHitung = dasarhitung.SelectedValue;
            bool Inaktif = inaktif.Checked ? true : false;

            Db.Execute("EXEC spSkomCFEdit"
                + "  " + Nomor
                + ",'" + Nama + "'"
                + ",'" + Inaktif + "'"
                + ", " + SalesTipe
                + ",'" + Dari + "'"
                + ",'" + Sampai + "'"
                + ",'" + Rumus + "'"
                + ",'" + DasarHitung + "'"
                );

            //Delete all Detail
            Db.Execute("DELETE FROM REF_SKOM_CF_DETAIL WHERE NoSkema = " + Nomor);
            Db.Execute("DELETE FROM REF_SKOM_CF_DETAIL2 WHERE NoSkema = " + Nomor);
        }
        private void UpdateSkemaDetail()
        {
            int i = 1;
            foreach (var r in list.Controls)
            {
                DropDownList lvlsales = (DropDownList)list.FindControl("lvlsales_" + i);
                RadioButtonList tipetarif = (RadioButtonList)list.FindControl("tipetarif_" + i);
                TextBox tarif = (TextBox)list.FindControl("tarif_" + i);
                RadioButtonList potongkomisi = (RadioButtonList)list.FindControl("potongkomisi_" + i);

                if (tarif.Text != "")
                {
                    int SalesLevel = Convert.ToInt32(lvlsales.SelectedValue);
                    string TipeTarif = tipetarif.SelectedValue;
                    decimal Nilai = Convert.ToDecimal(tarif.Text);
                    int Potong = Convert.ToInt16(potongkomisi.SelectedValue);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spSkomCFTambah "
                        + Nomor
                        + ", " + SalesLevel
                        + ",'" + TipeTarif + "'"
                        + ", " + Nilai
                        + ",'" + Potong + "'"
                        );
                }
                i++;
            }
        }
        private void UpdateSkemaDetail2()
        {
            int i = 1;
            foreach (var r in list2.Controls)
            {
                DropDownList lvlsales = (DropDownList)list2.FindControl("lvlsaless_" + i);
                RadioButtonList tipetarget = (RadioButtonList)list2.FindControl("tipetarget_" + i);
                TextBox bawah = (TextBox)list2.FindControl("bawah_" + i);
                TextBox atas = (TextBox)list2.FindControl("atas_" + i);
                RadioButtonList tipetarif = (RadioButtonList)list2.FindControl("tipetariff_" + i);
                TextBox tarif = (TextBox)list2.FindControl("tariff_" + i);
                RadioButtonList potongkomisi = (RadioButtonList)list.FindControl("potongkomisii_" + i);

                if (bawah.Text != "" && atas.Text != "" && tarif.Text != "")
                {
                    int SalesLevel = Convert.ToInt32(lvlsales.SelectedValue);
                    string TipeTarget = tipetarget.SelectedValue;
                    decimal TargetBawah = Convert.ToDecimal(bawah.Text);
                    decimal TargetAtas = Convert.ToDecimal(atas.Text);
                    string TipeTarif = tipetarif.SelectedValue;
                    decimal Nilai = Convert.ToDecimal(tarif.Text);
                    int Potong = Convert.ToInt16(potongkomisi.SelectedValue);

                    Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spSkomCFTambah2 "
                        + Nomor
                        + ", " + SalesLevel
                        + ",'" + TipeTarget + "'"
                        + ", " + TargetBawah
                        + ", " + TargetAtas
                        + ",'" + TipeTarif + "'"
                        + ", " + Nilai
                        + ",'" + Potong + "'"
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
                + " NoSkema"
                + ",SalesTipe"
                + ",Nama"
                + ",CONVERT(varchar,Dari,106) AS [Periode Dari]"
                + ",CONVERT(varchar,Sampai,106) AS [Periode Sampai]"
                + ",Rumus AS [Rumus Komisi]"
                + ",DasarHitung AS [Dasar Perhitungan]"
                + ",Inaktif AS [Status Inaktif]"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_CF "
                + " WHERE NoSkema = " + Nomor);

            DataTable rsSkemaBef = Db.Rs("SELECT [Nama] as [Nama Komisi] "
                                  + " ,case when [Inaktif]=0 then 'Aktif' else 'Inaktif' end as [Status] "
                                  + " ,(SELECT Tipe FROM REF_AGENT_TIPE WHERE ID = SalesTipe) AS [Tipe Sales] "
                                  + " ,[Dari] "
                                  + " ,[Sampai]"
                                  + " ,[Rumus]"
                                  + " ,[DasarHitung]"
                                  + " FROM REF_SKOM_CF where NoSkema = " + Nomor);

            DataTable rsDetailBef = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,SN) + '.   ' + (SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = SalesLevel) + '   ' + CONVERT(VARCHAR,Nilai,1) + '(' + TipeTarif + ')  ' "
                    + " + '  ' + CONVERT(VARCHAR,PotongKomisi) "
                    + "FROM REF_SKOM_CF_DETAIL WHERE NoSkema = '" + Nomor + "' ORDER BY SN");

            DataTable rsDetailBef2 = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,SN) + '.   ' + (SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = SalesLevel) + '   ' + CONVERT(VARCHAR,TargetBawah,1) + ' - ' + CONVERT(VARCHAR,TargetAtas,1) + ' (' + TipeTarget + ')   ' + CONVERT(VARCHAR,Nilai,1) + '(' + TipeTarif + ')  ' "
                    + " + '  ' + CONVERT(VARCHAR,PotongKomisi) "
                    + "FROM REF_SKOM_CF_DETAIL2 WHERE NoSkema = '" + Nomor + "' ORDER BY SN");

            UpdateSkema();

            DataTable rsSkemaAft = Db.Rs("SELECT [Nama] as [Nama Komisi] "
                                  + " ,CASE WHEN [Inaktif]=0 THEN 'Aktif' ELSE 'Inaktif' END AS [Status] "
                                  + " ,(SELECT Tipe FROM REF_AGENT_TIPE WHERE ID = SalesTipe) AS [Tipe Sales] "
                                  + " ,[Dari] "
                                  + " ,[Sampai]"
                                  + " ,[Rumus]"
                                  + " ,[DasarHitung]"
                                  + " FROM REF_SKOM_CF WHERE NoSkema = " + Nomor);

            if (rumus.SelectedValue == "UNIT")
            {
                UpdateSkemaDetail();
            }
            else
            {
                UpdateSkemaDetail2();
            }

            DataTable rsDetailAft = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,SN) + '.   ' + (SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = SalesLevel) + '   ' + CONVERT(VARCHAR,Nilai,1) + '(' + TipeTarif + ')  ' "
                    + " + '  ' + CONVERT(VARCHAR,PotongKomisi) "
                    + "FROM REF_SKOM_CF_DETAIL WHERE NoSkema = '" + Nomor + "' ORDER BY SN");

            DataTable rsDetailAft2 = Db.Rs("SELECT "
                    + "CONVERT(VARCHAR,SN) + '.   ' + (SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = SalesLevel) + '   ' + CONVERT(VARCHAR,TargetBawah,1) + ' - ' + CONVERT(VARCHAR,TargetAtas,1) + ' (' + TipeTarget + ')   ' + CONVERT(VARCHAR,Nilai,1) + '(' + TipeTarif + ')  ' "
                    + " + '  ' + CONVERT(VARCHAR,PotongKomisi) "
                    + "FROM REF_SKOM_CF_DETAIL2 WHERE NoSkema = '" + Nomor + "' ORDER BY SN");

            string Ket = Cf.LogCapture(rsHeader)
                + "<br>---EDIT SKEMA---<br>"
                + Cf.LogCompare(rsSkemaBef, rsSkemaAft)
                + "<br>---EDIT RUMUS---<br>"
                + Cf.LogList(rsDetailBef, rsDetailAft, "RUMUS UNIT")
                + Cf.LogList(rsDetailBef2, rsDetailAft2, "RUMUS KUMULATIF");

            Db.Execute("EXEC spLogSkomCF"
                + " 'EDIT'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + Nomor.PadLeft(5, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_SKOM_CF_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM REF_SKOM_CF WHERE NoSkema = " + Nomor);
            Db.Execute("UPDATE REF_SKOM_CF_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

        }
        protected void param_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rumus.SelectedValue == "UNIT")
            {
                tbRumus1.Visible = true;
                tbRumus2.Visible = false;
            }
            else
            {
                tbRumus1.Visible = false;
                tbRumus2.Visible = true;
            }
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

            for (int i = 1; i <= Baris2; i++)
            {
                if (!Response.IsClientConnected) break;

                DropDownList lvlsales = (DropDownList)list.FindControl("lvlsaless_" + i);
                lvlsales.Items.Clear();
                LibMkt.ListLvlSales(lvlsales, Convert.ToInt32(tipe.SelectedValue), project.SelectedValue);
            }
        }
        protected void gantilist(object sender, EventArgs e)
        {
            LibMkt.ListTipeSales(tipe, project.SelectedValue);
        }
    }
}
