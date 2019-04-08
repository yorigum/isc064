<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Retensi.aspx.cs" Inherits="ISC064.KPA.Retensi" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Setup Rekening</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Retensi - Setup Retensi">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Kategori Retensi</h1>
        <br>
        <table cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="220">
                    <p style="font: bold 10pt">Daftar Jenis Retensi</p>
                    <ul id="list" runat="server" class="plike">
                    </ul>
                </td>
                <td style="padding: 5px 10px 0px 15px">
                    <img src="/Media/line_vert.gif"></td>
                <td>
                    <h2 style="padding-left: 5px; padding-bottom: 5px">Pendaftaran Kategori Retensi Baru</h2>
                    <table cellspacing="5">
                        <tr>
                            <td>Kode</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="kode" runat="server" Width="160" MaxLength="20" CssClass="txt"></asp:TextBox>
                                <asp:Label ID="kodec" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Nama Kategori Retensi</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="nama" runat="server" Width="200" CssClass="txt" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Project</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList runat="server" ID="project" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table height="50">
                        <tr>
                            <td>
                                <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                                </asp:LinkButton>
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
    </form>
</body>
</html>

