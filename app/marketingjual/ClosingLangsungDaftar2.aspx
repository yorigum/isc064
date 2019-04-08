<%@ Reference Page="~/Customer.aspx" %>
<%@ Reference Page="~/Unit.aspx" %>
<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.ClosingLangsungDaftar2" CodeFile="ClosingLangsungDaftar2.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Pendaftaran Closing Langsung</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Closing Langsung - Pendaftaran Closing Langsung (Hal. 2)">
    <style type="text/css">
        .style1 {
            height: 26px;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Closing Langsung</h1>
        <table>
            <tr style="vertical-align: top">
                <td style="width: 550px">
                    <div id="pilih" runat="server">
                        <p><b><i>Halaman 1 dari 3</i></b></p>
                        <br>
                        <table>
                            <tr>
                                <td>
                                    <b>Customer</b>
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="nocustomer" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                                    <input class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Customer' type="button" value="&#xf002;" style="font-family: 'fontawesome'" id="btnpop" runat="server" name="btnpop">
                                    <input class="btn btn-blue" id="btnbaru" type="button" value="Baru" name="btnbaru" runat="server">
                                </td>
                                <td>&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">
                                     Next <i class="fa fa-arrow-right"></i>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
                    <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="frm" runat="server">
                                <p><b><i>Halaman 2 dari 3</i></b></p>
                                <br />
                                <div id="dclosing" runat="server">
                                    <table>
                                        <tr>
                                            <td colspan="3">
                                                <h3>
                                                    <span style="width: 30px">1.</span> UNIT YANG DIPESAN</h3>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>NUP
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="noqueue" runat="server" Width="50" CssClass="txt_center"></asp:TextBox>
                                                <asp:Label ID="noqueuec" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tanggal Kontrak
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tglKontrak" runat="server" Width="100px" CssClass="txt_center" ReadOnly="False"
                                                    Height="20px"></asp:TextBox>&nbsp;
                                        <label for="tglKontrak" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                <asp:Label ID="tglkontrakc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Lokasi Penjualan</td>
                                            <td>:</td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="lokpen">
                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Kode Unit
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="unit" runat="server" Width="100" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                <div style="display: none">
                                                    <asp:TextBox ID="nostock" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="nokontrak" runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="nokontrakmanual" runat="server"></asp:TextBox>
                                                </div>
                                                <input visible="false" class="btn" id="btnUnit" onclick="popDaftarUnit2('a')" type="button"
                                                    value="&#xf002;" style="font-family: 'fontawesome'" name="btnUnit" runat="server" />
                                                <asp:Label ID="unitc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                            <asp:TextBox ID="nostock2" runat="server" Style="display: none"></asp:TextBox>
                                        </tr>
                                        <tr style="vertical-align: top">
                                            <td>Price List
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Pricelist" runat="server" Width="100" ReadOnly="true"></asp:TextBox>&nbsp
                                    rupiah
                                    <asp:Label ID="pricec" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <div id="pldefault" runat="server">
                                            <tr style="vertical-align: top; display: none;">
                                                <td class="style1">Pricelist Berdasarkan
                                                </td>
                                                <td class="style1">:
                                                </td>
                                                <td class="style1">
                                                    <asp:DropDownList ID="pldef" runat="server" Width="200" AutoPostBack="True"
                                                        OnSelectedIndexChanged="pldef_SelectedIndexChanged" Enabled="false">
                                                        <asp:ListItem>Pilih Salah Satu</asp:ListItem>
                                                        <asp:ListItem Value="1">PriceList Kavling</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">PriceList Rumah</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </div>
                                        <tr style="vertical-align: top">
                                            <td class="style1">Skema
                                            </td>
                                            <td class="style1">:
                                            </td>
                                            <td class="style1">
                                                <asp:DropDownList ID="skema" runat="server" Width="400" AutoPostBack="True"
                                                    OnSelectedIndexChanged="skema_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Label ID="skemac" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sifat PPN
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="sifatppn" Enabled="false" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                    <%--onselectedindexchanged="sifatppn_SelectedIndexChanged">--%>
                                                    <asp:ListItem Selected="False">Tanpa PPN</asp:ListItem>
                                                    <asp:ListItem Selected="True">Dengan PPN</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trppn" runat="server">
                                            <td colspan="2">&nbsp;
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="roundppn" runat="server" Checked="True" Text="Nilai PPN Dibulatkan"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top">Diskon Harga Jual
                                            </td>
                                            <td style="vertical-align: top">:
                                            </td>
                                            <td>
                                                <div id="lumsum" runat="server" style="display: none">
                                                    <asp:TextBox ID="diskon" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                                                        ID="diskonc" runat="server" CssClass="err"></asp:Label>
                                                </div>
                                                <div id="persentingkat" runat="server">
                                                    <input class="btn" onclick="popdiskon('diskon2', 'diskonket')" type="button" value="..."
                                                        id="btnBertingkat" runat="server" />
                                                    <asp:TextBox ID="diskon2" runat="server" Width="60px" MaxLength="100" CssClass="txt_num"
                                                        AutoPostBack="true" OnTextChanged="diskon2_TextChanged">0</asp:TextBox>&nbsp;
                                        <div style="display: none">
                                            <asp:TextBox ID="diskonket" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="nilaiDiskon" runat="server" CssClass="txt_num" ReadOnly="True" Width="108px">0</asp:TextBox>
                                        </div>
                                                    <asp:Label ID="diskon2c" runat="server" CssClass="err"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top">Diskon Tambahan
                                            </td>
                                            <td style="vertical-align: top">:
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="jenisDiskon" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="jenisDiskon_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">Rp</asp:ListItem>
                                                    <asp:ListItem>% Bertingkat</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <div id="divLumpSum" runat="server">
                                                    <asp:TextBox ID="diskonLumpSum" runat="server" CssClass="txt_num">0</asp:TextBox>
                                                    <asp:Label ID="diskonLumpSumc" runat="server" CssClass="err" />
                                                </div>
                                                <div id="divPersenBertingkat" runat="server">
                                                    <asp:TextBox ID="diskontambahPersen" runat="server" Width="150" MaxLength="100" CssClass="txt">0</asp:TextBox>
                                                    <input class="btn" onclick="popdiskon('diskontambahPersen', 'diskontambahKet')" type="button" value="..." />
                                                    <div style="display: none;">
                                                        <asp:TextBox ID="diskontambahKet" runat="server"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="diskontambahPersenc" runat="server" CssClass="err" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top">Bunga
                                            </td>
                                            <td style="vertical-align: top">:
                                            </td>
                                            <td>
                                                <div id="persentingakat" runat="server">
                                                    <input class="btn" onclick="popbunga('bunga2', 'bungaket')" type="button" value="&#xf002;" style="font-family: 'fontawesome'"
                                                        id="btnBertingkat2" runat="server">
                                                    <asp:TextBox ID="bunga2" runat="server" CssClass="txt" Width="50px" MaxLength="100"
                                                        AutoPostBack="true" OnTextChanged="bunga2_TextChanged" Text='0'>0</asp:TextBox>&nbsp;
                                        <div style="display: none">
                                            <asp:TextBox ID="bungaket" runat="server"></asp:TextBox>
                                        </div>
                                                    <asp:TextBox ID="nilaiBunga" runat="server" CssClass="txt_num" ReadOnly="True" Text='0'>0</asp:TextBox>
                                                    <asp:Label ID="bunga2c" runat="server" CssClass="err"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top">Sumber Dana
                                            </td>
                                            <td style="vertical-align: top">:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSumberDana" runat="server" OnSelectedIndexChanged="ddlSumberDana_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="0">Dana Sendiri</asp:ListItem>
                                                    <asp:ListItem Value="1">Pinjaman Bank</asp:ListItem>
                                                    <asp:ListItem Value="2">Warisan/Hibah</asp:ListItem>
                                                    <asp:ListItem Value="3">Lainnya</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="ddlSumberDanac" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trLainnya" runat="server" visible="false">
                                            <td style="vertical-align: top">&nbsp;
                                            </td>
                                            <td style="vertical-align: top">&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lainnya" runat="server" Width="400px" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top">Tujuan Pembelian
                                            </td>
                                            <td style="vertical-align: top">:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTujuan" runat="server" OnSelectedIndexChanged="ddlTujuan_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="0">Investasi</asp:ListItem>
                                                    <asp:ListItem Value="1">Jual Kembali</asp:ListItem>
                                                    <asp:ListItem Value="2">Dipakai Sendiri</asp:ListItem>
                                                    <asp:ListItem Value="3">Lainnya</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="ddlTujuanc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trTujuanLain" runat="server" visible="false">
                                            <td style="vertical-align: top">&nbsp;
                                            </td>
                                            <td style="vertical-align: top">&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tujuanlain" runat="server" Width="400px" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td style="vertical-align: top">PPN Ditanggung
                                            </td>
                                            <td style="vertical-align: top">:
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="JenisPPN" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="False">PEMERINTAH</asp:ListItem>
                                                    <asp:ListItem Selected="True">KONSUMEN</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:Label ID="JenisPPNc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="vertical-align: top">Status Titip Jual
                                            </td>
                                            <td style="vertical-align: top">:
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="titipjual" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="False" Value="1">Titip Jual</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="0">Non Titip Jual</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:Label ID="titipjualc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="vertical-align: top">Status Paket Investasi
                                            </td>
                                            <td style="vertical-align: top">:
                                            </td>
                                            <td style="vertical-align: top">
                                                <asp:CheckBox ID="paketinvest" runat="server" Text="Merupakan paket investasi" />
                                                <br />
                                                Tgl Berakhir Paket Investasi :
                                    <asp:TextBox ID="tglinv" runat="server" Width="100px" CssClass="txt_center" ReadOnly="False"></asp:TextBox>&nbsp;
                                    <label for="tglinv" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                <br />
                                                <asp:Label ID="tglinvc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="3">
                                                <br />
                                                <h3>
                                                    <span style="width: 30px">2.</span> SALES PERSON</h3>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Kode Sales
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="agent" runat="server" Width="400" AutoPostBack="true"
                                                    OnSelectedIndexChanged="GantiTipeSales">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td>Refferator
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="reffcust" runat="server" Width="300"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td>Bank Refferator
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="bankreff" runat="server" Width="300"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td>No Rek. Refferator
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="norekreff" runat="server" Width="300"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td>A/N Bank Reff
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="anreff" runat="server" Width="300"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td>NPWP Refferator
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="npwpreff" runat="server" Width="300"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>Data eSales
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="esales" runat="server">
                                                    <asp:ListItem Value="">Bukan berasal dari eSales</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="reff" runat="server" visible="false" style="display: none;">
                                            <td>Refferator
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="agentreff" runat="server" Width="300" Visible="false">
                                                    <asp:ListItem>Refferator:</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="rep" runat="server" Width="300"></asp:TextBox>
                                                <asp:Label ID="agentreffc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr style="display: none;">
                                            <td colspan="3">
                                                <br />
                                                <h3>
                                                    <span>3.</span>Gimmick</h3>
                                            </td>
                                        </tr>

                                        <tr style="display: none;">
                                            <td>Type Gimmick
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="gimmick" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                                                <input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Gimmick' modal-url='DaftarGimmick.aspx' id="btnpopgimmick"
                                                    runat="server" name="btnpop">
                                                <asp:Label ID="gimmickc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr style="display: none">
                                            <td colspan="3">
                                                <br />
                                                <h3>
                                                    <span style="display: none">4.</span> BOOKING FEE</h3>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>Rekening Bank
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAcc" runat="server" Width="300">
                                                    <%--<asp:listitem selected="True">- Pilih Rekening Bank -</asp:listitem>--%>
                                                </asp:DropDownList>
                                                <asp:Label ID="ddlAccErr" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>Nilai
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="carabayar" runat="server">
                                                    <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
                                                    <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                                                    <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                                                    <asp:ListItem Value="TR">TR = Transfer Bank</asp:ListItem>
                                                    <asp:ListItem Value="BG">BG = Cek Giro</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="nilai" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                                                    ID="nilaic" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>Keterangan TTS
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="kettts" runat="server" Width="200" MaxLength="200" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>Bank BG
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="bankbg" runat="server" Width="125" MaxLength="50" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>No. BG
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nobg" runat="server" Width="125" MaxLength="20" CssClass="txt"></asp:TextBox>&nbsp;
                                    Tgl :
                                    <asp:TextBox ID="tglbg" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                                <label for="tglbg" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                <asp:Label ID="bgc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>No. Kartu
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nokk" runat="server" Width="125" CssClass="txt" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>Bank Kartu
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="bankkk" runat="server" Width="125" CssClass="txt" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td colspan="3">
                                                <br>
                                                <h3>
                                                    <span style="width: 30px">3.</span> KATEGORISASI</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top">Cara Bayar
                                            </td>
                                            <td style="vertical-align: top">:
                                            </td>
                                            <td>
                                                <table id="tablecarabayar" cellspacing="1" cellpadding="1" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList ID="carabayar2" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="CASH KERAS">CASH KERAS</asp:ListItem>
                                                                <asp:ListItem Value="CASH BERTAHAP">CASH BERTAHAP</asp:ListItem>
                                                                <asp:ListItem Value="KPR">KPR</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="carabayarc" runat="server" CssClass="err"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server">
                                            <td style="vertical-align: top">&nbsp;
                                        Note</td>
                                            <td style="vertical-align: top">&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="note" runat="server" Width="350" Height="150" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>Fitting Out
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="focounter" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox>&nbsp;x&nbsp;Angsuran
                                    <asp:TextBox ID="fo" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                                        ID="foc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table height="50">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                                            </td>
                                            <td>
                                                <input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
                                                    runat="server">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ok" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 70px">
                    <img src="/Media/line_vert.gif">
                </td>
                <td style="padding-top: 60px">
                    <table cellspacing="5">
                        <tr>
                            <td>Unit :
                            <br>
                                <asp:Label ID="nounit" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Customer :
                            <br>
                                <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <script src="/Js/jquery.signalR-2.2.3.min.js"></script>
        <script src="signalr/hubs" type="text/javascript"></script>
        <script type="text/javascript">
            ////ini untuk booking saat closing
            $(function () {
                var userid = "<%=ISC064.Act.UserID %>";
                var nostock = $("#nostock2").val();
                var test = $.connection.closingHub;
                $.connection.hub.qs = "UserID=" + userid + "&NoStock=" + nostock;

                test.client.broadcastMsg = function (user, nostock) {
                    console.log(user);
                    console.log(nostock);
                };

                $.connection.hub.start().done(function () {
                    test.server.hello(userid, nostock);
                });
            });
            /////
            function call(nomor) {
                document.getElementById('nocustomer').value = nomor;
                document.getElementById('next').click();
            }
            function call2(nomor, nounit) {
                document.getelementbyid('nostock').value = nomor;
                document.getelementbyid('unit').value = nounit;
            }
            function kalk(foo) {
                nomor = foo.options[foo.selectedIndex].value;
                if (nomor != 0) {
                    pl = document.getElementById('pl').value;
                    tgl = document.getElementById('tgl').value;

                    popSkema(nomor, pl, tgl);
                }
            }

            function callgimmick(nomor) {
                document.getElementById('gimmick').value = nomor;
            }

            function callSource(nomor, source) {
                document.getElementById('spgabungan').value = nomor;
            }
            function popdiskon(d1, d2) {
                foo1 = document.getElementById(d1);
                foo2 = document.getElementById(d2);
                openModal('SkemaDiskon.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
            }

            function popbunga(d1, d2) {
                foo1 = document.getElementById(d1);
                foo2 = document.getElementById(d2);
                openModal('SkemaBunga.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
            }
            function recaldisc(discTxt) {
                disc = discTxt.value.split("+");

                discTxt.value = "";

                for (i = 0; i < disc.length; i++) {
                    if (!isNaN(disc[i]) && disc[i] != "") {
                        if (discTxt.value != "") discTxt.value = discTxt.value + "+";
                        discTxt.value = discTxt.value + disc[i];
                    }
                }
            }
            function cvtnum(foo) {
                return foo.replace(/,/gi, "");
            }
            function recal() {

            }
        </script>

    </form>
</body>
</html>
