<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.TTSRegistrasi" CodeFile="TTSRegistrasi.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Registrasi Pembayaran</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tanda Terima Sementara - Registrasi Pembayaran">
</head>
<style type="text/css">
    .sm TD
    {
        font-weight: normal;
        font-size: 8pt;
        line-height: normal;
        font-style: normal;
        font-variant: normal;
    }
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
</style>
<body style="text-align: center">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div style="float: left; width: 40%;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_prev_c.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Back" class="nav2" style="width: 80px;" onmouseover="over(this)"
                        onclick="window.location='Cashier.aspx';" onmouseout="out(this)">
                </td>
            </tr>
        </table>
    </div>
    <div style="float: right; width: 10%;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_gateway.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Gateway" class="nav2" onclick="top.location.href='/Gateway.aspx'"
                        style="width: 100px" onmouseover="over(this)" onmouseout="out(this)">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_out.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Sign-Out" class="nav2" onclick="if(confirm('Apakah anda ingin melakukan sign-out?\nProgram dan absensi aktif anda akan ditutup.')){top.location.href='SignOut.aspx'}"
                        style="width: 100px" onmouseover="over(this)" onmouseout="out(this)">
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <br />
    <input type="text" style="display: none">
    <p style="text-align: left;">
        <b style="font-size: x-large;">Registrasi Pembayaran</b>
        <br />
        <br />
        Halaman 1 dari 2</p>
    <br>
    <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid;
        border-bottom: #dcdcdc 1px solid" cellspacing="5">
        <tr>
            <td style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal;
                font-variant: normal">
                Customer / Unit / Dokumen :
            </td>
            <td>
                <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="300"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="search" runat="server" CssClass="btn" Text="Search" AccessKey="s"
                    OnClick="search_Click"></asp:Button>
            </td>
        </tr>
    </table>
    <p class="feed" style="float: left;">
        <asp:Label ID="feed" runat="server"></asp:Label>
    </p>
    <br />
    <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
        <asp:TableRow HorizontalAlign="Left" VerticalAlign="Bottom">
            <asp:TableHeaderCell Width="120">Ref.</asp:TableHeaderCell>
            <asp:TableHeaderCell>Status</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="50">Tipe</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="120">Unit</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="280">Customer</asp:TableHeaderCell>
            <asp:TableHeaderCell>#Tagihan</asp:TableHeaderCell>
        </asp:TableRow>
    </asp:Table>

    <script language="javascript">
			function call(ref,tipe)
			{
				if(tipe=="TENANT")
					location.href='TTSRegistrasiPenghuni.aspx?Ref='+ref+'&Tipe='+tipe;
				else
					location.href='TTSRegistrasiMarketing.aspx?Ref='+ref+'&Tipe='+tipe;
			}
    </script>

    </form>
</body>
</html>
