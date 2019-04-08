<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanRescheduleTagihan" CodeFile="LaporanRescheduleTagihan.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Reschedule Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Reschedule Tagihan">
	</head>
	<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="display:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" class="title title-line" runat="server">
							Laporan Custom Tagihan
						</h1>
						<table cellpadding="0" cellspacing="0">
							<tr valign="top">
								<td>
									<p class="pparam">
										<strong>Tgl. Custom Tagihan :</strong>
										<table>
											<tr>
												<td>Dari</td>
												<td>
													<asp:textbox id="dari" runat="server" width="85" cssclass="txt_center"></asp:textbox>
													<input class="btn" onclick="openCalendar('dari');" type="button" value="...">
												</td>
												<td rowspan="2">&nbsp;&nbsp;</td>
												<td>Sampai</td>
												<td>
													<asp:textbox id="sampai" runat="server" width="85" cssclass="txt_center"></asp:textbox>
													<input class="btn" onclick="openCalendar('sampai');" type="button" value="...">
												</td>
											</tr>
											<tr>
												<td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
												<td colspan="3"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
											</tr>
										</table>
									</p>
								</td>
								<td width="20"></td>
								<td>
									<%--<p class="pparam">
										<b>Sales Account :</b>
										<br>
										<asp:listbox id="agentinput" runat="server" cssclass="ddl" width="200" rows="15">
											<asp:listitem>SEMUA</asp:listitem>
										</asp:listbox>
									</p>--%>
								</td>
								<td width="20"></td>
								<td>
									<table cellpadding="0" cellspacing="0">
										<tr valign="top">
											<td width="40"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<div class="ins">
							<table>
								<tr>
									<td>
										<asp:button id="scr" runat="server" text="Screen Preview" CssClass="btn btn-blue" width="150" accesskey="s" onclick="scr_Click"></asp:button>
										<asp:button id="xls" runat="server" text="Download Excel" CssClass="btn btn-green" width="150" accesskey="e" onclick="xls_Click"></asp:button>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<asp:Label ID="headJudul" Runat="server"></asp:Label>
			<asp:table id="rpt" runat="server" CssClass="tb blue-skin" cellspacing="1">
				<asp:TableRow verticalalign="Middle" BackColor="LightGray">
					<asp:tableheadercell horizontalalign="Left" RowSpan="2">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" RowSpan="2">No Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" RowSpan="2">Nama</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" RowSpan="2">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" RowSpan="2">Tgl. Custom</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" RowSpan="2">User</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" ColumnSpan="5">Tagihan Sekarang</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left"></asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" ColumnSpan="6">Tagihan Sebelumnya</asp:tableheadercell>
				</asp:TableRow>
				<asp:TableRow BackColor="LightGray">
				    <asp:tableheadercell horizontalalign="Left">Skema</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. Urut</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Nama Tagihan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. Jatuh Tempo</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Nilai Tagihan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left"></asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Skema</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. Urut</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Nama Tagihan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl Jatuh Tempo</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Nilai Tagihan</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
