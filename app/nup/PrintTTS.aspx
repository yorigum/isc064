<%@ Reference Control="~/PrintTTSTemplate2.ascx" %>
<%@ Page language="c#" Inherits="ISC064.NUP.PrintTTS" CodeFile="PrintTTS.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Print Tanda Terima Sementara</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Print.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="3">
		<meta name="sec" content="Print Tanda Terima Sementara">
		<%--<style type="text/css">
		#p td
		{
			font-size: 10pt;
		}
		</style>--%>
	</head>
	<body onkeyup="if(event.keyCode==27&&document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
		<script language="JavaScript" src="/Js/MD5.js"></script>
		<form id="Form1" method="post" runat="server">
			<div id="reprint" runat="server">
				<h1 style="border-bottom:1px solid silver">Otorisasi Reprint</h1>
				<p style="padding:5">
					Jenis Dokumen : <u style="font:bold 10pt">Tanda Terima Sementara</u>
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
							<asp:textbox id="username" runat="server" cssclass="txt" width="150"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td>Password</td>
						<td>:</td>
						<td>
							<asp:textbox id="pass" runat="server" cssclass="txt" width="150" textmode="Password"></asp:textbox>
						</td>
					</tr>
				</table>
				<br>
				<div class="ins">
					<table>
						<tr>
							<td>
								<asp:button id="btn" runat="server" cssclass="btn" text="Authorize" width="75" onclick="btn_Click"></asp:button>
							</td>
							<td>
								<input type="button" id="cancel" runat="server" class="btn" value="Cancel" style="width:75"
									name="cancel">
							</td>
							<td style="padding-left:10">
								<asp:label id="salah" runat="server" cssclass="err"></asp:label>
							</td>
						</tr>
					</table>
				</div>
			</div>
			<div style="display:none"><input type="button" id="cancel2" runat="server" name="cancel2"></div>
			<div id="p">
				<asp:placeholder id="list" runat="server"></asp:placeholder>
			</div>
		</form>
	</body>
</html>
