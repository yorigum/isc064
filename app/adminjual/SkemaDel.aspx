<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.SkemaDel" CodeFile="SkemaDel.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Delete Skema Cara Bayar</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Setup Skema Cara Bayar - Delete Skema CB">
	</head>
	<body class="default-content" onkeyup="if(event.keyCode==27) history.back(-1)">
		<form id="Form1" method="post" runat="server">
			<div id="frm" runat="server">
				<input type="text" style="display:none">
				<h1 class="title title-line">Delete Skema Cara Bayar</h1>
				<br>
				Keterangan :
				<asp:textbox id="ket" runat="server" cssclass="txt" width="400"></asp:textbox>
				<asp:button id="delbtn" runat="server" cssclass="btn btn-red" text="Delete" onclick="delbtn_Click"></asp:button>
			</div>
			<asp:label id="nodel" runat="server" visible="false">
				<h1>
					Skema Cara Bayar Tidak Dapat Dihapus
				</h1>
				<br>
				<div class="plike">
					<h2>Kemungkinan Terjadi Karena:</h2>
					<ul>
					</ul>
				</div>
			</asp:label>
		</form>
	</body>
</html>
