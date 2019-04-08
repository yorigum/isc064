<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GimmickEdit.aspx.cs" Inherits="ISC064.ADMINJUAL.GimmickEdit" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gimmick Edit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Gimmick Edit">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div>
            <h1 class="title title-line">Edit Gimmick</h1>
            <br>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%" cellpadding="5">
                            <tr>
                                <td colspan="3">Gimmick</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellspacing="5">
                                        <tr>
                                            <td width="100%"></td>
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
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:RadioButton ID="aktif" runat="server" Text="Aktif" Font-Size="12" Font-Bold="True" ForeColor="green"
                                        GroupName="status" Style="padding-right: 20px"></asp:RadioButton>
                                    <asp:RadioButton ID="inaktif" runat="server" Text="Inaktif" Font-Size="12" Font-Bold="True" ForeColor="red"
                                        GroupName="status"></asp:RadioButton>
                                </td>
                            </tr>
                            <tr>
                                <td>Project</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="project" runat="server">
                                        <asp:ListItem>Project :</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Tipe Barang</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="tipe" runat="server">
                                        <asp:ListItem>Tipe :</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%;">Item ID</td>
                                <td style="width: 1%;">:</td>
                                <td style="width: 89%;" >
                                    <asp:TextBox ID="noid" runat="server" ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>Nama Barang</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="nama" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Satuan Barang</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="satuan" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Qty Barang</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="qty" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Harga Satuan Barang</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="hsb" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Harga Total Barang</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="htb" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="htb2" runat="server" style="display:none;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align:top;">Ket</td>
                                <td style="vertical-align:top;">:</td>
                                <td>
                                    <asp:TextBox ID="ket" runat="server" TextMode="MultiLine" Width="200" Height="150"/>
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
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
