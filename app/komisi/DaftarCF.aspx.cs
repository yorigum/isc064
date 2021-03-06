﻿using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI
{
    public partial class DaftarCF : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Act.Pass();
            //Act.NoCache();

            Js.ConfirmKeyword(this, keyword);
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            string strSql = "SELECT *"
                + " FROM MS_KOMISI_CF "
                + "INNER JOIN MS_KONTRAK ON MS_KOMISI_CF.NoKontrak = MS_KONTRAK.NoKontrak "
                + " WHERE NoCF + MS_KOMISI_CF.NoUnit + NamaCust + NamaAgent + MS_KOMISI_CF.NoKontrak "
                + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
                + " AND MS_KOMISI_CF.Project IN (" + Act.ProjectListSql + ")"
                + " ORDER BY NoCF";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ditemukan data closing fee dengan keyword diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoCF"] + "')\">"
                        + rs.Rows[i]["NoCF"].ToString() + "</a>";
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCust"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaSkema"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
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
    }
}
