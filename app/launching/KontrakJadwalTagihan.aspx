<%@ Page language="c#" Inherits="ISC064.LAUNCHING.KontrakJadwalTagihan" CodeFile="KontrakJadwalTagihan.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Jadwal Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Jadwal Tagihan">
	</HEAD>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
			<uc1:navkontrak id="NavKontrak1" runat="server" aktif="3"></uc1:navkontrak>
			<div class="tabdata">
				<div class="pad">
					<uc1:headkontrak id="HeadKontrak1" runat="server"></uc1:headkontrak>
					<table cellspacing="5">
						<tr>
							<td>
								<input type="button" id="edit" runat="server" value="Edit Tagihan" class="btn" style="WIDTH:100px"
									name="edit" accesskey="e">
							</td>
							<td style="PADDING-LEFT:10px">
								<p class="feed">
									<asp:label id="feed" runat="server"></asp:label>
								</p>
							</td>
						</tr>
					</table>
					<table cellspacing="5">
						<tr>
							<td colspan="3">
								Skema :
								<asp:label id="skema" runat="server" font-bold="True" font-size="14"></asp:label>
							</td>
						</tr>
						<tr>
							<td width="150">Nilai Kontrak</td>
							<td>:</td>
							<td width="150" align="right">
								<asp:label id="nilai" runat="server" font-bold="True"></asp:label>
							</td>
						</tr>
						<tr>
							<td>Tagihan</td>
							<td>:</td>
							<td align="right">
								<asp:label id="totaltagihan" runat="server" font-bold="True"></asp:label>
							</td>
						</tr>
						<tr>
							<td>Biaya</td>
							<td>:</td>
							<td align="right">
								<asp:label id="totalbiaya" runat="server" font-bold="True"></asp:label>
							</td>
						</tr>
						<tr>
							<td>Tagihan + Biaya</td>
							<td>:</td>
							<td align="right">
								<asp:label id="tagihanbiaya" runat="server" font-bold="True"></asp:label>
							</td>
						</tr>
						<tr id="outtr" runat="server">
							<td align="right" colspan="3">
								<font style="FONT-WEIGHT:normal; FONT-SIZE:8pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal">
									Selisih Kontrak dengan Tagihan (out of balance)</font>
								<br>
								<asp:label id="outofbalance" runat="server" font-bold="True" forecolor="Red"></asp:label>
							</td>
						</tr>
						<tr>
							<td>Pembayaran</td>
							<td>:</td>
							<td align="right">
								<asp:label id="pembayaran" runat="server" font-bold="True"></asp:label></td>
							<td></td>
						<tr valign="top">
							<td>
								Pelunasan (<asp:label id="persenlunas" runat="server" font-bold="True"></asp:label>%)
								<br>
								<font style="FONT-WEIGHT:normal; FONT-SIZE:8pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal">
									Pembayaran yang sudah cair</font>
							</td>
							<td>:</td>
							<td align="right">
								<asp:label id="pelunasan" runat="server" font-bold="True"></asp:label>
							</td>
							<td></td>
						<tr id="unatr" runat="server">
							<td align="right" colspan="3">
								<font style="FONT-WEIGHT:normal; FONT-SIZE:8pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal">
									Pembayaran tanpa alokasi (unallocated)</font>
								<br>
								<asp:label id="unallocated" runat="server" font-bold="True" forecolor="Red"></asp:label>
							</td>
						</tr>
					</table>
					<br>
					<p style="PADDING-RIGHT:3px;PADDING-LEFT:3px;FONT-WEIGHT:normal;FONT-SIZE:8pt;PADDING-BOTTOM:3px;LINE-HEIGHT:normal;PADDING-TOP:3px;FONT-STYLE:normal;FONT-VARIANT:normal">
						Tipe : BF = Booking Fee / DP = Down Payment / ANG = Angsuran / ADM = Biaya 
						Administrasi
						<br>
						Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer 
						Bank / BG = Cek Giro / DN = Diskon / KR = Kredit Kepemilikan Rumah
					</p>
					<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
						<asp:tablerow horizontalalign="Left">
							<asp:tableheadercell width="100">No.</asp:tableheadercell>
							<asp:tableheadercell>Tipe</asp:tableheadercell>
							<asp:tableheadercell width="200">Tagihan</asp:tableheadercell>
							<asp:tableheadercell width="75">Jatuh Tempo</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right">Nilai</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right">Pelunasan</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right">Sisa</asp:tableheadercell>
						</asp:tablerow>
					</asp:table>
				</div>
			</div>
		</form>
	</body>
</HTML>
