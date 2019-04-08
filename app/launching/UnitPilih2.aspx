<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnitPilih2.aspx.cs" Inherits="ISC064.LAUNCHING.UnitPilih2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tabel Stok</title>
</head>
<style>
    .nav2 {
        border: 0px;
        background-color: #EEEEEE;
        font: 14pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 200;
        height: 30px;
    }

        .nav2:hover {
            border: 1px;
            background-color: #666;
        }
</style>
<body>
    <form id="form1" runat="server">
        <div style="float: left; width: 10%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="ClosingNUP.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_prev_c.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: right; width: 10%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="/Gateway.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_gateway2.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px">
                        <a href="/SignOut.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_out.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabdata">
            <div class="pad" style="font-size: medium">
                <br />
                <table>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>
                            <img src="Image/logosavasa.png" width="215px" height="65px" align="middle" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td valign="top">
                            <table>
                                <tr>
                                    <td>
                                        <p style="font-size: 20px;">
                                            No. NUP
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="priority" runat="server"></asp:Label></b>
                                        </p>
                                        <asp:Label ID="priorityc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style="font-size: 20px;">
                                            Nama Customer
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="namacust" runat="server"></asp:Label></b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style="font-size: 20px;">
                                            Cara Bayar
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="CB" runat="server"></asp:Label></b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style="font-size: 20px;">
                                            Price List
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <p style="font-size: 20px;">
                                            <b>Rp. <asp:Label ID="PL" runat="server"></asp:Label></b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style="font-size: 20px;">
                                            Jalan
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="jalan" runat="server"></asp:Label></b>
                                        </p>
                                        <asp:Label ID="jalanc" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style="font-size: 20px;">
                                            No. Unit
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="nomorunit" runat="server"></asp:Label></b>
                                        </p>
                                        <asp:Label ID="nomorunitc" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style="font-size: 20px;">
                                            Tipe Unit
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="tepe" runat="server"></asp:Label></b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style="font-size: 20px;">
                                            Luas Tanah
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="luast" runat="server"></asp:Label>
                                                m<sup>2</sup></b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style="font-size: 20px;">
                                            Luas Bangunan
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="luasb" runat="server"></asp:Label>
                                                m<sup>2</sup></b>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <p style="font-size: x-large">
                            Terima Kasih Anda Telah Memilih Unit
                            <asp:Label ID="nounit" runat="server" CssClass="err"></asp:Label>
                        </p>
                        </td>
                    </tr>
                </table>
              <%--  <br />
                <p style="font-size: x-large">
                    <asp:Button ID="closingcancel" runat="server" CssClass="nav2" Text="Cancel"
                        Width="200" OnClick="closingcancel_Click"></asp:Button>
                </p>--%>
            </div>
        </div>
    </form>
</body>
</html>
