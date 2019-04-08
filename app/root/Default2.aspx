<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="page1" style="float:inherit; margin-bottom:260px; padding-left:70px; padding-right:50px; page-break-after:always; width:100%">
            <table style="width:100%">
	            <tbody>
		            <tr>
			            <td style="text-align:center"><strong>SURAT PESANAN</strong></td>
		            </tr>
		            <tr>
			            <td style="text-align:center">&nbsp;</td>
		            </tr>
		            <tr>
			            <td style="text-align:center">No. @@NoKontrak</td>
		            </tr>
		            <tr>
			            <td>&nbsp;</td>
		            </tr>
	            </tbody>
            </table>
            <table style="width:100%">
	            <tbody>
		            <tr>
			            <td colspan="3">Yang bertanda tangan di bawah ini :</td>
		            </tr>
		            <tr>
			            <td style="width:20%">Nama Pemesan</td>
			            <td style="width:2%">:</td>
			            <td style="width:78%">@@NamaCustomer</td>
		            </tr>
		            <tr>
			            <td>Nomor KTP</td>
			            <td>:</td>
			            <td>@@NoKTP</td>
		            </tr>                    
		            <tr>
			            <td>NPWP</td>
			            <td>:</td>
			            <td>@@NoNPWP</td>
		            </tr>
		            <tr>
			            <td style="vertical-align:top;">Alamat (sesuai KTP)</td>
			            <td style="vertical-align:top;">:</td>
			            <td style="vertical-align:top;">@@AlamatKTP</td>
		            </tr>
		            <tr>
			            <td style="vertical-align:top;">Alamat Sekarang</td>
			            <td style="vertical-align:top;">:</td>
			            <td style="vertical-align:top;">@@Alamat</td>
		            </tr>
		            <tr>
			            <td>Nomor HP 1</td>
			            <td>:</td>
			            <td>@@NoHP</td>
		            </tr>
		            <tr>
			            <td>Nomor HP 2</td>
			            <td>:</td>
			            <td>@@NoHP2</td>
		            </tr>
		            <tr>
			            <td>Email</td>
			            <td>:</td>
			            <td>@@Email</td>
		            </tr>
                </tbody>
            </table>
            <table style="width:100%;">
                <tbody>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td style="text-align:justify;">Selanjutnya disebut "PEMESAN"/PIHAK KEDUA dengan ini sepakat untuk memesan satu unit RUMAH di SAVASA dari PT. PANAHOME DELTAMAS INDONESIA yang berkedudukan di Kelurahan HEGARMUKTI, Kecamatan CIKARANG PUSAT, Kabupaten BEKASI, yang selanjutnya disebut sebagai "PENERIMA PESANAN"/PIHAK PERTAMA</td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                </tbody>
            </table>
            <table style="width:100%">
                <tbody>
                    <tr>
                        <td style="width:20%;">Cluster</td>
                        <td style="width:2%;">:</td>
                        <td style="width:28%;"></td>
                        <td style="width:20%;">Type / Model</td>
                        <td style="width:2%;">:</td>
                        <td style="width:28%;">@@Jenis</td>
                    </tr>
                    <tr>
                        <td>Nama Jalan</td>
                        <td>:</td>
                        <td></td>
                        <td>Luas Bangunan</td>
                        <td>:</td>
                        <td>@@LuasNett</td>
                    </tr>
                    <tr>
                        <td>Nomor Unit</td>
                        <td>:</td>
                        <td>@@NoUnit</td>
                        <td>Luas Tanah</td>
                        <td>:</td>
                        <td>@@LuasSG</td>
                    </tr>
                </tbody>
            </table>
            <table style="width:100%">
	            <tbody>
                    <tr><td colspan="3">&nbsp;</td></tr>
		            <tr>
			            <td colspan="3"><strong>Harga jual & Cara Pembayaran :</strong></td>
		            </tr>
		            <tr>
			            <td style="width:20%">Harga Jual</td>
			            <td style="width:2%">:</td>
			            <td style="width:78%">Rp. @@NilaiKontrak,-(Include PPN)</td>
		            </tr>
		            <tr>
			            <td>Cara Pembayaran</td>
			            <td>:</td>
			            <td>@@CaraBayar (Jadwal Pembayaran Terlampir)</td>
		            </tr>
                </tbody>
            </table>
            <table style="width:100%;">
                <tbody>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td style="font-size:smaller;"><i>Catatan :</i></td>
                    </tr>
                    <tr>
                        <td style="text-align:justify; font-size:smaller;"><i>Dengan menandatangani Surat Pesanan ini, Pemesan telah menyetujui semua syarat-syarat dan ketentuan yang ada di balik halaman Surat Pesanan.</i></td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td>Cikarang, 01 September 2018</td>
                    </tr>                    
                    <tr><td>&nbsp;</td></tr>
                </tbody>
            </table>
            <table style="width:100%">
                <tbody>
                    <tr>
                        <td style="width:25%; text-align:center">Pemesan,</td>
                        <td colspan="2" style="width:50%; text-align:center">Mengetahui,</td>
                        <td style="width:25%; text-align:center">Menyetujui,</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height:100px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width:25%; text-align:center; vertical-align:top">(@@NamaCustomer)</td>
                        <td style="width:25%; text-align:center; vertical-align:top">(ANDIYANTO)<br /><strong>SALES EXECUTIVE</strong></td>
                        <td style="width:25%; text-align:center; vertical-align:top">(RUDY ANDREAS)<br /><strong>SALES & MARKETING MANAGER</strong></td>
                        <td style="width:25%; text-align:center; vertical-align:top">(DJONO KARJADI)<br /><strong>GENERAL MANAGER</strong></td>
                    </tr>
                </tbody>
            </table>
        </div>    
    </div>
    </form>
</body>
</html>
