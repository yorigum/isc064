<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintSPTemplate2" CodeFile="PrintSPTemplate2.ascx.cs" %>
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

<div align="center">
    <table style="width: 90%;" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2">
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="font-size: 10pt; vertical-align: top; font-weight: bold; text-align: center" colspan="2">
                <br />
                SURAT PEMESANAN<br />
                No.
                <asp:Label ID="NoKontrak1" runat="server" />
            </td>
        </tr>
    </table>
    <table style="width: 90%;" cellpadding="3" cellspacing="0">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%" cellpadding="3" cellspacing="0">
                    <tr>
                        <td colspan="2" style="width: 20%; font-size: 10pt; text-align: left">Yang bertanda tangan dibawah ini :<br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; font-size: 10pt; text-align: left">Nama</td>
                        <td colspan="3" style="width: 80%; font-size: 10pt; border-bottom: 2px solid black; text-align: left">: 
                            <asp:Label ID="namacs" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="width: 20%; font-size: 10pt; text-align: left">No. KTP/Paspor</td>
                        <td colspan="3" style="width: 80%; font-size: 10pt; border-bottom: 2px solid black; text-align: left">: 
                            <asp:Label ID="noktp" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="font-size: 10pt; text-align: left">Alamat <i>(sesuai ktp)</i></td>
                        <td colspan="3" style="font-size: 10pt; border-bottom: 2px solid black; text-align: left">: 
                            <asp:Label ID="Almt" runat="server" />
                            ,
                            <asp:Label ID="Almt2" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-size: 10pt; text-align: left"></td>
                        <td colspan="3" style="font-size: 10pt; border-bottom: 2px solid black; text-align: left">: 
                            <asp:Label ID="Almt3" runat="server"></asp:Label>
                            , 
                            <asp:Label ID="Almt4" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="font-size: 10pt; text-align: left; width: 20%;">No. Telephone</td>
                        <td style="font-size: 10pt; border-bottom: 2px solid black; width: 30%; text-align: left">: 
                            <asp:Label ID="telp" runat="server" /></td>
                        <td style="font-size: 10pt; border-bottom: 2px solid black; width: 45%; text-align: left">No. Fax &nbsp;:
                            <asp:Label ID="fax" runat="server" /></td>
                        <td style="font-size: 10pt; border-bottom: 2px solid black;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-size: 10pt; text-align: left; width: 20%;">No. Handphone</td>
                        <td style="font-size: 10pt; border-bottom: 2px solid black; text-align: left" colspan="3">: 
                            <asp:Label ID="hp" runat="server" /></td>

                    </tr>
                    <tr>
                        <td style="font-size: 10pt; text-align: left; width: 20%;">No. Handphone (<i>kerabat dekat</i>)</td>
                        <td style="font-size: 10pt; border-bottom: 2px solid black; text-align: left">: 
                            <asp:Label ID="hpkerabat" runat="server" /></td>
                        <td style="font-size: 10pt; border-bottom: 2px solid black; width: 45%; text-align: left">&nbsp;</td>
                        <td style="font-size: 10pt; border-bottom: 2px solid black;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 20%; font-size: 10pt; text-align: left">Alamat Email
                        </td>
                        <td colspan="3" style="width: 80%; font-size: 10pt; border-bottom: 2px solid black; text-align: left">: 
                            <asp:Label ID="email" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="font-size: 10pt; text-align: left">Alamat <i>(Surat menyurat)</i></td>
                        <td colspan="3" style="font-size: 10pt; border-bottom: 2px solid black; text-align: left">: 
                            <asp:Label ID="AlmtSrt" runat="server" />
                            -
                            <asp:Label ID="AlmtSrt2" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-size: 10pt; color: #FFFFFF; text-align: left"></td>
                        <td colspan="3" style="font-size: 10pt; border-bottom: 2px solid black; text-align: left">: 
                            <asp:Label ID="AlmtSrt3" runat="server"></asp:Label>
                            ,
                            <asp:Label ID="AlmtSrt4" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; font-size: 10pt; text-align: left">No. NPWP
                        </td>
                        <td colspan="3" style="width: 80%; font-size: 10pt; border-bottom: 2px solid black; text-align: left">: 
                            <asp:Label ID="nonpwp" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="font-size: 10pt; text-align: left; width: 20%;">No. Telp Kantor</td>
                        <td style="font-size: 10pt; border-bottom: 2px solid black; text-align: left" colspan="3">: 
                            <asp:Label ID="NoKantor1" runat="server" /></td>
                    </tr>
                </table>
                <br />
                <table style="width: 100%" cellpadding="3" cellspacing="0">
                    <tr>
                        <td style="font-size: 10pt; text-align: left;">Untuk selanjutnya disebut <b>“Pemesan”</b> dengan ini menyatakan bahwa : 
                        </td>
                    </tr>
                </table>
                <br />
                <table style="text-align: center; width: 96%;">
                    <tr>
                        <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">1.</td>
                        <td style="font-size: 10pt; text-align: left; text-wrap: none">Pemesan benar telah memesan 1 (satu) unit Shophouse Paul Lane dengan perincian sbb :
                            <br />

                            <table width="100%">
                                <tr>
                                    <td style="font-size: 10pt; text-align: left; width: 14%;">No.Kavling</td>
                                    <td style="font-size: 10pt; text-align: left; width: 1%;">:</td>
                                    <td style="font-size: 10pt; text-align: left; width: 20%;">
                                        <asp:Label ID="tower" runat="server" />
                                    </td>
                                    <td style="font-size: 10pt; text-align: left; width: 14%;">Tipe</td>
                                    <td style="font-size: 10pt; text-align: left; width: 1%;">:</td>
                                    <td style="font-size: 10pt; text-align: left; width: 20%;">
                                        <asp:Label ID="tipeunit" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; text-align: left; width: 14%;">Blok</td>
                                    <td style="font-size: 10pt; text-align: left; width: 1%;">:</td>
                                    <td style="font-size: 10pt; text-align: left; width: 20%;">
                                        <asp:Label ID="lantai" runat="server" />
                                    </td>
                                    <td style="font-size: 10pt; text-align: left; width: 14%;">Luas Tanah</td>
                                    <td style="font-size: 10pt; text-align: left; width: 1%;">:</td>
                                    <td style="font-size: 10pt; text-align: left; width: 20%;">
                                        <asp:Label ID="luassg" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; text-align: left; width: 14%;">No.Unit</td>
                                    <td style="font-size: 10pt; text-align: left; width: 1%;">:</td>
                                    <td style="font-size: 10pt; text-align: left; width: 20%;">
                                        <asp:Label ID="nounit" runat="server" />
                                    </td>
                                    <td style="font-size: 10pt; text-align: left; width: 14%;">Luas Bangunan</td>
                                    <td style="font-size: 10pt; text-align: left; width: 1%;">:</td>
                                    <td style="font-size: 10pt; text-align: left; width: 20%;">
                                        <asp:Label ID="luasnett" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; text-align: left; width: 29%;">Harga Jual</td>
                                    <td style="font-size: 10pt; text-align: left; width: 1%;">:</td>
                                    <td style="font-size: 10pt; text-align: left; width: 70%;" colspan="4">Rp. 
                                        <asp:Label ID="hargajual1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; text-align: left;">PPN (0%)</td>
                                    <td style="font-size: 10pt; text-align: left;">:</td>
                                    <td style="font-size: 10pt; text-align: left;" colspan="4">Rp. 
                                        <asp:Label ID="ppn1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; text-align: left;">Harga Pengikatan</td>
                                    <td style="font-size: 10pt; text-align: left;">:</td>
                                    <td style="font-size: 10pt; text-align: left;" colspan="4">Rp. 
                                        <asp:Label ID="hargapengikat1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; text-align: left;" valign="top">Terbilang</td>
                                    <td style="font-size: 10pt; text-align: left;" valign="top">:</td>
                                    <td style="font-size: 10pt; text-align: left;" colspan="4">
                                        <asp:Label ID="terbilang" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt; text-align: left;"><span lang="IN" style="font-size: 10.0pt; line-height: 115%; font-family: &quot; times new roman&quot; ,serif; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; letter-spacing: -.05pt; mso-ansi-language: IN; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">No Virtual Account</span><span lang="EN-US" style="font-size: 10.0pt; line-height: 115%; font-family: &quot; times new roman&quot; ,serif; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; letter-spacing: -.05pt; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"> (BCA)</span></td>
                                    <td style="font-size: 10pt; text-align: left;">:</td>
                                    <td style="font-size: 10pt; text-align: left;">
                                        <asp:Label ID="vabca" runat="server" />
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">2.</td>
                        <td style="font-size: 10pt; text-align: left;">Pemesan akan membeli dengan cara pembayaran:<asp:Label ID="skema" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">3.</td>
                        <td style="font-size: 10pt; text-align: left;">Harga sudah termasuk :<br />
                            <table width="100%">
                                <tr>
                                    <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">-</td>
                                    <td style="font-size: 10pt; text-align: left;">Pajak Pertambahan Nilai ( 0%)</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">-</td>
                                    <td style="font-size: 10pt; text-align: left;">Izin Mendirikan Bangunan Induk (IMB Induk)</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">-</td>
                                    <td style="font-size: 10pt; text-align: left;">Jaringan Air, Telepon, & Listrik</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">-</td>
                                    <td style="font-size: 10pt; text-align: left;">Pajak Bumi dan Bangunan (PBB) sampai dengan penyerahan Unit Apartemen (jika ada)</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">4.</td>
                        <td style="font-size: 10pt; text-align: left;">Harga Pengikatan belum termasuk biaya-biaya :<br />
                            <table width="100%">
                                <tr>
                                    <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">-</td>
                                    <td style="font-size: 10pt; text-align: left;">PPJB di hadapan Notaris / PPAT</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">-</td>
                                    <td style="font-size: 10pt; text-align: left;">Akta Jual Beli (AJB) di hadapan Notaris / PPAT</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">-</td>
                                    <td style="font-size: 10pt; text-align: left;">Biaya Balik Nama SHM-SRS ke atas nama Pemesan</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">-</td>
                                    <td style="font-size: 10pt; text-align: left;">Bea Perolehan Hak atas Tanah dan Bangunan (BPHTB)</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">-</td>
                                    <td style="font-size: 10pt; text-align: left;">Iuran Pengelolaan dan/atau biaya terkait pengelolaan lingkungan</td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; width: 1%; font-size: 10pt; text-align: left;">-</td>
                                    <td style="font-size: 10pt; text-align: left;">Biaya yang mungkin timbul sehubungan dengan perubahan ketentuan / peraturan dari pemerintah (jika ada)</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td style="text-align: left; font-size: 10pt; padding-top: 1.5%;">Dengan ditandatanganinya Surat Pemesanan ini, Pemesan menyatakan setuju dan mengikatkan diri atas syarat dan ketentuan yang ada pada Surat Pemesanan ini.
            <br />
                <br />
                Batam,
                <asp:Label ID="tglsp" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <table style="text-align: center; width: 90%;">
        <tr>
            <td style="width: 23%; font-size: 10pt; text-align: center; vertical-align: top;">Pemesan</td>
            <td style="width: 2%;">&nbsp</td>
            <td style="width: 23%; font-size: 10pt; text-align: center; vertical-align: top;">Sales In Charge</td>
            <td style="width: 2%;">&nbsp</td>
            <td style="width: 23%; font-size: 10pt; text-align: center; vertical-align: top;">GM Marketing<br />
            </td>
            <td style="width: 2%;">&nbsp</td>
            <td style="width: 25%; font-size: 10pt; text-align: center; vertical-align: top;">Finance</td>
        </tr>
        <tr>
            <td style="width: 18%; height: 120px; font-size: 10pt; border-bottom-style: solid; border-bottom-width: 1px; text-align: center; vertical-align: bottom;">
                <asp:Label ID="cs2" runat="server" />
            </td>
            <td style="width: 2%; height: 120px">&nbsp</td>
            <td style="width: 18%; height: 120px; font-size: 10pt; border-bottom-style: solid; border-bottom-width: 1px; text-align: center; vertical-align: bottom;">
                <asp:Label ID="sales" runat="server" />
            </td>
            <td style="width: 2%; height: 120px">&nbsp</td>
            <td style="width: 18%; height: 120px; font-size: 10pt; border-bottom-style: solid; border-bottom-width: 1px; text-align: center; vertical-align: bottom;">
                <asp:Label ID="gm" runat="server" />
            </td>
            <td style="width: 2%; height: 120px">&nbsp</td>
            <td style="width: 30%; height: 120px; font-size: 10pt; border-bottom-style: solid; border-bottom-width: 1px; text-align: center; vertical-align: bottom;">
                <asp:Label ID="fin" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 18%; height: 30px; font-size: 10pt; text-align: center; vertical-align: bottom;">Putih 	: Finance
            </td>
            <td style="width: 2%; height: 30px">&nbsp</td>
            <td style="width: 18%; height: 30px; font-size: 10pt; text-align: center; vertical-align: bottom;">Merah : Pembeli
            </td>
            <td style="width: 2%; height: 30px">&nbsp</td>
            <td style="width: 18%; height: 30px; font-size: 10pt; text-align: center; vertical-align: bottom;">Kuning : Legal 
            </td>
            <td style="width: 2%; height: 30px">&nbsp</td>
            <td style="width: 30%; height: 30px; font-size: 10pt; text-align: center; vertical-align: bottom;">Hijau : Sales
            </td>
        </tr>
    </table>
</div>
