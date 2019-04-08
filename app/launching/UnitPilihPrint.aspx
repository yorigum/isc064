<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnitPilihPrint.aspx.cs" Inherits="ISC064.LAUNCHING.UnitPilihPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tabel Stok</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tabdata">
        <div class="pad" style="font-size: medium">
            <br />
            <table>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <img src="Image/dave.png" width="400px" height="500px" align="middle" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td valign="top">
                        <table cellspacing="5">
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
                                        <b>Rp.
                                            <asp:Label ID="harga" runat="server"></asp:Label></b></p>
                                </td>
                            </tr>
                        </table>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <p style="font-size: x-large">
                            Terima Kasih Anda Telah Memilih Unit
                            <asp:Label ID="nounit" runat="server" CssClass="err"></asp:Label></p>
                    </td>
                </tr>
            </table>
            <br />
            <p style="font-size: x-large">
                <asp:Button ID="print" runat="server" CssClass="btn" Text="Print" Width="75"></asp:Button>
            </p>
        </div>
    </div>
    </form>
</body>
</html>
