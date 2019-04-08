<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintTTSTemplate" CodeFile="PrintTTSTemplate.ascx.cs" %>
<div style="width: 100%; padding-top: 3cm; text-align: center;">
	<h1>TANDA TERIMA SEMENTARA</h1>
	<table>
		<tr>
			<td>
				No.
			</td>
			<td>
				:
				<asp:label id="nomorl" runat="server" font-bold="True"></asp:label>
			</td>
		</tr>
		<tr>
			<td>
				Tanggal
			</td>
			<td>
				:
				<asp:label id="tgl" runat="server" font-bold="True"></asp:label>
			</td>
		</tr>
	</table>
</div>
<table>
	<tr valign="top">
		<td width="150">Telah Diterima Dari</td>
		<td>:</td>
		<td>
			<asp:label id="cs" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Sejumlah Uang</td>
		<td>:</td>
		<td>
			<asp:label id="jumlah" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr valign="top">
		<td>Terbilang</td>
		<td>:</td>
		<td>
			<asp:label id="terbilang" runat="server" font-bold="True"></asp:label>
			<b>RUPIAH</b>
		</td>
	</tr>
	<tr>
		<td>Cara Pembayaran</td>
		<td>:</td>
		<td>
			<asp:label id="carabayar" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Surat Pesanan</td>
		<td>:</td>
		<td>
			<asp:label id="noref" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr valign="top">
		<td>Untuk Pembayaran</td>
		<td>:</td>
		<td>
			<asp:label id="pembayaran" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
</table>
<div align="right">
	<table>
		<tr>
			<td colspan="3" align="center">
				Yang Menerima :<br>
				PT. PETRA TOWN SQUARE
			</td>
		</tr>
		<tr>
			<td colspan="3" align="center">
				<div style="" style="width: 100; height: 75;"></div>
			</td>
		</tr>
		<tr>
			<td colspan="3" align="center">
				(......................................)
			</td>
		</tr>
		<tr>
			<td colspan="3" align="center">
				(Kasir / Bendahara) (Authorized Signature)
			</td>
		</tr>
	</table>
</div>
<br />
<p style="font:7pt arial">
	TANDA TERIMA SEMENTARA INI BERLAKU SAH SETELAH PEMBAYARAN DANANYA EFEKTIF 
	DITERIMA DI REKENING PT. BINTANG MILENIUM INDONESIA. TANDA TERIMA SEMENTARA INI 
	HARAP DITUKARKAN DENGAN KWITANSI RESMI.
</p>
