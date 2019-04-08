<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintWLTemplate" CodeFile="PrintWLTemplate.ascx.cs" %>
<div style="height: 4cm;"></div>
<table cellpadding="5">
	<tr>
		<td width="140px">Nama Pemesan</td>
		<td width="10">:</td>
		<td>
			<h3><asp:label id="nama" runat="server"></asp:label></h3>
		</td>
	</tr>
	<tr>
		<td valign="top">Alamat</td>
		<td valign="top">:</td>
		<td>
			<asp:label id="alamat" runat="server"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Telepon</td>
		<td>:</td>
		<td>
			<asp:label id="telp" runat="server"></asp:label>
		</td>
	</tr>
	<tr>
		<td colspan="3">
			<br>
			<br>
		</td>
	</tr>
	<tr>
		<td>Unit Properti</td>
		<td>:</td>
		<td>
			<h3><asp:label id="nounit" runat="server"></asp:label></h3>
		</td>
	</tr>
	<tr>
		<td>No. Urut</td>
		<td>:</td>
		<td>
			<asp:label id="nourut" runat="server" font-bold="True" font-size="40px"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Harga Jual</td>
		<td>:</td>
		<td><asp:label id="harga" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td>Terbilang</td>
		<td>:</td>
		<td><asp:label id="terbilang" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td>Cara Pembayaran</td>
		<td>:</td>
		<td>
			<asp:label id="carabyr" runat="server"></asp:label>
		</td>
	</tr>
</table>
<br>
<br>
<table width="100%" border="0">
	<tr valign="top">
		<td width="33%" align="left">
			<table cellspacing="0" cellpadding="0" border="0" width="70%">
				<tr>
					<td align="center">Pemesan</td>
				</tr>
				<tr>
					<td height="100px"></td>
				</tr>
				<tr>
					<td align="center">(&nbsp;<asp:label id="namacust" runat="server"></asp:label>&nbsp;)</td>
				</tr>
			</table>
		</td>
		<td width="33%" align="center" style="border:1px solid black">
			<i>Jakarta,&nbsp;<asp:label id="tgl" runat="server"></asp:label></i>
			<br>
			<br>
			Jam Reservasi :
			<br>
			<b style="font-size:12pt" id="masuk" runat="server"></b>
			<br>
			<br>
			Batas Waktu Pemesanan :
			<br>
			<b style="font-size:12pt" id="batas" runat="server"></b>
			<br>
			<br>
			NUP :
			<b style="font-size:12pt" id="nup" runat="server"></b>
		</td>
		<td width="33%" align="right">
			<table cellspacing="0" cellpadding="0" border="0" width="70%">
				<tr>
					<td align="center">Marketing</td>
				</tr>
				<tr>
					<td height="100px"></td>
				</tr>
				<tr>
					<td align="center">(&nbsp;<asp:label id="namaagent" runat="server"></asp:label>&nbsp;)</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<br>