<%@ Control Language="c#" Inherits="ISC064.LAUNCHING.PrintSPTemplate" CodeFile="PrintSPTemplate.ascx.cs" %>
<link href="/Media/Style.css" type="text/css" rel="stylesheet">
<style type="text/css">
   /*.header 
   {
       text-align:center;
       font-size:16px;
       margin-bottom:30px;
   }*/

   .fontheader
   {
       font-size:18pt;
       font-family:'Times New Roman', Times, serif;
   }

   .fontisi
   {
       font-size:11pt;
       font-family:'Times New Roman', Times, serif;
   }
</style>

<div style="width:100%">
    <table style="width:100%">
	    <tbody>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
		    <tr>
			    <td style="text-align:center"><strong class="fontheader"><u>SURAT PESANAN</u></strong></td>
		    </tr>
		    <tr>
			    <td style="text-align:center"><span class="fontheader"><asp:Label ID="nokontrak" runat="server"></asp:Label></span></td>
		    </tr>
		    <tr>
			    <td>&nbsp;</td>
		    </tr>
	    </tbody>
    </table>

    <table style="width:100%">
	    <tbody>
		    <tr>
			    <td colspan="3"><span class="fontisi">Yang bertanda tangan di bawah ini : </span></td>
		    </tr>
		    <tr>
			    <td style="width:20%"><span class="fontisi">Nama Pemesan</span></td>
			    <td style="width:2%"><span class="fontisi">:</span></td>
			    <td style="width:78%"><span class="fontisi"><asp:Label ID="namacs" runat="server"></asp:Label></span></td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">NIK</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="noktp" runat="server"></asp:Label></span></td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">NPWP</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="npwp" runat="server"></asp:Label></span></td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">Alamat (sesuai KTP)</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="alamatktp1" runat="server"></asp:Label></span></td>
		    </tr>
            <tr>
			    <td>&nbsp;</td>
			    <td>&nbsp;</td>
			    <td><span class="fontisi"><asp:Label ID="alamatktp2" runat="server"></asp:Label></span></td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">Alamat Sekarang</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="alamatsekarang1" runat="server"></asp:Label></span></td>
		    </tr>
            <tr>
			    <td>&nbsp;</td>
			    <td>&nbsp;</td>
			    <td><span class="fontisi"><asp:Label ID="alamatsekarang2" runat="server"></asp:Label></span></td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">Nomor HP 1</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="hp1" runat="server"></asp:Label></span></td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">Nomor HP 2</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="hp2" runat="server"></asp:Label></span></td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">Email</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="email" runat="server"></asp:Label></span></td>
		    </tr>
	    </tbody>
    </table>

    <table style="width:100%">
	    <tbody>
		    <tr>
			    <td>&nbsp;</td>
		    </tr>
		    <tr>
			    <td style="text-align:justify">
                    <span class="fontisi">
                        Selanjutnya disebut "PEMESAN" dengan ini sepakat untuk memesan satu unit <asp:Label ID="jenisproperti" runat="server"></asp:Label> 
                        di Perumahan <asp:Label ID="namaproject" runat="server"></asp:Label> dari PT. <asp:Label ID="namapers" runat="server"></asp:Label> yang 
                        berkedudukan di Kota Deltamas  (Marketing Gallery SAVASA) Green Land Square Blok BA No.1a Desa Sukamahi, Kecamatan Cikarang Pusat, Kabupaten Bekasi, 
                        yang selanjutnya disebut sebagai "PENERIMA PESANAN"
                    </span>
			    </td>
		    </tr>
		    <tr>
			    <td>&nbsp;</td>
		    </tr>
	    </tbody>
    </table>

    <table style="width:100%">
	    <tbody>
		    <tr>
			    <td style="width:20%"><span class="fontisi">Cluster</span></td>
			    <td style="width:2%"><span class="fontisi">:</span></td>
			    <td style="width:28%"><span class="fontisi"><asp:Label ID="lokasi" runat="server"></asp:Label></span></td>
			    <td style="width:20%"><span class="fontisi">Type / Model</span></td>
			    <td style="width:2%"><span class="fontisi">:</span></td>
			    <td style="width:28%"><span class="fontisi"><asp:Label ID="jenis" runat="server"></asp:Label></span></td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">Nama Jalan</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="namajalan" runat="server"></asp:Label></span></td>
			    <td><span class="fontisi">Luas Bangunan</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="luasnett" runat="server"></asp:Label> m<sup>2</sup></span></td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">Nomor Unit</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="nounit" runat="server"></asp:Label></span></td>
			    <td><span class="fontisi">Luas Tanah</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="luassg" runat="server"></asp:Label> m<sup>2</sup></span></td>
		    </tr>
	    </tbody>
    </table>

    <table style="width:100%">
	    <tbody>
		    <tr>
			    <td colspan="3">&nbsp;</td>
		    </tr>
		    <tr>
			    <td colspan="3"><strong class="fontisi">Harga Jual &amp; Cara Pembayaran :</strong></td>
		    </tr>
		    <tr>
			    <td style="width:20%"><span class="fontisi">Harga Jual</span></td>
			    <td style="width:2%"><span class="fontisi">:</span></td>
			    <td style="width:78%"><span class="fontisi">Rp. <asp:Label ID="nilaikontrak" runat="server"></asp:Label>,-(Include PPN)</span></td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">Cara Pembayaran</span></td>
			    <td><span class="fontisi">:</span></td>
			    <td><span class="fontisi"><asp:Label ID="skema" runat="server"></asp:Label> (Jadwal Pembayaran Terlampir)</span></td>
		    </tr>
	    </tbody>
    </table>

    <table style="width:100%" id="gimmicktr" runat="server">
        <tr>
		    <td style="width:20%; vertical-align:top"><span class="fontisi">Gimmick</span></td>
		    <td style="width:2%; vertical-align:top"><span class="fontisi">:</span></td>
		    <td style="vertical-align:top; text-align:left">
                <asp:table id="rpt" runat="server" HorizontalAlign="Left">
	            </asp:table>
		    </td>
	    </tr>
    </table>
	    
    <table style="width:100%">
	    <tbody>
		    <tr>
			    <td>&nbsp;</td>
		    </tr>
		    <tr>
			    <td><em class="fontisi">Catatan :</em></td>
		    </tr>
		    <tr>
			    <td style="text-align:justify">
                    <em class="fontisi">Dengan menandatangani Surat Pesanan ini, Pemesan telah menyetujui Surat Pesanan ini dan semua syarat-syarat dan ketentuan 
                        yang ada di balik halaman Surat Pesanan.</em>
			    </td>
		    </tr>
		    <tr>
			    <td>&nbsp;</td>
		    </tr>
		    <tr>
			    <td><span class="fontisi">Cikarang, <asp:Label ID="tglkontrak" runat="server"></asp:Label></span></td>
		    </tr>
	    </tbody>
    </table>

    <table style="width:100%">
	    <tbody>
		    <tr>
			    <td style="text-align:center; width:25%"><span class="fontisi"></span></td>
			    <td colspan="2" style="text-align:center; width:50%"><span class="fontisi">Mengetahui,</span></td>
			    <td style="text-align:center; width:25%"><span class="fontisi">Menyetujui,</span></td>
		    </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
		    <tr>
			    <td style="text-align:center; vertical-align:top; width:25%"><span class="fontisi">(<asp:Label ID="namacs2" runat="server"></asp:Label>)</span>
                    <br /><strong class="fontisi">(PEMESAN)</strong>
			    </td>
			    <td style="text-align:center; vertical-align:top; width:25%"><span class="fontisi">(<asp:Label ID="ag" runat="server" />)</span><br />
			        <strong class="fontisi">MARKETING</strong>
			    </td>
			    <td style="text-align:center; vertical-align:top; width:25%"><span class="fontisi">(RUDY)</span><br />
			        <strong class="fontisi">MARKETING MANAGER</strong>
			    </td>
			    <td style="text-align:center; vertical-align:top; width:25%"><span class="fontisi">(.........................................)</span><br />
			        <strong class="fontisi">GENERAL MANAGER</strong>
			    </td>
		    </tr>
	    </tbody>
    </table>
</div>