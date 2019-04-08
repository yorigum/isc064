<%@ Reference Control="~/PrintFPSTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.PrintFPS" CodeFile="PrintFPS.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    	<title>Print Faktur Pajak Sederhana</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="C#"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="/Media/Print.css" type="text/css" rel="stylesheet"/>
		<meta name="ctrl" content="3"/>
		<meta name="sec" content="Print Faktur Pajak Sederhana"/>
		<style type="text/css">
		#p TD { FONT-SIZE: 10pt; FONT-FAMILY: Arial }
		</style>
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
		<script type="text/javascript" language="JavaScript" src="/Js/MD5.js"></script>
		<form id="Form1" method="post" runat="server">
			<div id="reprint" runat="server">
				<h1 style="BORDER-BOTTOM:silver 1px solid">Otorisasi Reprint</h1>
				<p style="PADDING-RIGHT:5px; PADDING-LEFT:5px; PADDING-BOTTOM:5px; PADDING-TOP:5px">
					Jenis Dokumen : <u style="FONT-WEIGHT:bold; FONT-SIZE:10pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal">
						Faktur Pajak Sederhana</u>
					<br/>
					Dokumen sudah di-print sebanyak
					<asp:label id="count" runat="server"></asp:label>
					kali.
				</p>
				<br/>
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
				<br/>
				<div class="ins">
					<table>
						<tr>
							<td>
								<asp:button id="btn" runat="server" cssclass="btn btn-green" Text="Authorize" onclick="btn_Click"></asp:button>
							</td>
							<td>
								<input type="button" id="cancel" runat="server" class="btn btn-red" value="Cancel"
									name="cancel"/>
							</td>
							<td style="PADDING-LEFT:10px">
								<asp:label id="salah" runat="server" cssclass="err"></asp:label>
							</td>
						</tr>
					</table>
				</div>
			</div>
			<div style="DISPLAY:none"><input type="button" id="cancel2" runat="server" name="cancel2"/></div>
			<div id="p">
				<asp:placeholder id="list" runat="server"></asp:placeholder>
			</div>
		</form>
	</body>
</html>
