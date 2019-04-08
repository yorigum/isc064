<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.MasterTagihan" CodeFile="MasterTagihan.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Master Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Master Tagihan">
	</head>
	<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="display:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan Master Tagihan
						</h1>
						<p class="pparam">
							<b>Status :</b>
							<asp:radiobutton id="statusS" runat="server" groupname="status" font-size="14" text="SEMUA"></asp:radiobutton>
							<asp:radiobutton id="statusA" runat="server" groupname="status" font-size="14" text="AKTIF" checked="True"></asp:radiobutton>
							<asp:radiobutton id="statusB" runat="server" groupname="status" font-size="14" text="BATAL"></asp:radiobutton>
						</p>
						<p class="pparam">
							<asp:radiobutton id="tglkontrak" runat="server" text="Tanggal Kontrak" font-bold="True" font-size="10"
								groupname="tgl" checked="True"></asp:radiobutton>
							:
							<br>
							<asp:radiobutton id="tgljt" runat="server" text="Tanggal Jatuh Tempo" font-bold="True" font-size="10"
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
							<asp:checkbox id="tipeCheck" runat="server" text="<b>Tipe :</b>" checked="True" autopostback="True" oncheckedchanged="tipeCheck_CheckedChanged"></asp:checkbox>
							<asp:label id="tipec" runat="server" cssclass="err"></asp:label>
						</p>
						<asp:checkboxlist id="tipe" runat="server">
							<asp:listitem selected="True" value="BF">BF = Booking Fee</asp:listitem>
							<asp:listitem selected="True" value="DP">DP = Downpayment</asp:listitem>
							<asp:listitem selected="True" value="ANG">ANG = Angsuran</asp:listitem>
							<asp:listitem selected="True" value="ADM">ADM = Biaya Administrasi</asp:listitem>
						</asp:checkboxlist>
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
					<asp:tablecell columnspan="11" font-size="8pt">
						Status : A = Aktif / B = Batal.<br>
						Tipe : BF = Booking Fee / DP = Downpayment / ANG = Angsuran / ADM = Biaya Administrasi.<br>
						Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer Bank / BG = Cek Giro / DN = Diskon.<br>
						** = Jatuh Tempo.
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Left">No. Tagihan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Status</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tipe</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tagihan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Jatuh Tempo</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Nilai Tagihan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="left">Pelunasan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Sisa Tagihan</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
