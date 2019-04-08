<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KontrakRealisasi.aspx.cs" Inherits="ISC064.KPA.KontrakRealisasi" %>

<!DOCTYPE html>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>


<html>
<head>
    <title>Realisasi Tagihan KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tagihan KPR - Realisasi Tagihan">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt" style="padding-left: 10px; padding-top: 10px;">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Realisasi Tagihan KPR</h1>
        <p>Halaman 1 dari 2</p>
        <br>
        <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid"
            cellspacing="5">
            <tr>
                <td style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">Customer 
						/ Unit / Dokumen Pengajuan :</td>
                <td>
                    <asp:TextBox ID="keyword" runat="server" CssClass="input-text" Width="300"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="project"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="search" runat="server" CssClass="btn btn-blue" Text="Search" AccessKey="s" OnClick="search_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <br />
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Next" />
                <asp:BoundField HeaderText="No. Pengajuan" DataField="Pengajuan" />
                <asp:BoundField HeaderText="Tgl. / Kasir" DataField="Tgl" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Total Pengajuan" DataField="Total" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call2(ref) {
                location.href = 'KontrakRealRegistrasi.aspx?id=' + ref;
            }
            function call(nomor) {
                popEditPengajuan(nomor);
            }
        </script>
    </form>
</body>
</html>
