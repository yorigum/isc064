<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnitPetaBooking1.aspx.cs" Inherits="ISC064.LAUNCHING.UnitPetaBooking1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Booking Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit Launching - Pilih Unit">
    <style type="text/css">
        .style1
        {
            width: 250px;
        }
        .style2
        {
            width: 326px;
        }
    </style>
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
    <div class="tabdata">
        <div class="pad">
            <table cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td>
                        <img src="Image/GEMCity.png" style="width: 500px; height: 500px;">
                    </td>
                    <td>
                        <table cellspacing="5" cellpadding="5">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="style1">
                                    <p style="font-size: 20px;">
                                        No. NUP</p>
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="style2">
                                    <p style="font-size: 20px;">
                                        <b>
                                            <asp:Label ID="priority" runat="server"></asp:Label></b>
                                        <asp:Label ID="priorityc" runat="server" CssClass="err"></asp:Label></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="style1">
                                    <p style="font-size: 20px;">
                                        Nama Customer</p>
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="style2">
                                    <p style="font-size: 20px;">
                                        <b>
                                            <asp:Label ID="namacust" runat="server"></asp:Label></b></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="style1">
                                    <p style="font-size: 20px;">
                                        No. Unit</p>
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="style2">
                                    <p style="font-size: 20px;">
                                        <b>
                                            <asp:Label ID="nomorunit" runat="server"></asp:Label></b>
                                        <asp:Label ID="nomorunitc" runat="server" CssClass="err"></asp:Label></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="style1">
                                    <p style="font-size: 20px;">
                                        Status Unit</p>
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="style2">
                                    <p style="font-size: 20px;">
                                        <b>
                                            <asp:Label ID="statusunit" runat="server"></asp:Label></b>
                                        <asp:Label ID="statusunitc" runat="server" CssClass="err"></asp:Label></p>
                                </td>
                            </tr>
                        </table>
                        <table height="50" cellpadding="5" cellspacing="5">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="6" style="font-size: medium">
                                    Apakah Anda yakin memilih unit
                                    <asp:Label ID="unitpilih" runat="server"></asp:Label>
                                    ?
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="display: none;">
                                    <asp:Button ID="ok" runat="server" CssClass="btn" Text="OK" Width="75" OnClick="ok_Click">
                                    </asp:Button>
                                </td>
                                <td id="tdsave" runat="server">
                                    <asp:Button ID="save" runat="server" CssClass="btn" Text="Yes" Width="75" AccessKey="a"
                                        OnClick="save_Click"></asp:Button>
                                </td>
                                <td>
                                    <asp:Button ID="cancel" runat="server" CssClass="btn" Text="No" Width="75" AccessKey="n"
                                        OnClick="cancel_Click"></asp:Button>
                                </td>
                                <td style="padding-left: 10px">
                                    <p class="feed">
                                        <asp:Label ID="feed" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
