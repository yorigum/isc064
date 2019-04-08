<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.TTSRegistrasi" CodeFile="TTSRegistrasi.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Registrasi Tanda Terima Sementara</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tanda Terima Sementara - Registrasi TTS">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Registrasi Tanda Terima Sementara</h1>
        <p><b><i>Halaman 1 dari 2</i></b></p>
        <table>
            <tr>
                <td>
                    <b>Customer / Unit / Dokumen </b>
                </td>
                <td>
                    <asp:TextBox ID="keyword" runat="server" Width="300"></asp:TextBox>
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
                    location.href = 'TTSRegistrasiPenghuni.aspx?Ref=' + ref + '&Tipe=' + tipe;
                else
                    location.href = 'TTSRegistrasiMarketing.aspx?Ref=' + ref + '&Tipe=' + tipe;
            }
        </script>
    </form>
</body>
</html>
