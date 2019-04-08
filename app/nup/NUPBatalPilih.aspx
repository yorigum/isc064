<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NUPBatalPilih.aspx.cs" Inherits="ISC064.NUP.NUPBatalPilih" %>

<!DOCTYPE html>

<html>
<head>
    <title>Pilih Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit Launching - Pilih Unit">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
    <div class="tabdata">
        <div class="pad">
            <table cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td width="400">
                        <table cellspacing="5">
                            <tr>
                                <td colspan="5">
                                    <p style="font-size: 24px;">
                                        Cancel Closing NUP</p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p style="font-size: 20px;">
                                        No. NUP</p>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <p style="font-size: 20px;">
                                        <b>
                                            <asp:Label ID="priority" runat="server"></asp:Label></b></p>
                                    <asp:Label ID="priorityc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p style="font-size: 20px;">
                                        Nama Customer</p>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <p style="font-size: 20px;">
                                        <b>
                                            <asp:Label ID="namacust" runat="server"></asp:Label></b></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p style="font-size: 20px;">
                                        No. Unit</p>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <p style="font-size: 20px;">
                                        <b>
                                            <asp:Label ID="nomorunit" runat="server"></asp:Label></b></p>
                                    <asp:Label ID="nomorunitc" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p style="font-size: 20px;">
                                        Tipe Unit</p>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <p style="font-size: 20px;">
                                        <b>
                                            <asp:Label ID="tipe" runat="server"></asp:Label></b></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p style="font-size: 20px;">
                                        Luas</p>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <p style="font-size: 20px;">
                                        <b>
                                            <asp:Label ID="luas" runat="server"></asp:Label>
                                            m<sup>2</sup></b></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p style="font-size: 20px;">
                                        Harga</p>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <p style="font-size: 20px;">
                                        <b>
                                            <asp:Label ID="harga" runat="server"></asp:Label></b></p>
                                </td>
                            </tr>
                        </table>
                        <table height="50">
                            <tr>
                                <td colspan="5">
                                    Apakah Anda yakin membatalkan unit
                                    <asp:Label ID="unitpilih" runat="server"></asp:Label>
                                    ?
                                </td>
                            </tr>
                            <tr>
                                <%--<td style="display: none;">
                                    <asp:Button ID="ok" runat="server" CssClass="btn" Text="OK" Width="75" OnClick="ok_Click">
                                    </asp:Button>
                                </td>--%>
                                <td>
                                    <asp:Button ID="save" runat="server" CssClass="btn" Text="Yes" Width="75" AccessKey="a"></asp:Button>
                                </td>
                                <td>
                                    <input id="cancel" type="button" onclick="window.history.back()" class="btn" value="Cancel"
                                        style="width: 75px">
                                </td>
                                <td style="padding-left: 10px">
                                    <p class="feed">
                                        <asp:Label ID="feed" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="460">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
