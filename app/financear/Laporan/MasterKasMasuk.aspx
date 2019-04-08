<%@ Page language="c#" Inherits="ISC064.FINANCEAR.Laporan.MasterKasMasuk" CodeFile="MasterKasMasuk.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Laporan Master Kas Masuk</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Master Kas Masuk">
	</head>
	<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="display:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Master Kas Masuk
						</h1>
						<p class="pparam">
							<b>Rekening Bank :</b>
							<br>
							<asp:dropdownlist id="lbAcc" runat="server" cssclass="ddl" width="250"></asp:dropdownlist>
						</p>
						<p class="pparam">
							<asp:checkbox id="carabayarCheck" runat="server" text="<b>Cara Bayar :</b>" checked="True" autopostback="True" oncheckedchanged="carabayarCheck_CheckedChanged"></asp:checkbox>
							<asp:label id="carabayarc" runat="server" cssclass="err"></asp:label>
						</p>
						<asp:checkboxlist id="carabayar" runat="server">
							<asp:listitem selected="True" value="TN">TN = Tunai</asp:listitem>
							<asp:listitem selected="True" value="BG">BG = Cek Giro</asp:listitem>
						</asp:checkboxlist>
						<p class="pparam">
							<b>Tanggal :</b>
						</p>
						<table>
							<tr>
								<td>dari</td>
								<td>
									<asp:textbox id="dari" runat="server" width="85" cssclass="txt_center"></asp:textbox>
									<label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
								</td>
								<td rowspan="2">&nbsp;&nbsp;</td>
								<td>sampai</td>
								<td>
									<asp:textbox id="sampai" runat="server" width="85" cssclass="txt_center"></asp:textbox>
									<label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
								</td>
							</tr>
							<tr>
								<td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
								<td colspan="3"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
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
					<asp:tablecell columnspan="7" font-size="8pt">
						Cara Bayar : TN = Tunai / BG = Cek Giro.<br>
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Left">No. Voucher</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tanggal</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Cara Bayar</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Alat Bayar</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Diterima Dari</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Keterangan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Nilai</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
