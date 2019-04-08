<%@ Reference Control="~/PrintSuratLunasTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.COLLECTION.PrintSuratLunas1" CodeFile="PrintSuratLunas1.aspx.cs" %>
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>Print Surat Lunas</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Print.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="3">
		<meta name="sec" content="Print Surat Lunas">
		<style type="text/css">
		#print TD { FONT: 11pt Calibri }
		#print P { FONT: 11pt Calibri }
		#print LI { FONT: 11pt Calibri }
		#print DIV { FONT: 11pt Calibri }
		#print H3 { FONT: bold 11pt Calibri; PADDING-TOP: 20px }
		#print H2 { BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT: bold 14pt Calibri; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px }
		#print TH { FONT: bold 14pt Calibri }
		</style>
	</HEAD>
	<body onkeyup="if(event.keyCode==27&amp;&amp;document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
		<script language="JavaScript" src="/Js/MD5.js"></script>
		<form id="Form1" method="post" runat="server">
			<div id="print">
				<asp:placeholder id="list" runat="server"></asp:placeholder>
			</div>
		</form>
	</body>
</HTML>
