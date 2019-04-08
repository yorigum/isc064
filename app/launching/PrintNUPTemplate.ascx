<%@ Control Language="c#" Inherits="ISC064.LAUNCHING.PrintNUPTemplate" CodeFile="PrintNUPTemplate.ascx.cs" %>
<link href="/Media/Style.css" type="text/css" rel="stylesheet">
<style type="text/css">
    .header
    {
        text-align: center;
        font-size: 16px;
        margin-bottom: 30px;
    }
    #header
    {
        text-align: center;
        font-size: 16px;
        margin-bottom: 30px;
    }
    .normalTXT
    {
        font-size: 12px;
        font-family: arial;
        text-align: justify;
    }
    .customerTB
    {
        margin: 10px 0px 10px 10px;
        border-collapse: collapse;
        border-color: black;
    }
    .customerTB td
    {
        padding: 8px 8px 8px 12px;
    }
    .tableTXT
    {
        font-size: 12px;
    }
    .tableASP
    {
        font-size: 12px;
    }
    .ol
    {
        margin-bottom: 15px;
    }
</style>
<body>
    <div id="divPrint" runat="server" visible="false">
    <div style="text-align:right; display:none;"> <img id="QRImage" runat="server" /></div>
        <div style="width: 100%">
            
            <h1 style="text-align: center">
                NUP
            </h1>
            <h2 style="text-align: center">
                Townhome & Shophouse Grand Victorian Life Style</h2>
            <br />
            Telah diterima uang sejumlah Rp
            <asp:Label ID="nbayar" runat="server"></asp:Label>
            (<asp:Label ID="nterbilang" runat="server"></asp:Label>)<br />
            pembelian [1] unit Rumah/Kavling dicluster Victorian & Shophouse di Grand Kawanua
            International City<br />
            <table style="width: 100%">
                <tr>
                    <td>
                        Nama Pemesan
                    </td>
                    <td>
                        :
                    </td>
                    <td colspan="4" style="border-bottom: 1px solid black">
                        <asp:Label ID="pemesan" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        No KTP
                    </td>
                    <td>
                        :
                    </td>
                    <td style="border-bottom: 1px solid black">
                        <asp:Label ID="noktp" runat="server"></asp:Label>
                    </td>
                    <td>
                        NoNPWP
                    </td>
                    <td>
                        :
                    </td>
                    <td style="border-bottom: 1px solid black">
                        <asp:Label ID="npwp" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Alamat Korespondensi
                    </td>
                    <td>
                        :
                    </td>
                    <td colspan="4" style="border-bottom: 1px solid black">
                        <asp:Label ID="korespon1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                    <td colspan="4" style="border-bottom: 1px solid black">
                        <asp:Label ID="korespon2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        No Telp/HP
                    </td>
                    <td>
                        :
                    </td>
                    <td style="border-bottom: 1px solid black">
                        <asp:Label ID="notelp" runat="server"></asp:Label>
                        /
                        <asp:Label ID="nohp" runat="server"></asp:Label>
                    </td>
                    <td>
                        Email
                    </td>
                    <td>
                        :
                    </td>
                    <td style="border-bottom: 1px solid black">
                        <asp:Label ID="email" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Bank / No Rek Tabungan
                    </td>
                    <td>
                        :
                    </td>
                    <td colspan="4" style="border-bottom: 1px solid black">
                        <asp:Label ID="bank" runat="server"></asp:Label>
                        /
                        <asp:Label ID="norek" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            Pembayaran Uang Reservasi dengan
            <table style="width: 100%">
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                            <asp:Label ID="cabarTunai" runat="server"></asp:Label>
                        </div>&nbsp;
                        Tunai ke Kasir PT. WPS
                    </td>
                    <td>
                    </td>
                    <td>
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                            <asp:Label ID="cabarCC" runat="server"></asp:Label>
                        </div>&nbsp;
                        Credit Card / Debit Card Bank
                        <asp:Label ID="bankCC" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                            <asp:Label ID="cabarTrf" runat="server"></asp:Label>
                        </div>&nbsp;
                        Transfer ke Rekening
                    </td>
                    <td>
                    </td>
                    <td>
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                            <asp:Label ID="cabarLainnya" runat="server"></asp:Label>
                        </div>&nbsp;
                        Lainnya
                        <asp:Label ID="ketLainnya" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            Bersama ini telah membaca & menyetujui surat, ketentuan & prosedur launching di
            balik formulir ini
            <br />
            <br />
            <table style="width: 100%;">
                <tr>
                    <td style="width: 70%; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td colspan="4">
                                    Manado,
                                    <asp:Label ID="tglNUP" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 110px;">
                                <td style="vertical-align: bottom; text-align: center;">
                                    <asp:Label ID="pemesan2" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td style="vertical-align: bottom; text-align: center;">
                                    <asp:Label ID="sales" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%; text-align: center; border-top: 1px solid black;">
                                    Pemesan
                                </td>
                                <td style="width: 20%">
                                </td>
                                <td style="width: 30%; text-align: center; border-top: 1px solid black;">
                                    Marketing/Agent
                                </td>
                                <td style="width: 20%">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 30%;">
                        <div style="width: 80%; margin: 0 auto; border: 1px solid black; text-align: justify;">
                            NOMOR : <asp:Label ID="nonup" runat="server" Font-Bold="true"></asp:Label><br />
                            (Diberikan bila dana sudah efektif diterima di PT.WPS dan data Pemesan diisi dengan
                            lengkap disertai copy KTP & NPWP)
                            <br />
                            <br />
                            <br />
                            <br />
                            <div style="text-align: center">
                                Andy Purwanto<br />
                                Senior Sales & Marketing Manager
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            Berminat untuk membeli town home / kavling dengan ukuran (tidak mengikat) :<br />
            <table style="width: 100%">
                <tr>
                    <td>
                    </td>
                    <td style="vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        LT/LB : 7x18 / 101m<sup>2</sup>
                    </td>
                    <td>
                    </td>
                    <td style="vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        LT/LB : 8x18 / 128m<sup>2</sup>
                    </td>
                    <td>
                    </td>
                    <td style="vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        LT/LB : 10x20 / 205m<sup>2</sup>
                    </td>
                    <td>
                    </td>
                    <td style="vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        LT/LB : 12x25 / 213m<sup>2</sup>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width:15%;">
                    </td>
                    <td style="vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        Kavling Hook
                    </td>
                    <td>
                    </td>
                    <td style="vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        Kavling View Golf
                    </td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="width:40%;">
                        Berminat untuk membeli shophouse (tidak mengikat) :
                    </td>
                    <td style="vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        3Lantai
                    </td>
                    <td style="vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        4Lantai
                    </td>
                    <td style="vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        5Lantai
                    </td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="width:32%;">
                        Rencana bayar (tidak mengikat) :
                    </td>
                    <td style="width:17%; vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        Tunai Keras
                    </td>
                    <td style="width:14%; vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        KPR
                    </td>
                    <td style="width:20%; vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        Cicilan ke Developer
                    </td>
                    <td style="width:17%; vertical-align:bottom">
                        <div style="border: 1px solid black; width: 13px; height: 13px; float: left">
                        </div>&nbsp;
                        Ballon Payment
                    </td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td>
                        Copy : PT WPS
                    </td>
                    <td>
                        Original : Pemesan
                    </td>
                </tr>
            </table>
        </div>
        <div style="page-break-after: always">
        </div>
        <div style="width: 100%">
            <h1 style="text-align: center">
                SYARAT & KETENTUAN</h1>
            <br />
            <br />
            <br />
            <h3>
                A. FORM RESERVASI</h3>
            <table style="width: 100%">
                <tr>
                    <td style="width: 4%; text-align: right; vertical-align: top">
                        1.
                    </td>
                    <td style="width: 96%; vertical-align: top">
                        Peminat/Sales/Agent wajib mengisi data pemesan secara lengkap dan sebenar-benarnya
                        pada form reservasi sesuai dengan yang tercantum pada KTP/NPWP dan rekening tabungan
                        peminat serta memberikan fotocopy KTP & NPWP.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        2.
                    </td>
                    <td style="vertical-align: top">
                        Form ini disertai dengan paraf pejabat yang berwenang dan stempel perusahaan adalah
                        tanda terima resmi atas Uang Reservasi (NUP). Uang reservasi ini bukanlah
                        booking fee, sehingga bukan merupakan tanda jadi penjualan.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        3.
                    </td>
                    <td style="vertical-align: top">
                        Setiap pemesanan 1 unit yang dipesan wajib membayar uang reservasi sebesar Rp.5.000.000,-
                        (lima juta rupiah) dan berlaku kelipatan untuk reservasi, apabila ingin membeli
                        lebih dari 1 unit. Maksimal reservasi 5 unit per person.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        4.
                    </td>
                    <td style="vertical-align: top">
                        Uang reservasi ini tidak mengikat dan bersifat sementara hanya untuk memilih unit,
                        bukan merupakan Surat Pemesanan Unit (SPU).
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        5.
                    </td>
                    <td style="vertical-align: top">
                        Untuk Peminat yang berhalangan hadir saat Pendataan ulang/pemilihan unit, dapat
                        diwakilkan dan wajib melampirkan surat kuasa asli bermaterai, dilampirkan fotokopi
                        KTP pemberi dan penerima kuasa yang berlaku. Kuasa yang sah adalah orang yang memperoleh
                        kuasa dari Peminat berdasarkan surat kuasa.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        6.
                    </td>
                    <td style="vertical-align: top">
                        Nama yang tercantum pada form reservasi tidak dapat di pindah tangankan kepada Pihak
                        lain, nama yang dipergunakan pada form reservasi bersifat pasti, tidak dapat berubah,
                        tidak berlaku sistem "atau" untuk penggunaan nama peminat, apabila didapati hal
                        tersebut maka dinyatakan batal/gugur. Dan pembayaran uang reservasi yang telah masuk
                        tidak dapat dikembalikan/hangus.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        7.
                    </td>
                    <td style="vertical-align: top">
                        Penggantian nama setelah pemilihan unit dikenakan biaya administrasi sebesar 2%
                        dari harga unit tersebut.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        8.
                    </td>
                    <td style="vertical-align: top">
                        Pembayaran uang reservasi/pembayaran lainnya melalui cash, harap diserahkan langsung
                        ke kasir WPS dengan alamat : Grand Kawanua International City, Jl A.A Maramis, Kayuwuti,
                        Kairagi II. Manado Sulawesi Utara. No. telp : (0431)818919
                        <br />
                        <br />
                        Pembayaran melalui transfer, dialamatkan ke :
                        <table style="width: 80%; margin-left: 3%;">
                            <tr>
                                <td>
                                    PT WENANG PERMAI SENTOSA<br />
                                    Bank Permata Cabang Wisma AKR Jakarta<br />
                                    No Rek : 0750 638 058
                                </td>
                                <td>
                                    PT WENANG PERMAI SENTOSA<br />
                                    Bank BCA Jakarta<br />
                                    No Rek : 4910259569
                                </td>
                            </tr>
                        </table>
                        <br />
                        Mohon disertakan nama pemesan untuk setiap uang yang ditransfer.
                    </td>
                </tr>
            </table>
            <br />
            <h3>
                B. ACARA LAUNCHING</h3>
            <table style="width: 100%">
                <tr>
                    <td style="width: 4%; text-align: right; vertical-align: top">
                        1.
                    </td>
                    <td style="width: 96%; vertical-align: top">
                        Setiap pemesanan 1 unit wajib memilih 3 alternative pilihan unit dan berlaku kelipatan
                        untuk alternative pilihan apabila ingin membeli lebih dari 1 unit. Kesalahan penulisan
                        nama cluster/blok/unit/nomor bukan merupakan tanggung jawab pengembang / developer.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        2.
                    </td>
                    <td style="vertical-align: top">
                        Setelah pemilihan unit, para pemesan harus menyelesaikan proses administrasi dengan
                        menandatangani SPU dan melunasi booking fee hingga sebesar Rp.20.000.000,-/unit.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        3.
                    </td>
                    <td style="vertical-align: top">
                        Apabila terjadi pembatalan pembelian unit, maka reservasi fee Rp.5.000.000,-/unit
                        akan dikembalikan full 100% untuk Pembayaran Cash/Transfer dan pembayaran menggunakan
                        mesin EDC (kartu kredit & debit) akan dipotong biaya administrasi bank.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        4.
                    </td>
                    <td style="vertical-align: top">
                        PT. WPS telah menyeleksi dan memberikan pelatihan secara saksama kepada setiap tenaga
                        penjual atau penerima pesanana. Walaupun demikian, PT WPS tidak bertanggung jawab
                        seandainya terjadi kerugian dan/atau penyalahgunaan dana, terutama akibat tidak
                        disetorkannya secara langsung ke alamat dan rekening WPS diatas
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        5.
                    </td>
                    <td style="vertical-align: top">
                        Harap Formulir Reservasi ini disimpan dan dibawah salinan originalnya pada saat
                        launching.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        6.
                    </td>
                    <td style="vertical-align: top">
                        Segala konsekuensi akibat hilangnya Formulir Reservasi ini menjadi tanggung jawab
                        Pemesan
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top">
                        7.
                    </td>
                    <td style="vertical-align: top">
                        Hal-hal yang belum diatur atau belum cukup diatur, merupakan wewenang sepenuhnya
                        PT Wenang Permai Sentosa selaku pengembang Grand Kawanua International City
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divInfo" runat="server" visible="false">
        <h1>
            Tidak dapat mencetak Form NUP</h1>
        <p>
            Karena :</p>
        <p>
            1. Data wajib customer untuk NUP belum lengkap</p>
        <p>
            2. Customer belum melakukan pembayaran NUP pertama</p>
    </div>
</body>

