using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
    public partial class PL : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);

                if (Request.QueryString["f"] == null)
                {
                    frm.Visible = false;
                    FillHeader();
                }
                else
                {
                    Act.CekInt(Request.QueryString["f"]);
                    pilih.Visible = false;
                }
            }

            Js.Confirm(this, "Lanjutkan proses set price list unit properti?");
            FeedBack();

            if (frm.Visible) Fill();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Set Price List Berhasil...";
                project.SelectedValue = Request.QueryString["project"];
                FillHeader();
            }
        }

        private void FillHeader()
        {
            int c = 0;

            c = Db.SingleInteger("SELECT COUNT(*) FROM MS_UNIT WHERE FlagSPL = 0 AND Status = 'A' AND Project = '" + project.SelectedValue + "'");
            //f0.InnerHtml = f0.InnerHtml + " (" + c + ")";
            pending.InnerText = "(" + c + ")";

            c = Db.SingleInteger("SELECT COUNT(*) FROM MS_UNIT WHERE FlagSPL = 1 AND Status = 'A' AND Project = '" + project.SelectedValue + "'");
            //f1.InnerHtml = f1.InnerHtml + " (" + c + ")";
            approve.InnerText = "(" + c + ")";

            c = Db.SingleInteger("SELECT COUNT(*) FROM MS_UNIT WHERE FlagSPL = 2 AND Status = 'A' AND Project = '" + project.SelectedValue + "'");
            //f2.InnerHtml = f2.InnerHtml + " (" + c + ")";
            edit.InnerText = "(" + c + ")";

            f0.HRef = "?f=0&project='" + project.SelectedValue + "'";
            f1.HRef = "?f=1&project='" + project.SelectedValue + "'";
            f2.HRef = "?f=2&project='" + project.SelectedValue + "'";
        }

        private void Fill()
        {
            if (Request.QueryString["f"] == "0")
                //judul.InnerHtml = f0.InnerHtml;            
                judul.InnerText = "Price List - Pending " + Project;
            if (Request.QueryString["f"] == "1")
                judul.InnerText = "Price List - Approved " + Project;
            if (Request.QueryString["f"] == "2")
                judul.InnerText = "Price List - Edit Unit " + Project;

            string flag = " AND FlagSPL = " + Request.QueryString["f"];

            string strSql = "SELECT"
                + " MS_UNIT.NoStock"
                + ",NoUnit"
                + ",Luas"
                + ",LuasSG"
                + ",LuasNett"
                + ",LuasLebih"
                + ",MS_UNIT.PriceListMin"
                + ",MS_UNIT.PriceList"
                + ",MS_UNIT.PricelistKavling"
                + ",MS_PRICELIST_HISTORY.Periode"
                + ",BiayaBPHTB"
                + ",BiayaSurat"
                + ",BiayaProses"
                + ",BiayaLainLain"
                + ",TglInput"
                + ",TglPriceList"
                + ",TambahanHargaGimmick "
                + ",TambahanHargaLainLain "
                + " FROM MS_UNIT"
                + " INNER JOIN MS_PRICELIST_HISTORY ON MS_UNIT.NoStock = MS_PRICELIST_HISTORY.NoStock"
                + " WHERE"
                + " Status = 'A'"
                + " AND Project = '" + Project + "'"
                + flag
                + " ORDER BY NoStock";

            rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Daftar unit untuk kondisi price list yang dipilih tidak ada.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;
                TextBox tgl;
                CheckBox cb;
                HtmlInputButton btn;

                l = new Label();
                l.Text = "<tr>"
                    + "<td><a show-modal='#ModalPopUp' modal-title='Edit Unit' modal-url='UnitEdit.aspx?NoStock=" + rs.Rows[i]["NoStock"] + "'>" + rs.Rows[i]["NoStock"] + "</a></td>"
                    + "<td>" + rs.Rows[i]["NoUnit"] + "</td>"
                    + "<td align='right'>" + Cf.Num(rs.Rows[i]["Luas"]) + "</td>"
                    + "<td align='right'>" + Cf.Num(rs.Rows[i]["LuasSG"]) + "</td>"
                    + "<td align='right'>" + Cf.Num(rs.Rows[i]["LuasLebih"]) + "</td>"
                    + "<td align='right'>" + Cf.Num(rs.Rows[i]["LuasNett"]) + "</td>"                    
                    + "<td>";
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "min_" + i;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Text = Cf.Num(rs.Rows[i]["PriceListMin"]);
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                t.TabIndex = 1000;
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "pl_" + i;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Text = Cf.Num(rs.Rows[i]["PriceList"]);
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                t.TabIndex = 1000;
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                cb = new CheckBox();
                cb.ID = "cbrumah_" + i;
                cb.Text = "&nbsp";
                cb.Attributes["onclick"] = "nonaktif(cbrumah_" + i + ",cbkavling_" + i + ")";
                list.Controls.Add(cb);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "plkav_" + i;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Text = Cf.Num(rs.Rows[i]["PricelistKavling"]);
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                t.TabIndex = 1000;
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                cb = new CheckBox();
                cb.ID = "cbkavling_" + i;
                cb.Attributes["onclick"] = "nonaktif(cbrumah_" + i + ",cbkavling_" + i + ")";
                cb.Text = "&nbsp";
                list.Controls.Add(cb);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "bphtb_" + i;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Text = Cf.Num(rs.Rows[i]["BiayaBPHTB"]);
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                t.TabIndex = 1000;
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "bsurat_" + i;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Text = Cf.Num(rs.Rows[i]["BiayaSurat"]);
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                t.TabIndex = 1000;
                list.Controls.Add(t);
                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "bproses_" + i;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Text = Cf.Num(rs.Rows[i]["BiayaProses"]);
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                t.TabIndex = 1000;
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                t = new TextBox();
                t.ID = "blain_" + i;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Text = Cf.Num(rs.Rows[i]["BiayaLainLain"]);
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                t.TabIndex = 1000;
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                l = new Label();
                //l.ID = "tgl_" + i;
                l.Width = 75;
                //tgl.CssClass = "tgl txt_center";
                l.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["Periode"]));
                list.Controls.Add(l);

                //if (rs.Rows[i]["TglPriceList"] == DBNull.Value)
                //    tgl.Text = Cf.Day(DateTime.Now);
                //else
                //tgl.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["Periode"]));
                //tgl.Attributes["style"] = "font:8pt; readonly:true";
                //list.Controls.Add(tgl);

                //l = new Label();
                //l.Text = "<i class='fa fa-calendar'></i>";
                //l.CssClass = "btn btn-cal";
                //l.Attributes.Add("for", "tgl_" + i);
                //list.Controls.Add(l);

                //t = new TextBox();
                //t.ID = "gimmick_" + i;
                //t.Width = 100;
                //t.CssClass = "txt_num";
                //t.Text = Cf.Num(rs.Rows[i]["TambahanHargaGimmick"]);
                //t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                //t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                //t.Attributes["onblur"] = "CalcBlur(this);";
                //t.TabIndex = 1000;
                //list.Controls.Add(t);

                //l = new Label();
                //l.Text = "</td>";
                //list.Controls.Add(l);

                //l = new Label();
                //l.Text = "<td>";
                //list.Controls.Add(l);

                //t = new TextBox();
                //t.ID = "lainlain_" + i;
                //t.Width = 100;
                //t.CssClass = "txt_num";
                //t.Text = Cf.Num(rs.Rows[i]["TambahanHargaLainLain"]);
                //t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                //t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                //t.Attributes["onblur"] = "CalcBlur(this);";
                //t.TabIndex = 1000;
                //list.Controls.Add(t);

                l = new Label();
                l.Text = "</td>";

                l = new Label();
                l.Text = "<td>";
                list.Controls.Add(l);

                tgl = new TextBox();
                tgl.ID = "tgl_" + i;
                tgl.Width = 75;
                tgl.CssClass = "tgl txt_center";
                //if (rs.Rows[i]["Periode"] == DBNull.Value)
                //    tgl.Text = Cf.Day(DateTime.Now);
                //else
                //tgl.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglPriceList"]))
                tgl.Text = Cf.Day(DateTime.Now); ;
                tgl.Attributes["style"] = "font:8pt";
                list.Controls.Add(tgl);

                l = new Label();
                l.Text = "<i class='fa fa-calendar'></i>";
                l.CssClass = "btn btn-cal";
                l.Attributes.Add("for", "tgl_" + i);
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td></tr>";
                list.Controls.Add(l);

            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TextBox min = (TextBox)list.FindControl("min_" + i);
                TextBox t = (TextBox)list.FindControl("pl_" + i);

                try
                {
                    decimal z = Convert.ToDecimal(min.Text);
                }
                catch
                {
                    s = "min_" + i;
                    x = false;
                    break;
                }

                try
                {
                    decimal z = Convert.ToDecimal(t.Text);
                }
                catch
                {
                    s = "pl_" + i;
                    x = false;
                    break;
                }

                decimal plmin = Convert.ToDecimal(min.Text);
                decimal pl = Convert.ToDecimal(t.Text);

                if (plmin < 0)
                {
                    s = "min_" + i;
                    x = false;
                    break;
                }
                if (pl < 0 || pl < plmin)
                {
                    s = "pl_" + i;
                    x = false;
                    break;
                }
            }

            if (!x)
                Js.Alert(this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Harga tidak boleh kosong.\\n"
                    + "2. Harga harus berupa angka dan positif.\\n"
                    + "3. Price List harus lebih besar atau sama dengan harga minimum.\\n"
                    , "document.getElementById('" + s + "').select();"
                    + "document.getElementById('" + s + "').focus();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                System.Text.StringBuilder log = new System.Text.StringBuilder();
                int c = 0;

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TextBox min = (TextBox)list.FindControl("min_" + i);
                    TextBox t = (TextBox)list.FindControl("pl_" + i);
                    TextBox tgl = (TextBox)list.FindControl("tgl_" + i);
                    TextBox gimmick = (TextBox)list.FindControl("gimmick_" + i);
                    TextBox kavling = (TextBox)list.FindControl("plkav_" + i);
                    TextBox bphtb = (TextBox)list.FindControl("bphtb_" + i);
                    TextBox bsurat = (TextBox)list.FindControl("bsurat_" + i);
                    TextBox bproses = (TextBox)list.FindControl("bproses_" + i);
                    TextBox blain = (TextBox)list.FindControl("blain_" + i);
                    TextBox lainlain = (TextBox)list.FindControl("lainlain_" + i);
                    CheckBox cbrumah = (CheckBox)list.FindControl("cbrumah_" + i);
                    CheckBox cbkavling = (CheckBox)list.FindControl("cbkavling_" + i);

                    decimal plmin = Convert.ToDecimal(min.Text);
                    decimal plkav = Convert.ToDecimal(kavling.Text);
                    decimal BPHTB = Convert.ToDecimal(bphtb.Text);
                    decimal BiayaSurat = Convert.ToDecimal(bsurat.Text);
                    decimal BiayaProses = Convert.ToDecimal(bproses.Text);
                    decimal BiayaLain = Convert.ToDecimal(blain.Text);
                    decimal pl = Convert.ToDecimal(t.Text);
                    DateTime Tgl = Convert.ToDateTime(tgl.Text);
                    decimal BiayaTambahanGimmick = 0;
                    decimal BiayaTambahanLainLain = 0;
                    int defaultpl = 0;
                    if (cbrumah.Checked)
                    {
                        defaultpl = 1;
                    }
                    else if (cbkavling.Checked)
                    {
                        defaultpl = 2;
                    }
                    

                    if (pl != 0)
                    {
                        Db.Execute("EXEC spUnitPriceList "
                            + "'" + rs.Rows[i]["NoStock"] + "'"
                            + "," + plmin
                            + "," + pl
                            );
                    }

                    Db.Execute("UPDATE MS_UNIT SET TambahanHargaGimmick = '" + BiayaTambahanGimmick + "'"
                            + ", TambahanHargaLainLain = '" + BiayaTambahanLainLain + "'"
                            + ", TglPriceList = '" + Tgl + "'"
                            + ", PricelistKavling = " + plkav
                            + ", BiayaBPHTB = " + BPHTB
                            + ", BiayaSurat = " + BiayaSurat
                            + ", BiayaProses = " + BiayaProses
                            + ", BiayaLainLain = " + BiayaLain
                            + ", DefaultPL = " + defaultpl
                            + "  WHERE NoStock = '" + rs.Rows[i]["NoStock"] + "'");

                    if (Convert.ToDecimal(rs.Rows[i]["PriceListMin"]) != plmin
                        || Convert.ToDecimal(rs.Rows[i]["PriceList"]) != pl
                        )
                    {
                        Db.Execute("EXEC spPriceListHistory"
                            + " '" + rs.Rows[i]["NoStock"] + "'"
                            + ", " + plmin
                            + ", " + pl
                            + ", " + plkav
                            + ", '" + Tgl + "'"
                            );

                        c++;
                        log.Append(Cf.Str(rs.Rows[i]["NoStock"]) + " (" + Cf.Str(rs.Rows[i]["NoUnit"]) + ") "
                            + " " + Cf.Num(rs.Rows[i]["PriceListMin"]) + " / " + Cf.Num(rs.Rows[i]["PriceList"])
                            + " --> " + Cf.Num(plmin) + " / " + Cf.Num(pl)
                            + "<br>"
                            );
                    }
                }

                if (c != 0)
                {
                    string Ket = "Jumlah Unit : " + c
                        + "<br><br>=============DATA :<br>"
                        + log.ToString()
                        ;

                    Db.Execute("EXEC spLogUnit "
                        + " 'SPL'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",''"
                        );
                }

                Response.Redirect("PL.aspx?done=" + c + "&project=" + Project);
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
            FillHeader();
        }

        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
		        
		protected void cbrumah_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                CheckBox cbrumah = (CheckBox)list.FindControl("cbrumah_" + i);
                cbrumah.Checked = true;                
            }
        }
    }
}
