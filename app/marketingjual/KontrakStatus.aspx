<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakStatus" CodeFile="KontrakStatus.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Edit Kontrak (Status)</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Edit Kontrak (Status)">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="body-padding pop" onkeyup="if(event.keyCode==27)window.close();">
		<form id="Form1" method="post" runat="server">
			<table cellspacing="5">
				<tr>
					<td>
						<asp:radiobutton id="statusA" runat="server" groupname="status" font-size="14" text="AKTIF"></asp:radiobutton>
						<p style="font:8pt">Kondisi normal.</p>
						<br>
						<asp:radiobutton id="statusB" runat="server" groupname="status" font-size="14" text="BATAL"></asp:radiobutton>
						<p style="font:8pt">
							Kontrak sudah dibatalkan melalui Prosedur Pembatalan. Status tidak bisa dibalik 
							apabila unit sudah disewakan ke orang lain.</p>
					</td>
				</tr>
				<tr>
					<td>
						<br>
						<asp:checkbox id="resetPPJB" runat="server" text="Reset PPJB"></asp:checkbox>
						<br>
						<asp:checkbox id="resetAJB" runat="server" text="Reset AJB"></asp:checkbox>
						<br>
						<asp:checkbox id="resetst" runat="server" text="Reset SERAH TERIMA"></asp:checkbox>
					</td>
				</tr>
			</table>
			<table height="50">
				<tr>
					<td>
						<asp:button id="save" runat="server" cssclass="btn btn-blue" text="OK" width="75" onclick="save_Click"></asp:button>
					</td>
					<td>
						<input id="cancel" type="button" class="btn btn-red" value="Cancel" onclick="window.close()">
					</td>
					<td style="padding-left:10">
						<asp:label id="err" runat="server" cssclass="err"></asp:label>
					</td>
				</tr>
			</table>
			<br>
			<h2 style="padding:3px">Kondisi Data</h2>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="100">No.</asp:tableheadercell>
					<asp:tableheadercell>Status</asp:tableheadercell>
					<asp:tableheadercell width="250">Customer</asp:tableheadercell>
					<asp:tableheadercell width="120">Keterangan</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
