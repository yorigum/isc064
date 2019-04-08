<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintPPJBTemplate" CodeFile="PrintPPJBTemplate.ascx.cs" %>

<style type="text/css">
    .Halaman {
        padding-top: 4em;
    }

    .PageBreak {
        page-break-before: always;
    }

    .Font {
        font-family: Calibri;
        font-size: 13pt;
        line-height: 40px;
    }

    .TAlignC {
        text-align: center;
    }

    .TAlignJ {
        text-align: justify;
    }

    .W100 {
        width: 100%;
    }

    .W50 {
        width: 50%;
    }

    .W2 {
        width: 2%;
    }

    .VAlignT {
        vertical-align: top;
    }

    .CellSpacing {
        border-spacing: 0;
    }
</style>

<div id="content" runat="server"></div>
<%--<div id="Halaman01" runat="server" class="Halaman W100">
    <asp:Table ID="Hal01" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal01L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>PERJANJIAN PENGIKATAN JUAL BELI</b></asp:TableCell>
                        <asp:TableCell CssClass="W2" RowSpan="6"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>TANAH DAN BANGUNAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><asp:Label ID="noppjb" runat="server"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC">&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Perjanjian Pengikatan Jual Beli Tanah Dan Bangunan (Rumah Tinggal) di Perumahan SAVASA ini (untuk selanjutnya
                            disebut “<b>Perjanjian</b>“) dibuat dan ditandatangani di
                            <asp:Label ID="lbltempatid" runat="server" />
                            pada hari
                            <asp:Label ID="lblhariid" runat="server" />
                            tanggal 
                            <asp:Label ID="lbltanggalid" runat="server" />, oleh dan antara:
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="W100 VAlignT">
                            <asp:Table ID="Hal01L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell Width="10%" CssClass="Font VAlignT">I.</asp:TableCell>
                                    <asp:TableCell Width="15%" CssClass="Font VAlignT">Nama</asp:TableCell>
                                    <asp:TableCell Width="1%" CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell><b>TAKAYA MOTOOKA</b></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">Jabatan</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">
                                        Direktur <i>(Penerima Kuasa dari Presiden Direktur No: 63/PHDI/SK-DIR/VII/2018)</i>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">Nama</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT"><b>HONGKY JEFFRY NANTUNG</b></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">Jabatan</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">
                                        Wakil Presiden Direktur
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal01R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2" RowSpan="6"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignC"><b>CONDITIONAL SALE AND PURCHASE</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>AGREEMENT LAND AND BUILDING</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><asp:Label ID="noppjb_eng" runat="server"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            This conditional agreement on the Sale and purchase of Land and Building Sale (Residence) in SAVASA Residence 
                            (hereinafter referred to as an "<b>Agreement</b>") is made and entered at
                            <asp:Label ID="lbltempaten" runat="server" />
                            , on the day
                            <asp:Label ID="lblharien" runat="server" />
                            date 
                            <asp:Label ID="lbltanggalen" runat="server" />, by:
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="W100 VAlignT">
                            <asp:Table ID="Hal01R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell Width="10%" CssClass="Font VAlignT">I.</asp:TableCell>
                                    <asp:TableCell Width="15%" CssClass="Font VAlignT">Name</asp:TableCell>
                                    <asp:TableCell Width="1%" CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell><b>TAKAYA MOTOOKA</b></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">Position</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">
                                        Director <i>(the Power of Attorney of the President Director No:. 63 /PHDI/SK-DIR/VII/2018)</i>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">Name</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT"><b>HONGKY JEFFRY NANTUNG</b></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">Position</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">
                                        Vice President Director
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman02" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal02" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal02L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            dalam hal ini secara bersama-sama bertindak sesuai dalam jabatannya tersebut mewakili 
                            Direksi dari dan oleh karenanya sah bertindak untuk dan atas nama perseroan terbatas 
                            <b><asp:Label ID="pers" runat="server"></asp:Label></b> berkedudukan di Kabupaten Bekasi dan beralamat di Kota 
                            Deltamas (Marketing Gallery SAVASA), Green Land Square Blok BA No. 1A, Desa Sukamahi, 
                            Kecamatan Cikarang Pusat, Kabupaten Bekasi 17530. Untuk selanjutnya disebut <b>PENJUAL</b>
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2" RowSpan="3"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="W100 VAlignT">
                            <asp:Table ID="Hal02L01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell Width="10%" CssClass="Font VAlignT">II.</asp:TableCell>
                                    <asp:TableCell Width="15%" CssClass="Font VAlignT">Nama</asp:TableCell>
                                    <asp:TableCell Width="1%" CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblnamaid" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">NIK</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblnikid" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">Alamat</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblalamatid" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT" HorizontalAlign="Right">-</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT" ColumnSpan="3">untuk selanjutnya disebut <b>“PEMBELI”</b>.</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            <b>PENJUAL</b> dan <b>PEMBELI</b> bersama-sama, untuk selanjutnya disebut <b>Para Pihak</b>.
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal02R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2" RowSpan="3"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            in this matter jointly acting in accordance with the positions aforementioned representing 
                            the Board of Directors of and therefore validly acting for and on behalf of the limited 
                            liability company of <b><asp:Label ID="pers_eng" runat="server"></asp:Label></b> having its domicile in Bekasi Regency 
                            and at Kota Deltamas (Marketing Gallery SAVASA), Green Land Square Blok BA No. 1A, Sukamahi 
                            Village, Cikarang Pusat Sub-district, Bekasi Regency 17530. Hereinafter referred to as the <b>SELLER</b>.
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="W100 VAlignT">
                            <asp:Table ID="Hal02R01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell Width="10%" CssClass="Font VAlignT">II.</asp:TableCell>
                                    <asp:TableCell Width="15%" CssClass="Font VAlignT">Name</asp:TableCell>
                                    <asp:TableCell Width="1%" CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblnamaen" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">NIK</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblniken" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">Address</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblalamaten" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT" HorizontalAlign="Right">-</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT" ColumnSpan="3">hereinafter referred to as the <b>"BUYER"</b>.</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Hereinafter <b>the SELLER and BUYER</b> jointly referred to as the <b>Parties</b>.
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman03" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal03" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal03L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Para Pihak menerangkan terlebih dahulu :
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2" RowSpan="2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table ID="Hal03L01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT w2">-</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Bahwa PENJUAL adalah Perusahaan Pengembang yang akan/sedang/telah membangun wilayah 
                                        lingkungan perumahan dan/atau bangunan bertingkat untuk pertokoan, perkantoran, 
                                        perdagangan serta fasilitas penunjangnya yang terletak di KOTA DELTAMAS dikenal 
                                        sebagai perumahan <b>“SAVASA”</b>, berlokasi di Jalan Raya Boulevard, Desa Hegarmukti dan 
                                        Desa Pasirranji, Kecamatan Cikarang Pusat, Kabupaten Bekasi, Propinsi Jawa Barat.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">-</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Bahwa PENJUAL bermaksud untuk menjual dan nanti pada waktunya menyerahkan 
                                        sebidang tanah dan bangunan di perumahan SAVASA untuk selanjutnya disebut 
                                        <b>“Tanah dan Bangunan“</b> (sebagaimana didefinisikan 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal03R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Witnesseth :
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2" RowSpan="2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table ID="Hal03R01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT w2">-</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        That the SELLER is a Developer that will construct/are constructing/have constructed a 
                                        residential area and/or multi-storey building for shops, offices, trade center and 
                                        supporting facilities thereof located in DELTAMAS CITY known as <b>"SAVASA"</b> residence, 
                                        on Jalan Raya Boulevard, Hegarmukti and Pasirranji Villages, Cikarang Pusat 
                                        Sub-district, Bekasi Regency, West Java Province.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">-</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        That the SELLER intends to sell and afterward handovers the plot of land and 
                                        building on SAVASA residence hereinafter referred to as <b>"Land and Building"</b> 
                                        (as defined in Article 1 paragraph (2) of this Agreement) to  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman04" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal04" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal04L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table ID="Hal04L01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT w2"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        dalam Pasal 1 ayat (2) Perjanjian ini) kepada PEMBELI yang dengan ini menyetujui 
                                        untuk membeli dan nanti pada waktunya menerima penyerahan atas Tanah dan Bangunan 
                                        tersebut sesuai syarat-syarat dan ketentuan-ketentuan dalam Perjanjian ini.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">-</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Bahwa karena penandatanganan Akta Jual Beli di hadapan Pejabat Pembuat Akta 
                                        Tanah <b>(“PPAT”)</b> atas Tanah dan Bangunan yang akan diperjual belikan pada saat 
                                        ini belum dapat dilakukan, maka Para Pihak dengan ini sepakat untuk mengadakan 
                                        Perjanjian ini menurut syarat-syarat dan ketentuan-ketentuan sebagai berikut :
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2" RowSpan="3"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 1</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>DEFINISI</b></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal04R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2" RowSpan="3"></asp:TableCell>
                        <asp:TableCell>
                            <asp:Table ID="Hal04R01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT w2"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        the BUYER who hereby agrees to purchase and afterward accepts the Land and 
                                        Building handed over under the terms and conditions of this Agreement.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">-</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        That due to the Sale and Purchase Deed of Land and Building to be sold and 
                                        purchased was still not executed before the Land Deed Official <b>("PPAT")</b>, 
                                        the Parties hereby agree to enter into this Agreement under the following 
                                        terms and conditions:  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 1</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>DEFINITION</b></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman05" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal05" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal05L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Dalam Perjanjian ini yang dimaksud dengan :
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2" RowSpan="2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table ID="Hal05L01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT w2">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Label ID="lbltempatid2" runat="server"></asp:Label> adalah suatu perumahan dengan konsep Smart Lifestyle memiliki 
                                        wilayah komersial berlokasi di Kota Deltamas industrial yang berlokasi di 
                                        Kecamatan Cikarang Pusat, Kabupaten Bekasi, Propinsi Jawa Barat.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Tanah dan Bangunan adalah sebidang tanah berikut bangunan yang berdiri 
                                        diatasnya, yang telah dipilih dan dimaksudkan untuk dibeli oleh PEMBELI dari 
                                        PENJUAL sebagaimana diuraikan dalam Pasal 2 ayat (1) Perjanjian ini. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Harga Pengikatan adalah sejumlah uang yang harus dibayar oleh PEMBELI 
                                        kepada PENJUAL untuk pembelian Tanah dan Bangunan sebagaimana diuraikan 
                                        dalam Pasal 3 ayat (1) Perjanjian ini. 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal05R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2" RowSpan="2"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            In this Agreement, the following terms shall mean:
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table ID="Hal05R01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT w2">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Label ID="lbltempaten2" runat="server"></asp:Label> is a residence with the concept of Smart Lifestyle having a commercial area at 
                                        Kota Deltamas industrial in Cikarang Pusat Sub-district, Bekasi Regency, West Java Province.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The Land and Building is a plot of land along with the building on it, which has been 
                                        selected and intended to be purchased by the BUYER from the SELLER as defined in the 
                                        Article 2 paragraph (1) of this Agreement.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The conditional price is the amount of money to be paid by the BUYER to the SELLER 
                                        for the purchase of Land and Building as defined in the Article 3 paragraph (1) 
                                        of this agreement.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman06" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal06" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal06L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table ID="Hal06L01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT w2">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI adalah perseorangan atau badan hukum yang memenuhi syarat sebagai pemegang hak atas tanah.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Berita Acara Serah Terima <b>(“BAST”)</b> adalah bukti penyerahan Tanah dan Bangunan 
                                        secara fisik dari PENJUAL kepada PEMBELI sebagaimana dimaksud dalam Pasal 6 Perjanjian ini. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">6.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Perjanjian adalah Perjanjian Pengikatan Jual Beli ini termasuk penambahan, 
                                        perubahan dan lampirannya (bila ada). 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">7.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Sertipikat Hak Guna Bangunan <b>(“Sertipikat”)</b> adalah tanda bukti kepemilikan hak 
                                        atas Tanah dan Bangunan yang dikeluarkan oleh instansi yang berwenang.  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal06R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2" RowSpan="2"></asp:TableCell>
                        <asp:TableCell>
                            <asp:Table ID="Hal06R01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT w2">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The BUYER is an individual or legal entity complying with the requirements as holders 
                                        of land rights.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The Minutes of Handover <b>("BAST")</b> is the physical evidence of Land and Building handover 
                                        from the SELLER to the BUYER as referred to in Article 6 of this Agreement. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">6.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The agreement is the Conditional Agreement of Sale and Purchase including 
                                        additions, modifications and attachments thereto (if any). 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT">7.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The Certificate of Right to Build <b>("Certificate")</b> is an evidence of 
                                        ownership of the rights of Land and Building issued by the competent authority.   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman07" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal07" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal07L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 2</b></asp:TableCell>
                        <asp:TableCell CssClass="W2" RowSpan="6"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>OBJEK JUAL BELI</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table ID="Hal07L01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT" Width="2%">1. </asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PENJUAL dengan ini berjanji dan mengikatkan diri untuk menjual dan nanti pada 
                                        waktunya menyerahkan kepada PEMBELI, dan PEMBELI dengan ini berjanji dan 
                                        mengikatkan diri untuk membeli dan nanti pada waktunya menerima penyerahan 
                                        Tanah dan Bangunan dari PENJUAL di :
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Table ID="Hal07L02" runat="server" CssClass="W100">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT W2"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="35%">Lokasi</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="1%">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Perumahan <asp:Label ID="lbltempatid3" runat="server"></asp:Label></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Tipe</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lbltipeid" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Jalan</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lbljalanid" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Nomor Unit</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblnounitid" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Luas Tanah</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblluastanahid" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Luas Bangunan</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblluasbangunanid" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Keadaan fisik dan letak Tanah tersebut dalam hal ini telah diketahui 
                                        dan disetujui oleh PEMBELI.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal07R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2" RowSpan="6"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 2</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>SALE AND PURCHASE OBJECT</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table ID="Hal07R01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT" Width="2%">1. </asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        THE SELLER hereby pledges and binds it to sell and afterward hands over to the 
                                        BUYER, and the BUYER hereby pledges and binds itself to buy and afterward time 
                                        accepts the Land and Building handed over from SELLER in :
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Table ID="Hal07R02" runat="server" CssClass="W100">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT W2"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="40%">Location</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="1%">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Perumahan <asp:Label ID="lbltempaten3" runat="server"></asp:Label> </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Type</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lbltipeen" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Street</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lbljalanen" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Unit Number</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblnouniten" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Area of the Land</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblluastanahen" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Area of the Building</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblluasbangunanen" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        In this case, the physical condition and location of the 
                                        land have been known and approved by the BUYER.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman08" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal08" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal08L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font VAlignT" Width="2%">2. </asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            pabila terdapat perbedaan luas tanah yang tercantum dalam ayat (1) pasal ini dengan luas 
                            tanah yang tercantum dalam Sertipikat, maka Para Pihak sepakat untuk tunduk pada luas 
                            tanah yang tercantum dalam Sertipikat, dengan ketentuan kekurangan atau kelebihan luas 
                            tanah sampai dengan 1 M2 (satu meter persegi) tidak mengubah Harga Pengikatan. Perbedaan 
                            luas tanah yang lebih dari 1 M2 (satu meter persegi) akan diperhitungkan kembali dengan 
                            satuan harga tanah sebesar Rp. <asp:Label ID="lblhargaid" runat="server" />,- (diluar PPn) per meter persegi dan harus dilunasi 
                            oleh salah satu pihak tanpa bunga dalam waktu 7 (tujuh) hari setelah pemberitahuan 
                            dan/atau surat pengajuan kekurangan atau kelebihan atas Harga Pengikatan dari pihak terkait.
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal08R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font VAlignT" Width="2%">2. </asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            If the area of the land as referred to in the paragraph (1) of this article differs from 
                            the area of the land as referred to in the Certificate, the Parties agree to be subject to 
                            the area of the land as stated on the Certificate, provided that the difference of area of 
                            the land more less than 1 M2 (one square meter) shall not change the conditional price. The 
                            difference of the area of the land more than 1 M2 (one square meter) shall be recalculated at 
                            the unit price of the land of Rp. <asp:Label ID="lblhargaen" runat="server" />, - (excluding VAT) per square meter and shall be fully 
                            paid by one of the parties without interest within 7 (seven) days as of a notification and/or 
                            a letter claiming the difference of the Conditional Price from the relevant party.
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman09" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal09" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal09L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font VAlignT" Width="2%">3. </asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Perpindahan letak Tanah dan Bangunan dikenakan biaya administrasi sebesar 1% (satu persen) 
                            dari Harga Pengikatan. Perpindahan letak Tanah dan Bangunan sebagaimana dimaksud dalam ayat 
                            1 Pasal ini hanya diperkenankan selama Harga Pengikatan atas Tanah dan Bangunan belum dibayar 
                            lunas seluruhnya dan PEMBELI tidak mempunyai tunggakan maupun denda keterlambatan atas pembayaran 
                            Harga Pengikatan.
                            <br />
                            Khusus untuk pajak-pajak yang telah dibayarkan oleh PEMBELI dan PENJUAL kepada instansi yang berwenang 
                            berkenaan dengan objek Tanah dan Bangunan yang lama yang tidak
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal09R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font VAlignT" Width="2%">3. </asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            The change of Land and Building location shall be subject to an administrative fee of 
                            1% (one percent) of the conditional Price. The change of land and building as referred 
                            to in paragraph 1 of this article shall be only permitted to the extent that the conditional 
                            price of land and building has not been fully paid and the BUYER does have any arrears or 
                            delay penalties payable to the Conditional Price.
                            <br />
                            Only for the tax paid by the BUYER and the SELLER to the competent authority relating 
                            to the old Land and Building objects that cannot be 
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman10" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal10" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal10L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font VAlignT" Width="2%" RowSpan="5"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            dapat diperhitungkan atau dialihkan untuk objek tanah dan bangunan yang baru, 
                            wajib ditanggung oleh PEMBELI sepenuhnya dan harus dibayar oleh PEMBELI kepada PENJUAL 
                            sebelum dilakukan perpindahan atas letak Tanah dan Bangunan tersebut. Perpindahan letak 
                            Tanah dan Bangunan ke unit yang Harga Pengikatan lebih murah hanya diperkenankan apabila 
                            ada pemotongan /pengurangan plafond Kredit Pemilikan Rumah (selanjutnya disebut “KPR”) 
                            PEMBELI dari bank pemberi KPR. 
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 3</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>HARGA PENGIKATAN DAN CARA</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>PEMBAYARANNYA</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal10L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Harga Pengikatan jual beli Tanah dan Bangunan adalah sebagai berikut:
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal10R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell Width="4%" RowSpan="5"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            calculated or transferred to new land and building objects, it shall be fully borne by 
                            the BUYER and it shall be paid by the BUYER to the SELLER prior to the change of the 
                            location of the Land and Building. The change of the location of Land and Building to 
                            units with lower conditional prices shall be only permitted if the BUYER is given 
                            deduction/reduction of House Ownership Credit ceiling (hereinafter referred to as the "KPR") 
                            from the Bank provided the KPR.
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 3</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>CONDITIONAL PRICE OF AND THE</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>PAYMENT METHOD</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal10R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The conditional Prices of land and building purchase and sales are as follows:
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman11" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal11" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal11L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font" ColumnSpan="3">
                            <asp:Table ID="Hal11L01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font" Width="35%">Harga Tanah dan Bangunan</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT" Width="1%">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblhargatanahid" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">PPN</asp:TableCell>
                                    <asp:TableCell CssClass="Font">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblppnid" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font" HorizontalAlign="Right"><b>TOTAL</b></asp:TableCell>
                                    <asp:TableCell CssClass="Font">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbltotalid" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font" ColumnSpan="3">Terbilang :  <asp:Label ID="lblterbilangid" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2" RowSpan="4"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                        <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Cara pembayaran Harga Pengikatan dari PEMBELI ke PENJUAL dilakukan dengan 
                            cara <asp:Label ID="lblcarabayarid" runat="server" /> dengan jadwal pembayaran ditentukan sebagai berikut :
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                        <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                        <asp:TableCell CssClass="Font" ColumnSpan="2">
                            <asp:Table ID="Hal11L02" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font" Width="40%">Uang Tanda Jadi</asp:TableCell>
                                    <asp:TableCell CssClass="Font" Width="1%">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblbfid" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">Uang Muka ke-1</asp:TableCell>
                                    <asp:TableCell CssClass="Font">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbldp1id" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">Uang Muka ke-2</asp:TableCell>
                                    <asp:TableCell CssClass="Font">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbldp2id" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">Pelunasan</asp:TableCell>
                                    <asp:TableCell CssClass="Font">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblpelid" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                        <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Biaya Notaris/PPAT untuk Akta Jual Beli dan biaya balik nama Sertipikat ke atas nama PEMBELI) 
                            termasuk Penerimaan Negara Bukan Pajak (PNBP) sebagaimana dimaksud ayat 5 
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal11R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2" RowSpan="4"></asp:TableCell>
                        <asp:TableCell CssClass="Font" ColumnSpan="3">
                            <asp:Table ID="Hal11R01" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font" Width="35%">Land and Building Prices</asp:TableCell>
                                    <asp:TableCell CssClass="Font VAlignT" Width="1%">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblhargatanahen" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">Value Added Tax</asp:TableCell>
                                    <asp:TableCell CssClass="Font">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblppnen" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font" HorizontalAlign="Right"><b>TOTAL</b></asp:TableCell>
                                    <asp:TableCell CssClass="Font">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbltotalen" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font" ColumnSpan="3">In Words : <asp:Label ID="lblterbilangen" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>

                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                        <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            The payment method of the conditional Price from the BUYER to the SELLER 
                            shall be made by <asp:Label ID="lblcarabayaren" runat="server" /> with the following payment schedule:
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                        <asp:TableCell CssClass="Font VAlignT"></asp:TableCell>
                        <asp:TableCell CssClass="Font" ColumnSpan="2">
                            <asp:Table ID="Hal11R02" runat="server" CssClass="W100">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font" Width="50%">Booking Fee</asp:TableCell>
                                    <asp:TableCell CssClass="Font" Width="1%">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblbfen" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">1<sup>st</sup> Down Payment</asp:TableCell>
                                    <asp:TableCell CssClass="Font">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbldp1en" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">2<sup>nd</sup> Down Payment</asp:TableCell>
                                    <asp:TableCell CssClass="Font">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbldp2en" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">Settlement</asp:TableCell>
                                    <asp:TableCell CssClass="Font">:</asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblpelen" runat="server" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                        <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            The Notary/PPAT (Land Deed Official) fees of Purchase and Sale Deed and Certificate 
                            of title transfer under the BUYER name including Non-Tax State  
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman12" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal12" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal12L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal12L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        huruf b di bawah akan diinformasikan oleh Penjual/Notaris/PPAT dan wajib dibayar oleh 
                                        PEMBELI sebelum penandatanganan Akta Jual Beli (AJB) dilaksanakan.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila PEMBELI akan melunasi sisa pembayaran Harga Pengikatan melalui pinjaman KPR 
                                        dari bank dan KPR tersebut tidak disetujui atau ditolak oleh bank, dikarenakan PEMBELI 
                                        tidak melengkapi data/ persyaratan yang ditentukan oleh bank, maka hal tersebut menjadi 
                                        tanggung jawab PEMBELI sepenuhnya dan PEMBELI tetap berkewajiban untuk melunasi sisa 
                                        pembayaran Harga Pengikatan pada tanggal yang telah disepakati oleh Para Pihak 
                                        sebagaimana dimaksud dalam ayat 1 pasal ini dan keterlambatan 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal12R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal12R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Revenues (PNBP) as referred to in paragraph 5 item b hereunder shall be informed by 
                                        Seller/Notary/PPAT and shall be paid by BUYER prior to the execution of the Sale 
                                        and Purchase Deed (AJB).
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the BUYER shall repay the remaining Conditional Price through KPR loan from the Bank 
                                        and the KPR is not approved or is rejected by the bank, because the BUYER does not 
                                        furnish the data/ requirements stipulated by the bank, the matters said shall be the 
                                        full responsibility of the BUYER and the BUYER shall remain repay the remaining 
                                        conditional price on the date agreed upon by the Parties as referred to in paragraph 
                                        1 of this article and the 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman13" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal13" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal13L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal13L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        pembayaran Harga Pengikatan akan dikenakan sanksi sebagaimana dimaksud 
                                        dalam Pasal 4 jo.Pasal 12 ayat 2 Perjanjian ini.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila bank tidak menyetujui permohonan KPR yang diajukan oleh 
                                        PEMBELI atas pertimbangan bank semata-mata dimana PEMBELI bermaksud 
                                        untuk membatalkan Perjanjian ini maka berlaku ketentuan sebagaimana 
                                        dimaksud dalam Pasal 12 ayat 3 Perjanjian ini. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Harga Pengikatan tersebut sudah termasuk biaya-biaya: 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal13L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Pembuatan Sertpikat HGB (Induk);</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Pengurusan Izin Mendirikan Bangunan (IMB);</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">c.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Pemasangan instalasi listrik PLN;</asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal13R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal13R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        delay to repay the Conditional Price shall be subject to sanctions 
                                        as referred to in Article 4 in conjunction with Article 12 paragraph 2 of this Agreement.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the bank does not approve the KPR application submitted by the BUYER under the 
                                        sole bank’s consideration that the BUYER intends to terminate this Agreement, 
                                        then the provisions as referred to in Article 12 paragraph 3 of this Agreement shall apply. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The conditional price shall include the following Costs: 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal13R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Application of Right To Build (HGB) Certificate (main land);</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">The Building Permit (IMB);</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">c.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">PLN electrical installation;</asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman14" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal14" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal14L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal14L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal14L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">d.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Pemasangan instalasi air bersih; dan</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">e.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Pemasangan instalasi jaringan telepon sampai dengan panel pembagi tertentu.</asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Sedangkan biaya-biaya lainnya yang menjadi beban PEMBELI termasuk tetapi tidak terbatas pada:
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal14L03" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Bea Perolehan Hak Atas Tanah dan Bangunan (BPHTB);</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Biaya Notaris/PPAT untuk Akta Jual Beli dan biaya pengurusan balik nama 
                                                    sertipikat ke atas nama Pembeli termasuk tapi tidak terbatas pada Penerimaan 
                                                    Negara Bukan Pajak (PNBP) terkait dengan transaksi sesuai ketentuan yang berlaku;
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">c.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Penggantian Pemeliharaan Lingkungan (PPL) yang meliputi : kebersihan, keamanan, 
                                                    pemeliharaan (maintenance) dan Penerangan Jalan Umum (PJU);
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal14R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal14R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal14R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">d.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Clean water installation; and</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">e.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Telephone network installation to certain line panels.</asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        While other costs to be borne by BUYER shall include but not limited to:
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal14R03" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Fees for Acquisition of Rights to Lands and Buildings (BPHTB);</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    The fees of Notary/PPAT of Sale and Purchase Deed and fees to obtain land 
                                                    title transfer in name of the Buyer, including but not limited to Non-Tax 
                                                    State Revenues (PNBP) related to the transactions according to the applicable 
                                                    regulations;
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">c.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    The reimbursement of Vicinity Maintenance costs (PPL) including: cleaning service, 
                                                    security, maintenance and Public Street Lighting (PJU);
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman15" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal15" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal15L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal15L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal15L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">d.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Pajak Bumi dan Bangunan (PBB);</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">e.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Biaya bulanan, antara lain: Penggantian Pemakaian Air (PPA), 
                                                    aktivasi dan balik nama listrik, telepon ke atas nama PEMBELI.
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font">&nbsp;</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font TAlignJ" ColumnSpan="2">
                                                    Untuk menghindari keraguan atas biaya-biaya tersebut, dengan ketentuan : 
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">&bull;</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Biaya pada poin a, b, dan c wajib dibayar oleh PEMBELI sebelum 
                                                    penandatanganan AJB dilaksanakan setelah adanya pemberitahuan dari PENJUAL.
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">&bull;</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Apabila dikemudian hari atas transaksi ini dan pendaftaran di 
                                                    kantor BPN dikenakan biaya-biaya, pajak-pajak (termasuk BPHTB), 
                                                    peningkatan nilai pajak,
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal15R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal15R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal15R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">d.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">Land and Building Tax (PBB);</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">e.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Monthly fees, including: Compensation of Water Bill (PPA), 
                                                    activation and title transfer of the electricity, telephone in the name of the BUYER.
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font">&nbsp;</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font TAlignJ" ColumnSpan="2">
                                                    For the avoidance of the doubts concerning the costs, provided that: 
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">&bull;</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    The costs in the items a, b, and c shall be paid by 
                                                    the BUYER prior to the execution of the AJB after the notification from the SELLER.
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">&bull;</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    If at the later date, the transaction and registration 
                                                    in BPN (National Land agency) office are subject to fees, 
                                                    taxes (including BPHTB), increase of the value of taxes, 
                                                    levies or other fees in any form, then all the said shall
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman16" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal16" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal16L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal16L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal16L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    retribusi atau pungutan lainnya dalam bentuk apapun, 
                                                    maka kesemuanya itu menjadi tanggungan PEMBELI dan 
                                                    harus dibayarkan dalam waktu 7 (tujuh) hari kalender 
                                                    setelah diminta oleh PENJUAL dan PEMBELI dengan ini melepaskan 
                                                    haknya untuk menuntut PENJUAL apabila terjadi peningkatan 
                                                    atau penambahan biaya, pajak atau pungutan tersebut di atas.
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">6.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Harga Pengikatan beserta biaya biaya yang lain yang timbul berdasarkan 
                                        Perjanjian ini harus dibayar oleh PEMBELI kepada PENJUAL melalui dan atau  ditujukan ke :
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font"><b>PT PANAHOME DELTAMAS INDONESIA</b></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">................................................................</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">Cabang</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">................................................................</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal16R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal16R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal16R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    be borne by the BUYER and shall be paid within 7 ( seven) 
                                                    calendar days after requested by the SELLER and the BUYER 
                                                    hereby waives its right to claim the SELLER if the fees, 
                                                    taxes or levies abovementioned increase or there is an addition 
                                                    to the matters said.
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">6.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The conditional Prices and other costs incurred under this Agreement 
                                        shall be paid by the BUYER to the SELLER through and or to:
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font"><b>PT PANAHOME DELTAMAS INDONESIA</b></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">................................................................</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">Branch</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">................................................................</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman17" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal17" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal17L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal17L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        dan sesuai dengan petunjuk pembayaran yang disiapkan oleh PENJUAL. 
                                        Pembayaran yang menggunakan cheque/bilyet giro harus ditujukan kepada rekening 
                                        Virtual Account yang telah ditetapkan PENJUAL dan baru dianggap “Sah” setelah 
                                        Cheque/Bilyet giro dapat dicairkan/ diuangkan dan telah dikeluarkan kwitansi 
                                        resmi dari PENJUAL.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">7.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Setiap pembayaran Harga Pengikatan beserta biaya-biaya lain yang harus dibayar 
                                        oleh PEMBELI kepada PENJUAL berdasarkan Perjanjian ini harus dilakukan secara penuh 
                                        dan tanpa potongan apapun, kecuali pajak-pajak yang timbul sebagaimana dimaksud dalam 
                                        Pasal 5 Perjanjian ini.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal17R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal17R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        and according to the payment method instructed by the SELLER. The Payment with the 
                                        cheque/bilyet giro shall be made to the Virtual Account designated by the SELLER 
                                        and shall be considered "Valid" after the Cheque/Bilyet giro can be disbursed/cashed 
                                        and after the issuance of the official receipt from the SELLER.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">7.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Any payment of the Conditional Price and other costs to be paid by the BUYER to 
                                        the SELLER under this Agreement shall be made in full and without any deductions, 
                                        except for taxes arising as referred to in Article 5 of this Agreement.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman18" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal18" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal18L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 4</b></asp:TableCell>
                        <asp:TableCell CssClass="Font VAlignT w2" RowSpan="4"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>KETERLAMBATAN PEMBAYARAN,</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>DENDA DAN SANKSI</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal18L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila PEMBELI dengan alasan apapun tidak melaksanakan pembayaran Harga Pengikatan 
                                        kepada PENJUAL pada waktu yang ditetapkan di dalam Pasal 3 ayat (1) Perjanjian ini, 
                                        maka PEMBELI dikenakan denda keterlambatan pembayaran sebesar 1‰ (satu permil) untuk 
                                        setiap hari keterlambatan yang dihitung dari jumlah angsuran yang terlambat dibayar. 
                                        Denda dihitung sejak keterlambatan pembayaran tersebut terjadi sampai dengan pembayaran 
                                        dilakukan, dan wajib dibayar sekaligus lunas bersama-sama dengan angsuran pokoknya oleh 
                                        PEMBELI kepada PENJUAL dalam waktu 7 (tujuh) hari setelah diminta oleh PENJUAL. 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal18R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font VAlignT w2" RowSpan="4"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 4</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>DELAY OF PAYMENT, PENALTY AND</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>SANCTION</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal18R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the BUYER for any reason does not make the payment of the Conditional Price 
                                        to the SELLER at the time specified in Article 3 paragraph (1) of this Agreement, 
                                        then the BUYER shall be subject to a delay of the payment penalty of 1 ‰ (one permil) 
                                        for each day of delay, which is calculated from the amount of the installment delay 
                                        to be paid. The penalty is calculated from the delay of the payment to the payment is 
                                        made, and shall be fully paid at once along with the principal by the BUYER to the 
                                        SELLER within 7 (seven) days after requested by the SELLER.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman19" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal19" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal19L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal19L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila PEMBELI lalai melakukan kewajiban pembayaran angsuran Harga Pengikatan 
                                        beserta dendanya kepada PENJUAL sebagaimana dimaksud dalam ayat (1) pasal ini, 
                                        kelalaian mana telah diberikan surat teguran atau peringatan dari PENJUAL kepada 
                                        PEMBELI sebanyak 3 (tiga) kali dan PEMBELI tetap tidak melakukan kewajibannya, 
                                        maka dalam hal demikian bukti pengiriman surat teguran atau peringatan tersebut 
                                        merupakan bukti cukup untuk menganggap bahwa PEMBELI telah lalai, dan Para Pihak 
                                        sepakat bahwa Perjanjian ini menjadi batal dengan sendirinya sesuai ketentuan dalam 
                                        Pasal 12 ayat (2) Perjanjian ini. 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal19R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal19R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the BUYER is negligent to pay the Installment of the Conditional Price along 
                                        with the penalty to the SELLER as referred to in paragraph (1) of this article, 
                                        that the reprimand or warning letters has been issued for the negligence from the 
                                        SELLER to the BUYER 3 (three) times and the BUYER still does not perform the 
                                        obligations, then in this case, the delivery receipt of the reprimand or warning 
                                        letters shall constitute a sufficient evidence to assume that the BUYER has been 
                                        negligent, and the Parties agree that this Agreement shall be automatically terminated 
                                        according to the provisions of Article 12 paragraph (2) of this Agreement.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman20" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal20" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal20L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal20L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila PEMBELI melunasi pembelian Tanah dan Bangunan dalam Perjanjian ini melalui 
                                        pinjaman dari Bank, yang telah mempunyai perjanjian kerjasama dengan PENJUAL, maka 
                                        apabila PEMBELI wanprestasi atau lalai membayar angsuran sebanyak 3 (tiga) kali 
                                        berturut-turut pada Bank yang cukup dibuktikan dengan surat pemberitahuan dari 
                                        Bank saja, dan telah dinyatakan lalai oleh pihak Bank, maka PEMBELI dengan ini 
                                        setuju bahwa PENJUAL berhak untuk mengambil alih hak-hak yang dimiliki oleh Bank 
                                        berdasarkan perjanjian kredit yang telah di tandatangani oleh Bank dengan PEMBELI (subrogasi). 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal20R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal20R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the BUYER repays Land and Building purchases in this Agreement through a loan from a 
                                        Bank, which has a cooperation agreement with the SELLER, then if the BUYER is in default 
                                        or fails to pay the installments in 3 (three) consecutive times to the Bank that is 
                                        sufficiently evidenced by a notification from the Bank, and has been declared in 
                                        negligence by the Bank, the BUYER hereby agrees that SELLER has the right to take 
                                        over the rights held by the Bank in accordance with the credit agreement signed 
                                        by the Bank with the BUYER (subrogation).
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman21" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal21" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal21L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 5</b></asp:TableCell>
                        <asp:TableCell CssClass="Font VAlignT w2" RowSpan="8"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>PAJAK-PAJAK</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal21L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Pajak-pajak yang timbul berdasarkan Perjanjian ini menjadi tanggung jawab masing-masing 
                                        pihak sesuai peraturan perpajakan yang berlaku di Negara Republik Indonesia, kecuali 
                                        terdapat keadaan sebagaimana dimaksud dalam Pasal 10 Ayat (5) Perjanjian ini. 
                                        Para Pihak sepakat dan setuju PENJUAL hanya bertanggung jawab membayar dan menyetor 
                                        kewajiban pajaknya sesuai dengan Harga Pengikatan yang diterima. 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 6</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>PENYELESAIAN PEMBANGUNAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>DAN SERAH TERIMA TANAH DAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>BANGUNAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal21L02" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PENJUAL akan menyelesaikan pembangunan dan siap untuk 
                                        diserah terimakan kepada PEMBELI pada tanggal <asp:Label ID="lbltglserahterimaid" runat="server" />.  Dengan 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal21R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font VAlignT w2" RowSpan="8"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 5</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>TAXES</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal21R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Any taxes arising under this Agreement shall be the responsibility of each party 
                                        according to the tax regulations prevailing in the Republic of Indonesia, 
                                        unless the conditions as referred to in Article 10 Paragraph (5) of this Agreement 
                                        apply. The Parties agree and approve that SELLER shall only be responsible for paying 
                                        and performing tax obligations according to the Acceptable conditional Price. 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 6</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>COMPLETION OF THE DEVELOPMENT</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>AND HANDOVER OF LAND AND</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>BUILDING</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal21R02" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The SELLER shall complete the development and is ready to 
                                        hand over to the BUYER on <asp:Label ID="lblserahterimaen" runat="server" />. Under the provisions that at that time  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman22" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal22" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal22L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal22L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        ketentuan pada saat itu PARA PIHAK telah menandatangani Perjanjian ini dan 
                                        seluruh Harga Pengikatan berikut denda (jika ada) sebagaimana diatur 
                                        dalam Pasal 3 Ayat 3.1 dan Pasal 4 Ayat 4.1 Perjanjian ini telah dilunasi oleh 
                                        PEMBELI kepada PENJUAL  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila PENJUAL tidak dapat menyelesaikan pembangunan pada tanggal yang telah 
                                        ditetapkan, maka PENJUAL dikenakan denda sebesar 1%0 (satu permil) untuk setiap 
                                        hari keterlambatan dari nilai pekerjaan bangunan yang belum diselesaikan sesuai 
                                        dengan perhitungan yang dibuat oleh PENJUAL <b>(nilai sisa pekerjaan)</b>, dengan 
                                        ketentuan denda maksimum adalah sebesar 5% (lima persen) dari harga bangunan 
                                        sebagaimana dimaksud dalam Pasal 3   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal22R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal22R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        the PARTIES have executed this Agreement and any Conditional Prices and 
                                        penalties (if any) as stipulated in Article 3 Paragraph 3.1 and Article 4 Paragraph 
                                        4.1 of this Agreement have been fully paid by the BUYER to the SELLER  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the SELLER cannot complete the development on the date specified, the SELLER shall 
                                        be subject to a penalty of 1% 0 (one permil) for each day of delay from the value of 
                                        unfinished development work according to the calculation made by the SELLER <b>(the value 
                                        of the remaining work)</b>, provided that the maximum penalty of 5% (five percent) of the 
                                        building price as referred to in Article 3 paragraph (1) of this Agreement. The penalty 
                                        shall be paid by the SELLER to the BUYER   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman23" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal23" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal23L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal23L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        ayat (1) Perjanjian ini. Denda tersebut akan dibayar oleh PENJUAL kepada PEMBELI 
                                        menurut cara dan ketentuan yang akan diatur kemudian oleh PENJUAL setelah dilakukan 
                                        serah terima Tanah dan Bangunan dari PENJUAL kepada PEMBELI.  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Kewajiban PENJUAL untuk membayar denda sebagaimana dimaksud dalam ayat (2) pasal ini, 
                                        dikecualikan apabila keterlambatan penyelesaian pembangunan disebabkan oleh suatu 
                                        keadaan sebagaimana dimaksud dalam Pasal 14 Perjanjian ini dan/atau berkenaan dengan 
                                        pemasangan instalasi listrik PLN dan air bersih pada Tanah dan Bangunan.   
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Penyerahan Tanah dan Bangunan akan dilakukan oleh PENJUAL kepada   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal23R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal23R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        according to the method and conditions to be set forth at a later date by the 
                                        SELLER after the Land and Building are handed over from the SELLER to the BUYER.  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The obligation of the SELLER to pay the penalty as referred to in paragraph (2) 
                                        of this article is excluded, if the delay to complete the development is caused by 
                                        conditions as referred to in Article 14 of this Agreement and/or in connection to the 
                                        installation of electricity from PLN and clean water of the Land and Building.   
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The Land and Building shall be handed over by the SELLER to the BUYER after the 
                                        building is ready to be handed   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman24" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal24" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal24L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal24L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI setelah bangunan siap untuk diserah terimakan dan PEMBELI telah melunasi 
                                        seluruh Harga Pengikatan kepada PENJUAL. Waktu penyerahan Tanah dan Bangunan akan 
                                        diberitahukan secara tertulis oleh PENJUAL kepada PEMBELI dan dibuktikan dengan 
                                        penandatanganan BAST dalam bentuk yang dipersiapkan oleh PENJUAL.  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila PEMBELI tidak menandatangani BAST dalam waktu 14 (empat belas) hari kalender 
                                        sejak diberitahukan secara tertulis oleh PENJUAL, maka PEMBELI dianggap menyetujui 
                                        bahwa penyerahan Tanah dan Bangunan telah dilakukan dan dalam hal demikian bukti 
                                        pengiriman surat pemberitahuan dari PENJUAL kepada PihakPembeli mengenai penyerahan 
                                        Tanah dan Bangunan tersebut merupakan bukti   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal24R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal24R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        over and the BUYER has repaid all the Conditional Prices to the SELLER. 
                                        The time of Land and Building handover shall be notified in writing by the 
                                        SELLER to the BUYER and is evidenced by the BAST signing in the form prepared by the SELLER.  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the BUYER does not sign the BAST within 14 (fourteen) calendar days as of the 
                                        notification in writing by the SELLER, then the BUYER shall be deemed to agree that 
                                        the handover of the Land and Building has been carried out and in this case the 
                                        receipt of notification delivery from the SELLER to the Buyer regarding the 
                                        handover of the Land and the building shall constitute a sufficient evidence 
                                        to assume that the handover Land and  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman25" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal25" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal25L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal25L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        cukup untuk menganggap bahwa penyerahan Tanah dan Bangunan telah dilakukan pada 
                                        hari ke-14 (empat belas) setelah pemberitahuan tersebut disampaikan.  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">6.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Terhitung sejak serah terima Tanah dan Bangunan dari PENJUAL kepada PEMBELI, 
                                        sebagaimana dimaksud dalam ayat (4) atau ayat (5) pasal ini, maka:   
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">
                                        <asp:Table ID="Hal25L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Segala resiko atas Tanah dan Bangunan beralih kepada PEMBELI.
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Segala beban dan biaya-biaya yang timbul berkenaan dengan kepemilikan 
                                                    dan/atau penggunaan Tanah dan Bangunan, termasuk tetapi tidak terbatas pada 
                                                    Pajak Bumi dan Bangunan (PBB), pajak/iuran/pungutan lainnya menjadi tanggungan 
                                                    dan wajib  
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal25R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal25R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Building was carried out on the 14th day following the notification delivery.  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">6.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        As of the handover of Land and Building from SELLER to BUYER, 
                                        as referred to in paragraph (4) or paragraph (5) of this article, then:  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">
                                        <asp:Table ID="Hal25R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Any risks to Land and Building shall be passed to BUYER.
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Any expenses and costs arising in connection with ownership and/or use of 
                                                    Land and Building, including but not limited to Land and Building Taxes (PBB), 
                                                    taxes/fees/other contribution shall be borne and must be fully paid by the BUYER. 
                                                    The Land and Building Tax (PBB), the BUYER shall pay directly to the 
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman26" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal26" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal26L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal26L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">
                                        <asp:Table ID="Hal26L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    dibayar oleh PEMBELI sepenuhnya. Khusus untuk PBB wajib dibayar oleh 
                                                    PEMBELI langsung kepada instansi yang berwenang, setelah diperolehnya Surat 
                                                    Pemberitahuan Pajak Terhutang (SPPT) PBB.
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">c.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    PENJUAL tidak bertanggung jawab atas Tanah dan Bangunan dan 
                                                    tidak mempunyai kewajiban terhadap PEMBELI, kecuali :  
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font">
                                                    <asp:Table ID="Hal26L0203" runat="server" Width="100%">
                                                        <asp:TableRow>
                                                            <asp:TableCell CssClass="Font W2 VAlignT">-</asp:TableCell>
                                                            <asp:TableCell CssClass="Font TAlignJ">
                                                                untuk masa pemeliharaan yang dimaksud dalam Pasal 9 Perjanjian ini, dan
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableCell CssClass="Font W2 VAlignT">-</asp:TableCell>
                                                            <asp:TableCell CssClass="Font TAlignJ">
                                                                untuk penandatanganan Akta Jual Beli di hadapan PPAT 
                                                                sebagaimana dimaksud dalam Pasal 10 Perjanjian ini.
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">7.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Segala pengubahan dan/atau penambahan atas bangunan sebelum dilakukannya 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal26R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal26R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">
                                        <asp:Table ID="Hal26R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    competent authority, after receiving the Annual Tax Return (SPPT) 
                                                    of the said Land and Building Tax (PBB).
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">c.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    The SELLER shall not be responsible for Land and Building and does 
                                                    not have any obligations to BUYERS, except :  
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font">
                                                    <asp:Table ID="Hal26R03" runat="server" Width="100%">
                                                        <asp:TableRow>
                                                            <asp:TableCell CssClass="Font W2 VAlignT">-</asp:TableCell>
                                                            <asp:TableCell CssClass="Font TAlignJ">
                                                                for the maintenance period as referred to in Article 9 of this Agreement, and
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow>
                                                            <asp:TableCell CssClass="Font W2 VAlignT">-</asp:TableCell>
                                                            <asp:TableCell CssClass="Font TAlignJ">
                                                                for signing of the Sale and Purchase Deed before PPAT 
                                                                (Land Deed Official) as referred to in Article 10 of this Agreement.
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">7.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Any modifications and/or additions to the building prior to the handover,
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman27" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal27" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal27L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal27L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        serah terima, harus mendapat persetujuan tertulis terlebih dahulu dari PENJUAL 
                                        setelah PEMBELI memenuhi seluruh syarat dan kondisi yang ditetapkan oleh PENJUAL. 
                                        Setiap pengubahan dan/atau penambahan bangunan harus dikerjakan oleh tenaga ahli 
                                        PENJUAL dan biaya-biaya yang timbul serta resiko keterlambatan penyelesaian 
                                        pembangunan menjadi beban dan tanggung jawab PEMBELI.  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 7</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>JAMINAN PENJUAL</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            PENJUAL memberikan jaminan kepada PEMBELI bahwa Tanah dan Bangunan yang dijual 
                            sebagaimana dimaksud dalam Pasal 2 ayat (1) Perjanjian ini adalah hak PENJUAL 
                            sepenuhnya, dan karena itu PENJUAL dengan ini membebaskan PEMBELI dari segala 
                            tuntutan, gugatan atau tagihan dari pihak manapun sepanjang menyangkut hak 
                            PENJUAL atas Tanah dan Bangunan tersebut.
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal27R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font VAlignT w2" RowSpan="4"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal27R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        shall obtain prior written approval from the SELLER after the BUYER fulfills all the 
                                        terms and conditions set forth by the SELLER. Any modification and/or additions to the 
                                        building shall be carried out by the experts hired by the SELLER and the costs and the 
                                        risk of delays arising to complete the development shall be the liability and 
                                        responsibility of the BUYER.  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font w2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 7</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>THE SELLER GUARANTEE</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            The SELLER guarantees the BUYER that the Land and Building sold as referred to in Article 2 
                            paragraph (1) of this Agreement shall be the SELLER’s full rights and therefore the SELLER 
                            waives the BUYER from any claims, demands or compensation from any parties to the extent of the
                            SELLER's rights to the Land and Building concerned.
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman28" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal28" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal28L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 8</b></asp:TableCell>
                        <asp:TableCell CssClass="Font W2" RowSpan="4"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>KEWAJIBAN PEMBELI BERKENAAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>DENGAN PENGGUNAAN TANAH DAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>BANGUNAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal28L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI dengan ini menyatakan setuju dan tunduk bahwa peruntukan Tanah dan 
                                        Bangunan adalah khusus untuk <b>Rumah Tinggal</b> dan tidak untuk peruntukan lainnya.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI wajib untuk tunduk dan taat terhadap seluruh peraturan tata tertib 
                                        hunian (Township regulation) berikut perubahan-perubahannya yang akan dibuat 
                                        oleh PENJUAL dan/atau pengelola yang ditunjuk oleh PENJUAL untuk mengelola 
                                        kawasan lokasi Tanah
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal28R01" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2" RowSpan="4"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 8</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>THE BUYER OBLIGATIONS REGARDING</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>THE USE OF LAND AND BUILDING</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal28R02" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The BUYER hereby declares agreeing and complying with the intended purpose of the 
                                        Land and Building, it is specifically for <b>Residence</b> and not for other purposes.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The BUYER shall observe and comply with all Township regulation and amendment 
                                        thereof to be made by the SELLER and/or the manger appointed by the SELLER to 
                                        manage the area of the Land and Building in connection with security, hygiene and
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman29" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal29" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal29L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal29L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        dan Bangunan berkenaan dengan keamanan, kebersihan dan ketertiban antara 
                                        sesama penghuni serta untuk menjaga keharmonisan bangunan dan lingkungan disekitar 
                                        lokasi Tanah dan Bangunan. Tata tertib hunian (Township regulation) berikut 
                                        perubahan-perubahannya tersebut merupakan satu kesatuan yang tidak terpisahkan dari 
                                        Perjanjian ini.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI dengan ini menyatakan sepakat dan setuju bahwa PENJUAL dan/atau pengelola 
                                        yang ditunjuk oleh PENJUAL hanya akan melakukan pengelolaan lingkungan terhadap kawasan 
                                        lokasi Tanah dan Bangunan berada, dengan jangka waktu pengelolaan maksimal sampai 
                                        dengan 2 (dua) tahun setelah biaya pengelolaan lingkungan diberlakukan 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal29R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal29R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        dan Bangunan berkenaan dengan keamanan, kebersihan dan ketertiban antara 
                                        sesama penghuni serta untuk menjaga keharmonisan bangunan dan lingkungan disekitar 
                                        lokasi Tanah dan Bangunan. Tata tertib hunian (Township regulation) berikut 
                                        perubahan-perubahannya tersebut merupakan satu kesatuan yang tidak terpisahkan dari 
                                        Perjanjian ini.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI dengan ini menyatakan sepakat dan setuju bahwa PENJUAL dan/atau pengelola 
                                        yang ditunjuk oleh PENJUAL hanya akan melakukan pengelolaan lingkungan terhadap kawasan 
                                        lokasi Tanah dan Bangunan berada, dengan jangka waktu pengelolaan maksimal sampai 
                                        dengan 2 (dua) tahun setelah biaya pengelolaan lingkungan diberlakukan 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman30" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal30" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal30L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal30L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        pertama kali oleh PENJUAL dan/atau pengelola yang ditunjuk oleh PENJUAL, yang 
                                        dibuktikan dengan surat pemberitahuan dari PENJUAL dan/atau pengelola yang 
                                        ditunjuk oleh PENJUAL,  atau  dalam jangka waktu pengelolaan sesuai ketentuan 
                                        peraturan dan perundang-undangan yang berlaku. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">&nbsp</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila PENJUAL dan/atau pengelola yang ditunjuk oleh PENJUAL bermaksud mengakhiri 
                                        pengelolaan lingkungan tersebut dan menyerahkan pengelolaan kepada warga melalui 
                                        pengurus RT/RW atau pihak lain yang secara sah mewakili seluruh warga, maka PENJUAL 
                                        dan/atau pengelola yang ditunjuk oleh PENJUAL akan melakukan sosialisasi terlebih 
                                        dahulu selambat-lambatnya 3 (tiga) bulan sebelum jangka waktu pengakhiran pengelolaan  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal30R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal30R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        evidenced by the notification from the SELLER and/or the manager appointed by the SELLER, 
                                        or within the management period in accordance with the applicable rules and regulations. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">&nbsp</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the SELLER and/or manager appointed by the SELLER intends to cease the management 
                                        of the environment and delegate the management to surrounding community through 
                                        the administrator of Neighborhood Unit (RT)/Community Unit (RW)or other parties 
                                        who validly representing all community, then the SELLER and/or manager appointed 
                                        by the SELLER shall first disseminate the delegation not later than 3 (three)   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman31" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal31" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal31L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal31L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        yang dikehendaki PENJUAL dan/atau pengelola yang ditunjuk oleh PENJUAL. 
                                        Aturan-aturan lain terkait pengelolaan kawasan lokasi Tanah dan Bangunan pada 
                                        saat serah terima pengelolaan lingkungan dengan warga akan ditentukan kemudian.  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">&nbsp</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila ternyata PEMBELI dan atau mayoritas penghuni kawasan lokasi Tanah dan Bangunan 
                                        melalui RT/RW atau pihak lain yang secara sah mewakili seluruh warga menghendaki agar 
                                        pengelolaan lingkungan  tersebut tetap dilakukan oleh PENJUAL atau pengelola yang ditunjuk 
                                        oleh PENJUAL  maka Para Pihak sepakat bahwa pengelolaannya akan dilakukan dengan sistim 
                                        keuangan terbuka (open book system), sehingga tidak ada beban   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal31R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal31R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        months prior to the management cessation designated by the SELLER and/or the manager 
                                        appointed by the SELLER. Other regulations concerning to the management of the area 
                                        land and building located at the time of handover to the community shall be determined later. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font">&nbsp</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If it is obvious that the BUYER and/or the majority of community surrounding the 
                                        location of Land and Building area through Neighborhood Unit (RT)/Community Unit 
                                        (RW) or other parties validly representing the community require the environmental 
                                        management to be carried out by the SELLER or the manager appointed by the SELLER, 
                                        the Parties agree that the management shall be carried out under open book system, 
                                        thus there is    
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman32" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal32" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal32L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal32L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        subsidi dari PENJUAL atau pengelola yang ditunjuk oleh PENJUAL untuk pengelolaan 
                                        lingkungan. Ketentuan dalam ayat ini juga berlaku dan wajib ditaati oleh setiap 
                                        penerima pengalihan Tanah dan Bangunan dari PEMBELI.  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 9</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>MASA PEMELIHARAAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal32L02" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Selama 90 (sembilan puluh) hari kalender terhitung sejak serah terima Tanah dan 
                                        Bangunan dari PENJUAL kepada PEMBELI sebagaimana dimaksud dalam Pasal 6 ayat (4) 
                                        atau ayat (5) Perjanjian ini, PENJUAL wajib memperbaiki atas biaya PENJUAL, kerusakan 
                                        bangunan yang langsung   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal32R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2" RowSpan="4"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal32R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        not any expenses in the form of subsidies from the SELLER or managers appointed by 
                                        the SELLER for environmental management. The provisions in this paragraph shall 
                                        also apply and must be complied with by those receiving the transfer of Land and 
                                        Building from the BUYER.  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 9</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>MAINTENANCE PERIOD</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal32R02" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        During 90 (ninety) calendar days from the handover of Land and Building from the 
                                        SELLER to the BUYER as referred to in Article 6 paragraph (4) or paragraph (5) this 
                                        Agreement, the SELLER shall repair at the SELLER costs, the building damage directly 
                                        caused by the construction faults, such as damaged walls and ceramics   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman33" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal33" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal33L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal33L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        disebabkan karena kesalahan dalam pembangunannya, seperti dinding rusak dan keramik 
                                        rusak (selanjutnya disebut <b>“Masa Pemeliharaan”</b>). Kerusakan atas bangunan setelah 
                                        lewatnya Masa Pemeliharaan menjadi resiko PEMBELI sepenuhnya.  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Perbaikan kerusakan terhadap bangunan selama Masa Pemeliharaan baru dapat dilakukan 
                                        oleh PENJUAL setelah PEMBELI menandatangani dokumen BAST sebagaimana diatur dalam 
                                        Pasal 6 ayat (4) Perjanjian ini, atau 14 (empat belas) hari kalender setelah surat 
                                        pemberitahuan atas penyerahan Tanah dan Bangunan disampaikan oleh PENJUAL kepada 
                                        PEMBELI sebagaimana diatur dalam Pasal 6 ayat (5) Perjanjian ini. Perbaikan kerusakan 
                                        tersebut akan dilakukan   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal33R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal33R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        (hereinafter referred to as <b>"Maintenance Period"</b>). The damage of the building 
                                        after the expiration of the Maintenance Period shall be the full risks of the BUYER.  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The repair of damage to buildings during the new Maintenance Period shall be carried 
                                        out by the SELLER after the BUYER signs the BAST document as stipulated in Article 6 
                                        paragraph (4) of this Agreement, or within 14 (fourteen) calendar days after the 
                                        notification of the Land and Building handover is delivered by the SELLER to the 
                                        BUYERS as stipulated in Article 6 paragraph (5) of this Agreement. The repair of 
                                        the damage shall be carried out in accordance with the list of    
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman34" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal34" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal34L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal34L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        sesuai dengan daftar kerusakan bangunan yang akan dibuat dan disepakati bersama oleh Para Pihak. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Ketentuan mengenai Masa Pemeliharaan sebagaimana dimaksud ayat (1) pasal ini 
                                        menjadi berakhir apabila PEMBELI melakukan pengubahan atau renovasi atas bangunan. 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 10</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>PENANDATANGANAN AKTA JUAL BELI</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal34L02" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PENJUAL dan PEMBELI dengan ini setuju dan sepakat untuk membuat dan menandatangani 
                                        Akta Jual Beli dihadapan PPAT setelah terpenuhinya syarat-syarat dan 
                                        ketentuan-ketentuan sebagai berikut :   
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">
                                        <asp:Table ID="Hal34L03" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    PEMBELI telah memenuhi seluruh kewajibannya kepada PENJUAL   
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal34R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2" RowSpan="4"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal34R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        building damage to be made and agreed upon by the Parties. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The provisions on the Maintenance Period as referred to in paragraph (1) of 
                                        this article shall be terminated if the BUYER makes modifications or renovation of the building. 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 10</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>EXECUTION OF SALE AND PURCHASE DEED</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal34R02" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The SELLER and the BUYER hereby agrees and approves to make and sign a Sale and 
                                        Purchase Deed before the PPAT (Land Deed Official) after the following terms and 
                                        conditions are complied with :   
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">
                                        <asp:Table ID="Hal34R03" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    The BUYER has fulfilled all of the obligations to the SELLER under   
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman35" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal35" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal35L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal35L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">
                                        <asp:Table ID="Hal35L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    berdasarkan Perjanjian ini termasuk tetapi tidak terbatas 
                                                    pada denda keterlambatan pembayaran Harga Pengikatan (jika ada).   
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Sertipikat (pecahan) atas nama PENJUAL telah dikeluarkan oleh 
                                                    instansi yang berwenang dan telah diterima oleh PENJUAL.   
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">c.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    PEMBELI telah melunasi PBB tahun berjalan dan PBB tahun sebelumnya 
                                                    yang menjadi kewajiban Pembeli (jika ada) dan Bea Perolehan Hak Atas 
                                                    Tanah dan Bangunan (BPHTB) serta PNBP; dan   
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">d.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    PEMBELI telah melunasi seluruh biaya-biaya dan/atau pajak-pajak 
                                                    lain yang ada/timbul karena berlakunya peraturan pemerintah ;  
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal35R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal35R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font">
                                        <asp:Table ID="Hal35R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    this Agreement including but not limited to the penalty of the 
                                                    delay of the conditional price payment (if any).   
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    The certificate (separation) in the name of the SELLER has been issued 
                                                    by the competent authority and has been received by the SELLER.   
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">c.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    The BUYER has fully paid Current Year Land and Building Tax (PBB) and the 
                                                    previous Land and Building Tax (PBB) that is the obligation of the Buyer 
                                                    (if any) and the Fees for Acquisition of Rights to Lands and Buildings 
                                                    (BPHTB) and Non-Tax State Revenue (PNBP); and  
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">d.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    The BUYER has fully paid any costs and/or other taxes 
                                                    that exist/arise due to the enactment of government regulations;   
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman36" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal36" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal36L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal36L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PENJUAL akan mengirimkan surat pemberitahuan kepada PEMBELI ke alamat sebagaimana 
                                        yang tercantum dalam Perjanjian ini atau yang secara layak diinformasikan kemudian 
                                        oleh PEMBELI kepada PENJUAL atau ke alamat Objek Jual Beli untuk memberitahukan 
                                        PEMBELI tentang kesiapan pelaksanaan Akta Jual Beli atas Objek Jual Beli 
                                        (“Surat Pemberitahuan”) setidak-tidaknya setelah PEMBELI melunasi seluruh Harga 
                                        Pengikatan dan denda keterlambatan pembayaran Harga Pengikatan (jika ada), serta 
                                        Sertipikat pecahan Objek Jual Beli atas nama PENJUAL telah diterbitkan oleh 
                                        instansi yang berwenang dan telah diterima oleh PENJUAL.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal36R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal36R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The SELLER shall send a letter notifying the BUYER to the address specified in 
                                        this Agreement or reasonably informed at later date by the BUYER to the SELLER 
                                        or to the address of the Buying and Selling Object to inform the BUYER the readiness 
                                        to enter into a Sale and Purchase deed ("Notification Letter") at least after the 
                                        BUYER has fully paid any Conditional Prices and penalties of the delay of the late 
                                        payment of the Conditional Price (if any), and the Certificate of separation of Sale 
                                        and Purchase Object in the name of the SELLER has been issued by the competent authority 
                                        and has been received by the SELLER. 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman37" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal37" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal37L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal37L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI sepakat dan setuju untuk dikenakan biaya administrasi penyimpanan 
                                        dokumen (sertipikat pecahan dan/atau IMB Objek Jual Beli) sebesar Rp. 1.200.000,- 
                                        (satu juta dua ratus ribu rupiah) per tahun apabila dalam jangka waktu 180 
                                        (seratus delapan puluh) hari kalender sejak tanggal Surat Pemberitahuan yang 
                                        pertama kali dikirimkan oleh PENJUAL kepada PEMBELI, ternyata Akta Jual Beli 
                                        atas Objek Jual Beli tidak atau belum ditandatangani oleh PENJUAL dan PEMBELI. 
                                        Biaya Administrasi penyimpanan dokumen tersebut akan dihitung dan ditagihkan 
                                        oleh PENJUAL kepada PEMBELI dan harus dibayarkan langsung dan seketika oleh PEMBELI kepada 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal37R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal37R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The BUYER agrees and approves to be subject to document depository administration 
                                        fees (separation certificates and/or Building Permit (IMB) of the Sale and Purchase 
                                        Objects) of Rp. 1,200,000 (one million two hundred thousand rupiahs) per year if 
                                        within 180 (one hundred and eighty) calendar days as of the first date of the 
                                        notification delivery by the SELLER to the BUYER, it is obvious that the Deed 
                                        of Sale and Purchase of the object sold is not or has not been signed by the 
                                        SELLER and the BUYER. The administrative costs of the document depository shall 
                                        be calculated and invoiced by the SELLER to the BUYER and shall be paid directly 
                                        and immediately by the BUYER to the  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman38" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal38" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal38L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal38L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PENJUAL sebelum penandatanganan Akta Jual Beli dengan periode perhitungan per 1 
                                        (satu) tahunan (bukan proporsional per bulan atau per hari) terhitung sejak tanggal 
                                        berakhirnya jangka waktu 180 (seratus delepan puluh) hari kalender dari tanggal 
                                        Surat Pemberitahuan yang pertama kali dikirimkan oleh PENJUAL kepada PEMBELI. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI sepakat dan setuju apabila penandatanganan Akta Jual Beli atas Objek Jual 
                                        Beli tidak, belum atau tertunda setelah lewatnya jangka waktu 180 (seratus delapan 
                                        puluh) hari kalender sejak tanggal Surat Pemberitahuan yang pertama kali dikirimkan 
                                        oleh PENJUAL kepada PEMBELI, maka segala resiko karena tidak, belum atau tertundanya 
                                        penandatanganan Akta Jual Beli atas Objek Jual Beli tersebut menjadi beban, 
                                        tanggungan dan tanggung jawab PEMBELI sepenuhnya. 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal38R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal38R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        SELLER prior to the execution of the Sale and Purchase Deed with the calculation 
                                        period per 1 (one) year (not proportional per month or per day) from the expiration 
                                        of the period of 180 (one hundred and forty) calendar days from the date of the 
                                        Notification first sent by the SELLER to the BUYER.  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The BUYER agrees and approves if the Sale and Purchase Deed of the Sale and 
                                        Purchase Object is not signed, has not been signed or delays to be signed after 
                                        the expiration of 180 (one hundred and eighty) calendar days from the date the 
                                        Notification first sent by the SELLER to the BUYER, all risks due to the 
                                        abovementioned event on the Sale and Purchase Deed of the Sale and Purchase object shall
                                        be fully the expenses, liability and responsibility of the BUYER.   
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman39" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal39" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal39L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal39L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Sebelum dapat dilangsungkannya penandatanganan Akta Jual Beli atas Objek Jual Beli, 
                                        PENJUAL akan menginformasikan dan menyampaikan Surat Pemberitahuan tertulis kepada 
                                        PEMBELI mengenai waktu penandatangan Akta Jual Beli,  Pajak-Pajak yang harus 
                                        dibayarkan oleh PEMBELI kepada Negara, Biaya-Biaya yang wajib dibayarkan oleh 
                                        PEMBELI kepada PENJUAL dan dokumen-dokumen PEMBELI yang harus dilengkapi, termasuk 
                                        tetapi tidak terbatas pada bukti-bukti pembayaran pajak kepada instansi yang berwenang 
                                        sesuai dengan ketentuan/peraturan yang berlaku. 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal39R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal39R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Prior to the execution of the Sale and Purchase Deed of the Sale and Purchase 
                                        Objects, the SELLER shall inform and delivers Notification in writing to the 
                                        BUYER concerning the time to execute the Deed of Sale and Purchase, Taxes to be 
                                        paid by the BUYER to the State, Costs to be paid by the BUYER to the SELLER and 
                                        BUYER documents to be furnished, including but not limited to tax payment receipt 
                                        to the competent authority in accordance with applicable rules/regulations.  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman40" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal40" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal40L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal40L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">6.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI sepakat dan setuju untuk melangsungkan penandatanganan Akta Jual Beli atas 
                                        Objek Jual Beli dalam waktu 14 (empat belas) hari kalender sejak tanggal Surat 
                                        Pemberitahuan tertulis dari PENJUAL sebagaimana dimaksud dalam ayat (5) pasal ini 
                                        dan apabila dalam waktu 14 (empat belas) hari kalender sejak tanggal Surat Pemberitahuan 
                                        tertulis tersebut ternyata PEMBELI belum atau tidak bersedia menandatangani Akta Jual 
                                        Beli atas Objek Jual Beli dan/atau melengkapi seluruh dokumen-dokumen serta membayar 
                                        pajak-pajak dan biaya-biaya yang diisyaratkan sebagaimana dimaksud dalam ayat (5) 
                                        pasal ini, maka segala resiko karena tidak, belum atau tertundanya Akta Jual Beli 
                                        atas Objek Jual Beli tersebut menjadi beban, tanggungan dan tanggung jawab PEMBELI sepenuhnya.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal40R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal40R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">6.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The BUYER agrees and approves to execute the Sale and Purchase Deed of the Sale 
                                        and Purchase Object within 14 (fourteen) calendar days as of the date of the 
                                        Notification in writing from the SELLER as referred to in paragraph (5) of this 
                                        article and if within 14 (fourteen) calendar days as of the date of the Notification 
                                        in writing, it is obvious that the BUYER has not or is not willing to sign the Deed 
                                        of Sale and Purchase of the Object of Sale and/or furnish all documents and pay the 
                                        required taxes and fees as referred to in paragraph (5) of this article, then any 
                                        risks due to not implementing or the delay to implement the Sale and Purchase Deed 
                                        of the Sale and Purchase object Shall be the fully expenses, liabilities and 
                                        responsibilities of the BUYER.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman41" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal41" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal41L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal41L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">7.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI dengan ini menyatakan bahwa dirinya memenuhi syarat untuk memiliki Tanah dan 
                                        Bangunan berdasarkan hukum yang berlaku di Indonesia, dam apabila PEMBELI ternyata 
                                        tidak memenuhi syarat untuk memiliki Tanah dan Bangunann tersebut, maka segala resiko 
                                        dan akibat yang timbul menjadi beban, tanggungan dan tanggung jawab PEMBELI sendiri yang 
                                        dengan ini membebaskan PENJUAL dari beban, tangggungan dan tanggung jawab tersebut.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">8.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila penandatangan Akta Jual Beli tidak dapat dilaksanakan karena sebab-sebab 
                                        sebagaimana dimaksud dalam ayat (4) atau ayat (6) atau ayat (7) pasal ini, sehingga menimbulkan 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal41R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal41R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">7.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The BUYER hereby declares that it fulfills the requirements to possess Land and 
                                        Building according to the applicable law in Indonesia, and if the BUYER obviously 
                                        does not comply with the requirements to possess the Land and Building, then any risks 
                                        and consequences that may arise shall be fully the expenses, liabilities and 
                                        responsibilities of the BUYER who hereby waives the SELLER from the expenses, 
                                        liabilities and responsibilities.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">8.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the Sale and Purchase Act cannot be executed for the reasons as referred to in 
                                        paragraph (4) or paragraph (6) or paragraph (7) of this article, which result in 
                                        losses, deficit or any fees and/or additional taxes (including one borne by the 
                                        SELLER (if any), any 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman42" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal42" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal42L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal42L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        menjadi beban, tanggungan dan tanggung jawab PEMBELI sepenuhnya, dan harus dibayar 
                                        oleh PEMBELI kepada PENJUAL sebelum dilangsungkannya penandatanganan Akta Jual Beli.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">9.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Untuk PEMBELI yang berkewarganegaraan asing (WNA), selain berlaku seluruh ketentuan 
                                        dari ayat (1) sampai dengan ayat (8) pasal ini, penandatanganan Akta Jual Beli atas 
                                        Objek Jual Beli hanya dapat dilangsungkan apabila sertipikat/ tandabukti hak atas 
                                        tanahnya telah dilakukan penurunan haknya dari Hak Guna Bangunan (HGB) ke Hak Pakai 
                                        sesuai ketentuan yang berlaku, dengan ketentuan PEMBELI terlebih dahulu menandatangani 
                                        perjanjian jual beli secara notarial dengan membuat surat pernyataan, sehubungan 
                                        dengan penurunan hak tersebut.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal42R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal42R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        consequences that may arise shall be fully the expenses, liabilities and 
                                        responsibilities of the BUYER, and shall be paid by the BUYER to the SELLER 
                                        prior to the execution of the Sale and Purchase Deed.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">9.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        For the foreign (WNA) BUYERS, in addition to the application of all provisions of 
                                        paragraph (1) to paragraph (8) of this article, the Sale and Purchase Deed of the 
                                        sale and purchase object can only be executed if the status of the certificate/ 
                                        evidence of right of the land has been changed from Right to Build (HGB) into the 
                                        Right of Use according to the applicable provisions, provided that the BUYER has 
                                        executed a notarial sale and purchase agreement by making a statement, consisting 
                                        of the change of said right status.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman43" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal43" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal43L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal43L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">10.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Untuk PEMBELI berkewarganegaraan asing (WNA), apabila PEMBELI tidak bersedia 
                                        menurunkan hak atas tanah Objek Jual Beli sebagaimana dimaksud dalam ayat (9) pasal 
                                        ini sehingga mengakibatkan tidak dapat dilangsungkannya penandatanganan Akta Jual Beli, 
                                        maka segala resiko dan akibat yang timbul sehubungan dengan dengan hal tersebut menjadi 
                                        beban, tanggungan dan tanggung jawab PEMBELI sendiri yang dengan ini membebaskan PENJUAL 
                                        dari beban, tanggungan dan tanggung jawab tersebut dan apabila karena hal tersebut timbul 
                                        kerugian di PENJUAL, maka kerugian tersebut harus diganti seluruhnya oleh PEMBELI.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal43R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal43R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">10.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        For the Foreign (WNA) BUYERS, if the BUYERS are not willing to change the status of the 
                                        land rights of the Sale and Purchase object as referred to in paragraph (9) of this 
                                        article, causing the Sale and Purchase Deed cannot be executed, any risks and 
                                        consequences arising in connection with such matters shall be fully the expenses, 
                                        liabilities and responsibilities of the BUYER who hereby waives the SELLER from 
                                        the expenses, liabilities and responsibilities and if the SELLER suffers from loss, 
                                        the loss shall be fully indemnified by the BUYER.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman44" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal44" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal44L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 11</b></asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>PENGALIHAN HAK DAN KEWAJIBAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal44L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Selama Akta Jual Beli belum ditandatangani dan PEMBELI belum melunasi pembayaran 
                                        Harga Pengikatan termasuk dendanya (jika ada) kepada PENJUAL, maka PEMBELI tidak 
                                        berhak mengalihkan atau memindahkan seluruh atau sebagian hak dan kewajibannya 
                                        berdasarkan Perjanjian ini, tanpa persetujuan tertulis terlebih dahulu dari PENJUAL.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila pengalihan seluruh hak dan kewajiban PEMBELI kepada pihak ketiga telah 
                                        mendapat persetujuan dari PENJUAL, maka:
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal44L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Untuk setiap pengalihan hak dan kewajiban PEMBELI 
                                                    dikenakan biaya administrasi pengalihan sebesar    
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal44R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2" RowSpan="3"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 11</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>TRANSFER OF RIGHTS AND OBLIGATIONS</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal44R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        To the extent that the Sale and Purchase Deed has not been signed and the BUYER has 
                                        not fully paid the Conditional Price including a penalty (if any) to the SELLER, 
                                        then the BUYER does not reserve right to transfer or assign all or part of its rights 
                                        and obligations under this Agreement, without prior approval in writing from the SELLER.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the transfer of all rights and obligations of the BUYER to a third 
                                        party has been approved by the SELLER, then:
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal44R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    For each transfer of right and obligation, the BUYER shall be 
                                                    subject to a transfer administration 
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman45" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal45" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal45L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal45L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal45L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    1% (satu persen) dari Harga Pengikatan. Biaya administrasi pengalihan tersebut 
                                                    maupun biaya-biaya lain yang menjadi tanggungan PEMBELI harus dibayar oleh 
                                                    PEMBELI kepada PENJUAL sebelum dilakukannya pengalihan tersebut. Besarnya 
                                                    biaya administrasi pengalihan tersebut di atas dapat berubah dari waktu ke 
                                                    waktu sebagaimana ditetapkan oleh PENJUAL tanpa diperlukannya pemberitahuan 
                                                    terlebih dahulu  
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Segala biaya-biaya dan pajak yang timbul akibat dari pengalihan ini 
                                                    menjadi tanggungjawab pihak yang mengalihkan.  
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Dalam hal terjadi pengalihan hak dan kewajiban atas Tanah dan Bangunan dari
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal45R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal45R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal45R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    fee of 1% (one percent) of the Conditional Price. The administrative 
                                                    costs for the transfer and other costs to be borne by the BUYER shall be 
                                                    paid by the BUYER to the SELLER prior to the transfer. The amount of the 
                                                    transfer administrative fee abovementioned shall subject to change from 
                                                    time to time as determined by the SELLER without prior notification 
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    Any costs and taxes that arise as a result of this transfer shall 
                                                    be the responsibility of the transferor. 
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        In the event of the transfer of rights and obligations on Land and Building from the 
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman46" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal46" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal46L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal46L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI kepada pihak ketiga tersebut, maka pihak ketiga harus menandatangani 
                                        perjanjian yang mengatur pengalihan hak dan kewajiban antara PENJUAL, PEMBELI 
                                        dan pihak ketiga (untuk selanjutnya disebut <b>"Perjanjian Pengalihan"</b>), Perjanjian 
                                        Pengalihan tersebut akan disediakan oleh PENJUAL, dan berdasarkan perjanjian 
                                        tersebut pihak ketiga setuju untuk tunduk dan terikat dengan Perjanjian ini.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Pihak ketiga yang menerima pengalihan hak dan kewajiban dari PEMBELI wajib untuk 
                                        langsung menandatangani Akta Jual Beli dengan PENJUAL bersama-sama dengan 
                                        ditandatanganinya Perjanjian Pengalihan dan melengkapi seluruh persyaratan 
                                        dokumen yang telah ditentukan oleh PENJUAL, apabila seluruh syarat-syarat dan 
                                        ketentuan untuk menandatangani Akta Jual Beli atas Tanah dan Bangunan sebagaimana 
                                        dimaksud dalam Pasal 10 ayat (1) Perjanjian ini telah terpenuhi.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal46R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal46R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        BUYER to the third party, the third party shall sign an agreement governing the 
                                        transfer of rights and obligations between the SELLER, the BUYER and the third party 
                                        (hereinafter referred to as <b>"Transfer Agreement"</b>), the Transfer Agreement shall be 
                                        provided by the SELLER, and based on the agreement, the third party agrees to comply 
                                        with and bind to this Agreement.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The third party receiving the transfer of rights and obligations from the BUYER 
                                        shall immediately sign the Deed of Sale and Purchase with the SELLER, at the same 
                                        time the Transfer Agreement is executed and all the documents specified by the 
                                        SELLER shall be furnished, if all terms and conditions to execute the sale and purchase 
                                        Deed of Land and Building as referred to in Article 10 paragraph (1) of this agreement 
                                        have been fulfilled.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman47" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal47" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal47L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 12</b></asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>PEMBATALAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal47L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Perjanjian ini tidak dapat dibatalkan oleh salah satu dari Para Pihak, kecuali 
                                        karena alasan-alasan yang secara tegas disebutkan dalam Perjanjian ini
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila terjadi pembatalan Perjanjian ini sebagaimana dimaksud dalam Pasal 
                                        4 ayat (2) Perjanjian ini, maka seluruh jumlah uang yang telah dibayarkan oleh 
                                        PEMBELI kepada PENJUAL tidak dapat dikembalikan dan akan diperhitungkan sebagai 
                                        kompensasi ganti kerugian dari PEMBELI kepada PENJUAL.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal47R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2" RowSpan="3"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 12</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>CANCELLATION</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal47R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        This agreement cannot be canceled by one of the Parties, unless under 
                                        the reasons expressly stated in this Agreement
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If this Agreement is canceled as referred to in Article 4 paragraph (2) 
                                        of this Agreement, then all amount of money paid by the BUYER to the SELLER 
                                        cannot be refunded and shall be calculated as the compensation of the BUYER 
                                        to the SELLER.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman48" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal48" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal48L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal48L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Para Pihak sepakat apabila PEMBELI membatalkan Perjanjian ini dengan alasan apapun, 
                                        maka seluruh jumlah uang yang telah dibayarkan oleh PEMBELI kepada PENJUAL 
                                        tidak dapat dikembalikan dan akan diperhitungkan sebagai kompensasi ganti kerugian 
                                        kepada PENJUAL atas pembatalan Perjanjian ini, kecuali pembatalan dengan alasan 
                                        sebagaimana dimaksud dalam Pasal 3 ayat 3 b Perjanjian ini, yang dibuktikan dengan 
                                        surat pemberitahuan penolakan dari bank, maka PENJUAL akan mengembalikan seluruh 
                                        uang muka yang telah diterima tanpa bunga setelah dikurangi pajak-pajak yang telah 
                                        disetorkan ke instansi yang berwenang dan denda (jika ada) sedangkan uang tanda jadi 
                                        tidak dapat dikembalikan dan akan diperhitungkan  sebagai kompensasi 
                                        ganti kerugian kepada PENJUAL.  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal48R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal48R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The Parties agree that if the BUYER cancels this Agreement for any reason, then 
                                        all amount of money paid by the BUYER to the SELLER cannot be returned and shall 
                                        be calculated as the compensation for the SELLER due to the cancellation of this 
                                        Agreement, except the cancellation is made as referred to in Article 3 paragraph 
                                        3 b of this agreement, which is evidenced by a notification of refusal from the bank, 
                                        then the SELLER shall refund all advances received without interest less taxes that 
                                        have been paid to the competent authority and penalties (if any) while the booking 
                                        fee cannot be refunded and it shall be calculated as the compensation to the SELLER.  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman49" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal49" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal49L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal49L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Sehubungan dengan pembatalan terhadap Perjanjian ini, Para Pihak sepakat untuk 
                                        mengesampingkan ketentuan Pasal 1266 dan Pasal 1267 Kitab Undang-Undang Hukum Perdata.  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 13</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>AKIBAT PEMBATALAN PERJANJIAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>&nbsp;</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Apabila terjadi pembatalan Perjanjian sebagaimana dimaksud dalam Pasal 12 Perjanjian ini 
                            dan Tanah dan Bangunan telah diserahterimakan oleh PENJUAL kepada PEMBELI, maka Para Pihak 
                            sepakat untuk mengatur hal-hal sebagai berikut:
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal49R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2" RowSpan="5"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal49R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        In relation to the cancellation of this Agreement, the Parties agree to 
                                        waive the provisions of Article 1266 and Article 1267 of the Indonesia Civil Code.  
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 13</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>THE CONSEQUENCES OF THE</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>CANCELLATION OF THE AGREEMENT</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            If the agreement is canceled as referred to in the Article 12 of this Agreement and the Land 
                            and Building have been handed over by the SELLER to the BUYER, the Parties agree to arrange 
                            the following matters:
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman50" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal50" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal50L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal50L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI tetap berkewajiban untuk membayar semua biaya, tunggakan/ denda berupa 
                                        apapun juga yang merupakan kewajiban PEMBELI berdasarkan Perjanjian ini kepada 
                                        instansi terkait dan/atau pengelola (Estate Management) “PERUMAHAN SAVASA” sampai 
                                        PENJUAL menerima kembali Tanah dan Bangunan dari PEMBELI.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Terhitung sejak tanggal pembatalan Perjanjian ini PEMBELI wajib menyerahkan 
                                        Tanah dan Bangunan kepada PENJUAL dalam keadaan kosong, bersih dari barang-barang 
                                        milik PEMBELI atau pihak lain serta lengkap dengan kunci-kuncinya dan terpelihara 
                                        dengan baik seperti keadaan pada saat serah terima Tanah dan Bangunan.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal50R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal50R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The BUYER shall remain pay any costs, arrears/penalties in any forms that constitute 
                                        the BUYER’S obligation under this Agreement to the competent authority and/or 
                                        "PERUMAHAN SAVASA " Estate Management to the SELLER regains the Land and Building from BUYER. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        As of the date of cancellation of this Agreement, the BUYER shall hands over 
                                        Land and Building to the SELLER in a vacant condition, free of the BUYER’S or 
                                        other parties goods along with keys and properly maintained as same as the 
                                        conditions at the time of the Land and Building is handed over.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman51" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal51" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal51L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal51L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Bilamana PEMBELI tidak mengosongkan dan menyerahkan Tanah dan Bangunan kepada PENJUAL 
                                        menurut kondisi dan pada waktu yang ditetapkan ayat (2) pasal ini, maka atas keterlambatan 
                                        tersebut PEMBELI dikenakan denda sebesar Rp. 1.000.000,- (satu juta Rupiah) untuk setiap 
                                        hari keterlambatan, denda mana wajib dibayar sekaligus lunas kepada PENJUAL sejak diminta oleh PENJUAL.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Dengan tidak mengurangi kewajiban pembayaran denda tersebut, apabila PEMBELI tidak 
                                        menyerahkan kembali Tanah dan Bangunan sesuai dengan ketentuan ayat (2) pasal ini, 
                                        PEMBELI dengan ini memberi kuasa kepada PENJUAL dengan hak substitusi untuk:
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal51R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal51R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If the BUYER does not vacate and hands over Land and Building to the SELLER 
                                        according to the conditions and at the time stipulated in paragraph (2) of this 
                                        article, then the BUYER shall be subject to a fine of Rp.1,000,000 (one million Rupiah) 
                                        for each day delay, the fine shall be fully paid at once to the SELLER as 
                                        requested by the SELLER. 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Without prejudice to the obligation to pay the fine, if the BUYER does not return 
                                        the Land and Building in accordance with the provisions of paragraph (2) of this article, 
                                        the BUYER hereby authorizes the SELLER with the substitution right to:
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman52" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal52" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal52L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal52L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal52L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    melakukan pengosongan Tanah dan Bangunan dan mengeluarkan semua barang yang 
                                                    terdapat di Tanah dan Bangunan baik milik PEMBELI maupun pihak lain; dan  
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    menjalankan segala tindakan yang diperlukan agar dapat menerima kembali 
                                                    Tanah dan Bangunan dalam keadaan baik dan kosong.  
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Untuk melakukan pengosongan tersebut, apabila diperlukan, PENJUAL dapat meminta bantuan 
                                        pihak yang berwajib dan segala resiko serta biaya yang timbul atas pengosongan tersebut 
                                        menjadi tanggungan dan wajib dibayar sekaligus lunas oleh PEMBELI kepada PENJUAL 
                                        segera setelah diminta oleh PENJUAL.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal52R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal52R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal52R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">a.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    vacate Land and Building and remove any items in Land and Building either 
                                                    BUYER’S or other parties’; and  
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">b.</asp:TableCell>
                                                <asp:TableCell CssClass="Font TAlignJ">
                                                    carry out all necessary measures to regain the Land and Building in 
                                                    good a condition and vacant.   
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        To vacate the said, if necessary, the SELLER may ask for assistance from the competent 
                                        authority and any risks and costs incurred to vacate thereof shall be borne and must be 
                                        fully paid at once by the BUYER to the SELLER as soon as requested by the SELLER.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman53" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal53" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal53L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal53L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PEMBELI juga bertanggung jawab sepenuhnya atas kerusakan yang timbul pada bangunan 
                                        dan/atau bagian darinya, dan biaya perbaikan kerusakan tersebut harus dibayar oleh 
                                        PEMBELI kepada PENJUAL sejak diminta oleh PENJUAL.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 14</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>FORCE MAJEURE</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal53L02" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Force Majeure yaitu peristiwa yang terjadi di luar kekuasaan Para Pihak untuk 
                                        mencegahnya termasuk akan tetapi tidak terbatas pada huru hara, kebakaran, ledakan, 
                                        perang, bencana alam seperti gempa bumi atau peristiwa alam lainnya, maka Para Pihak 
                                        sepakat dan setuju untuk tidak saling menuntut satu sama lain.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal53R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2" RowSpan="4"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal53R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The BUYER shall also fully be responsible for any damages on the building and/or 
                                        parts thereof, and the cost to repair the damage shall be paid by the BUYER to the 
                                        SELLER as requested by the SELLER.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 14</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>FORCE MAJEURE</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal53R02" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Force Majeure is an event beyond the control of the Parties to prevent its occurrence 
                                        that includes but not limited to riots, fires, explosions, wars, natural disasters 
                                        such as earthquakes or other natural events, the Parties agree and approve not to sue each other.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman54" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal54" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal54L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal54L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Dalam hal terjadi suatu keadaan Force Majeure yang mengakibatkan pekerjaan PENJUAL 
                                        tertunda, maka kewajiban PENJUAL untuk menyelesaikan pembangunan berdasarkan Pasal 
                                        6 Perjanjian ini akan diperpanjang untuk jangka waktu selama berlangsungnya Force 
                                        Majeure tersebut. Keadaan tersebut tidak mengurangi kewajiban PEMBELI kepada PENJUAL 
                                        berdasarkan Perjanjian ini.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Semua kerugian dan biaya yang diderita salah satu pihak sebagai akibat terjadinya 
                                        Force Majeure tidak dapat dibebankan sebagai tanggung jawab pihak lainnya.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal54R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal54R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        In the event of a Force Majeure resulting in the delay of SELLER's work, then the 
                                        SELLER's obligation to complete the development according to the Article 6 of this 
                                        Agreement shall be extended for a period of the occurrence of the Force Majeure. 
                                        This situation shall not prejudice the BUYER’s to SELLER’s obligations under this Agreement.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Any losses and costs incurred by one party as a result of Force Majeure cannot 
                                        be assumed of the responsibility of the other party.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman55" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal55" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal55L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 15</b></asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>PEMBERITAHUAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal55L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Setiap pemberitahuan, surat-menyurat, tawaran, permintaan, persetujuan dan lain 
                                        sebagainya sehubungan dengan Perjanjian ini (selanjutnya disebut sebagai <b>“Pemberitahuan”</b>) 
                                        harus dilakukan secara tertulis dan dikirimkan secara langsung dengan fax atau dengan 
                                        pos tercatat ke alamat yang tercantum di bawah ini :
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal55L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">(i)</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" ColumnSpan="3"><b>PENJUAL PT PANAHOME DELTAMAS INDONESIA</b></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="10%">Alamat</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="1%">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">
                                                    Kota Deltamas (Marketing Gallery SAVASA) Greenland Square Blok BA No. 1A, 
                                                    Desa Sukamahi, Kecamatan Cikarang Pusat, Kabupaten Bekasi 17530
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Email</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">cs@savasa.id</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Telepon</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">021-89915888</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">U.p</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><b><i>Departemen Administrasi</i></b></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal55R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2" RowSpan="3"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 15</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>NOTIFICATION</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal55R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Any notification, correspondence, offers, inquiries, approvals etc. In relation to 
                                        this Agreement (hereinafter referred to as <b>"Notifications"</b>) shall be made in writing 
                                        and delivered in hand by fax or by registered mail to the address specified below :
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal55R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">(i)</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" ColumnSpan="3"><b>SELLER PT PANAHOME DELTAMAS INDONESIA</b></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="10%">Address</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="1%">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">
                                                    Deltamas City (Marketing Gallery SAVASA) Greenland Square Blok BA No. 1A, Sukamahi Village, 
                                                    Cikarang Pusat Sub-district, Regency of Bekasi 17530
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Email</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">cs@savasa.id</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Phone</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">021-89915888</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Attn.</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><b><i>Administrative Department</i></b></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman56" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal56" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal56L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal56L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal56L02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">(ii)</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" ColumnSpan="3"><b>PEMBELI</b></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="10%">Alamat</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="1%">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblalamat2id" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Email</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblemail2id" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Telepon</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lbltelepon2id" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">U.p</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblup2id" runat="server"></asp:Label></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Pemberitahuan dianggap telah diterima (i) apabila dikirimkan langsung, pada saat 
                                        diterima, atau (ii) apabila dikirimkan melalui pos tercatat, pada tanggal hari ke-3 
                                        setelah diposkan, atau (iii) apabila dikirim melalui email, pada saat ditransmisikan 
                                        dengan bukti scan yang dikirim.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal56R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:TableCell CssClass="Font W2"></asp:TableCell>
                            <asp:Table ID="Hal56R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        <asp:Table ID="Hal56R02" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT">(ii)</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" ColumnSpan="3"><b>BUYER</b></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="10%">Address</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT" Width="1%">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblalamat2en" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Email</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblemail2en" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Phone</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lbltelepon2en" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">Attn.</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT">:</asp:TableCell>
                                                <asp:TableCell CssClass="Font VAlignT"><asp:Label ID="lblup2en" runat="server" /></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        A notification shall be deemed to be received (i) if delivered in hand, at the time of 
                                        its receipt, or (ii) if sent by registered mail, the 3rd day after mailing, or (iii) if 
                                        sent by email, at the time of its transmission with the scanned receipt to be sent.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman57" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal57" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal57L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal57L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Setiap perpindahan alamat wajib diberitahukan secara tertulis kepada pihak lainnya 
                                        selambat-lambatnya 7 (tujuh) hari kalender sejak saat kepindahan tersebut. Segala 
                                        resiko yang timbul akibat perpindahan alamat yang tidak diberitahukan secara tertulis 
                                        menjadi tanggung jawab pihak yang pindah alamat tanpa pemberitahuan tertulis.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 16</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>PENYELESAIAN PERSELISIHAN DAN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>DOMISILI HUKUM</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Setiap perselisihan yang timbul akibat Perjanjian ini akan diselesaikan secara musyawarah dan 
                            apabila tidak tercapai mufakat, maka PENJUAL dan PEMBELI dengan ini sepakat untuk memilih domisili 
                            hukum yang tetap dan tidak berubah di Kepaniteraan Pengadilan Negeri Bekasi.
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal57R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2" RowSpan="5"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal57R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Any change of address shall be notified in writing to the other party not later than 7 
                                        (seven) calendar days from the time of the change. Any risks arising from the change of 
                                        addresses that are not notified in writing shall be the responsibility of the party 
                                        changing the address without written notification.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 16</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>DISPUTE SETTLEMENT AND LEGAL</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>DOMISILE</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            Any disputes arising from this Agreement shall be resolved by deliberation and if a consensus 
                            cannot be reached, then the SELLER and the BUYER hereby agree to choose a permanent and 
                            unchanging legal domicile in the Registrar's Office of the Bekasi District Court.
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman58" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal58" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal58L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>Pasal 17</b></asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>KETENTUAN – KETENTUAN LAIN</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal58L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Perjanjian ini menggantikan seluruh kesepakatan Para Pihak baik yang dibuat secara 
                                        tertulis maupun lisan yang telah ada sebelumnya dan memuat seluruh kesepakatan Para 
                                        Pihak tentang jual beli Tanah dan Bangunan.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Perjanjian ini tidak berakhir karena salah satu pihak meninggal dunia atau bubar 
                                        tetapi mengikat (para) ahli waris atau pengganti hak masing-masing pihak. Dalam hal 
                                        PEMBELI meninggal dunia maka dalam jangka waktu 60 (enam puluh) hari kalender sejak 
                                        meninggalnya PEMBELI, ahli waris atau pengganti hak PEMBELI yang sah menurut undang-undang 
                                        wajib memberikan bukti keterangan waris, yang menunjukkan sebagai ahli waris yang sah dari 
                                        PEMBELI.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal58R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2" RowSpan="3"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignC"><b>Article 17</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignC"><b>OTHER PROVISIONS</b></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal58R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">1.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        This agreement supersedes any existing agreements between the Parties made in writing 
                                        or verbally and constitutes any agreements of the Parties concerning the sale and 
                                        purchase of Land and Building.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">2.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        This agreement shall not end since one party passes away or dissolves but it binds 
                                        heirs or the successor of each party. In the event that the BUYER passes away then within 
                                        the period of 60 (sixty) calendar days from the death of the BUYER, the valid heirs or 
                                        successor under the law shall provide the proof of the inheritance, which indicates the 
                                        legal heir of the BUYER.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman59" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal59" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal59L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal59L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Segala beban dan biaya yang mungkin timbul berkenaan dengan pengalihan tersebut di 
                                        atas menjadi beban dan dipikul oleh ahli waris atau pengganti hak PEMBELI.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        PENJUAL hanya mengakui PEMBELI sebagai pihak dalam Perjanjian ini dan tidak mengakui 
                                        pihak lain yang mengaku sebagai yang turut berhak atas pembelian Tanah dan Bangunan 
                                        dalam Perjanjian ini meskipun hal tersebut diakui oleh PEMBELI.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Sepanjang tidak diatur lain di dalam Akta Jual Beli, maka segala sesuatu yang diatur 
                                        dalam Perjanjian ini tetap berlaku dan mengikat Para Pihak serta pengganti hak 
                                        masing-masing pihak.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal59R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal59R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Any expenses and costs that may arise in connection with the succession abovementioned 
                                        shall be borne and liability of the heirs or the BUYER’s successor.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">3.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        The SELLER shall only acknowledge the BUYER as a party to this Agreement and does not 
                                        recognize other parties claiming to reserve the right to purchase Land and Building in this 
                                        Agreement though the parties said are recognized by the BUYER.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">4.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Unless otherwise stipulated in this Sale and Purchase Deed, then any matters regulated 
                                        in this Agreement shall remain valid and bind the Parties and each of their successor.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman60" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal60" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal60L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal60L01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Semua lampiran pada Perjanjian ini dan segala perubahannya merupakan suatu kesatuan 
                                        yang tidak terpisahkan dengan Perjanjian ini.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">6.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Apabila di kemudian hari ada hal-hal yang tidak atau belum cukup diatur dalam 
                                        Perjanjian ini, maka akan diatur dalam suatu addendum/amendment yang merupakan 
                                        satu kesatuan tidak terpisahkan dari Perjanjian ini.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal60R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font">
                            <asp:Table ID="Hal60R01" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">5.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        Any appendices to this Agreement and any amendments thereof constitute an 
                                        integral part of this Agreement.
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="Font W2 VAlignT">6.</asp:TableCell>
                                    <asp:TableCell CssClass="Font W2 VAlignT"></asp:TableCell>
                                    <asp:TableCell CssClass="Font TAlignJ">
                                        If at a later time, there are any matters that are not or have not been adequately 
                                        regulated in this Agreement, they shall be regulated in an addendum/amendment that 
                                        constitutes an integral part of this Agreement.
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div id="Halaman61" runat="server" class="Halaman W100 PageBreak">
    <asp:Table ID="Hal61T1" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal61L" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font TAlignJ">
                            DEMIKIANLAH UNTUK TERIKAT SECARA HUKUM, Para Pihak telah menandatangani Perjanjian ini pada 
                            hari dan tanggal sebagaimana tercantum pada awal Perjanjian ini, dalam rangkap 2 (dua) 
                            masing-masing rangkap bermeterai cukup dan mempunyai kekuatan hukum yang sama.
                        </asp:TableCell>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell CssClass="W50 VAlignT">
                <asp:Table ID="Hal61R" runat="server" CssClass="W100">
                    <asp:TableRow>
                        <asp:TableCell CssClass="Font W2"></asp:TableCell>
                        <asp:TableCell CssClass="Font TAlignJ">
                            IN WITNESS WHEREOF TO BE LEGALLY BINDING, the Parties have executed this Agreement on the day 
                            and date first stated above of this Agreement, in 2 (two) copies of each of which 
                            duly stamped and having the same legal force.
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <asp:Table ID="Hal61T2" runat="server" CssClass="W100 CellSpacing">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">PENJUAL/The SELLER</asp:TableCell>
            <asp:TableCell>PEMBELI/The BUYER</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Height="80px"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="33%"><b><u>TAKAYA MOTOOKA</u></b></asp:TableCell>
            <asp:TableCell Width="33%"><b><u>HONGKY JEFFRY NANTUNG</u></b></asp:TableCell>
            <asp:TableCell Width="33%"><b><u><asp:Label ID="lblttdnama" runat="server" /></u></b></asp:TableCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Direktur</asp:TableCell>
            <asp:TableCell>Wakil Presiden Direktur</asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                (Kuasa Presiden Direktur No: 
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                63 /PHDI/SK-DIR/VII/2018)
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>--%>
