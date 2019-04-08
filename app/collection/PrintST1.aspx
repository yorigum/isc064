<%@ Reference Control="~/PrintSTTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.COLLECTION.PrintST1" CodeFile="PrintST1.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Print Surat Peringatan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Print.css" type="text/css" rel="stylesheet">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="3">
		<meta name="sec" content="Print Surat Peringatan">
		<style type="text/css">
		#print td, #print p, #print, #print li{font:9pt arial}
		#print th {font:bold 9pt arial}
		</style>
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27&&document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
		<script type="text/JavaScript" src="/Js/MD5.js"></script>
		<form id="Form1" method="post" runat="server">
			<div id="print">
				<asp:placeholder id="list" runat="server"></asp:placeholder>
			</div>
		</form>
	</body>
</html>
