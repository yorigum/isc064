using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.MARKETINGJUAL
{

    public partial class KontrakComplain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Fill();
            FeedBack();
            listcomplain(ListComplain);
            tglcomplain.Text = Cf.Day(DateTime.Now);

        }
        protected void Fill()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_COMPLAIN "
                               + " INNER JOIN REF_COMPLAIN ON MS_COMPLAIN.NoComplain = REF_COMPLAIN.NoComplain"
                               + " WHERE NoKontrak='" + NoKontrak + "'");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                HtmlTableRow tr;
                HtmlTableCell c;
                TextBox tb;
                DropDownList d;
                HtmlInputButton hb;
                CheckBox cb;
                RadioButtonList rb;


                tr = new HtmlTableRow();
                tr.VAlign = "top";
                list.Controls.Add(tr);

                c = new HtmlTableCell();
                c.InnerHtml = (i + 1).ToString() + ".";
                c.ID = "pk_" + i;
                c.Attributes["title"] = rs.Rows[i]["SN"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglInput"]));
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                tb = new TextBox();
                tb.ID = "tglc_" + i;
                tb.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglComplain"]));
                tb.Width = 85;
                hb = new HtmlInputButton();
//                hb.Attributes["onclick"] = "openCalendar('tglc_" + i + "')";
                hb.Value = "...";
                c.Controls.Add(tb);
                c.Controls.Add(hb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                d = new DropDownList();
                d.ID = "ddl_" + i;
                listcomplain(d);
                c.Controls.Add(d);
                d.SelectedValue = rs.Rows[i]["NoComplain"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                tb = new TextBox();
                tb.ID = "det_" + i;
                tb.Width = 200;
                tb.Height = 100;
                tb.TextMode = TextBoxMode.MultiLine;
                tb.Text = rs.Rows[i]["Detil"].ToString();
                c.Controls.Add(tb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                tb = new TextBox();
                tb.ID = "sol_" + i;
                tb.Width = 200;
                tb.Height = 100;
                tb.TextMode = TextBoxMode.MultiLine;
                tb.Text = rs.Rows[i]["Solusi"].ToString();
                c.Controls.Add(tb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                tb = new TextBox();
                tb.ID = "tgls_" + i;
                tb.Text = (rs.Rows[i]["TglSolved"] != DBNull.Value) ? Cf.Day(Convert.ToDateTime(rs.Rows[i]["TglSolved"])) : "";
                tb.Width = 85;
                hb = new HtmlInputButton();
//                hb.Attributes["onclick"] = "openCalendar('tgls_" + i + "')";
                hb.Value = "...";
                c.Controls.Add(tb);
                c.Controls.Add(hb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                rb = new RadioButtonList();
                rb.ID = "stat_" + i;
                rb.Items.Add(new ListItem("On Progress", "2"));
                rb.Items.Add(new ListItem("Solved", "1"));
                rb.SelectedValue = rs.Rows[i]["Status"].ToString();
                rb.RepeatDirection = RepeatDirection.Vertical;
                c.Controls.Add(rb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                cb = new CheckBox();
                cb.ID = "cb_" + i;
                cb.Text = "Delete..";
                c.Controls.Add(cb);
                tr.Cells.Add(c);


                list.Controls.Add(tr);
            }
        }
        public static void listcomplain(DropDownList container)
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_COMPLAIN");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                container.Items.Add(new ListItem(rs.Rows[i]["Judul"].ToString(), rs.Rows[i]["NoComplain"].ToString()));
            }
        }
        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }
        private int baris
        {
            get
            {
                int a = Db.SingleInteger("SELECT * FROM MS_COMPLAIN "
                                       + " INNER JOIN REF_COMPLAIN ON MS_COMPLAIN.NoComplain = REF_COMPLAIN.NoComplain"
                                       + " WHERE NoKontrak='" + NoKontrak + "'");

                return a;
            }


        }
        protected void save_Click(object sender, EventArgs e)
        {
            Db.Execute("EXEC spComplain"
                    + " '" + NoKontrak + "'"
                    + ",'" + ListComplain.SelectedValue + "'"
                    + ",'" + Db.SingleInteger("SELECT NoCustomer FROM MS_KONTRAK WHERE NoKontrak='" + NoKontrak + "'") + "'"
                    + ",'" + det.Text + "'"
                    + ",'" + Convert.ToDateTime(tglcomplain.Text) + "'"
                    );

            Response.Redirect("KontrakComplain.aspx?done=1&NoKontrak=" + NoKontrak);
        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] == "1")
                {
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Simpan Complain Berhasil...";
                }
                else if (Request.QueryString["done"] == "2")
                {
                    feed2.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Update Complain Berhasil...";
                }
            }
        }

        protected bool valid()
        {
            bool x = true;
            int i = 0;
            foreach (Control tr in list.Controls)
            {
                HtmlTableCell c = (HtmlTableCell)list.FindControl("pk_" + i);
                TextBox tglc = (TextBox)list.FindControl("tglc_" + i);
                DropDownList ddl = (DropDownList)list.FindControl("ddl_" + i);
                TextBox detil = (TextBox)list.FindControl("det_" + i);
                TextBox sol = (TextBox)list.FindControl("sol_" + i);
                TextBox tgls = (TextBox)list.FindControl("tgls_" + i);
                CheckBox del = (CheckBox)list.FindControl("cb_" + i);
                RadioButtonList stat = (RadioButtonList)list.FindControl("stat_" + i);
                bool xx = true;
                if (stat.SelectedValue.ToString() == "1" && tgls.Text == "")
                {
                    xx = x = false;
                }
                if (stat.SelectedValue.ToString() == "1" && sol.Text == "")
                {
                    xx = x = false;
                }
            }
            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Apabila status solved harap isi tanggal solved.\\n"
                    + "2. Apabila status solved harap isi solusi.\\n"
                    , ""
                    );
            return x;
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                int i = 0;
                foreach (Control tr in list.Controls)
                {
                    HtmlTableCell c = (HtmlTableCell)list.FindControl("pk_" + i);
                    TextBox tglc = (TextBox)list.FindControl("tglc_" + i);
                    DropDownList ddl = (DropDownList)list.FindControl("ddl_" + i);
                    TextBox detil = (TextBox)list.FindControl("det_" + i);
                    TextBox sol = (TextBox)list.FindControl("sol_" + i);
                    TextBox tgls = (TextBox)list.FindControl("tgls_" + i);
                    CheckBox del = (CheckBox)list.FindControl("cb_" + i);
                    RadioButtonList stat = (RadioButtonList)list.FindControl("stat_" + i);

                    if (del.Checked)
                    {
                        Db.Execute("DELETE FROM MS_COMPLAIN WHERE SN=" + Convert.ToInt32(c.Attributes["title"]));
                    }
                    else
                    {
                        int status = Convert.ToInt32(stat.SelectedValue.ToString());
                        Db.Execute("UPDATE MS_COMPLAIN SET NoComplain=" + ddl.SelectedValue
                                 + " ,TglComplain='" + Convert.ToDateTime(tglc.Text) + "'"
                                 + " ,Detil='" + detil.Text.ToString() + "'"
                                 + " ,Solusi='" + sol.Text.ToString() + "'"
                                 + " ,Status=" + status
                                 + " WHERE SN=" + Convert.ToInt32(c.Attributes["title"])
                                  );
                        if (tgls.Text != "")
                        {
                            Db.Execute("UPDATE MS_COMPLAIN SET TglSolved='" + Convert.ToDateTime(tgls.Text) + "'"
                                    + " WHERE SN=" + Convert.ToInt32(c.Attributes["title"])
                                    );
                        }
                        if(status != 1)
                        {
                            Db.Execute("UPDATE MS_COMPLAIN SET TglSolved=NULL"
                                   + " WHERE SN=" + Convert.ToInt32(c.Attributes["title"])
                                   );
                        }
                    }

                    i++;
                }
                Response.Redirect("KontrakComplain.aspx?done=2&NoKontrak=" + NoKontrak);
            }

        }
    }

}