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
        font-size: 18pt;
        font-family: 'Times New Roman', Times, serif;
    }

    .fontisi {
        font-size: 11pt;
        font-family: 'Times New Roman', Times, serif;
    }
</style>

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
                    <td style="text-align: justify">Dengan ini menyatakan akan membeli bangunan sebagaimana diuraikan dalam butir 1 sampai dengan butir 7 surat pesanan ini (selanjutnya disebut “Bangunan”) dari PT.<span class="fontisi">&nbsp;<asp:Label ID="namapers" runat="server"></asp:Label> , berkedudukan di Kabupaten Tangerang, (selanjutnya disebut “Penjual”), dengan perincian dan ketentuan sebagai berikut :
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