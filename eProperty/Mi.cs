using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Management;
using System.Web;

namespace ISC064
{
	public class Mi
	{
		public static string Pt
		{
			get
			{
                return "Marchand";
			}
		}

        public static bool isCorporateLicense
        {
            get
            {
                return true;
                // false, ada fungsi-fungsi yang di hide, contoh HTMLEditor
            }
        }

        public static string Version
        {
            get
            {
                return "LAND.01.01.01";
                ///// APT => APARTMENT / LAND Landed
                ///// 01 => Sales / 02 Akunting
                ///// 01 => Price List Standard / 02 PriceList Skema
                ///// 01 => Fitur
            }
        }
        //untuk plugin
        public static string KodePerusahaan
        {
            get
            {
                return "ZLPHA8";
            }
        }
        public static string KodeProject
        {
            get
            {
                return "PWZT3V";
            }
        }

		public static string DbPrefix
		{
			get
			{
				System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
				string x = (string) s.GetValue("DbPrefix", typeof(string));
				s = null;
				return x;
			}
		}

		//Production Server
		public static bool isProduction
		{
			get
            {
                if (os == "xp")
                {
                    string dir = "C:\\Windows\\eproperty.txt";
                    if (System.IO.File.Exists(dir))
                    {
                        System.IO.StreamReader z = new System.IO.StreamReader(dir);
                        string x = z.ReadLine();
                        z.Close();

                        if (x == "00-22-4D-87-C5-0C") //server cloud
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                {
                    string mc = GetMACAddress();
                    if (
                        mc == "36-38-31-36-34-38"
                        || mc == "4C-D9-8F-07-E2-1B"
                        || mc == "00-0A-F7-E8-4A-D8"
                        || mc == "C2-CC-B7-7C-98-C2" //server Demo
                        || mc == "D0-C5-D3-7C-BE-CF"
                        || mc == "66-6C-2E-84-A4-B1"// server 203
                        || mc == "18-66-DA-97-F9-16"
                        || mc == "0C-D2-92-B1-1D-3B" // yohanes-pc
                        || mc == "28-E3-47-1B-C6-64" //febri
                        || mc == "00-16-3E-E3-BE-91" // server
                        )
						return true;
					else
						return false;
				}
			}
		}
		
		//Development Server
		public static bool isDevelopment
		{
			get
			{
				string mc = GetMACAddress();
                if (mc == "70-1C-E7-FA-69-FF"
                    || mc == "0C-D2-92-B1-18-59" //anggi-pc
                    || mc == "AC-E0-10-2E-DB-DB"
                    || mc == "0C-D2-92-B1-1D-3B" // yohanes-pc
                        || mc == "28-E3-47-1B-C6-64" //febri
                        || mc == "00-16-3E-E3-BE-91" // server

                    )
                    return true;
				else
					return false; //false
			}
		}

        //for convert pdf 
        public static string PathWkhtmlPDFReport
        {
            get
            {
                System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
                string x = (string)s.GetValue("PathWkhtmlPDFReport", typeof(string));
                s = null;
                return x;
            }
        }

        public static string PathFilePDFReport
        {
            get
            {
                System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
                string x = (string)s.GetValue("PathFilePDFReport", typeof(string));
                s = null;
                return x;
            }
        }
        public static void DownloadPDF(System.Web.UI.Page p, string physicalFile, string FileName, string ContentType)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(
                System.Web.HttpContext.Current.Request.PhysicalPath);

            p.Response.Clear();
            p.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".pdf");

            p.Response.CacheControl = "no-cache";
            p.Response.ContentType = "application/pdf";
            p.Response.WriteFile(physicalFile);
            p.Response.End();
            p.Response.Flush();
        }

        //setting default alamat web port
        public static string PathAlamatWeb
        {
            get
            {
                return "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/";
            }
        }

        //alamat plugin
        public static string PathPlugin
        {
            get
            {
                System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
                string x = (string)s.GetValue("PathPlugin", typeof(string));
                s = null;
                return x;
            }
        }

        #region public static string os
        public static string os
		{
			get
			{
				System.Configuration.AppSettingsReader s = new System.Configuration.AppSettingsReader();
				string x = (string) s.GetValue("os", typeof(string));
				s = null;
				return x;
			}
		}
		#endregion
		#region public static bool Licensed()
		public static bool Licensed()
		{
			if(isProduction || isDevelopment)
				return true;
			else
				return false;
		}
		#endregion

		//Get NIC mac-address.
		#region public string GetMACAddress()
		public static string GetMACAddress()
		{
			ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObjectCollection moc = mc.GetInstances();

			string MACAddress = String.Empty;

			foreach(ManagementObject mo in moc)
			{
				if(MACAddress == String.Empty) // only return MAC Address from first card
				{
					if((bool)mo["IPEnabled"] == true)
						MACAddress= mo["MacAddress"].ToString();
				}
				mo.Dispose();
			}

			MACAddress = MACAddress.Replace(":","-");

			return MACAddress;
		}
		#endregion
	}
}
