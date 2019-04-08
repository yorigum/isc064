<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintTKOMTemplateCF" CodeFile="PrintTKOMTemplateCF.ascx.cs" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<div style="TEXT-ALIGN: center; WIDTH: 100%">
	<h1>TANDA TERIMA CLOSING FEE</h1>
	<table>
		<tr>
			<td style="text-align:center;">
				No.Nota
				:
				<asp:label id="nomorl" runat="server" font-bold="True"></asp:label>
				<br />
				Hasil Cetak ke - <asp:Label ID="cetak" runat="server"></asp:Label>
			</td>
		</tr>
	</table>
</div>
<table>
	<tr valign="top">
		<td width="150">Unit</td>
		<td>:</td>
		<td>
			<asp:label id="unit" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Customer</td>
		<td>:</td>
		<td>
			<asp:label id="cs" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr valign="top">
		<td>Penerima</td>
		<td>:</td>
		<td>
			<asp:label id="agent" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr valign="top">
	    <td>Persentase Pelunasan</td>
	    <td>:</td>
	    <td>
	        <asp:Label id="persenlunas" runat="server" font-bold="True"></asp:Label>
	    </td>
	</tr>
	<tr>
		<td>Nilai Bayar</td>
		<td>:</td>
		<td>
			<asp:label id="nilai" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Nilai Komisi</td>
		<td>:</td>
		<td>
			<asp:label id="nilai2" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Tgl. Bayar</td>
		<td>:</td>
		<td>
			<asp:label id="tglbayar" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
	<tr>
		<td>Nama Komisi</td>
		<td>:</td>
		<td>
			<asp:label id="komisi" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
</table><br><br><br>
			<div>
				<table width="100%">
					<tr>
						<td colspan="3" align="center">
				Dibuat 
				Oleh,
			</td>
						<td colspan="3" align="center">Diperiksa Oleh,</td>
						<td colspan="3" align="center">Diperiksa Oleh,</td>
						<td colspan="3" align="center">Diterima Oleh,</td>
						<td colspan="3" align="center">Disetujui Oleh,</td>
					</tr>
					<tr>
						<td colspan="3" align="center">
							<div style="WIDTH: 100px; HEIGHT: 75px"></div>
						</td>
						<td colspan="3" align="center">
							<div style="WIDTH: 100px; HEIGHT: 75px"></div>
						</td>
						<td colspan="3" align="center">
							<div style="WIDTH: 100px; HEIGHT: 75px"></div>
						</td>
						<td colspan="3" align="center">
							<div style="WIDTH: 100px; HEIGHT: 75px"></div>
						</td>
						<td colspan="3" align="center">
							<div style="WIDTH: 100px; HEIGHT: 75px"></div>
						</td>
					</tr>
					<tr>
						<td colspan="3" align="center">
				Kasir
			</td>
						<td colspan="3" align="center">
				Controller
			</td>
						<td colspan="3" align="center">
				Finance
			</td>
			<td colspan="3" align="center">
				Marketing 
			</td>
	
						<td colspan="3" align="center">
				Direksi/GM
			</td>
					</tr>
				</table>
			</div>
