<%@ Reference Page="~/KontrakST.aspx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.ReminderST" CodeFile="ReminderST.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Reminder Terlambat Serah Terima</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Reminder - Terlambat Serah Terima">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Reminder Terlambat Serah Terima</h1>
			<p class="feed">
				<asp:label id="feed" runat="server"></asp:label>
			</p>
            <br />
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left" verticalalign="Bottom">
					<asp:tableheadercell width="75">No. Kontrak</asp:tableheadercell>
					<asp:tableheadercell width="100">Unit</asp:tableheadercell>
					<asp:tableheadercell width="150">Customer</asp:tableheadercell>
					<asp:tableheadercell width="150">Agent</asp:tableheadercell>
					<asp:tableheadercell width="100">Jadwal Serah Terima</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Pelunasan</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<table height="50">
				<tr>
					<td>
						<a href='Reminder.aspx' type="button" class="btn btn-blue t-white" value="OK"
							style="width:75px"><i class="fa fa-share"></i> OK</a>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
