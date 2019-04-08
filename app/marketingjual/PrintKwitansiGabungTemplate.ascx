<%@ Reference Page="~/Skema.aspx" %>
<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintKwitansiGabungTemplate" CodeFile="PrintKwitansiGabungTemplate.ascx.cs" %>
<div style="PADDING-LEFT: 8cm; PADDING-TOP: 3cm">
	<h1>KWITANSI</h1>
	<table>
		<tr>
			<td>No.
			</td>
			<td>:
				<asp:label id="nomorl" font-bold="True" runat="server"></asp:label></td>
		</tr>
		<tr>
			<td>Tanggal
			</td>
			<td>:
				<asp:label id="tgl" font-bold="True" runat="server"></asp:label></td>
		</tr>
	</table>
</div>
<table>
	<tr vAlign="top">
		<td width="150">Telah Diterima Dari</td>
		<td>:</td>
		<td><asp:label id="cs" font-bold="True" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td>Sejumlah Uang</td>
		<td>:</td>
		<td><asp:label id="jumlah" font-bold="True" runat="server"></asp:label></td>
	</tr>
	<tr vAlign="top">
		<td>Terbilang</td>
		<td>:</td>
		<td><asp:label id="terbilang" font-bold="True" runat="server"></asp:label><b>RUPIAH</b>
		</td>
	</tr>
	<tr>
		<td>Surat Pesanan</td>
		<td>:</td>
		<td><asp:label id="noref" font-bold="True" runat="server"></asp:label></td>
	</tr>
	<tr vAlign="top">
		<td>Untuk Pembayaran</td>
		<td>:</td>
		<td><asp:label id="pembayaran" font-bold="True" runat="server"></asp:label></td>
	</tr>
</table>
<div align="right">
	<table>
		<tr>
			<td align="center" colSpan="3">Yang Menerima :<br>
				PT. PETRA TOWN SQUARE
			</td>
		</tr>
		<tr>
			<td align="center" colSpan="3">
				<div style="BORDER-RIGHT: black 1px dotted; BORDER-TOP: black 1px dotted; BORDER-LEFT: black 1px dotted; WIDTH: 100px; BORDER-BOTTOM: black 1px dotted; HEIGHT: 75px; TEXT-ALIGN: center">Materai<br>
					6000</div>
			</td>
		</tr>
		<tr>
			<td align="center" colSpan="3">(......................................)
			</td>
		</tr>
		<tr>
			<td align="center" colSpan="3">(Kasir / Bendahara) (Authorized Signature)
			</td>
		</tr>
	</table>
</div>
