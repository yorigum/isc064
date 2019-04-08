<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.UnitLokasi" CodeFile="UnitLokasi.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadUnit" Src="HeadUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUnit" Src="NavUnit.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Lokasi Unit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Unit - Lokasi Unit">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27) document.getElementById('cancel').click()">
		<form id="Form1" method="post" runat="server" class="cnt">
			<asp:image id="unit" runat="server"></asp:image>
			<div id="noimg" runat="server" style="padding:45px;width:100%">
				<h2>Peta Lokasi Belum Tersedia</h2>
				<p style="font:8pt">Silakan menghubungi administrasi untuk melakukan setup floor 
					plan.</p>
			</div>
			<table height="50">
				<tr>
					<td>
                        <button class="btn btn-blue" style="width:75px;" OnClick="window.close()"><i class="fa fa-share"></i> OK</button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
