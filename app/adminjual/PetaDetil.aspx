<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.PetaDetil" CodeFile="PetaDetil.aspx.cs" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Peta Floor Plan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Setup Peta Floor Plan - Detil Peta Floor Plan">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server" class="cnt">
			<asp:image id="dasar" runat="server"></asp:image>
			<br>
			<br>
			<asp:table id="rpt" runat="server" cssclass="blue-skin tb" cellspacing="0">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="120">No. Unit</asp:tableheadercell>
					<asp:tableheadercell width="550">Koordinat</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
