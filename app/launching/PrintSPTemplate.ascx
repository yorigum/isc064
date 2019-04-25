<%@ Control Language="c#" Inherits="ISC064.LAUNCHING.PrintSPTemplate" CodeFile="PrintSPTemplate.ascx.cs" %>
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

    p.MsoNormal {
        margin-bottom: 8,0000pt;
        line-height: 107%;
        font-family: Calibri;
        font-size: 11,0000pt;
    }
    .auto-style1 {
        width: 5%;
        height: 76px;
    }
    .auto-style2 {
        height: 76px;
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
                    <td style="text-align: center"><strong class="fontheader">SURAT PESANAN<br />
                        MARCHAND BINTARO</strong></td>
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
                    <td colspan="3"><span class="fontisi">Yang bertanda tangan di bawah ini : </span></td>
                </tr>
                <tr>
                    <td style="width: 20%"><span class="fontisi">Nama </span></td>
                    <td style="width: 2%"><span class="fontisi">:</span></td>
                    <td style="width: 78%"><span class="fontisi">
                        <asp:Label ID="namacs" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td><span class="fontisi">Alamat (sesuai KTP)</span></td>
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
                    <td><span class="fontisi">Nomor KTP/NPWP</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="noktp" runat="server"></asp:Label>&nbsp;/
                    <asp:Label ID="npwp" runat="server"></asp:Label></span></td>
                </tr>

                <tr>
                    <td><span class="fontisi">Alamat Surat (toko/rumah/kantor</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="alamatsekarang1" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><span class="fontisi">
                        <asp:Label ID="alamatsekarang2" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td><span class="fontisi">Nomor Telepon/HP</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="hp1" runat="server"></asp:Label>&nbsp;/
                    <asp:Label ID="hp2" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td colspan="3">(Selanjutnya disebut "Pemesan") </td>
                </tr>
            </tbody>
        </table>

        <table style="width: 100%">
            <tbody>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: justify">Dengan ini menyatakan akan membeli bangunan sebagaimana diuraikan dalam bulir 1 sampai dengan butir 7 surat pesanan ini (selanjutnya disebut “Bangunan”) dari PT.</
                    <span class="fontisi">&nbsp;<asp:Label ID="namapers" runat="server"></asp:Label> , berkedudukan di Kabupaten Tangerang, (selanjutnya disebut “Penjual”), dengan perincian dan ketentuan sebagai berikut :
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
                    <td style="width: 5%">1.</td>
                    <td style="width: 20%"><span class="fontisi">Lokasi</span></td>
                    <td style="width: 2%"><span class="fontisi">:</span></td>
                    <td style="width: 28%"><span class="fontisi">
                        <asp:Label ID="lokasi" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td style="width: 5%">2.</td>
                    <td style="width: 20%"><span class="fontisi">Type </span></td>
                    <td style="width: 2%"><span class="fontisi">:</span></td>
                    <td style="width: 28%"><span class="fontisi">
                        <asp:Label ID="jenis" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td style="width: 5%">3.</td>
                    <td><span class="fontisi">Nomor</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="namajalan" runat="server"></asp:Label></span></td>

                </tr>
                <tr>
                    <td style="width: 5%">3.</td>
                    <td><span class="fontisi">Lantai/Blok</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="luasnett" runat="server"></asp:Label>
                        m<sup>2</sup></span></td>
                </tr>
                <tr>
                    <td style="width: 5%">4.</td>
                    <td><span class="fontisi">Nomor Unit</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="nounit" runat="server"></asp:Label></span></td>

                </tr>
                <tr>
                    <td style="width: 5%">5.</td>
                    <td><span class="fontisi">Luas Bangunan</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="luassg" runat="server"></asp:Label>
                        m<sup>2</sup></span></td>
                </tr>
                <tr>
                    <td style="width: 5%">6.</td>
                    <td><span class="fontisi">Harga Pengikatan</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">Rp.<asp:Label ID="hrgapengikatan" runat="server"></asp:Label>
                    </span></td>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td colspan="8"><span class="fontisi">(Selanjutnya disebut “Harga Pengikatan” dan besarnya PPN akan disesuaikan dengan peraturan perundang-undangan yang berlaku dari waktu ke waktu).</span></td>

                </tr>
                <tr>
                    <td style="width: 5%">7.</td>
                    <td colspan="3"><span class="fontisi">Harga pengikatan tersebut <b>sudah</b> termasuk : <span class="fontisi">
                        <asp:Label ID="hargainclude" runat="server"></asp:Label>
                    </span></span></td>
                </tr>
                <tr>
                    <td style="width: 5%">8.</td>
                    <td colspan="3"><span class="fontisi">Harga pengikatan tersebut <b>belum</b> termasuk : </span></td>
                </tr>

                <tr>
                    <td></td>
                    <td colspan="5">
                        <table class="width:100%">

                            <tr>
                                <td style="width: 5%">a.</td>
                                <td colspan="5"><span class="fontisi">Bea perolehan Hak atas Tanah dan Bangunan (BPHTB)</span></td>
                            </tr>

                            <tr>
                                <td style="width: 5%">b.</td>
                                <td colspan="5"><span class="fontisi">Biaya notaris/PPAT untuk Akta Jual Beli dan pengurusan Balik Nama Sertifikat ke atas nama pemesan termasuk tapi tidak terbatas pada penerimaan negara bukan pajak (PNBP) terkait transaksi sesuai ketentuan yang berlaku.</span></td>
                            </tr>
                            <tr>
                                <td style="width: 5%">c.</td>
                                <td colspan="5"><span class="fontisi">Iuran Pengelolaan Lingkungan (IPL) yang besarnya akan ditentukan tersendiri dari waktu ke waktu oleh penjual atau pihak yang ditunjuk untuk melakukan pengelolaan lingkungan.</span></td>

                            </tr>
                            <tr>
                                <td style="width: 5%">d.</td>
                                <td colspan="5"><span class="fontisi">Pajak Bumi dan Bangunan (PBB).</span></td>
                            </tr>

                            <tr>

                                <td style="width: 5%">e.</td>
                                <td colspan="5"><span class="fontisi">Biaya pemakaian, aktivitas listrik, air, telepon, dan PAM.</span></td>

                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%">9.</td>
                    <td colspan="5"><span class="fontisi">Khusus untuk pemesan berkewarganegaraan asing (WNA), diwajibkan membayar biaya penurunan hak atas sertifikat, sesuai dengan ketentuan yang berlaku bagi WNA.</span></td>

                </tr>
                <tr>
                    <td style="width: 5%">10.</td>
                    <td colspan="5"><span class="fontisi">Cara Pembayaran:
                        <asp:Label ID="skemabayar" runat="server"></asp:Label></span></td>

                </tr>
                <tr>
                    <td style="width: 5%">11.</td>
                    <td colspan="5"><span class="fontisi">Rincian cara pembayaran:</span></td>

                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td colspan="5">
                        <asp:Table ID="rincianBayar" runat="server"></asp:Table>
                    </td>

                </tr>

            </tbody>
        </table>

        <table style="width: 100%">
            <tbody>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3"><strong class="fontisi">Harga Jual &amp; Cara Pembayaran :</strong></td>
                </tr>
                <tr>
                    <td style="width: 20%"><span class="fontisi">Harga Jual</span></td>
                    <td style="width: 2%"><span class="fontisi">:</span></td>
                    <td style="width: 78%"><span class="fontisi">Rp.
                    <asp:Label ID="nilaikontrak" runat="server"></asp:Label>,-(Include PPN)</span></td>
                </tr>
                <tr>
                    <td><span class="fontisi">Cara Pembayaran</span></td>
                    <td><span class="fontisi">:</span></td>
                    <td><span class="fontisi">
                        <asp:Label ID="skema" runat="server"></asp:Label>
                        (Jadwal Pembayaran Terlampir)</span></td>
                </tr>
            </tbody>
        </table>
        <br />
    </div>
    <div id="hal2" style="page-break-before: always;">
        <table style="width: 100%">
            <tbody>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: center"><strong class="fontheader">SYARAT DAN KETENTUAN SURAT PESANAN RUKO &amp; TOKO</strong></td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">Pemesan dengan ini menyatakan menyetujui syarat-syarat dan ketentuan pemesanan atas bangunan yang ditetapkan oleh penjual, sebagai berikut :
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
                    <td colspan="5"> <span class="fontisi">Harga pengikatan, cara pembayaran, dan jadwal pembayaran atas OBYEK PESANAN yang telah disepakati, mengikat pada PEMESAN dan PENERIMA PESANAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">Segala gambar dan spesifikasi yang berada dalam materi promosi bukan bagian dari penawaran atau perjanjian tertulis ataupun menjadi alat pembuktian hukum dan merupakan data terakhir dalam persiapan promosi serta perubahan dapat terjadi sewaktu-waktu.</span></td>

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
                    <td colspan="5"> <span class="fontisi">Pembayaran dapat dilakukan dengan cara bayar tunai keras, tunai bertahap atau secara angsuran/cicilan melalui PENERIMA PESANAN atau Pihak Ketiga lainnya (termasuk bank).</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">PEMESAN tunduk dan terikat pada syarat-syarat dan ketentuan-ketentuan yang ditetapkan oleh PENERIMA PESANAN, termasuk kewajiban PEMESAN untuk melunasi setiap pembayaran (uang muka dan/atau pembayaran angsuran cicilan secara penuh (dalam arti tidak kurang dan tanpa potongan apapun) dan tepat waktu (dalam arti tidak melampaui tanggal jatuh tempo) sesuai jadwal pembayaran.</span></td>

                </tr>

                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Pembayaran Nomor Urut Pesanan (NUP) atau Uang tanda jadi (Booking fee) yang telah disetor kepada PENERIMA PESAN, tidak dapat dikembalikan kepada PEMESAN bilaman PEMESAN telah melakukan pembatalan pembelian dan atau tidak melakukan kewajiban berikutnya kepada PENERIMA PESAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">Pengembalian uang yang terjadi akibat di lakukannya pembatalan oleh PEMESAN atau akibat kelalaian dari PEMESAN yang tidak melakukan kewajibannya, maka uang yang telah masuk ke PENERIMA PESAN (selain Booking Fee atau Uang tanda jadi) selanjutnya akan dikembalikan setelah unit tersebut terjual kembali (<em>Mengacu pasal 3.F dan 5.E</em>) dan uang pengembalian telah cukup untuk pengembalian kepada PEMESAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">E.</td>
                    <td colspan="5"> <span class="fontisi">Seluruh pembayaran wajib disetorkan secara penuh tanpa potongan dengan memberikan keterangan (<em>Nama Proyek, Tipe Unit, Lantai, Nama Pemesan serta informasi DP/angsuran/cicilan keberapa</em>) dan kemudian di transfer ke dalam rekening PENERIMA PESAN sebagai berikut :</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top"></td>
                    <td style="width:10%"> <span class="fontisi">
                        <b>
                            Bank
                        </b></span>
                    </td>
                    <td colspan="5"> <span class="fontisi">
                        <b>
                            : <asp:Label ID="bank" runat="server"></asp:Label>
                        </b></span>

                    </td>

                </tr>
                 <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top"></td>
                    <td style="width:10%"> <span class="fontisi">
                        <b>
                            Cabang
                        </b></span>
                    </td>
                    <td colspan="5"> <span class="fontisi">
                        <b>
                            : <asp:Label ID="cabang" runat="server"></asp:Label>
                        </b></span>

                    </td>

                </tr>
                 <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top"></td>
                    <td style="width:15%"> <span class="fontisi">
                        <b>
                            Atas Nama
                        </b></span>
                    </td>
                    <td colspan="5"> <span class="fontisi">
                        <b>
                            : <asp:Label ID="atasnama" runat="server"></asp:Label>
                        </b></span>

                    </td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top"></td>
                    <td style="width:25%"> <span class="fontisi">
                        <b>
                            Nomor Rekening
                        </b></span>
                    </td>
                    <td colspan="5"> <span class="fontisi">
                        <b>
                            : <asp:Label ID="norek" runat="server"></asp:Label>
                        </b></span>

                    </td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">F.</td>
                    <td colspan="5"> <span class="fontisi">Pembayaran menggunakan Cek (cheque) atau bilyet giro, dapat dilakukan di kantor pemasaran setiap hari kerja (Senin-Jumat) Jam 09.00 – 16.00 WIB dan akan dianggap sah bilamana PENERIMA PEMESAN telah menggunakan kwitansi resmi yang berstempel perusahaan dan bernomor register.</span></td>

                </tr>
                <tr>

                    <td class="auto-style1"></td>
                    <td style="vertical-align:top" class="auto-style1">G.</td>
                    <td colspan="5" class="auto-style2"> <span class="fontisi">PEMESAN tidak diperkenankan melakukan pembayaran diluar butir 2.E dan 2.F, termasuk kepada sales atau pihak-pihak lain, PENERIMA PESANAN tidak bertanggung jawab atas pembayaran di luar butir tersebut di atas.</span>
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
                    <td colspan="5"> <span class="fontisi">Pembayaran menggunakan fasilitas Kredit Bank (KPR/KPK), PEMESAN wajib melengkapi persyaratan yang dibutuhkan oleh pihak bank pemberi kredit selambatnya 2 (dua) minggu setelah penandatanganan Surat Pesanan ini.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Bilamana Bank pemberi kredit telah menyetujui untuk memberikan fasilitas KPR/KPK kepada PEMESAN, maka dalam jangka waktu 14 (empat belas) hari kalender setelah tanggal persetujuan Bank atau hari yang ditentukan oleh Bank, PEMESAN berkedudukan untuk menandatangani Akta Perjanjian Kredit di hadapan Notaris yang ditunjuk atau disetujui oleh Bank dan PENERIMA PESANAN serta wajib memenuhi semua persyaratan kredit yang ditentukan Bank.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">Jangka waktu dan besarnya angsuran atau plafon kredit yang disetujui menjadi kewenangan pihak bank pemberi kredit. Apabila plafon kredit lebih rendah dari yang dimohon, maka penambahan uang muka menjadi kewajiban PEMESAN dan sanggup melunasi penambahan uang muka sebelum Akad Kredit dilaksanakan. Apabila PEMESAN tidak sanggup maka dinyatakan pemesanan OBJEK PESANAN menjadi batal dengan sendirinya.</span></td>

                </tr>
                 <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">E.</td>
                    <td colspan="5"> <span class="fontisi">Jika persetujuan atas pemberian fasilitas kredit (KPR/KPK) ditolak bank, maka PEMESAN diberi kesempatan untuk melakukan pembelian dengan cara bayar cash keras/ cash bertahap berdasarkan ketentuan yang berlaku. Dan bilamana PEMESAN tidak memenuhi ketentuan pembayaran dengan cara bayar cash keras/cash bertahap tersebut, maka PEMESAN sepakat untuk membatalkan SURAT PESANAN ini.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">F.</td>
                    <td colspan="5"> <span class="fontisi">Apabila terjadi pembatalan terkait dengan KPR/KPK sebagaimana pada butir diatas, maka PENERIMA PESANAN berhak membatalkan secara sepihak PEMESAN-an atas OBYEK PESANAN, dengan melepas ketentuan dalam pasal 1266 dan 1267 Kitab Undang-Undang Hukum Perdata, dan dalam hal ini jumlah uang yang telah dibayarkan oleh PEMESAN dinyatakan hangus dan menjadi hak penuh PENERIMA PESANAN serta PENERIMA PESANAN memiliki hak secara penuh dan berwenang untuk memasarkan dan menjual kembali OBYEK PESANAN tersebut kepada pihak lain tanpa harus persetujuan PEMESAN.</span></td>

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
                    <td colspan="5"> <span class="fontisi">Apabila PEMESAN tidak melakukan kewajibannya dalam pembayaran dan/atau terlambat membayar atau kurang membayar angsuran / cicilan sesuai tanggal jatuh tempo kewajiban pembayaran, maka PEMESAN dikenakan denda keterlambatan sebesar 1%0 (satu permill) per hari terhitung dari jumlah pembayaran tertunggak.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">PENERIMA PESANAN tidak berkewajiban untuk memberitahukan dan atau mengingatkan PEMESAN, apabila pemesan terlambat atau kurang membayar sesuai jadwal dan jumlah pembayaran yang telah disepakati.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Bilamana PEMESAN tidak segera melunasi pembayaran yang tertunggak beserta dendanya dan PENERIMA PESANAN akan memberikan pemberitahuan melalui telephone atau alat komunikasi online serta melalui media surat yang akan di kirimkan sesuai alamat PEMESAN dan bilamana PENERIMA PESAN telah mengirimkan Surat Peringatan mengenai tunggakan tersebut sebanyak 1 (satu) kali namun tidak adanya respond/tanggapan dari PEMESAN, maka dengan lewatnya waktu saja membuktikan bahwa PEMESAN lalai melakukan kewajibannya sehingga PEMESAN dianggap membatalkan pesanan ini.</span></td>

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
                    <td colspan="5"> <span class="fontisi">SURAT PESANAN ini batal apabila PEMESAN mengakhiri SURAT PESANAN atau dalam hal ini PEMESAN lalai atau tidak memenuhi kewajibannya, termasuk tetapi tidak terbatas pada kewajiban pembayaran angsuran atas OBJEK PESANAN, biaya dan pajak, serta denda (jika ada).</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">PARA PIHAK sepakat mengesampingkan Pasal 1266 dan Pasal 1267 Kitab Undang-Undang Hukum Perdata (yang mana dalam hal terjadi pengakhiran sepihak atas SURAT PESANAN, tidak memerlukan putusan / penetapan dari Pengadilan), serta Pasal 1813, Pasal 1814, dan Pasal 1816 Kitab Undang-Undang Hukum Perdata (mengenai berakhirnya pemberian kuasa).</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Sebagai akibat batalnya SURAT PESANAN maka pajak-pajak yang telah dibayar oleh PEMESAN akan hangus dan tidak dapat diganti atau dialihkan ke pihak lain / pihak ketiga,, termasuk tetapi tidak terbatas kepada PENERIMA PESANAN. Selain itu, apabila terdapat PPH FINAL yang sudah dibayar oleh PENERIMA PESANAN atas OBJEK PESANAN; maka PEMESAN wajib mengganti kepada PENERIMA PESANAN, uang sebesar (sama jumlahnya dengan) PPH FINAL yang sudah dibayar tersebut.</span></td>

                </tr>

                 <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">PEMESAN wajib menandatangani dokumen-dokumen sehubungan dengan pembatalan SURAT PESANAN ini serta sepakat untuk melunasi seluruh biaya/pajak/ongkos/denda (jika ada) dan PEMESAN wajib mengembalikan semua kuitansi serta berkas-berkas lainnya yang berkenaan dengan OBJEK PESANAN ini.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">E.</td>
                    <td colspan="5"> <span class="fontisi">Apabila terjadi keterlambatan pembayaran yang melewati 30 (tiga puluh) hari terhitung sejak tanggal jatuh tempo pembayaran serta PENERIMA PESANAN telah mengirimkan Surat Peringatan mengenai keterlambatan pembayaran tersebut sebanyak 1 (satu) kali atau apabila PEMESAN membatalkan pemesanan unit secara sepihak dengan berbagai alasan maka PENERIMA PESANAN berhak membatalkan secara sepihak PEMESAN-an atas OBYEK PESANAN, dengan melepas ketentuan dalam pasal 1266 dan 1267 Kitab Undang-Undang Hukum Perdata, dan PENERIMA PESANAN memilihi hak penuh atas jumlah uang yang telah dibayarkan oleh PEMESAN kepada PENERIMA PESANAN dan membebaskan PENERIMA PESANAN dari segala tuntutan serta PENERIMA PESANAN berhak secara penuh dan berwenang memasarkan dan menjual OBYEK PESANAN tersebut kepada pihak lain tanpa harus adanya persetujuan dari PEMESAN.</span></td>

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
                    <td colspan="5"> <span class="fontisi">PENERIMA PESANAN akan mengirimkan pemberitahuan secara tertulis kepada PEMESAN untuk segera menandatangani PPJB bilamana pembayaran OBYEK PESANAN telah mencapai 30% (tiga puluh persen) dari HARGA OBYEK PESANAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">B.Apabila PEMESAN tidak mengindahkan 2 (dua) kali pemberitahuan tertulis yang dikirimkan oleh PENERIMA PESANAN dengan tenggang waktu masing-masing 7 (tujuh) hari kalender, maka PEMESAN dengan ini menyatakan dan memberikan kuasa penuh kepada PENERIMA PESANAN, kuasa yang dimaksud dalam hal ini adalah diberikan dengan hak substitusi untuk : bertindak dan atas nama PEMESAN untuk menandatangani PPJB dan; untuk menyerahkan PPJB tersebut kepada pemesan. Atas penandatanganan PPJB tersebut PENERIMA PESANAN berbas dan terlepas dari segala somasi, tuntutan, gugatan, baik perdata maupun pidana, baik dari PEMESAN maupun dari pihak lain.</span></td>

                </tr>

                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Bilamana PEMESAN tidak segera melunasi pembayaran yang tertunggak beserta dendanya dan PENERIMA PESANAN akan memberikan pemberitahuan melalui telephone atau alat komunikasi online serta melalui media surat yang akan di kirimkan sesuai alamat PEMESAN dan bilamana PENERIMA PESAN telah mengirimkan Surat Peringatan mengenai tunggakan tersebut sebanyak 1 (satu) kali namun tidak adanya respond/tanggapan dari PEMESAN, maka dengan lewatnya waktu saja membuktikan bahwa PEMESAN lalai melakukan kewajibannya sehingga PEMESAN dianggap membatalkan pesanan ini.</span></td>

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
                        Perubahan Nama dalam SURAT PESANAN oleh PEMESAN harus mendapat persetujuan tertulis terlebih dahulu dari PENERIMA PESANAN. Perubahan Nama dilakukan dengan ketentuan sebagai berikut :
                    </td>
                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">Sebelum PPJB ditandatangani, PEMESAN dapat melakukan perubahan nama dalam SURAT PESANAN dengan ketentuan bahwa maksimum perubahan dilakukan sebanyak 1 (satu) kali dan perubahan nama tersebut dilakukan kepada pihak lain yang mempunyai alamat yang sama dnegan alamat PEMESAN (sesuai dengan Kartu Tanda Penduduk) dan namanya tercantum dalam Kartu Keluarga PEMESAN dan merupakan pasangan yang sah secara hukum dari PEMESAN tanpa adanya perjanjian pisah harta (Prenuptial Agreement), dan/atau merupakan anak kandung, orang tua, yang sah dari PEMESAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">Setelah pendandatanganan PPJB, perubahan Nama PEMESAN akan dikenakan biaya sebesar 2% (dua persen) dari harga pengikatan atas OBYEK PESANAN.</span></td>

                </tr>
                <tr>
                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Perubahan Nama kepada pihak lain yang bukan merupakan anak kandung atau orang tua kandung dan/atau pasangan yang sah menurut hukum dari PEMESAN akan dikenakan biaya sebesar Rp. 1.000.000,- (satu juta rupiah) bila belum dilakukan pendandatanganan PPJB dan biaya sebesar 3% (tiga persen) dari harga pengikatan atas OBYEK PESANAN bila telah dilakukan PPJB</span></td>

                </tr>
                <tr>
                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">Butir 7.B tersebut diatas tidak berlaku, apabila telah dilakukan penandatanganan Akta Jual Beli dihadapan Pejabat Pembuat Akta Tanah (PPAT).</span></td>

                </tr>
                <tr>
                    <td colspan="5" style="text-align: center">
                        <br />
                        <b>PASAL 8<br />
                        BERITA ACARA SERAH TERIMA</b>&nbsp;
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">A.</td>
                    <td colspan="5"> <span class="fontisi">Proses penyerahan OBYEK PESANAN akan dijabarkan dalam Perjanjian Pengikatan Jual Beli (PPJB).</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">Serah Terima OBYEK PESANAN secara fisik wajib dilaksanakan oleh PEMESAN atas pemberitahuan secara tertulis dari PENERIMA PESANAN (atau petugas/pejabatnya) dan dituangkan dalam suatu Berita Acara Serah Terima (“BAST”) yang telah ditetapkan, setelah OBYEK PESANAN telah siap untuk dilakukan serah terima. Untuk maksud tersebut PEMESAN wajib memenuhi semua syarat-syarat dan ketentuan-ketentuan (termasuk pelunasan Harga Pengikatan, denda dan kewajiban pembayaran lainnya (jika ada) yang ditetapkan oleh PENERIMA PESANAN.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Apabila dalam jangka waktu 7 (tujuh) hari setelah tanggal pemberitahuan untuk serah terima ternyata PEMESAN tidak datang dan tidak menandatangani Berita Acara Serah Terima (“BAST”) karena sebaba/alasan apapun, maka PEMESAN dianggap menyetujui bahwa penyerahan OBYEK PESANAN telah dilakukan dan dalam hal demikian bukti pengiriman surat pemberitahuan untuk melaksanakan serah terima OBYEK PESANAN tersebut dianggap telah merupakan bukti yang cukup bahwa serah terima OBYEK PESANAN telah dilaksanakan pada hari ke 7 (tujuh) setelah tanggal pemberitahuan tersebut disampaikan.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">PENERIMA PESANAN bertanggung jawab menyelesaikan OBYEK PESANAN dalam kondisi baik.</span></td>

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
                    <td colspan="5"> <span class="fontisi">Hal-hal yang tidak atau belum diatur dalam SURAT PESANAN ini akan diatur lebih lanjut dalam Perjanjian Pengikatan Jual Beli dan Perjanjian-Perjanjian lain yang dibuat sehubungan OBYEK PESANAN yang tunduk pada hukum yang berlaku di Negara Indonesia.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">B.</td>
                    <td colspan="5"> <span class="fontisi">Apabila terjadi perbedaan pendapat atau perselisihan antara PARA PIHAK, baik dalam penafsiran meupun pelaksananaan ketentuan dalam SURAT PESANAN ini, maka PARA PIHAK sepakat untuk menyelesaikannya secara musyawarah untuk mencapai mufakat. Dan bilamana PARA PIHAK tidak mencapai musyawarah untuk mufakat maka semua perselisihan atau sengketa yang timbul / terjadi oleh PARA PIHAK, baik yang bersifat teknis maupun non teknis akan diselesaikan melalui jalur hukum pada instansi yang berwenang.</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">C.</td>
                    <td colspan="5"> <span class="fontisi">Surat Pesanan ini merupakan lampiran yang tidak terpisahkan di dalam Perjanjian Pengikatan Jual Beli (PPJB).</span></td>

                </tr>
                <tr>

                    <td style="width:5%"></td>
                    <td style="width: 5%;vertical-align:top">D.</td>
                    <td colspan="5"> <span class="fontisi">Demikian dengan maksud untuk terikat oleh Hukum, setelah SURAT PESANAN ini dibaca dengan seksama dan di mengerti isinya, maka PARA PIHAK menandatangani Surat PEMESAN-an ini.</span><br /><br /><br /></td>

                </tr>
                <tr>
                    <td colspan="5">
                        Demikian Surat Pesanan ini dibuat dan ditandatangani dalam keadaan sadar, sehat jasmani maupun rohani, tanpa adanya paksaan dari pihak manapun, dan untuk dilaksanakan oleh Pemesan.
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
    <table style="width: 100%">
        <tbody>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><span class="fontisi">Tangerang,
                    <asp:Label ID="tglkontrak" runat="server"></asp:Label></span></td>
                <td style="width:30%">Menyetujui</td>
            </tr>
        </tbody>
    </table>

    <table style="width: 100%">
        <tbody>
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
