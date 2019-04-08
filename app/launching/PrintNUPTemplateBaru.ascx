<%@ Control Language="c#" Inherits="ISC064.LAUNCHING.PrintNUPTemplateBaru" CodeFile="PrintNUPTemplateBaru.ascx.cs" %>
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
            <div>
                <div style="width:70%; float:left; padding-left:80px">
                    <h1 style="text-align: center">
                        NUP
                    </h1>
                    <h2 style="text-align: center">
                        Townhome & Shophouse Grand Victorian Life Style</h2>
                    <br />
                </div>
                <div style="width:10%; float:right; font-size:20pt; font-weight:bold; padding-right:40px">
                    <asp:Label ID="nonup" runat="server"></asp:Label>
                </div>
                <div style="clear:both"></div>
            </div>
            
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

