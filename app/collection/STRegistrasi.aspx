<%@ Page language="c#" Inherits="ISC064.COLLECTION.STRegistrasi" CodeFile="STRegistrasi.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Registrasi Surat Peringatan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Surat Peringatan - Registrasi Surat Peringatan">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Registrasi Surat Peringatan</h1>
        <p>Halaman 1 dari 2</p>
        <br>
        <table cellspacing="5">
            <tr>
                <td style="font: 8pt"><b>Customer / Unit / Dokumen :</b></td>
                <td>
                    <asp:TextBox ID="keyword" runat="server" CssClass="input-text" Width="300"></asp:TextBox>
                    <asp:DropDownList runat="server" ID="project"></asp:DropDownList>
                    <asp:Button ID="search" runat="server" CssClass="btn btn-blue" Text="Search" AccessKey="s" OnClick="search_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="info" runat="server">Data yang ditampilkan hanya data tagihan yang menunggak</asp:Label>
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="Ref." DataField="Ref" />
                <asp:BoundField HeaderText="Status " DataField="Status" />
                <asp:BoundField HeaderText="Tipe" DataField="Tipe" />
                <asp:BoundField HeaderText="Unit" DataField="Unit" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="#Tagihan" DataField="Tagihan" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(ref, tipe) {
                if (tipe == "TENANT")
                    location.href = 'STRegistrasiPenghuni.aspx?Ref=' + ref + '&Tipe=' + tipe;
                else
                    location.href = 'STRegistrasiMarketing.aspx?Ref=' + ref + '&Tipe=' + tipe;
            }
        </script>
    </form>
</body>
</html>
