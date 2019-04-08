<%@ Page language="c#" Inherits="ISC064.SETTINGS.DaftarUserAktif" CodeFile="DaftarUserAktif.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Daftar User Aktif</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="(Pop-Up) Daftar User Aktif">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="body-padding" class="pop" onkeyup="if(event.keyCode==27)window.close()">
		<form id="Form1" method="post" runat="server">
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="250">Nama</asp:tableheadercell>
					<asp:tableheadercell width="100">Kode</asp:tableheadercell>
					<asp:tableheadercell Wrap="false">Sec. Level</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<script type="text/javascript">
			function call(userid)
			{
			    window.parent.call(userid);
			    window.parent.globalclose();
			}
			</script>
		</form>
	</body>
</html>
