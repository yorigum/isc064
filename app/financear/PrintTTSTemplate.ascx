<%@ Control Language="c#" Inherits="ISC064.FINANCEAR.PrintTTSTemplate" CodeFile="PrintTTSTemplate.ascx.cs" %>

<style>
    .fontheader {
        font-size: 18pt;
        font-family: 'Times New Roman', Times, serif;
    }

    .fontisi {
        font-size: 11pt;
        font-family: 'Times New Roman', Times, serif;
    }


    table, th, td {
        table-layout: auto;
        border: 1px solid;
        border-collapse: collapse;
        border-spacing: 0;
        width: 100%;
    }

    td{
        padding-left:10px;
    }


    .btb {
        border-top: hidden;
        border-bottom: hidden;
    }

    .bbtm {
        border-top: hidden;
    }

    .bbtm-hidden {
        border-bottom: hidden;
    }

    .hbr {
        border-right-style: hidden;
        border-bottom-style: hidden;
    }

    .hidden-bltb {
        border-left-style: hidden;
        border-bottom-style: hidden;
        border-top-style: hidden;
    }

    .htr {
        border-right-style: hidden;
        border-top-style: hidden;
    }

    .htl {
        border-top-style: hidden;
        border-left: hidden;
    }

    .hborder-tb {
        border-top-style: hidden;
        border-bottom: hidden;
    }

    .hbl {
        border-bottom: hidden;
        border-left: hidden;
    }

    @page {
   margin: 2cm;
   size: portrait;
   
}

    .br {
        border-right: hidden;
    }

    .brl {
        border-right: hidden;
        border-left: hidden;
    }





    .hb {
        border-style: hidden;
    }

    .ht {
        border-top-style: hidden;
    }

    .b {
        border-collapse: collapse;
    }

    .ba {
        border: 1px;
    }

    .auto-style3 {
        width: 30%;
        height: 40px;
        border-right-style: hidden;
        border-right-color: inherit;
        border-right-width: medium;
    }

    .auto-style4 {
        width: 1%;
        height: 40px;
        border-right-style: hidden;
        border-right-color: inherit;
        border-right-width: medium;
    }

    .auto-style5 {
        width: 50%;
        height: 40px;
    }
</style>

<div style="padding:30px; border: 1px; border-collapse: collapse;">


    <table style="width: 100%; border-collapse: collapse; border-spacing: 0;">
        <tr>
            <td rowspan="4" style="max-width: 100%; text-align: center; width: 15%;">
                <img src="/Media/Final-01.jpg" style="height: 50px; text-align: center" />
            </td>
            <td style="text-align: center; width: 70%" rowspan="4">
                <b>PT. SERPONG BANGUN CIPTA</b>
                <br />
                Jl. BSD RAYA UTAMA RUKO MENDRISIO III BLOK B NO.11, GADING SERPONG
                 <br />
                TELP (021) 2222-0080 FAX (021) 2222-0081</td>
            <td style="text-align: center; vertical-align: top">
                <b>No. Faktur</b>
            </td>
        </tr>
        <tr>
            <td rowspan="3" colspan="2" style="text-align: center; width: 30%;">
                <asp:Label ID="nobkm" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
    </table>
    <table style="border-collapse: collapse;" class="btb">
        <tr>
            <td colspan="3" style="text-align: center; vertical-align: central"><b>TANDA TERIMA SEMENTARA</b></td>
        </tr>
    </table>
    <table style="width: 100%; border: inherit">
        <tr>
            <td class="hbr" style="width: 30%; padding: 10px;">Telah Terima dari
            </td>
            <td class="hbr" style="width: 1%">&nbsp:</td>
            <td class="hbl" style="width: 50%" colspan="3">
                <asp:Label ID="namacs" runat="server"></asp:Label>
            </td>

        </tr>
        <tr>
            <td colspan="5" style=""></td>
        </tr>
        <tr>
            <td class="htr" style="width: 30%; padding: 10px;">Sejumlah Uang
            </td>
            <td class="htr" style="width: 1%">&nbsp:</td>
            <td class="htl" style="width: 50%" colspan="3">
                <asp:Label ID="nilainup" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="ht" colspan="5" style=""></td>
        </tr>
        <tr>
            <td class="auto-style3" style="padding: 10px">Untuk Pembayaran NUP Marchand<br />
            </td>
            <td class="auto-style4">&nbsp:</td>
            <td colspan="3" class="auto-style5">
                <b>Unit&nbsp;&nbsp; </b>
                <asp:Label runat="server" ID="jumlahUnit"></asp:Label>&nbsp&nbsp</td>
        </tr>
        <tr>
            <td colspan="2" style="border-right-width: 0px; width: 40%;">
                <br />
                <br />
            </td>
            <td colspan="3" class="bbtm-hidden;" style="border-left: none;border-bottom:none !important; width: 60%"></td>
        </tr>
        <tr>
            <td rowspan="2" colspan="2" style="width: 40%; border-top: 1px !important;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Rp.
                <asp:Label ID="nominal" runat="server"> </asp:Label>,-</td>

            <td style="border-top:none !important; text-align: right; width: 35%" class="hbr htr ">Tangerang, 
                        <asp:Label ID="tglttd" runat="server"></asp:Label>&nbsp; </td>
            <td style="border-top:none !important ;width: 10%">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>
        </tr>
        <tr>
            <td class="hidden-bltb" style="text-align: right; border-right: 1px;border-left: 1px;">Diberikan Oleh,</td>
            <td class="hidden-bltb">
                <br />
            </td>
            </tr>

        <tr>
            <td style="width: 20% !important;border-right:none !important;"  class="">
                <b><br />Data Rekening Pemilik NUP</b>
            </td>
            <td colspan="3" style="width: 20% !important;border-left:none !important;border-top:none !important;"  class="">
                            </td>
        </tr>
        <%--///--%>
         <tr >
                        <td style="width: 20% !important;border-top:1px !important">Nama bank&nbsp&nbsp&nbsp&nbsp:&nbsp<asp:Label ID="bankacc" runat="server"></asp:Label></td>
             <td colspan="3" class="hborder-tb"></td>
                    </tr>
                    <tr>
                        <td style="width: 20% !important">Cabang&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp<asp:Label ID="cabangBank" runat="server"></asp:Label></td>
                        <td colspan="3" class="hborder-tb"></td>
                    </tr>
                    <tr>
                        <td style="width: 20% !important">No. Account&nbsp&nbsp:&nbsp<asp:Label ID="noAcc" runat="server"></asp:Label></td>
                        <td colspan="3" class="hborder-tb"></td>
                    </tr>
                    <tr>
                        <td style="width: 20% !important">Nama Pemilik&nbsp:&nbsp<asp:Label ID="Customer" runat="server"></asp:Label></td>
                        <td colspan="3" class="hborder-tb" style=""></td>
                    </tr>

        <%--////--%>
        <tr>
            <td style="border-top-color:transparent !important" colspan="4">
                <table style="width: 95%; border: none">
                    <tr>
                        <td style="width:80%" class="hb"></td>
                        <td class="hb" style="text-align: right;">________________</td>

                    </tr>
                    <tr>
                        <td class="hb"></td>
                        <td class="hb" style="text-align: right;"><b>Admin PT. SBC</b></td>

                    </tr>
                    <tr>
                        <td style="border:none;">
                            <strong>NOTE</strong> : 
Tanda Terima Sementara harap di serahkan kembali pada saat pengambilan Kwitansi asli dan nomor urut NUP. 
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none"></td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>

    <br />



    &nbsp;<table style="width: 100%; visibility: hidden">
        <tr>
            <td class="fontisi" colspan="3">Telah Terima dari :</td>
            <td class="fontisi" style="width: 20%">&nbsp;</td>
            <td class="fontisi" style="width: 5%">Nomor</td>
            <td class="fontisi" style="width: 2%">:</td>
            <td class="fontisi">&nbsp;</td>
        </tr>
        <tr>
            <td class="fontisi" colspan="3">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="fontisi">Tanggal</td>
            <td class="fontisi">:</td>
            <td class="fontisi">
                <asp:Label ID="tglbkm" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi" style="width: 13%">Uang Sejumlah</td>
            <td class="fontisi" style="width: 2%">:</td>
            <td class="fontisi" style="width: 30%">
            Rp.

        </tr>
        <tr>
            <td class="fontisi" colspan="3">
                <asp:Label ID="alamat1" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi" colspan="3">
                <asp:Label ID="alamat2" runat="server"></asp:Label></td>
            <td>&nbsp;</td>
            <td class="fontisi">No. SP</td>
            <td class="fontisi">:</td>
            <td class="fontisi">
                <asp:Label ID="nosp" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi" colspan="3">
                <asp:Label ID="alamat3" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="fontisi" colspan="3">&nbsp;</td>
        </tr>

        <tr>
            <td class="fontisi">Terbilang</td>
            <td class="fontisi">:</td>
            <td class="fontisi">
                <asp:Label ID="terbilangnilainup" runat="server"></asp:Label></td>
        </tr>
    </table>


    <%--    <hr />--%>

    <div style="display: none">
        <table style="width: 100%">
            <tr>
                <td class="fontisi" colspan="6">Untuk Pembayaran : 
                <asp:Label ID="baya" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="fontisi" colspan="3"><b>Uang Tanda Jadi</b></td>
                <td class="fontisi" style="width: 30%">&nbsp;</td>
                <td class="fontisi" style="width: 7%">Rp.</td>
                <td class="fontisi" style="text-align: right;">
                    <asp:Label ID="dppnup" runat="server"></asp:Label></td>
            </tr>
            <tr id="hide1" runat="server">
                <td class="fontisi" style="width: 13%">Nomor Unit</td>
                <td class="fontisi" style="width: 2%">:</td>
                <td class="fontisi" style="width: 30%">
                    <asp:Label ID="nounit" runat="server"></asp:Label></td>
            </tr>
            <tr id="hide2" runat="server">
                <td class="fontisi">Jalan</td>
                <td class="fontisi">:</td>
                <td class="fontisi">
                    <asp:Label ID="jalan" runat="server"></asp:Label></td>
            </tr>
            <tr id="hide3" runat="server">
                <td class="fontisi">Cluster</td>
                <td class="fontisi">:</td>
                <td class="fontisi">
                    <asp:Label ID="cluster" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="fontisi" colspan="3"><b>PPN</b></td>
                <td class="fontisi">&nbsp;</td>
                <td class="fontisi">Rp.</td>
                <td class="fontisi" style="text-align: right;">
                    <asp:Label ID="ppnnup" runat="server"></asp:Label></td>
            </tr>
        </table>

        <%--    <hr />--%>

        <table style="width: 100%">
            <tr>
                <td class="fontisi" style="width: 13%">Via</td>
                <td class="fontisi" style="width: 2%">:</td>
                <td class="fontisi" style="width: 55%"></td>
                <td class="fontisi">Cikarang,
                </td>
            </tr>
            <tr>
                <td class="fontisi">Tanggal</td>
                <td class="fontisi">:</td>
                <td class="fontisi" colspan="2">
                    <asp:Label ID="tglbankacc" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="fontisi" colspan="4" style="height: 90px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="fontisi" colspan="3">&nbsp;</td>
                <td class="fontisi">.............................................</td>
            </tr>
        </table>

    </div>

</div>


