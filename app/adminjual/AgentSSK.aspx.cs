using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL
{
	public partial class AgentSSK : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Act.Pass();
			Act.NoCache();

			Act.CekInt("NoAgent");

			if(!Page.IsPostBack)
			{
				InitForm();
				Fill();
			}

			FeedBack();
		}

		private void FeedBack()
		{
			feed.Text = "";
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["done"]!=null)
					feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
						+ "Set Skema Komisi Berhasil...";
			}
		}

		private void InitForm()
		{
			target1.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			target1.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			target1.Attributes["onblur"] = "CalcBlur(this);";

			target2.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			target2.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			target2.Attributes["onblur"] = "CalcBlur(this);";

			target3.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			target3.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			target3.Attributes["onblur"] = "CalcBlur(this);";

			target4.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			target4.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			target4.Attributes["onblur"] = "CalcBlur(this);";

			target5.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
			target5.Attributes["onkeyup"] = "CalcType(this,tempnum);";
			target5.Attributes["onblur"] = "CalcBlur(this);";

			DataTable rs = Db.Rs("SELECT Nomor,Nama,Status FROM REF_SKOM ORDER BY Nama");
			
			for(int i=0;i<rs.Rows.Count;i++)
			{
				string v = rs.Rows[i]["Nomor"].ToString();
				string t = rs.Rows[i]["Nama"] + " ("+v.PadLeft(3,'0')+")";
				if(rs.Rows[i]["Status"].ToString()=="I")
					t = t + " ***INAKTIF";
				
				skema0.Items.Add(new ListItem(t,v));
				skema1.Items.Add(new ListItem(t,v));
				skema2.Items.Add(new ListItem(t,v));
				skema3.Items.Add(new ListItem(t,v));
				skema4.Items.Add(new ListItem(t,v));
				skema5.Items.Add(new ListItem(t,v));
			}
		}

		private void Fill()
		{
			string strSql = "SELECT * FROM MS_AGENT WHERE NoAgent = " + NoAgent;
			DataTable rs = Db.Rs(strSql);

			if(rs.Rows.Count==0)
				Response.Redirect("/CustomError/Deleted.html");
			else
			{
				skema0.SelectedValue = rs.Rows[0]["Skema0"].ToString();
				skema1.SelectedValue = rs.Rows[0]["Skema1"].ToString();
				skema2.SelectedValue = rs.Rows[0]["Skema2"].ToString();
				skema3.SelectedValue = rs.Rows[0]["Skema3"].ToString();
				skema4.SelectedValue = rs.Rows[0]["Skema4"].ToString();
				skema5.SelectedValue = rs.Rows[0]["Skema5"].ToString();

				target1.Text = Cf.Num(rs.Rows[0]["Target1"]);
				target2.Text = Cf.Num(rs.Rows[0]["Target2"]);
				target3.Text = Cf.Num(rs.Rows[0]["Target3"]);
				target4.Text = Cf.Num(rs.Rows[0]["Target4"]);
				target5.Text = Cf.Num(rs.Rows[0]["Target5"]);
			}
		}

		private bool valid()
		{
			string s = "";
			bool x = true;

			if(!Cf.isMoney(target1))
			{
				x = false;
				if(s=="") s = target1.ID;
				target1c.Text = "Angka";
			}
			else
				target1c.Text = "";

			if(!Cf.isMoney(target2))
			{
				x = false;
				if(s=="") s = target2.ID;
				target2c.Text = "Angka";
			}
			else
				target2c.Text = "";

			if(!Cf.isMoney(target3))
			{
				x = false;
				if(s=="") s = target3.ID;
				target3c.Text = "Angka";
			}
			else
				target3c.Text = "";

			if(!Cf.isMoney(target4))
			{
				x = false;
				if(s=="") s = target4.ID;
				target4c.Text = "Angka";
			}
			else
				target4c.Text = "";

			if(!Cf.isMoney(target5))
			{
				x = false;
				if(s=="") s = target5.ID;
				target5c.Text = "Angka";
			}
			else
				target5c.Text = "";

			if(!x)
				Js.Alert(
					this
					, "Input Tidak Valid.\\n\\n"
					+ "Aturan Proses :\\n"
					+ "1. Target harus berupa angka.\\n"
					, "document.getElementById('"+s+"').focus();"
					+ "document.getElementById('"+s+"').select();"
					);

			return x;
		}

		private bool Save()
		{
			if(valid())
			{
				decimal Target1 = Convert.ToDecimal(target1.Text);
				decimal Target2 = Convert.ToDecimal(target2.Text);
				decimal Target3 = Convert.ToDecimal(target3.Text);
				decimal Target4 = Convert.ToDecimal(target4.Text);
				decimal Target5 = Convert.ToDecimal(target5.Text);
				int Skema0 = Convert.ToInt32(skema0.SelectedValue);
				int Skema1 = Convert.ToInt32(skema1.SelectedValue);
				int Skema2 = Convert.ToInt32(skema2.SelectedValue);
				int Skema3 = Convert.ToInt32(skema3.SelectedValue);
				int Skema4 = Convert.ToInt32(skema4.SelectedValue);
				int Skema5 = Convert.ToInt32(skema5.SelectedValue);

				DataTable rsBef = Db.Rs("SELECT "
					+ " Target1 AS [Target #1]"
					+ ",Target2 AS [Target #2]"
					+ ",Target3 AS [Target #3]"
					+ ",Target4 AS [Target #4]"
					+ ",Target5 AS [Target #5]"
					+ ",Skema0 AS [Skema Komisi Standard]"
					+ ",Skema1 AS [Skema Komisi #1]"
					+ ",Skema2 AS [Skema Komisi #2]"
					+ ",Skema3 AS [Skema Komisi #3]"
					+ ",Skema4 AS [Skema Komisi #4]"
					+ ",Skema5 AS [Skema Komisi #5]"
					+ " FROM MS_AGENT"
					+ " WHERE NoAgent = " + NoAgent
					);

				Db.Execute("EXEC spAgentEditSSK"
					+ "  " + NoAgent
					+ ", " + Target1
					+ ", " + Target2
					+ ", " + Target3
					+ ", " + Target4
					+ ", " + Target5
					+ ", " + Skema0
					+ ", " + Skema1
					+ ", " + Skema2
					+ ", " + Skema3
					+ ", " + Skema4
					+ ", " + Skema5
					);

				DataTable rsAft = Db.Rs("SELECT "
					+ " Target1 AS [Target #1]"
					+ ",Target2 AS [Target #2]"
					+ ",Target3 AS [Target #3]"
					+ ",Target4 AS [Target #4]"
					+ ",Target5 AS [Target #5]"
					+ ",Skema0 AS [Skema Komisi Standard]"
					+ ",Skema1 AS [Skema Komisi #1]"
					+ ",Skema2 AS [Skema Komisi #2]"
					+ ",Skema3 AS [Skema Komisi #3]"
					+ ",Skema4 AS [Skema Komisi #4]"
					+ ",Skema5 AS [Skema Komisi #5]"
					+ " FROM MS_AGENT"
					+ " WHERE NoAgent = " + NoAgent
					);

				//Logfile
				DataTable rs = Db.Rs("SELECT "
					+ " NoAgent AS [No. Agent]"
					+ ",Nama AS [Nama]"
					+ " FROM MS_AGENT"
					+ " WHERE NoAgent = " + NoAgent
					);

				string Ket = Cf.LogCapture(rs)
					+ Cf.LogCompare(rsBef,rsAft);

				Db.Execute("EXEC spLogAgent"
					+ " 'SSK'"
					+ ",'" + Act.UserID + "'"
					+ ",'" + Act.IP + "'"
					+ ",'" + Ket + "'"
					+ ",'" + NoAgent.PadLeft(5,'0') + "'"
					);

				return true;
			}
			else
				return false;
		}

		protected void ok_Click(object sender, System.EventArgs e)
		{
			if(Save()) Js.Close(this);
		}

		protected void save_Click(object sender, System.EventArgs e)
		{
			if(Save()) Response.Redirect("AgentSSK.aspx?done=1&NoAgent="+NoAgent);
		}

		private string NoAgent
		{
			get
			{
				return Cf.Pk(Request.QueryString["NoAgent"]);
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
