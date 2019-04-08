<%@ Control Language="c#" Inherits="ISC064.LAUNCHING.PrintJadwalTagihanTemplate" CodeFile="PrintJadwalTagihanTemplate.ascx.cs" %>

<div style="width:100%">
    <table style="width:100%">
	    <tr>
		    <td style="text-align:center">
		        <h3><strong>JADWAL PEMBAYARAN</strong></h3>
		    </td>
	    </tr>
	    <tr>
		    <td>&nbsp;</td>
	    </tr>
    </table>
    <br />

    <table style="width:100%">
	    <tbody>
		    <tr>
			    <td style="width:25%">No. Surat Pesanan</td>
			    <td style="width:2%">:</td>
			    <td style="width:73%"><asp:Label ID="nokontrak" runat="server"></asp:Label></td>
		    </tr>
		    <tr>
			    <td colspan="3">&nbsp;</td>
		    </tr>
		    <tr>
			    <td>Nama Customer</td>
			    <td>:</td>
			    <td><asp:Label ID="namacs" runat="server"></asp:Label></td>
		    </tr>
		    <tr>
			    <td colspan="3">&nbsp;</td>
		    </tr>
            <tr>
			    <td>Nama Jalan</td>
			    <td>:</td>
			    <td><asp:Label ID="namajalan" runat="server"></asp:Label></td>
		    </tr>
		    <tr>
			    <td colspan="3">&nbsp;</td>
		    </tr>
		    <tr>
			    <td>No. Unit</td>
			    <td>:</td>
			    <td><asp:Label ID="nounit" runat="server"></asp:Label></td>
		    </tr>
		    <tr>
			    <td colspan="3">&nbsp;</td>
		    </tr>
		    <tr>
			    <td>Cara Bayar</td>
			    <td>:</td>
			    <td><asp:Label ID="carabayar" runat="server"></asp:Label></td>
		    </tr>
		    <tr>
			    <td colspan="3">&nbsp;</td>
		    </tr>
            <tr>
                <td>Harga Jual</td>
                <td>:</td>
                <td><asp:Label ID="dpp" runat="server"></asp:Label></td>
            </tr>
        </tbody>
    </table>

    <table style="width:100%" id="trdiskon" runat="server">
        <tbody>
            <tr>
			    <td colspan="3">&nbsp;</td>
		    </tr>
            <tr>
                <td style="width:25%">Diskon</td>
                <td style="width:2%">:</td>
                <td style="width:73%"><asp:Label ID="diskon" runat="server"></asp:Label></td>
            </tr>
            <tr>
			    <td colspan="3">&nbsp;</td>
		    </tr>
            <tr>
                <td>Harga Setelah Diskon</td>
                <td>:</td>
                <td><asp:Label ID="total" runat="server"></asp:Label></td>
            </tr>
	    </tbody>
    </table>
    <br />

    <div style="padding-left:10px;">
	    <asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3" width="90%">
		    <asp:tablerow horizontalalign="Left">
			    <asp:tableheadercell width="15%">No.</asp:tableheadercell>
			    <asp:tableheadercell width="30%">Nama Tagihan</asp:tableheadercell>
                <asp:tableheadercell width="5%">&nbsp;</asp:tableheadercell>
                <asp:tableheadercell width="5%">&nbsp;</asp:tableheadercell>
                <asp:tableheadercell width="12%" horizontalalign="Center">Nilai Tagihan</asp:tableheadercell>
                <asp:tableheadercell width="10%">&nbsp;</asp:tableheadercell>
			    <asp:tableheadercell width="20%">Tgl Jatuh Tempo</asp:tableheadercell>
                <asp:tableheadercell width="13%">&nbsp;</asp:tableheadercell>
		    </asp:tablerow>
	    </asp:table>
    </div>

    <br />

    <table style="width:90%; border-style:solid; border-width:2px; border-color:black;">
        <tr>
            <td><b>Pembayaran dilakukan melalui rekening :</b></td>
        </tr>
	    <tr>
		    <td><b>Nama Bank</b></td>
		    <td style="width:2%"><b>:</b></td>
		    <td><b><asp:Label ID="bank" runat="server"></asp:Label></b></td>
	    </tr>
        <tr>
		    <td><b>Atas Nama</b></td>
		    <td style="width:2%">:</td>
		    <td><asp:Label ID="atasnama" runat="server"></asp:Label></td>
	    </tr>
        <tr>
		    <td><b>Virtual Account</b></td>
		    <td style="width:2%">:</td>
		    <td><asp:Label ID="nova" runat="server"></asp:Label></td>
	    </tr>
    </table>

    <br />
    <table style="width:100%">
	    <tbody>
		    <tr>
			    <td style="width:30%">Cikarang, <asp:Label ID="tglkontrak" runat="server"></asp:Label></td>
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
			    <td><asp:Label ID="namacs2" runat="server"></asp:Label></td>
			    <td>&nbsp;</td>
		    </tr>
	    </tbody>
    </table>
</div>