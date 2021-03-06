﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP
{

    public partial class SetupLaunching : System.Web.UI.Page
    {
        protected string GantiUnit { get { return "ApprovGantiUnit" + project.SelectedValue; } }
        protected string GantiNama { get { return "ApprovGantiNama" + project.SelectedValue; } }
        protected string Batal { get { return "ApprovBatal" + project.SelectedValue; } }
        protected string Adjustment { get { return "ApprovAdjustment" + project.SelectedValue; } }
        protected string Reschedule { get { return "ApprovReschedule" + project.SelectedValue; } }
        protected string CustomTagihan { get { return "ApprovCustomTagihan" + project.SelectedValue; } }
        protected string Diskon { get { return "ApprovDiskon" + project.SelectedValue; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                fill();
            }
            Bind();
            FeedBack();
        }

        private void Bind()
        {
            int Cek = Db.SingleInteger("SELECT COUNT(*) FROM REF_MANDATORY WHERE Halaman = 'Customer' AND Project='" + project.SelectedValue + "'");
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
            DataTable rs = Db.Rs("SELECT * FROM REF_MANDATORY WHERE Halaman = 'Customer' AND Project='" + project.SelectedValue + "' ORDER BY NamaKolom");
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
            for (int i = 1; i <= 38; i++)
            {
                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                c = new HtmlTableCell();
                c.InnerHtml = (i) + ".";
                c.Align = "right";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = LibControls.Bind.KetCustomer(Convert.ToByte(i));
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                cb = new CheckBox();
                cb.ID = "cb_" + i;
                cb.Attributes.Add("data-id", LibControls.Bind.KolomCustomer(Convert.ToByte(i)));
                cb.Checked = false;
                c.Controls.Add(cb);
                tr.Cells.Add(c);

                list.Controls.Add(tr);
            }
        }
        protected void ok_Click(object sender, EventArgs e)
        {
            string Project = project.SelectedValue;

            int Cek = Db.SingleInteger("SELECT COUNT(*) FROM REF_MANDATORY Where Halaman= 'Customer' AND Project='" + Project + "'");
            if (Cek > 0)
            {
                int i = 0;
                foreach (var r in list.Controls)
                {
                    CheckBox cb = (CheckBox)list.FindControl("cb_" + i);

                    string NamaKolom = cb.Attributes["data-id"];
                    Db.Execute("UPDATE REF_MANDATORY SET HarusIsi = " + Cf.BoolToSql(cb.Checked) + "  WHERE Halaman= 'Customer' AND NamaKolom = '" + NamaKolom + "' AND Project='" + Project + "'");
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
                    Db.Execute("INSERT INTO REF_MANDATORY (Halaman, NamaKolom, Keterangan, HarusIsi, TipeData, Project) VALUES('Customer','" + LibControls.Bind.KolomCustomer(Convert.ToByte(i)) + "','" + LibControls.Bind.KetCustomer(Convert.ToByte(i)) + "'," + Cf.BoolToSql(cb.Checked) + "," + LibControls.Bind.TipeDataCustomer(Convert.ToByte(i)) + ",'" + Project + "')");

                    i++;
                }
            }
            Response.Redirect("MandatoryCustomer.aspx?d=1&project=" + Project);
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            list.Controls.Clear();
            Bind();
        }
    }
}