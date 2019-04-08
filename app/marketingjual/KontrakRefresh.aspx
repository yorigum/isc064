<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakRefresh" CodeFile="KontrakRefresh.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Refresh Data Unit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Refresh Data Unit">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
			<asp:label id="nodel" runat="server" visible="false">
				<h1 class="title title-line">
					Refresh Data Unit Gagal
				</h1>
				<br>
				<div class="plike">
					<h2>Kemungkinan Terjadi Karena:</h2>
					<ul>
						<li>
							Kontrak tidak terdaftar</li>
						<li>
							Kontrak sudah dibatalkan</li>
					</ul>
				</div>
			</asp:label>
		</form>
	</body>
</html>
