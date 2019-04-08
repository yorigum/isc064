<%@ Reference Control="~/PrintSTTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.COLLECTION.PrintST" CodeFile="PrintST.aspx.cs" %>
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
			<div id="reprint" runat="server">
				<h1 style="border-bottom:1px solid silver">Otorisasi Reprint</h1>
				<p style="padding:5px">
					Jenis Dokumen : <u style="font:bold 10pt">Surat Peringatan</u>
					<br>
					Dokumen sudah di-print sebanyak
					<asp:label id="count" runat="server"></asp:label>
					kali.
				</p>
				<br>
				<table cellspacing="5">
					<tr>
						<td>Username</td>
						<td>:</td>
						<td>
							<asp:textbox id="username" runat="server" cssclass="input-text" width="150"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td>Password</td>
						<td>:</td>
						<td>
							<asp:textbox id="pass" runat="server" cssclass="input-text" width="150" textmode="Password"></asp:textbox>
						</td>
					</tr>
				</table>
				<br>
				<div class="ins">
					<table>
						<tr>
							<td>
								<asp:button id="btn" runat="server" cssclass="btn btn-green" Text="Authorize" width="120" onclick="btn_Click"></asp:button>
							</td>
							<td>
								<input type="button" id="cancel" runat="server" class="btn btn-red" value="Cancel" style="width:75px"
									name="cancel">
							</td>
							<td style="padding-left:10px">
								<asp:label id="salah" runat="server" cssclass="err"></asp:label>
							</td>
						</tr>
					</table>
				</div>
			</div>
			<div style="display:none"><input type="button" id="cancel2" runat="server" name="cancel2"></div>
			<div id="print">
				<asp:placeholder id="list" runat="server"></asp:placeholder>
			</div>
		</form>
	</body>
</html>
