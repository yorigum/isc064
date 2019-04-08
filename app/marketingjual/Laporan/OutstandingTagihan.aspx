<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.OutstandingTagihan" CodeFile="OutstandingTagihan.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Outstanding Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Outstanding Tagihan">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" cellspacing="3" width="100%" runat="server">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">Laporan Outstanding Tagihan
						</h1>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td>
									<p class="pparam"><b>Per Tanggal:</b>
									</p>
									<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
									<label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
									<asp:label id="tglc" runat="server" cssclass="err"></asp:label></td>
							</tr>
							<tr>
								<td>
									<p class="pparam"><strong>Sales:</strong>
										<br>
										<asp:dropdownlist id="ddlAgent" runat="server" cssclass="ddl" width="200">
											<asp:listitem selected="True" value="SEMUA"></asp:listitem>
										</asp:dropdownlist></p>
								</td>
							</tr>
							<tr>
								<td>
									<p class="pparam">
										<asp:checkbox id="cbPrincipal" runat="server" autopostback="True" checked="True" text="<strong>Principal:</strong>" oncheckedchanged="cbPrincipal_CheckedChanged"></asp:checkbox><asp:label id="lblPrincipal" runat="server" cssclass="err"></asp:label><asp:checkboxlist id="cblPrincipal" runat="server"></asp:checkboxlist></p>
								</td>
							</tr>
							<tr>
								<td>
									<p class="pparam"><asp:checkbox id="toponly" runat="server" text="Tampilkan berdasarkan urutan customer"></asp:checkbox></p>
								</td>
							</tr>
						</table>
						<br>
						<div class="ins">
							<table>
								<tr>
									<td><asp:button id="scr" accesskey="s" runat="server" cssclass="btn" width="100" text="Screen Preview" onclick="scr_Click"></asp:button>
										<asp:button id="xls" accesskey="e" runat="server" cssclass="btn" width="100" text="Download Excel" onclick="xls_Click"></asp:button></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow>
					<asp:tablecell columnspan="22">
						<asp:label id="lblHeader" runat="server" font-size="12pt" font-bold="True"></asp:label>
						<br />
						<asp:label id="lblSubHeader" runat="server" font-size="9pt" font-bold="True"></asp:label>
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell horizontalalign="Center" backcolor="gray" forecolor="white" rowspan="3">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" backcolor="gray" forecolor="white" rowspan="3">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" backcolor="gray" forecolor="white" rowspan="3">No. Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" backcolor="gray" forecolor="white" rowspan="3">Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" backcolor="gray" forecolor="white" rowspan="3">Sales</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" backcolor="gray" forecolor="white" rowspan="3">Agent</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="15">OUTSTANDING TAGIHAN</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" rowspan="3">Keterangan</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">>= 1 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">> 3 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">> 7 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">> 15 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">> 30 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">> 60 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" columnspan="2">> 90 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white" rowspan="2">Total</asp:tableheadercell>
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
					<asp:tableheadercell backcolor="gray" forecolor="white">Nominal</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Telat (Hari)</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Nominal</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Telat (Hari)</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Nominal</asp:tableheadercell>
					<asp:tableheadercell backcolor="gray" forecolor="white">Telat (Hari)</asp:tableheadercell>
				</asp:tablerow>
			</asp:table></form>
	</body>
</html>
