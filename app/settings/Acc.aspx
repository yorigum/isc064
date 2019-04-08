<%@ Page Language="c#" Inherits="ISC064.SETTINGS.Acc" CodeFile="Acc.aspx.cs" %>

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
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kas Bank - Setup Rekening">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Rekening</h1>
        <br>
        <table cellpadding="1" cellspacing="0">
            <tr valign="top">
                <td style="width: 220px">
                    <h2>Daftar Rekening</h2>
                    <ul id="list" runat="server" class="plike">
                    </ul>
                </td>
                <td style="padding: 5px 10px 0px 15px">
                    <img src="/Media/line_vert.gif"></td>
                <td>
                    <h2 style="padding-left: 5px; padding-bottom: 5px">Pendaftaran Rekening Baru</h2>
                    <table cellspacing="5">
                        <tr>
                            <td>No. Account</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="acc" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:Label ID="accc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Sub ID</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="subid" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:Label ID="subidc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Project
                            </td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="project" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Rekening</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="rekening" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Bank</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="bank" runat="server" MaxLength="50"></asp:TextBox><asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Cabang</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="cabang1" runat="server" MaxLength="50"></asp:Textbox>
                            </td>
                        </tr>
                        <tr>
                            <td>Atas Nama</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="atasnama" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Saldo Awal</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="saldoawal" runat="server" CssClass="txt_num"></asp:TextBox>
                                <asp:Label ID="saldoawalc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="height: 50px">
                        <tr>
                            <td>
                                <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
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
