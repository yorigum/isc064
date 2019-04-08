<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintRKOMTemplate" CodeFile="PrintRKOMTemplate.ascx.cs" %>
<div style="PADDING-LEFT: 8cm">
	<h1>LAPORAN TRANSAKSI</h1>
	<table>
		<tr>
			<td>
				No. Reservasi
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
	<br>
	<br>
</div>
<table cellpadding="2">
	<tr>
		<td colspan="3">
			<h3><span style="WIDTH:30px">1.</span> SALES AGENT</h3>
		</td>
	</tr>
	<tr>
		<td width="35%">Nama Sales Agent</td>
		<td>:</td>
		<td width="65%"><asp:label id="ag" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td>Kode Sales Agent</td>
		<td>:</td>
		<td>
			<asp:label id="agid" runat="server"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Principal / Manager</td>
		<td>:</td>
		<td><asp:label id="principal" runat="server"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Alamat</td>
		<td>:</td>
		<td><asp:label id="agalamat" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td>No. Telp / HP</td>
		<td>:</td>
		<td><asp:label id="agtelp" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td>Skema Komisi Berlaku</td>
		<td>:</td>
		<td><asp:label id="skemakom" runat="server"></asp:label></td>
	</tr>
	<tr valign="top">
		<td>Komisi Penjualan</td>
		<td>:</td>
		<td>Rp.
			<asp:label id="kom" runat="server"></asp:label>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:label id="kompersen" runat="server"></asp:label>
			% dari harga jual nett sebelum pajak
			<br>
			<asp:label id="kom2" runat="server"></asp:label>
			RUPIAH
		</td>
	</tr>
	<tr>
		<td colspan="3">
			<h3><span style="WIDTH:30px">2.</span> DETAIL TRANSAKSI</h3>
		</td>
	</tr>
	<tr>
		<td>Nama Pembeli</td>
		<td>:</td>
		<td><asp:label id="nama" runat="server"></asp:label></td>
	</tr>
	<tr valign="top">
		<td>Alamat Pembeli</td>
		<td>:</td>
		<td><asp:label id="alamatsurat" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td>Telp / HP Pembeli</td>
		<td>:</td>
		<td><asp:label id="telp" runat="server"></asp:label>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:label id="hp" runat="server"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Tipe / Tower / Lantai / No. Unit</td>
		<td>:</td>
		<td><asp:label id="tipe" runat="server"></asp:label>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:label id="lokasi" runat="server"></asp:label>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:label id="unit" runat="server"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Tanggal Terima BF</td>
		<td>:</td>
		<td>
			<asp:label id="bftgl" runat="server"></asp:label>
		</td>
	</tr>
	<tr valign="top">
		<td>Jumlah BF</td>
		<td>:</td>
		<td>Rp.
			<asp:label id="bf" runat="server"></asp:label>
			<br>
			<asp:label id="bf2" runat="server"></asp:label>
			RUPIAH
		</td>
	</tr>
	<tr>
		<td>Cara Pembayaran BF</td>
		<td>:</td>
		<td><asp:label id="carabayar" runat="server"></asp:label></td>
	</tr>
	<tr valign="top">
		<td>Harga Transaksi Unit (nett sebelum pajak)</td>
		<td>:</td>
		<td>Rp.
			<asp:label id="netto" runat="server"></asp:label>
			<br>
			<asp:label id="netto2" runat="server"></asp:label>
			RUPIAH
		</td>
	</tr>
	<tr>
		<td>Rencana Cara Pembayaran Unit Apartemen</td>
		<td>:</td>
		<td><asp:label id="skema" runat="server"></asp:label></td>
	</tr>
</table>
<br>
<div align="center">
	<table>
		<tr>
			<td colspan="3" align="center">
				Jakarta,
				<% Response.Write(ISC064.Cf.Day(DateTime.Today)); %>
			</td>
		</tr>
		<tr align="center">
			<td width="300">
				Sales Agent
			</td>
			<td width="300">
				Project Sales Manager
			</td>
			<td width="300">
				Accounting Check
			</td>
		</tr>
		<tr height="60">
			<td></td>
			<td></td>
			<td></td>
		</tr>
		<tr align="center">
			<td>...................................</td>
			<td>...................................</td>
			<td>...................................</td>
		</tr>
	</table>
</div>
<div style="PAGE-BREAK-BEFORE:always"></div>
<div style="PADDING-LEFT: 8cm">
	<b>Lampiran-1</b>
	<h1>JADWAL KOMISI</h1>
	<br>
	<br>
	<br>
	<br>
	<br>
</div>
<table width="100%" border="1" style="BORDER-COLLAPSE:collapse">
	<tr align="center">
		<td width="25%">Nomor</td>
		<td width="25%">Customer</td>
		<td width="25%">Unit</td>
		<td width="25%">Sales</td>
	</tr>
	<tr align="center" height="40">
		<td><h2 id="tagno" runat="server"></h2>
		</td>
		<td><h2 id="tagcs" runat="server"></h2>
		</td>
		<td><h2 id="tagunit" runat="server"></h2>
		</td>
		<td><h2 id="tagag" runat="server"></h2>
		</td>
	</tr>
</table>
<br>
<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
	<asp:tablerow horizontalalign="Left">
		<asp:tableheadercell>No.</asp:tableheadercell>
		<asp:tableheadercell>Tipe</asp:tableheadercell>
		<asp:tableheadercell width="200">Keterangan</asp:tableheadercell>
		<asp:tableheadercell width="100">Jadwal</asp:tableheadercell>
		<asp:tableheadercell width="60">Term Cair</asp:tableheadercell>
		<asp:tableheadercell width="120" horizontalalign="Right">Nilai Komisi</asp:tableheadercell>
	</asp:tablerow>
</asp:table>
<br>
<br>
<div align="center">
	<table>
		<tr>
			<td colspan="3" align="center">
				Jakarta,
				<% Response.Write(ISC064.Cf.Day(DateTime.Today)); %>
			</td>
		</tr>
		<tr align="center">
			<td width="300">
				Sales Agent
			</td>
			<td width="300">
				Project Sales Manager
			</td>
			<td width="300">
				Accounting Check
			</td>
		</tr>
		<tr height="60">
			<td></td>
			<td></td>
			<td></td>
		</tr>
		<tr align="center">
			<td>...................................</td>
			<td>...................................</td>
			<td>...................................</td>
		</tr>
	</table>
</div>
