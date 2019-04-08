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

namespace ISC064.MARKETINGJUAL
{
	/// <summary>
	/// Summary description for KomisiGen2.
	/// </summary>
	public partial class KomisiGen2 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox periode;
		protected System.Web.UI.WebControls.DropDownList jenis;
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			if(!Page.IsPostBack)
			{
				Js.Focus(this,noagent);
                fillAcc();
                //fillDept();
				
				dari.Text = Cf.Day(Cf.AwalBulan());
				sampai.Text = Cf.Day(Cf.AkhirBulan());
			}

            FeedBack();
			
		}

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Generate Komisi Berhasil...";
                if (Request.QueryString["Clear"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Clear False Komisi Berhasil...";
            }
        }

        private void fillAcc()
        {
            DataTable rs = Db.Rs("SELECT * FROM ISC064_FINANCEAR..REF_ACC");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
            }
        }

        //private void fillDept()
        //{
        //    DataTable rs = Db.Rs("SELECT * FROM [41KVA]..GlDept ");
        //    dept.Items.Clear();
        //    for (int i = 0; i < rs.Rows.Count; i++)
        //    {
        //        string v = rs.Rows[i]["Dept"].ToString();
        //        string t = v + " : " + rs.Rows[i]["Nama"];
        //        dept.Items.Add(new ListItem(t, v));
        //    }
        //}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isInt(noagent))
			{
				x = false;
				//if(s=="") s = sampai.ID;
				//	targetstc.Text = "Tanggal";
			}

			if(!Cf.isTgl(dari))
			{
				x = false;
				//if(s=="") s = sampai.ID;
				//	targetstc.Text = "Tanggal";
			}

			if(!Cf.isTgl(sampai))
			{
				x = false;
				//if(s=="") s = sampai.ID;
				//	targetstc.Text = "Tanggal";
			}
		

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
                    + "1. No Sales tidak boleh kosong.\\n"
					+ "2. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		protected void next_Click(object sender, System.EventArgs e)
		{
			if(valid())
			{
				string NoAgent = noagent.Text ;
				DateTime Dari = Convert.ToDateTime(dari.Text);
				DateTime Sampai = Convert.ToDateTime(sampai.Text);
                string Acc = ddlAcc.SelectedValue;
                //string Proyek = dept.SelectedValue;
				
				Response.Redirect("KomisiGen2Detail.aspx?NoAgent="+NoAgent+"&Dari="+Dari+"&Sampai="+Sampai+"&Acc="+Acc+"");//&Proyek="+Proyek+"");
			}
		}

        protected void clear_Click(object sender, System.EventArgs e)
        {
            string NoAgent = noagent.Text;
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);

            Response.Redirect("KomisiClear.aspx?Dari=" + Dari + "&Sampai=" + Sampai + "&NoAgent=" + NoAgent + "");
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
