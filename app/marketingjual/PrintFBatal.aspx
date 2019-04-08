<%@ Reference Control="~/PrintFBatalTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.PrintFBatal" CodeFile="PrintFBatal.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Print Formulir Pembatalan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Print.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="3">
		<meta name="sec" content="Print Formulir Pembatalan">
		<style type="text/css">
		#print td, #print p, #print li, #print div{font:9pt arial}
		#print h3 {font:10pt arial;font-weight:bold;padding-top:20px;}
		#print h2 {font:10pt arial;font-weight:bold;border:0px;}
		#print th {font:bold 9pt arial}
		</style>
	</head>
	<body onkeyup="if(event.keyCode==27&&document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
		<form id="Form1" method="post" runat="server">
			<div id="print">
				<asp:placeholder id="list" runat="server"></asp:placeholder>
			</div>
		</form>
	</body>
</html>
