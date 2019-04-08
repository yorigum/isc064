<%@ Control Language="c#" Inherits="ISC064.NUP.PrintRefundTemplate" CodeFile="PrintRefundTemplate.ascx.cs" %>
<style>
    .tt {
    }

    #title {
        padding: 5px;
        color: white;
        font-weight: bold;
        width: 450px;
        height: 25px;
        text-align: center;
        font-size: x-large;
        position: relative;
        background: #000;
    }

        #title:after {
            content: "";
            position: absolute;
            left: 0;
            bottom:;
            width: 0;
            height: 0;
            border-left: 0px solid white;
            border-top: 20px solid transparent;
            border-bottom: 20px solid transparent;
        }

        #title:before {
            content: "";
            position: absolute;
            right: -20px;
            bottom: 0;
            width: 0;
            height: 0;
            border-left: 20px solid #000;
            border-top: 0px solid transparent;
            border-bottom: 40px solid transparent;
        }

    .auto-style1 {
        width: 113px;
        height: 144px;
    }

    .auto-style2 {
        width: 382px;
        height: 52px;
    }

    .auto-style3 {
        height: 28px;
    }
</style>

<div style="width: 95%; font-size: 10pt; height: 14cm">
    <div align="left" runat="server">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td width="60%">
                    <table style="text-align: left;">
                        <tr>
                            <td style="font-size: 14pt; text-align: justify"><b>Perumnas Mahata Tanjung Barat</b></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: justify">Project TOD Perumnas-KAI : Project SIte & Marketing Office</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: justify">Jl. Ir. H. juanda No. 88 Kemiri Muka - Beji - Depok</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: justify">Telpon : 021-77804144, Hotline : 082260039715</td>
                        </tr>
                    </table>
                </td>
                <td width="40%">
                    <img src="/Media/logoperum.jpg" style="height: 80px; width: 80%;" />
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td style="font-size: 10pt; vertical-align: top; font-weight: bold; text-align: left;">
                    <b>No :
                        <asp:Label ID="no" runat="server" /></b>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
        </table>
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="font-size: 10pt; vertical-align: top; font-weight: bold; text-align: center; background-color: gray;">
                    <b>FORM PERMINTAAN PENGEMBALIAN</b>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td style="font-size: 10pt; text-align: left">
                    <b>Identitas Pemesan</b>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 1%; font-size: 10pt; text-align: right">1. </td>
                            <td style="width: 34%; font-size: 10pt; text-align: left">Tanggal permohonan</td>
                            <td style="width: 75%; font-size: 10pt; text-align: left">: 
                            <asp:Label ID="tglp" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: right">2. </td>
                            <td style="width: 19%; font-size: 10pt; text-align: left">NUP</td>
                            <td style="width: 80%; font-size: 10pt; text-align: left">: 
                            <asp:Label ID="nup" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: right">3. </td>
                            <td style="width: 19%; font-size: 10pt; text-align: left">Nama Lengkap Pemesan</td>
                            <td style="width: 80%; font-size: 10pt; text-align: left">: 
                            <asp:Label ID="nama" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: right">4. </td>
                            <td style="width: 19%; font-size: 10pt; text-align: left">Tgl pembayaran Booking Fee</td>
                            <td style="width: 80%; font-size: 10pt; text-align: left">: 
                            <asp:Label ID="tglbf" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: right">5. </td>
                            <td style="width: 19%; font-size: 10pt; text-align: left">Jumlah Booking Fee</td>
                            <td style="width: 80%; font-size: 10pt; text-align: left">: 
                            <asp:Label ID="jumlah" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: right">6. </td>
                            <td style="width: 19%; font-size: 10pt; text-align: left">Rekening Pengembalian</td>
                            <td style="width: 80%; font-size: 10pt; text-align: left">
                                <%--<asp:Label ID="rek" runat="server" />--%></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: right">&nbsp;</td>
                            <td style="width: 19%; font-size: 10pt; text-align: left">Nomor Rekening</td>
                            <td style="width: 80%; font-size: 10pt; text-align: left">: 
                            <asp:Label ID="norek" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: right">&nbsp;</td>
                            <td style="width: 19%; font-size: 10pt; text-align: left">Nama Rekening</td>
                            <td style="width: 80%; font-size: 10pt; text-align: left">: 
                            <asp:Label ID="narek" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; text-align: right">&nbsp;</td>
                            <td style="width: 19%; font-size: 10pt; text-align: left">Bank</td>
                            <td style="width: 80%; font-size: 10pt; text-align: left">: 
                            <asp:Label ID="bank" runat="server" /></td>
                        </tr>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="font-size: 10pt; text-align: left">
                    <b>PERNYATAAN</b>
                </td>
            </tr>
            <tr>
                <td style="font-size: 10pt; text-align: left">Pemesan dengan ini menyatakan bahwa :																																										
                </td>
            </tr>
        </table>
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="font-size: 10pt; width: 3%; vertical-align: top">1. </td>
                <td style="font-size: 10pt; width: 97%; vertical-align: top; text-align: justify">Nomor Urut Pemesanan (NUP) unit atas nama Pemesan diatas menjadi batal dan tidak berlaku.	
                </td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 3%; vertical-align: top">2. </td>
                <td style="font-size: 10pt; width: 97%; vertical-align: top; text-align: justify">Dengan diterimanya pengembalian booking fee <i>(refund)</i> tersebut, Pemesan membebaskan Perum Perumnas dari segala tuntutan dan gugatan dari pihak manapun.			
                </td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 3%; vertical-align: top">3. </td>
                <td style="font-size: 10pt; width: 97%; vertical-align: top; text-align: justify">Pengembalian  NUP diproses dalam waktu 14 hari kerja sejak permohonan ini diterima oleh Bagian Keuangan Project TOD Perumnas-KAI.			
                </td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 3%; vertical-align: top">4. </td>
                <td style="font-size: 10pt; width: 97%; vertical-align: top; text-align: justify">Pengembalian NUP  akan dilakukan dengan cara transfer ke Rekening Pengembalian a/n Konsumen	
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="font-size: 10pt; width: 20%;">&nbsp;</td>
                <td style="font-size: 10pt; width: 20%;">&nbsp;</td>
                <td style="font-size: 10pt; width: 20%; text-align: left;">Jakarta,
                    <asp:Label ID="tglnow" runat="server"></asp:Label></td>
                <td style="font-size: 10pt; width: 20%;">&nbsp;</td>
                <td style="font-size: 10pt; width: 20%;"></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; text-align: center"><b>Pemesan</b></td>
                <td style="font-size: 10pt;">&nbsp;</td>
                <td style="font-size: 10pt;">&nbsp;</td>
                <td style="font-size: 10pt; text-align: center;"><b>Penerima Pesanan</b></td>
                <td style="font-size: 10pt">&nbsp;</td>
            </tr>
            <tr>
                <td style="font-size: 10pt;">&nbsp;</td>
                <td style="font-size: 10pt;">&nbsp;</td>
                <td style="font-size: 10pt; text-align: left">Petugas</td>
                <td style="font-size: 10pt">&nbsp;</td>
                <td style="font-size: 10pt; text-align: center;">Marketing Manager</td>
            </tr>
            <tr style="height: 100px;"></tr>
            <tr>
                <td style="font-size: 10pt; text-align: center; border-top-style: solid; border-top-width: 2px">
                    <asp:Label ID="Label1" runat="server" /></td>
                <td style="font-size: 10pt;">&nbsp;</td>
                <td style="font-size: 10pt; text-align: center; border-top-style: solid; border-top-width: 2px">
                    <asp:Label ID="Label2" runat="server" /></td>
                <td style="font-size: 10pt">&nbsp;</td>
                <td style="font-size: 10pt; text-align: center; border-top-style: solid; border-top-width: 2px">Fakhruddin Tsany</td>
            </tr>
        </table>
    </div>
</div>