using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
    public partial class MappingPluginUlang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Process();
        }

        private string app
        {
            get
            {
                return Request.PhysicalApplicationPath.Replace("security\\", "");
            }
        }

        private void Process()
        {
            Db.Execute("UPDATE PLUGIN SET Baru = 0");
            
            CustomMapping(); //mapping.txt

            Db.Execute("DELETE FROM PLUGIN WHERE Baru = 0");

            Response.Redirect("MappingPlugin.aspx?done=1");
        }

        private void PrintDir(string path, string root)
        {
            string[] files = System.IO.Directory.GetFiles(path, "*.aspx");
            string[] x = System.IO.Directory.GetDirectories(path);
            for (int i = 0; i <= x.GetUpperBound(0); i++)
            {
                if (!Response.IsClientConnected) break;

                //recursive
                PrintDir(x[i], root);
            }

            if (files.GetUpperBound(0) != -1)
            {
                PrintFile(files);
            }
        }

        private void PrintFile(string[] files)
        {
            for (int i = 0; i <= files.GetUpperBound(0); i++)
            {
                if (!Response.IsClientConnected) break;

                string file = System.IO.Path.GetFullPath(files[i]); //physical path

                string modul = file.Replace(app, "");
                modul = modul.Substring(0, modul.LastIndexOf("\\")).ToUpper();

                string ctrl = "";
                string judul = "";

                System.IO.StreamReader f = new System.IO.StreamReader(file);
                bool eof = false;
                while (!eof)
                {
                    if (!Response.IsClientConnected) break;

                    string x = f.ReadLine();
                    if (x == null)
                    {
                        eof = true;
                        break;
                    }

                    if (x.Trim().ToLower().StartsWith("<meta name=\"ctrl\" content=\""))
                    {
                        ctrl = x.Trim().Substring(27); //buang opening tag meta
                        ctrl = ctrl.Substring(0, ctrl.Length - 2); //buang tutup tag
                        ctrl = ctrl.Replace("'", "''");

                        try
                        {
                            judul = f.ReadLine().Trim().Substring(26);
                            judul = judul.Substring(0, judul.Length - 2);
                            judul = judul.Replace("'", "''");
                        }
                        catch { }
                        break;
                    }
                }
                f.Close();

                switch (ctrl)
                {
                    case "0": //No login
                        break;

                    case "1": //Normal
                        Map(file, modul, judul);
                        break;

                    case "2": //Free Akses
                        Map(file, modul, judul);
                        Db.Execute("UPDATE PLUGIN SET FreeAkses = 1 WHERE Halaman = '" + file + "'");
                        break;

                    case "3": //Print
                        Map(file, modul, judul);
                        Map("RP:" + file, modul, judul + " (Reprint)");
                        break;

                    case "4": //Log Detil
                        Map(file, modul, judul);
                        Map("AL:" + file, modul, judul + " (Approval)");
                        break;

                    case "5": //Data Detil+Edit
                        Map(file, modul, judul.Replace("Edit", "Detil"));
                        Map("ED:" + file, modul, judul);
                        break;
                }
            }
        }

        //mapping.txt
        private void CustomMapping()
        {
            string mappingtxt = app + "\\security\\MappingPlugin.txt";
            System.IO.StreamReader f = new System.IO.StreamReader(mappingtxt);

            bool eof = false;
            while (!eof)
            {
                if (!Response.IsClientConnected) break;

                string x = f.ReadLine();
                if (x == null)
                {
                    eof = true;
                    break;
                }

                string[] data = x.Split(';');
                string file = data[0];
                string modul = data[1];
                string judul = data[2];

                Map(file, modul, judul);
            }
            f.Close();
        }

        private void Map(string file, string modul, string judul)
        {
            if (Db.SingleInteger("SELECT COUNT(*) FROM PLUGIN WHERE Halaman = '" + file + "'") == 0)
                Db.Execute("INSERT INTO PLUGIN ("
                    + " Halaman"
                    + ",Modul"
                    + ",Nama"
                    + ",Baru"
                    + " ) VALUES ( "
                    + " '" + file + "'"
                    + ",'" + modul + "'"
                    + ",'" + judul + "'"
                    + ",1"
                    + ")"
                    );
            else
                Db.Execute("UPDATE PLUGIN"
                    + " SET "
                    + " Baru = 1"
                    + ",Modul = '" + modul + "'"
                    + ",Nama = '" + judul + "'"
                    + " WHERE Halaman = '" + file + "'"
                    );
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
