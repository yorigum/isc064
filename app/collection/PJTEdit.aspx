<%@ Reference Page="~/Customer.aspx" %>

<%@ Page Language="c#" AutoEventWireup="true" Inherits="ISC064.COLLECTION.PJTEdit" CodeFile="PJTEdit.aspx.cs" %>

<%@ Register TagPrefix="uc2" TagName="Head" Src="Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeadPJT" Src="HeadPJT.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavPJT" Src="NavPJT.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit Pemberitahuan Jatuh Tempo</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="P. Jatuh Tempo - Edit P. Jatuh Tempo">
</head>
<body onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <uc2:Head ID="Head1" runat="server"></uc2:Head>
        <div class="content-header">
            <uc1:NavPJT ID="NavPJT1" runat="server" Aktif="1"></uc1:NavPJT>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadPJT ID="HeadPJT1" runat="server"></uc1:HeadPJT>
                <table cellspacing="5">
                    <tr>
                        <td><b>Print</b></td>
                        <td><b>:</b></td>
                        <td class="printhref"><a id="printPJT" runat="server"><b>Surat P. Jatuh Tempo</b></a></td>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                    accesskey="l">
                            </label>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td>
                            <table cellspacing="5">
                                <tr>
                                    <td colspan="3">
                                        <p><b>Data Collection</b></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Tgl. Cetak</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <asp:TextBox ID="tgl" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                        <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Tgl. Jatuh Tempo</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <asp:Label ID="tgljt" runat="server" Font-Bold="True" Font-Size="12"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Nilai</b></td>
                                    <td><b>:</b></td>
                                    <td>
                                        <asp:Label ID="total" runat="server" Font-Bold="True" Font-Size="12"></asp:Label>
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
                                            <asp:TextBox ID="alamat1" runat="server" Width="350" CssClass="input-text" MaxLength="50"></asp:TextBox></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            <asp:TextBox ID="alamat2" runat="server" Width="350" CssClass="input-text" MaxLength="50"></asp:TextBox></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            <asp:TextBox ID="alamat3" runat="server" Width="200" CssClass="input-text" MaxLength="50"></asp:TextBox></p>
                                    </td>
                                </tr>
                            </table>
                            <table height="50">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply</asp:LinkButton>
                                    </td>
                                    <td>
                                        <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="width: 75px">
                                    </td>
                                    <td style="padding-left: 10">
                                        <p class="feed">
                                            <asp:Label ID="feed" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td>
                            <p><b>Detil Tagihan</b></p>
                            <ul id="detil" runat="server" class="plike" style="width:250px;">
                            </ul>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
