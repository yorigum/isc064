<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintJadwalTagihanTemplate2" CodeFile="PrintJadwalTagihanTemplate2.ascx.cs" %>

<table style="width:100%">
	<tbody>
		<tr>
			<td style="text-align:center">
			<h3><strong>JADWAL PEMBAYARAN</strong></h3>
			</td>
		</tr>
		<tr>
			<td>&nbsp;</td>
		</tr>
	</tbody>
</table>
<br />

<table style="width:100%">
	<tbody>
		<tr>
			<td style="width:25%">No. Surat Pesanan</td>
			<td style="width:2%">:</td>
			<td style="width:73%">@@NoKontrak</td>
		</tr>
		<tr>
			<td colspan="3">&nbsp;</td>
		</tr>
		<tr>
			<td>Nama Customer</td>
			<td>:</td>
			<td>@@NamaCustomer</td>
		</tr>
		<tr>
			<td colspan="3">&nbsp;</td>
		</tr>
		<tr>
			<td>No. Unit</td>
			<td>:</td>
			<td>@@NoUnit</td>
		</tr>
		<tr>
			<td colspan="3">&nbsp;</td>
		</tr>
		<tr>
			<td>Cara Bayar</td>
			<td>:</td>
			<td>@@CaraBayar</td>
		</tr>
		<tr>
			<td colspan="3">&nbsp;</td>
		</tr>
        <tr>
            <td>Harga Jual</td>
            <td>:</td>
            <td>@@NilaiDPP</td>
        </tr>
        <tr>
			<td colspan="3">&nbsp;</td>
		</tr>
        <tr>
            <td>Diskon</td>
            <td>:</td>
            <td>@@Diskon</td>
        </tr>
        <tr>
			<td colspan="3">&nbsp;</td>
		</tr>
        <tr>
            <td>Harga Setelah Diskon</td>
            <td>:</td>
            <td>@@NilaiKontrak</td>
        </tr>
        <tr>
			<td colspan="3">&nbsp;</td>
		</tr>
	</tbody>
</table>
<br />

<table style="width:90%">
    <tr>
        <td style="text-align:left; vertical-align:top; width:5%"><b>No.</b></td>
		<td colspan="2" style="text-align:center; vertical-align:top; width:40%"><b>Nama Tagihan</b></td>
		<td colspan="2" style="text-align:center; vertical-align:top; width:25%"><b>Nilai Tagihan</b></td>
		<td style="width:5%">&nbsp;</td>
		<td style="text-align:center; vertical-align:top;"><b>Tgl Jatuh Tempo</b></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <!-- $Var(TotalTagihan) $Var(Nomor) --><!-- $StartLooping -->
	<tr><!-- $Nomor={{ $Nomor + 1 }} -->
		<td style="text-align:left; vertical-align:top; width:5%">$Print(Nomor).</td>
		<td style="text-align:left; vertical-align:top; width:38%">@@NamaTagihan</td>
		<td style="width:2%">:</td>
        <td style="text-align:right; vertical-align:top; width:5%">Rp. </td>
		<td style="text-align:right; vertical-align:top; width:17%">@@NilaiTagihan</td>
		<td style="width:8%">&nbsp;</td>
		<td style="text-align:left; vertical-align:top;">Tanggal @@TanggalJT</td>
	</tr>
	<!-- $EndLooping -->
</table>
<br />

<table style="width:100%">
	<tbody>
		<tr>
			<td>Catatan</td>
			<td style="width:2%">:</td>
			<td>Cicilan pembayaran atau pelunasan sesuai dengan tanggal diatas, apabila lewat dari tanggal jatuh tempo maka harga akan diperhitungkan kembali.</td>
		</tr>
	</tbody>
</table>

<br />
<table style="width:100%">
	<tbody>
		<tr>
			<td style="width:30%">Cikarang, @@TglKontrak</td>
			<td style="width:70%">&nbsp;</td>
		</tr>
        <tr>
			<td>Menyetujui,</td>
			<td>&nbsp;</td>
		</tr>
        <tr>
			<td colspan="2">&nbsp;</td>
		</tr>
        <tr>
			<td colspan="2">&nbsp;</td>
		</tr>
        <tr>
			<td colspan="2">&nbsp;</td>
		</tr>
        <tr>
			<td colspan="2">&nbsp;</td>
		</tr>
        <tr>
			<td>@@NamaCustomer</td>
			<td>&nbsp;</td>
		</tr>
	</tbody>
</table>
