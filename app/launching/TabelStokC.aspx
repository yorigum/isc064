
<%@ Page language="c#" Inherits="ISC064.LAUNCHING.TabelStokC" CodeFile="TabelStokC.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>SOUTH TOWER</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Refresh" content="60">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<style type="text/css">
		    A { COLOR: black }
		    A:visited { COLOR: black }
		    A:hover { COLOR: black }
		    #tb { BORDER-COLLAPSE: collapse;COLOR: gray}
		    #tb TD { FONT: 10pt Open Sans; WIDTH: 35px; height:30px; TEXT-ALIGN: center; border:2px solid #e4e4e4 }
		    #tb .h { COLOR: white; BACKGROUND-COLOR: gray; border:2px solid #e4e4e4 }
		    #tb .lt { COLOR: black;border:2px solid #e4e4e4 }
		    #tb .ket { COLOR: black; font-size:8pt; font-weight:bold; width:auto; text-align:center; }
		</style>
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server">
			<h1 class="title title-line">Tabel Stock WESTPOINT Apartment</h1>
            <div align="center">
                <div class="peach">
				    <asp:Literal runat="server" ID="legend" Visible="true"/>
                </div>
				<br/>
				<h3 style="FONT-SIZE:10pt;MARGIN:5px"></h3>
				<asp:table id="tb" runat="server" gridlines="Both" cellpadding="1"></asp:table>
			</div>
		</form>
	</body>
</html>
