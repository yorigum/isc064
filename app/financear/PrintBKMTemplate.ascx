<%@ Control Language="c#" Inherits="ISC064.FINANCEAR.PrintBKMTemplate" CodeFile="PrintBKMTemplate.ascx.cs" %>
<link href="/Media/Style.css" type="text/css" rel="stylesheet">
<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
<style type="text/css">
    .Fix-Font {
        font-size: 10pt;
        font-family: Verdana;
    }

    .Fix-Space {
        font-size: 12pt;
        font-family: Verdana;
    }

    .tbTagihan {
        border: solid 2px black;
    }
</style>
<div style="width: 100%; height: 14cm">
    <table style="width: 100%;">
        <tr>
            <td style="width: 20%; text-align: center">
                <img alt="" style="height: 100px;" src="/Media/SBC.png" /></td>
            <td style="width: 55%; text-align: center; font-size: 10pt" rowspan="3">
                <b>PT.SERPONG BANGUN CIPTA<%--<asp:Label ID="pt" runat="server" />--%><br />
                    <br />
                </b>
                <%--<asp:Label ID="alamatpt" runat="server" />--%>Jl. BSD RAYA UTAMA RUKO MENDRISIO III BLOK B NO.11, GADING SERPONG<br />
                <br />
                TELP (021) 2222-0080 FAX (021) 2222-0081
KWITANSI<%--<asp:Label ID="notelp" runat="server" />--%>
            </td>
            <td style="width: 8%; text-align: left;">NO<br />
                <br />
                NO UNIT<br />
                <br />
                TANGGAL
            </td>
            <td style="width: 2%; text-align: left;">:<br />
                <br />
                :<br />
                <br />
                :
            </td>
            <td style="width: 15%; text-align: left;">
                <asp:Label ID="nobkm" runat="server" />
                <br />
                <br />
                <asp:Label ID="noUnit" runat="server" />
                <br />
                <br />
                <asp:Label ID="tglcetak" runat="server"></asp:Label>

            </td>
        </tr>
    </table>

    <table width="100%" border="0">
        <tr>
            <td style="border: 2px solid black; text-align: center; font-size: 12pt; font-family: 'Verdana'"><strong>KWITANSI</strong>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td rowspan="15" style="width: 2%"></td>
            <td rowspan="2" style="width: 23%; font-size: 10pt; height: 20px; font-family: 'Verdana'"><em>Telah Terima dari
            </em>
            </td>
            <td style="width: 1%; font-size: 10pt; vertical-align: middle;" rowspan="2">:
            </td>
            <td style="width: 80%; font-size: 10pt; border-bottom: 1px solid black;border-bottom-style:dotted; vertical-align: middle; font-family: 'Verdana'" rowspan="2">
                <asp:Label ID="cs" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
        <%--<tr>
            <td style="width: 19%; font-size: 10pt; font-family: 'Verdana'"><i>Received from</i>
            </td>
        </tr>--%>
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
        <tr>
            <td rowspan="2" style="font-size: 10pt; height: 20px; font-family: 'Verdana'"><em>Sejumlah Uang</em></td>
            <td style="vertical-align: top; font-size: 10pt; vertical-align: middle;" rowspan="2">:</td>
            <td style="width: 80%; font-size: 10pt; border-bottom: 1px solid black;border-bottom-style:dotted;vertical-align: middle; font-family: 'Verdana'" rowspan="2">Rp
                <asp:Label ID="jumlah" runat="server"></asp:Label>
            </td>
        </tr>
        <%--<tr>
            <td style="font-size: 10pt; font-family: 'Verdana'"><i>A Sum of</i>
            </td>
        </tr>--%>
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
        <%--<tr>
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
        </tr>--%>
    </table>
    <%--<table width="100%">
        <tr>
            <td class="Fix-Space" style="text-align:right; font-size:10pt; vertical-align:top">Batam,&nbsp;</td>
        </tr>
    </table>--%>
    <table width="100%">
        <tr>
            <td class="Fix-Space">&nbsp;</td>
        </tr>
    </table>
    <div style="margin: 0 10px 0 10px">
        <asp:Table ID="tbTagihan" runat="server" Width="100%" HorizontalAlign="Center">
        </asp:Table>
       Terbilang: <em>&nbsp;&nbsp; (&nbsp; <asp:Label ID="terbilang" runat="server"></asp:Label>  &nbsp;  )
       
        </em>
       
    </div>
    <table width="100%">
        <tr>
            <td></td>
        </tr>
        <tr>
            <td style="width: 50%" valign="top"><strong>
                <br />
                <br />
                <br />
                <br />
                NOTE </strong>: 
                <br />
                *Booking Fee Tidak dapat di kembalikan.</td>
            <td style="width: 20%;text-align:center">Diterima oleh,<br />
                <br />
                <br />
                <br />
                _____________________<br />
                <br />
            </td>
            <td></td>
            <td style="width: 20%; height: 80px; text-align: center; vertical-align: bottom; "><%--(<asp:Label ID="use" runat="server"></asp:Label>)--%>
                Diberikan oleh:<br />
                <br />
                <br />
                <br />
                _____________________<br />
                Finance Manager</td>
        </tr>
    </table>
 <%--   <table class="Fix-Font" style="width: 100%">
        <tr>
            <td rowspan="2" style="width: 2%"></td>
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
    <table class="Fix-Font" style="width: 100%">
        <tr>
            <td style="width: 2%"></td>
            <td style="border-top: 1px solid black; font-size: 10pt;">Lembar I : Customer &nbsp;&nbsp;&nbsp;&nbsp; Lembar II : Finance and Accounting &nbsp;&nbsp;&nbsp;&nbsp; Lembar III : Legal &nbsp;&nbsp;&nbsp;&nbsp; Lembar IV : File</td>
        </tr>
    </table>--%>
</div>
