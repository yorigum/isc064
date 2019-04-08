<%@ Page Language="c#" Inherits="ISC064.LEGAL.KontrakEdit" CodeFile="KontrakEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
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
        <div class="content-header">
            <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="1"></uc1:NavKontrak>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadKontrak ID="HeadKontrak1" runat="server"></uc1:HeadKontrak>
                <table cellspacing="5">
                    <tr valign="middle">
                        <td style="width: 8%">Print :
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
                        <%--   <td class="printhref">
                        <a id="printSuratLunas" runat="server"><b>Surat Keterangan Lunas</b></a>
                    </td>--%>
                        <td width="80%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                    accesskey="l">
                            </label>
                        </td>
                        <td>
                            <label class="ibtn ibtn-remove">
                                <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel"
                                    accesskey="d">
                            </label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="stamp">Input :
                        <asp:Label ID="tglInput" runat="server"></asp:Label>
                        </td>
                        <td class="stamp">Edit :
                        <asp:Label ID="tglEdit" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr valign="top">
                            <td>
                                <table cellspacing="5">
                                    <tr>
                                        <td colspan="3">
                                            <p>
                                                <b>Dokumen</b>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a id="aKey" runat="server"><b>No. Kontrak</b></a>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="nokontrak" runat="server" ReadOnly="True" Width="200" CssClass="txt"></asp:TextBox><asp:Label
                                                ID="nokontrakc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>No. Kontrak Manual</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="nkm" runat="server" Width="200" CssClass="txt"></asp:TextBox><asp:Label
                                                ID="nkmc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <b>No. Kontrak yang Digunakan</b>
                                        </td>
                                        <td valign="top">:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="nokused" runat="server">
                                                <asp:ListItem Value="1">Manual</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">System</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <b>Status Titip Jual</b>
                                        </td>
                                        <td valign="top">:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="titipjual" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1">Titip Jual</asp:ListItem>
                                                <asp:ListItem Value="0">Non Titip Jual</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Label ID="titipjualc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <b>Status Paket Investasi</b>
                                        </td>
                                        <td valign="top">:
                                        </td>
                                        <td valign="top">
                                            <asp:CheckBox ID="paketinvest" runat="server" Text="Merupakan paket investasi" />
                                            <br />
                                            Tgl Berakhir Paket Investasi :
                                        <asp:TextBox ID="tglinv" runat="server" Width="100px" CssClass="txt_center" ReadOnly="False"></asp:TextBox>
                                            <label for="tglinv" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            <br />
                                            <asp:Label ID="tglinvc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>NUP</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="noqueue" runat="server" Width="150" CssClass="txt_center"></asp:TextBox>
                                            <asp:Label ID="noqueuec" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Tanggal</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <nobr><asp:textbox id="tglkontrak" runat="server" width="85" cssclass="txt_center"></asp:textbox>
								            <label for="tglkontrak" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
												</nobr>
                                            <asp:Label ID="tglkontrakc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Target BAST</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="targetst" runat="server" Width="85" CssClass="txt_center">1 Jan 2010</asp:TextBox>
                                            <label for="targetst" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="targetstc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Sales</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="agent" runat="server" Width="300" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>Refferator</b></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="reffcust" runat="server" Width="300"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><b>Bank Refferator</b></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="bankreff" runat="server" Width="300"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><b>A/N Bank</b></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="anreff" runat="server" Width="300"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><b>No Rek. Refferator</b></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="norekreff" runat="server" Width="300"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><b>NPWP Refferator</b></td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="npwpreff" runat="server" Width="300"></asp:TextBox></td>
                                    </tr>
                                    <tr id="reff" runat="server" visible="false" style="display: none;">
                                        <td>
                                            <b>Refferator</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="agentreff" runat="server" Width="300" CssClass="ddl">
                                                <asp:ListItem>Refferator:</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="agentreffc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <b>PPN Ditanggung</b>
                                        </td>
                                        <td valign="top">:
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
                                            <b>Jenis KPR</b>
                                        </td>
                                        <td valign="top">:
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
                                            <b>Cara Bayar</b>
                                        </td>
                                        <td valign="top">:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="carabayar2" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>KPR</asp:ListItem>
                                                <asp:ListItem>CASH BERTAHAP</asp:ListItem>
                                                <asp:ListItem>CASH KERAS</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <b>Lokasi Penjualan</b>
                                        </td>
                                        <td valign="top">:
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="lokpen"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <b>Sumber Dana</b>
                                        </td>
                                        <td valign="top">:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSumberDana" runat="server" OnSelectedIndexChanged="ddlSumberDana_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Dana Sendiri</asp:ListItem>
                                                <asp:ListItem Value="1">Pinjaman Bank</asp:ListItem>
                                                <asp:ListItem Value="2">Warisan/Hibah</asp:ListItem>
                                                <asp:ListItem Value="3">Lainnya</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trLainnya" runat="server" visible="false">
                                        <td valign="top">&nbsp;
                                        </td>
                                        <td valign="top">&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lainnya" runat="server" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <b>Tujuan Pembelian</b>
                                        </td>
                                        <td valign="top">:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTujuan" runat="server" AutoPostBack="true">
                                                <asp:ListItem Value="0">Investasi</asp:ListItem>
                                                <asp:ListItem Value="1">Jual Kembali</asp:ListItem>
                                                <asp:ListItem Value="2">Dipakai Sendiri</asp:ListItem>
                                                <asp:ListItem Value="3">Lainnya</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trTujuanLain" runat="server" visible="false">
                                        <td valign="top">&nbsp;
                                        </td>
                                        <td valign="top">&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tujuanlain" runat="server" Width="400px" MaxLength="200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Fitting Out</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="focounter" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox>&nbsp;x&nbsp;Angsuran
                                        <asp:TextBox ID="fo" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                                            ID="foc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="tra" runat="server" style="display: none">
                                        <td>
                                            <b>Nilai Realisasi KPR</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="nilaikpa" runat="server" Width="100" CssClass="txt_num">0</asp:TextBox><asp:Label
                                                ID="nilaikpac" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trb" runat="server" style="display: none">
                                        <td>
                                            <b>Rekening Cair KPR</b>
                                        </td>
                                        <td>:
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
                                            <b>Nilai Klaim</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="nilaiklaim" runat="server" Width="100" CssClass="txt_num">0</asp:TextBox><asp:Label
                                                ID="nilaiklaimc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trd" runat="server">
                                        <td>
                                            <b>Total Pelunasan saat Batal</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="totallunas" runat="server" Width="100" CssClass="txt_num">0</asp:TextBox><asp:Label
                                                ID="totallunasc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trk" runat="server">
                                        <td>
                                            <b>Tgl Pengembalian</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tglkembali" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                            <label for="tglkembali" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="tglkembalic" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="tre" runat="server">
                                        <td>
                                            <b>Nilai Kembali</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="nilaipulang" runat="server" Width="100" CssClass="txt_num">0</asp:TextBox><asp:Label
                                                ID="nilaipulangc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trf" runat="server" style="display: none">
                                        <td>
                                            <b>Rekening Pembatalan</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="acc" runat="server" Width="300" CssClass="ddl">
                                            </asp:DropDownList>
                                            <asp:Label ID="accerr" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>No. Faktur Pajak</b>
                                        </td>
                                        <td>:
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
                                            <b>Virtual Account</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="nova" runat="server" Width="140" CssClass="txt" />
                                            <input type="button" value="Submit" class="btn btn-blue" id="btnpop" runat="server" name="btnpop" />
                                            <asp:CheckBox ID="delva" runat="server" Text="Delete VA" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Status Kontrak</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="prosesbatal" runat="server" Text="Kontrak Sedang Dalam Proses Pembatalan"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <b>Note</b>
                                        </td>
                                        <td valign="top">:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="note" runat="server" Width="350" Height="150" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table height="50">
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a"
                                                OnClick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton>
                                        </td>
                                        <td>
                                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                                style="width: 75px">
                                        </td>
                                        <td style="padding-left: 10px">
                                            <p class="feed">
                                                <asp:Label ID="feed" runat="server"></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                                <input class="btn" id="refresh" type="button" value="Refresh Unit" name="refresh"
                                    runat="server" style="display: none;">
                                <div style="padding-right: 5px; padding-left: 5px; font-weight: bold; font-size: 15pt; padding-bottom: 5px; line-height: normal; padding-top: 5px; font-style: normal; font-variant: normal; display: none;">
                                    <asp:Label ID="refreshinfo" runat="server" ForeColor="red"></asp:Label>&nbsp;
                                </div>
                            </td>
                            <td>
                                <table cellspacing="5">
                                    <tr>
                                        <td>
                                            <a id="aStatus" runat="server">Status</a>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="status" runat="server" Font-Size="14"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <b>PPJB</b>
                                        </td>
                                        <td valign="top">:
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="ppjb" runat="server"></asp:Label>
                                            <br />
                                            <span id="jbm" runat="server">PPJB Manual :
                                            <asp:TextBox ID="ppjbm" runat="server"></asp:TextBox>
                                                <br />
                                                Nomor PPJB yang digunakan :
                                            <asp:RadioButtonList ID="ppjbused" runat="server">
                                                <asp:ListItem Value="1">Manual</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">System</asp:ListItem>
                                            </asp:RadioButtonList>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>AJB</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="ajb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>BAST</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="st" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="bataltr" runat="server">
                                        <td>
                                            <b>Pembatalan</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="batal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br>
                                <table cellspacing="5">
                                    <tr>
                                        <td>
                                            <b>Perhitungan Harga</b>
                                        </td>
                                        <td colspan="3" align="right">
                                            <asp:TextBox ID="skema" Width="90%" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Luas</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="luas" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">m2
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Price List (Gross)</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right" width="120">
                                            <asp:Label ID="gross" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <%--<tr style="display:none;">
											<td>Biaya Gimmick</td>
											<td>:</td>
											<td align="right"><asp:label id="biayagimmick" runat="server" ></asp:label></td>
											<td style="PADDING-LEFT:10px;FONT-WEIGHT:normal;FONT-SIZE:8pt;LINE-HEIGHT:normal;FONT-STYLE:normal;FONT-VARIANT:normal">
												rupiah
											</td>
										</tr>
										<tr style="display:none;">
											<td>Biaya Lain Lain</td>
											<td>:</td>
											<td align="right"><asp:label id="biayalainlain" runat="server" ></asp:label></td>
											<td style="PADDING-LEFT:10px;FONT-WEIGHT:normal;FONT-SIZE:8pt;LINE-HEIGHT:normal;FONT-STYLE:normal;FONT-VARIANT:normal">
												rupiah
											</td>
										</tr>--%>
                                    <tr style="display: none">
                                        <td align="right" colspan="3">
                                            <hr noshade size="1">
                                        </td>
                                        <td style="font-weight: bold">-
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td colspan="2">&nbsp;
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="afterdisc" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td>
                                            <b>PPN</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="ppn" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td align="right" colspan="3">
                                            <hr noshade size="1">
                                        </td>
                                        <td style="font-weight: bold">+
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3">
                                            <hr noshade size="1">
                                        </td>
                                        <td>+
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Total Harga</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="hargaSD" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Bunga</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblBunga" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Diskon</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="diskon" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Diskon Tambahan</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="diskontambahan" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3">
                                            <hr noshade size="1">
                                        </td>
                                        <td>-
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>DPP</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblDPP" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>PPN</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblPPN" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3">
                                            <hr noshade size="1">
                                        </td>
                                        <td>+
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Nilai Kontrak</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="nilai" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Harga Tanah</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="hargatanah" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah / m<sup>2</sup>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td colspan="4">
                                            <b>Perincian Kontrak</b>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td>
                                            <b>DPP</b>
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="DPP2" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td>PPN
                                        <asp:Label ID="statusPPN" runat="server" />
                                        </td>
                                        <td>:
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="PPN2" runat="server"></asp:Label>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">rupiah
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

    <script type="text/javascript">
        function call(nova) {
            document.getElementById('nova').value = nova;
        }
    </script>

</body>
</html>
