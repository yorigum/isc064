<%@ Control Language="C#" Inherits="ISC064.MARKETINGJUAL.PrintFPSTemplate" CodeFile="PrintFPSTemplate.ascx.cs"%>
<style type="text/css">
    .style1
    {
        width: 59%;
    }
    .style2
    {}
    .style3
    {
        width: 169px;
    }
    .style6
    {
        width: 170px;
    }
    .style9
    {
        width: 75px;
    }
    .style10
    {
        width: 136px;
    }
    .style12
    {
        width: 170px;
        height: 22px;
    }
    .style13
    {
        height: 22px;
    }
    .style15
    {
        width: 75px;
        height: 22px;
    }
    .style16
    {
        width: 136px;
        height: 22px;
    }
    .style17
    {
        width: 145px;
        height: 22px;
    }
    .style18
    {
        width: 145px;
    }
</style>
<div style="WIDTH: 100%; POSITION: absolute; TEXT-ALIGN: center">
				<strong>FAKTUR PAJAK STANDAR</strong>
			</div><br /><br />
<table style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 100%; BORDER-BOTTOM: black 1px solid"
	cellpadding="5" cellspacing="0">
	<tr>
		<td>
<%--			<div style="WIDTH: 100%; POSITION: absolute; TEXT-ALIGN: center">
				<strong>FAKTUR PAJAK</strong>
			</div>--%>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr valign="middle">
					<td class="style2">Kode dan Nomor Seri Faktur Pajak :
						<asp:label id="nopajak" runat="server"></asp:label></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
	    <td style="BORDER-TOP: black 1px solid">Pengusaha Kena Pajak</td>
	</tr>
	<tr>
		<td style="BORDER-TOP: black 1px solid">
			<table>
				<tr>
					<td class="style3">Nama</td>
					<td>:</td>
					<td>PT. GRAHA RAYHAN TRI PUTRA</td>
				</tr>
				<tr>
					<td class="style3">Alamat</td>
					<td>:</td>
					<td>Jl. Pengadegan Timur IV No. 30 Rt. 006 Rw.001</td>
				</tr>
				<tr>
					<td class="style3"></td>
					<td></td>
					<td>Pengadegan, Pancoran, Jakarta Selatan</td>
				</tr>
				<tr>
					<td class="style3"></td>
					<td></td>
					<td>DKI Jakarta Raya 12770</td>
				</tr>
				<tr>
					<td class="style3">NPWP</td>
					<td>:</td>
					<td>02.399.555.8-061.000</td>
				</tr>
				<tr>
					<td class="style3">Tanggal Pengukuhan PKP</td>
					<td>:</td>
					<td>&nbsp;</td>
				</tr>
				</table>
		</td>
	</tr>
		<tr>
	    <td style="BORDER-TOP: black 1px solid">Pembeli Barang Kena Pajak/Penerima Jasa Kena Pajak</td>
	</tr>
	<tr>
		<td style="BORDER-TOP: black 1px solid">
			<table>
				<tr>
					<td class="style12">Nama</td>
					<td class="style13">:</td>
					<td class="style17">
						<asp:label id="nama" runat="server"></asp:label>
					</td>
					<td class="style15">
					</td>
					<td class="style13">
					</td>
					<td class="style16">
					</td>
				</tr>
				<tr>
					<td valign="top" class="style6">Alamat</td>
					<td valign="top">:</td>
					<td style="margin-left: 40px" class="style18">
						<asp:label id="alamat" runat="server"></asp:label>
					</td>
					<td style="margin-left: 40px" class="style9">
						&nbsp;</td>
					<td style="margin-left: 40px">
						&nbsp;</td>
					<td style="margin-left: 40px" class="style10">
						&nbsp;</td>
				</tr>
				<tr>
					<td valign="top" class="style6">NPWP</td>
					<td valign="top">:</td>
					<td style="margin-left: 40px" class="style18">
						<asp:Label ID="npwp" runat="server"></asp:Label>
					</td>
					<td style="margin-left: 40px" class="style9">
						NPPKP</td>
					<td style="margin-left: 40px">
						:</td>
					<td style="margin-left: 40px" class="style10">
						&nbsp;</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td style="BORDER-TOP: black 1px solid">
			<table style="WIDTH: 100%; BORDER: black 1px solid;" cellpadding="5" cellspacing="0">
				<tr>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid;">No. Urut</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid; width:500px" align="center">Nama 
						Barang Kena Pajak / Jasa Kena Pajak</td>
					<td align="center" style="BORDER-BOTTOM: black 1px solid; width:250px">Harga Jual/Penggantian/Uang 
                        Muka/Termin (Rp)</td>
				</tr>
				<asp:placeholder id="list" runat="server"></asp:placeholder>
			</table>
		</td>
	</tr>
	<tr>
		<td style="BORDER-TOP: black 1px solid">
			<table style="WIDTH: 100%;" cellpadding="5" cellspacing="0">
				<tr>
					<td class="style1">				
					<br/>
					<div>Pajak Penjualan Atas Barang Mewah</div><br />
                        <table cellpadding="4" cellspacing="0" style="width:300px; border: 1px solid black;">
                            <tr>
                                <td style="width:50px; border-right:1px solid black">
                                    Tarif</td>
                                <td style="width:125px; border-right:1px solid black">
                                    DPP</td>
                                <td style="width:125px;">
                                    PPnBM</td>
                            </tr>
                            <tr>
                                <td style="width:50px; border-top:1px solid black; border-right:1px solid black">
                                    ............%<br />
                                    ............%<br />
                                    ............%<br />
                                    ............%</td>
                                <td style="width:125px; border-top:1px solid black; border-right:1px solid black" >
                                    Rp. ....................<br /> 
                                    Rp. ....................<br />              
                                    Rp. ....................<br />
                                    Rp. ....................</td>
                                <td style="width:125px; border-top:1px solid black">
                                    Rp. ....................<br />
                                    Rp. ....................<br />
                                    Rp. ....................<br />
                                    Rp. ....................</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:100px; border-top:1px solid black; border-right:1px solid black">
                                    Jumlah</td>
                                <td style="width:125px; border-top:1px solid black">
                                    Rp. ....................</td>
                            </tr>
                        </table>
					
					</td>
					<td style="WIDTH: 20%" valign="top" align="right">
						<div style="TEXT-ALIGN: left">
							<br/><table style="width:250px;"><tr><td align="center">Jakarta,
							<asp:label id="tgl" runat="server"></asp:label>
							    </td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr><tr>
                                <td align="center">Muhammad Ridwan</td></tr><tr><td align="center">Direktur</td></tr></table>
							<br/>
						</div>
					</td>
				</tr>
			</table>
			<br />
		</td>
	</tr>
</table>
 <font style="font-size:12px; font-family:Arial">&nbsp;&nbsp;&nbsp;
 *) Coret yang tidak perlu</font>