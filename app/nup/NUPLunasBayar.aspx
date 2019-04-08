<%@ Page language="c#" Inherits="ISC064.NUP.NUPLunasBayar" CodeFile="NUPLunasBayar.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP - Pelunasan Registrasi NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Pelunasan Registrasi NUP (Hal. 1)">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1>Pelunasan Registrasi NUP</h1>
        <p style="font-size: 8pt; color: #666;">Halaman 1 dari 3</p>
        <br />
        <br />
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>
                    <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="150"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="project"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display"
                        OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        Daftar NUP yang belum melakukan pelunasan kedua.
                    <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="NUP" DataField="NUP" />
                            <asp:BoundField HeaderText="Customer " DataField="Customer" />
                            <asp:BoundField HeaderText="No. Unit " DataField="Unit" />                            
                            <asp:BoundField HeaderText="" DataField="Act" />
                        </Columns>
                    </asp:GridView>
        <script language="javascript">
            function call(nomor) {
                popNUP(nomor);
            }
        </script>
    </form>
</body>
</html>
