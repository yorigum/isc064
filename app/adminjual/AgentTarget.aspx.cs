using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.ADMINJUAL
{
    public partial class AgentTarget : System.Web.UI.Page
    {
        protected static DataTable rs;
	
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;

                Cf.BindBulan(bln);
                Cf.BindTahun(thn);

                bln.SelectedValue = DateTime.Now.Month.ToString();
                thn.SelectedValue = DateTime.Now.Year.ToString();

                if (Request.QueryString["NoKontrak"] != null)
                {
                    dariReminder.Checked = true;
                    LoadKontrak();
                }
                else
                {
                    frm.Visible = false;
                }
            }
            Fill();
            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses setting target agent?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        //+ "<a href=\"javascript:popEditKontrak('" + Request.QueryString["done"] + "')\">"
                        + "Target Sales Berhasil..."
                        //+ "</a>"
                        ;
            }
        }

        private bool valid()
        {
            bool x = true;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Sales tersebut tidak terdaftar.\\n"
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        private void LoadKontrak()
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                Fill();

                //Js.Focus(this, save);
                Js.Confirm(this, "Lanjutkan proses pencatatan target agent?");
            }
            else
            {
                backbtn.Visible = true;
                //Js.Focus(this, nokontrak);
                frm.Visible = false;
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                //Fill();

                //Js.Focus(this, save);
                Js.Confirm(this, "Lanjutkan proses pencatatan target agent?");
            }
        }

        private void Fill()
        {
            string strSql = "SELECT"
                + " NoAgent"
                + ",Nama"
                + " FROM MS_Agent"
                + " WHERE"
                + " Status = 'A'"
                + " ORDER BY NoAgent";

            //DataTable 
            rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Sales Belum di input.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                TextBox t;

                l = new Label();
                l.Text = "<tr>"
                    + "<td><a show-modal='#ModalPopUp' modal-title='Edit Agent' modal-url='AgentEdit.aspx?NoAgent="+ rs.Rows[i]["NoAgent"] + "'>" + rs.Rows[i]["NoAgent"].ToString().PadLeft(5, '0') + "</a></td>"
                    + "<td>" + rs.Rows[i]["Nama"] + "</td>"
                    + "<td>";
                list.Controls.Add(l);

                decimal trg = Db.SingleDecimal("SELECT ISNULL(SUM(Target),0) FROM MS_AGENT_TARGET WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "' AND Bulan = '"+ bln.SelectedValue +"' AND Tahun = '"+ thn.SelectedValue +"'");
                t = new TextBox();
                t.ID = "target_" + i;
                t.Width = 100;
                t.CssClass = "txt_num";
                t.Text = Cf.Num(trg);
                t.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                t.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                t.Attributes["onblur"] = "CalcBlur(this);";
                t.TabIndex = 1000;
                list.Controls.Add(t);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);


            }
        }

        private bool datavalid()
        {
            string s = "";
            bool x = true;

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                TextBox t = (TextBox)list.FindControl("target_" + i);

                try
                {
                    decimal z = Convert.ToDecimal(t.Text);
                }
                catch
                {
                    s = "target_" + i;
                    x = false;
                    break;
                }

                decimal tgt = Convert.ToDecimal(t.Text);

                if (tgt < 0)
                {
                    s = "target_" + i;
                    x = false;
                    break;
                }
            }

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Target harus berupa angka."
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (datavalid())
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TextBox tar = (TextBox)list.FindControl("target_" + i);

                    decimal target = 0;
                    try
                    {
                        target = Convert.ToDecimal(tar.Text);
                    }
                    catch { };
                    
                    bool exist = Db.SingleInteger("SELECT Count(*) FROM MS_AGENT_TARGET WHERE NoAgent = '"+ rs.Rows[i]["NoAgent"] +"' AND Bulan = '"+ bln.SelectedValue +"' AND Tahun = '"+ thn.SelectedValue +"'") > 0;
                    if (!exist)
                        Db.Execute("EXEC spAgentTarget "
                            + "'" + rs.Rows[i]["NoAgent"] + "'"
                            + ",'" + Convert.ToInt32(bln.SelectedValue) + "'"
                            + ",'" + Convert.ToInt32(thn.SelectedValue) + "'"
                            + "," + target
                            );
                    else
                        Db.Execute("UPDATE MS_AGENT_TARGET SET Target = '"+ target +"' WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "' AND Bulan = '" + bln.SelectedValue + "' AND Tahun = '" + thn.SelectedValue + "'");
                }
                Response.Redirect("AgentTarget.aspx?done=");
            }
        }
    }
}
