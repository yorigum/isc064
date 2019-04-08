<%@ Reference Page="~/Customer.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.TunggakanEdit" CodeFile="TunggakanEdit.aspx.cs" %>

<%@ Register TagPrefix="uc2" TagName="Head" Src="Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeadTunggakan" Src="HeadTunggakan.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavTunggakan" Src="NavTunggakan.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit Surat Peringatan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Surat Peringatan - Edit Surat Peringatan">


    <script type="text/javascript">
    </script>
</head>
<body onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <uc2:Head ID="Head1" runat="server"></uc2:Head>
        <div class="content-header">
            <uc1:NavTunggakan ID="NavTunggakan1" runat="server" Aktif="1"></uc1:NavTunggakan>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadTunggakan ID="HeadTunggakan1" runat="server"></uc1:HeadTunggakan>
                <table cellspacing="5">
                    <tr>
                        <td style="width: 5%">Print :</td>
                        <td class="printhref"><a id="printST" runat="server">
                            <b>
                                <asp:Label ID="lblSP" runat="server"></asp:Label><asp:Label ID="printSP" runat="server"></asp:Label>
                            </b>
                        </a>

                        </td>
                        <td width="75%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                    accesskey="l">
                            </label>
                        </td>
                        <td>
                            <input type="button" class="btn btn-blue" value="Settlement" id="btnsettle" runat="server" name="btnsettle"
                                accesskey="s" style="width: 120px">
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td>
                            <table cellspacing="5">
                                <tr>
                                    <td colspan="3">
                                        <p><b>Status</b></p>
                                        <asp:Label ID="lv" runat="server" Font-Bold="True" Font-Size="40"></asp:Label>
                                        <i>
                                            <asp:Label ID="status" runat="server" Font-Size="14"></asp:Label>
                                        </i>
                                        <br>
                                        <br>
                                        <p><b>Data Collection</b></p>
                                    </td>
                                    <td width="20"></td>
                                    <td>
                                        <asp:Table ID="detil" runat="server" CssClass="tb" CellSpacing="5">
                                            <asp:TableRow HorizontalAlign="Left">
                                                <asp:TableHeaderCell>Tagihan</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>Jatuh Tempo</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>Telat (Hari)</asp:TableHeaderCell>
                                                <asp:TableHeaderCell HorizontalAlign="Right">Pokok</asp:TableHeaderCell>
                                                <asp:TableHeaderCell HorizontalAlign="Right">Denda</asp:TableHeaderCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td><b>No. Tunggakan</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <asp:TextBox ID="manuTunggakan" CssClass="input-text" runat="server" Width="85px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Tanggal</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <asp:TextBox ID="tgl" runat="server" type="text" CssClass="txt_center" Style="width: 135px"></asp:TextBox>
                                        <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Nilai</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <asp:Label ID="total" runat="server" Font-Bold="True" Font-Size="12"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" id="trTglSuratKuasa">
                                    <td><b>Tgl Kuasa</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <div class="input-group input-medium">
                                            <asp:TextBox ID="tglKuasa" runat="server" type="text" CssClass="form-control" Style="width: 50%" Height="20"></asp:TextBox>
                                            <span class="input-group-btn" style="height: 34px; display: block">
                                                <button class="btn-a default" runat="server" onclick="openCalendar('tglKuasa');" type="button" style="height: 100%">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                        </div>
                                        <asp:Label ID="tglKuasac" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br>
                                        <p><b>Unit Properti</b></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Unit</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <asp:TextBox ID="unit" runat="server" Width="200" CssClass="input-text" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="unitc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br>
                                        <p><b>Alamat Penagihan</b></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Customer</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <asp:TextBox ID="customer" runat="server" Width="300" CssClass="input-text" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="customerc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Telepon</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <asp:TextBox ID="notelp" runat="server" Width="350" CssClass="input-text" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Handphone</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <asp:TextBox ID="hp" runat="server" Width="350" CssClass="input-text" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td rowspan="3"><b>Alamat</b></td>
                                    <td rowspan="3"><b>:</b></td>
                                    <td>
                                        <p>
                                            <asp:TextBox ID="alamat1" runat="server" Width="350" CssClass="input-text" MaxLength="50"></asp:TextBox>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            <asp:TextBox ID="alamat2" runat="server" Width="350" CssClass="input-text" MaxLength="50"></asp:TextBox>
                                        </p>
                                    </td>
                                </tr>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <asp:TextBox ID="alamat3" runat="server" Width="200" CssClass="input-text" MaxLength="50"></asp:TextBox>
                            </p>
                        </td>
                    </tr>
                </table>
                <table height="50">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"> <i class="fa fa-check"></i> Apply</asp:LinkButton>
                        </td>
                        <td>
                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="width: 75px">
                        </td>
                        <td style="padding-left: 10px">
                            <p class="feed">
                                <asp:Label ID="feed" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
