<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RewardR.aspx.cs" Inherits="ISC064.KOMISI.RewardR" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Realisasi Pencairan Reward</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Realisasi Pencairan Reward">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Realisasi Pencairan Reward</h1>
        <br />
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>
                    <input type="button" style="" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Realisasi Pencairan Reward' modal-url='DaftarRewardR.aspx' id="search" runat="server" name="search" accesskey="s">
                </td>
                <td><b>Dari</b></td>
                <td>
                    <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="135"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td><b style="padding-left: 10px;">Sampai</b></td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="135"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
                <td style="">
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>

        </table>
        <br>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" style="min-width:80%" CellSpacing="1">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell>Kode Realisasi</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="70">Tgl.</asp:TableHeaderCell>
                <asp:TableHeaderCell>Keterangan</asp:TableHeaderCell>
                <asp:TableHeaderCell>Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <script type="text/javascript">
            function call(nomor) {
                popEditRewardR(nomor);
            }
        </script>
    </form>
</body>
</html>
