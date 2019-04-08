<%@ Reference Page="~/Laporan/LaporanKartuPiutang.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.COLLECTION.Laporan.LaporanKartuPiutang2" CodeFile="LaporanKartuPiutang2.aspx.cs" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Laporan Kartu Piutang</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Kartu Piutang - Hal.2">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Laporan Kartu Piutang</h1>
			<br>
			<table cellspacing="5">
				<tr>
					<td>Nomor Kontrak / Nomor Unit / Nama Customer</td><td>
						<asp:textbox id="key" CssClass="input-text" Width="250" runat="server"></asp:textbox>
					</td>
                    <td>
                        <asp:DropDownList runat="server" ID="project"></asp:DropDownList>
                    </td>
					<td>
						<asp:button id="display" runat="server" cssclass="btn btn-blue" text="Display" onclick="display_Click"></asp:button>
					</td>
				</tr>
			</table>
			<br>
			Status : A = Aktif / B = Batal
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left" verticalalign="Bottom">
					<asp:tableheadercell width="180" columnspan="2">No. Kontrak</asp:tableheadercell>
					<asp:tableheadercell width="100">Unit</asp:tableheadercell>
					<asp:tableheadercell width="75">Tgl.</asp:tableheadercell>
					<asp:tableheadercell width="200">Customer / Sales</asp:tableheadercell>
                    <asp:tableheadercell>Project</asp:tableheadercell>
					<asp:tableheadercell>Keterangan</asp:tableheadercell>                    
				</asp:tablerow>
			</asp:table>
			<script type = "text/javascript">
			function call(nomor)
			{
				popEditKontrak(nomor);
			}
			</script>
		</form>
	</body>
</html>
