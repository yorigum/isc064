<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakEditKey" CodeFile="KontrakEditKey.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Edit Kontrak (Nomor Kontrak)</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Edit Kontrak (Nomor Kontrak)">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="body-padding pop" onkeyup="if(event.keyCode==27)window.close();"
		onload="document.getElementById('baru').select()">
		<form id="Form1" method="post" runat="server">
			<table cellspacing="5">
				<tr>
					<td>No. Kontrak</td>
					<td>:</td>
					<td>
						<asp:textbox id="baru" runat="server" cssclass="txt" width="150" maxlength="50"></asp:textbox>
						<asp:label id="baruc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr id="ppjb" runat="server">
					<td>No. PPJB</td>
					<td>:</td>
					<td>
						<asp:textbox id="noppjb" runat="server" cssclass="txt" width="150" maxlength="20"></asp:textbox>
						<asp:label id="noppjbc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr id="ajb" runat="server">
					<td>No. AJB</td>
					<td>:</td>
					<td>
						<asp:textbox id="noajb" runat="server" cssclass="txt" width="150" maxlength="20"></asp:textbox>
						<asp:label id="noajbc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
				<tr id="st" runat="server">
					<td>No. BAST</td>
					<td>:</td>
					<td>
						<asp:textbox id="nobast" runat="server" cssclass="txt" width="150" maxlength="20"></asp:textbox>
						<asp:label id="nobastc" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
			</table>
			<table height="40">
				<tr>
					<td>
						<asp:button id="save" runat="server" cssclass="btn btn-blue" text="OK" width="75" onclick="save_Click"></asp:button>
					</td>
					<td>
						<input id="cancel" type="button" class="btn btn-red" value="Cancel" onclick="window.close()">
					</td>
				</tr>
			</table>
			<input type="text" style="display:none">
		</form>
	</body>
</html>
