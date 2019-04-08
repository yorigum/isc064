<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.ReservasiJadwalTagihan" CodeFile="ReservasiJadwalTagihan.aspx.cs" %>

<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="NavReservasi" Src="NavReservasi.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeadReservasi" Src="HeadReservasi.ascx" %>
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
        <uc1:NavReservasi ID="NavReservasi1" runat="server" Aktif="2"></uc1:NavReservasi>
    </div>
    <div class="tabdata">
        <div class="pad">
            <uc1:HeadReservasi ID="HeadReservasi1" runat="server"></uc1:HeadReservasi>
            <table cellspacing="5">
                <tr>
                    <td>
                        <input type="button" visible="false" id="edit" runat="server" value="Edit Tagihan"
                            class="btn" style="width: 100px" name="edit" accesskey="e">
                    </td>
                    <td style="padding-left: 10">
                        <p class="feed">
                            <asp:Label ID="feed" runat="server"></asp:Label>
                        </p>
                    </td>
                </tr>
            </table>
            <table cellspacing="5">
                <tr>
                    <td colspan="3">
                        Skema :
                        <asp:Label ID="skema" runat="server" Font-Bold="True" Font-Size="14"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="150">
                        Nilai Reservasi
                    </td>
                    <td>
                        :
                    </td>
                    <td width="150" align="right">
                        <asp:Label ID="nilai" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tagihan
                    </td>
                    <td>
                        :
                    </td>
                    <td align="right">
                        <asp:Label ID="totaltagihan" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Biaya
                    </td>
                    <td>
                        :
                    </td>
                    <td align="right">
                        <asp:Label ID="totalbiaya" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tagihan + Biaya
                    </td>
                    <td>
                        :
                    </td>
                    <td align="right">
                        <asp:Label ID="tagihanbiaya" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr id="outtr" runat="server">
                    <td align="right" colspan="3">
                        <font style="font: 8pt">Selisih Kontrak dengan Tagihan (out of balance)</font>
                        <br>
                        <asp:Label ID="outofbalance" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <%--<tr>
							<td>Pembayaran</td>
							<td>:</td>
							<td align="right">
								<asp:label id="pembayaran" runat="server" font-bold="True"></asp:label></td>
							<td>
						</tr>
						<tr valign="top">
							<td>
								Pelunasan (<asp:label id="persenlunas" runat="server" font-bold="True"></asp:label>%)
								<br>
								<font style="font:8pt">Pembayaran yang sudah cair</font>
							</td>
							<td>:</td>
							<td align="right">
								<asp:label id="pelunasan" runat="server" font-bold="True"></asp:label>
							</td>
							<td>
						</tr>
						<tr id="unatr" runat="server">
							<td align="right" colspan="3">
								<font style="font:8pt">Pembayaran tanpa alokasi (unallocated)</font>
								<br>
								<asp:label id="unallocated" runat="server" font-bold="True" forecolor="Red"></asp:label>
							</td>
						</tr>--%>
            </table>
            <br>
            <p style="padding: 3px; font: 8pt">
                Tipe : BF = Booking Fee / DP = Down Payment / ANG = Angsuran / ADM = Biaya Administrasi
                <%--<br>
						Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer 
						Bank / BG = Cek Giro / DN = Diskon--%>
            </p>
            <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableHeaderCell Width="100">No.</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Tipe</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="200">Tagihan</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="75">Jatuh Tempo</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Right">Nilai</asp:TableHeaderCell>
                    <%--	<asp:tableheadercell horizontalalign="Right">Pelunasan</asp:tableheadercell>
							<asp:tableheadercell horizontalalign="Right">Sisa</asp:tableheadercell>--%>
                </asp:TableRow>
            </asp:Table>
        </div>
    </div>
    </form>
</body>
</html>
