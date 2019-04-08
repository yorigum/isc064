<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.Laporan_LaporanWawancara" CodeFile="LaporanWawancara.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Wawancara</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Wawancara">
	</head>
    <title></title>
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Wawancara
						</h1>
						<p class="pparam">
							<b>Lokasi :</b>
							<br>
							<asp:listbox id="lokasi" runat="server" width="200" cssclass="ddl" rows="10">
							<asp:listitem selected="True">SEMUA</asp:listitem>
							</asp:listbox>
						</p>
						<%--<p class="pparam"><strong>PT :</strong>
												<br>
												<asp:dropdownlist id="pt" runat="server" width="250" cssclass="ddl" autopostback="True" onselectedindexchanged="pt_SelectedIndexChanged">
													<asp:listitem selected="True">SEMUA</asp:listitem>
												</asp:dropdownlist></p>
											<p class="pparam" id="prj" runat="server"><strong>Project :</strong>
												<br>
												<asp:dropdownlist id="proj" runat="server" width="250" cssclass="ddl" autopostback="True" onselectedindexchanged="proj_SelectedIndexChanged">
													<asp:listitem selected="True">SEMUA</asp:listitem>
												</asp:dropdownlist></p>
											<p class="pparam" id="clsr" runat="server"><strong>Cluster :</strong>
												<br>
												<asp:dropdownlist id="cluster" runat="server" width="250" cssclass="ddl">
													<asp:listitem selected="True">SEMUA</asp:listitem>
												</asp:dropdownlist></p>--%>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td>
									<p class="pparam">
										<asp:radiobutton id="tbTarget" runat="server" text="Target Wawancara" font-bold="True" font-size="10"
											groupname="tgl" checked="True"></asp:radiobutton>
										:
										<br>
										<asp:radiobutton id="tbTgl" runat="server" text="Tanggal Wawancara" font-bold="True" font-size="10"
											groupname="tgl"></asp:radiobutton>
										:
									</p>
									<table>
										<tr>
											<td>dari</td>
											<td><asp:textbox id="dari" runat="server" width="85" cssclass="txt_center"></asp:textbox><label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</td>
											<td rowspan="2">&nbsp;&nbsp;</td>
											<td>sampai</td>
											<td><asp:textbox id="sampai" runat="server" width="85" cssclass="txt_center"></asp:textbox><label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</td>
										</tr>
										<tr>
											<td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
											<td colspan="3"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
										</tr>
									</table>
									<p class="pparam">
										<strong>Status Wawancara :</strong><br>
										<asp:dropdownlist id="ddlStatus" runat="server" cssclass="ddl" width="200">
											<asp:listitem>SEMUA</asp:listitem>
											<asp:listitem>BELUM DITENTUKAN</asp:listitem>
											<asp:listitem>TIDAK PERLU</asp:listitem>
											<asp:listitem>DIJADWALKAN</asp:listitem>
											<asp:listitem>SELESAI</asp:listitem>
										</asp:dropdownlist>
									</p>
									<p class="pparam">
										<b>Rekening Bank :</b>
										<br>
										<asp:listbox id="rekening" runat="server" width="250" cssclass="ddl" rows="10">
											<asp:listitem selected="True">SEMUA</asp:listitem>
											<%-- <asp:listitem>MANDIRI</asp:listitem>
										    <asp:listitem>MANDIRI SYA</asp:listitem>
										    <asp:listitem>NISP</asp:listitem>
										    <asp:listitem>BCA</asp:listitem>
										    <asp:listitem>BCA SYA</asp:listitem>
										    <asp:listitem>BRI</asp:listitem>
										    <asp:listitem>BRI SYA</asp:listitem>
										    <asp:listitem>NIAGA</asp:listitem>
										    <asp:listitem>BII</asp:listitem>
										    <asp:listitem>BNI</asp:listitem>
										    <asp:listitem>BNI SYA</asp:listitem>
										    <asp:listitem>BAG</asp:listitem>
										    <asp:listitem>PERMATA</asp:listitem>
										    <asp:listitem>BUMI PUTERA</asp:listitem>
										    <asp:listitem>TUNAI</asp:listitem>
										    <asp:listitem>MEGA</asp:listitem>
										    <asp:listitem>MEGA SYA</asp:listitem>
										    <asp:listitem>BTN</asp:listitem>
										    <asp:listitem>BTN SYA</asp:listitem>--%>
										</asp:listbox>
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
					<asp:tablecell columnspan="10" font-size="8pt"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Left">#</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Bank KPR</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Status Wawancara</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Target Wawancara</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. Wawancara</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Lokasi Wawancara</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">KPR Yang Diajukan</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
