<%@ Reference Page="~/Skema.aspx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.DataCustomerSiapPPJB" CodeFile="DataCustomerSiapPPJB.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Data Konsumen Siap PPJB</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Data Customer Siap PPJB">
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Data Customer Siap PPJB
						</h1>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td>
									<p class="pparam">
										<b>Urut Customer:</b>
										<br>
										<asp:radiobuttonlist id="urut" runat="server">
											<asp:listitem value="ASC" selected="True">ASCENDING (0-9 A-Z a-z)</asp:listitem>
											<asp:listitem value="DESC">DESCENDING (9-0 Z-A z-a)</asp:listitem>
										</asp:radiobuttonlist>
									</p>
								</td>
							</tr>
							<tr>
								<td>
									<p class="pparam">
										<asp:checkbox id="skema" runat="server" text="<strong>Skema Bayar:" checked="True" autopostback="True" oncheckedchanged="skema_CheckedChanged"></asp:checkbox>
										<asp:label id="skemac" runat="server" cssclass="err"></asp:label>
										<asp:checkboxlist id="skemalist" runat="server"></asp:checkboxlist>
									</p>
								</td>
							</tr>
						</table>
						<br>
						<div class="ins">
							<table>
								<tr>
									<td>
										<asp:button id="scr" accesskey="s" runat="server" text="Screen Preview" width="100" cssclass="btn" onclick="scr_Click"></asp:button>
										<asp:button id="xls" accesskey="e" runat="server" text="Download Excel" width="100" cssclass="btn" onclick="xls_Click"></asp:button>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow>
					<asp:tablecell columnspan="13">
						<asp:label id="header" runat="server" font-size="12pt" font-bold="True"></asp:label>
						<br />
						<asp:label id="subheader" runat="server" font-size="9pt" font-bold="True"></asp:label>
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell horizontalalign="Center" backcolor="gray" forecolor="white" rowspan="2">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" backcolor="gray" forecolor="white" rowspan="2">Nama Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" backcolor="gray" forecolor="white" rowspan="2">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" backcolor="gray" forecolor="white" rowspan="2">Tipe</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" backcolor="gray" forecolor="white" rowspan="2">Harga Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" backcolor="gray" forecolor="white" rowspan="2">Sudah Bayar</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">Bayar yang akan Datang</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" rowspan="2">Nilai Denda</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="4">Cara Pembayaran</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell backcolor="gray" forecolor="white" width="150">JT</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" width="150">Nilai</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Cash Keras</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Credit<br />Developer</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">KPR Non<br />Subsidi</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">KPR<br />Subsidi</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
