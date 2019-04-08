<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GimmickDaftar.aspx.cs" Inherits="ISC064.ADMINJUAL.GimmickDaftar" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pendaftaran Gimmick</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Pendaftaran Gimmick">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div>
            <h1 class="title title-line">Pendaftaran Tipe Gimmick</h1>
            <br />
            <table cellspacing="0">
                <tr valign="top">
                    <td>
                        <td style="padding-right: 10px">
                            <p>
                                <b>Terbaru :</b>
                            </p>
                            <asp:ListBox ID="baru" Rows="25" runat="server" Width="200" CssClass="ddl"></asp:ListBox>
                            <p class="feed">
                                <asp:Label ID="feed" runat="server"></asp:Label>
                            </p>
                        </td>
                        <td style="padding-right: 10px; padding-left: 15px; padding-bottom: 0px; padding-top: 5px">
                            <img src="/Media/line_vert.gif">
                        </td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td colspan="3">Spesifikasi</td>
                            </tr>
                            <tr>
                                <td>Project</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="project" runat="server" Width="150" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem>Project :</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>No. ID</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="noid" runat="server" Text="#AUTO" ReadOnly="true" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    Tipe Barang
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="tipe" runat="server" Width="150">
                                        <asp:ListItem Selected="True" Value="">Pilih Tipe :</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Nama Barang</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="Nama" runat="server" Width="200" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Satuan Barang
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="satuan" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   Qty
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="qty" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Harga Satuan Barang
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="hrst" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align:top;">
                                    Ket
                                </td>
                                <td style="vertical-align:top;">:</td>
                                <td>
                                    <asp:TextBox ID="ket" runat="server" TextMode="MultiLine" Width="200" Height="150"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table height="50">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click">
                                <i class="fa fa-share"></i> OK
                                    </asp:LinkButton>
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
