<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintTTRTemplate" CodeFile="PrintTTRTemplate.ascx.cs" %>
<table width="100%" cellspacing="0" cellpadding="0" border="0">
	<tr>
        <td style="font-size: 10pt;">PT. ANDALAND PROPERTY DEVELOPMENT</td>
	</tr>
    <tr>
        <td style="font-size: 10pt;">Jl. Macan Kav. 4-5<br />Jakarta Barat<br /><br /></td>
	</tr>
    <tr valign="middle">
		<td align="center" style="font-family: Arial;">
			<font style="font-size: 14pt;"><strong>TANDA TERIMA SEMENTARA</strong></font>
		</td>
	</tr>
</table>
<br />

<table cellpadding="2" cellspacing="0">
	<tr valign="top">
		<td width="150" style="font-family: Arial; font-size: 10pt;">Telah Diterima Dari</td>
		<td style="font-family: Arial; font-size: 10pt;">:</td>
		<td style="font-family: Arial; font-size: 10pt;">Tn/Ny/Nn. 
			<asp:label id="cs" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr>
		<td style="font-family: Arial; font-size: 10pt;">Jumlah</td>
		<td style="font-family: Arial; font-size: 10pt;">:</td>
		<td style="font-family: Arial; font-size: 10pt;">Rp. 
			<asp:label id="jumlah" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr valign="top">
		<td style="font-family: Arial; font-size: 10pt;">Terbilang</td>
		<td style="font-family: Arial; font-size: 10pt;">:</td>
		<td style="font-family: Arial; font-size: 10pt;">
			<asp:label id="terbilang" runat="server" font-bold="True"></asp:label>
			<b>RUPIAH</b>
		</td>
	</tr>
	<tr>
		<td style="font-family: Arial; font-size: 10pt;">Cara Pembayaran</td>
		<td style="font-family: Arial; font-size: 10pt;">:</td>
		<td style="font-family: Arial; font-size: 10pt;">
			<asp:label id="carabayar" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
    <tr>
		<td style="font-family: Arial; font-size: 10pt;">Untuk Pembayaran</td>
		<td style="font-family: Arial; font-size: 10pt;">:</td>
		<td style="font-family: Arial; font-size: 10pt;">
			<asp:label id="ketbayar" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr style="display:none;">
		<td style="font-family: Arial; font-size: 10pt;">Reservasi</td>
		<td style="font-family: Arial; font-size: 10pt;">:</td>
		<td style="font-family: Arial; font-size: 10pt;">
			<asp:label id="noref" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr style="display:none;">
		<td style="font-family: Arial; font-size: 10pt;">Unit</td>
		<td style="font-family: Arial; font-size: 10pt;">:</td>
		<td style="font-family: Arial; font-size: 10pt;">
			<asp:label id="unit" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
</table>
<br /><br />
<table width="100%" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td style="text-align:left; width:60%;">Pembayaran Transfer ditujukan kepada :</td>
        <td style="text-align:left">Jakarta, <asp:Label ID="tgl2" runat="server" /></td>
    </tr>
    <tr>
        <td><b>PT. Andaland Property Development</b></td>
        <td rowspan="3">&nbsp;</td>
    </tr>
    <tr>
        <td><b>BCA A/C. 477-0150502</b></td>
    </tr>
    <tr>
        <td><b>Cabang Pasar Baru</b></td>
    </tr>
    <tr>
        <td colspan="2" style="height:40px;"">&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td style="height:40px;vertical-align:top">Authorized Signature</td>
    </tr>
    <tr>
        <td style="font-size:11px;">*) Coret yang tidak perlu</td>
    </tr>
</table>
