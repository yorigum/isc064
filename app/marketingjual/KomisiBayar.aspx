<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.KomisiBayar" CodeFile="KomisiBayar.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Bayar Komisi</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Bayar Komisi">

    <script language="javascript" type="text/javascript" src="/Js/NumberFormat.js"></script>

    <script language="javascript" src="/Js/Common.js"></script>

    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body onkeyup="if(event.keyCode==27&&confirm('Kembali ke halaman jadwal komisi?')) document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
    <div class="pad">
        <table cellpadding="5">
            <tr>
                <td>
                    No. Kontrak
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="lblNoKontrak" runat="server" Font-Bold="True" Font-Size="14pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Penerima
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="lblAgent" runat="server"></asp:Label>
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
                    <asp:Label ID="lblCustomer" runat="server"></asp:Label>
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
                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <hr style="margin: 0px; color: silver; border-bottom: silver 1px dashed" noshade
            size="1">
        <table cellpadding="5">
            <tr>
                <td>
                    No. Nota
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="tbNota" runat="server" CssClass="txt"></asp:TextBox><asp:Label ID="lblNotac"
                        runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Tgl. Bayar
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="tbTglBayar" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                    <label for="tbTglBayar" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="lblTglBayarc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Nilai Bayar
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="tbNilai" runat="server" CssClass="txt_num"></asp:TextBox><asp:Label
                        ID="lblNilaic" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:Button ID="btnOK" runat="server" CssClass="btn" Width="75" Text="OK" OnClick="btnOK_Click">
                    </asp:Button>
                </td>
                <td>
                    <asp:Button ID="cancel" runat="server" CssClass="btn" Width="75" Text="Cancel" OnClick="cancel_Click" >
                    </asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <asp:CheckBox ID="cbStatus" runat="server" Visible="False"></asp:CheckBox>

    </form>
</body>
</html>
