using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class MandatorySales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
            }

            FeedBack();
            Bind();            
        }
        private void Bind()
        {
            int Cek = Db.SingleInteger("SELECT COUNT(*) FROM REF_MANDATORY WHERE Halaman = 'Sales' AND Project='" + project.SelectedValue + "'");            
            if (Cek > 0)
            {
                fill();
            }
            else
            {
                fill1();
            }
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["d"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil..."
                        ;
                project.SelectedValue = Request.QueryString["project"];
            }
        }

        protected void fill()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_MANDATORY WHERE Halaman = 'Sales' AND Project='" + project.SelectedValue + "' ORDER BY NamaKolom ");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                list.Controls.Add(tr);

                c = new HtmlTableCell();
                c.InnerHtml = (i + 1) + ".";
                c.Align = "right";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Keterangan"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                cb = new CheckBox();
                cb.ID = "cb_" + i;
                cb.Attributes.Add("data-id", rs.Rows[i]["NamaKolom"].ToString());
                cb.Checked = Convert.ToBoolean(rs.Rows[i]["HarusIsi"]);
                c.Controls.Add(cb);
                tr.Cells.Add(c);
            }
        }
        protected void fill1()
        {
            for (int i = 1; i <= 11; i++)
            {
                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                c = new HtmlTableCell();
                c.InnerHtml = (i) + ".";
                c.Align = "right";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = LibControls.Bind.KetSales(Convert.ToByte(i));
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                cb = new CheckBox();
                cb.ID = "cb_" + i;
                cb.Attributes.Add("data-id", LibControls.Bind.KolomSales(Convert.ToByte(i)));
                cb.Checked = false;
                c.Controls.Add(cb);
                tr.Cells.Add(c);

                list.Controls.Add(tr);
            }
        }

        protected void ok_Click(object sender, EventArgs e)
        {
            string Project = project.SelectedValue;

            int Cek = Db.SingleInteger("SELECT COUNT(*) FROM REF_MANDATORY Where Halaman= 'Sales' AND Project='" + Project + "'");
            if (Cek > 0)
            {
                int i = 0;
                foreach (var r in list.Controls)
                {
                    CheckBox cb = (CheckBox)list.FindControl("cb_" + i);

                    string NamaKolom = cb.Attributes["data-id"];
                    Db.Execute("UPDATE REF_MANDATORY SET HarusIsi = " + Cf.BoolToSql(cb.Checked) + " WHERE Halaman= 'Sales' AND NamaKolom = '" + NamaKolom + "' AND Project='" + Project + "'");
                    i++;
                }
            }
            else
            {
                int i = 1;
                foreach (var r in list.Controls)
                {
                    CheckBox cb = (CheckBox)list.FindControl("cb_" + i);

                    string NamaKolom = cb.Attributes["data-id"];
                    Db.Execute("INSERT INTO REF_MANDATORY (Halaman, NamaKolom, Keterangan, HarusIsi, TipeData, Project) VALUES('Sales','" + LibControls.Bind.KolomSales(Convert.ToByte(i)) + "','" + LibControls.Bind.KetSales(Convert.ToByte(i)) + "'," + Cf.BoolToSql(cb.Checked) + "," + LibControls.Bind.TipeDataSales(Convert.ToByte(i)) + ",'" + Project + "')");

                    i++;
                }
            }
            Response.Redirect("MandatorySales.aspx?d=1&project=" + Project);
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            list.Controls.Clear();
            Bind();
        }
    }
}