<%@ Reference Control="~/PrintJadwalTagihanReservasiTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.PrintJadwalTagihanReservasi" CodeFile="PrintJadwalTagihanReservasi1.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Print Jadwal Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Print.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="3">
		<meta name="sec" content="Print Jadwal Tagihan">
		<style type="text/css">
		#print TD { FONT: 9pt arial }
		#print P { FONT: 9pt arial }
		#print LI { FONT: 9pt arial }
		#print DIV { FONT: 9pt arial }
		#print H3 { FONT: bold 10pt arial; PADDING-TOP: 20px }
		#print H2 { BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT: bold 10pt arial; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px }
		#print TH { FONT: bold 9pt arial }
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
