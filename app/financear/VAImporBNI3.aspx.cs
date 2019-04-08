using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ISC064.financear
{
    public partial class VAImporBNI3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
 
                fill();
        }

        protected string NoID
        {
            get
            {
                return Request.QueryString["ID"];
            }
        }


        private void fill()
        {
            string[] id = NoID.Split(';', ' ');
            int count = id.Length;

            for (int i = 0; i < count; i++)
            {
                if (id[i] != "")
                {
                    string strSql = "SELECT * FROM MS_TTS WHERE NOTTS = " + id[i];
                    DataTable rs = Db.Rs(strSql);

                    //Result
                    HtmlTableRow r = new HtmlTableRow();
                    HtmlTableCell c;
                    TextBox t = new TextBox();

                    c = new HtmlTableCell();
                    c.InnerHtml = rs.Rows[0]["Ket"].ToString();
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.InnerHtml = Cf.Day(rs.Rows[0]["TglTTS"]);
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.InnerHtml = "<a href=\"javascript:call('" + id[i] + "')\">"
                        + id[i].ToString().PadLeft(7, '0') + "</a>"
                        + "<br /><i>POST</i>";
                    //+ "<br />BKM: " + rs.Rows[0]["NoBKM"];
                    c.ID = id[i].ToString();
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.InnerHtml = "JUAL No. " + rs.Rows[0]["Ref"]
                        + "<br />" + rs.Rows[0]["Unit"]
                        + "<br />" + rs.Rows[0]["Customer"];
                    r.Cells.Add(c);

                    c = new HtmlTableCell();
                    c.InnerHtml = Cf.Num(rs.Rows[0]["Total"]);
                    //c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);

                    t = new TextBox();
                    t.ID = "no_" + i;
                    t.Width = 60;
                    c = new HtmlTableCell();
                    c.Controls.Add(t);
                    r.Cells.Add(c);

                    t = new TextBox();
                    t.ID = "tts_" + i;
                    t.Visible = false;
                    t.Text = id[i];
                    c.Controls.Add(t);
                    r.Cells.Add(c);

                    ph.Controls.Add(r);
                }
            }
        }
        #region save1_Click(object sender, EventArgs e)
        protected void save1_Click(object sender, EventArgs e)
        {

            string[] id = NoID.Split(';', ' ');
            int count = id.Length;


            for (int i = 0; i < count; i++)
            {

                if (id[i] != "")
                {


                    TextBox no = (TextBox)ph.FindControl("no_" + i);
                    TextBox tts = (TextBox)ph.FindControl("tts_" + i);
                    string ManualBKM = no.Text != "" ? Cf.Str(no.Text).PadLeft(6, '0') : "";
                    int NoTTS = Convert.ToInt32(tts.Text);

                    int bkm = Db.SingleInteger("SELECT COUNT(*) FROM MS_TTS WHERE ManualBKM = '" + ManualBKM + "' AND NoTTS != " + NoTTS);
                    if (bkm == 0)
                    {
                        Db.Execute("UPDATE MS_TTS  SET ManualBKM = '" + ManualBKM + "' WHERE NOTTS = " + NoTTS);
                    }

                }
            }
            div1.Visible = false;
            feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                + "Upload Berhasil.."
                ;
        }
        #endregion
    }
}
