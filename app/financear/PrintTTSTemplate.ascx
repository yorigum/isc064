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
  border: 1px solid black;
  border-spacing:0;
}
table {
  border-spacing: 0;
}

</style>

<div style="width: 100%; padding:10px;" >
    <table style="width: 100%;border-collapse:collapse;" >
        <tr >
            <td style="max-width:100%;white-space:nowrap;">
                <img src="/Media/logo_marchand.jpg" style="display:block; height:150px" /></td>
            <td style="text-align: center">
                <b>PT. SERPONG BANGUN CIPTA</b>
                <br />
                Jl. BSD RAYA UTAMA RUKO MENDRISIO III BLOK B NO.11, GADING SERPONG
                 <br />
                TELP (021) 2222-0080 FAX (021) 2222-0081</td>
            <td style="text-align:center;vertical-align:top">
                <table style="width: 100%;vertical-align:super;height:150px" >
                    <tr style="height:20%">
                        <td><b>No. Faktur</b></td>
                    </tr>
                    <tr style="height:80%">
                        <td style="text-align:center;vertical-align:middle" rowspan="2">
                            <asp:Label ID="nobkm" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>


        <tr>
            <td colspan="3" style="text-align: center; vertical-align: central"><b>TANDA TERIMA SEMENTARA</b></td>

        </tr>
    </table>

    <br />
    <table style="width: 100%">
        <tr>
            <td style="width: 40%">Telah Terima dari<br />
            </td>
            <td style="width: 5%">&nbsp:</td>
            <td colspan="3">
                <asp:Label ID="namacs" runat="server"></asp:Label>
            </td>
            
        </tr>

        <tr>
            <td style="width: 40%">Sejumlah Uang</td>
            <td style="width: 5%">&nbsp:</td>
            <td colspan="3">Rp.
                <asp:Label ID="nilainup" runat="server"> ,-</asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td></td>
        </tr>

    </table>
    <table style="width: 100%">
        <tr>
            <td style="width: 40%">Untuk Pembayaran NUP Marchand<br />
            </td>
            <td style="width: 5%">&nbsp:</td>
            <td colspan="3"><asp:Label runat="server" ID="jumlahUnit"></asp:Label>&nbsp&nbsp<b>Unit</b></td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td></td>
        </tr>

    </table>
    <br />
    <table style="width: 100%">
        <tr>
            <td rowspan="2" class="fontisi">Rp.
                <asp:Label ID="Label1" runat="server"> ,-</asp:Label></td>
        
            <td style="text-align: right">Tangerang, 
                <asp:Label ID="tglttd" runat="server"></asp:Label>&nbsp; </td>

        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="5" style="text-align: right">Diberikan Oleh,</td>

        </tr>
    </table>
    <br />
    <b>Data Rekening Pemilik NUP</b>
    <table style="width: 60%;">

        <tr>
            <td style="width: 20%">Nama bank</td>
            <td>
                <asp:Label ID="bankacc" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">Cabang</td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 20%">No. Account</td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 20%">Nama Pemilik</td>
            <td>
                <asp:Label ID="Customer" runat="server"></asp:Label></td>
        </tr>

    </table>
    <table style="width: 100%">
        <tr>
            <td></td>
            <td style="text-align: right">________________</td>

        </tr>
        <tr>
            <td></td>
            <td style="text-align: right"><b>Admin PT. SBC</b></td>

        </tr>
    </table>

    <b>NOTE	:</b>
    <br />
    Tanda Terima Sementara harap di serahkan kembali	pada saat pengambilan	Kwitansi asli dan	nomor urut NUP.&nbsp;

    <table style="width: 100%; visibility: hidden">
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
