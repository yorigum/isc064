using System;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Collections.Generic;
using System.Web.Services;

namespace ISC064.FINANCEAR
{
    public partial class FPPosting : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                init();
            }

            Fill();
            FeedBack();
            //if (frm.Visible) Js.Confirm(this, "Jalankan prosedur POSTING FAKTUR PAJAK?");
        }
        private void init()
        {
            dari.Text = Cf.Day(Cf.AwalBulan());
            sampai.Text = Cf.Day(Cf.AkhirBulan());
            Act.ProjectList(project);
            project.SelectedIndex = 1;
        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Posting Faktur Pajak Selesai..."
                        ;
            }
        }

        private void Fill()
        {  

            string strSql = "";
            list.Controls.Clear();
            if (dari.Text != "" && sampai.Text != "")
            {
                DateTime Dari = Convert.ToDateTime(dari.Text);
                DateTime Sampai = Convert.ToDateTime(sampai.Text);
                if (Dari > Sampai)
                {
                    DateTime x = Sampai;
                    Sampai = Dari;
                    Dari = x;
                }
                strSql = "SELECT a.*, b.NoKontrak, b.NoUnit FROM MS_TTS a"
                                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                                + " WHERE a.Status = 'POST' AND a.NoFPS = '' AND b.Status = 'A'"
                                + " AND CONVERT(varchar,a.TglBKM,112) >= '" + Cf.Tgl112(Dari) + "'"
                                + " AND CONVERT(varchar,a.TglBKM,112) <= '" + Cf.Tgl112(Sampai) + "'"
                                + " AND a.Project = '" + project.SelectedValue + "'"
                                + " ORDER BY a.TglBKM, a.NoTTS"
                                ;
            }
            else
            {
                strSql = "SELECT a.*, b.NoKontrak, b.NoUnit FROM MS_TTS a"
                                + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK b ON a.Ref = b.NoKontrak"
                                + " WHERE a.Status = 'POST' AND a.NoFPS = '' AND b.Status = 'A'"
                                + " AND a.Project = '" + project.SelectedValue + "'"
                                + " ORDER BY a.TglBKM, a.NoTTS"
                                ;
            }
            
            DataTable rs = Db.Rs(strSql);
            if (rs.Rows.Count == 0)
            {
                save.Enabled = false;
            }
            else
            {
                save.Enabled = true;
            }

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow tr;
                HtmlTableCell c;
                CheckBox cb;
                HtmlInputButton bt;
                TextBox tb;
                HtmlGenericControl ie;

                tr = new HtmlTableRow();
                list.Controls.Add(tr);

                cb = new CheckBox();
                cb.ID = "notts_" + i;
                //cb.Checked = NoFP(i) != "" ? true : false;
                //cb.Enabled = NoFP(i) != "" ? true : false;

                c = new HtmlTableCell();
                c.ID = "pk_" + i;
                c.Attributes["title"] = rs.Rows[i]["NoTTS"].ToString();
                c.Controls.Add(cb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = "<a href=\"javascript:popEditTTS('" + rs.Rows[i]["NoTTS"] + "')\">"
                    + rs.Rows[i]["NoTTS"].ToString().PadLeft(7,'0')
                    + "</a>";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                tb = new TextBox();
                tb.Width = 250;
                tb.ID = "fp_" + i;

                bt = new HtmlInputButton();
                bt.Value = "...";
                bt.Attributes["onclick"] = "popDaftarFP('"+ tb.ID +"','"+ cb.ID +"','" + project.SelectedValue + "','"+ Cf.Tgl112(Convert.ToDateTime(rs.Rows[i]["TglBKM"])) + "');";
                bt.Attributes["class"] = "btn";
                bt.ID = "btn_" + i;
                
                c.Controls.Add(tb);
                c.Controls.Add(bt);
                c.NoWrap = true;
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                tb = new TextBox();
                tb.ID = "tglbkm_" + i;
                tb.Text = Cf.Day(rs.Rows[i]["TglBKM"]);
                tb.Width = 100;
                tb.Visible = false;
                c.InnerHtml = Cf.Day(rs.Rows[i]["TglBKM"]);
                c.Controls.Add(tb);
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoKontrak"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Customer"].ToString();
                tr.Cells.Add(c);

                decimal Nilai = Convert.ToDecimal(rs.Rows[i]["Total"]);
                decimal DPP = Math.Round(Nilai / (decimal)1.1);
                decimal PPN = Nilai - DPP;

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(DPP);
                c.Align = "right";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(PPN);
                c.Align = "right";
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Nilai);
                c.Align = "right";
                tr.Cells.Add(c);
            }
        }
        
        protected string NoFP(int i)
        {
            string FP = "";
            int stok = Db.SingleInteger("SELECT COUNT(*) FROM REF_FP WHERE Status = 0");
            if (i < stok)
            {
                FP = Db.SingleString("SELECT NoFPS"
                        + " FROM (SELECT NoFPS, row_number()"
                        + " OVER (ORDER BY NoFPS) AS 'Row'"
                        + " FROM REF_FP WHERE Status = 0) AS tbl"
                        + " WHERE Row = " + (i + 1));

                CheckBox cb = (CheckBox)list.FindControl("fp_" + i);
                //cb.Checked = true;
            }
            return FP;
        }

        protected bool datavalid()
        {
            bool x = true;
            string s = "";

            //if (!Cf.isTgl(tglot))
            //{
            //    x = false;
            //    if (s == "") s = tglot.ID;
            //    tglotc.Text = "Format Tanggal";
            //}
            //else
            //    tglotc.Text = "";

            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script type='text/javascript'>"
                    + "document.getElementById('" + s + "').focus();"
                    + "</script>"
                    );
            }

            return x;
        }
        public string[] a ;
        public string[] a2;
        protected void simpan()
        {
            var listt = new List<string>();
            var listt2 = new List<string>();
            listt.Clear();
            listt2.Clear();
            a = listt.ToArray();
            a2 = listt2.ToArray();
            int i = 0;
            foreach (Control tr in list.Controls)
            {
                HtmlTableCell c = (HtmlTableCell)list.FindControl("pk_" + i);
                TextBox c2 = (TextBox)list.FindControl("fp_" + i);
                CheckBox cb = (CheckBox)list.FindControl("notts_" + i);

                if (cb.Checked && c2.Text != "")
                {
                    listt.Add(c2.Text.ToString());
                    listt2.Add("'" + c2.Text.ToString() + "'");
                }
                i++;
            }
            a = listt.ToArray();
            a2 = listt2.ToArray();
            
        }

        protected bool unik()
        {
            bool x = true;
            int i = 0;
            
            int check = 0;
            
            foreach (Control tr in list.Controls)
            {
                HtmlTableCell c = (HtmlTableCell)list.FindControl("pk_" + i);
                TextBox c2 = (TextBox)list.FindControl("fp_" + i);
                TextBox tglbkm = (TextBox)list.FindControl("tglbkm_" + i);
                CheckBox cb = (CheckBox)list.FindControl("notts_" + i);

                if (cb.Checked)
                {
                    if (c2.Text == "")
                    {
                        x = false;
                        c2.Attributes["style"] = "background-color:#ffff00"; //yellow
                    }
                    else if (c2.Text != "")
                    {
                        decimal j = 0;//a.Count(m => a.Contains(""));
                        for (int o = 0; o < a.Length; o++)
                        {
                            if (a[o].Contains(c2.Text.ToString()))
                            {
                                j += 1;
                            }
                        }

                        //Validasi FP terdaftar atau tidak
                        int FpExist = Db.SingleInteger("SELECT COUNT(*) FROM REF_FP WHERE NoFPS = '" + c2.Text.ToString() + "' AND Status = 0");

                        //Validasi FP sudah terpakai
                        int FpTerpakai = Db.SingleInteger("SELECT COUNT(*) FROM MS_TTS WHERE NoFPS = '" + c2.Text.ToString() + "'");

                        //Validasi FP backdate
                        DataTable dtFP = Db.Rs("SELECT * FROM REF_FP WHERE NoFPS = '" + c2.Text.ToString() + "' AND Status = 0");

                        if (FpExist == 0)
                        {
                            x = false;
                            c2.Attributes["style"] = "background-color:#ff9900"; //orange
                        }
                        else if (FpTerpakai > 0)
                        {
                            x = false;
                            c2.Attributes["style"] = "background-color:#ffb3b3"; //merah muda
                        }
                        else if (Convert.ToDateTime(tglbkm.Text) < Convert.ToDateTime(Cf.Day(dtFP.Rows[0]["TglTerimaFP"])))
                        {
                            x = false;
                            c2.Attributes["style"] = "background-color:#ff3333"; //merah
                        }
                        else if (j > 1)
                        {
                            x = false;
                            c2.Attributes["style"] = "background-color:#b3c6ff"; //lightblue
                        }
                        else
                        {
                            x = true;
                            c2.Attributes["style"] = "background-color:#ffffff"; //white
                        }
                    }
                    check++;
                }
                else
                {
                    c2.Attributes["style"] = "background-color:#ffffff"; //white
                    c2.Text = "";
                }


                i++;  
            }
          

           
            return x;
        }

      
        protected void save_Click(object sender, System.EventArgs e)
        {
            if (checkOne())
            {
                simpan();
                if (unik())
                {

                    int index = 0;
                    foreach (Control tr in list.Controls)
                    {
                        HtmlTableCell c = (HtmlTableCell)list.FindControl("pk_" + index);
                        TextBox c2 = (TextBox)list.FindControl("fp_" + index);
                        CheckBox cb = (CheckBox)list.FindControl("notts_" + index);

                        if (cb.Checked)
                        {
                            Save(c.Attributes["title"], c2.Text, cb);
                        }

                        index++;
                    }
                    Response.Redirect("FPPosting.aspx?done=yes");

                }
            }
        }

        private void Save(string NoTTS, string NoFP, CheckBox cb)
        {
            if (cb.Checked)
            {
                DataTable rs = Db.Rs("SELECT "
                    + " NoTTS AS [No. TTS]"
                    + ",Tipe"
                    + ",Ref AS [Ref.]"
                    + ",CaraBayar AS [Cara Bayar]"
                    + ",Total AS [Nilai TTS]"
                    + " FROM MS_TTS"
                    + " WHERE NoTTS = " + NoTTS
                    );

                DataTable rsBef = Db.Rs("SELECT "
                    + " NoFPS AS [No. Faktur Pajak]"
                    + " FROM MS_TTS"
                    + " WHERE NoTTS = " + NoTTS
                    );

                Db.Execute("UPDATE MS_TTS SET "
                    + " NoFPS = '" + NoFP + "'"
                    + " WHERE NoTTS = " + NoTTS);

                Db.Execute("UPDATE REF_FP SET "
                    + " Status = 1"
                    + " WHERE NoFPS = '" + NoFP + "'");

                DataTable rsAft = Db.Rs("SELECT "
                    + " NoFPS AS [No. Faktur Pajak]"
                    + " FROM MS_TTS"
                    + " WHERE NoTTS = " + NoTTS
                    );

                //Logfile
                string ketlog = Cf.LogCapture(rs)
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogTTS"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + NoTTS.ToString().PadLeft(7, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_TTS_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = (SELECT Ref FROM MS_TTS WHERE NoTTS = '" + NoTTS + "')");
                Db.Execute("UPDATE MS_TTS_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }
        protected void display_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                Fill();
            }
        }

        protected bool checkOne()
        {
            bool x = false;
            int i = 0;
            foreach (Control tr in list.Controls)
            {
                CheckBox cb = (CheckBox)list.FindControl("notts_" + i);

                if (cb.Checked)
                {
                    x = true;
                }
                i++;
            }

            if (!x)
            {
                Js.Alert(this, "Anda belum memilih!", "");
            }

            return x;
        }
    }
}
