using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{

    public partial class SmsFormat2 : System.Web.UI.Page
    {
        protected string Tipe { get { return (Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {
                //filter();
                Act.ProjectList(project);
                fill();
                fillparam();
            }
            FeedBack();
            //CekSMS();
        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["d"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit berhasil..."
                        ;
                project.SelectedValue = Request.QueryString["project"];
                fill();

            }
        }
        //protected void CekSMS()
        //{
        //    if (Auth.SMS(dept.SelectedValue) != "ST")
        //        App.ErrAuthorize(this, this.Request.Url.ToString());
        //}
        //protected void filter()
        //{
        //    LibSec.SecProject(dept, Auth.ListAksesDept(Auth.UserID));
        //}
        protected void fillparam()
        {
            DataTable rs = Db.Rs("SELECT * FROM SmsParam WHERE Tipe = '" + Tipe + "'");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                HtmlTableRow tr = new HtmlTableRow();
                list.Controls.Add(tr);

                HtmlTableCell c;

                c = new HtmlTableCell();
                c.InnerHtml = "@@" + rs.Rows[i]["Param"];
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Keterangan"].ToString();
                tr.Cells.Add(c);

            }
        }
        protected void fill()
        {
            //var d = db.SmsFormats.SingleOrDefault(p => p.Project == dept.SelectedValue && p.Tipe == Tipe);
            //if (d != null)
            //{
            //    waktu.Text = Cf.NumBulat(d.NilaiWaktu);
            //    satuan.SelectedValue = d.SatuanWaktu;
            //    format.Text = d.Format;
            //    inaktif.SelectedIndex = d.Inaktif ? 1 : 0;
            //}            
            DataTable rsHeader = Db.Rs("SELECT * FROM SmsFormat WHERE Project = '" + project.SelectedValue + "' AND Tipe ='" + Tipe + "'");
            if (rsHeader.Rows.Count == 0)
            {
                tipe.Text = Tipe;
                waktu.Text = "";
                satuan.SelectedIndex = 0;
                format.Text = "";
                inaktif.SelectedIndex = 0;
            }
            else
            {
                tipe.Text = rsHeader.Rows[0]["Tipe"].ToString();
                waktu.Text = Cf.NumBulat(rsHeader.Rows[0]["NilaiWaktu"]);
                satuan.SelectedValue = (rsHeader.Rows[0]["SatuanWaktu"].ToString());
                format.Text = rsHeader.Rows[0]["Format"].ToString();
                inaktif.SelectedIndex = Convert.ToBoolean(rsHeader.Rows[0]["Inaktif"]) ? 1 : 0;
                project.SelectedValue = (rsHeader.Rows[0]["Project"].ToString());

                //decimal waktu2 = Db.SingleDecimal("SELECT ISNULL(NilaiWaktu, '0') FROM SmsFormat WHERE Project = '" + project.SelectedValue + "' AND Tipe ='"+Tipe+ "'");
                //string satuan2 = Db.SingleString("SELECT ISNULL(SatuanWaktu, '0') FROM SmsFormat WHERE Project = '" + project.SelectedValue + "' AND Tipe ='" + Tipe + "'");
                //string format2 = Db.SingleString("SELECT ISNULL(Format, '0') FROM SmsFormat WHERE Project = '" + project.SelectedValue + "' AND Tipe ='" + Tipe + "'");
                //string inaktif2 = Db.SingleString("SELECT ISNULL(Inaktif, '0') FROM SmsFormat WHERE Project = '" + project.SelectedValue + "' AND Tipe ='" + Tipe + "'");
                //string project2 = Db.SingleString("SELECT ISNULL(Project, '0') FROM SmsFormat WHERE Project = '" + project.SelectedValue + "' AND Tipe ='" + Tipe + "'");

                //waktu.Text = Convert.ToString(waktu2);
                //satuan.Text = satuan2;
                //format.Text = format2;
                //inaktif.SelectedIndex  = Convert.ToBoolean(inaktif2) ? 1:0 ;
                //project.Text = project2;
            }

            if (Tipe != "UlangTahun")
                divwaktu.Visible = true;
        }
        protected void save_Click(object sender, EventArgs e)
        {
            Save();
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            x = !Cf.isEmpty(waktu) ? x : false;
            x = !Cf.isEmpty(format) ? x : false;

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
        protected void Save()
        {
            if (valid())
            {
                DataTable rs = Db.Rs("SELECT * FROM SmsFormat WHERE Project = '" + project.SelectedValue + "' AND Tipe ='" + Tipe + "'");
                if (rs.Rows.Count == 0)
                {
                    string Project = Cf.Str(project.SelectedValue);
                    string Tipe2 = Cf.Str(tipe.Text);
                    string SatuanWaktu = satuan.SelectedValue;
                    int NilaiWaktu = Convert.ToInt32(waktu.Text);
                    string Format = Cf.Str(format.Text);
                    bool Inaktif = inaktif.SelectedIndex == 0 ? false : true;

                    Db.Execute("EXEC spSmsFormatDaftar"
                        + " '" + Project + "'"
                        + ",'" + Tipe2 + "'"
                        + ",'" + SatuanWaktu + "'"
                        + ",'" + NilaiWaktu + "'"
                        + ",'" + Format + "'"
                        + ",'" + Inaktif + "'"
                        );

                    DataTable rsHeader = Db.Rs("SELECT "
                    + " Project"
                    + ",Tipe"
                    + ",SatuanWaktu"
                    + ",NilaiWaktu"
                    + ",Format"
                    + ",Inaktif"
                    + " FROM " + Mi.DbPrefix + "SECURITY..SmsFormat "
                    + " WHERE Project = '" + project.SelectedValue + "'");

                    string Ket = Cf.LogCapture(rsHeader);

                    Db.Execute("EXEC spLogSmsFormat"
                        + " 'DAFTAR'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + project.SelectedValue + "'"
                        );
                }
                else
                {
                    string Tipe2 = Cf.Str(tipe.Text);
                    string SatuanWaktu = satuan.SelectedValue;
                    int NilaiWaktu = Convert.ToInt32(waktu.Text);
                    string Format = Cf.Str(format.Text);
                    bool Inaktif = inaktif.SelectedIndex == 0 ? false : true;

                    Db.Execute("EXEC spSmsFormatEdit"
                        + " '" + Tipe2 + "'"
                        + ",'" + SatuanWaktu + "'"
                        + ",'" + NilaiWaktu + "'"
                        + ",'" + Format + "'"
                        + ",'" + Inaktif + "'"
                        );
                    //Db.Execute("UPDATE SmsSatuTitik SET Username = '" + Username + "', Pass = '" + Pass + "', Masking = '" + Masking + "', Divisi = '" + Divisi +"' WHERE Username = '" + Username + "'");
                    DataTable rsHeader = Db.Rs("SELECT "
                   + " Project"
                   + ",Tipe"
                   + ",SatuanWaktu"
                   + ",NilaiWaktu"
                   + ",Format"
                   + ",Inaktif"
                   + " FROM " + Mi.DbPrefix + "SECURITY..SmsFormat "
                   + " WHERE Project = '" + project.SelectedValue + "'");

                    string Ket = Cf.LogCapture(rsHeader);

                    Db.Execute("EXEC spLogSmsFormat"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + project.SelectedValue + "'"
                        );
                }
                Response.Redirect("SmsFormat2.aspx?d=1&id=" + Tipe + "&project=" + project.SelectedValue);
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
            fillparam();
        }
    }
}