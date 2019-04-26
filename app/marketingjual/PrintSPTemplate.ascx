<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintSPTemplate" CodeFile="PrintSPTemplate.ascx.cs" %>
<link href="/Media/Style.css" type="text/css" rel="stylesheet">
<style type="text/css">
    /*.header 
   {
       text-align:center;
       font-size:16px;
       margin-bottom:30px;
   }*/

    .fontheader {
        font-size: 16pt;
        font-family: 'Times New Roman', Times, serif;
    }

    .fontisi {

       font-size: 11pt;
        font-family: 'Times New Roman', Times, serif;
    }
    td {
        font-size: 11pt;
        font-family: 'Times New Roman', Times, serif;
    }

   
    </style>

<div style="width: 100%">
    <div id="hal1">
        <table style="width: 100%">
            <tbody>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center"><strong class="fontheader"><u>SURAT PESANAN</u></strong></td>
                </tr>
                <tr>
                    <td style="text-align: center"><span class="fontheader">NO:<asp:Label ID="nokontrak" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </tbody>
        </table>
        <table style="width: 100%">
            <tbody>
                <tr>
                    <td colspan="3"><span class="fontisi"><b>DATA PEMBELI</b></span></td>
                </tr>
                <tr>
                    <td style="width: 20%"><span class="fontisi">Nama </span></td>
                    <td style="width: 2%"><span class="fontisi">:</span></td>
                    <td style="width: 78%"><span class="fontisi">
                        <asp:Label ID="namacs" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td><span class="fontisi">Alamat</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="alamatktp1" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><span class="fontisi">
                        <asp:Label ID="alamatktp2" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td><span class="fontisi">Nomor KTP</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="noktp" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td><span class="fontisi">Nomor NPWP</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="npwp" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td><span class="fontisi">No.Telepon/HP</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="telpon" runat="server"></asp:Label>/<asp:Label ID="noHp" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td colspan="3"><span class="fontisi">&nbsp</span></td>
                </tr>
                <tr>
                    <td colspan="3"><span class="fontisi"><b>INFORMASI UNIT</b></span></td>
                </tr>
                <tr>
                    <td><span class="fontisi">Type Unit</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi"><asp:Label ID="jenisproperti" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td><span class="fontisi">Lantai/ Blok/ Unit</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">Lantai: <asp:Label ID="lantai_blok_unit" runat="server"></asp:Label> Unit: <asp:Label ID="lantai_blok_unit2" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td><span class="fontisi">Luas Bangunan</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi"><asp:Label ID="luasbgn" runat="server"></asp:Label> &nbsp M2</span></td>
                </tr>
                <tr>
                    <td colspan="3"><span class="fontisi">&nbsp</span></td>
                </tr>
               <tr>
                    <td colspan="3"><span class="fontisi"><b>HARGA PENGIKATAN DAN CARA PEMBAYARAN</b></span></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table style="width:100%">
                            <tr>
                                <td style="width:2%" class="fontisi">a)</td>
                                <td style="width:13%" class="fontisi">Harga Pengikatan</td>
                                <td style="width:2%" class="fontisi">:</td>
                                <td style="width:81%" class="fontisi"><asp:Label ID="hargapengikatan" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width:2%" class="fontisi">b)</td>
                                <td style="width:13%" class="fontisi">Booking Fee</td>
                                <td style="width:2%" class="fontisi">:</td>
                                <td style="width:81%" class="fontisi"><asp:Label ID="booking_fee" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width:2%" class="fontisi">c)</td>
                                <td style="width:13%" class="fontisi">Cara Bayar</td>
                                <td style="width:2%" class="fontisi">:</td>
                                <td style="width:81%" class="fontisi"><asp:Label ID="cara_bayar" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3"><span class="fontisi">&nbsp</span></td>
                </tr>
                <tr>
                    <td colspan="3"><span class="fontisi"><b>ILUSTRASI PEMBAYARAN (Lampiran I)</b></span></td>
                </tr>
                <tr>
                    <td colspan="3"><span class="fontisi">&nbsp</span></td>
                </tr>
                <tr>
                    <td colspan="3"><span class="fontisi"><b>SYARAT DAN KETENTUAN SURAT PESANAN (Lampiran II)</b></span></td>
                </tr>
            </tbody>
        </table>

        <table style="width: 100%">
            <tbody>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: justify">Pemesan dengan ini menyatakan menyetujui syarat-syarat dan ketentuan pemesanan atas unit yang ditetapkan oleh Penerima Pesanan, sebagai berikut :
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </tbody>
        </table>
        <table style="width: 100%">
            <tbody>
                <tr>
                    <td style="width:2%;vertical-align:top" class="fontisi">1)</td>
                    <td style="width:98%" class="fontisi">Harga pengikatan tersebut sudah termasuk  PPN 10 %</td>
                </tr>
                <tr>
                    <td style="width:2%;vertical-align:top" class="fontisi">2)</td>
                    <td style="width:98%" class="fontisi">Harga Pengikatan tersebut belum termasuk :</td>
                </tr>
                <tr>
                    <td style="width:2%">&nbsp</td>
                    <td style="width:98%">
                        <table style="width:100%">
                            <tr>
                                <td style="width:2%;vertical-align:top" class="fontisi">a.</td>
                                <td style="width:98%" class="fontisi">Biaya perolehan Hak atas Tanah dan Bangunan (BPHTB)</td>
                            </tr>
                            <tr>
                                <td style="width:2%;vertical-align:top" class="fontisi">b.</td>
                                <td style="width:98%" class="fontisi">Biaya notaris/PPAT untuk Akta Jual Beli dan pengurusan Balik Nama Sertifikat ke atas nama pemesan termasuk tapi tidak terbatas pada penerimaan negara bukan pajak (PNBP) terkait transaksi sesuai ketentuan yang berlaku.</td>
                            </tr>
                            <tr>
                                <td style="width:2%;vertical-align:top" class="fontisi">c.</td>
                                <td style="width:98%" class="fontisi">Pajak Bumi dan Bangunan (PBB) dan biaya lainnya yang timbul akibat peraturan perundang-undangan dan ketentuan hukum yang berlaku.</td>
                            </tr>
                            <tr>
                                <td style="width:2%;vertical-align:top" class="fontisi">d.</td>
                                <td style="width:98%" class="fontisi">Biaya pemakaian, aktivitas listrik, air, telepon dan PAM sertai Bangunan</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width:2%;vertical-align:top" class="fontisi">3)</td>
                    <td style="width:98%" class="fontisi">Surat Pesanan ini bukan merupakan tanda terima/ bukti pembayaran, dan dengan ditandatanganinya, maka Pemesan menyatakan   sudah membaca, mengetahui dan mengerti. Surat Pesanan ini berikut semua lampirannya.</td>
                </tr>
                <tr>
                    <td style="width:100%;vertical-align:top" colspan="2" class="fontisi">Demikian Surat Pesanan Ini dibuat dan ditandatangani dalam keadaan sadar, sehat jasmani maupun rohani, tanpa adanya paksaan dari pihak manapun, dan untuk dilaksanakan oleh Pemesan.</td>
                </tr>
                <tr>
                    <td style="width:100%" colspan="2" class="fontisi"><br />&nbsp</td>
                </tr>
                <tr>
                    <td style="width:100%" colspan="2" class="fontisi">Tangerang Selatan, <asp:Label ID="tgl_surat" runat="server">27 April 2019</asp:Label></td>
                </tr>
                <tr>
                    <td style="width:100%" colspan="2" class="fontisi">
                        <table style="width:100%">
                            <tr>
                                <td style="width:20%;text-align:center">Pemesan</td>
                                <td style="width:20%;text-align:center">Marketing</td>
                                <td style="width:20%;text-align:center">Penerima Pesanan</td>
                            </tr>
                            <tr>
                                <td style="width:20%;height:100px;text-align:left"></td>
                                <td style="width:20%;text-align:left"></td>
                                <td style="width:20%;text-align:left"></td>
                            </tr>
                            <tr>
                                <td style="width:20%;text-align:center">.......................</td>
                                <td style="width:20%;text-align:center">.......................</td>
                                <td style="width:20%;text-align:center">Sugino</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
    </div>
    <div id="hal2" style="page-break-before: always;">
        <table style="width:100%">
            <tr>
                <td style="text-align:center;height:40PX;vertical-align:top">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%">
                        <tr>
                            <td style="width:2%"><span class="fontisi"><b>A.</b></span></td>
                            <td style="width:98%"><span class="fontisi"><b>INFORMASI UNIT</b></span></td>
                        </tr>
                        <tr>
                            <td style="width:2%">&nbsp</td>
                            <td style="width:98%">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:10%"><span class="fontisi">Type Unit</span></td>
                                        <td style="width:2%"><span class="fontisi">:</span></td>
                                         <td><span class="fontisi"><asp:Label ID="jenispro2" runat="server"></asp:Label></span></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><span class="fontisi">Lantai/ Blok/ Unit</span></td>
                                        <td class="auto-style4"><span class="fontisi">:</span></td>
                                        <td><span class="fontisi">Lantai: <asp:Label ID="lantai1" runat="server"></asp:Label> Unit: <asp:Label ID="unit1" runat="server"></asp:Label></span></td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%"><span class="fontisi">Luas bangun</span></td>
                                        <td style="width:2%"><span class="fontisi">:</span></td>
                                        <td style="width:88%"><span class="fontisi"><asp:Label ID="luas_bangun" runat="server"></asp:Label></span></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="2">&nbsp</td></tr>
                        <tr>
                            <td style="width:2%"><span class="fontisi"><b>B.</b></span></td>
                            <td style="width:98%"><span class="fontisi"><b>HARGA PENGIKATAN DAN CARA PEMBAYARAN</b></span></td>
                        </tr>
                        <tr>
                            <td style="width:2%">&nbsp</td>
                            <td style="width:98%">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:2%;vertical-align:top"><span class="fontisi">a)</span></td>
                                        <td style="width:10%"><span class="fontisi">Harga Pengikatan</span></td>
                                        <td style="width:80%"><span class="fontisi">: <asp:Label ID="nilaikontrak1" runat="server"></asp:Label></span></td>
                                    </tr>
                                    <tr>
                                        <td style="width:2%;vertical-align:top"><span class="fontisi">b)</span></td>
                                        <td style="width:10%"><span class="fontisi">Cara Bayar</span></td>
                                        <td style="width:80%"><span class="fontisi">: <asp:Label ID="cara_bayar1" runat="server"></asp:Label></span></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="2">&nbsp</td></tr>
                        <tr>
                            <td style="width:2%"><span class="fontisi"><b>C.</b></span></td>
                            <td style="width:98%"><span class="fontisi"><b>ILUSTRASI PEMBAYARAN</b></span></td>
                        </tr>
                        <tr>
                            <td style="width:2%">&nbsp</td>
                            <td style="width:98%">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:30%"><span class="fontisi"><u><b>KETERANGAN</b></u></span></td>
                                        <td style="width:30%"><span class="fontisi"><u><b>NILAI(Rp)</b></u></span></td>
                                        <td style="width:30%"><span class="fontisi"><u><b>TGL JATUH TEMPO</b></u></span></td>
                                    </tr>
                                    <asp:placeholder id="list" runat="server"></asp:placeholder>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div style="padding-left: 10px;">

    <table cellspacing="5" style="display:none">
        <tr>
            <td>No. SP</td>
            <td>:</td>
            <td width="450" colspan="4">
                <asp:Label ID="NoKontrak1" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Nama</td>
            <td>:</td>
            <td width="450" colspan="4">
                <asp:Label ID="nama1" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Tower / Lantai / Unit</td>
            <td>:</td>
            <td width="450" colspan="4">
                <asp:Label ID="Label2" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Harga Pengikatan</td>
            <td>:</td>
            <td width="450" colspan="4">
                <asp:Label ID="nilaikontrak" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Cara Pembayaran</td>
            <td>:</td>
            <td width="450" colspan="4">
                <asp:Label ID="carabayar" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<div style="padding-left: 10px;">
    <asp:Table ID="rpt" runat="server" CellSpacing="0" Width="100%">
        <asp:TableRow HorizontalAlign="Left">
            <asp:TableHeaderCell Width="" Style="font-size:small"><b>No.</b></asp:TableHeaderCell>
            <asp:TableHeaderCell Width="" Style="font-size:small"><b>Keterangan</b></asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Right" Style="font-size:small"><b>Nilai</b></asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" Width="" Style="font-size:small"><b>Jatuh Tempo</b></asp:TableHeaderCell>
        </asp:TableRow>
    </asp:Table>
</div>
<br />
<div>
    <br />

</div>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                    <td style="width:100%" colspan="2" class="fontisi">Tangerang Selatan, <asp:Label ID="Label1" runat="server">27 April 2019</asp:Label></td>
                </tr>
                <tr>
                    <td style="width:100%" colspan="2" class="fontisi">
                        <table style="width:100%">
                            <tr>
                                <td style="width:20%;text-align:center">Pemesan</td>
                                <td style="width:20%;text-align:center">Marketing</td>
                                <td style="width:20%;text-align:center">Penerima Pesanan</td>
                            </tr>
                            <tr>
                                <td style="width:20%;height:100px;text-align:left"></td>
                                <td style="width:20%;text-align:left"></td>
                                <td style="width:20%;text-align:left"></td>
                            </tr>
                            <tr>
                                <td style="width:20%;text-align:center">.......................</td>
                                <td style="width:20%;text-align:center">.......................</td>
                                <td style="width:20%;text-align:center">Sugino</td>
                            </tr>
                        </table>
                    </td>
                </tr>
        </table>
    </div>
    <div id ="hal3" style="display:none;page-break-before: always;">
        <table style="width: 100%">
            <tbody>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: center"><strong class="fontheader"><u>SYARAT DAN KETENTUAN SURAT PESANAN</u></strong></td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">PEMESAN dengan ini menyatakan menyetujui syarat-syarat dan ketentuan pemesanan atas Bangunan yang ditetapkan oleh PENERIMA PESANAN, sebagai berikut :
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        <b>PASAL 1</b>
                        <br />
                        <b>KETENTUAN UMUM</b>
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">Harga pengikatan, cara pembayaran, dan jadwal pembayaran atas Bangunan yang telah disepakati, mengikat pada PEMESAN dan PENERIMA PESANAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">Segala gambar dan spesifikasi yang berada dalam materi promosi bukan bagian dari penawaran penjualan atau perjanjian tertulis ataupun menjadi alat pembuktian hukum dan merupakan data terakhir dalam persiapan promosi serta perubahan dapat terjadi sewaktu-waktu.</span></td>

                </tr>


                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        <b>PASAL 2</b>
                        <br />
                        <b>PEMBAYARAN</b>
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">Pembayaran dapat dilakukan dengan cara bayar tunai keras, tunai bertahap atau secara angsuran/cicilan melalui PENERIMA PESANAN atau pihak ketiga lainnya (termasuk bank).</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">PEMESAN tunduk dan terikat pada syarat-syarat dan ketentuan-ketentuan yang ditetapkan oleh PENERIMA PESANAN, termasuk kewajiban PEMESAN untuk melunasi setiap pembayaran (uang muka dan/atau pembayaran angsuran cicilan secara penuh (dalam arti tidak kurang dan tanpa potongan apapun) dan tepat waktu (dalam arti tidak melampau tanggal jatuh tempo) sesuai jadwal pembayaran.</span></td>

                </tr>

                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Pembayaran uang tanda jadi (booking fee) yang telah disetor kepada PENERIMA PESAN, tidak dapat dikembalikan kepada PEMESAN bilamana PEMESAN telah melakukan pembatalan pembelian dan atau tidak melakukan kewajiban berikutnya kepada PENERIMA PESAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">Pengembalian uang yang terjadi akibat di lakukannya pembatalan oleh PEMESAN atau akibat kelalaian dari PEMESAN yang tidak melakukan kewajibannya, maka uang yang telah masuk ke PENERIMA PESAN (selain booking fee atau uang tanda jadi) selanjutnya akan dikembalikan setelah unit tersebut terjual kembali (mengacu pasal 3.F dan 5.E) dan uang pengembalian telah cukup untuk pengembalian kepada PEMESAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">E.</td>
                    <td colspan="5"> <span class="fontisi">Seluruh pembayaran wajib disetorkan secara penuh tanpa potongan dengan memberikan keterangan (Nama Proyek, Tipe Unit, Nomor Unit, Lantai, Nama Pemesan serta informasi DP/angsuran/ cicilan keberapa) dan kemudian di transfer ke dalam rekening PENERIMA PESAN  sebagai berikut ;</span></td>

                </tr>
                <tr>
                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top"></td>
                    <td colspan="6">
                        <table style="width:100%">
                            <tr>
                                <td style="width:40%">
                                    <table style="width:100%">
                                        <tr>
                                            <td style="border-left:solid;border-right:solid;border-top:solid;border-bottom:solid;">
                                                <table style="width:100%">
                                                    <tr>
                                                        <td class="fontisi" style="width:18%">Nama Bank</td>
                                                        <td class="fontisi" style="width:2%">:</td>
                                                        <td class="fontisi" style="width:80%">Mandiri</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="fontisi" style="width:18%">No Rekening</td>
                                                        <td class="fontisi" style="width:2%">:</td>
                                                        <td class="fontisi" style="width:80%">155.00.5777800.6</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="fontisi" style="width:18%">Atas Nama</td>
                                                        <td class="fontisi" style="width:2%">:</td>
                                                        <td class="fontisi" style="width:80%">PT Serpong Bangun Cipta</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:10%"></td>
                                <td style="width:40%">
                                    <table style="width:100%">
                                        <tr>
                                            <td style="border-left:solid;border-right:solid;border-top:solid;border-bottom:solid;">
                                                <table style="width:100%">
                                                    <tr>
                                                        <td class="fontisi" style="width:18%">Nama Bank</td>
                                                        <td class="fontisi" style="width:2%">:</td>
                                                        <td class="fontisi" style="width:80%">BCA</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="fontisi" style="width:18%">No Rekening</td>
                                                        <td class="fontisi" style="width:2%">:</td>
                                                        <td class="fontisi" style="width:80%">497.757.7780</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="fontisi" style="width:18%">Atas Nama</td>
                                                        <td class="fontisi" style="width:2%">:</td>
                                                        <td class="fontisi" style="width:80%">PT Serpong Bangun Cipta</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">F.</td>
                    <td colspan="5"> <span class="fontisi">Pembayaran menggunakan Cek (cheque) atau bilyet giro, dapat dilakukan di kantor pemasaran setiap hari kerja (Senin-Jum’at) Jam 09.00 – 16.00 WIB dan akan dianggap sah bilamana PENERIMA PEMESAN telah mengeluarkan kwitansi resmi yang berstempel perusahaan dan bernomor register.</span></td>

                </tr>
                <tr>

                    <td class="auto-style1"></td>
                    <td style="vertical-align:top" class="auto-style1">G.</td>
                    <td colspan="5" class="auto-style2"> <span class="fontisi">PEMESAN tidak diperkenankan melakukan pembayaran diluar butir 2.E dan 2.F, termasuk kepada sales atau pihak-pihak lain. PENERIMA PESANAN tidak bertanggung jawab atas pembayaran di luar butir tersebut di atas.</span>
                        <br />
                        <br />
                    </td>

                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        <b>PASAL 3<br />
                        KREDIT PEMILIKAN RUKO ATAU TOKO (KPR/KPK)</b>&nbsp;
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">Dalam hal penggunaan fasilitas kredit bank, PEMESAN menyadari dan mengakui bahwa PENERIMA PESANAN hanya perantara PEMESAN sehingga segala akibat dan resiko yang berkaitan dengan permohonan fasilitas pinjaman kredit bank merupakan beban dan tanggung jawab PEMESAN sepenuhnya serta tidak dapat dikaitkan dan dibebankan kepada PENERIMA PESANAN karena sebab atau alasan apapun.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">Pembayaran menggunakan fasilitas Kredit Bank (KPR/KPK), PEMESAN wajib melengkapi persyaratan yang dibutuhkan oleh pihak bank pemberi kredit selambatnya 2 (dua) minggu setelah penandatangan Surat Pesanan ini.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Bilamana Bank pemberi kredit telah menyetujui untuk memberikan fasilitas KPR/KPK kepada PEMESAN, maka dalam jangka waktu 14 (empat belas) hari kalender setelah tanggal persetujuan Bank atau hari yang ditentukan oleh Bank, PEMESAN berkewajiban untuk menandatangani akta perjanjian kredit/akta pengakuan hutang di hadapan Notaris yang ditunjuk atau disetujui oleh Bank dan PENERIMA PESANAN serta wajib memenuhi semua persyaratan kredit yang ditentukan Bank.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">Jangka waktu dan besarnya angsuran atau plafon kredit yang disetujui menjadi kewenangan pihak Bank pemberi kredit. Apabila plafon kredit lebih rendah dari yang dimohon, maka penambahan uang muka menjadi kewajiban PEMESAN dan sanggup melunasi penambahan uang muka sebelum akad kredit dilaksanakan. Apabila PEMESAN tidak sanggup maka dinyatakan pemesanan Bangunan menjadi batal dengan sendirinya.</span></td>

                </tr>
                 <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">E.</td>
                    <td colspan="5"> <span class="fontisi">Jika persetujuan atas pemberian fasilitas kredit (KPR/KPK) ditolak bank, maka PEMESAN diberi kesempatan untuk melakukan pembelian dengan cara bayar cash keras/cash bertahap berdasarkan ketentuan yang berlaku. Dan bilamana PEMESAN tidak memenuhi ketentuan pembayaran dengan cara bayar cash keras/cash bertahap tersebut, maka PEMESAN sepakat untuk membatalkan SURAT PESANAN ini.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">F.</td>
                    <td colspan="5"> <span class="fontisi">Apabila terjadi pembatalan terkait dengan KPR/KPK sebagaimana pada butir di atas, maka PENERIMA PESANAN berhak membatalkan secara sepihak pemesanan atas Bangunan, dengan melepas ketentuan dalam pasal 1266 dan 1267 Kitab Undang-Undang Hukum Perdata, dan dalam hal ini jumlah uang yang telah dibayarkan oleh PEMESAN diperhitungkan sebagai kompensasi ganti kerugian kepada PENERIMA PESANAN akibat pembatalan ini serta PENERIMA PESANAN  memiliki hak secara penuh dan berwenang untuk memasarkan dan menjual kembali Bangunan tersebut kepada pihak lain tanpa harus persetujuan PEMESAN.</span></td>

                </tr>
                 <tr>
                    <td colspan="5" style="text-align: center">
                        <br />
                        <b>PASAL 4<br />
                        DENDA KETERLAMBATAN</b>&nbsp;
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">Apabila PEMESAN tidak melakukan kewajibannya dalam pembayaran dan/atau terlambat membayar atau kurang membayar angsuran/cicilan sesuai tanggal jatuh tempo kewajiban pembayaran, maka PEMESAN dikenakan denda keterlambatan sebesar 1‰ (satu permil) per hari terhitung dari jumlah pembayaran tertunggak.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">PENERIMA PESANAN tidak berkewajiban untuk memberitahukan dan atau mengingatkan PEMESAN, apabila Pemesan terlambat atau kurang membayar sesuai jadwal dan jumlah pembayaran yang telah disepakati.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Bilamana PEMESAN tidak segera melunasi pembayaran yang tertunggak beserta dendanya dan PENERIMA PESANAN akan memberikan pemberitahuan melalui telepon atau alat komunikasi lainnya baik melaui media online maupun media surat yang akan dikirimkan sesuai alamat PEMESAN yang tercantum dalam SURAT PESANAN ini dan bilamana PENERIMA PESAN telah mengirimkan Surat Peringatan mengenai tunggakan tersebut sebanyak 1 (satu) kali namun tidak ada respon/tanggapan dari PEMESAN, maka dengan lewatnya waktu saja membuktikan bahwa PEMESAN lalai melakukan kewajibannya sehingga PEMESAN dianggap membatalkan pesanan ini.</span></td>

                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        <br />
                        <b>PASAL 5<br />
                        PEMBATALAN</b>&nbsp;
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">SURAT PESANAN ini batal apabila PEMESAN mengakhiri SURAT PESANAN atau dalam hal PEMESAN lalai atau tidak memenuhi kewajibannya, termasuk tetapi tidak terbatas pada kewajiban pembayaran angsuran atas Bangunan, biaya dan pajak, serta denda (jika ada).</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">PARA PIHAK sepakat mengesampingkan Pasal 1266 dan Pasal 1267 Kitab Undang-Undang Hukum Perdata (yang mana dalam hal terjadi pengakhiran sepihak atas SURAT PESANAN, tidak memerlukan putusan/penetapan dari Pengadilan), serta Pasal 1813, Pasal 1814, dan Pasal 1816 Kitab Undang-Undang Hukum Perdata (mengenai berakhinya pemberian kuasa).</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Sebagai akibat batalnya SURAT PESANAN maka pajak-pajak yang telah dibayar oleh PEMESAN akan hangus dan tidak dapat diganti atau dialihkan ke pihak lain/pihak ketiga, termasuk tetapi tidak terbatas kepada PENERIMA PESANAN. Selain itu, apabila terdapat PPH FINAL yang sudah dibayar oleh PENERIMA PESANAN atas Bangunan; maka PEMESAN wajib mengganti kepada PENERIMA PESANAN, uang sebesar (sama jumlahnya dengan) PPH FINAL yang sudah dibayar tersebut.</span></td>
                </tr>
                 <tr>
                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">PEMESAN wajib menandatangani dokumen-dokumen sehubungan dengan pembatalan SURAT PESANAN ini serta sepakat untuk melunasi seluruh biaya/pajak/ongkos/denda (jika ada) dan PEMESAN wajib mengembalikan semua kuitansi serta berkas-berkas lainnya yang berkenaan dengan Bangunan ini.</span></td>
                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">E.</td>
                    <td colspan="5"> <span class="fontisi">Apabila terjadi keterlambatan pembayaran yang melewati 30 (tiga puluh) hari terhitung sejak tanggal jatuh tempo pembayaran serta PENERIMA PESANAN telah mengirimkan Surat Peringatan mengenai keterlambatan pembayaran tersebut sebanyak 1 (satu) kali atau apabila PEMESAN membatalkan pemesanan unit secara sepihak dengan berbagai alasan maka PENERIMA PESANAN berhak membatalkan secara sepihak pemesanan atas Bangunan, dengan melepas ketentuan dalam pasal 1266 dan 1267 Kitab Undang-Undang Hukum Perdata, dan uang yang telah dibayarkan oleh PEMESAN kepada PENERIMA PESANAN diperhitungkan sebagai kompensasi ganti kerugian kepada PENERIMA PESANAN akibat pembatalan ini dan membebaskan PENERIMA PESANAN dari segala tuntutan serta PENERIMA PESANAN berhak secara penuh dan berwenang memasarkan dan menjual OBYEK PESANAN tersebut kepada pihak lain tanpa harus adanya persetujuan dari PEMESAN.</span></td>

                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        <br />
                        <b>PASAL 6<br />
                        PERJANJIAN PENGIKATAN JUAL BELI (PPJB)</b>&nbsp;
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">PENERIMA PESANAN akan mengirimkan pemberitahuan secara tertulis kepada PEMESAN untuk segera menandatangani PPJB bilamana pembayaran Bangunan telah mencapai 30% (tiga puluh persen) dari harga Pengikatan atas Bangunan.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">B.Apabila PEMESAN tidak mengindahkan 2 (dua) kali pemberitahuan tertulis yang dikirimkan oleh PENERIMA PESANAN dengan tenggang waktu masing-masing 7 (tujuh) hari kalender, maka PEMESAN dengan ini menyatakan dan memberikan kuasa penuh kepada PENERIMA PESANAN, kuasa yang dimaksud dalam hal ini adalah diberikan dengan hak substitusi untuk: bertindak dan atas nama PEMESAN untuk menandatangani PPJB dan; untuk menyerahkan PPJB tersebut kepada PEMESAN. Atas penandatanganan PPJB tersebut PENERIMA PESANAN bebas dan terlepas dari segala somasi, tuntutan, gugatan, baik perdata maupun pidana, baik dari PEMESAN maupun dari pihak lain. </span></td>

                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        <br />
                        <b>PASAL 7<br />
                        PERUBAHAN NAMA</b>&nbsp;
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td colspan="5">
                        Perubahan nama dalam SURAT PESANAN oleh PEMESAN harus mendapat persetujuan tertulis terlebih dahulu dari PENERIMA PESANAN. Perubahan nama dilakukan dengan ketentuan sebagai berikut :
                    </td>
                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">Sebelum PPJB ditandatangani, PEMESAN dapat melakukan perubahan nama dalam SURAT PESANAN dengan ketentuan bahwa maksimum perubahan dilakukan sebanyak 1 (satu) kali dan perubahan nama tersebut dilakukan kepada pihak lain yang mempunyai alamat yang sama dengan alamat PEMESAN (sesuai dengan Kartu Tanda Penduduk) dan namanya tercantum dalam Kartu Keluarga PEMESAN dan merupakan pasangan yang sah secara hukum dari PEMESAN tanpa adanya perjanjian pisah harta (prenuptial agreement/postnuptial agreement), dan/atau merupakan anak kandung, orang tua, yang sah dari PEMESAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">Setelah penandatanganan PPJB, perubahan nama PEMESAN akan dikenakan biaya sebesar 2% (dua persen) dari harga pengikatan atas Bangunan.</span></td>

                </tr>
                <tr>
                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Perubahan nama kepada pihak lain yang bukan merupakan anak kandung atau orangtua kandung atau pasangan yang sah menurut hukum dari PEMESAN akan dikenakan biaya sebesar Rp. 1.000.000,- (satu juta rupiah) bila belum dilakukan penandatanganan PPJB dan biaya sebesar 3% (tiga persen) dari harga pengikatan atas Bangunan bila telah dilakukan PPJB.</span></td>

                </tr>
                <tr>
                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">Butir 7.B. tersebut diatas tidak berlaku, apabila telah dilakukan penandatanganan Akta Jual Beli dihadapan Pejabat Pembuat Akta Tanah (PPAT).</span></td>

                </tr>
                <tr>
                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">E.</td>
                    <td colspan="5"> <span class="fontisi">Periode Bebas Biaya Ganti Nama adalah dari tanggal 01 November 2019 sampai dengan 30 Juni 2020.</span></td>

                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        <br />
                        <b>PASAL 8<br />
                        BERITA ACARA SERAH TERIMA (BAST)</b>&nbsp;
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">Proses penyerahan Bangunan akan dijabarkan dalam Perjanjian Pengikatan Jual Beli (PPJB).</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">B.Serah Terima Bangunan secara fisik wajib dilaksanakan oleh PEMESAN atas pemberitahuan secara tertulis dari PENERIMA PESANAN (atau petugas/pejabatnya) dan dituangkan dalam suatu Berita Acara Serah Terima (“BAST”) yang telah ditetapkan, setelah Bangunan telah siap untuk dilakukan serah terima. Untuk maksud tersebut PEMESAN wajib memenuhi semua syarat-syarat dan ketentuan-ketentuan (termasuk pelunasan Harga Pengikatan, denda dan kewajiban pembayaran lainnya (Jika ada) yang ditetapkan oleh PENERIMA PESANAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Apabila dalam jangka waktu 7 (tujuh) hari setelah tanggal pemberitahuan untuk serah terima ternyata PEMESAN tidak datang dan tidak menandatangani Berita Acara Serah Terima (“BAST”) karena sebab/alasan apapun, maka PEMESAN dianggap menyetujui bahwa penyerahan Bangunan telah dilakukan dan dalam hal demikian bukti pengiriman surat pemberitahuan untuk melaksanakan serah terima Bangunan tersebut dianggap telah merupakan bukti yang cukup bahwa serah terima Bangunan telah dilaksanakan pada hari ke 7 (tujuh) setelah tanggal pemberitahuan tersebut disampaikan.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">PENERIMA PESANAN bertanggung jawab menyelesaikan Bangunan dalam kondisi baik.</span></td>

                </tr>

                <tr>
                    <td colspan="5" style="text-align: center">
                        <br />
                        <b>PASAL 9<br />
                        LAIN-LAIN</b>&nbsp;
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">Hal-hal yang tidak atau belum diatur dalam SURAT PESANAN ini akan diatur lebih lanjut dalam Perjanjian Pengikatan Jual Beli dan Perjanjian-Perjanjian lain yang dibuat sehubungan Bangunan yang tunduk pada hukum yang berlaku di Negara Indonesia.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">Apabila terjadi perbedaan pendapat atau perselisihan antara PARA PIHAK, baik dalam penafsiran maupun pelaksanaan ketentuan dalam SURAT PESANAN ini, maka PARA PIHAK sepakat untuk menyelesaikannya secara musyawarah untuk mencapai mufakat. Dan bilamana PARA PIHAK tidak mencapai musyawarah untuk mufakat maka semua perselisihan atau sengketa yang timbul/terjadi oleh PARA PIHAK, baik yang bersifat teknis maupun non teknis akan diselesaikan melalui jalur hukum pada instansi yang berwenang.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Surat Pesanan ini merupakan lampiran yang tidak terpisahkan di dalam Perjanjian Pengikatan Jual Beli (PPJB).</span></td>

                </tr>
                <tr>
                    <td colspan="5">
                        Demikian Surat Pesanan ini dibuat dan ditandatangani dalam keadaan sadar, sehat jasmani maupun rohani, tanpa adanya paksaan dari pihak manapun, dan untuk dilaksanakan oleh Pemesan.
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
        <br />
    <br />
    <br />
    <table style="width: 100%;display:none">
        <tbody>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width:25%;text-align:center"><span class="fontisi">Tangerang Selatan,
                    <%--<asp:Label ID="tglkontrak" runat="server"></asp:Label></span></td>--%>
                    27 April 2019</td>
                <td></td>
                <td style="width:30%">Menyetujui</td>
            </tr>
        </tbody>
    </table>

    <table style="width: 100%;display:none">
        <tbody style="">
            <tr>
                <td style="text-align: center; width: 25%"><span class="fontisi">Pemesan</span></td>
                <td colspan="2" style="text-align: center; width: 50%">&nbsp;</td>
                <td style="text-align: center; width: 25%"><span class="fontisi">PT.SERPONG BANGUN CIPTA</span></td>
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
                <td style="text-align: center; vertical-align: top; width: 25%"><span class="fontisi">(<asp:Label ID="namacs2" runat="server"></asp:Label>)</span>
                    <br />
                </td>
                <td style="text-align: center; vertical-align: top; width: 25%">&nbsp;</td>
                <td style="text-align: center; vertical-align: top; width: 25%">&nbsp;</td>
                <td style="text-align: center; vertical-align: top; width: 25%"><span class="fontisi">(.........................................)</span>
                </td>
            </tr>
            <tr>
                <td><br /><br /></td>
            </tr>
            
        </tbody>
    </table>

    <table style="width:100%">
        
        <tr>
                <td style="width:1%">CC</td>
                <td style="width:5%">Asli</td>
                <td style="width:1%">:</td>
                <td style="width:25%">Bagian Keuangan/Finance</td>
            </tr>
        <tr>
                <td style="width:5%"></td>
                <td style="width:5%">Copy 2</td>
                <td style="width:1%">:</td>
                <td style="width:25%">Sales Adm.Proyek</td>
            </tr>
        
        <tr>
                <td style="width:5%"></td>
                <td style="width:5%">Copy 3</td>
                <td style="width:1%">:</td>
                <td style="width:25%">Pemesan</td>
            </tr>
        
        <tr>
                <td style="width:1%"></td>
                <td style="width:5%">Sales</td>
                <td style="width:1%">:</td>
                <td style="width:25%"><asp:Label ID="salesname" runat="server">sales</asp:Label></td>
            </tr>
    </table>
</div>
