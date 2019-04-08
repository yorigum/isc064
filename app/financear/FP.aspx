<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.FP" CodeFile="FP.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Faktur Pajak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Faktur Pajak">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Faktur Pajak</h1>
        <br />
        <table style="border: 1px solid #DCDCDC" cellspacing="5" width="50%">
            <tr>
                <td>Project</td>
                <td colspan="2">
                    <asp:DropDownList ID="project" runat="server" Width="180">
                        <%--<asp:ListItem Value="SEMUA">Project :</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Tgl. Terima</td>
                <td colspan="2">
                    <asp:DropDownList ID="ddltgl" runat="server" Width="180">
                        <asp:ListItem Value="">Tgl Terima :</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Status</td>
                <td>
                    <asp:RadioButton ID="statusA" runat="server" Text="TIDAK TERPAKAI" GroupName="status"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="statusB" runat="server" Text="BELUM TERPAKAI" GroupName="status" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:RadioButton ID="statusC" runat="server" Text="TERPAKAI" Checked="True" GroupName="status"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="statusD" runat="server" Text="SEMUA" GroupName="status"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <table cellspacing="1" class="tb blue-skin">
            <tr>
                <th></th>
                <th>No. Faktur Pajak</th>
                <th>No. TTS</th>
                <th>No. Kwitansi</th>
            </tr>
            <asp:PlaceHolder ID="rpt" runat="server"></asp:PlaceHolder>
        </table>
        <br />
        <asp:Button ID="del" CssClass="btn-submit button-submit2" runat="server" Text="Hapus" OnClick="del_Click" Visible="false" />
        <asp:Button ID="update" CssClass="btn-submit button-submit2" runat="server" Text="Update" OnClick="update_Click" />

        <script type="text/javascript">
            function call(nomor) {
                popEditTTS(nomor);
            }
        </script>

    </form>
</body>
</html>
