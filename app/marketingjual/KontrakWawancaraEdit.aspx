<%@ Reference Page="~/Log.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakWawancaraEdit" CodeFile="KontrakWawancaraEdit.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Edit Wawancara</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Edit Wawancara">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman proses KPR?')) document.getElementById('cancel').click()">
    <script type="text/javascript" src="/Js/Common.js"></script>
    <script type="text/javascript" src="/Js/NumberFormat.js"></script>
    <form id="Form1" method="post" runat="server" class="cnt">
        <input style="display: none">
        <h1 class="title title-line">Edit Wawancara
        </h1>
        <table cellspacing="5">
            <tr>
                <td>No. Kontrak</td>
                <td>:</td>
                <td>
                    <asp:Label ID="nokontrak" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td>Unit</td>
                <td>:</td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td>Customer</td>
                <td>:</td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
        </table>
        <br>
        <table cellspacing="5">
            <tr>
                <td>Status</td>
                <td>:</td>
                <td>
                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblStatus_SelectedIndexChanged">
                        <asp:ListItem>BELUM DITENTUKAN</asp:ListItem>
                        <asp:ListItem>TIDAK PERLU</asp:ListItem>
                        <asp:ListItem>DIJADWALKAN</asp:ListItem>
                        <asp:ListItem>SELESAI</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <table id="dijadwalkan" runat="server" cellspacing="5">
            <tr>
                <td>Target Wawancara</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="tbTarget" runat="server" CssClass="txt_center" Width="75" Font-Size="8"></asp:TextBox>
                    <label for="tbTarget" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="lblTarget" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Lokasi Wawancara</td>
                <td>:</td>
                <td>
                    <asp:RadioButtonList ID="rblLokasi" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">ON THE SPOT</asp:ListItem>
                        <asp:ListItem>BANK</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Nilai Pengajuan</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="nilaipengajuan" runat="server" CssClass="txt_num" Width="150"></asp:TextBox>
                    <asp:Label ID="nilaipengajuanc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="selesai" runat="server" cellspacing="5">
            <tr>
                <td>Tgl. Wawancara</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="tbTgl" runat="server" CssClass="txt_center" Width="75" Font-Size="8"></asp:TextBox>
                    <label for="tbTgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="lblTgl" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top">Keterangan</td>
                <td valign="top">:</td>
                <td>
                    <asp:TextBox ID="tbKet" runat="server" CssClass="txt" TextMode="MultiLine" Rows="5" Columns="40"></asp:TextBox></td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
                <td>
                    <input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
                        runat="server">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
