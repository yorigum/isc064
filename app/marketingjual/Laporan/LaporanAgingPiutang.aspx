<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.COLLECTION.Laporan.LaporanAgingPiutang" CodeFile="LaporanAgingPiutang.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Aging Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Aging Tagihan">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Aging Tagihan
						</h1>
						<p class="pparam">
							<b>Lokasi :</b>
							<br>
							<asp:listbox id="lokasi" runat="server" width="200" cssclass="ddl" rows="10">
								<asp:listitem selected="True">SEMUA</asp:listitem>
							</asp:listbox>
						</p>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td>
									<p class="pparam">
										<b>As Of:</b>
									</p>
									<asp:textbox id="tgl" runat="server" width="85" cssclass="txt_center"></asp:textbox>
									<label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
									<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
							<tr>
								<td>
									<p class="pparam">
										<strong>Sales:</strong>
										<br>
										<asp:dropdownlist id="ddlAgent" runat="server" cssclass="ddl" width="200">
											<asp:listitem selected="True" value="SEMUA"></asp:listitem>
										</asp:dropdownlist>
									</p>
								</td>
							</tr>
							<tr>
								<td>
									<p class="pparam">
										<asp:checkbox id="cbPrincipal" runat="server" text="<strong>Principal:</strong>" checked="True"
											autopostback="True" oncheckedchanged="cbPrincipal_CheckedChanged"></asp:checkbox>
										<asp:label id="lblPrincipal" runat="server" cssclass="err"></asp:label>
										<asp:checkboxlist id="cblPrincipal" runat="server"></asp:checkboxlist>
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
					<asp:tablecell columnspan="19">
						<asp:label id="lblHeader" runat="server" font-size="12pt" font-bold="True"></asp:label>
						<br />
						<asp:label id="lblSubHeader" runat="server" font-size="9pt" font-bold="True"></asp:label>
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell horizontalalign="Center" backcolor="gray" forecolor="white" rowspan="3">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="gray" forecolor="white" rowspan="3">No. Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="gray" forecolor="white" rowspan="3">Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="gray" forecolor="white" rowspan="3">Total</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="gray" forecolor="white" rowspan="3">Keterangan</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="8">AGING TAGIHAN</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="gray" forecolor="white" rowspan="3">Denda</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">0 - 30 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">31 - 60 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">61 - 90 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">> 91 hari</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell backcolor="gray" forecolor="white">Nominal</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Telat (Hari)</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Nominal</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Telat (Hari)</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Nominal</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Telat (Hari)</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Nominal</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Telat (Hari)</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
