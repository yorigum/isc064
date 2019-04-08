<%@ Control Language="c#" Inherits="ISC064.LEGAL.PrintBASTTemplate" CodeFile="PrintBASTTemplate.ascx.cs" %>
<div>
    <table width="100%" border="0">
        <tr>
            <td width="50%">
                    <img src="/Media/wp.png" width="auto" height="100px"/>
            </td>
        </tr>
    </table>
</div>
<div align="center">
	<h3>BERITA ACARA SERAH TERIMA (BAST)<br />WESTPOINT</h3>
	Nomor :
	<asp:label id="nomorl" runat="server" font-bold="True"></asp:label>
	<br>
	<%--Tgl. :
	<asp:label id="tgl" runat="server" font-bold="True"></asp:label>--%>
</div>
<table width="100%" align="center">
    <tr>
        <td>Pada hari ini <asp:Label ID="harist" runat="server" />, tanggal <asp:Label ID="tglst" runat="server" /> pukul <asp:Label ID="jamst" runat="server" />&nbsp; WIB, telah dilakukan serah terima dari dan kepada:<br />
            <table width="100%" align="left">
                <tr>
                    <td style="vertical-align:top;">I.</td>
                    <td>PT. ANDALAND PROPERTY DEVELOPMENT<br />
                        Berkedudukan di Jakarta, selanjutnya disebut <b>Pihak Pertama</b>.
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;">II.</td>
                    <td>
                        <table>
                            <tr>
                                <td width="10%">Nama</td><td width="1%">:</td>
                                <td><asp:Label ID="Namacs" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Alamat</td><td width="1%">:</td>
                                <td><asp:Label ID="Alamatcs" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>No KTP</td><td>:</td>
                                <td><asp:Label ID="ktpcs" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="3">Bertindak untuk dirinya sendiri / selaku Kuasa*, dari …………………… berdasarkan Surat Kuasa tanggal…………………, selanjutnya disebut <b>Pihak Kedua</b>.</td>
                            </tr>
                            <tr>
                                <td colspan="3"><br />Atas 1 (satu) unit satuan Rumah Susun Westpoint, pada lokasi :</td>
                            </tr>
                            <tr>
                                <td colspan="3">Lantai <asp:Label ID="lantai1" runat="server" /> No. Unit <asp:Label ID="nounit1" runat="server" />, Tipe <asp:Label ID="tipe1" runat="server" /> **)</td>
                            </tr>
                            <tr>
                                <td colspan="3">Luas <asp:Label ID="luas1" runat="server" /> m2 (Semi gross),	Luas <asp:Label ID="semigross1" runat="server" /> m2 (net). </td>
                            </tr>
                            <tr>
                                <td colspan="3">(selanjutnya  akan disebut juga <b>“OBJEK SERAH TERIMA”</b>).</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Adapun Serah terima atas OBJEK SERAH TERIMA dilakukan dengan syarat-syarat dan ketentuan-ketentuan sebagai berikut:
                        <table>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">1</td>
                                <td style="text-align:justify;vertical-align:top">
                                    Garansi atas OBYEK SERAH TERIMA diterima dalam kondisi standar terhadap segala kerusakan akibat kesalahan kontruksi dan dengan jangka waktu (masa garansi) selama  90 (sembilan puluh) sejak tanggal penandatanganan BAST OBJEK SERAH TERIMA dari Pihak Pertama. Ketentuan ini menjadi tidak berlaku apabila Pihak Kedua telah mengadakan perubahan dan/atau perbaikan dalam bentuk apapun terhadap OBJEK SERAH TERIMA.
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">2</td>
                                <td style="text-align:justify;vertical-align:top">
                                    Apabila terjadi kerusakan – kerusakan atau kerugian pada bangunan OBJEK SERAH TERIMA atau bangunan yang terletak bersebelahan atau berdampingan akibat pekerjaan renovasi (Fitting out) sampai dengan operasional yang dilakukan oleh Pihak Kedua, maka Pihak Kedua bertanggung jawab sepenuhnya.
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">3</td>
                                <td style="text-align:justify;vertical-align:top">
                                    Khusus OBJEK SERAH TERIMA yang pembayarannya belum lunas, Pihak Kedua wajib mengasuransikan Objek Serah Terima terhitung sejak tanggal serah terima OBJEK SERAH TERIMA dari Pihak Pertama kepada Perusahaan asuransi yang disetujui oleh Pihak Pertama terhadap segala bahaya dan dengan syarat-syarat yang ditetapkan oleh Pihak Pertama. Polis asuransi harus mencantumkan klausul klaim pertanggungan untuk kepentingan Pihak Pertama dan disimpan oleh Pihak Pertama.
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">4</td>
                                <td style="text-align:justify;vertical-align:top">
                                    Dalam hal Pihak Kedua belum melunasi kewajiban pembayaran terhadap OBJEK SERAH TERIMA, maka Pihak Kedua bersedia membuat dan menandatangani Perjanjian Pinjam Pakai  (terlebih dahulu) atas OBJEK SERAH TERIMA dan biaya pengikatannya (termasuk biaya Notaris) ditanggung Pihak Kedua. Berita Acara Serah Terima ini berlaku pula sebagai bagian dari perjanjian pinjam pakai tersebut.
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">5</td>
                                <td style="text-align:justify;vertical-align:top">
                                    Terhitung sejak serah terima berdasarkan BAST ini :
                                    <table>
                                        <tr>
                                            <td style="vertical-align:top;width:16px">a.</td>
                                            <td style="text-align:justify">Segala resiko dan kerusakan atau kehilangan yang terjadi atas OBJEK SERAH TERIMA maupun barang /alat/perlengkapan Iainnya pada Objek Serah Terima merupakan beban dan tanggung jawab Pihak Kedua sepenuhnya.</td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align:top;width:16px">b.</td>
                                            <td style="text-align:justify">Segala tagihan/biaya penggunaan Iistrik, air, telepon, iuran pengelolaan (biaya pemeliharaan/maintenance fee), iuran retribusi atas Objek Serah Terima menjadi beban dan wajib dibayar oleh Pihak Kedua kepada instansi yang berkepentingan tepat pada waktunya, termasuk juga Pajak Bumi dan Bangunan (PBB) serta biaya-biaya Iainnya menjadi beban dan wajib dibayar Pihak Kedua. Apabila Pihak Kedua lalai dalam melaksanakan kewajibannya tersebut, maka Pihak Pertama/Badan Pengelola berhak memutuskan/menyegel Objek Serah Terima, sambungan telepon, listrik, air, dan lain-lain. Sehubungan sanksi pemutusan/penyegelan oleh Pihak yang berwenang, maka biaya penyambungan/pembukaan segel tersebut menjadi tanggung jawab Pihak Kedua sepenuhnya.</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">6</td>
                                <td style="text-align:justify;vertical-align:top">
                                    Pihak Kedua wajib membayar Biaya Pemeliharaan (Service Charge) dan Dana Cadangan (Sinking Fund) UNIT untuk jangka waktu………bulan, yaitu bulan…………… hingga bulan…………… sebesar Rp. ………………………. .. (……………………………………)
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">7</td>
                                <td style="text-align:justify;vertical-align:top">
                                    selama pembayaran harga OBJEK SERAH TERIMA belum lunas seluruhnya, Pihak Kedua berjanji tidak akan melakukan perubahan atau penambahan pada OBJEK SERAH TERIMA. Apabila hal tersebut dilakukan dan ternyata Pihak Kedua melakukan pelanggaran pembayaran kepada Pihak Pertama yang menyebabkan Pihak Pertama membatalkan transaksi pengikatan jual beli OBJEK SERAH TERIMA atau mengakhiri perjanjian pinjam pakai (butir 2b di atas), maka Pihak Kedua tidak berhak dan tidak dapat menuntut penggantian/pengembalian dalam bentuk apapun atas setiap atau semua biaya perubahan atau penambahan dimaksud dan Pihak Pertama dibebaskan oleh Pihak Kedua dari segala tuntutan dan gugatan dalam bentuk apapun.
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">8</td>
                                <td style="text-align:justify;vertical-align:top">
                                    apabila Pihak Kedua melakukan perbaikan, penambahan atau perubahan pada OBJEK SERAH TERIMA dalam jangka waktu jaminan perbaikan, maka jangka waktu jaminan perbaikan tersebut menjadi berakhir dan Pihak Kedua harus harus bertangggung jawab sepenuhnya atas OBJEK SERAH TERIMA.
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">9</td>
                                <td style="text-align:justify;vertical-align:top">
                                    dengan mengindahkan setiap dan seluruh ketentuan/peraturan yang berlaku secara khusus di lingkungan/kawasan Westpoint, maka terhitung sejak tanggal ditandatanganinya Berita Acara Serah Terima ini Pihak Kedua dapat melakukan pekerjaan-pekerjaan interior/partisi/penyempurnaan atas OBJEK SERAH TERIMA, akan tetapi pelaksanaanya wajib terlebih dulu disetujui oleh dan dikoordinasikan dengan Pihak Pertama atau Pengelola Westpoint.
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">10</td>
                                <td style="text-align:justify;vertical-align:top">
                                    khusus untuk Pihak Kedua yang belum menandatangani (akta) perjanjian pengikatan jual beli atas OBJEK SERAH TERIMA, apabila Pihak Pertama telah menyiapkan pelaksanaan penandatangan (akta) perjanjian pengikatan jual belinya dimana Pihak Kedua telah memenuhi kewajiban pembayarannya dalam jumlah minimal pembayaran (yang ditentukan sendiri dari waktu ke waktu oleh Pihak Pertama), maka Pihak Pertama akan memberitahukan tentang hal tersebut kepada Pihak Kedua. Karenanya Pihak Kedua wajib melangsungkan dan menandatangani (akta) perjanjian pengikatan jual belinya yang syarat-syarat dan ketentuannya ditentukan oleh Pihak Pertama (berlaku standard untuk perjanjian pengikatan jual beli pada Westpoint) sebelum batas waktu yang ditetapkan dan diberitahukan secara tertulis oleh Pihak Pertama kepada Pihak Kedua (minimal 14 hari dan maksimal 21 hari setelah tanggal surat pemberitahuan Pihak Pertama).
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">11</td>
                                <td style="text-align:justify;vertical-align:top">
                                    Pihak Kedua dengan ini menyatakan tunduk dan taat terhadap setiap ketentuan dan peraturan tata tertib lingkungan Westpoint serta peraturan lainnya yang keluarkan oleh Badan Pengelola Apartemen Westpoint. 
                                </td>
                            </tr>
                            <tr>
                                <td width="15px" style="vertical-align:top;text-align:center">&nbsp;</td>
                                <td style="vertical-align:top">12</td>
                                <td style="text-align:justify;vertical-align:top">
                                    setelah ditandanganinya Berita Acara Serah Terima ini, maka:
                                    <table>
                                        <tr>
                                            <td style="width:16px;vertical-align:top">a.</td>
                                            <td style="text-align:justify">
                                                setiap keluhan Pihak Kedua selama Masa Garansi diajukan secara tertulis dengan mengisi Formulir yang disediakan oleh Pihak Pertama, dan akan diterima dan ditanda tangani oleh <i>Tenant Relation Officer</i> Westpoint.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:16px;vertical-align:top">b.</td>
                                            <td style="text-align:justify">Setiap keluhan Pihak Kedua setelah berakhirnya Masa Garansi ditanda tangani oleh Pengelola Apartemen Westpoint. Pengelola akan menyiapkan perkiraan biaya untuk perbaikan OBJEK SERAH TERIMA yang disepakati oleh kedua belah pihak dan ditanggung oleh Pihak Kedua.</td>
                                        </tr>
                                        <tr>
                                            <td style="width:16px;vertical-align:top">c.</td>
                                            <td style="text-align:justify">Pihak Kedua dengan ini menyatakan telah melihat, memeriksa dan menyetujui OBJEK SERAH TERIMA berikut segala perlengkapan dan kunci-kuncinya dalam keadaan baik dan lengkap yang diserahkan dari Pihak Pertama berdasarkan BAST ini, sehingga Pihak Kedua dengan ini berjanji kepada Pihak Pertama untuk tidak melakukan klaim dalam bentuk apapun berkaitan dengan Apartemen tersebut dikemudian hari.</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">Catatan awal Kwh Meter Listrik ……………………. Catatan Awal Meteran air ……………………</td>
                </tr>
                <tr>
                    <td colspan="3">Angka-angka tersebut nerupakan pencatatan awal pada saat serah terima ini dan segel diterima dalam keadaan baik.<br /><br /></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table>
    
    <tr>
        <td style="width:40%;vertical-align:top">PIHAK PERTAMA<br /><b>PT. ANDALAND PROPERTY DEVELOPMENT</b></td>
        <td style="width:40%;vertical-align:top">PIHAK KEDUA</td>
    </tr>
    <tr>
        <td style="height:100px">&nbsp;</td>
        <td style="height:100px">&nbsp;</td>
    </tr>
    <tr>
        <td style="vertical-align:top;border-top:1px solid ">Nama Jelas</td>
        <td style="vertical-align:top;border-top:1px solid ">Nama Jelas</td>
    </tr>
</table>
<br />
<table>
    <tr>
        <td width="25%">*) coret yang tidak perlu </td>
        <td width="25%">* Apartemen Hunian </td>
        <td width="25%">** Kantor/Komersial Area</td>
        <td width="25%">&nbsp;</td>
    </tr>
    <tr>
        <td width="25%">Asli: Pihak Pertama</td>
        <td width="25%">Merah: Administrasi</td>
        <td width="25%">Kuning: Pengelola</td>
        <td width="25%">Hijau: Pihak Kedua</td>
    </tr>
</table>
