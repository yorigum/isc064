<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.ShowPetaTV2" CodeFile="ShowPetaTV2.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Tower A</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Refresh" content="5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<style type="text/css">
		A { COLOR: black }
		A:visited { COLOR: black }
		A:hover { COLOR: black }
		#tb { BORDER-COLLAPSE: collapse  ;  COLOR: white}
		#tb2 { BORDER-COLLAPSE: collapse }
		#tb3 { BORDER-COLLAPSE: collapse }
		#tb_kios { BORDER-COLLAPSE: collapse }
		#tb_food { BORDER-COLLAPSE: collapse }
		#tb TD { FONT: 8pt lucida; WIDTH: 23px; TEXT-ALIGN: center }
		#tb_kios TD { FONT: 8pt lucida; WIDTH: 23px; TEXT-ALIGN: center }
		#tb_food TD { FONT: 8pt lucida; WIDTH: 23px; TEXT-ALIGN: center }
		#tb .h { COLOR: white; BACKGROUND-COLOR: gray }
		#tb_kios .h { COLOR: white; BACKGROUND-COLOR: gray }
		#tb .lt { COLOR: black }
		#tb .ket { COLOR: black; font-size:12pt; font-weight:bold; width:auto; }
		#tb .ket2 { COLOR: black; font-size:11pt; }
		</style>
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server">
			<span class="title">Tabel Stock Tower A</span>
            <div align="center">
				<br />
				    <asp:Literal runat="server" ID="legend" Visible="true"/>
				<br/>
				<h3 style="FONT-SIZE:10pt;MARGIN:5px"></h3>
				<asp:table id="tb" runat="server" gridlines="Both" cellpadding="1"></asp:table>
			</div>
		</form>
	</body>
</html>