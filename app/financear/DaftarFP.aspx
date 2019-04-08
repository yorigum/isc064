<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.DaftarFP" CodeFile="DaftarFP.aspx.cs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Daftar FP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar FP">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="pop" onkeyup="if(event.keyCode==27){window.close()}" onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <div class="pad">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="search" runat="server" CssClass="btn" Text="Search" AccessKey="s"
                            OnClick="search_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <br>
        <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
            <asp:TableRow HorizontalAlign="Left" VerticalAlign="Bottom">
                <asp:TableHeaderCell Width="110">No. FP</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="110">Tgl Terima FP</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <input type="text" style="display: none;">

        <script type="text/javascript">
            function call(nomor, ctrl1,ctrl2) {
                window.close();
                window.opener.call2(nomor, ctrl1,ctrl2);

            }

            function callSource(nomor, source) {
                window.close();
                dialogArguments.callSource(nomor, source);

            }
        </script>

    </form>
</body>
</html>
