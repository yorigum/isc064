<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToolTipSitePlan.aspx.cs" Inherits="ISC064.LAUNCHING.ToolTipSitePlan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="background-color: #1a1a00; color: white;">
                <tr>
                    <td>Nama Blok :</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="namablok" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>NDR :</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="unit" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Tipe :</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="tipe" runat="server" />
                    </td>
                </tr>
             <%--   <tr>
                    <td>Luas Netto</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lsnett" runat="server" />
                    </td>
                </tr>--%>
                <tr>
                    <td>Luas Semi Gross :</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lsg" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Posisi Rumah :</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="arahhadap" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Table ID="listharga" runat="server" GridLines="Both" CellPadding="1"></asp:Table>
                    </td>
                </tr>
                <%-- <tr>
                    <td>Harga KPA :</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="hargakpa" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Cash Keras / 3 Kali :</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="cashkeras" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Cash 12 Kali :</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="cash12" runat="server" />
                    </td>
                </tr>--%>
            </table>

        </div>
        <script type="text/javascript">
           
        </script>
    </form>
</body>
</html>
