<%@ Control Language="c#" Inherits="ISC064.FINANCEAR.PrintBKMTemplate" CodeFile="PrintBKMTemplate.ascx.cs" %>
<link href="/Media/Style.css" type="text/css" rel="stylesheet">
<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
<style type="text/css">
    .Fix-Font {
        font-size: 10pt;
        font-family:Verdana;
    }
    .Fix-Space {
        font-size: 12pt;
        font-family:Verdana;
    }
</style>
<div style="width: 100%; height: 14cm">
    <table style="width: 100%;">
        <tr>
            <td style="width: 30%"></td>
            <td style="width: 40%; text-align: center; font-size: 10pt">
                <b>
                    <asp:Label ID="pt" runat="server" /><br />
                </b>
                <asp:Label ID="alamatpt" runat="server" /><br />
                <asp:Label ID="notelp" runat="server" />
            </td>
            <td style="width: 30%; text-align: right;"></td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td style="border-bottom: 2px solid black; border-top: 2px solid black; text-align: center; font-size: 12pt; font-family: 'Verdana'">KWITANSI / RECEIPT &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Nomor :
                <asp:Label ID="nobkm" runat="server" />
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td rowspan="15" style="width:2%"></td>
            <td style="width: 19%; font-size: 10pt; height: 20px; font-family: 'Verdana'"><u>Terima dari</u>
            </td>
            <td style="width: 1%; font-size: 10pt; vertical-align: middle;" rowspan="2">:
            </td>
            <td style="width: 80%; font-size: 10pt; border-bottom: 2px solid black; vertical-align: middle; font-family: 'Verdana'" rowspan="2">
                <asp:Label ID="cs" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 19%; font-size: 10pt; font-family: 'Verdana'"><i>Received from</i>
            </td>
        </tr>
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
        <tr>
            <td style="font-size: 10pt; height: 20px; font-family: 'Verdana'"><u>Sejumlah Uang</u></td>
            <td style="vertical-align: top; font-size: 10pt; vertical-align: middle;" rowspan="2">:</td>
            <td style="width: 80%; font-size: 10pt; border-bottom: 2px solid black; vertical-align: middle; font-family: 'Verdana'" rowspan="2">
                <asp:Label ID="jumlah" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="font-size: 10pt; font-family: 'Verdana'"><i>A Sum of</i>
            </td>
        </tr>
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-size: 10pt; height: 20px; font-family: 'Verdana'"><u>Untuk Pembayaran</u></td>
            <td style="vertical-align: top; font-size: 10pt; vertical-align: middle;" rowspan="2">:</td>
            <td style="border-bottom: 2px solid black; font-size: 10pt; vertical-align: middle; font-family: 'Verdana'" rowspan="2">
                <asp:Label ID="pembayaran" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; font-size: 10pt; font-family: Verdana"><i>In Settlement of</i></td>
        </tr>
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
        <tr>
            <td style="font-size: 10pt; height: 20px; font-family: 'Verdana'"><u>Melalui</u></td>
            <td style="font-size: 10pt; vertical-align: middle;" rowspan="2">:
            </td>
            <td style="width: 80%; font-size: 10pt; vertical-align: middle;" rowspan="2">
                <table width="75%" height="20px" cellspacing="3">
                    <tr>
                        <td style="width: 4%; border: 2px solid black; text-align: center; font-family: 'Verdana'">
                            <asp:Label ID="gr" runat="server" /></td>
                        <td style="width: 6%; font-family: 'Verdana'">Cek</td>
                        <td style="width: 4%; border: 2px solid black; text-align: center; font-family: 'Verdana'">
                            <asp:Label ID="cc" runat="server" /></td>
                        <td style="width: 10%; font-family: 'Verdana'">Debit or Credit</td>
                        <td style="width: 4%; border: 2px solid black; text-align: center; font-family: 'Verdana'">
                            <asp:Label ID="tr" runat="server" /></td>
                        <td style="width: 10%; font-family: 'Verdana'">Bank Transfer </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-size: 10pt; font-family: 'Verdana'"><i>By</i></td>
        </tr>
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
        <tr>
            <td style="font-size: 10pt; height: 20px; font-family: 'Verdana'"><u>Jumlah</u></td>
            <td style="font-size: 10pt; vertical-align: middle;" rowspan="2">:
            </td>
            <td style="width: 80%; font-size: 10pt; vertical-align: middle; font-family: Verdana" rowspan="2">
                <table width="100%">
                    <tr>
                        <td style="width: 20%; border: solid 2px black; height: 30px; font-family: Verdana">Rp.
                            <asp:Label ID="jumlahbayar" runat="server" />
                        </td>
                        <td style="text-align: right; vertical-align: bottom"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-size: 8pt; font-family: 'Verdana'"><i>Amount</i></td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="Fix-Space" style="text-align:right; font-size:10pt; vertical-align:top">Batam,&nbsp;<asp:Label ID="tglcetak" runat="server"></asp:Label></td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 70%" valign="top"></td>
            <td style="width: 15%"></td>
            <td style="width: 15%; height: 80px; text-align: right; vertical-align: bottom; border-bottom: solid 1px black"><%--(<asp:Label ID="use" runat="server"></asp:Label>)--%>
            </td>
        </tr>
    </table>
    <table class="Fix-Font" style="width:100%">
        <tr>
            <td rowspan="2" style="width:2%"></td>
            <td><u>Pembayaran dengan Cek / Giro baru dianggap sah setelah dicairkan ke rekening kami.</u></td>
        </tr>
        <tr>
            <td><i>Payment by Cheque / Giro is subject to realization </i></td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="vertical-align: top; font-size: 2pt; font-family: Verdana"><i>&nbsp;</i></td>
        </tr>
    </table>
    <table class="Fix-Font" style="width:100%">
        <tr>
            <td style="width:2%"></td>
            <td style="border-top: 1px solid black; font-size: 10pt;">Lembar I : Customer &nbsp;&nbsp;&nbsp;&nbsp; Lembar II : Finance and Accounting &nbsp;&nbsp;&nbsp;&nbsp; Lembar III : Legal &nbsp;&nbsp;&nbsp;&nbsp; Lembar IV : File</td>
        </tr>
    </table>
</div>