<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakJadwalTagihan" CodeFile="KontrakJadwalTagihan.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Jadwal Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Jadwal Tagihan">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="3"></uc1:NavKontrak>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadKontrak ID="HeadKontrak1" runat="server"></uc1:HeadKontrak>
                <table cellspacing="5">
                    <tr>
                        <td>
                            <input type="button" id="edit" runat="server" value="Edit Tagihan" class="btn btn-blue" style="width: 100px"
                                name="edit" accesskey="e">
                        </td>
                        <td style="padding-left: 10px">
                            <p class="feed">
                                <asp:Label ID="feed" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
                <table cellspacing="5">
                    <tr>
                        <td colspan="3">Skema :
								<asp:Label ID="skema" runat="server" Font-Bold="True" Font-Size="14"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="150">Nilai Kontrak</td>
                        <td>:</td>
                        <td width="150" align="right">
                            <asp:Label ID="nilai" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Tagihan</td>
                        <td>:</td>
                        <td align="right">
                            <asp:Label ID="totaltagihan" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Biaya</td>
                        <td>:</td>
                        <td align="right">
                            <asp:Label ID="totalbiaya" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Tagihan + Biaya</td>
                        <td>:</td>
                        <td align="right">
                            <asp:Label ID="tagihanbiaya" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr id="outtr" runat="server">
                        <td align="right" colspan="3">
                            <font style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">Selisih Kontrak dengan Tagihan (out of balance)</font>
                            <br>
                            <asp:Label ID="outofbalance" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Pembayaran</td>
                        <td>:</td>
                        <td align="right">
                            <asp:Label ID="pembayaran" runat="server" Font-Bold="True"></asp:Label></td>
                        <td></td>
                        <tr valign="top">
                            <td>Pelunasan (<asp:Label ID="persenlunas" runat="server" Font-Bold="True"></asp:Label>%)
								<br>
                                <font style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">Pembayaran yang sudah cair</font>
                            </td>
                            <td>:</td>
                            <td align="right">
                                <asp:Label ID="pelunasan" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td></td>
                            <tr id="unatr" runat="server">
                                <td align="right" colspan="3">
                                    <font style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">Pembayaran tanpa alokasi (unallocated)</font>
                                    <br>
                                    <asp:Label ID="unallocated" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                </table>
                <br>
                <p style="padding-right: 3px; padding-left: 3px; font-weight: normal; font-size: 8pt; padding-bottom: 3px; line-height: normal; padding-top: 3px; font-style: normal; font-variant: normal">
                    Tipe : BF = Booking Fee / DP = Down Payment / ANG = Angsuran / ADM = Biaya 
						Administrasi
						<br>
                    Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer 
						Bank / BG = Cek Giro / DN = Diskon / KR = Kredit Kepemilikan Rumah
                </p>
                <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="1">
                    <asp:TableRow HorizontalAlign="Left">
                        <asp:TableHeaderCell Width="100">No.</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Tipe</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="200">Tagihan</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="75">Jatuh Tempo</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Right">Nilai</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Right">Pelunasan</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Right">Sisa</asp:TableHeaderCell>
                        <%--<asp:TableHeaderCell HorizontalAlign="Right">Lebih Bayar</asp:TableHeaderCell>--%>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </form>
</body>
</html>
