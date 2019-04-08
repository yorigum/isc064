<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KomisiEditDataOver.aspx.cs"
    Inherits="ISC064.MARKETINGJUAL.KomisiEditDataOver" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Edit Jadwal Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Komisi - Edit Jadwal Komisi">
</head>
<body onkeyup="if(event.keyCode==27&&confirm('Kembali ke halaman jadwal komisi?')) document.getElementById('cancel').click()">

    <script language="javascript" src="/Js/Common.js"></script>

    <script language="javascript" src="/Js/NumberFormat.js"></script>

    <form id="Form1" method="post" runat="server" class="cnt">
    <h1 style="padding: 5">
        Data Penerima Komisi Overriding
    </h1>
    <br />
    <table cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td>
                <table>
                    <tr>
                        <td>
                            General Manager
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="GeneralManager" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sales Manager
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="SalesManager" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Admin Sales
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="AdminSales" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Project Manager
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ProjectManager" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Kepala Unit Sales
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="KepalaUnitSales" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Marketing Support
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="MarketingSupport" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Billing and Collection
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="Collection" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="cross1" runat="server" visible="false">
                        <td>
                            General Manager Cross Selling
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="GMCross" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="cross2" runat="server" visible="false">
                        <td>
                            Sales Manager Cross Selling
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="SMCross" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right">
                            <asp:Button ID="save" runat="server"  Text="Save" onclick="save_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
            <td width="400" align="right">
                <table cellspacing="5">
                    <tr>
                        <td>
                            No. Kontrak
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="nokontrak" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Unit
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Customer
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sales
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p class="feed" style="padding-left: 5">
                    <asp:Label ID="feed" runat="server"></asp:Label>
                </p>
            </td>
        </tr>
    </table>
    <br />
    </form>
</body>
</html>
