<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.TTS" CodeFile="TTS.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Tanda Terima Sementara</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tanda Terima Sementara">
</head>
<style>
    .nav, .navsub
    {
        border: 0px;
        background-color: #EEEEEE;
        font: 8pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 190;
        height: 18px;
    }
    .nav2
    {
        border: 0px;
        background-color: #EEEEEE;
        font: 14pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 200;
        height: 30px;
    }
    .nav2:hover
    {
        border: 1px;
        background-color: #666;
    }
    a
    {
        text-decoration: none !important;
        color: #666 !important;
    }
    a:hover
    {
        color: Black !important;
    }
    .masuk
    {
        border: none;
    }
    .masuk:hover
    {
        border: 1px solid black;
    }
</style>
<body style="text-align: center">
    <form id="Form1" method="post" runat="server" class="cnt">
    <div style="float: left;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_prev_c.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Back" class="nav2" style="width: 80px;" onmouseover="over(this)"
                        onclick="window.location='Index.html';" onmouseout="out(this)">
                </td>
            </tr>
        </table>
    </div>
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <input type="text" style="display: none">
    <h1>
        Kwitansi</h1>
    <br>
    <table style="border: 1px solid #DCDCDC" cellspacing="5">
        <tr>
            <td>
                <input type="button" class="btn" value="Search" onclick="popDaftarTTS();" id="search"
                    runat="server" name="search" accesskey="s">
            </td>
            <td>
                <asp:DropDownList ID="user" runat="server" CssClass="ddl" Width="180">
                    <asp:ListItem Value="">Kasir :</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                Dari
            </td>
            <td>
                <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                <input type="button" value="..." class="btn" onclick="openCalendar('dari');">
                <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
            </td>
            <td>
                Sampai
            </td>
            <td>
                <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                <input type="button" value="..." class="btn" onclick="openCalendar('sampai');">
                <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
            </td>
            <td>
                <asp:Button ID="display" runat="server" CssClass="btn" Text="Display" OnClick="display_Click">
                </asp:Button>
            </td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:DropDownList ID="carabayar" runat="server" CssClass="ddl" Width="180">
                    <asp:ListItem Value="">Cara Bayar :</asp:ListItem>
                    <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
                    <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                    <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                    <asp:ListItem Value="TR">TR = Transfer Bank</asp:ListItem>
                    <asp:ListItem Value="BG">BG = Cek Giro</asp:ListItem>
                    <asp:ListItem Value="UJ">UJ = Uang Jaminan</asp:ListItem>
                    <asp:ListItem Value="DN">DN = Diskon</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="4">
                Status :
                <asp:RadioButton ID="statusA" runat="server" Text="SEMUA" Checked="True" GroupName="status">
                </asp:RadioButton>
                <asp:RadioButton ID="statusB" runat="server" Text="BARU" GroupName="status"></asp:RadioButton>
                <asp:RadioButton ID="statusP" runat="server" Text="POST" GroupName="status"></asp:RadioButton>
                <asp:RadioButton ID="statusV" runat="server" Text="VOID" GroupName="status"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:DropDownList ID="tipe" runat="server" CssClass="ddl" Width="180">
                    <asp:ListItem Value="">Tipe :</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br>
    <p style="padding: 3; font: 8pt">
        Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer Bank
        / BG = Cek Giro / UJ = Uang Jaminan / DN = Diskon
    </p>
    <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
        <asp:TableRow HorizontalAlign="Left">
            <asp:TableHeaderCell Width="100">No. TTS</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="110">Tgl. / Kasir</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="200">Customer</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="200">Keterangan</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="130">Cara Bayar</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="90" HorizontalAlign="Right">Total</asp:TableHeaderCell>
        </asp:TableRow>
    </asp:Table>

    <script language="javascript">
        function call(nomor) {
            popEditTTS(nomor);
        }
    </script>

    </form>
</body>
</html>
