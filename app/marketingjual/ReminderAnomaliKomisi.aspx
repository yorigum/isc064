<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.ReminderAnomaliKomisi" CodeFile="ReminderAnomaliKomisi.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Reminder Penjualan Berubah Setelah Komisi Dikeluarkan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Reminder - Penjualan Berubah Setelah Komisi Dikeluarkan">
	</head>
	<body onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Reminder Penjualan Berubah Setelah Komisi Dikeluarkan</h1>
			<p class="feed">
				<asp:label id="feed" runat="server"></asp:label>
			</p>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow horizontalalign="Left" verticalalign="Bottom">
					<asp:tableheadercell width="75">No. Kontrak</asp:tableheadercell>
					<asp:tableheadercell width="100">Unit</asp:tableheadercell>
					<asp:tableheadercell width="150">Customer</asp:tableheadercell>
					<asp:tableheadercell width="150">Sales</asp:tableheadercell>
					<asp:tableheadercell width="120" horizontalalign="Right">Nilai Kontrak</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<table height="50">
				<tr>
					<td>
						<input id="cancel" onclick="location.href='Reminder.aspx'" type="button" class="btn" value="OK"
							style="width:75">
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
