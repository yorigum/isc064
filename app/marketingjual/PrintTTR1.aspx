<%@ Reference Control="~/PrintTTRTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.PrintTTR1" CodeFile="PrintTTR1.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Print Tanda Terima Reservasi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Print.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="3">
		<meta name="sec" content="Print Tanda Terima Reservasi">
	</head>
	<body onkeyup="if(event.keyCode==27&&document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
		<script language="JavaScript" src="/Js/MD5.js"></script>
		<form id="Form1" method="post" runat="server">
			<asp:placeholder id="list" runat="server"></asp:placeholder>
		</form>
	</body>
</html>
