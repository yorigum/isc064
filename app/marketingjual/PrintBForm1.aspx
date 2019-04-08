﻿<%@ Reference Control="~/PrintBFormTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.PrintBForm1" CodeFile="PrintBForm1.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Print Booking Form</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Print.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="3">
		<meta name="sec" content="Print Tanda Terima Reservasi">
		<style type="text/css">
		 
		        #print TD { FONT: 9pt arial }
	            #print P { FONT: 9pt arial }
	            #print LI { FONT: 9pt arial }
	            #print DIV { FONT: 9pt arial }
	            #print H3 { FONT: bold 10pt arial; PADDING-TOP: 20px }
	            #print H2 { BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT: bold 10pt arial; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px }
	            #print TH { FONT: bold 9pt arial }
		    #print table { padding-top:0px;padding-bottom:0px;padding-left:0px;padding-right:0px; }
		    
		    
		</style>
	</head>
	<body onkeyup="if(event.keyCode==27&&document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
		<script language="JavaScript" src="/Js/MD5.js"></script>
		<form id="Form1" method="post" runat="server">
			<asp:placeholder id="list" runat="server"></asp:placeholder>
		</form>
	</body>
</html>
