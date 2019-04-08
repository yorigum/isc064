using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
    public partial class MKP : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Head();
            Bind();

            //Initialize awal
            if (!Page.IsPostBack)
                Fill();
        }

        private void Head()
        {
            string strSql = "SELECT * FROM REF_PROJECT ORDER BY SN";
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                HtmlTableCell c = new HtmlTableCell("th");
                c.InnerHtml = rs.Rows[i]["Nama"].ToString();
                c.ID = rs.Rows[i]["Project"].ToString();
                c.Attributes["onclick"] = "location.href='?SL=" + rs.Rows[i]["Project"] + "'";
                c.Attributes["onmouseover"] = "this.style.color='blue'";
                c.Attributes["onmouseout"] = "this.style.color=''";
                head.Cells.Add(c);
            }
        }

        private void Bind()
        {
            string addq = "";
            //if (Request.QueryString["SL"] != null)
            //    addq = " AND SecLevel = '" + Request.QueryString["SL"] + "'";

            string strSql = "SELECT "
                + " UserID"
                + ",Nama"
                + " FROM USERNAME"
                + " WHERE Status = 'A'"
                + addq
                + " ORDER BY Nama, UserID";

            rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "No username with ACTIVE status.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                Label l;
                CheckBox ch;

                l = new Label();
                l.Text = "<tr valign='top'>"
                    + "<td style='border-bottom:1px dashed silver'>"
                    + rs.Rows[i]["UserID"]
                    + "</td>"
                    + "<td style='border-bottom:1px dashed silver'>"
                    + "<a href=\"javascript:popEditUser('" + rs.Rows[i]["UserID"] + "')\">"
                    + rs.Rows[i]["Nama"]
                    + "</a></td>";
                list.Controls.Add(l);

                for (int j = 2; j <= head.Cells.Count - 1; j++)
                {
                    l = new Label();
                    l.Text = "<td style='border-bottom:1px dashed silver'>";
                    list.Controls.Add(l);

                    string s = head.Cells[j].ID;

                    ch = new CheckBox();
                    ch.ID = s + i;
                    ch.ToolTip = ch.ID;
                    if (Act.AksesPers(Act.NamaPT(s), rs.Rows[i]["UserID"].ToString())) // hanya menampilkan project yg sudah di matrikulasi pt nya untuk user tsb
                        list.Controls.Add(ch);

                    l = new Label();
                    l.Text = "</td>";
                    list.Controls.Add(l);
                }

                l = new Label();
                l.Text = "</tr>";
                list.Controls.Add(l);
            }
        }

        private void Fill()
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                for (int j = 2; j <= head.Cells.Count - 1; j++)
                {
                    string Project = head.Cells[j].ID;
                    try
                    {
                        bool Granted = Db.SingleBool("SELECT Granted FROM PROJECTSEC WHERE Project = '" + Project + "' AND USERID = '" + rs.Rows[i]["UserID"] + "'");
                        CheckBox r = (CheckBox)list.FindControl(Project + i);
                        if (Granted) r.Checked = true;
                    }
                    catch { }

                }
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            Db.Execute("DELETE FROM PROJECTSEC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                string UserID = rs.Rows[i]["UserID"].ToString();
                for (int j = 2; j <= head.Cells.Count - 1; j++)
                {
                    string Project = head.Cells[j].ID;
                    if (Act.AksesPers(Act.NamaPT(Project), rs.Rows[i]["UserID"].ToString()))
                    {
                        string Granted = "0";
                        string NamaProject = head.Cells[j].InnerHtml;
                        CheckBox r = (CheckBox)list.FindControl(Project + i);
                        if (r.Checked) Granted = "1";

                        Db.Execute("EXEC spUserProject"
                            + " '" + Project + "'"
                            + ",'" + UserID + "'"
                            + ",'" + Granted + "'"
                            + ",'" + NamaProject + "'"
                            );
                    }
                }

                //DataTable rsDetail = Db.Rs("SELECT "
                //    + " UserID AS [Kode / Username]"
                //    + ",Nama AS [Nama Lengkap]"
                //    + " FROM USERNAME WHERE UserID = '" + UserID + "'");

                //DataTable rsBef = Db.Rs("SELECT "
                //    + " SecLevel AS [Security Level]"
                //    + " FROM USERNAME WHERE UserID = '" + UserID + "'");

                //DataTable rsAft = Db.Rs("SELECT "
                //    + " SecLevel AS [Security Level]"
                //    + " FROM USERNAME WHERE UserID = '" + UserID + "'");

                //if (seclevel != rs.Rows[i]["SecLevel"].ToString())
                //{
                //    string Ket = Cf.LogCapture(rsDetail)
                //        + Cf.LogCompare(rsBef, rsAft);

                //    Db.Execute("EXEC spLogUsername "
                //        + " 'MKA'"
                //        + ",'" + Act.UserID + "'"
                //        + ",'" + Act.IP + "'"
                //        + ",'" + Ket + "'"
                //        + ",'" + UserID + "'"
                //        );
                //}
            }

            feed.Text = "<img src='/Media/db.gif' align=absmiddle> Berhasil...";
        }

    }
}