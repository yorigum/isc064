<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.KontrakEdit" CodeFile="KontrakEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Edit Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Kontrak - Edit Kontrak">
</head>
<body onkeyup="if(event.keyCode==27) document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="1"></uc1:NavKontrak>
    <div class="tabdata">
        <div class="pad">
            <uc1:HeadKontrak ID="HeadKontrak1" runat="server"></uc1:HeadKontrak>
            <table cellspacing="5">
                <tr valign="middle">
                    <td>
                        Print :
                    </td>
                    <td class="printhref">
                        <a id="printSP" runat="server"><b>Surat Pesanan</b></a>
                    </td>
                    <td class="printhref">
                        <a id="printPPJB" runat="server"><b>PPJB</b></a>
                    </td>
                    <td class="printhref">
                        <a id="printAJB" runat="server"><b>AJB</b></a>
                    </td>
                    <td class="printhref">
                        <a id="printBAST" runat="server"><b>BAST</b></a>
                    </td>
                    <td class="printhref">
                        <a id="printRKOM" runat="server"><b>Rencana Komisi</b></a>
                    </td>
                    <td class="printhref">
                        <a id="printFPS" runat="server"><b>Faktur Pajak</b></a>
                    </td>
                    <td class="printhref">
                        <a id="printFBatal" runat="server"><b>Formulir Pembatalan</b></a>
                    </td>
                    <td class="printhref">
                        <a id="printJadwalTagihan" runat="server"><b>Jadwal Pembayaran</b></a>
                    </td>
                    <td width="100%">
                    </td>
                    <td>
                        <input class="btn" id="btnlog" accesskey="l" type="button" value="  Log  " name="btnlog"
                            runat="server">
                    </td>
                    <td>
                        <input class="btn" id="btndel" accesskey="d" type="button" value="Delete" name="btndel"
                            runat="server">
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="stamp">
                        Input :
                        <asp:Label ID="tglInput" runat="server"></asp:Label>
                    </td>
                    <td class="stamp">
                        Edit :
                        <asp:Label ID="tglEdit" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0">
                <tbody>
                    <tr valign="top">
                        <td width="500">
                            <table cellspacing="5">
                                <tr>
                                    <td colspan="3">
                                        <p>
                                            <b>Dokumen</b></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a id="aKey" runat="server">No. Kontrak</a>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nokontrak" runat="server" ReadOnly="True" Width="150" CssClass="txt"></asp:TextBox><asp:Label
                                            ID="nokontrakc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display:none">
                                    <td>
                                        NUP
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="noqueue" runat="server" Width="150" CssClass="txt_center">0</asp:TextBox>
                                        <asp:Label ID="noqueuec" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tanggal
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <nobr><asp:textbox id="tglkontrak" runat="server" width="85" cssclass="txt_center"></asp:textbox>
													<input class="btn" onclick="openCalendar('tglkontrak')" type="button" value="...">
												</nobr>
                                        <asp:Label ID="tglkontrakc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Target BAST
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="targetst" runat="server" Width="85" CssClass="txt_center">1 Jan 2010</asp:TextBox>
                                        <input class="btn" onclick="openCalendar('targetst')" type="button" value="...">
                                        <asp:Label ID="targetstc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Sales
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="agent" runat="server" Width="300" CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="reff" runat="server" style="display:none">
                                    <td>
                                        Refferator
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="agentreff" runat="server" Width="300" CssClass="ddl">
                                            <asp:ListItem>Refferator:</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="agentreffc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td valign="top">
                                        PPN Ditanggung
                                    </td>
                                    <td valign="top">
                                        :
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="JenisPPN" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>PEMERINTAH</asp:ListItem>
                                            <asp:ListItem>KONSUMEN</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td valign="top">
                                        Jenis KPR
                                    </td>
                                    <td valign="top">
                                        :
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="jeniskpr" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">KPR</asp:ListItem>
                                            <asp:ListItem Value="1">NON-KPR</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Cara Bayar
                                    </td>
                                    <td valign="top">
                                        :
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="carabayar2" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>KPR</asp:ListItem>
                                            <asp:ListItem>INSTALLMENT</asp:ListItem>
                                            <asp:ListItem>HARD CASH</asp:ListItem>
                                            <asp:ListItem>SOFT CASH</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td valign="top">
                                        Sumber Dana
                                    </td>
                                    <td valign="top">
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSumberDana" runat="server" OnSelectedIndexChanged="ddlSumberDana_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0">Gaji</asp:ListItem>
                                            <asp:ListItem Value="1">Warisan</asp:ListItem>
                                            <asp:ListItem Value="2">Pinjaman</asp:ListItem>
                                            <asp:ListItem Value="3">Usaha</asp:ListItem>
                                            <asp:ListItem Value="4">Lainnya</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trLainnya" runat="server" visible="false">
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lainnya" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td valign="top">
                                        Tujuan Transaksi
                                    </td>
                                    <td valign="top">
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTujuan" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="0">Investasi</asp:ListItem>
                                            <asp:ListItem Value="1">Dihuni</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>
                                        Fitting Out
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="focounter" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox>&nbsp;x&nbsp;Angsuran
                                        <asp:TextBox ID="fo" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                                            ID="foc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="tra" runat="server" style="display: none">
                                    <td>
                                        Nilai Realisasi KPR
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nilaikpa" runat="server" Width="100" CssClass="txt_num">0</asp:TextBox><asp:Label
                                            ID="nilaikpac" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trb" runat="server" style="display: none">
                                    <td>
                                        Rekening Cair KPR
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="rekcair" runat="server" Width="250" CssClass="ddl">
                                            <asp:ListItem Value="1.102.100.01">1.102.100.01 - BCA 491 032 0080</asp:ListItem>
                                            <asp:ListItem Value="1.102.100.02">1.102.100.02 - BCA 491 032 3500</asp:ListItem>
                                            <asp:ListItem Value="1.102.100.03">1.102.100.03 - BTN BANDUNG</asp:ListItem>
                                            <asp:ListItem Value="1.102.100.04">1.102.100.04 - BANK MANDIRI</asp:ListItem>
                                            <asp:ListItem Value="1.102.100.05">1.102.100.05 - VICTORIA GIRO</asp:ListItem>
                                            <asp:ListItem Value="1.102.100.06">1.102.100.06 - VICTORIA TABUNGAN</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trc" runat="server">
                                    <td>
                                        Nilai Klaim
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nilaiklaim" runat="server" Width="100" CssClass="txt_num">0</asp:TextBox><asp:Label
                                            ID="nilaiklaimc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trd" runat="server">
                                    <td>
                                        Total Pelunasan saat Batal
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="totallunas" runat="server" Width="100" CssClass="txt_num">0</asp:TextBox><asp:Label
                                            ID="totallunasc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="tre" runat="server">
                                    <td>
                                        Nilai Kembali
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nilaipulang" runat="server" Width="100" CssClass="txt_num">0</asp:TextBox><asp:Label
                                            ID="nilaipulangc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trf" runat="server" style="display: none">
                                    <td>
                                        Rekening Pembatalan
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="acc" runat="server" Width="300" CssClass="ddl">
                                        </asp:DropDownList>
                                        <asp:Label ID="accerr" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        No. Faktur Pajak
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="kdPajak" runat="server">
                                            <asp:ListItem>010</asp:ListItem>
                                            <asp:ListItem>011</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="noFPS" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Virtual Account
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nova" runat="server" Width="140" CssClass="txt" />
                                        <input type="button" value="..." class="btn" id="btnpop" runat="server" name="btnpop" />
                                        <asp:CheckBox ID="delva" runat="server" Text="Delete VA" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>
                                        Status Kontrak
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="prosesbatal" runat="server" Text="Kontrak Sedang Dalam Proses Pembatalan">
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Keterangan
                                    </td>
                                    <td valign="top">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="note" runat="server" Width="350" Height="150" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table height="50">
                                <tr>
                                    <td>
                                        <asp:Button ID="ok" runat="server" Width="75" CssClass="btn" Text="OK" OnClick="ok_Click">
                                        </asp:Button>
                                    </td>
                                    <td>
                                        <input class="btn" id="cancel" style="width: 75px" onclick="window.close()" type="button"
                                            value="Cancel">
                                    </td>
                                    <td>
                                        <asp:Button ID="save" AccessKey="a" runat="server" Width="75" CssClass="btn" Text="Apply"
                                            OnClick="save_Click"></asp:Button>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <p class="feed">
                                            <asp:Label ID="feed" runat="server"></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                            <input class="btn" id="refresh" type="button" value="Refresh Unit" name="refresh"
                                runat="server" style="display: none;">
                            <div style="padding-right: 5px; padding-left: 5px; font-weight: bold; font-size: 15pt;
                                padding-bottom: 5px; line-height: normal; padding-top: 5px; font-style: normal;
                                font-variant: normal; display: none;">
                                <asp:Label ID="refreshinfo" runat="server" ForeColor="red"></asp:Label>&nbsp;</div>
                        </td>
                        <td>
                            <table cellspacing="5">
                                <tr>
                                    <td>
                                        <a id="aStatus" runat="server">Status</a>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:Label ID="status" runat="server" Font-Size="14" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PPJB
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:Label ID="ppjb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        AJB
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:Label ID="ajb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        BAST
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:Label ID="st" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="bataltr" runat="server">
                                    <td>
                                        Pembatalan
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:Label ID="batal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br>
                            <table cellspacing="5">
                                <tr>
                                    <td colspan="3">
                                        <p>
                                            <b>Perhitungan Harga</b></p>
                                        <asp:TextBox ID="skema" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Luas Semi Gross
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="luas" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        m2
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Luas Nett
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="luas2" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        m2
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Price List (Gross)
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right" width="120">
                                        <asp:Label ID="gross" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <%--<tr style="display:none;">
											<td>Biaya Gimmick</td>
											<td>:</td>
											<td align="right"><asp:label id="biayagimmick" runat="server" font-bold="True"></asp:label></td>
											<td style="PADDING-LEFT:10px;FONT-WEIGHT:normal;FONT-SIZE:8pt;LINE-HEIGHT:normal;FONT-STYLE:normal;FONT-VARIANT:normal">
												rupiah
											</td>
										</tr>
										<tr style="display:none;">
											<td>Biaya Lain Lain</td>
											<td>:</td>
											<td align="right"><asp:label id="biayalainlain" runat="server" font-bold="True"></asp:label></td>
											<td style="PADDING-LEFT:10px;FONT-WEIGHT:normal;FONT-SIZE:8pt;LINE-HEIGHT:normal;FONT-STYLE:normal;FONT-VARIANT:normal">
												rupiah
											</td>
										</tr>--%>
                                <tr style="display: none">
                                    <td align="right" colspan="3">
                                        <hr noshade size="1">
                                    </td>
                                    <td style="font-weight: bold">
                                        -
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="afterdisc" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>
                                        PPN
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="ppn" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td align="right" colspan="3">
                                        <hr noshade size="1">
                                    </td>
                                    <td style="font-weight: bold">
                                        +
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3">
                                        <hr noshade size="1">
                                    </td>
                                    <td>
                                        +
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Total Harga
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="hargaSD" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Bunga
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblBunga" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Diskon Skema
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="diskon" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Diskon Harga Jual
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="diskontambahan" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3">
                                        <hr noshade size="1">
                                    </td>
                                    <td>
                                        -
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Nilai Kontrak</strong>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="nilai" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3">
                                        <hr noshade size="1">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>
                                        DPP
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblDPP" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>
                                        PPN
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblPPN" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <b>Perincian Kontrak</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        DPP
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="DPP2" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PPN
                                    </td>
                                    <%--<asp:label id="statusPPN" runat="server" />--%>
                                    <td>
                                        :
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="PPN2" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal;
                                        font-style: normal; font-variant: normal">
                                        rupiah
                                    </td>
                                </tr>
                            </table>
                            <div>
                            </div>
                            <div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    </form>

    <script language="javascript" type="text/javascript">
        function call(nova) {
            document.getElementById('nova').value = nova;
        }
    </script>

</body>
</html>
