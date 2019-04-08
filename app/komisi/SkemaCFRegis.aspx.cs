﻿using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.KOMISI
{
    public partial class SkemaCFRegis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Bind();

                Baris = 5;
                Baris2 = 5;

                tbRumus1.Visible = true;
                tbRumus2.Visible = false;
            }

            fillbaris();
            fillbaris2();

            FeedBack();
        }
        private void Bind()
        {
            Js.Focus(this, nama);

            dari.Text = Cf.Day(Cf.AwalBulan(DateTime.Now.Month, DateTime.Now.Year));
            sampai.Text = Cf.Day(Cf.AkhirBulan(DateTime.Now.Month, DateTime.Now.Year));

            LibMkt.ListTipeSales(tipe, project.SelectedValue);
            Act.ProjectList(project);
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
            get { return Convert.ToInt32(Session["RowsSkom"]); }
            set { Session["RowsSkom"] = value; }
        }
        protected int Baris2
        {
            get { return Convert.ToInt32(Session["RowsSkomTermin"]); }
            set { Session["RowsSkomTermin"] = value; }
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
        protected void add2_Click(object sender, EventArgs e)
        {
            Add2(1);
        }

        protected void del2_Click(object sender, EventArgs e)
        {
            TableRow r = (TableRow)list2.FindControl("baristermin_" + (--Baris2));

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
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popSkomTerm('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
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
            Label l;

            ddl = new DropDownList();
            ddl.ID = "lvlsales_" + index;
            if(tipe.SelectedIndex == 0)
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

            c = new TableCell();
            l = new Label();
            l.Text = "<i class='fa fa-trash'></i>";
            l.CssClass = "btn btn-cal";
            StringBuilder x = new StringBuilder();
            x.Append("ClearSkema1('tarif_" + index + "');");
            l.Attributes["onclick"] = x.ToString();
            c.Controls.Add(l);
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
            Label l;

            ddl = new DropDownList();
            ddl.ID = "lvlsaless_" + index;

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

            c = new TableCell();
            l = new Label();
            l.Text = "<i class='fa fa-trash'></i>";
            l.CssClass = "btn btn-cal";
            StringBuilder x = new StringBuilder();
            x.Append("ClearSkema2('bawah_" + index + "','atas_" + index + "','tariff_" + index + "');");
            l.Attributes["onclick"] = x.ToString();
            c.Controls.Add(l);
            r.Cells.Add(c);

            list2.Controls.Add(r);
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
                tipec.Text = "Pilih Project & Tipe";
            }

            if (project.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = project.ID;
                tipec.Text = "Pilih Project & Tipe";
            }

            if (Cf.isTgl(dari) && Cf.isTgl(sampai))
            {
                DateTime Dari = Convert.ToDateTime(dari.Text);
                DateTime Sampai = Convert.ToDateTime(sampai.Text);
                if (Dari > Sampai)
                {
                    DateTime x1 = Sampai;
                    Sampai = Dari;
                    Dari = x1;
                }

                //cek existing periode komisi
                int cek = 0;
                if (tipe.SelectedIndex != 0)
                {
                    cek = Db.SingleInteger("SELECT COUNT(*) FROM REF_SKOM_CF WHERE SalesTipe = " + Convert.ToInt32(tipe.SelectedValue)
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
                }                

                if (cek > 0)
                {
                    x = false;
                    Cf.MarkError(dari);
                    Cf.MarkError(sampai);
                }
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
            else
            {
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
                        if (!xx) err.Append("Skema CF - Baris ke-" + i + "\\n");
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
                        if (!xx) err.Append("Skema CF - Baris ke-" + i + "\\n");
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
        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                if (rumus.SelectedValue == "UNIT")
                {
                    if (validform1)
                        Save();
                }
                else
                {
                    if (validform2)
                        Save();
                }
            }
        }
        protected void Save()
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
            int SalesTipe = Convert.ToInt32(tipe.SelectedValue);
            string Rumus = rumus.SelectedValue;
            string DasarHitung = dasarhitung.SelectedValue;

            Db.Execute("EXEC spSkomCFBaru"
                + " '" + Nama + "'"
                + ", " + SalesTipe
                + ",'" + Dari + "'"
                + ",'" + Sampai + "'"
                + ",'" + Rumus + "'"
                + ",'" + DasarHitung + "'"
                );

            int Nomor = Db.SingleInteger("SELECT TOP 1 NoSkema FROM REF_SKOM_CF"
                + " ORDER BY NoSkema DESC"
                );

            Db.Execute("UPDATE REF_SKOM_CF SET "
               + " Project = '" + project.SelectedValue + "'"
               + " WHERE NoSkema = '" + Nomor + "'");

            if (Rumus == "UNIT")
            {
                SaveRumus(Nomor);
            }
            else
            {
                SaveRumus2(Nomor);
            }

            DataTable rsHeader = Db.Rs("SELECT "
                + " NoSkema"
                + ",SalesTipe"
                + ",Nama"
                + ",Project"
                + ",CONVERT(varchar,Dari,106) AS [Periode Dari]"
                + ",CONVERT(varchar,Sampai,106) AS [Periode Sampai]"
                + ",Rumus AS [Rumus Komisi]"
                + ",DasarHitung AS [Dasar Perhitungan]"
                + ",Inaktif AS [Status Inaktif]"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_CF "
                + " WHERE NoSkema = " + Nomor);

            DataTable rsDetail = Db.Rs("SELECT "
                + " CONVERT(VARCHAR, SN) "
                + " + '.  ' + (SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = SalesLevel)  + '  '"
                + " + '  ' + CONVERT(VARCHAR, Nilai, 1) + TipeTarif "
                + " + '  ' + CONVERT(VARCHAR,PotongKomisi)"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_CF_DETAIL WHERE NoSkema = " + Nomor);

            DataTable rsDetail2 = Db.Rs("SELECT "
                + " CONVERT(VARCHAR, SN) "
                + " + '.  ' + (SELECT Nama FROM REF_AGENT_LEVEL WHERE LevelID = SalesLevel)  + '  '"
                + " + '  ' + CONVERT(VARCHAR, TargetBawah, 1) + ' - ' + CONVERT(VARCHAR, TargetAtas, 1)  + TipeTarget + '  '"
                + " + '  ' + CONVERT(VARCHAR, Nilai, 1) + TipeTarif "
                + " + '  ' + CONVERT(VARCHAR,PotongKomisi)"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_SKOM_CF_DETAIL2 WHERE NoSkema = " + Nomor);

            string Ket = Cf.LogCapture(rsHeader)
                + Cf.LogList(rsDetail, "RUMUS UNIT")
                + Cf.LogList(rsDetail2, "RUMUS KUMULATIF");

            Db.Execute("EXEC spLogSkomCF"
                + " 'DAFTAR'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + Nomor.ToString().PadLeft(5, '0') + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_SKOM_CF_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE REF_SKOM_CF_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

            Response.Redirect("SkemaCF.aspx?done=" + Nomor);
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

        void SaveRumus(int Nomor)
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

        void SaveRumus2(int Nomor)
        {
            int i = 1;
            foreach (var r in list.Controls)
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
                if (tipe.SelectedIndex != 0)
                {
                    LibMkt.ListLvlSales(lvlsales, Convert.ToInt32(tipe.SelectedValue), project.SelectedValue);
                }
            }

            for (int i = 1; i <= Baris2; i++)
            {
                if (!Response.IsClientConnected) break;

                DropDownList lvlsales = (DropDownList)list.FindControl("lvlsaless_" + i);
                lvlsales.Items.Clear();
                if (tipe.SelectedIndex != 0)
                {
                    LibMkt.ListLvlSales(lvlsales, Convert.ToInt32(tipe.SelectedValue), project.SelectedValue);
                }
            }
        }
        protected void gantilist(object sender, EventArgs e)
        {
            LibMkt.ListTipeSales(tipe, project.SelectedValue);
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipe.Items.Clear();
            tipe.Items.Add(new ListItem("Tipe Marketing : "));
            LibMkt.ListTipeSales(tipe, project.SelectedValue);
            for (int i = 1; i <= Baris; i++)
            {
                if (!Response.IsClientConnected) break;

                DropDownList lvlsales = (DropDownList)list.FindControl("lvlsales_" + i);
                lvlsales.Items.Clear();
                if (tipe.SelectedIndex != 0)
                {
                    LibMkt.ListLvlSales(lvlsales, 0, project.SelectedValue);
                }
            }

            for (int i = 1; i <= Baris2; i++)
            {
                if (!Response.IsClientConnected) break;

                DropDownList lvlsaless = (DropDownList)list.FindControl("lvlsaless_" + i);
                lvlsaless.Items.Clear();
                if (tipe.SelectedIndex != 0)
                {
                    LibMkt.ListLvlSales(lvlsaless, 0, project.SelectedValue);
                }
            }

        }
    }
}
