<%@ Page Language="c#" Inherits="ISC064.SETTINGS.SmsFormat2" CodeFile="SmsFormat2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Setup Format SMS</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup - Format SMS">
</head>
<body class="body-padding">
    <form id="Form2" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Format SMS</h1>
        <br>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <table cellspacing="5">
            <tr>
                <td>Project</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Tipe</td>
                <td>:</td>
                <td>
                    <asp:Label ID="tipe" runat="server" class="form-control" Width="180" />
                </td>
            </tr>
            <div id="divwaktu" runat="server" visible="false">
                <tr>
                    <td>Waktu Pengiriman</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="waktu" runat="server" CssClass="txt" required="required" Text="0" Width="50" />
                        <asp:Label ID="waktuc" runat="server" CssClass="err"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="satuan" runat="server" CssClass="txt" Width="100%">
                            <asp:ListItem Value="detik">Detik</asp:ListItem>
                            <asp:ListItem Value="menit">Menit</asp:ListItem>
                            <asp:ListItem Value="jam">Jam</asp:ListItem>
                            <asp:ListItem Value="hari" Selected="True">Hari</asp:ListItem>
                            <asp:ListItem Value="bulan">Bulan</asp:ListItem>
                            <asp:ListItem Value="tahun">Tahun</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </div>
            <tr>
                <td>Format</td>
                <td>:</td>
                <td colspan="2">
                    <asp:TextBox ID="format" runat="server" CssClass="txt" required="required" TextMode="MultiLine" Width="300" Height="100" />
                    <asp:Label ID="maskingc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Status Inaktif</td>
                <td>:</td>
                <td>
                    <asp:RadioButtonList ID="inaktif" runat="server" CssClass="cb" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="true">Aktif</asp:ListItem>
                        <asp:ListItem>Inaktif</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
        <table class="tb blue-skin">
            <tr>
                <th>Parameter</th>
                <th>Keterangan</th>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>

    </form>
</body>
</html>
