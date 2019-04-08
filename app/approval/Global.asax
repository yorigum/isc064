<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<script RunAt="server">
	void Application_Start(object sender, EventArgs e) {

	}
    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        Exception ex = Server.GetLastError();
		AppSettingsReader s = new AppSettingsReader();
		string x = (string) s.GetValue("cnnString", typeof(string));
		s = null;

        string query = "EXECUTE ISC064_SECURITY..InsertProblem '" + Request.Url.AbsoluteUri + "', '" + ex.ToString().Replace("'", "''") + "'";        

        SqlConnection Conn = new SqlConnection(x);
        SqlCommand sqlCmd = new SqlCommand(query, Conn);
        
		Conn.Open();
		sqlCmd.ExecuteNonQuery();
		Conn.Close();
    }

</script>
